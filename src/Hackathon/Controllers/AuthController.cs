using Hackathon.Core.Configuration;
using Hackathon.Core.Database;
using Hackathon.Core.Helpers;
using Hackathon.Core.Models;
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
                Token = new AuthModel().GetToken(user.Username, user.Role),
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

    #endregion
}