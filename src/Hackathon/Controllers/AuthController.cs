using Hackathon.Core.Configuration;
using Hackathon.Core.Database;
using Hackathon.Core.Helpers;
using Hackathon.Core.Models;
using Hackathon.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    #region Private Members

    private readonly ILogger<HomeController> _logger;
    
    #endregion

    #region Constructor

    public AuthController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    #endregion

    #region Public Methods Post
    
    /// <summary>
    /// POST метод авторизации пользователя в системе
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    /// <param name="password">Пароль пользователя в системе</param>
    /// <returns>Ответ, который содержит в себе специальный идентификационный токен</returns>
    [HttpPost]
    public async Task<IActionResult> Login(string? username, string? password)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password)
               )
                return BadRequest("Неправльные данные для авторизации");

            var user = await DatabaseController.GetInstance().Users!
                .FirstOrDefaultAsync(x => x.Username == username && x.Password == Sha256Helper.Convert(password));

            // Отрабатываем ошибку
            if (user == null)
                return NotFound("Неверный логин или пароль");

            // Создаем ответ на авторизованного пользователя
            var data = new
            {
                Token = new AuthModel().GetToken(user.Username, user.Role, user.Id),
                Lifetime = JwtConfigurator.LifeTime,
                Role = user.Role
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
    /// Регистрация пользователя в системе производится только администратором
    /// </summary>
    /// <param name="login">Логин, который используется в формате регистрации пользвователя</param>
    /// <param name="password">Пароль пользователя в системе, через который он будет проводить регистрацию</param>
    /// <returns>HTTP ответ с кодами 400 и 200</returns>
    [HttpPost(Name = "Register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register(string? login, string? password)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password)
               )
                return BadRequest("Неправльные данные запроса");

            var user = await DatabaseController.GetInstance().Users!
                .FirstOrDefaultAsync(x => x.Username == login);

            // Отрабатываем ошибку уже созданного пользователя
            if (user != null)
                return BadRequest("Такой пользователь уже существует");

            await DatabaseController.GetInstance().Users!.AddAsync(new User()
            {
                Username = login,
                Password = Sha256Helper.Convert(password),
                Role = "Client"
            });

            return Ok($"Пользователь с логином {login} добавлен");
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