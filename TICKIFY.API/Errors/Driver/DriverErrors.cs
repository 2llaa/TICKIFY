

using TICKIFY.API.Abstracts;
public class DriverErrors : Error
{
    public DriverErrors(string code, string description) : base(code, description)
    {
    }

    // Add more specific properties or methods if needed
    public static readonly DriverErrors DriverNotFound = new("DRIVER_NOT_FOUND", "Driver not found.");
    public static readonly Error NoDriversForHotel = new("NO_DRIVERS_FOR_HOTEL", "No drivers found for this hotel.");
}
