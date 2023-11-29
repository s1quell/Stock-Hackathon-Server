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
    public string? Username { get; set; }
    
    [NotNull]
    public string? Password { get; set; }
    
    [NotNull]
    public string? Role { get; set; }
    
    /// <summary>
    /// ФИО пользователя
    /// </summary>
    public string? Name { get; set; }
    
    public string? RegistrationDate { get; set; }
    
    public string? LastLogin { get; set; }
}