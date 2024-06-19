using MauMau.API.Models;
using Microsoft.EntityFrameworkCore;
/*
 *  (C) by Akama Aka
 *  LICENSE: ASPL 1.0 | https://licenses.akami-solutions.cc/
 *
 */
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CounterContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MauMauAPIContext")));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();