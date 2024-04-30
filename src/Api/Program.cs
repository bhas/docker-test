using Application.HttpClients;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure.Database;
using Infrastructure.HttpClients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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
    var connectionString = "Host=postgres-db;Database=docker_test;Username=postgres;Password=postgres";
    options.UseNpgsql(connectionString);
});
builder.Services.AddSingleton<IAssetApi, MockApi>();
builder.Services.AddSingleton<IOrderListApi, MockApi>();
builder.Services.AddSingleton<IBriefingApi, MockApi>();





var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    using var serviceScope = app.Services.CreateScope();
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<DockerTestContext>();
    dbContext.Database.Migrate();
}
app.UseHttpsRedirection();
app.UseFastEndpoints();
app.UseSwaggerGen();


app.Run();