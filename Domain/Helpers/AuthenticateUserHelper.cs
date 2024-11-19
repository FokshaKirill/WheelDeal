using System.Security.Claims;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.ModelsDb;

namespace WheelDeal.Domain.Helpers;

public static class AuthenticateUserHelper
{
    public static ClaimsIdentity Authenticate(UserDb user)
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