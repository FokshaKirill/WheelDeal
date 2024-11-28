using FluentValidation;
using WheelDeal.Domain.Database.Entities;

namespace WheelDeal.Domain.Validation.Validators;

public class RateValidator : AbstractValidator<Rate>
{
    public RateValidator()
    {
        RuleFor(rate => rate.UserId)
            .GreaterThan(0).WithMessage(ValidationMessages.UserIdGreaterThanZero);

        RuleFor(rate => rate.Comment)
            .MaximumLength(1000).WithMessage(ValidationMessages.CommentMaxLength);

        RuleFor(rate => rate.Date)
            .LessThanOrEqualTo(DateTime.Now).WithMessage(ValidationMessages.DateNotInFuture);

        RuleFor(rate => rate.Points)
            .InclusiveBetween(1, 5).WithMessage(ValidationMessages.PointsRange)
            .When(rate => rate.Points.HasValue);
    }

}