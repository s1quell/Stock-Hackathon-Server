using Hackathon.Core.Database;
using Hackathon.Domain.ApiModels.Products;
using Hackathon.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductsController : ControllerBase
{
    #region Private Members

    private readonly ILogger<ProductsController> _logger;
    
    #endregion

    #region Constructor

    public ProductsController(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }

    #endregion

    #region HTTP Chanels
    
    [HttpGet(Name = "GetAllProducts")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = 
                DatabaseController.GetInstance()
                    .Products!
                    .AsNoTracking();
            
            return Ok(products);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet(Name = "GetProducts")]
    [Authorize]
    public async Task<IActionResult> GetByClientId(int clientId)
    {
        try
        {
            if (clientId <= 0)
                return BadRequest("Ошибка данных");

            var products = 
                DatabaseController.GetInstance()
                    .Products!
                    .Where(x => x.ClientId == clientId)
                    .AsNoTracking();
            
            return Ok(products);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost(Name = "AddProduct")]
    [Authorize]
    public async Task<IActionResult> Add(ProductModel model)
    {
        try{
            
            if (model == null)
                return BadRequest("Я не знаю как ты это сделал...");

            await DatabaseController.GetInstance().Products!.AddAsync(new Product()
            {
                ClientId = model.ClientId,
                Name = model.Name,
                Amount = model.Amount,
                Article = model.Article,
                Price = model.Price,
                Width = model.Width,
                Height = model.Height,
                Depth = model.Depth,
                Code = model.Code,
                Description = model.Description,
                Weight = model.Weight,
                Volume = model.Width * model.Height * model.Depth
                
            });

            await DatabaseController.GetInstance().SaveChangesAsync();
            
            return Ok("Товар был успешно добавлен");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete(Name = "DeleteProduct")]
    [Authorize]
    public async Task<IActionResult> Delete(int productId)
    {
        try
        {
            if (productId <= 0)
                return BadRequest("Неправильный код продукта");

            DatabaseController.GetInstance().Products!.Attach(new Product() { Id = productId });
            DatabaseController.GetInstance().Products!.Remove(new Product() { Id = productId });

            await DatabaseController.GetInstance().SaveChangesAsync();
            
            return Ok($"Продукт с кодом {productId}");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }

    #endregion
}