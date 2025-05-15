using MediReserva.Models;
namespace MediReserva.Services.Interfaces


{
    public interface IPacienteService
    {
        Task<List<Paciente>> GetAllAsync();
        Task<Paciente?> GetByIdAsync(int id);
        Task AddAsync(Paciente paciente);
        Task UpdateAsync(Paciente paciente);
        Task DeleteAsync(int id);
    }
}
