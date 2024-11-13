using Domain.Guests.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Guests.Ports
{
    public interface IGuestRepository
    {
        Task<Guest> GetByIdAsync(int id);
        Task<IEnumerable<Guest>> GetAllAsync();
        Task<Guest> Create(Guest guest);
        Task UpdateAsync(Guest guest);
        Task DeleteAsync(Guest guest);
    }
}
