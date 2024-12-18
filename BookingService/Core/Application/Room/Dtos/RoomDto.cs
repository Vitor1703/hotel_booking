﻿namespace Application.Rooms.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsInMaintenance { get; set; }
        public decimal PriceValue { get; set; } // Valor do preço
        public string Currency { get; set; }    // Moeda do preço
    }
}
