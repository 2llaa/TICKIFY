namespace Tickfy.Errors
{
    public class DriverErrors
    {
        public static readonly Error NoAnyDriver = 
            new Error("Driver.NotFound", "No drivers were found.");

    }
}
