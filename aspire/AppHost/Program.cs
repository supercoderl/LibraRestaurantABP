using LibraRestaurant.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddForwardedHeaders();

var profile = "http";

// Microservices
var administrationService =
    builder.AddProject<Projects.LibraRestaurant_AdministrationService_HttpApi_Host>("administrationService", profile);
var identityService = builder.AddProject<Projects.LibraRestaurant_IdentityService_HttpApi_Host>("identityService", profile);
var catalogService = builder.AddProject<Projects.LibraRestaurant_CatalogService_HttpApi_Host>("catalogService", null)
    .WithHttpEndpoint(name: "http", port: 5054, isProxied: false)
    .WithEndpoint(
        endpointName: "grpc",
        callback: static endpoint =>
        {
            endpoint.Port = 8181;
            endpoint.UriScheme = "http";
            endpoint.Transport = "http2";
            endpoint.IsProxied = false;
        }
    );
var basketService = builder.AddProject<Projects.LibraRestaurant_BasketService>("basketService", profile)
    .WithReference(catalogService);
var cmsKitService = builder.AddProject<Projects.LibraRestaurant_CmskitService_HttpApi_Host>("cmsKitService", profile);
var orderingService = builder.AddProject <Projects.LibraRestaurant_OrderingService_HttpApi_Host>("orderingService", profile);
var paymentService = builder.AddProject<Projects.LibraRestaurant_PaymentService_HttpApi_Host>("paymentService", profile);

// Gateways
var webGateway = builder.AddProject<Projects.LibraRestaurant_WebGateway>("webGateway");
var webPublicGateway = builder.AddProject<Projects.LibraRestaurant_WebPublicGateway>("webPublicGateway")
    .WithReference(administrationService)
    .WithReference(identityService)
    .WithReference(catalogService)
    .WithReference(basketService)
    .WithReference(cmsKitService)
    .WithReference(orderingService)
    .WithReference(paymentService);

// Apps
var publicWebApp = builder.AddProject<Projects.LibraRestaurant_PublicWeb>("public-web", "https")
    .WithExternalHttpEndpoints()
    .WithReference(catalogService)
    .WithReference(webPublicGateway);

builder.Build().Run();