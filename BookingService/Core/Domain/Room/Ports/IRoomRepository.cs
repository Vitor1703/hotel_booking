using Domain.Rooms.Entities;

namespace Domain.Rooms.Ports
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync(); // Recupera todos os quartos
        Task<Room?> GetByIdAsync(int id); // Recupera um quarto pelo ID
        Task<Room> CreateAsync(Room room); // Cria um quarto
        Task UpdateAsync(Room room); // Atualiza um quarto
        Task DeleteAsync(Room room); // Deleta um quarto
    }
}
