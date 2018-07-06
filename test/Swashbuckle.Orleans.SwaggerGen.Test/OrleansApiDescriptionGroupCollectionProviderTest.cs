using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Orleans;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Swashbuckle.Orleans.SwaggerGen.Test
{
    public class OrleansApiDescriptionGroupCollectionProviderTest
    {
        [Fact]
        public void GetApiDescription()
        {
            var apiDescriptionsProvider = new OrleansApiDescriptionGroupCollectionProvider(Options.Create<OrleansSwaggerGenOptions>(new OrleansSwaggerGenOptions()
            {
                GrainAssembly = typeof(IGrainTestService).Assembly
            }));
            var gr = apiDescriptionsProvider.ApiDescriptionGroups;
            Assert.NotNull(gr);
        }
        [Fact]
        public void GetSwagger()
        {
            var swagger = Subject().GetSwagger("v1");

            JsonSerializer _swaggerSerializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None,
                ContractResolver = new SwaggerContractResolver(new JsonSerializerSettings())
            };

            var jsonBuilder = new StringBuilder();
            using (var writer = new StringWriter(jsonBuilder))
            {
                _swaggerSerializer.Serialize(writer, swagger);
            }
            string json = jsonBuilder.ToString();
        }

        private SwaggerGenerator Subject(
           Action<OrleansApiDescriptionGroupCollectionProvider> setupApis = null,
           Action<SwaggerGeneratorSettings> configure = null)
        {


            var apiDescriptionsProvider = new OrleansApiDescriptionGroupCollectionProvider(Options.Create<OrleansSwaggerGenOptions>(new OrleansSwaggerGenOptions()
            {
                GrainAssembly = typeof(IGrainTestService).Assembly
            }));
            setupApis?.Invoke(apiDescriptionsProvider);

            var options = new SwaggerGeneratorSettings();
            options.SwaggerDocs.Add("v1", new Info { Title = "API", Version = "v1" });

            configure?.Invoke(options);

            return new SwaggerGenerator(
                apiDescriptionsProvider,
                new SchemaRegistryFactory(new JsonSerializerSettings(), new SchemaRegistrySettings()),
                options
            );
        }
    }

    public interface IGrainTestService : IGrainWithIntegerKey
    {
        Task<string> Get(string name = "");
        Task<string> GetValue(UserInfo user);

        Task<Result> Add(UserInfo user);
    }

    public class IGer : Grain, IGrainTestService
    {
        public Task<string> GetValue(UserInfo user)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Add(UserInfo user)
        {
            throw new NotImplementedException();
        }
        public Task<string> Get(string name = "")
        {
            throw new NotImplementedException();
        }
    }

    public class UserInfo
    {
        [Required]
        public string Nick { get; set; }

        public string Name { get; set; }
    }

    public class Result
    {
        public string Code { get; set; }

        public string Msg { get; set; }
    }
}
