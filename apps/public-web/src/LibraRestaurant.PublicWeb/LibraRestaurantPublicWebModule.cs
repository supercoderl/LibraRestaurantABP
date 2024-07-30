using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Ui.LayoutHooks;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.AspNetCore.Mvc.Client;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.Account;
using LibraRestaurant.Shared.Hosting.AspNetCore;
using LibraRestaurant.CatalogService;
using LibraRestaurant.BasketService;
using LibraRestaurant.OrderingService;
using Volo.Abp.AspNetCore.SignalR;
using LibraRestaurant.PaymentService;
using Volo.Abp.AutoMapper;
using Volo.CmsKit.Public.Web;
using Microsoft.Extensions.DependencyInjection;
using LibraRestaurant.Localization;
using Volo.Abp.Http.Client;
using Polly;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;
using LibraRestaurant.PublicWeb.Menus;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Yarp.ReverseProxy.Transforms;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using LibraRestaurant.BasketService.Contracts;
using LibraRestaurant.PublicWeb.ServiceProviders;
using LibraRestaurant.PaymentService.PaymentMethods;
using Microsoft.Extensions.Configuration;
using Volo.Abp.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using LibraRestaurant.CmskitService;
using LibraRestaurant.PublicWeb.Components.Toolbar.Footer;
using LibraRestaurant.PublicWeb.Components.Toolbar.Cart;

