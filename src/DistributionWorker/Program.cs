using DistributionWorker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ContentDistributorWorker>();

var host = builder.Build();
host.Run();
