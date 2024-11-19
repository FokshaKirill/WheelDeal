using System.ComponentModel.DataAnnotations;

namespace WheelDeal.Domain.ViewModels.LogAndReg;

public class LoginViewModel
{
    [Required(ErrorMessage = "Введите почту")]
    [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
    [RegularExpression(@"^([\w\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
    [Display(Name = "Почта")]
    public string Email { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Введите пароль")]
    [MinLength(6)] 
    [RegularExpression(@"^[a-zA-Z0-9\-\.]{6,120}$")]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
}