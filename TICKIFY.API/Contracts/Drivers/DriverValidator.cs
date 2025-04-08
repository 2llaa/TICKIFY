using FluentValidation;
using TICKIFY.API.Contracts.Drivers;
using TICKIFY.API.Contracts.Requests;


namespace TICKIFY.API.Contracts.Drivers
{
    public class DriverValidator : AbstractValidator<DriverReq>
    {
        public DriverValidator()
        {
            // التحقق من أن اسم السواق غير فارغ
            RuleFor(driver => driver.DriverName)
                .NotEmpty().WithMessage("Driver name is required.")
                .Length(3, 100).WithMessage("Driver name must be between 3 and 100 characters.");

            // التحقق من نوع السيارة (يجب أن يكون قيمة صحيحة)
            RuleFor(driver => driver.CarType)
                .IsInEnum().WithMessage("Car type must be valid.");

            // التحقق من أن السعر أكبر من 0
            RuleFor(driver => driver.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            // التحقق من أن التقييم بين 1 و 5
            RuleFor(driver => driver.StarRating)
                .InclusiveBetween(1, 5).WithMessage("Star rating must be between 1 and 5.");


        }
    }
}