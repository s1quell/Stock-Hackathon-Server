#region Usings

using System.Text;
using Hackathon.Core.Configuration;
using Hackathon.Core.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

// ----- Конфигурирование конфигураторов ----- //
JwtConfigurator.Audience = builder.Configuration["JWT:Audience"];
JwtConfigurator.Issue = builder.Configuration["JWT:Issue"];
JwtConfigurator.SecurityKey = builder.Configuration["JWT:SecurityKey"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            
            // Строка, представляющая издателя
            ValidIssuer = JwtConfigurator.Issue,
            
            // Будет ли валидироваться потребитель токена
            ValidateAudience = true,
            
            // Установка потребителя токена
            ValidAudience = JwtConfigurator.Audience,
            
            // Будет ли валидироваться время существования
            ValidateLifetime = true,
            
            // Установка ключа безопасности
            IssuerSigningKey = JwtConfigurator.GetSymmetricSecurityKey(),
            
            // Валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Редирект с HTTP на HTTPS
app.UseHttpsRedirection();

// Добавление авторизации и аудентификации пользователя
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ----- Дополнительные сервисы ----- //

// Подключение базы данных
var login = app.Configuration["Database:Login"];
var password = app.Configuration["Database:Password"];
var schema = app.Configuration["Database:Schema"];
var host = app.Configuration["Database:Host"];

// DON'T DELETE
try
{
    DatabaseController.Init(host, login, password, schema);
}
catch (Exception e)
{
    app.Logger.LogCritical(e.Message);
}

app.Run();