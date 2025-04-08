

using Mapster;
using TICKIFY.API.Contracts.Drivers;
using TICKIFY.API.Contracts.Requests;
using TICKIFY.Data.Entities;
using TICKIFY.Data.Enums;

namespace TICKIFY.API.Mapping.Driver
{
    public class DriverMappingConfgs : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            #region DriverReq to Driver
            config.NewConfig<DriverReq, Drivers>()
                .Map(dest => dest.DriverName, src => src.DriverName)
                .Map(dest => dest.CarType, src => Enum.GetName(typeof(DriverCarType), src.CarType))  //mapping enum to string
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.StarRating, src => src.StarRating)
                .Map(dest => dest.HotelId, src => src.HotelId);
            #endregion

            #region Driver to DriverRes
            config.NewConfig<Drivers, DriverRes>()
                .Map(dest => dest.DriverId, src => src.DriverId)
                .Map(dest => dest.DriverName, src => src.DriverName)
                .Map(dest => dest.CarType, src => Enum.GetName(typeof(DriverCarType), src.CarType))  // Convert enum to string
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.StarRating, src => src.StarRating)
                .Map(dest => dest.HotelId, src => src.HotelId);
            #endregion

            #region Driver to HotelDriversRes
            config.NewConfig<Drivers, HotelDriversRes>()
                .Map(dest => dest.DriverId, src => src.DriverId)
                .Map(dest => dest.DriverName, src => src.DriverName)
                .Map(dest => dest.CarType, src => Enum.GetName(typeof(DriverCarType), src.CarType))  // Convert enum to string
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.StarRating, src => src.StarRating);
            #endregion

        }
    }
}
