using FluentValidation;
using WheelDeal.Domain.Database.Entities;

namespace WheelDeal.Domain.Validation.Validators;

public class RateValidator : AbstractValidator<Rate>
{
    public RateValidator()
    {
        RuleFor(rate => rate.UserId)
            .NotEmpty().WithMessage(ValidationMessages.RateUserIdRequired);

        RuleFor(rate => rate.Comment)
            .Length(0, 500).WithMessage(ValidationMessages.RateCommentLength);

        RuleFor(rate => rate.Points)
            .InclusiveBetween(1, 5).WithMessage(ValidationMessages.RatePointsRange);

        RuleFor(rate => rate.Date)
            .NotEmpty().WithMessage(ValidationMessages.RateDateRequired);
    }
}
