using Microsoft.EntityFrameworkCore;
using GestionProyectosYTareas.Infrastructure;
using GestionProyectosYTareas.Infrastructure.Repositories;
using GestionProyectosYTareas.Application.Services;
using GestionProyectosYTareas.Domain.Interfaces;
using GestionProyectosYTareas.Infrastructure.AppDbContext;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conn = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<GestionPTDbContext>(opts =>
    opts.UseSqlServer(conn, sqlOptions => sqlOptions.MigrationsAssembly("GestionProyectosYTareas.Infrastructure")));
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ProjectService>();

var app = builder.Build();

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
