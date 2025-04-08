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
                .Map(dest => dest.Type, src => src.Type)
                .Map(dest => dest.PricePerNight, src => src.PricePerNight)
                .Map(dest => dest.HotelId, src => src.HotelId);

            // Rooms to RoomRes
            config.NewConfig<Rooms, RoomRes>()
                .Map(dest => dest.RoomId, src => src.RoomId)
                .Map(dest => dest.RoomNumber, src => src.RoomNumber)
                .Map(dest => dest.Type, src => src.Type)
                .Map(dest => dest.BedCount, src => src.BedCount)
                .Map(dest => dest.PricePerNight, src => src.PricePerNight)
                .Map(dest => dest.DateIn, src => src.DateIn)
                .Map(dest => dest.DateOut, src => src.DateOut)
                .Map(dest => dest.Status, src => Enum.GetName(typeof(RoomStatus), src.Status))  // Convert enum to string
                .Map(dest => dest.HotelName, src => src.Hotel.Name);

            // Rooms to HotelRoomsRes
            config.NewConfig<Rooms, HotelRoomsRes>()
                .Map(dest => dest.RoomId, src => src.RoomId)
                .Map(dest => dest.RoomNumber, src => src.RoomNumber)
                .Map(dest => dest.Type, src => src.Type)
                .Map(dest => dest.BedCount, src => src.BedCount)
                .Map(dest => dest.PricePerNight, src => src.PricePerNight)
                .Map(dest => dest.Status, src => Enum.GetName(typeof(RoomStatus), src.Status) ?? "Unknown");  // Handle null enum values

        }
    }
}
