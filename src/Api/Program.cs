using FastEndpoints;
using FastEndpoints.Swagger;

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




var app = builder.Build();
app.UseHttpsRedirection();
app.UseFastEndpoints();
app.UseSwaggerGen();


app.Run();