namespace Tickfy.Contracts.Hotels;

public record SearchAvailableHotelsRequest(
    DateTime CheckInDate,
    DateTime CheckOutDate,
    int NumberOfAdults,
    int NumberOfChildren,
    decimal MaxAmountOfMoney
);
