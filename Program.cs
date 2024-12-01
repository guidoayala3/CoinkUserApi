using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using UserRegistrationApi.Services;
using UserRegistrationApi.Repositories;
using UserRegistrationApi.Data;
using FluentValidation;
using UserRegistrationApi.Validators;
using UserRegistrationApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

Env.Load();


var connectionString = $"Host={Env.GetString("DB_HOST")};Database={Env.GetString("DB_NAME")};Username={Env.GetString("DB_USER")};Password={Env.GetString("DB_PASSWORD")}";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>(); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();  

app.MapControllers();

app.Run();
