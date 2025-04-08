using FluentValidation;

namespace TICKIFY.API.Contracts.hotels
{
    public class SearchHotelValidator : AbstractValidator<SearchHotelReq>
    {
        public SearchHotelValidator()
        {
            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(100).WithMessage("Location can't be longer than 100 characters.");
        }
    }
}
