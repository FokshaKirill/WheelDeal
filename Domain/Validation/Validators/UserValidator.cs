using FluentValidation;
using WheelDeal.Domain.Database.Entities;

namespace WheelDeal.Domain.Validation.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Login)
            .NotEmpty().WithMessage(ValidationMessages.LoginRequired)
            .MaximumLength(50).WithMessage(ValidationMessages.LoginMaxLength)
            .Matches(RegexPatterns.LoginRegex).WithMessage(ValidationMessages.LoginInvalid);

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage(ValidationMessages.PasswordRequired)
            .MinimumLength(6).WithMessage(ValidationMessages.PasswordMinLength)
            .Matches(RegexPatterns.PasswordRegex).WithMessage(ValidationMessages.PasswordInvalid);

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage(ValidationMessages.EmailRequired)
            .Matches(RegexPatterns.EmailRegex).WithMessage(ValidationMessages.EmailInvalid);

        RuleFor(user => user.Role)
            .InclusiveBetween(1, 3).WithMessage(ValidationMessages.RoleRange); // Предположим, что роли от 1 до 3.

        RuleFor(user => user.ImagePath)
            .MaximumLength(200).WithMessage(ValidationMessages.ImagePathMaxLength);

        RuleFor(user => user.CreatedAt)
            .LessThanOrEqualTo(DateTime.Now).WithMessage(ValidationMessages.CreatedAtValid);
    }
}