using Microsoft.AspNetCore.Mvc;
using Hotel.DataAccess.Models;
using System;
using System.Threading.Tasks;
using Hotel.API.Interfaces;
using Hotel.API.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpGet("room-statuses")]
        public async Task<IActionResult> GetRoomStatuses()
        {
            var roomStatuses = await _bookingService.GetAllRoomStatusesAsync();
            return Ok(roomStatuses);
        }

        // POST: api/booking
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDTO booking)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdBooking = await _bookingService.CreateBookingAsync(booking);
                return CreatedAtAction(nameof(GetBooking), new { id = createdBooking.ID }, createdBooking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("bookroom")]
        public async Task<IActionResult> BookRoom([FromBody] RoomBookingDTO roomBookingDto)
        {
            try
            {
                await _bookingService.AssignRoomToBooking(roomBookingDto);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{bookingId}/totalprice")]
        public async Task<IActionResult> GetTotalPrice(int bookingId)
        {
            try
            {
                var totalPrice = await _bookingService.CalculateTotalPrice(bookingId);
                return Ok(totalPrice);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }

        [HttpGet("bycustomer/{customerId}")]
        public async Task<IActionResult> GetBookingsByCustomerId(string customerId)
        {
            var bookings = await _bookingService.GetBookingsByCustomerIdAsync(customerId);
            if (bookings == null || !bookings.Any())
            {
                return NotFound();
            }
            return Ok(bookings);
        }

        // GET: api/booking/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        // PUT: api/booking/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingDTO bookingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != bookingDto.ID)
                return BadRequest("Mismatched booking ID.");

            try
            {
                await _bookingService.UpdateBookingAsync(bookingDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Booking with ID {id} not found.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, ex.Message);
            }
        }


        // DELETE: api/booking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                await _bookingService.DeleteBookingAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, ex.Message);
            }
        }

    }
}