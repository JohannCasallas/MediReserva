using MediReserva.Data;
using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediReserva.Services.Implementations
{
    public class CitaService : ICitaService
    {
        private readonly ApplicationDbContext _context;

        public CitaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Citum>> GetAllAsync()
        {
            return await _context.Cita.ToListAsync();
        }

        public async Task<Citum?> GetByIdAsync(int id)
        {
            return await _context.Cita.FindAsync(id);
        }

        public async Task<List<Citum>> GetByFechaAsync(DateOnly fecha)
        {
            return await _context.Cita
                .Where(c => c.FechaCita == fecha)
                .ToListAsync();
        }

        public async Task<List<Citum>> GetByPacienteAsync(int pacienteId)
        {
            return await _context.Cita
                .Where(c => c.PacienteId == pacienteId)
                .ToListAsync();
        }

        public async Task<List<Citum>> GetByMedicoAsync(int medicoId)
        {
            return await _context.Cita
                .Where(c => c.MedicoId == medicoId)
                .ToListAsync();
        }

        public async Task AddAsync(Citum cita)
        {
            _context.Cita.Add(cita);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Citum cita)
        {
            if (!await _context.Cita.AnyAsync(c => c.Id == cita.Id))
                throw new KeyNotFoundException("Cita no encontrada.");

            _context.Entry(cita).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task CancelAsync(int id)
        {
            var cita = await _context.Cita.FindAsync(id);
            if (cita == null)
                throw new KeyNotFoundException("Cita no encontrada.");

            cita.Estado = "Cancelada"; // Asegúrate de tener esta propiedad
            await _context.SaveChangesAsync();
        }
    }
}
