namespace Hotel.MVC.DTO
{
    public class RoomBookingDTO
    {
        public int BookingID { get; set; }
        public int RoomID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}