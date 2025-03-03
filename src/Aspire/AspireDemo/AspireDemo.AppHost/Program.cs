var builder = DistributedApplication.CreateBuilder(args);

//Uncomment the lines that use 'cache' to use Redis as a distributed cache

//var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.AspireDemo_ApiService>("apiservice");

builder.AddProject<Projects.AspireDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    //.WithReference(cache)
    //.WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
