using Hotel.API.DTO;
using Hotel.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.API.Interfaces
{

    public interface IBookingService
    {
        Task<BookingDTO> CreateBookingAsync(BookingDTO bookingDto);
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(string customerId);
        Task UpdateBookingAsync(BookingDTO bookingDto);
        Task DeleteBookingAsync(int bookingId);
        //Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut, string branchName);
        Task<IEnumerable<RoomStatusDTO>> GetAllRoomStatusesAsync();
        Task AssignRoomToBooking(RoomBookingDTO roomBookingDto);
        Task<decimal> CalculateTotalPrice(int bookingId);


    }
}
