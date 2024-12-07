using System.Security.Claims;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.ModelsDb;
using WheelDeal.Domain.ViewModels.LogAndReg;

namespace WheelDeal.Domain.Helpers;

public static class AuthenticateUserHelper
{
    public static ClaimsIdentity Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
            new Claim("AvatarPath", user.ImagePath),
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimTypes.Email, ClaimsIdentity.DefaultRoleClaimType);
    }
}