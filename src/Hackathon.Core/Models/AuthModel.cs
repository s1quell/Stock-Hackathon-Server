#region Usings

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Hackathon.Core.Configuration;
using Hackathon.Core.Exceptions;
using Microsoft.IdentityModel.Tokens;

#endregion


namespace Hackathon.Core.Models;

/// <summary>
/// Модель используется для добавления прав пользователю системы
/// </summary>
public class AuthModel
{
    #region Public Methods

    /// <summary>
    /// Метод позволяет получить уникальный токен для авторизации пользователя в системе
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    /// <param name="role">Роль пользователя в системе</param>
    /// <returns>JWT-токен в формате <see cref="string"/></returns>
    /// <exception cref="AuthException">Ошибка введенных данных</exception>
    public string GetToken(string? username, string? role)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(role))
            throw new AuthException("Неверно введены данные о пользователе для идентификации в системе");
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role),
        };
        
        var jwt = new JwtSecurityToken(
            issuer: JwtConfigurator.Issue,
            audience: JwtConfigurator.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(JwtConfigurator.LifeTime)), 
            signingCredentials: new SigningCredentials(JwtConfigurator.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    #endregion
}