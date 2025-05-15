using FluentValidation;
using Tickfy.Enums;

namespace Tickfy.Contracts.Rooms;

public class SelectRoomRequestValidator: AbstractValidator<SelectRoomRequest>
{

    public SelectRoomRequestValidator()
    {


        RuleFor(r => r.BedType)
            .Must(bed => bed == null)
            .When(r => (RoomType)r.RoomType == RoomType.Single)
            .WithMessage(r => $"{nameof(r.OpptionalBedType)} must be empty when RoomType is {(RoomType)r.RoomType}.");


        RuleFor(r => r.OpptionalBedType)
           .Must(bed => bed == null)
           .When(r => (RoomType)r.RoomType == RoomType.Single)
            .WithMessage(r => $"{nameof(r.OpptionalBedType)} must be empty when RoomType is {(RoomType)r.RoomType}.");

        RuleFor(r => r.OpptionalBedType)
          .Must(bed => bed == null)
          .When(r => (RoomType)r.RoomType == RoomType.Double)
          .WithMessage(r => $"{nameof(r.OpptionalBedType)} must be empty when RoomType is {(RoomType)r.RoomType}.");

        RuleFor(r => r.OpptionalBedType)
           .Must(bed => bed == null)
           .When(r => (RoomType)r.RoomType == RoomType.Twin)
            .WithMessage(r => $"{nameof(r.OpptionalBedType)} must be empty when RoomType is {(RoomType)r.RoomType}.");

        RuleFor(r => r.OpptionalBedType)
           .Must(bed => bed == null)
           .When(r => (RoomType)r.RoomType == RoomType.Suite)
            .WithMessage(r => $"{nameof(r.OpptionalBedType)} must be empty when RoomType is {(RoomType)r.RoomType}.");

        RuleFor(r => r.OpptionalBedType)
           .Must(bed => bed == null)
           .When(r => (RoomType)r.RoomType == RoomType.Family)
            .WithMessage(r => $"{nameof(r.OpptionalBedType)} must be empty when RoomType is {(RoomType)r.RoomType}.");

        RuleFor(r => r.BedType)
            .Must(bed => bed == (int)BedType.Single || bed == (int)BedType.Double)
            .When(r => (RoomType)r.RoomType == RoomType.Twin && r.BedType != null)
            .WithMessage(r => $"Only Single or Double bed types are allowed for {(RoomType)r.RoomType} rooms.");




        RuleFor(r => r.BedType)
            .Must(bed => bed == (int)BedType.Double || bed == (int)BedType.Queen)
            .When(r => (RoomType)r.RoomType == RoomType.Double && r.BedType != null)
            .WithMessage(r => $"Only Queen or Double bed types are allowed for {(RoomType)r.RoomType} rooms.");



        RuleFor(r => r.BedType)
           .Must(bed => bed == (int)BedType.King || bed == (int)BedType.Queen)
           .When(r => (RoomType)r.RoomType == RoomType.Deluxe && r.BedType != null)
           .WithMessage(r => $"{nameof(r.BedType)} Only Queen or King bed types are allowed for {(RoomType)r.RoomType} rooms.");


        RuleFor(r => r.OpptionalBedType)
           .Must(bed => bed == (int)BedType.Crib || bed == (int)BedType.SofaBed)
           .When(r => (RoomType)r.RoomType == RoomType.Deluxe && r.OpptionalBedType != null)
           .WithMessage(r => $"{nameof(r.OpptionalBedType)} Only Crib or SofaBed bed types are allowed for {(RoomType)r.RoomType} rooms.");






    }
}
