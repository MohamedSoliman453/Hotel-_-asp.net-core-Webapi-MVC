using Microsoft.AspNetCore.Identity;

namespace Hotel.DataAccess.Models
{
    public class Customer : IdentityUser
    {

        // Navigation property
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
 