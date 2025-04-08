using FluentValidation;
using TICKIFY.API.Contracts.Rooms;
using TICKIFY.Data.Enums;

namespace TICKIFY.API.Validators.Rooms
{
    public class HotelRoomsResValidator : AbstractValidator<HotelRoomsRes>
    {
        public HotelRoomsResValidator()
        {
            RuleFor(x => x.RoomId)
                .GreaterThan(0).WithMessage("RoomId must be a positive integer.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid room status.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid room type.");

            RuleFor(x => x.RoomNumber)
                .GreaterThan(0).WithMessage("Room number must be greater than 0.");

            RuleFor(x => x.BedCount)
                .GreaterThan(0).WithMessage("Bed count must be greater than 0.");

            RuleFor(x => x.PricePerNight)
                .GreaterThan(0).WithMessage("Price per night must be a positive value.");
        }
    }
}
