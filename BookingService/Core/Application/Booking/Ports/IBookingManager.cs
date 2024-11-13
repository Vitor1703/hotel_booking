using Application.Bookings.Dtos;
using Application.Bookings.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Bookings.Ports
{
    public interface IBookingManager
    {
        Task<BookingDto> GetBookingByIdAsync(int id);
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
        Task<BookingDto> CreateBookingAsync(CreateBookingRequest request);
        Task<bool> UpdateBookingAsync(int id, BookingDto bookingDto);
        Task<bool> DeleteBookingAsync(int id);
    }
}
