using System.Security.Claims;
using System.Threading.Tasks;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.Responses;

namespace WheelDeal.Service.Interfaces;

public interface IAccountService
{
    Task<BaseResponse<ClaimsIdentity>> Register(User model);
    
    Task<BaseResponse<ClaimsIdentity>> Login(User model);
}