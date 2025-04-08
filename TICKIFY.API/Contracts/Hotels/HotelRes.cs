using TICKIFY.API.Contracts.Drivers;
using TICKIFY.Data.Enums;

namespace TICKIFY.API.Contracts.hotels
{
    public class HotelRes
    {
        public int HotelId { get; set; } // معرّف الفندق

        public string Name { get; set; } = string.Empty; // اسم الفندق

        public HotelCategory Category { get; set; } // الفئة (مثل فاخر، اقتصادي، إلخ)

        public string Location { get; set; } = string.Empty; // موقع الفندق (المدينة، الدولة)

        public int StarRating { get; set; } // تصنيف النجوم (من 1 إلى 5)

        public List<DriverRes> Drivers { get; set; } = new List<DriverRes>();
    }
}
