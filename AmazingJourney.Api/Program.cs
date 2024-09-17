using AmazingJourney.Application.Services;
using AmazingJourney.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using AmazingJourney.Application.Mappings;
using AutoMapper;
using AmazingJourney.Application.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);


// Đăng ký ICategoryService
builder.Services.AddMvc();
// Đăng ký AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IHotelImageService, HotelImageService>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("AmazingJourney.Infrastructure"))
);


builder.Services.AddEndpointsApiExplorer();

// Cấu hình Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AmazingJourney.Api", Version = "v1" });

    // Cấu hình hỗ trợ multipart/form-data cho các endpoint có upload file
    c.OperationFilter<FileUploadOperationFilter>();
});

builder.Services.AddSwaggerGen();



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