using Divination.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

var rulesRepository = new  RulesRepository();
builder.Services.AddSingleton<IRulesRepository>(rulesRepository);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

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