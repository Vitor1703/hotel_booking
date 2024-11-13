using Application.Rooms.Dtos;

namespace Application.Rooms.Ports
{
    public interface IRoomManager
    {
        Task<RoomDto> GetRoomByIdAsync(int id);               // Obtém uma sala pelo ID
        Task<IEnumerable<RoomDto>> GetAllRoomsAsync();        // Lista todas as salas
        Task<RoomDto> CreateRoomAsync(RoomDto roomDto);       // Cria uma nova sala
        Task<bool> UpdateRoomAsync(int id, RoomDto roomDto);  // Atualiza uma sala existente
        Task<bool> DeleteRoomAsync(int id);                   // Exclui uma sala pelo ID
    }
}
