using Application;
using Core.Application.Pipelines.Caching;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Microsoft.Extensions.Configuration;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

//builder.Services.AddDistributedMemoryCache();
builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = "localhost:6379");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//if(app.Environment.IsDevelopment())
app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
