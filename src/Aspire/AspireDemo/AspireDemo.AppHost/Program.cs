var builder = DistributedApplication.CreateBuilder(args);

//Uncomment the lines that use 'cache' to use Redis as a distributed cache

//var cache = builder.AddRedis("cache");

// https://learn.microsoft.com/en-us/dotnet/aspire/extensibility/secure-communication-between-integrations
// Normally, you would use a secret manager to store these values
var mailDevUsername = builder.AddParameter("maildev-username");
var mailDevPassword = builder.AddParameter("maildev-password");
var maildev = builder.AddMailDev(
    name: "maildev",
    userName: mailDevUsername,
    password: mailDevPassword);

var apiService = builder.AddProject<Projects.AspireDemo_ApiService>("apiservice")
    .WithReference(maildev);

builder.AddProject<Projects.AspireDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    //.WithReference(cache)
    //.WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithReference(maildev);

builder.Build().Run();
