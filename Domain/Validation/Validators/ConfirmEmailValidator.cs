using FluentValidation;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.ModelsDb;
using WheelDeal.Domain.ViewModels.LogAndReg;

namespace WheelDeal.Domain.Validation.Validators;

using FluentValidation;

public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailViewModel>
{
    public ConfirmEmailValidator()
    {
        RuleFor(model => model.CodeConfirm)
            .NotEmpty().WithMessage(ValidationMessages.RegCodeConfirmRequired)
            .Length(6).WithMessage(ValidationMessages.RegCodeConfirmLength)
            .Matches(RegexPatterns.OnlyNumsRegex).WithMessage(ValidationMessages.RegCodeConfirmInvalid);

        RuleFor(user => user.Login)
            .NotEmpty().WithMessage(ValidationMessages.UserLoginRequired)
            .MaximumLength(50).WithMessage(ValidationMessages.UserLoginLength)
            .Matches(RegexPatterns.LoginRegex).WithMessage(ValidationMessages.LoginInvalid);

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage(ValidationMessages.UserPasswordRequired)
            .MinimumLength(6).WithMessage(ValidationMessages.UserPasswordLength)
            .Matches(RegexPatterns.PasswordRegex).WithMessage(ValidationMessages.PasswordInvalid);

        RuleFor(user => user.PasswordConfirm)
            .NotEmpty().WithMessage(ValidationMessages.UserPasswordRequired)
            .Equal(user => user.Password).WithMessage(ValidationMessages.PasswordMismatch);

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage(ValidationMessages.UserEmailRequired)
            .Matches(RegexPatterns.EmailRegex).WithMessage(ValidationMessages.UserEmailInvalid);
    }
}