using Domain.Rooms.ValueObjects;

namespace Application.Rooms.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public Price Price { get; set; }
    }
}
