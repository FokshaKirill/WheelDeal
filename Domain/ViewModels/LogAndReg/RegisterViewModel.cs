using System.ComponentModel.DataAnnotations;

namespace WheelDeal.Domain.ViewModels.LogAndReg;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Логин должен содержать 3-20 символов (лат. буквы, цифры, точки и дефисы)")]
    [RegularExpression(@"^[a-zA-Z0-9\-\.]{6,20}$")]
    [Display(Name = "Логин")]
    public string Login { get; set; }
    
    [Required(ErrorMessage ="Укажите почту")]
    [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")] 
    [RegularExpression(@"^([\w\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
    [Display(Name = "Почта")]
    public string Email { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage ="Пароль должен иметь длину больше 6 символов")]
    [MinLength(6)] 
    [RegularExpression(@"^[a-zA-Z0-9\-\.]{6,120}$")]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
    
    [DataType(DataType. Password)]
    [Required(ErrorMessage ="Подтвердите пароль")]
    [Compare("Password", ErrorMessage ="Пароли должны совпадать")] 
    [Display(Name = "Подтверждение пароля")]
    public string PasswordConfirm { get; set; }
}