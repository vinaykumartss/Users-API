using App.EnglishBuddy.API.Extensions;
using App.EnglishBuddy.Application;
using App.EnglishBuddy.Infrastructure;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddLog4Net(); 
builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();

builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var serviceScope = app.Services.CreateScope();
app.UseSwagger();
app.UseSwaggerUI();
app.UseErrorHandler();
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseStaticFiles();


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "wwwroot/user_images")),
    RequestPath = "/app-images"
});

app.MapControllers();
app.Run();