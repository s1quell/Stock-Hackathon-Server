using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    #region Private Members

    private readonly ILogger<HomeController> _logger;
    
    #endregion

    #region Constructor

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    #endregion

    #region HTTP Chanels

    [HttpGet(Name = "GetTest")]
    [Authorize(Roles = "Admin")]
    public string? Get()
    {
        return "Test";
    }

    #endregion
}