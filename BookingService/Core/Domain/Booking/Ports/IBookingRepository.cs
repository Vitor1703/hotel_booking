using Domain.Bookings.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Bookings.Ports
{
    public interface IBookingRepository
    {
        Task<Booking> GetByIdAsync(int id); // Método para obter uma reserva pelo ID
        Task<IEnumerable<Booking>> GetAllAsync(); // Método para listar todas as reservas
        Task<Booking> Create(Booking booking); // Método para criar uma reserva
        Task UpdateAsync(Booking booking); // Método para atualizar uma reserva
        Task DeleteAsync(Booking booking); // Método para deletar uma reserva
        Task<bool> HasBookingsForGuest(int guestId);
        Task<bool> IsRoomOccupied(int roomId, DateTime start, DateTime end);
    }
}
