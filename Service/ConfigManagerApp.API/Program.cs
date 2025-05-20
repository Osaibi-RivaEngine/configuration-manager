using ConfigManagerApp.API.Repos;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSingleton<IConfigRepo, ConfigRepo>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.MapOpenApi();
app.MapScalarApiReference(option => {
    option.WithModels(true);
    option.WithTitle("Config Manager API");
    option.WithTheme(ScalarTheme.BluePlanet);
    option.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    
});

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
