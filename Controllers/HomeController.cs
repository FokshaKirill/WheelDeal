using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WheelDeal.Database;
using WheelDeal.Domain.ViewModels.LogAndReg;
using WheelDeal.Entities;
using WheelDeal.Models;

namespace WheelDeal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {            
        return View();
    }

    public IActionResult AutoPark()
    {
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Countries()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginViewModel model)
    {
        if (ModelState.IsValid)
            return Ok(model);
        var errors = ModelState.Values.SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        return BadRequest(errors); // Возвращаем ошибки 400 Bad Request с сообщениями об ошибках
    }
    
    [HttpPost]
    public IActionResult Register([FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)

            .Select(e => e.ErrorMessage)
            .ToList();
            
            return BadRequest(errors); // Возвращаем ошибки 400 Bad Request с сообщениями об ошибках
        }
        
        return Ok(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
