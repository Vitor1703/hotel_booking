using Application.Rooms.Dtos;
using Application.Rooms.Ports;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/room")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomManager _roomManager;

        public RoomController(IRoomManager roomManager)
        {
            _roomManager = roomManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await _roomManager.GetRoomByIdAsync(id);
            return room != null ? Ok(room) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await _roomManager.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoomDto roomDto)
        {
            var room = await _roomManager.CreateRoomAsync(roomDto);
            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RoomDto roomDto)
        {
            var result = await _roomManager.UpdateRoomAsync(id, roomDto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _roomManager.DeleteRoomAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
