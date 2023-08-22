using Microsoft.EntityFrameworkCore;
using WebApi.Dal.Context;
using AutoMapper;
using WebApi.Interfaces;
using WebApi.Services;
using WebApi.Dal.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ProjectContext>(options =>
                                                           options.UseSqlServer(builder.Configuration
                                                                                   .GetConnectionString("WebApiDatabase")), ServiceLifetime.Transient);

builder.Services.AddScoped<ProjectContext, ProjectContext>();
builder.Services.AddScoped<UnitOfWork, UnitOfWork>();


builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IIncidentService, IncidentService>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
