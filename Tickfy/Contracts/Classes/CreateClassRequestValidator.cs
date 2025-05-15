using FluentValidation;
using System.Data;
using Tickfy.Contracts.Classes;
using Tickfy.Entities;

namespace Tickfy.Contracts.Flights;

public class CreateClassRequestValidator : AbstractValidator<CreateClassRequest>
{
    public CreateClassRequestValidator()
    {
        RuleFor(c => c.className)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(c => c.Capacity)
           .NotEmpty()
          .GreaterThanOrEqualTo(5)
          .WithMessage("{PropertyName} must be greater than or equal to 5 seats");


        RuleFor(c => c)
            .Must(BeValidate)
            .WithName(nameof(CreateClassRequest.AvailableSeats))
            .WithMessage("{PropertyName} must be less than Capacity");

        RuleFor(c => c.Price)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);


    }

    private bool BeValidate(CreateClassRequest _classRequest)
    {
        return (_classRequest.Capacity >= _classRequest.AvailableSeats);
    }

}
