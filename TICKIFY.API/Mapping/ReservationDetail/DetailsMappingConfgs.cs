using Mapster;
using TICKIFY.API.Contracts.ReservationDetails;
using TICKIFY.Data.Entities;

namespace TICKIFY.API.Mapping.ReservationDetail
{
    public class DetailsMappingConfgs : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // ReservationDetailReq to ReservationDetail
            config.NewConfig<ReservationDetailReq, ReservationDetails>()
                .Map(dest => dest.RoomId, src => src.RoomId)

                .Map(dest => dest.HotelReservationId, src => src.HotelReservationId);

            // ReservationDetail to ReservationDetailRes 
            config.NewConfig<ReservationDetails, ReservationDetailRes>()
                .Map(dest => dest.Id, src => src.ReservationDetailsId)
                .Map(dest => dest.RoomId, src => src.RoomId)

                .Map(dest => dest.HotelReservationId, src => src.HotelReservationId);
        }
    }
}
