using System.Diagnostics.CodeAnalysis;

namespace Hackathon.Domain.Entity;

/// <summary>
/// Сущность пользователя с полями из таблицы базы данных
/// </summary>
public class User
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    public int Id { get; set; }
    
    [NotNull]
    public string? Email { get; set; }
    
    [NotNull]
    public string? Password { get; set; }
    
    /// <summary>
    /// Роль пользователя в системе
    /// </summary>
    public string? Role { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string? Surname { get; set; }
    
    /// <summary>
    /// Номер телефона формата +79266776767
    /// </summary>
    public string? Phone { get; set; }
    
    /// <summary>
    /// Дата регистрации пользователя
    /// </summary>
    public string? RegistrationDate { get; set; }
    
    /// <summary>
    /// День рождения пользователя
    /// </summary>
    public string? DateOfBirth { get; set; }
    
    public string? Company { get; set; }
}