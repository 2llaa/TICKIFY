using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public decimal TotalPrice => (Room?.PricePerNight ?? 0) *
                                   (int)(CheckOutDate - CheckInDate).TotalDays +
                                   (Driver?.Price ?? 0);
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public int HotelId { get; set; }
        public  Hotels Hotel { get; set; }
        public int RoomId { get; set; }
        public Rooms Room { get; set; }
        public int DriverId { get; set; }
        public Drivers Driver { get; set; }
        public List<ReservationDetails> ReservationDetails { get; set; } = [];




    }
}
