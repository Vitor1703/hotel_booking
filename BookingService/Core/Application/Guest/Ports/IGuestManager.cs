using Application.Guests.Dtos;
using Application.Guests.Requests;

namespace Application.Guests.Ports
{
    public interface IGuestManager
    {
        Task<GuestDto> GetGuestByIdAsync(int id);
        Task<IEnumerable<GuestDto>> GetAllGuestsAsync();
        Task<GuestDto> CreateGuestAsync(CreateGuestRequest request);
        Task<bool> UpdateGuestAsync(int id, GuestDto guestDto);
        Task<bool> DeleteGuestAsync(int id);
    }
}
