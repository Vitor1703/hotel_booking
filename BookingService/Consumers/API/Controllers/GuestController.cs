using Application.Guests.Dtos;
using Application.Guests.Ports;
using Application.Guests.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("guest")]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IGuestManager _guestManager;

        public GuestController(
            ILogger<GuestController> logger,
            IGuestManager guestManager)
        {
            _logger = logger;
            _guestManager = guestManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestDto>> Get(int id)
        {
            var res = await _guestManager.GetGuestByIdAsync(id);

            if (res != null)
                return Ok(res);

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestDto>>> GetAll()
        {
            var res = await _guestManager.GetAllGuestsAsync();
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<GuestDto>> Post(CreateGuestRequest request)
        {
            var res = await _guestManager.CreateGuestAsync(request);

            if (res != null)
                return Created("", res);

            _logger.LogError("Failed to create guest");
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, GuestDto guestDto)
        {
            var success = await _guestManager.UpdateGuestAsync(id, guestDto);

            if (success)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _guestManager.DeleteGuestAsync(id);
            if (!deleted)
            {
                return BadRequest(); // Retorna 400 se a exclusão falhou (guest não encontrado ou com dependências)
            }

            return Ok(); // Retorna 200 se a exclusão foi bem-sucedida
        }
    }
}
