using FluentValidation;

namespace TICKIFY.API.Contracts.Rooms
{
    public class RoomValidator : AbstractValidator<RoomReq>
    {
        public RoomValidator()
        {

            //RuleFor(x => x.)
            //    .NotEmpty().WithMessage("Bed count is required.")
            //    .GreaterThan(0).WithMessage("Bed count must be greater than zero.");


            //RuleFor(x => x.DateIn)
            //    .NotEmpty().WithMessage("Check-in date is required.")
            //    .GreaterThan(DateTime.Now).WithMessage("Check-in date must be in the future.");

            //RuleFor(x => x.DateOut)
            //    .NotEmpty().WithMessage("Check-out date is required.")
            //    .GreaterThan(x => x.DateIn).WithMessage("Check-out date must be after the check-in date.");

            //RuleFor(x => x.HotelId)
            //    .NotEmpty().WithMessage("Hotel ID is required.");
        }
    }
}
