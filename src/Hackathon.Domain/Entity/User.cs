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
    [NotNull]
    public int Id { get; set; }
    
    [NotNull]
    public string? Username { get; set; }
    
    [NotNull]
    public string? Password { get; set; }
}