using Application.Bookings.Dtos;
using Application.Bookings.Ports;
using Application.Bookings.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("booking")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingManager _bookingManager;

        public BookingController(
            ILogger<BookingController> logger,
            IBookingManager bookingManager)
        {
            _logger = logger;
            _bookingManager = bookingManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> Get(int id)
        {
            var res = await _bookingManager.GetBookingByIdAsync(id);

            if (res != null)
                return Ok(res);

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAll()
        {
            var res = await _bookingManager.GetAllBookingsAsync();
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Post(CreateBookingRequest request)
        {
            var res = await _bookingManager.CreateBookingAsync(request);

            if (res != null)
                return Created("", res);

            _logger.LogError("Failed to create booking");
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, BookingDto bookingDto)
        {
            var success = await _bookingManager.UpdateBookingAsync(id, bookingDto);

            if (success)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bookingManager.DeleteBookingAsync(id);

            if (success)
                return NoContent();

            return NotFound();
        }
    }
}
