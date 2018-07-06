# Swashbuckle.Orleans.SwaggerGen

## Orleans based on Swashbuckle survival Swagger document component

IServiceCollection configuration.

```
services.AddOrleansSwaggerGen((OrleansSwaggerGenOptions options) =>
    {
        options.GrainAssembly = typeof(UserService).Assembly;
        options.DocumentName = "uc";
        options.Host = "doc.xxx.com/uc/";
        options.Schemes.Add("http");
        options.GrainInterfaceGrainKeyAsName.Add(typeof(IUserService), "userId");

    }, (SwaggerGenOptions options) =>
    {
        var basePath = Path.GetDirectoryName(typeof(IUserService).Assembly.Location);
        var xmlPath = Path.Combine(basePath, "Users.xml");
        options.SwaggerDoc("uc", new Swashbuckle.AspNetCore.Swagger.Info()
        {
            Title = "User API",
            Version = "1.0.0"
        });
        options.IncludeXmlComments(xmlPath);
    });
```

Get the survival swagger Json documentation.

```
string json = await Startup.CreateCluster().GrainFactory.GetGrain<ISwaggerGenGrain>(Guid.NewGuid()).Generator();

```
  
