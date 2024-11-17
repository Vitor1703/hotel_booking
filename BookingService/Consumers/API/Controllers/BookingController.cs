using Application.Bookings.Dtos;
using Application.Bookings.Ports;
using Application.Bookings.Requests;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                var res = await _bookingManager.CreateBookingAsync(request);

                if (res != null)
                    return Created("", res); // Retorna 201 Created com a URL do recurso

                _logger.LogError("Failed to create booking - No result returned.");
                return BadRequest(new { message = "Failed to create booking." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, BookingDto bookingDto)
        {
            try
            {
                var success = await _bookingManager.UpdateBookingAsync(id, bookingDto);

                if (success)
                    return NoContent(); // 204 No Content

                _logger.LogWarning($"Booking with ID {id} not found for update.");
                return NotFound(new { message = $"Booking with ID {id} not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating booking with ID {id}");
                return BadRequest(new { message = ex.Message });
            }
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
