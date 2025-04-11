using Mapster;
using TICKIFY.API.Contracts.HotelReservations;
using TICKIFY.Data.Entities;
using TICKIFY.Data.Enums;

namespace TICKIFY.API.Mapping.HotelReservation
{
    public class ReservationMappingConfgs : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // HotelReservationReq to HotelReservations 
            config.NewConfig<HotelReservationReq, HotelReservations>()
               // .Map(dest => dest.HotelId, src => src.HotelId)
                //.Map(dest => dest.CheckInDate, src => src.CheckInDate)
                .Map(dest => dest.CheckOutDate, src => src.CheckOutData);

            // HotelReservations (Entity) to HotelReservationRes
            config.NewConfig<HotelReservations, HotelReservationRes>()
                .Map(dest => dest.HotelReservationId, src => src.HotelReservationId)
                .Map(dest => dest.HotelId, src => src.HotelId)
                .Map(dest => dest.CheckInDate, src => src.CheckInDate)
                .Map(dest => dest.CheckOutDate, src => src.CheckOutDate)
                .Map(dest => dest.Details, src => src.Hotel.Drivers.Select(driver => new HotelDriverRes
                {
                  //  HotelName = Enum.GetName(typeof(HotelName), src.Hotel.Name) ?? "",  
                    RoomType = src.Hotel.Rooms.FirstOrDefault() != null
                    ? Enum.GetName(typeof(RoomType), src.Hotel.Rooms.FirstOrDefault().Type) ?? "Unknown"
                      : "Unknown",
                    PricePerNight = src.Hotel.Rooms.FirstOrDefault() != null && src.Hotel.Rooms.FirstOrDefault().PricePerNight != null
                     ? src.Hotel.Rooms.FirstOrDefault().PricePerNight
                :   0m, 

                    
                    DriverName = driver.DriverName ?? "", 
                    CarType = Enum.GetName(typeof(DriverCarType), driver.CarType) ?? ""  // CarType enum to string

                }).ToList());

            config.NewConfig<HotelReservations, ReservationRes>()
             .Map(dest => dest.DriverId,
              src => src.Hotel != null && src.Hotel.Drivers != null
               ? src.Hotel.Drivers.FirstOrDefault().DriverId
              : 0)
              .Map(dest => dest.ReservationId, src => src.HotelReservationId);

        }
    }

}
