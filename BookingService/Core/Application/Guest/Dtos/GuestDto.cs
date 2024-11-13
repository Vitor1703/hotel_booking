namespace Application.Guests.Dtos
{
    public class GuestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string DocumentId { get; set; } // Representação como string, pode ser ajustada se necessário
    }
}
