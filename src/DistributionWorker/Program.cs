using Application.Distribution;
using Application.Distribution.Distributors;
using Application.HttpClients;
using Application.Integrations;
using Application.Utilities;
using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Database.Repositories;
using Infrastructure.HttpClients;
using Infrastructure.Integrations;
using Microsoft.EntityFrameworkCore;
using Workers.Workers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ScheduledWorker>();

// database
builder.Services.AddDbContext<DockerTestContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");
    options.UseNpgsql(connectionString);
});
builder.Services.AddScoped<IDistributionConfigRepository, DistributionConfigRepository>();
builder.Services.AddScoped<IDistributionRepository, DistributionRepository>();

// distribution
builder.Services.AddScoped<IDistributionManager, DistributionManager>();
builder.Services.AddSingleton<IDistributor, FacebookMessageDistributor>();
builder.Services.AddSingleton<IDistributor, FacebookMessageDistributor>();
builder.Services.AddSingleton<IDistributor, FacebookPostDistributor>();
builder.Services.AddSingleton<IDistributor, EmailDistributor>();

// utilities
builder.Services.AddSingleton<IMessageCreator, MessageCreator>();
builder.Services.AddSingleton<IAssetSelector, AssetSelector>();

// integrations
builder.Services.AddSingleton<IFacebookClient, MockFacebookClient>();
builder.Services.AddSingleton<IEmailClient, MockEmailClient>();
builder.Services.AddSingleton<IAssetApi, MockApi>();
builder.Services.AddSingleton<IOrderListApi, MockApi>();
builder.Services.AddSingleton<IBriefingApi, MockApi>();

var host = builder.Build();
host.Run();
