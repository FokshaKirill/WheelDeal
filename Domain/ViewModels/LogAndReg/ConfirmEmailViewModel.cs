using System.ComponentModel.DataAnnotations;

namespace WheelDeal.Domain.ViewModels.LogAndReg;

public class ConfirmEmailViewModel {
    [Required(ErrorMessage = "Введите код")]
    public string CodeConfirm { get; set; }
    public string GeneratedCode { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
}