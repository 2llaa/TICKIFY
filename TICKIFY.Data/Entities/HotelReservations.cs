using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TICKIFY.Data.Enums;

namespace TICKIFY.Data.Entities
{
    public class HotelReservations
    {
        public int HotelReservationId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public int HotelId { get; set; }
        public  Hotels Hotel { get; set; }
        public List<ReservationDetails> ReservationDetails { get; set; } = [];

        public HotelReservationStatus ReservationStatus { get; set; } = HotelReservationStatus.Pending;  

    }
}
