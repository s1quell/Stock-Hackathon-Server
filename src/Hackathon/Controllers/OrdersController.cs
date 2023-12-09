using Hackathon.Core.Database;
using Hackathon.Domain.ApiModels.Orders;
using Hackathon.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Hackathon.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OrdersController : ControllerBase
{
    #region Private Members

    private readonly ILogger<ProductsController> _logger;
    
    #endregion

    #region Constructor

    public OrdersController(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }

    #endregion

    #region HTTP Chanels

    [HttpGet(Name = "GetAllOrders")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> All()
    {
        try
        {
            var orders = DatabaseController
                .GetInstance()
                .Orders!
                .AsNoTracking();
            
            return Ok(orders);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    
    [HttpGet(Name = "GetOrdersById")]
    [Authorize]
    public async Task<IActionResult> GetByClientId(int clientId)
    {
        try
        {
            if (clientId <= 0)
                return BadRequest("Ошибка данных");

            var products = 
                DatabaseController.GetInstance()
                    .Orders!
                    .Where(x => x.UserId == clientId)
                    .AsNoTracking();
            
            return Ok(products);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet(Name = "GetOrderById")]
    [Authorize]
    public async Task<IActionResult> GetOrder(int orderId, int clientId)
    {
        try
        {
            if (clientId <= 0 || orderId <= 0)
                return BadRequest("Неверные данные");

            var order = await DatabaseController.GetInstance()
                    .Orders!
                    .FirstOrDefaultAsync(x => x.UserId == clientId && x.Id == orderId);

            // Получаем список 
            var orderProduct = JsonConvert.DeserializeObject(order.Products);
            
            // Получаем продукты в заявке
            
            
            return Ok(order);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost(Name = "AddOrder")]
    [Authorize]
    public async Task<IActionResult> Add(OrderModel model)
    {
        try{
            
            if (model == null)
                return BadRequest("Тело запроса не может быть не включать значения");

            var user = await DatabaseController.GetInstance()
                .Users!
                .FirstOrDefaultAsync(x => x.Id == model.ClientId);
            
            // Добавление заявки в базу
            await DatabaseController.GetInstance()
                .Orders!
                .AddAsync(new Order()
                {
                    UniqueId = Guid.NewGuid().ToString(),
                    Status = 0,
                    Company = user.Company,
                    Date = DateTime.Now.ToString(),
                    Products = JsonConvert.SerializeObject(model.OrderProducts)
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
    
    [HttpDelete(Name = "DeleteOrder")]
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
            
            return Ok("Авторизация была успешно пройдена");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }

    #endregion
}