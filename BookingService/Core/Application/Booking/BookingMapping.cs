using Application.Bookings.Dtos;
using Domain.Bookings.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Application.Bookings
{
    public static class BookingMapping
    {
        public static BookingDto ToDto(Booking booking)
        {
            if (booking == null)
            {
                return null;
            }

            return new BookingDto
            {
                Id = booking.Id,
                PlacedAt = booking.PlacedAt,
                Start = booking.Start,
                End = booking.End,
                RoomId = booking.Room?.Id ?? 0,
                GuestId = booking.Guest?.Id ?? 0,
                StatusId = (int)booking.Status
            };
        }

        public static List<BookingDto> ToDtoList(IEnumerable<Booking> bookings)
        {
            return bookings?.Select(ToDto).ToList() ?? new List<BookingDto>();
        }
    }
}
