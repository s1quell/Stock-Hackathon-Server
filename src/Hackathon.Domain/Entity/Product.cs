using System.Diagnostics.CodeAnalysis;

namespace Hackathon.Domain.Entity;

public class Product
{
    [NotNull]
    public int Id { get; set; }
    
    /// <summary>
    /// Идентификатор клиента <see cref="User"/>.Id
    /// </summary>
    public int ClientId { get; set; }
    
    /// <summary>
    /// Наименование товара
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Количество определенного товара
    /// </summary>
    public int Amount { get; set; }
    
    /// <summary>
    /// Специальный артикул товара
    /// </summary>
    public string? Article { get; set; }
    
    /// <summary>
    /// Стоимость товара за единицу
    /// </summary>
    public double Price { get; set; }
    
    /// <summary>
    /// Длина товара
    /// </summary>
    public double Width { get; set; }
    
    /// <summary>
    /// Высота товара
    /// </summary>
    public double Height { get; set; }
    
    /// <summary>
    /// Ширина товара
    /// </summary>
    public double Depth { get; set; }
    
    /// <summary>
    /// Специальный баркод товара
    /// </summary>
    public int Code { get; set; }
    
    /// <summary>
    /// Описание товара
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Вес товара
    /// </summary>
    public double Weight { get; set; }
    
    /// <summary>
    /// Объем товара. Высчитывается на сервере
    /// </summary>
    public double Volume { get; set; }
}