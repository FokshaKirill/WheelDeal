using WheelDeal.Database.Entities;
using WheelDeal.Database.Responses;

namespace Service.Interfaces;

public interface IAccountService
{
    Task<BaseResponse<User>> Register(User model);
    
    Task<BaseResponse<User>> Login(User model);
}