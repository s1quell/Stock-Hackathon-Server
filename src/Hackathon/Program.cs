
using Hackathon.Core.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Редирект с HTTP на HTTPS
app.UseHttpsRedirection();

// Добавление авторизации и аудентификации пользователя
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