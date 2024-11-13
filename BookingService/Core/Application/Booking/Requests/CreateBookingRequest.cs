using System;

namespace Application.Bookings.Requests
{
    public class CreateBookingRequest
    {
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
