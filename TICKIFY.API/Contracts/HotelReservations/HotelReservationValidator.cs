using FluentValidation;

namespace TICKIFY.API.Contracts.HotelReservations
{
    public class HotelReservationValidator : AbstractValidator<HotelReservationReq>
    {
        public HotelReservationValidator()
        {
            //RuleFor(x => x.HotelId)
            //    .GreaterThan(0).WithMessage("HotelId must be greater than 0.");

            //RuleFor(x => x.CheckInDate)
            //    .GreaterThan(DateTime.Now).WithMessage("CheckInDate must be in the future.");

            //RuleFor(x => x.CheckOutData)
            //    .GreaterThan(x => x.CheckInDate).WithMessage("CheckOutDate must be after CheckInDate.");

            //RuleFor(x => x.CheckOutData)
            //    .GreaterThan(DateTime.Now).WithMessage("CheckOutDate must be in the future.");
        }
    }
}
