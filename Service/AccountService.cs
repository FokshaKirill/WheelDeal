using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.ModelsDb;
using WheelDeal.Domain.Database.Responses;
using WheelDeal.Domain.Helpers;
using WheelDeal.Domain.Interfaces;
using WheelDeal.Domain.Validation.Validators;
using WheelDeal.Service.Interfaces;

namespace WheelDeal.Service;

public class AccountService : IAccountService
{
        private readonly IBaseStorage<UserDb> _userStorage;

        private IMapper _mapper { get; set; }

        private UserValidator _validationRules { get; set; }

        MapperConfiguration mapperConfiguration = new MapperConfiguration(p => { p.AddProfile<AppMappingProfile>(); });

        public AccountService(IBaseStorage<UserDb> userStorage)
        {
                _userStorage = userStorage;
                _mapper = mapperConfiguration.CreateMapper();
                _validationRules = new UserValidator();
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(User model)
        {
                try
                {
                        await _validationRules.ValidateAndThrowAsync(model);

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

                        var result = AuthenticateUserHelper.Authenticate(model);

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

        public async Task<BaseResponse<string>> Register(User model)
        {
                try
                {
                        Random random = new Random();
                        string confirmationCode =
                                $"{random.Next(10)}{random.Next(10)}{random.Next(10)}{random.Next(10)}{random.Next(10)}{random.Next(10)}{random.Next(10)}{random.Next(10)}";

                        if (await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) != null)
                        {
                                return new BaseResponse<string>()
                                {
                                        Description = "Пользователь с такой почтой уже есть",
                                };
                        }
                        
                        await SendEmail(model.Email, confirmationCode);

                        return new BaseResponse<string>()
                        {
                                Data = confirmationCode,
                                Description = "Письмо отправлено",
                                StatusCode = StatusCode.OK
                        };
                }
                catch (ValidationException e)
                {
                        var errorMessages = string.Join("; ", e.Errors.Select(x => x.ErrorMessage));
                        return new BaseResponse<string>()
                        {
                                Description = errorMessages,
                                StatusCode = StatusCode.BadRequest
                        };
                }
                catch (Exception e)
                {
                        return new BaseResponse<string>()
                        {
                                Description = e.Message,
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
                                throw; // Перебросьте исключение для дальнейшей обработки.
                        }
                }
        }

        public async Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(User model, string code, string confirmCode)
        {
                try
                {
                        if (code != confirmCode)
                                throw new Exception("Неверный код! Регистрация не выполнена.");

                        model.ImagePath = "/images/user.png";
                        model.CreatedAt = DateTime.Now;
                        model.Password = HashPasswordHelper.HashPassword(model.Password);

                        await _validationRules.ValidateAndThrowAsync(model);

                        var userdb = _mapper.Map<UserDb>(model);

                        await _userStorage.Add(userdb);

                        var result = AuthenticateUserHelper.Authenticate(model);

                        return new BaseResponse<ClaimsIdentity>()
                        {
                                Data = result,
                                Description = "Объект добавился",
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
 