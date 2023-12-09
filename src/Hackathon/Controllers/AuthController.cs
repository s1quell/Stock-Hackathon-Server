#region Usings

using Hackathon.Core.Configuration;
using Hackathon.Core.Database;
using Hackathon.Core.Helpers;
using Hackathon.Core.Models;
using Hackathon.Domain.ApiModels.Auth;
using Hackathon.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Hackathon.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    #region Private Members

    private readonly ILogger<AuthController> _logger;
    
    #endregion

    #region Constructor

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    #endregion

    #region Public Methods Post

    // TODO пример запросов
    
    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <param name="loginModel">Модель представления данных для авториазации в системе</param>
    /// <returns>Ответ, который содержит в себе специальный идентификационный токен</returns>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Пользователя не существует или неверный логин или пароль</response>
    [HttpPost]
    public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(loginModel.Email) ||
                string.IsNullOrWhiteSpace(loginModel.Password)
               )
                return BadRequest("Неправльные данные для авторизации");

            var user = await DatabaseController.GetInstance().Users!
                .FirstOrDefaultAsync(x => x.Email == loginModel.Email && x.Password == Sha256Helper.Convert(loginModel.Password));

            // Отрабатываем ошибку
            if (user == null)
                return NotFound("Неверный логин или пароль");

            // Создаем ответ на авторизованного пользователя
            var data = new
            {
                Token = new AuthModel().GetToken(user.Email, user.Role, user.Id),
                Lifetime = JwtConfigurator.LifeTime,
                Role = user.Role,
                ClientId = user.Id,
            };

            return Ok(data);
        }
        catch (Exception e)
        {
            // TODO Сделать полноценную обработку ошибок
            _logger.LogError(e.Message);
            
            return BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Регистрация пользователя. Только администратор
    /// </summary>
    /// <param name="registrationModel">Модель представления данных авторизации</param>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Ошибка API или такой пользователь уже существует</response>
    [HttpPost(Name = "Register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register([FromBody]RegistrationModel registrationModel)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(registrationModel.Email) ||
                string.IsNullOrWhiteSpace(registrationModel.Password)
               )
                return BadRequest("Неправильные данные запроса");

            var user = await DatabaseController.GetInstance().Users!
                .FirstOrDefaultAsync(x => x.Email == registrationModel.Email);

            // Отрабатываем ошибку уже созданного пользователя
            if (user != null)
                return BadRequest("Такой пользователь уже существует");

            await DatabaseController.GetInstance().Users!.AddAsync(new User()
            {
                Email = registrationModel.Email,
                Password = Sha256Helper.Convert(registrationModel.Password),
                Role = "Client"
            });

            await DatabaseController.GetInstance().SaveChangesAsync();
            
            return Ok($"Пользователь с логином {registrationModel.Email} добавлен");
        }
        catch (Exception e)
        {
            // TODO Сделать полноценную обработку ошибок
            _logger.LogError(e.Message);
            
            return BadRequest(e.Message);
        }
    }
    #endregion
}