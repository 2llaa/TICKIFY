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
                });

            // Rooms to RoomRes
            config.NewConfig<Rooms, RoomRes>()

                .Map(dest => dest.Type, src => Enum.GetName(typeof(RoomType), src.Type)) ;

            TypeAdapterConfig<Rooms, RoomDto>.NewConfig();


            // Rooms to HotelRoomsRes
            config.NewConfig<Rooms, HotelRoomsRes>()

                .Map(dest => dest.Type, src => Enum.IsDefined(typeof(RoomType), src.Type)
                    ? Enum.GetName(typeof(RoomType), src.Type)
                    : "Unknown"); // Handle null enum values

        }
    }
}
