using MediReserva.Models;

namespace MediReserva.Services.Interfaces
{
    public interface ICitaService
    {
        Task<List<Citum>> GetAllAsync();
        Task<Citum?> GetByIdAsync(int id);
        Task<List<Citum>> GetByFechaAsync(DateOnly fecha);
        Task<List<Citum>> GetByMedicoAsync(int medicoId);
        Task<List<Citum>> GetByPacienteAsync(int pacienteId);
        Task AddAsync(Citum cita);
        Task UpdateAsync(Citum cita);
        Task CancelAsync(int id);
    }
}
