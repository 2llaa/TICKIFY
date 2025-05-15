using Tickfy.Enums;

namespace Tickfy.Contracts.Classes;

public record CreateClassRequest(
    int Id,
    String className,
    decimal Price,
    int Capacity,
    int AvailableSeats
);
