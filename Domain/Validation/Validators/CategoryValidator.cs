using FluentValidation;
using WheelDeal.Domain.Database.Entities;

namespace WheelDeal.Domain.Validation.Validators;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(category => category.Name)
            .NotEmpty().WithMessage(ValidationMessages.CategoryNameRequired)
            .Length(1, 100).WithMessage(ValidationMessages.CategoryNameLength);

        RuleFor(category => category.ImagePath)
            .Length(0, 255).WithMessage(ValidationMessages.CategoryImagePathLength);

        RuleFor(category => category.CountPosts)
            .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.CategoryCountPostsNonNegative);
    }
}
