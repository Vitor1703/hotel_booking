using Application.Rooms.Dtos;
using Application.Rooms.Ports;
using Application.Rooms.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("room")]
public class RoomController : ControllerBase
{
    private readonly IRoomManager _roomManager;

    public RoomController(IRoomManager roomManager)
    {
        _roomManager = roomManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomDto>>> GetAll()
    {
        var rooms = await _roomManager.GetAllRoomsAsync();
        return Ok(rooms);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDto>> GetById(int id)
    {
        var room = await _roomManager.GetRoomByIdAsync(id);
        if (room == null) return NotFound();
        return Ok(room);
    }

    [HttpPost]
    public async Task<ActionResult<RoomDto>> Post([FromBody] CreateRoomRequest request)
    {
        var room = await _roomManager.CreateRoomAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] RoomDto roomDto)
    {
        var result = await _roomManager.UpdateRoomAsync(id, roomDto);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _roomManager.DeleteRoomAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
