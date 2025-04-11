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
        public string Status { get; set; }
       // public int UserId { get; set; }
        //public int TotalPrice { get; set; } = 0;
        public string GuestName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int HotelId { get; set; }
        public  Hotels Hotel { get; set; }
        public int RoomId { get; set; }
        public Rooms Room { get; set; }
        public List<ReservationDetails> ReservationDetails { get; set; } = [];


    }
}
