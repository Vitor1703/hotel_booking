using Application.Bookings.Dtos;
using Application.Bookings.Ports;
using Application.Bookings.Requests;
using Domain.Bookings.Entities;
using Domain.Bookings.Ports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Bookings
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingManager(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingDto> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            return booking != null ? BookingMapping.ToDto(booking) : null;
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            return BookingMapping.ToDtoList(bookings);
        }

        public async Task<BookingDto?> CreateBookingAsync(CreateBookingRequest request)
        {
            // Verificar se o quarto está ocupado
            var isRoomOccupied = await _bookingRepository.IsRoomOccupied(request.RoomId, request.CheckIn, request.CheckOut);
            if (isRoomOccupied)
            {
                throw new Exception("Room is occupied or unavailable for the selected time.");
            }

            // Criar a reserva
            var booking = new Booking
            {
                RoomId = request.RoomId,
                GuestId = request.GuestId,
                Start = request.CheckIn,
                End = request.CheckOut
            };

            booking = await _bookingRepository.Create(booking);
            return BookingMapping.ToDto(booking);
        }

        public async Task<bool> UpdateBookingAsync(int id, BookingDto bookingDto)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null) return false;

            // Verifica se o quarto está ocupado
            var isRoomOccupied = await _bookingRepository.IsRoomOccupied(bookingDto.RoomId, bookingDto.Start, bookingDto.End);
            if (isRoomOccupied)
            {
                throw new Exception("Room is occupied or unavailable for the selected time.");
            }

            booking.Start = bookingDto.Start;
            booking.End = bookingDto.End;
            booking.RoomId = bookingDto.RoomId;
            booking.GuestId = bookingDto.GuestId;

            await _bookingRepository.UpdateAsync(booking);
            return true;
        }


        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null) return false;

            await _bookingRepository.DeleteAsync(booking);
            return true;
        }
    }
}
