using Hotel.API.DTO;
using Hotel.API.Interfaces;
using Hotel.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RoomStatusDTO>> GetAllRoomStatusesAsync()
        {
            var roomStatuses = await _context.Rooms
                .Include(r => r.RoomType)
                .Select(r => new RoomStatusDTO
                {
                    RoomID = r.RoomID,
                    RoomTypeName = r.RoomType.TypeName, 
                    BranchName = r.HotelBranch.BranchName,
                    Status = r.RoomBookings.Any(rb => rb.CheckInDate <= DateTime.Now && rb.CheckOutDate >= DateTime.Now) ? "Booked" : "Available"
                })
                .ToListAsync();

            return roomStatuses;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(string customerId)
        {
            return await _context.Bookings
                                 .Where(b => b.CustomerID == customerId)
                                 .ToListAsync();
        }
        private async Task<bool> IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            return !await _context.RoomBookings.AnyAsync(rb =>
                rb.RoomID == roomId &&
                rb.CheckInDate < checkOutDate &&
                rb.CheckOutDate > checkInDate);
        }

        public async Task AssignRoomToBooking(RoomBookingDTO roomBookingDto)
        {
            var isRoomAvailable = await IsRoomAvailable(roomBookingDto.RoomID, roomBookingDto.CheckInDate, roomBookingDto.CheckOutDate);
            if (!isRoomAvailable)
            {
                throw new InvalidOperationException("The room is not available for the selected dates.");
            }

            var roomBooking = new RoomBooking
            {
                BookingID = roomBookingDto.BookingID,
                RoomID = roomBookingDto.RoomID,
                CheckInDate = roomBookingDto.CheckInDate,
                CheckOutDate = roomBookingDto.CheckOutDate
            };

            _context.RoomBookings.Add(roomBooking);
            await _context.SaveChangesAsync();
        }

        public async Task<BookingDTO> CreateBookingAsync(BookingDTO bookingDto)
        {
            var booking = new Booking
            {
                CustomerID = bookingDto.CustomerID,
                NoOfPersons = bookingDto.NoOfPersons,
                CheckInDate = bookingDto.CheckInDate,
                CheckOutDate = bookingDto.CheckOutDate,
            };

            var customer = await _context.Customers.FindAsync(booking.CustomerID);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }

            try
            {
                _context.Bookings.Add(booking);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }



            return bookingDto;
        }



        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            return await _context.Bookings
                                 .FirstOrDefaultAsync(b => b.ID == bookingId);
        }


        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.RoomBookings)
                .ThenInclude(rb => rb.Room)
                .ToListAsync();
        }

        public async Task UpdateBookingAsync(BookingDTO bookingDto)
        {
            var booking = await GetBookingByIdAsync(bookingDto.ID);

            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found.");
            }

            booking.CustomerID = bookingDto.CustomerID;
            booking.CheckInDate = bookingDto.CheckInDate;
            booking.CheckOutDate = bookingDto.CheckOutDate;
            booking.NoOfPersons = bookingDto.NoOfPersons;
            
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteBookingAsync(int bookingId)
        {
            var booking = await GetBookingByIdAsync(bookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> CalculateTotalPrice(int bookingId)
        {
            var booking = await _context.Bookings
                                       .Include(b => b.RoomBookings)
                                           .ThenInclude(rb => rb.Room)
                                           .ThenInclude(r => r.RoomType)
                                       .SingleOrDefaultAsync(b => b.ID == bookingId);

            if (booking == null)
                throw new KeyNotFoundException("Booking not found.");

            var customer = await _context.Customers.FindAsync(booking.CustomerID);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            var totalPrice = booking.RoomBookings.Sum(rb =>
                (rb.CheckOutDate.Date - rb.CheckInDate.Date).Days * rb.Room.RoomType.Price);

            if (IsEligibleForDiscount(customer))
            {
                totalPrice *= 0.95m;
            }
            booking.TotalPrice = totalPrice;

            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
            return totalPrice;
        }

        private bool IsEligibleForDiscount(Customer customer)
        {
            int bookingCount = _context.Bookings.Count(b => b.CustomerID == customer.Id);
            return bookingCount > 1;
        }



    }
}
