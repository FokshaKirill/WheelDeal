using WheelDeal.Domain.Database.Entities;

namespace WheelDeal.Domain.Validation.Validators;

using FluentValidation;

public class PostValidator : AbstractValidator<Post>
{
    public PostValidator()
    {
        RuleFor(post => post.CarId)
            .NotEmpty().WithMessage(ValidationMessages.PostCarIdRequired);

        RuleFor(post => post.CategoryId)
            .NotEmpty().WithMessage(ValidationMessages.PostCategoryIdRequired);

        RuleFor(post => post.Description)
            .NotEmpty().WithMessage(ValidationMessages.PostDescriptionRequired)
            .Length(10, 1000).WithMessage(ValidationMessages.PostDescriptionLength);

        RuleFor(post => post.Price)
            .GreaterThan(0).WithMessage(ValidationMessages.PostPricePositive);

        RuleFor(post => post.AvailabilityStatus)
            .NotNull().WithMessage(ValidationMessages.PostAvailabilityStatusRequired);

        RuleFor(post => post.CreatedAt)
            .NotEmpty().WithMessage(ValidationMessages.PostCreatedAtRequired);
    }
}
