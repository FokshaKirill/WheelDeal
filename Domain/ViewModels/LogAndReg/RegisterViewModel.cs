using System.ComponentModel.DataAnnotations;

namespace WheelDeal.Domain.ViewModels.LogAndReg;

public class RegisterViewModel
{
    public string Login { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public string PasswordConfirm { get; set; }
}