namespace Hotel.DataAccess.Models
{
    public class RoomType
    {
        public int RoomTypeID { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        // Navigation property
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
