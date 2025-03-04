using System.Threading.RateLimiting;
using Divination.Data.Repository;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var rulesRepository = new RulesRepository();
builder.Services.AddSingleton<IRulesRepository>(rulesRepository);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Divination API",
        Version = "v1.0.0",
        Description = "This is a Web API for fetching rules text from Magic: The Gathering's comprehensive rules."
    });
});
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 100;
        options.Window = TimeSpan.FromSeconds(10);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 5;
    });
});

var app = builder.Build();

app.UseSwagger(options => { options.RouteTemplate = "divination/{documentName}/swagger.json"; });
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/divination/v1/swagger.json", "Divination API");
    options.RoutePrefix = "divination";
});

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();

app.Run();