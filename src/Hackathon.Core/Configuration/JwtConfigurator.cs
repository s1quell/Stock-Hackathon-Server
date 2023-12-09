#region Usings

using System.Text;
using Microsoft.IdentityModel.Tokens;

#endregion


namespace Hackathon.Core.Configuration;

public static class JwtConfigurator
{
    #region Public Fields
    
    /// <summary>
    /// Издатель токена
    /// </summary>
    public static string? Issue { get; set; }
    
    /// <summary>
    /// Потребитель токена
    /// </summary>
    public static string? Audience { get; set; }
    
    /// <summary>
    /// Специальный уникальный ключ авторизации
    /// </summary>
    public static string? SecurityKey { get; set; }

    /// <summary>
    /// Срок действия Токена
    /// 
    /// Значение указывается в минутах
    /// </summary>
    public static int LifeTime { get; set; } = 30;
    
    /// <summary>
    /// Метод создает симметричный ключ аудентификации, который является уникальным и не может быть изменен
    /// </summary>
    /// <returns>Ключ шифрования</returns>
    public static SymmetricSecurityKey? GetSymmetricSecurityKey()
    {
        return SecurityKey != null ? new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey)) : null;
    }
    
    #endregion
    
}