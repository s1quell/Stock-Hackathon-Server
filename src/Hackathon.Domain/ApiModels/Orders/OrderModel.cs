using System.Diagnostics.CodeAnalysis;
using Hackathon.Domain.ApiModelsEntity.Orders;

namespace Hackathon.Domain.ApiModels.Orders;

public class OrderModel
{
    /// <summary>
    /// Уникальный идентификатор клиента
    /// </summary>
    public int? ClientId { get; set; }
    
    /// <summary>
    /// Продукты отдельной компании
    /// </summary>
    public List<OrderProduct> OrderProducts { get; set; }
}