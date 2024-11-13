namespace Application.Guests.Requests
{
    public class CreateGuestRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string DocumentId { get; set; } // Representação como string, pode ser ajustada se necessário
    }
}
