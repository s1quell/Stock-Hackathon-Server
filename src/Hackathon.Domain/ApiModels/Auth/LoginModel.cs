using System.ComponentModel.DataAnnotations;

namespace Hackathon.Domain.ApiModels.Auth;

/// <summary>
/// Данные для авторизации
/// </summary>
public class LoginModel
{
    /// <summary>
    /// Логин пользователя в системе
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Логин обязателен для метода")]
    public string? Email { get; set; }
    
    /// <summary>
    /// Пароль для идентификации пользователя
    ///
    /// Не использовать хеширование!
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Пароль обязателен")]
    public string? Password { get; set; }
}