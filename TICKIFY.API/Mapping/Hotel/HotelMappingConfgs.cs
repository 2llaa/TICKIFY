using Mapster;
using TICKIFY.API.Contracts.hotels;
using TICKIFY.API.Contracts.Requests;
using TICKIFY.Data.Entities;
using TICKIFY.API.Contracts.Drivers;
using TICKIFY.Data.Enums;
using TICKIFY.API.Contracts.Rooms;

namespace TICKIFY.API.Mapping.Hotel
{
    public class HotelMappingConfgs : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {


            #region  HotelReq to Hotels 


            config.NewConfig<HotelReq, Hotels>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Category, src => src.Category)
                .Map(dest => dest.Location, src => src.Location)
                .Map(dest => dest.StarRating, src => src.StarRating);

            #endregion


            #region Hotel to HotelRes

            config.NewConfig<Hotels, HotelRes>()
                .Map(dest => dest.HotelId, src => src.HotelId)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Category, src => src.Category)
                .Map(dest => dest.Location, src => src.Location)
                .Map(dest => dest.StarRating, src => src.StarRating)
                 .Map(dest => dest.Drivers, src => src.Drivers.Select(d => new DriverRes
                  {
                     DriverName = d.DriverName,
                     CarType = Enum.GetName(typeof(DriverCarType), d.CarType),
                     Price = d.Price,
                     StarRating = d.StarRating
                  }));

            #endregion


            #region searchHotelReq to Hotels 

            config.NewConfig<SearchHotelReq, Hotels>()
                .Map(dest => dest.Name, src => Enum.GetName(typeof(HotelName), src.Name))  // تحويل enum إلى string
                .Map(dest => dest.Location, src => src.Location)
                .Map(dest => dest.StarRating, src => src.StarRating);

            #endregion



            #region hotels to SearchHotelRes

            config.NewConfig<Hotels, SearchHotelRes>()
                .Map(dest => dest.HotelId, src => src.HotelId)
                .Map(dest => dest.Name, src => Enum.GetName(typeof(HotelName), src.Name))  // تحويل enum إلى string
                .Map(dest => dest.Category, src => src.Category)
                .Map(dest => dest.Location, src => src.Location)
                .Map(dest => dest.StarRating, src => src.StarRating)

                .Map(dest => dest.Drivers, src => src.Drivers.Select(d => new HotelDriversRes
                {
                    DriverId = d.DriverId,
                    DriverName = d.DriverName,
                     CarType = Enum.GetName(typeof(DriverCarType), d.CarType),
                     Price = d.Price,
                     StarRating = d.StarRating
                 }))

                .Map(dest => dest.Rooms, src => src.Rooms.Select(r => new HotelRoomsRes
                {
                     RoomId = r.RoomId,
                     Type = r.Type,
                    PricePerNight = r.PricePerNight,
                    Status = Enum.GetName(typeof(RoomStatus), r.Status),
                }));
            #endregion
        }
    }

}
