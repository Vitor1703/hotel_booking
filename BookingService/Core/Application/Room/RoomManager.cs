using Application.Rooms.Dtos;
using Application.Rooms.Ports;
using Domain.Rooms.Entities;
using Domain.Rooms.Ports;

namespace Application.Rooms
{
    public class RoomManager : IRoomManager
    {
        private readonly IRoomRepository _roomRepository;

        public RoomManager(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<RoomDto> GetRoomByIdAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return null;

            return new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Level,
                Price = room.Price
            };
        }

        public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync()
        {
            var rooms = await _roomRepository.GetAllAsync();
            return rooms.Select(room => new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Level,
                Price = room.Price
            });
        }

        public async Task<RoomDto> CreateRoomAsync(RoomDto roomDto)
        {
            var room = new Room
            {
                Name = roomDto.Name,
                Level = roomDto.Capacity,
                Price = roomDto.Price
            };

            await _roomRepository.CreateAsync(room);

            roomDto.Id = room.Id; // Assign the generated ID
            return roomDto;
        }

        public async Task<bool> UpdateRoomAsync(int id, RoomDto roomDto)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return false;

            room.Name = roomDto.Name;
            room.Level = roomDto.Capacity;
            room.Price = roomDto.Price;

            await _roomRepository.UpdateAsync(room);
            return true;
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return false;

            await _roomRepository.DeleteAsync(room);
            return true;
        }
    }
}
