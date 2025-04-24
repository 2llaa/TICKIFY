namespace TICKIFY.API.Contracts.HotelReservations
{
    public class ReservationReq
    {
        
            public int RoomId { get; set; }
            public int HotelId { get; set; }
       
            //public string HotelName { get; set; }
            public int DriverId { get; set; }
            public DateTime CheckInDate { get; set; }
            public DateTime CheckOutData { get; set; }
        

    }
}
