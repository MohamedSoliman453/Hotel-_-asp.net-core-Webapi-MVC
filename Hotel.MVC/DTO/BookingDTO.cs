namespace Hotel.MVC.DTO
{
    public class BookingDTO
    {
        public int ID { get; set; }
        public string CustomerID { get; set; }
        public int NoOfPersons { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        // Additional properties as needed
        // For example, customer or room details if those should be included in the response
    }

}
