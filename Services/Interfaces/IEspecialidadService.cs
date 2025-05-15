using MediReserva.Models;

namespace MediReserva.Services.Interfaces
{
    public interface IEspecialidadService
    {
        Task<List<Especialidad>> GetAllAsync();
        Task<Especialidad?> GetByIdAsync(int id);
        Task AddAsync(Especialidad especialidad);
    }
}
