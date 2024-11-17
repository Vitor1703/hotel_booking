using Application.Rooms.Dtos;
using Application.Rooms.Ports;
using Application.Rooms.Requests;
using Domain.Rooms.Entities;
using Domain.Rooms.Ports;

namespace Application.Rooms;

public class RoomManager : IRoomManager
{
    private readonly IRoomRepository _roomRepository;

    public RoomManager(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync()
    {
        var rooms = await _roomRepository.GetAllAsync();
        return RoomMapping.ToDtoList(rooms);
    }

    public async Task<RoomDto?> GetRoomByIdAsync(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        return room != null ? RoomMapping.ToDto(room) : null;
    }

    public async Task<RoomDto> CreateRoomAsync(CreateRoomRequest request)
    {
        var room = RoomMapping.ToEntity(request);
        room = await _roomRepository.CreateAsync(room);

        return RoomMapping.ToDto(room);
    }


    public async Task<bool> UpdateRoomAsync(int id, RoomDto roomDto)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null) return false;

        room.Name = roomDto.Name;
        room.Level = roomDto.Level;
        room.IsInMaintenance = roomDto.IsInMaintenance;
        room.Price = new Domain.Rooms.ValueObjects.Price(roomDto.PriceValue, Enum.Parse<Domain.Rooms.Enums.AcceptedCurrencies>(roomDto.Currency));

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
