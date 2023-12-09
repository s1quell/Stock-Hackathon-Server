namespace Hackathon.Domain.ApiModelsEntity.Orders;

/// <summary>
/// Класс реализует сущность товара в заявке
/// </summary>
public class OrderProduct
{
    /// <summary>
    /// Уникальный идентификатор продукта
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Количество товара в заявке
    /// </summary>
    public int Amount { get; set; }
}