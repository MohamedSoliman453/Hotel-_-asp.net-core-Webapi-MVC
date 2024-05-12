using Hotel.MVC.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hotel.MVC.Controllers
{
    public class BookingController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ITokenService _tokenService;

        public BookingController(IHttpClientFactory clientFactory, ITokenService okenService)
        {
            _clientFactory = clientFactory;
            _tokenService = okenService;
        }

        public async Task<IActionResult> CheckAvailability()
        {
            var client = _clientFactory.CreateClient("BookingAPI");
            var response = await client.GetAsync("api/booking/room-statuses");
            if (response.IsSuccessStatusCode)
            {
                var roomStatuses = await response.Content.ReadFromJsonAsync<IEnumerable<RoomStatusDTO>>();
                return View(roomStatuses);
            }
            return View("Error"); // Or handle the error appropriately
        }

        public IActionResult CreateBooking()
        {
            return View(new BookingDTO());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingDTO bookingDto)
        {
            var userId = _tokenService.GetUserIdFromToken();
            bookingDto.CustomerID = userId; 

            var client = _clientFactory.CreateClient("BookingAPI");
            var response = await client.PostAsJsonAsync("api/booking", bookingDto);

            if (response.IsSuccessStatusCode)
            {
                var createdBooking = await response.Content.ReadFromJsonAsync<BookingDTO>();
                int bookingId = createdBooking.ID;
                return RedirectToAction("ChooseRoom", new { bookingId = bookingId });
            }

            return View("Error");
        }

        public async Task<IActionResult> ChooseRoom(int bookingId)
        {
            var client = _clientFactory.CreateClient("BookingAPI");
            var response = await client.GetAsync("api/booking/room-statuses");

            if (response.IsSuccessStatusCode)
            {
                var availableRooms = await response.Content.ReadFromJsonAsync<IEnumerable<RoomStatusDTO>>();
                ViewBag.AvailableRooms = new SelectList(availableRooms.Where(r => r.Status == "Available"), "RoomID", "RoomTypeName");
                ViewBag.BookingId = bookingId;
                return View();
            }
            return View("Error"); 
        }

        [HttpPost]
        public async Task<IActionResult> BookRoom(RoomBookingDTO roomBookingDto)
        {
            var client = _clientFactory.CreateClient("BookingAPI");
            var response = await client.PostAsJsonAsync("api/booking/bookroom", roomBookingDto);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetTotalPrice", new { bookingId = roomBookingDto.BookingID });
            }
            else
            {
               
                return View("Error"); 
            }
        }



        public async Task<IActionResult> GetTotalPrice(int bookingId)
        {
            var client = _clientFactory.CreateClient("BookingAPI");
            var response = await client.GetAsync($"api/booking/{bookingId}/totalprice");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var totalPrice = JsonConvert.DeserializeObject<decimal>(responseString); ViewBag.TotalPrice = totalPrice;
                return View("BookingDetails", new { bookingId = bookingId });
            }
            return View("Error"); 
        }

        public async Task<IActionResult> BookingDetails(int bookingId)
        {
            var client = _clientFactory.CreateClient("BookingAPI");
            var response = await client.GetAsync($"api/booking/{bookingId}");

            if (response.IsSuccessStatusCode)
            {
                var bookingDetails = await response.Content.ReadFromJsonAsync<BookingDTO>();
                return View(bookingDetails);
            }
            return View("Error"); 
        }

    }
}
