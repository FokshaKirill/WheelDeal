using System.Security.Claims;
using FluentValidation;
using MailKit.Net.Smtp;
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

    private readonly LoginValidator _validationRulesLogin;
    private readonly RegisterValidator _validationRulesRegister;

    public AccountService(IBaseStorage<UserDb> userStorage)
    {
        _userStorage = userStorage;
        _validationRulesLogin = new LoginValidator();
        _validationRulesRegister = new RegisterValidator();
    }

    public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
    {
        try
        {
            await _validationRulesLogin.ValidateAndThrowAsync(model);

            var userDb = await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);

            if (userDb == null || userDb.Password != HashPasswordHelper.HashPassword(model.Password))
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Неверный email или пароль",
                    StatusCode = StatusCode.BadRequest
                };
            }

            var claimsIdentity = AuthenticateUserHelper.Authenticate(new User
            {
                Login = userDb.Login,
                Email = userDb.Email,
                ImagePath = userDb.ImagePath,
                Role = userDb.Role
            });

            return new BaseResponse<ClaimsIdentity>
            {
                Data = claimsIdentity,
                StatusCode = StatusCode.OK
            };
        }
        catch (ValidationException e)
        {
            return new BaseResponse<ClaimsIdentity>
            {
                Description = string.Join("; ", e.Errors.Select(x => x.ErrorMessage)),
                StatusCode = StatusCode.BadRequest
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<ClaimsIdentity>
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
            await _validationRulesRegister.ValidateAndThrowAsync(model);

            if (await _userStorage.GetAll().AnyAsync(x => x.Email == model.Email))
            {
                return new BaseResponse<string>
                {
                    Description = "Пользователь с такой почтой уже существует",
                    StatusCode = StatusCode.BadRequest
                };
            }

            string confirmationCode = new Random().Next(100000, 999999).ToString();
            await SendEmail(model.Email, confirmationCode);

            return new BaseResponse<string>
            {
                Data = confirmationCode,
                Description = "Код подтверждения отправлен",
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
                Description = e.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(ConfirmEmailViewModel model, string code, string confirmCode)
    {
        try
        {
            if (code != confirmCode)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Неверный код подтверждения",
                    StatusCode = StatusCode.BadRequest
                };
            }

            var userDb = new UserDb
            {
                Login = model.Login,
                Email = model.Email,
                Password = HashPasswordHelper.HashPassword(model.Password),
                ImagePath = @"G:\Study\GitHub\Practica November-December\WheelDeal\wwwroot\images\avatars\default.png",
                Role = 1
            };

            await _userStorage.Add(userDb);

            var claimsIdentity = AuthenticateUserHelper.Authenticate(new User
            {
                Login = userDb.Login,
                Email = userDb.Email,
                ImagePath = userDb.ImagePath,
                Role = userDb.Role
            });

            return new BaseResponse<ClaimsIdentity>
            {
                Data = claimsIdentity,
                Description = "Регистрация завершена",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<ClaimsIdentity>
            {
                Description = e.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    private async Task SendEmail(string email, string confirmationCode)
    {
        var emailMessage = new MimeMessage
        {
            From = { new MailboxAddress("WheelDeal", "foksakirillwork@gmail.com") },
            To = { new MailboxAddress("", email) },
            Subject = "Код подтверждения",
            Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<html>" + "<head>" + "<style>" +
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
            }
        };

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 465, true);

        string password =
            await File.ReadAllTextAsync(
                "G:\\Study\\GitHub\\Practica November-December\\materials\\passwordPractice.txt");
        await client.AuthenticateAsync("foksakirillwork@gmail.com", password);
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }

    public async Task<BaseResponse<ClaimsIdentity>> IsCreatedAccount(User model)
    {
        try
        {
            var userDb = new UserDb();
            if (await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) == null)
            {
                model.Password = "google";

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