using Mapster;
using TICKIFY.API.Contracts.ReservationDetails;
using TICKIFY.Data.Entities;

namespace TICKIFY.API.Mapping.ReservationDetail
{
    public class DetailsMappingConfgs : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {


            // ReservationDetail to ReservationDetailRes 
            config.NewConfig<ReservationDetails, ReservationDetailRes>()
                .Map(dest => dest.Id, src => src.ReservationDetailsId);
        }
    }
}
