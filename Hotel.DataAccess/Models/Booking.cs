using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.DataAccess.Models
{
    public class Booking
    {
        public int ID { get; set; }
        public string CustomerID { get; set; }
        public DateTime? BookingDate { get; set; } = DateTime.Now;
        public int? NoOfPersons { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Status { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        // Navigation properties
        public virtual Customer Customer { get; set; }

        public virtual ICollection<RoomBooking> RoomBookings { get; set; }
    }
}
