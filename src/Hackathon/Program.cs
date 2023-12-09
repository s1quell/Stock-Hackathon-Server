#region Usings

using Hackathon.Core.Configuration;
using Hackathon.Core.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

#endregion

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80);
    
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

// ------ Конфигурирование отображения документации ------ //
builder.Services.AddSwaggerGen(options =>
{
    var basePath = AppContext.BaseDirectory;

    var xmlPath = Path.Combine(basePath, "Hackathon.xml");
    options.IncludeXmlComments(xmlPath);
    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Введите JWT токен авторизации.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0.1",
        Title = "API приложение для Хакатона",
        Description = "Сервер автоматизации логистики в торговле на маркетплейсах. Проект написан на языке C# с использованием фрейморка ASP.NET Core\n\nПочти на всех методах требуется авторизация пользователя в системе",
        Contact = new OpenApiContact
        {
            Name = "Разработкой проекта занималась команда ODIN",
            Url = new Uri("https://t.me/@dequizz")
        }
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
            },
            new List<string>()
        }
    });
});


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

// ------ Конфигурирование документации API ------ //
app.UseSwagger();
app.UseSwaggerUI();


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