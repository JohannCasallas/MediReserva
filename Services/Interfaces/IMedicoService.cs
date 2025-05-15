using MediReserva.Models;

namespace MediReserva.Services.Interfaces
{
    public interface IMedicoService
    {
        Task<List<Medico>> GetAllAsync();
        Task<Medico?> GetByIdAsync(int id);
        Task AddAsync(Medico medico);
        Task UpdateAsync(Medico medico);
        Task DeleteAsync(int id);
    }
}
