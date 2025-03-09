var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

// https://learn.microsoft.com/en-us/dotnet/aspire/extensibility/secure-communication-between-integrations
// Normally, you would use a secret manager to store these values
var mailDevUsername = builder.AddParameter("maildev-username");
var mailDevPassword = builder.AddParameter("maildev-password");
var maildev = builder.AddMailDev(
    name: "maildev",
    userName: mailDevUsername,
    password: mailDevPassword);

var sql = builder.AddSqlServer("sql")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("PIMS");

var apiService = builder.AddProject<Projects.AspireDemo_ApiService>("apiservice")
    .WithReference(maildev)
    .WithReference(sql)
    .WaitFor(sql);

builder.AddProject<Projects.AspireDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithReference(maildev);

builder.Build().Run();
