

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
                .AfterMapping((src, dest) =>
                {
                    if (Enum.TryParse<DriverCarType>(src.CarType, true, out var carType))
                    {
                        dest.CarType = carType; 
                    }
                    else
                    {
                        dest.CarType = DriverCarType.None; 
                    }
                });
            #endregion

            #region Driver to DriverRes
            config.NewConfig<Drivers, DriverRes>()
                .Map(dest => dest.CarType, src => Enum.GetName(typeof(DriverCarType), src.CarType)); // Convert enum to string
            #endregion







            #region Driver to HotelDriversRes
            config.NewConfig<Drivers, HotelDriversRes>()
                .Map(dest => dest.CarType, src => Enum.GetName(typeof(DriverCarType), src.CarType)) ;
            #endregion

        }
    }
}
