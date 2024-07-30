using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Swashbuckle;

namespace LibraRestaurant.Shared.Hosting.AspNetCore
{
    public static class AbpSwaggerUIBuilderExtensions
    {
        public static IApplicationBuilder UseAbpSwaggerWithCustomScriptUI(
            this IApplicationBuilder app,
            Action<SwaggerUIOptions>? setupAction = null)
        {
            var resolver = app.ApplicationServices.GetService<ISwaggerHtmlResolver>();

            return app.UseSwaggerUI(options =>
            {
                options.InjectJavascript("ui/abp.js");
                options.InjectJavascript("ui/abp.swagger.js");
                options.InjectJavascript("ui/requestinterceptor.js");
                options.IndexStream = () => resolver?.Resolver();

                setupAction?.Invoke(options);
            });
        }
    }
}
