
namespace Tickfy.Errors;

public static class FlightErrors
{
    public static readonly Error NoAnyFlight =
        new Error(" No Any Flight Found", "No Flight was found.");
    public static readonly Error FlightNotFound = 
        new Error("Flight.NotFound","No Flight was found with the given ID.");
    public static readonly Error EmptyFlightResults =
        new Error("Empty Results.", "There is no Flights matches your criteria.");
    public static readonly Error DuplicateFlight =
     new Error("Duplicate Flight Found", "A flight with the same airport, departure time already exists.");
}
