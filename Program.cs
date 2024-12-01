using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using UserRegistrationApi.Services;
using UserRegistrationApi.Repositories;
using UserRegistrationApi.Data;
using FluentValidation;
using UserRegistrationApi.Validators;
using UserRegistrationApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Cargar las variables del archivo .env
Env.Load();


// Cargar configuraciones de la base de datos desde .env
var connectionString = $"Host={Env.GetString("DB_HOST")};Database={Env.GetString("DB_NAME")};Username={Env.GetString("DB_USER")};Password={Env.GetString("DB_PASSWORD")}";

// Configuración de la base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Inyección de dependencias
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Agregar validadores usando FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();

// Agregar Swagger para la documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar controladores (esto es necesario para mapear las rutas de los controladores)
builder.Services.AddControllers();

// Agregar autorización
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>(); 

// Habilitar Swagger solo en el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuración de middlewares
app.UseAuthorization();  // Usa la autorización configurada anteriormente

// Mapear los controladores
app.MapControllers();

app.Run();
