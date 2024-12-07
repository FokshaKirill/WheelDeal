using System.Diagnostics;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.ViewModels.LogAndReg;
using WheelDeal.Service.Interfaces;
using WheelDeal.Service.Realizations;

namespace WheelDeal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAccountService _accountService;
    private readonly IWebHostEnvironment _appEnvironment;
    
    public HomeController(ILogger<HomeController> logger, IAccountService accountService, IWebHostEnvironment appEnvironment)
    {
        _accountService = accountService;
        _logger = logger;
        _appEnvironment = appEnvironment;
    }

    #region Pages
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

        public IActionResult Contacts()
        {
            return View();
        }
    #endregion

    #region HttpPosts
            [HttpPost]
            public async Task<IActionResult> Login([FromBody] LoginViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var response = await _accountService.Login(model);
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
                    // Вызываем сервис для регистрации
                    var response = await _accountService.Register(model);

                    // Проверяем, успешна ли регистрация
                    if (response.StatusCode == Domain.Database.Responses.StatusCode.OK)
                    {
                        // Возвращаем форму подтверждения кода только при успешной валидации
                        var confirm = new ConfirmEmailViewModel
                        {
                            Login = model.Login,                            
                            Email = model.Email,                            
                            Password = model.Password,
                            PasswordConfirm = model.PasswordConfirm,
                            GeneratedCode = response.Data
                        };
                        return Ok(confirm);
                    }

                    // Если есть ошибки регистрации, добавляем их в ModelState
                    ModelState.AddModelError("", response.Description);
                }
                
                // Возвращаем ошибки валидации
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                
                return BadRequest(errors); // Возвращаем ошибки 400 Bad Request
            }

            [HttpPost]
            public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailViewModel confirmEmailModel)
            {
                var response = await _accountService.ConfirmEmail(confirmEmailModel, confirmEmailModel.GeneratedCode, confirmEmailModel.CodeConfirm);

                if (response.StatusCode == Domain.Database.Responses.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));
                    
                    return Ok(confirmEmailModel);
                }
                
                ModelState.AddModelError("", response.Description);

                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                
                return BadRequest(errors);
            }
            
            [AutoValidateAntiforgeryToken]
            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return RedirectToAction("Index", "Home");
            }

            public async Task AuthenticationGoogle(string returnUrl = "/")
            {
                await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                    new AuthenticationProperties
                    {
                        RedirectUri = Url.Action("GoogleResponse", new { returnUrl }),
                        Parameters = { {"prompt", "select_account"} }
                    });
            }

            public async Task<ActionResult> GoogleResponse(string returnUrl = "/")
            {
                try
                {
                    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    if (result?.Succeeded == true)
                    {
                        User model = new User
                        {
                            Login = result.Principal.FindFirst(ClaimTypes.Name)?.Value,
                            Email = result.Principal.FindFirst(ClaimTypes.Email)?.Value,
                            ImagePath = @"\" + SaveImageInImageUser(result.Principal.FindFirst("picture")?.Value, result).Result ?? @"\images\user.png",
                        };

                        var response = await _accountService.IsCreatedAccount(model);

                        if (response.StatusCode == Domain.Database.Responses.StatusCode.OK)
                        {
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(response.Data));
                            return Redirect(returnUrl);
                        }
                    }
                    
                    return BadRequest("Аутентификация не удалась.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            private async Task<string> SaveImageInImageUser(string imageUrl, AuthenticateResult result)
            {
                try
                {
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        using (var httpClient = new HttpClient())
                        {
                            string fileName = $"{result.Principal.FindFirst(ClaimTypes.Email)?.Value}-avatar.jpg";
                            string filePath = Path.Combine(_appEnvironment.WebRootPath, @"images\ImageUser", fileName);

                            var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                            return Path.Combine(@"images\ImageUser", fileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка сохранения изображения.");
                }

                return @"\images\user.png";
            }

    #endregion 
    
    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
