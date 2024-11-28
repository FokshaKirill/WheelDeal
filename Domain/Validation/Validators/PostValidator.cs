using FluentValidation;
using WheelDeal.Domain.Database.Entities;

namespace WheelDeal.Domain.Validation.Validators;

public class PostValidator : AbstractValidator<Post>
{
    public PostValidator()
    {
        RuleFor(post => post.Description)
            .MaximumLength(500).WithMessage(ValidationMessages.DescriptionMaxLength);

        RuleFor(post => post.Price)
            .GreaterThan(0).WithMessage(ValidationMessages.PriceGreaterThanZero);

        RuleFor(post => post.RatesID)
            .NotNull().WithMessage(ValidationMessages.RatesIDNotEmpty)
            .Must(rates => rates.Count > 0).WithMessage(ValidationMessages.RatesIDNotEmpty);

        RuleFor(post => post.CarID)
            .GreaterThan(0).WithMessage(ValidationMessages.CarIDGreaterThanZero);
    }
}