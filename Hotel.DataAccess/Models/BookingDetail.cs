namespace Hotel.DataAccess.Models
{
    public class BookingDetail
    {
        public int BookingDetailId { get; set; }
        public int BookingId { get; set; } // Foreign Key
        public int RoomId { get; set; } // Foreign Key


        // Navigation properties
        public Booking Booking { get; set; }
        public Room Room { get; set; }
    }


}