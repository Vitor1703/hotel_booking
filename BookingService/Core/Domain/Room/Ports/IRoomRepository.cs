using Domain.Rooms.Entities;
using System;

namespace Domain.Rooms.Ports
{
    public interface IRoomRepository
    {
        Task<bool> IsRoomAvailable(int roomId, DateTime startDate, DateTime endDate);
        Task<Room> CreateAsync(Room room); // Alterado para CreateAsync
        Task<Room> GetByIdAsync(int id);
        Task<IEnumerable<Room>> GetAllAsync();
        Task<IEnumerable<Room>> ListRooms(int offset, int take);
        Task UpdateAsync(Room room);
        Task DeleteAsync(Room room);
    }
}