namespace LibraRestaurant.PublicWeb
{
    [DependsOn(
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpEventBusRabbitMqModule),
        typeof(AbpAspNetCoreMvcClientModule),
        typeof(AbpAspNetCoreAuthenticationOpenIdConnectModule),
        typeof(AbpHttpClientIdentityModelWebModule),
        typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
        typeof(AbpAccountHttpApiClientModule),
        typeof(LibraRestaurantSharedHostingAspNetCoreModule),
        typeof(LibraRestaurantSharedLocalizationModule),
        typeof(CatalogServiceHttpApiClientModule),
        typeof(BasketServiceContractsModule),
        typeof(OrderingServiceHttpApiClientModule),
        typeof(AbpAspNetCoreSignalRModule),
        typeof(PaymentServiceHttpApiClientModule),
        typeof(AbpAutoMapperModule),
        typeof(CmskitServiceHttpApiClientModule),
        typeof(CmsKitPublicWebModule)
    )]
    public class LibraRestaurantPublicWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(LibraRestaurantResource),
                    typeof(LibraRestaurantPublicWebModule).Assembly
                );
            });

            PreConfigure<AbpHttpClientBuilderOptions>(options =>
            {
                options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) =>
                {
                    clientBuilder.AddTransientHttpErrorPolicy(policyBuilder =>
                        policyBuilder.WaitAndRetryAsync(
                            3,
                            i => TimeSpan.FromSeconds(Math.Pow(2, i))
                        )
                    );
                });
            });

            FeatureConfigurer.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            var configuration = context.Services.GetConfiguration();

            context.Services.AddHttpForwarderWithServiceDiscovery();

            ConfigureBasketHttpClient(context);

            context.Services.AddAutoMapperObjectMapper<LibraRestaurantPublicWebModule>();
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<LibraRestaurantPublicWebModule>(validate: true); });

            Configure<AbpLayoutHookOptions>(options =>
            {
                options.Add(
                    LayoutHooks.Body.Last,
                    typeof(FooterComponent)
                );
            });

            Configure<AbpBundlingOptions>(options =>
            {
                options.StyleBundles.Configure(
                    LeptonXLiteThemeBundles.Styles.Global,
                    bundle =>
                    {
                        bundle.AddContributors(typeof(CartWidgetStyleContributor));
                        bundle.AddFiles("/global.css");
                    }
                );
            });

            context.Services.Configure<AbpRemoteServiceOptions>(options =>
            {
                options.RemoteServices.Default =
                    new RemoteServiceConfiguration(configuration["RemoteServices:Default:BaseUrl"] ?? throw new Exception("Environment resource not found!"));
            });

            Configure<AbpMultiTenancyOptions>(options => { options.IsEnabled = true; });

            Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "LibraRestaurant:"; });

            Configure<AppUrlOptions>(options => { options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"]; });

            ConfigurePayment(configuration);

            context.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddAbpOpenIdConnect("oidc", options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.ClientId = configuration["AuthServer:ClientId"];
                    options.MetadataAddress = configuration["AuthServer:MetaAddress"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add("phone");
                    options.Scope.Add("roles");
                    options.Scope.Add("offline_access");

                    options.Scope.Add("AdministrationService");
                    options.Scope.Add("BasketService");
                    options.Scope.Add("IdentityService");
                    options.Scope.Add("CatalogService");
                    options.Scope.Add("PaymentService");
                    options.Scope.Add("OrderingService");
                    options.Scope.Add("CmskitService");

                    options.SaveTokens = true;

                    //SameSite is needed for Chrome/Firefox, as they will give http error 500 back, if not set to unspecified.
                    // options.NonceCookie.SameSite = SameSiteMode.Unspecified;
                    // options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;
                    //
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = ClaimTypes.Role,
                        ValidateIssuer = true
                    };

                    options.Events.OnAuthorizationCodeReceived = async (authContext) =>
                    {
                        var userLoggedInEto = CreateUserLoggedInEto(authContext.Principal, authContext.HttpContext);
                        if (userLoggedInEto != null)
                        {
                            var eventBus =
                                authContext.HttpContext.RequestServices.GetRequiredService<IDistributedEventBus>();
                            await eventBus.PublishAsync(userLoggedInEto);
                        }
                    };

                    if (AbpClaimTypes.UserName != "preferred_username")
                    {
                        options.ClaimActions.MapJsonKey(AbpClaimTypes.UserName, "preferred_username");
                        options.ClaimActions.DeleteClaim("preferred_username");
                        options.ClaimActions.RemoveDuplicate(AbpClaimTypes.UserName);
                    }
                });

            if (Convert.ToBoolean(configuration["AuthServer:IsOnProd"]))
            {
                context.Services.Configure<OpenIdConnectOptions>("oidc", options =>
                {
                    options.MetadataAddress = configuration["AuthServer:MetaAddress"]?.EnsureEndsWith('/') +
                                              ".well-known/openid-configuration";

                    var previousOnRedirectToIdentityProvider = options.Events.OnRedirectToIdentityProvider;
                    options.Events.OnRedirectToIdentityProvider = async ctx =>
                    {
                        // Intercept the redirection so the browser navigates to the right URL in your host
                        ctx.ProtocolMessage.IssuerAddress = configuration["AuthServer:Authority"]?.EnsureEndsWith('/') +
                                                            "protocol/openid-connect/auth";

                        if (previousOnRedirectToIdentityProvider != null)
                        {
                            await previousOnRedirectToIdentityProvider(ctx);
                        }
                    };
                    var previousOnRedirectToIdentityProviderForSignOut =
                        options.Events.OnRedirectToIdentityProviderForSignOut;
                    options.Events.OnRedirectToIdentityProviderForSignOut = async ctx =>
                    {
                        // Intercept the redirection for signout so the browser navigates to the right URL in your host
                        ctx.ProtocolMessage.IssuerAddress = configuration["AuthServer:Authority"]?.EnsureEndsWith('/') +
                                                            "protocol/openid-connect/logout";

                        if (previousOnRedirectToIdentityProviderForSignOut != null)
                        {
                            await previousOnRedirectToIdentityProviderForSignOut(ctx);
                        }
                    };
                });
            }

            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"] ?? throw new Exception("Environment resource not found!"));
            context.Services
                .AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "LibraRestaurant-Protection-Keys")
                .SetApplicationName("LibraRestaurant-PublicWeb");

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new LibraRestaurantPublicWebMenuContributor(configuration));
            });

            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new LibraRestaurantPublicWebToolbarContributor());
            });

            context.Services
                .AddReverseProxy()
                .LoadFromConfig(configuration.GetSection("ReverseProxy"))
                .AddTransforms(builderContext =>
                {
                    builderContext.AddRequestTransform(async (transformContext) =>
                    {
                        transformContext.ProxyRequest.Headers
                            .Authorization = new AuthenticationHeaderValue(
                            "Bearer",
                            await transformContext.HttpContext.GetTokenAsync("access_token")
                        );
                    });
                });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.Use((ctx, next) =>
            {
                ctx.Request.Scheme = "https";
                return next();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAbpSerilogEnrichers();
            app.UseAuthorization();
            app.UseConfiguredEndpoints(endpoints =>
            {
                endpoints.MapReverseProxy();
                endpoints.MapForwarder("*/product-images/{name}", "http://_http.catalogService/product-images", "/{name}");
                endpoints.MapForwarder("/products/product-images/{name}", "http://_http.catalogService/product-images", "/{name}");
            });
        }

        private void ConfigureBasketHttpClient(ServiceConfigurationContext context)
        {
            context.Services.AddStaticHttpClientProxies(
                typeof(BasketServiceContractsModule).Assembly,
                remoteServiceConfigurationName: BasketServiceConstants.RemoteServiceName
            );

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<LibraRestaurantPublicWebModule>();
            });
        }

        private void ConfigurePayment(IConfiguration configuration)
        {
            Configure<LibraRestaurantPublicWebPaymentOptions>(options =>
            {
                options.PaymentSuccessfulCallbackUrl =
                    configuration["App:SelfUrl"]?.EnsureEndsWith('/') + "PaymentCompleted";
            });

            Configure<PaymentMethodUiOptions>(options =>
            {
                options.ConfigureIcon(PaymentMethodNames.PayPal, "fa-cc-paypal paypal");
            });
        }

        private UserLoggedInEto? CreateUserLoggedInEto(ClaimsPrincipal principal, HttpContext httpContext)
        {
            var logger = httpContext.RequestServices.GetRequiredService<ILogger<LibraRestaurantPublicWebModule>>();

            if (principal == null)
            {
                logger.LogWarning($"AuthorizationCode does not contain principal to create/update user!");
                return null;
            }

            var claims = principal.Claims.ToList();

            var userNameClaim = claims.FirstOrDefault(x => x.Type == "preferred_username");
            var emailClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            var isEmailVerified = claims.FirstOrDefault(x => x.Type == "email_verified")?.Value == "true";
            var phoneNumberClaim = claims.FirstOrDefault(x => x.Type == "phone");
            var userIdString = claims.First(t => t.Type == ClaimTypes.NameIdentifier).Value;

            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                logger.LogWarning(
                    $"Handling UserLoggedInEvent... User creation failed! {userIdString} can not be parsed!");
                return null;
            }

            return new UserLoggedInEto
            {
                Id = userId,
                Email = emailClaim?.Value,
                UserName = userNameClaim?.Value,
                Phone = phoneNumberClaim?.Value,
                IsEmailVerified = isEmailVerified
            };
        }
    }
}
