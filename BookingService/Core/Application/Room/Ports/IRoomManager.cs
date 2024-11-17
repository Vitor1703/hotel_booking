using Application.Rooms.Dtos;
using Application.Rooms.Requests;

namespace Application.Rooms.Ports;
public interface IRoomManager
{
    Task<IEnumerable<RoomDto>> GetAllRoomsAsync();
    Task<RoomDto?> GetRoomByIdAsync(int id);
    Task<RoomDto> CreateRoomAsync(CreateRoomRequest request);
    Task<bool> UpdateRoomAsync(int id, RoomDto roomDto);
    Task<bool> DeleteRoomAsync(int id);
}
