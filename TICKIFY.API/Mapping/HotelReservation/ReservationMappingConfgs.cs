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

            config.NewConfig<HotelReservations, ReservationRes>()
             .Map(dest => dest.DriverId,
              src => src.Hotel != null && src.Hotel.Drivers != null
               ? src.Hotel.Drivers.FirstOrDefault().DriverId
              : 0)
              .Map(dest => dest.ReservationId, src => src.HotelReservationId);

        }
    }

}
