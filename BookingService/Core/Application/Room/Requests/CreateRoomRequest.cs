namespace Application.Rooms.Requests
{
    public class CreateRoomRequest
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsInMaintenance { get; set; }
        public decimal PriceValue { get; set; } // Valor do preço
        public string Currency { get; set; }    // Moeda do preço
    }
}
