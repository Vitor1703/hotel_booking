using Application.Common;
using Application.Guests.Dtos;
using Application.Guests.Ports;
using Application.Guests.Requests;
using Domain.Bookings.Ports;
using Domain.Guests.Entities;
using Domain.Guests.Ports;

namespace Application.Guests
{
    public class GuestManager : IGuestManager
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IBookingRepository _bookingRepository;

        public GuestManager(IGuestRepository guestRepository, IBookingRepository bookingRepository)
        {
            _guestRepository = guestRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<GuestDto> GetGuestByIdAsync(int id)
        {
            var guest = await _guestRepository.GetByIdAsync(id);
            return guest != null ? GuestMapping.ToDto(guest) : null;
        }

        public async Task<IEnumerable<GuestDto>> GetAllGuestsAsync()
        {
            var guests = await _guestRepository.GetAllAsync();
            return GuestMapping.ToDtoList(guests);
        }

        public async Task<GuestDto> CreateGuestAsync(CreateGuestRequest request)
        {
            var guest = new Guest
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                DocumentId = new Domain.Guests.ValueObjects.PersonId()//todo
            };

            guest = await _guestRepository.Create(guest);
            return GuestMapping.ToDto(guest);
        }

        public async Task<bool> UpdateGuestAsync(int id, GuestDto guestDto)
        {
            var guest = await _guestRepository.GetByIdAsync(id);
            if (guest == null) return false;

            guest.Name = guestDto.Name;
            guest.Surname = guestDto.Surname;
            guest.Email = guestDto.Email;
            guest.DocumentId = new Domain.Guests.ValueObjects.PersonId();//todo

            await _guestRepository.UpdateAsync(guest);
            return true;
        }

        public async Task<bool> DeleteGuestAsync(int id)
        {
            var guest = await _guestRepository.GetByIdAsync(id);
            if (guest == null)
            {
                return false; // Retorna false se o guest não existir
            }

            var hasBookings = await _bookingRepository.HasBookingsForGuest(id);
            if (hasBookings)
            {
                return false; // Retorna false se houver reservas associadas ao guest
            }

            await _guestRepository.DeleteAsync(guest);
            return true; // Retorna true se a exclusão foi bem-sucedida
        }
    }
}
