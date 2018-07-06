using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.Orleans.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerGenServiceCollectionExtensions
    {
        public static IServiceCollection AddOrleansSwaggerGen(
            this IServiceCollection services,
            Action<OrleansSwaggerGenOptions> orleansOption, Action<SwaggerGenOptions> swaggerAction = null)
        {
            services.AddSwaggerGen(swaggerAction);
            services.Configure<OrleansSwaggerGenOptions>(option =>
            {
                orleansOption.Invoke(option);
            });
            services.AddSingleton<IApiDescriptionGroupCollectionProvider, OrleansApiDescriptionGroupCollectionProvider>();
            return services;
        }


    }
}
