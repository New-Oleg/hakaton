using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UniversitySchedule.API.Middlewares;
using UniversitySchedule.Application.Interfaces.Repositories;
using UniversitySchedule.Application.Interfaces.Services;
using UniversitySchedule.Application.Services;
using UniversitySchedule.Domain.Entities;
using UniversitySchedule.Infrastructure.Auth;
using UniversitySchedule.Infrastructure.Data;
using UniversitySchedule.Infrastructure.Repositories;
using UniversitySchedule.Persistence;
using UniversitySchedule.UniversitySchedule.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Чтение конфигурации (appsettings.json)
IConfiguration configuration = builder.Configuration;

// ===========================
// Регистрируем сервисы
// ===========================

builder.Services.AddTransient<IEmailSender, EmailSender>();

// Используем SQLite как провайдер базы данных
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

// Регистрация репозиториев
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IUserRepository<User>, UserRepository>();
builder.Services.AddScoped<IStudentRepository<Student>, StudentRepository>();

// Регистрация сервисов
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Регистрация JWT генератора (используется в AuthService)
builder.Services.AddSingleton<JwtTokenGenerator>();

// Добавляем контроллеры
builder.Services.AddControllers();

//builder.Services.AddTransient<IEmailSender, EmailSender>();

// (Опционально) Настройка аутентификации с JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    var key = System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key)
    };
});

// Добавление Swagger для документирования API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "UniversitySchedule API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new()
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// ===========================
// Обработка исключений через кастомный Middleware
// ===========================
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    // В режиме разработки включаем Swagger UI и Developer Exception Page
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UniversitySchedule API v1");
        c.RoutePrefix = string.Empty; // Чтобы Swagger UI был доступен по корневому URL, например: http://localhost:5000/
    });
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Инициализация базы данных при старте приложения
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Применяем миграции (если они есть)
    context.Database.Migrate();

    // Инициализация базы данных
    DbInitializer.Initialize(context);
}

app.Run();

public partial class Program { }