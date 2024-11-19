using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.ModelsDb;
using WheelDeal.Domain.Database.Responses;
using WheelDeal.Domain.Helpers;
using WheelDeal.Domain.Interfaces;
using WheelDeal.Service.Interfaces;

namespace WheelDeal.Service;

public class AccountService : IAccountService
{
        private readonly IBaseStorage<UserDb> _userStorage;

        private IMapper _mapper { get; set; }

        MapperConfiguration mapperConfiguration = new MapperConfiguration(p => { p.AddProfile<AppMappingProfile>(); });

        public AccountService(IBaseStorage<UserDb> userStorage)
        {
                _userStorage = userStorage;
                _mapper = mapperConfiguration.CreateMapper();
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(User model)
        {
                try
                {
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

                        var result = AuthenticateUserHelper.Authenticate(userdb);
                        
                        return new BaseResponse<ClaimsIdentity>()
                        {
                                Data = result,
                                StatusCode = StatusCode.OK
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
        
        public async Task<BaseResponse<ClaimsIdentity>> Register(User model)
        {
                try
                {
                        model.ImagePath = "../images/user.png";
                        model.CreatedAt = DateTime.Now.ToUniversalTime();
                        model.Password = HashPasswordHelper.HashPassword(model.Password);
                        
                        var userdb = _mapper.Map<UserDb>(model);

                        if (await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) != null)
                        {
                                return new BaseResponse<ClaimsIdentity>()
                                {
                                        Description = "Пользователь с такой почтой уже зарегистрирован"
                                };
                        }

                        await _userStorage.Add(userdb);

                        var result = AuthenticateUserHelper.Authenticate(userdb);
                        
                        return new BaseResponse<ClaimsIdentity>()
                        {
                                Data = result,
                                Description = "Пользователь успешно зарегистрирован",
                                StatusCode = StatusCode.OK
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
}