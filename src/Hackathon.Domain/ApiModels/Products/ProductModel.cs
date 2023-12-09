using System.ComponentModel.DataAnnotations;

namespace Hackathon.Domain.ApiModels.Products;

/// <summary>
/// Модель добавления товара в систему
/// </summary>
public class ProductModel
{
    /// <summary>
    /// Уникальный идентификатор пользователя в системе
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательно для определения пользователя в системе")]
    public int ClientId { get; set; }
    
    /// <summary>
    /// Наименование товара
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Наименование товара в системе")]
    public string? Name { get; set; }
    
    /// <summary>
    /// Количество товара
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Количество товара")]
    public int Amount { get; set; }
    
    /// <summary>
    /// Артикль товара
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Артикль товара")]
    public string? Article { get; set; }
    
    /// <summary>
    /// Стоимость товара за единицу
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Цена за единицу товара")]
    public double Price { get; set; }
    
    /// <summary>
    /// Длина товара
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Длина товара")]
    public double Width { get; set; }
    
    /// <summary>
    /// Высота товара
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Высота товара")]
    public double Height { get; set; }
    
    /// <summary>
    /// Ширина товара
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Ширина товара")]
    public double Depth { get; set; }
    
    /// <summary>
    /// Баркод товара
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Баркод товара")]
    public int Code { get; set; }
    
    /// <summary>
    /// Описание к товару
    /// </summary>
    [Required(AllowEmptyStrings = true, ErrorMessage = "Описание к товару")]
    public string? Description { get; set; }
    
    /// <summary>
    /// Масса товара
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Масса товара")]
    public double Weight { get; set; }
    
}