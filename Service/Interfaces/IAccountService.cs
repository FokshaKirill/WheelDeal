using System.Security.Claims;
using System.Threading.Tasks;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.Responses;

namespace WheelDeal.Service.Interfaces;

public interface IAccountService
{
    Task<BaseResponse<string>> Register(User model);
    
    Task<BaseResponse<ClaimsIdentity>> Login(User model);
    
    Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(User model, string code, string confirmCode);
    
    Task<BaseResponse<ClaimsIdentity>> IsCreatedAccount(User model);
}