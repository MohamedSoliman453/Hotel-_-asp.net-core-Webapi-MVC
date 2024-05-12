namespace Hotel.DataAccess.Models
{
    public class RoomBooking
    {
        public int BookingID { get; set; }
        public int RoomID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        // Navigation properties
        public virtual Booking Booking { get; set; }
        public virtual Room Room { get; set; }
    }
}
