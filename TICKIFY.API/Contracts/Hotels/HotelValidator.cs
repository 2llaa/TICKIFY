using TICKIFY.API.Contracts.hotels;
using TICKIFY.Data.Enums;

namespace TICKIFY.API.Contracts.Hotels
{
    public class HotelValidator
    {
        public List<string> ValidateHotel(HotelRes hotelRes)
        {
            var errors = new List<string>();

            // Validate hotel name
            if (string.IsNullOrEmpty(hotelRes.Name))
                errors.Add("Hotel name is required.");

            // Validate category (assumes enum values are 1, 2, 3, etc.)
            if (!Enum.IsDefined(typeof(HotelCategory), hotelRes.Category))
                errors.Add("Invalid hotel category.");

            // Validate location
            if (string.IsNullOrEmpty(hotelRes.Location))
                errors.Add("Hotel location is required.");

            // Validate star rating (should be between 1 and 5)
            if (hotelRes.StarRating < 1 || hotelRes.StarRating > 5)
                errors.Add("Star rating must be between 1 and 5.");

            // Validate drivers list (if present)
            if (hotelRes.Drivers != null && hotelRes.Drivers.Any())
            {
                //foreach (var driver in hotelRes.Drivers)
                //{
                //    var driverErrors = ValidateDriver(driver);
                //    errors.AddRange(driverErrors);
                //}
            }

            return errors;
        }
    }
}
