using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.ModelsDb;
using WheelDeal.Domain.Database.Responses;
using WheelDeal.Domain.Helpers;
using WheelDeal.Domain.Interfaces;
using WheelDeal.Domain.Validation.Validators;
using WheelDeal.Domain.ViewModels.LogAndReg;
using WheelDeal.Service.Interfaces;

namespace WheelDeal.Service.Realizations;

public class AccountService : IAccountService
{
        private readonly IBaseStorage<UserDb> _userStorage;

        private IMapper _mapper { get; set; }

        private LoginValidator _validationRulesLogin { get; set; }
        
        private RegisterValidator _validationRulesRegister { get; set; }
        
        private ConfirmEmailValidator _validationRulesConfirmEmail { get; set; }

        MapperConfiguration mapperConfiguration = new MapperConfiguration(p => { p.AddProfile<AppMappingProfile>(); });

        public AccountService(IBaseStorage<UserDb> userStorage)
        {
                _userStorage = userStorage;
                _mapper = mapperConfiguration.CreateMapper();
                _validationRulesLogin = new();
                _validationRulesRegister = new();
                _validationRulesConfirmEmail = new();
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
                try
                {
                        await _validationRulesLogin.ValidateAndThrowAsync(model);

                        var userdb = await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);

                        if (userdb == null)
                        {
                                return new BaseResponse<ClaimsIdentity>()
                                {
                                        Description = "Пользователь не найден"
                                };
                        }

                        if (userdb.Password != HashPasswordHelper.HashPassword(model.Password))
                        {
                                return new BaseResponse<ClaimsIdentity>()
                                {
                                        Description = "Неверный пароль или почта"
                                };
                        }

                        var user = new User { Login = userdb.Login, PasswordConfirm = userdb.Password, Email = userdb.Email, ImagePath = userdb.ImagePath, Role = userdb.Role };
                        var result = AuthenticateUserHelper.Authenticate(user);

                        return new BaseResponse<ClaimsIdentity>()
                        {
                                Data = result,
                                StatusCode = StatusCode.OK
                        };
                }
                catch (ValidationException e)
                {
                        var errorMessages = string.Join("; ", e.Errors.Select(x => x.ErrorMessage));
                        return new BaseResponse<ClaimsIdentity>()
                        {
                                Description = errorMessages,
                                StatusCode = StatusCode.BadRequest
                        };
                }
                catch (Exception e)
                {
                        return new BaseResponse<ClaimsIdentity>()
                        {
                                Description = e.Message,
                                StatusCode = StatusCode.InternalServerError
                        };
                }
        }

        public async Task<BaseResponse<string>> Register(RegisterViewModel model)
        {
                try
                {
                        // Проверка валидности модели
                        await _validationRulesRegister.ValidateAndThrowAsync(model);

                        // Проверяем, есть ли уже пользователь с таким email
                        if (await _userStorage.GetAll().AnyAsync(x => x.Email == model.Email))
                        {
                                return new BaseResponse<string>
                                {
                                        Description = "Пользователь с такой почтой уже существует",
                                        StatusCode = StatusCode.BadRequest
                                };
                        }

                        // Генерация и отправка кода подтверждения
                        Random random = new Random();
                        var confirmationCode = $"{random.Next(100000, 999999)}";
                        await SendEmail(model.Email, confirmationCode);

                        return new BaseResponse<string>
                        {
                                Data = confirmationCode,
                                Description = "Письмо с кодом подтверждения отправлено",
                                StatusCode = StatusCode.OK
                        };
                }
                catch (ValidationException e)
                {
                        return new BaseResponse<string>
                        {
                                Description = string.Join("; ", e.Errors.Select(x => x.ErrorMessage)),
                                StatusCode = StatusCode.BadRequest
                        };
                }
                catch (Exception e)
                {
                        return new BaseResponse<string>
                        {
                                Description = "Ошибка на сервере: " + e.Message,
                                StatusCode = StatusCode.InternalServerError
                        };
                }
        }


        public async Task SendEmail(string email, string confirmationCode) 
        {
                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                {
                        throw new ArgumentException("Invalid email address");
                }
                
                string path = "G:\\Study\\GitHub\\Practica November-December\\materials\\passwordPractice.txt"; 
                var emailMessage = new MimeMessage();
                
                emailMessage.From.Add(new MailboxAddress("Администрация сайта WheelDeal", "foksakirillwork@gmail.com")); 
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = "Добро пожаловать!"; 
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) 
                {
                        Text = "<html>" + "<head>" +"<style>" +
                               "body { font-family: Arial, sans-serif; background-color: #f2f2f2; }" + 
                               ".container { max-width: 600px; margin: 0 auto; padding: 20px; background-color: #fff; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1); }" + 
                               ".header { text-align: center; margin-bottom: 20px; }" +
                               ".message { font-size: 16px; line-height: 1.6; }" + 
                               ".container-code { background-color: #f0f0f0; padding: 5px; border-radius: 5px; font-weight: bold; }" + 
                               ".code {text-align: center; }" +
                               "</style>" +
                               "</head>" +
                               "<body>" +
                               "<div class='container'>" +
                               "<div class='header'><h1>Добро пожаловать на сайт WheelDeal!</h1></div>" + 
                               "<div class='message'>" + 
                                "<p>Пожалуйста, введите данный код на сайте, чтобы подтвердить ваш email и завершить регистрацию:</p>" + 
                                "<div class='container-code'><p class='code'>" + confirmationCode + "</p></div>" + 
                                "</div>" + "</div>" + "</body>" + "</html>"
                };
                
                using (StreamReader reader = new StreamReader(path))
                {
                        string password = (await reader.ReadToEndAsync()).Trim();

                        try
                        {
                                using (var client = new SmtpClient())
                                {
                                        await client.ConnectAsync("smtp.gmail.com", 465, true);
                                        await client.AuthenticateAsync("foksakirillwork@gmail.com", password);
                                        await client.SendAsync(emailMessage);
                                        await client.DisconnectAsync(true);
                                }
                        }
                        catch (Exception ex)
                        {
                                Console.WriteLine($"Error: {ex.Message}");
                                throw;
                        }
                }
        }

        public async Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(ConfirmEmailViewModel model, string code, string confirmCode)
        {
                try
                {
                        // Проверяем соответствие введенного кода
                        if (code != confirmCode)
                        {
                                throw new Exception("Неверный код подтверждения!");
                        }

                        // Генерируем дату создания и хэшируем пароль
                        model.Password = HashPasswordHelper.HashPassword(model.Password);

                        // Создаем пользователя в базе данных
                        var userdb = _mapper.Map<UserDb>(model);
                        await _userStorage.Add(userdb);

                        var user = new User { Login = userdb.Login, PasswordConfirm = userdb.Password, Email = userdb.Email, ImagePath = userdb.ImagePath, Role = userdb.Role };
                        
                        // Аутентифицируем пользователя
                        var result = AuthenticateUserHelper.Authenticate(user);

                        return new BaseResponse<ClaimsIdentity>
                        {
                                Data = result,
                                Description = "Email успешно подтвержден, регистрация завершена.",
                                StatusCode = StatusCode.OK
                        };
                }
                catch (Exception ex)
                {
                        return new BaseResponse<ClaimsIdentity>
                        {
                                Description = ex.Message,
                                StatusCode = StatusCode.InternalServerError
                        };
                }
        }

        public async Task<BaseResponse<ClaimsIdentity>> IsCreatedAccount(User model)
        {
                try
                {
                        var userDb = new UserDb();
                        if (await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) == null)
                        {
                                model.Password = "google";
                                
                                userDb = _mapper.Map<UserDb>(model);
                                
                                await _userStorage.Add(userDb);
                                
                                var resultRegister = AuthenticateUserHelper.Authenticate(model);
                                return new BaseResponse<ClaimsIdentity>()
                                {
                                        Data = resultRegister,
                                        Description = "Объект добавился",
                                        StatusCode = StatusCode.OK
                                };
                        }
                        
                        var resultLogin = AuthenticateUserHelper.Authenticate(model);
                        return new BaseResponse<ClaimsIdentity>()
                        {
                                Data = resultLogin,
                                Description = "Объект уже был создан",
                                StatusCode = StatusCode.OK
                        };
                }
                catch (Exception ex)
                {
                        return new BaseResponse<ClaimsIdentity>()
                        {
                                Description = ex.Message,
                                StatusCode = StatusCode.InternalServerError
                        };
                }
        }
}
 