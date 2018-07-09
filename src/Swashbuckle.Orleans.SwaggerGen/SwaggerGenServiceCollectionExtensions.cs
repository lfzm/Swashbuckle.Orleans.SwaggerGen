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
            OrleansSwaggerGenOptions swaggerGenOptions = new OrleansSwaggerGenOptions();
            orleansOption.Invoke(swaggerGenOptions);

            services.AddSwaggerGen(opt =>
            {
                opt.ParameterFilter<GrainKeyParmeterFilter>(swaggerGenOptions);
                swaggerAction?.Invoke(opt);
            });
            services.Configure<OrleansSwaggerGenOptions>(opt=>
            {
                opt.BasePath = swaggerGenOptions.BasePath;
                opt.DocumentName = swaggerGenOptions.DocumentName;
                opt.GrainAssembly = swaggerGenOptions.GrainAssembly;
                opt.GrainInterfaceGrainKeyAsName = swaggerGenOptions.GrainInterfaceGrainKeyAsName;
                opt.GrainInterfaceNameExtractRegexString = swaggerGenOptions.GrainInterfaceNameExtractRegexString;
                opt.Host = swaggerGenOptions.Host;
                opt.Schemes = swaggerGenOptions.Schemes;
            });
            services.AddSingleton<IApiDescriptionGroupCollectionProvider, OrleansApiDescriptionGroupCollectionProvider>();
            return services;
        }


    }
}
