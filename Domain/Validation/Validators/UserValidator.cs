using FluentValidation;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.ModelsDb;

namespace WheelDeal.Domain.Validation.Validators;

using FluentValidation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Login)
            .NotEmpty().WithMessage(ValidationMessages.UserLoginRequired)
            .MaximumLength(50).WithMessage(ValidationMessages.UserLoginLength)
            .Matches(RegexPatterns.LoginRegex).WithMessage(ValidationMessages.LoginInvalid);

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage(ValidationMessages.UserPasswordRequired)
            .MinimumLength(6).WithMessage(ValidationMessages.UserPasswordLength)
            .Matches(RegexPatterns.PasswordRegex).WithMessage(ValidationMessages.PasswordInvalid);

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage(ValidationMessages.UserEmailRequired)
            .Matches(RegexPatterns.EmailRegex).WithMessage(ValidationMessages.UserEmailInvalid);

        RuleFor(user => user.Role)
            .InclusiveBetween(1, 3).WithMessage(ValidationMessages.UserRoleRange);

        RuleFor(user => user.ImagePath)
            .MaximumLength(200).WithMessage(ValidationMessages.UserImagePathMaxLength);

        RuleFor(user => user.CreatedAt)
            .LessThanOrEqualTo(DateTime.Now).WithMessage(ValidationMessages.UserCreatedAtValid);
    }
}