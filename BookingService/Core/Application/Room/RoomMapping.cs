using Application.Rooms.Dtos;
using Domain.Rooms.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Application.Rooms
{
    public static class RoomMapping
    {
        public static RoomDto ToDto(Room room)
        {
            return new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Level,
                Price = room.Price
            };
        }

        public static Room ToEntity(RoomDto roomDto)
        {
            return new Room
            {
                Id = roomDto.Id,
                Name = roomDto.Name,
                Level = roomDto.Capacity,
                Price = roomDto.Price
            };
        }

        public static List<RoomDto> ToDtoList(IEnumerable<Room> rooms)
        {
            return rooms.Select(ToDto).ToList();
        }
    }
}
