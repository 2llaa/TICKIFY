using Mapster;
using Microsoft.EntityFrameworkCore.Internal;
using Tickfy.Contracts.BedInfos;
using Tickfy.Contracts.Classes;
using Tickfy.Contracts.Flights;
using Tickfy.Contracts.HotelReservations;
using Tickfy.Contracts.Hotels;
using Tickfy.Contracts.Rooms;
using Tickfy.Entities;
using Tickfy.Enums;

namespace Tickfy.Mapping;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<FlightReservation, FlightReservationResponse>()
            .Map(dest => dest.DepartureAirport, src => src.Flight.DepartureAirport)
            .Map(dest => dest.ArrivalAirport, src => src.Flight.ArrivalAirport)
            .Map(dest => dest.DepartureDate, src => src.Flight.DepartureDate)
            .Map(dest => dest.ArrivalDate, src => src.Flight.ArrivalDate)
            .Map(dest => dest.ClassName, src => src.Class.className)
            .Map(dest => dest.Price, src => src.Class.Price)
            .Map(dest => dest.CustomerName, src => src.CreatedBy.FirstName + " " + src.CreatedBy.LastName)
            .Map(dest => dest.CustomerEmail, src => src.CreatedBy.Email)
            .Map(dest => dest.CustomerPhone, src => src.CreatedBy.PhoneNumber);


        config.NewConfig<Flight, CreateFlightRequest>()
            .Map(dest => dest.Classes, src => src.Classes);

        config.NewConfig<Class, ClassResponse>()
            .Map(dest => dest.ClassName, src => src.className);



        config.NewConfig<BedInfo, BedInfoResponse>()
    .Map(dest => dest.Type, src => src.Type.ToString());

        config.NewConfig<Room, RoomResponse>()
            .Map(dest => dest.RoomType, src => src.RoomType.ToString())
            .Map(dest => dest.Beds, src => src.Beds.Adapt<IEnumerable<BedInfoResponse>>());

        config.NewConfig<Hotel, HotelResponse>()
            .Map(dest => dest.Rooms, src => src.Rooms.Adapt<IEnumerable<RoomResponse>>());





        config.NewConfig<BedInfo, BedInfoResponse>()
        .Map(dest => dest.Type, src => src.Type);

        config.NewConfig<Room, RoomResponse>()
            .Map(dest => dest.RoomType, src => src.RoomType);

        config.NewConfig<Hotel, HotelResponse>();






        //HotelReservation
        config.NewConfig<HotelReservation, HotelReservationResponse>()
            .Map(dest => dest.TotalPrice, src => src.TotaPrice)
            .Map(dest => dest.CustomerName, src => src.CreatedBy.FirstName + " " + src.CreatedBy.LastName)
            .Map(dest => dest.CustomerEmail, src => src.CreatedBy.Email)
            .Map(dest => dest.CustomerPhone, src => src.CreatedBy.PhoneNumber);
    }
}
