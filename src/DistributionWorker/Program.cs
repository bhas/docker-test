using Application.Integrations;
using Application.MessageCreators;
using DistributionWorker;
using Domain.ValueType.AssetSelector;
using Domain.ValueType.Channels;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ContentDistributorWorker>();
builder.Services.AddSingleton<IDistributor, FacebookMessageDistributor>();
builder.Services.AddSingleton<IDistributor, FacebookPostDistributor>();
builder.Services.AddSingleton<IDistributor, EmailDistributor>();
builder.Services.AddSingleton<IMessageCreator, MessageCreator>();
builder.Services.AddSingleton<IAssetSelector, AssetSelector>();
builder.Services.AddSingleton<IFacebookClient, MockFacebookClient>();
builder.Services.AddSingleton<IEmailClient, MockEmailClient>();

var host = builder.Build();
host.Run();
