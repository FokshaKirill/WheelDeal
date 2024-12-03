using FluentValidation;
using WheelDeal.Domain.Database.Entities;

namespace WheelDeal.Domain.Validation.Validators;

public class CarValidator : AbstractValidator<Car>
{
    public CarValidator()
    {
        RuleFor(car => car.Brand)
            .NotEmpty().WithMessage(ValidationMessages.CarBrandRequired)
            .Length(1, 50).WithMessage(ValidationMessages.CarBrandLength);

        RuleFor(car => car.Model)
            .NotEmpty().WithMessage(ValidationMessages.CarModelRequired)
            .Length(1, 50).WithMessage(ValidationMessages.CarModelLength);

        RuleFor(car => car.Year)
            .InclusiveBetween(1900, 2100).WithMessage(ValidationMessages.CarYearRange);

        RuleFor(car => car.PlacesCount)
            .GreaterThan(0).WithMessage(ValidationMessages.CarPlacesCountPositive);

        RuleFor(car => car.EngineValue)
            .InclusiveBetween(0, 10).WithMessage(ValidationMessages.CarEngineValueRange);

        RuleFor(car => car.Mileage)
            .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.CarMileageNonNegative);

        RuleFor(car => car.Body)
            .Length(0, 20).WithMessage(ValidationMessages.CarBodyLength);

        RuleFor(car => car.Fuel)
            .Length(0, 20).WithMessage(ValidationMessages.CarFuelLength);

        RuleFor(car => car.Transmission)
            .Length(0, 20).WithMessage(ValidationMessages.CarTransmissionLength);

        RuleFor(car => car.FuelConsumption)
            .InclusiveBetween(0, 100).WithMessage(ValidationMessages.CarFuelConsumptionRange);

        RuleFor(car => car.Power)
            .GreaterThan(0).WithMessage(ValidationMessages.CarPowerPositive);
    }
}