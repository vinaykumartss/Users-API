using App.EnglishBuddy.API.Extensions;
using App.EnglishBuddy.Application;
using App.EnglishBuddy.Infrastructure;
using App.EnglishBuddy.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

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

app.MapControllers();
app.Run();