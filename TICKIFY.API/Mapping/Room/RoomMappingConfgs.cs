using Mapster;
using TICKIFY.API.Contracts.Rooms;
using TICKIFY.Data.Entities;
using TICKIFY.Data.Enums;

namespace TICKIFY.API.Mapping.Room
{
    public class RoomMappingConfgs : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // RoomReq to Rooms
            config.NewConfig<RoomReq, Rooms>()
                .Map(dest => dest.RoomNumber, src => src.RoomNumber)
                .AfterMapping((src, dest) =>
                {
                    // Convert CarType string to the enum value after the mapping
                    if (Enum.TryParse<RoomType>(src.Type, true, out var type))
                    {
                        dest.Type = type; // Set the CarType enum value
                    }
                    else
                    {
                        dest.Type = RoomType.Single; // Default value if parsing fails
                    }
                })
                .Map(dest => dest.PricePerNight, src => src.PricePerNight)
                .Map(dest => dest.HotelId, src => src.HotelId);

            // Rooms to RoomRes
            config.NewConfig<Rooms, RoomRes>()
                .Map(dest => dest.RoomId, src => src.RoomId)
                .Map(dest => dest.RoomNumber, src => src.RoomNumber)
                .Map(dest => dest.Type, src => Enum.GetName(typeof(RoomType), src.Type)) // Convert enum to string

                 .Map(dest => dest.BedCount, src => src.BedCount)
                .Map(dest => dest.PricePerNight, src => src.PricePerNight)
                .Map(dest => dest.DateIn, src => src.DateIn)
                .Map(dest => dest.DateOut, src => src.DateOut)
                .Map(dest => dest.HotelName, src => src.Hotel.Name);

            TypeAdapterConfig<Rooms, RoomDto>.NewConfig();


            // Rooms to HotelRoomsRes
            config.NewConfig<Rooms, HotelRoomsRes>()
                .Map(dest => dest.RoomId, src => src.RoomId)
                .Map(dest => dest.RoomNumber, src => src.RoomNumber)
                .Map(dest => dest.Type, src => Enum.IsDefined(typeof(RoomType), src.Type)
                    ? Enum.GetName(typeof(RoomType), src.Type)
                    : "Unknown")
                .Map(dest => dest.BedCount, src => src.BedCount)
                .Map(dest => dest.PricePerNight, src => src.PricePerNight);

            //.Map(dest => dest.Status, src => Enum.GetName(typeof(RoomStatus), src.Status) ?? "Unknown");  // Handle null enum values

        }
    }
}
