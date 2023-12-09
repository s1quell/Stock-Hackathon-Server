using System.ComponentModel.DataAnnotations;

namespace Hackathon.Domain.ApiModels.Auth;

public class RegistrationModel
{
    /// <summary>
    /// Логин пользователя в системе. Рекомендуется использовать почту Email
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Логин обязателен для записи в базу")]
    public string? Email { get; set; }
    
    /// <summary>
    /// Пароль для идентификации пользователя
    ///
    /// Не использовать хеширование!
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Пароль обязателен")]
    public string? Password { get; set; }
}