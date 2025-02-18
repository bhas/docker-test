using Application.HttpClients;
using Application.Utilities;
using Domain.Entities;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure.Cache;
using Infrastructure.Database;
using Infrastructure.Database.Repositories;
using Infrastructure.HttpClients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(options =>
{
    options.AutoTagPathSegmentIndex = 2;
    options.ShortSchemaNames = true;
    options.DocumentSettings = doc =>
    {
        doc.Title = "Docker Test";
        doc.Version = "v1";
    };
});

builder.Services.AddDbContext<DockerTestContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");
    options.UseNpgsql(connectionString);
});
builder.Services.AddSingleton<IAssetApi, MockApi>();
builder.Services.AddSingleton<IOrderListApi, MockApi>();
builder.Services.AddSingleton<IBriefingApi, MockApi>();
builder.Services.AddScoped<IDistributionConfigRepository, DistributionConfigRepository>();
builder.Services.AddScoped<IDistributionRepository, DistributionRepository>();
builder.Services.AddSingleton<ICache, Cache>();
builder.Services.AddMemoryCache();



var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    using var serviceScope = app.Services.CreateScope();
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<DockerTestContext>();
    dbContext.Database.Migrate();
}
app.UseHttpsRedirection();
app.UseFastEndpoints(options =>
{
    options.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
});
app.UseSwaggerGen();


app.Run();