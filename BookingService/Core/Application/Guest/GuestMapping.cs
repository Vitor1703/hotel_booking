using Application.Guests.Dtos;
using Domain.Guests.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Application.Guests
{
    public static class GuestMapping
    {
        public static GuestDto ToDto(Guest guest)
        {
            if (guest == null)
                return null;

            return new GuestDto
            {
                Id = guest.Id,
                Name = guest.Name,
                Surname = guest.Surname,
                Email = guest.Email,
                DocumentId = guest.DocumentId?.IdNumber
            };
        }

        public static List<GuestDto> ToDtoList(IEnumerable<Guest> guests)
        {
            return guests?.Select(ToDto).ToList() ?? new List<GuestDto>();
        }
    }
}
