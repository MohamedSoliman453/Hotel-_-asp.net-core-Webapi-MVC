namespace Hotel.DataAccess.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public int HotelBranchID { get; set; }
        public int RoomTypeID { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public virtual HotelBranch HotelBranch { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual ICollection<RoomBooking> RoomBookings { get; set; }
    }
}
