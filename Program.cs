using Divination.Data.Repository;
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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();