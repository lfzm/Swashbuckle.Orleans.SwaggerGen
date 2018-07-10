# Swashbuckle.Orleans.SwaggerGen

## Orleans based on Swashbuckle survival Swagger document component

IServiceCollection configuration.

```
IServiceCollection services = new ServiceCollection();
services.AddTransient<ISwaggerGenGrain, SwaggerGenGrain>();
services.AddOrleansSwaggerGen((OrleansSwaggerGenOptions options) =>
{
    options.GrainAssembly = typeof(UserService).Assembly;
    options.DocumentName = "uc";
    options.Host = "www.xxx.com/cotc";
    options.Schemes.Add("http");
    options.GrainInterfaceGrainKeyAsName.Add(typeof(IUserService), new GrainKeyDescription("userId", "User Id", "UserList"));
},
(SwaggerGenOptions options) =>
{
    var basePath = Path.GetDirectoryName(typeof(IUserService).Assembly.Location);
    var xmlPath = Path.Combine(basePath, "User.xml");
    options.SwaggerDoc("cotc", new Swashbuckle.AspNetCore.Swagger.Info()
    {
        Title = "User API",
        Version = "1.0.0"
    });
    options.IncludeXmlComments(xmlPath);
});
var serviceProvider = services.BuildServiceProvider();

var ser = serviceProvider.GetRequiredService<ISwaggerGenGrain>();
string json = await ser.Generator();
```
  
