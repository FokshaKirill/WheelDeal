using System.Text.RegularExpressions;

namespace WheelDeal.Domain.Validation;

public static class RegexPatterns
{
    public const string EmailRegex = @"^([\w\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
    public const string PasswordRegex = @"^[a-zA-Z0-9\-\.]$";
    public const string LoginRegex = @"^[a-zA-Z0-9]{3,50}$";
    
}