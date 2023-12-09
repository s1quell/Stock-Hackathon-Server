using System.Diagnostics.CodeAnalysis;

namespace Hackathon.Domain.Entity;

public class Order
{
    [NotNull]
    public int Id { get; set; }
    
    /// <summary>
    /// Уникальный код, который генерируется на стороне сервера
    /// </summary>
    public string? UniqueId { get; set; }
    
    /// <summary>
    /// Статус заявки в системе
    /// </summary>
    public int Status { get; set; }
    
    /// <summary>
    /// Название компании
    /// </summary>
    public string? Company { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор <see cref="User"/>.Id
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор склада
    /// </summary>
    public int StockId { get; set; }
    
    /// <summary>
    /// Дата созюдания заявки 
    /// </summary>
    public string? Date { get; set; }
    
    /// <summary>
    /// Стоимость всей заявки, который высчитывается на сервере
    /// </summary>
    public double Price { get; set; }
    
    /// <summary>
    /// Список продуктов в формате JSON
    /// </summary>
    public string? Products { get; set; }
}