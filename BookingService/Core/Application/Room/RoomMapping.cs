using Application.Rooms.Dtos;
using Application.Rooms.Requests;
using Domain.Rooms.Entities;
using Domain.Rooms.Enums;
using Domain.Rooms.ValueObjects;

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
                Level = room.Level,
                IsInMaintenance = room.IsInMaintenance,
                PriceValue = room.Price.Value,
                Currency = room.Price.Currency.ToString() // Converte AcceptedCurrencies para string
            };
        }

        public static Room ToEntity(RoomDto roomDto)
        {
            return new Room
            {
                Id = roomDto.Id,
                Name = roomDto.Name,
                Level = roomDto.Level,
                IsInMaintenance = roomDto.IsInMaintenance,
                Price = new Price(roomDto.PriceValue, Enum.Parse<AcceptedCurrencies>(roomDto.Currency)) // Mapeia preço
            };
        }

        public static Room ToEntity(CreateRoomRequest request)
        {
            return new Room
            {
                Name = request.Name,
                Level = request.Level,
                IsInMaintenance = request.IsInMaintenance,
                Price = new Price(request.PriceValue, Enum.Parse<AcceptedCurrencies>(request.Currency)) // Constrói o preço
            };
        }

        public static IEnumerable<RoomDto> ToDtoList(IEnumerable<Room> rooms)
        {
            return rooms.Select(ToDto); // Converte lista de Room para RoomDto
        }
    }
}
