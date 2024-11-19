using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.ViewModels.LogAndReg;
using WheelDeal.Models;
using WheelDeal.Service;
using WheelDeal.Service.Interfaces;

namespace WheelDeal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAccountService _accountService;
    
    private IMapper _mapper { get; set; }

    MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });
    
    public HomeController(ILogger<HomeController> logger, IAccountService accountService)
    {
        _accountService = accountService;
        _logger = logger;
        _mapper = mapperConfiguration.CreateMapper();
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

    #region HttpPosts
            [HttpPost]
            public async Task<IActionResult> Login([FromBody] LoginViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var user = _mapper.Map<User>(model);

                    var response = await _accountService.Login(user);
                    if (response.StatusCode == Domain.Database.Responses.StatusCode.OK)
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(response.Data));
                        
                        return Ok(model);  
                    }
                    
                    ModelState.AddModelError("", response.Description);
                }
                
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(errors); // Возвращаем ошибки 400 Bad Request с сообщениями об ошибках
            }
            
            [HttpPost]
            public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var user = _mapper.Map<User>(model);

                    var response = await _accountService.Register(user);
                    if (response.StatusCode == Domain.Database.Responses.StatusCode.OK)
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(response.Data));
                        
                        return Ok(model);  
                    }
                    
                    ModelState.AddModelError("", response.Description);
                }
                
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(errors); // Возвращаем ошибки 400 Bad Request с сообщениями об ошибках
            }

            [AutoValidateAntiforgeryToken]
            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return RedirectToAction("Index", "Home");
            }
    #endregion 
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
