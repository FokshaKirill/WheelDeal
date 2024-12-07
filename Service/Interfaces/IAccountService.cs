using System.Security.Claims;
using System.Threading.Tasks;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.Responses;
using WheelDeal.Domain.ViewModels.LogAndReg;

namespace WheelDeal.Service.Interfaces;

public interface IAccountService
{
    Task<BaseResponse<string>> Register(RegisterViewModel model);
    
    Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    
    Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(ConfirmEmailViewModel model, string code, string confirmCode);
    
    Task<BaseResponse<ClaimsIdentity>> IsCreatedAccount(User model);
}