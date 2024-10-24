using WebRoster.Data;
using AutoMapper;
using WebRoster.Models;
using WebRoster.Services;
using WebRoster.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using WebRoster.Utils.Generators;
using WebRoster.Utils.Mappers;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration["ConnectionString"]!;
builder.Services.AddDbContext<RosterContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ICourseRepo, CourseRepo>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IGenerator, UserGenerator>();
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddControllers();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Welcome to WebRosterApplication!");

app.UseRouting();
app.UseEndpoints(endpoints => { _ = endpoints.MapControllers(); });

app.Run();
