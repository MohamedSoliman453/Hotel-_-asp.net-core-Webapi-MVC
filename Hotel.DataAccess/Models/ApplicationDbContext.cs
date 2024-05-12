using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotel.DataAccess.Models
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomBooking> RoomBookings { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<HotelBranch> HotelBranch { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships and keys here
            // Booking to Customer relationship
            modelBuilder.Entity<Booking>()
                 .HasOne(b => b.Customer)
                 .WithMany(c => c.Bookings)
                 .HasForeignKey(b => b.CustomerID);

            // RoomBooking to Booking relationship
            modelBuilder.Entity<RoomBooking>()
                .HasOne(rb => rb.Booking)
                .WithMany(b => b.RoomBookings)
                .HasForeignKey(rb => rb.BookingID);

            modelBuilder.Entity<RoomBooking>()
       .HasKey(rb => new { rb.BookingID, rb.RoomID });
            // RoomBooking to Room relationship
            modelBuilder.Entity<RoomBooking>()
                .HasOne(rb => rb.Room)
                .WithMany(r => r.RoomBookings)
                .HasForeignKey(rb => rb.RoomID);

            // Room to RoomType relationship
            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeID);

            modelBuilder.Entity<HotelBranch>().HasData(
        new HotelBranch { HotelBranchID = 1, BranchName = "Downtown Hotel", Address = "123 Downtown Ave" },
        new HotelBranch { HotelBranchID = 2, BranchName = "Airport Hotel", Address = "1 Airport Rd" },
        new HotelBranch { HotelBranchID = 3, BranchName = "Beachside Hotel", Address = "100 Beach Dr" },
        new HotelBranch { HotelBranchID = 4, BranchName = "Mountain View Hotel", Address = "200 Mountain St" },
        new HotelBranch { HotelBranchID = 5, BranchName = "City Center Hotel", Address = "500 City Center Blvd" }
    );
            modelBuilder.Entity<RoomType>().HasData(
        new RoomType { RoomTypeID = 1, TypeName = "Single", Description = "Single Room", Price = 100m },
        new RoomType { RoomTypeID = 2, TypeName = "Double", Description = "Double Room", Price = 150m },
        new RoomType { RoomTypeID = 3, TypeName = "Triple", Description = "Triple Room", Price = 180m },
        new RoomType { RoomTypeID = 4, TypeName = "Suite", Description = "Suite Room", Price = 220m }
    );
            var roomId = 1;
            foreach (var branchId in new int[] { 1, 2, 3, 4, 5 })
            {
                foreach (var roomTypeId in new int[] { 1, 2, 3, 4 }) // Room types
                {
                    for (int i = 0; i < 4; i++) // 4 rooms for each room type in each branch
                    {
                       
                        modelBuilder.Entity<Room>().HasData(
                            new Room { RoomID = roomId++, HotelBranchID = branchId, RoomTypeID = roomTypeId, Status = "Available"}
                        );
                    }
                }
            }
            base.OnModelCreating(modelBuilder);

        }


    }
}
