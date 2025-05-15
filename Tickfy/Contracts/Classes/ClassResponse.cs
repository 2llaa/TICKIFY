using Tickfy.Enums;

namespace Tickfy.Contracts.Classes;

public record ClassResponse (
   int Id,
   String ClassName,
   decimal Price,
   int AvailableSeats,
   int Capacity
);