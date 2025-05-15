using Tickfy.Enums;

namespace Tickfy.Contracts.Drivers
{
    public record DriverResponse(
       int Id,
       string DriverName,
       DriverCarType CarType,
       decimal Price,
       int StarRating
   );
}
