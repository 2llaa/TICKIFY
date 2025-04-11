using FluentValidation;
using TICKIFY.API.Contracts.Drivers;
using TICKIFY.API.Contracts.Requests;
using TICKIFY.Data.Enums;


namespace TICKIFY.API.Contracts.Drivers
{
    public class DriverValidator : AbstractValidator<DriverReq>
    {
        public DriverValidator()
        {
            RuleFor(driver => driver.DriverName)
                .NotEmpty().WithMessage("Driver name is required.")
                .Length(3, 100).WithMessage("Driver name must be between 3 and 100 characters.");

            RuleFor(driver => driver.CarType)
                .Must(value => Enum.TryParse<DriverCarType>(value, true, out _))
                .WithMessage("Car type must be valid.");


            RuleFor(driver => driver.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(driver => driver.StarRating)
                .InclusiveBetween(1, 5).WithMessage("Star rating must be between 1 and 5.");


        }
    }
}