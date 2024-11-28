using FluentValidation;
using WheelDeal.Domain.Database.Entities;

namespace WheelDeal.Domain.Validation.Validators;

public class CarValidator : AbstractValidator<Car>
{
    public CarValidator()
        {
            RuleFor(car => car.Brand)
                .NotEmpty().WithMessage(ValidationMessages.BrandRequired)
                .MaximumLength(50).WithMessage(ValidationMessages.BrandMaxLength);

            RuleFor(car => car.Model)
                .NotEmpty().WithMessage(ValidationMessages.ModelRequired)
                .MaximumLength(50).WithMessage(ValidationMessages.ModelMaxLength);

            RuleFor(car => car.Year)
                .InclusiveBetween(1886, DateTime.Now.Year).WithMessage(string.Format(ValidationMessages.YearRange, DateTime.Now.Year));

            RuleFor(car => car.CarItemId)
                .GreaterThan(0).WithMessage(ValidationMessages.CarItemIdGreaterThanZero);

            RuleFor(car => car.PlacesCount)
                .InclusiveBetween(1, 9).WithMessage(ValidationMessages.PlacesCountRange);

            RuleFor(car => car.EngineValue)
                .GreaterThan(0).WithMessage(ValidationMessages.EngineValueGreaterThanZero);

            RuleFor(car => car.Mileage)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MileageNonNegative);

            RuleFor(car => car.Body)
                .NotEmpty().WithMessage(ValidationMessages.BodyRequired)
                .MaximumLength(30).WithMessage(ValidationMessages.BodyMaxLength);

            RuleFor(car => car.Fuel)
                .NotEmpty().WithMessage(ValidationMessages.FuelRequired)
                .MaximumLength(20).WithMessage(ValidationMessages.FuelMaxLength);

            RuleFor(car => car.Transmission)
                .NotEmpty().WithMessage(ValidationMessages.TransmissionRequired)
                .MaximumLength(20).WithMessage(ValidationMessages.TransmissionMaxLength);

            RuleFor(car => car.FuelConsumption)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.FuelConsumptionNonNegative);

            RuleFor(car => car.Power)
                .GreaterThan(0).WithMessage(ValidationMessages.PowerGreaterThanZero);
        }

}

