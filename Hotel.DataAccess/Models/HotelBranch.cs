namespace Hotel.DataAccess.Models
{
    public class HotelBranch
    {
        public int HotelBranchID { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }

        // Navigation property
        public ICollection<Room> Rooms { get; set; }

    }
}