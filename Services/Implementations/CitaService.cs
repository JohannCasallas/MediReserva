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
            return await _context.Cita
                                 .Include(c => c.Paciente)
                                 .Include(c => c.Medico)
                                 .ToListAsync();
        }

        public async Task<Citum?> GetByIdAsync(int id)
        {
            return await _context.Cita
                                 .Include(c => c.Paciente)
                                 .Include(c => c.Medico)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Citum>> GetByFechaAsync(DateOnly fecha)
        {
            return await _context.Cita
                                 .Include(c => c.Paciente)
                                 .Include(c => c.Medico)
                                 .Where(c => c.FechaCita == fecha)
                                 .ToListAsync();
        }

        public async Task<List<Citum>> GetByMedicoAsync(int medicoId)
        {
            return await _context.Cita
                                 .Include(c => c.Paciente)
                                 .Include(c => c.Medico)
                                 .Where(c => c.MedicoId == medicoId)
                                 .ToListAsync();
        }

        public async Task<List<Citum>> GetByPacienteAsync(int pacienteId)
        {
            return await _context.Cita
                                 .Include(c => c.Paciente)
                                 .Include(c => c.Medico)
                                 .Where(c => c.PacienteId == pacienteId)
                                 .ToListAsync();
        }

        public async Task AddAsync(Citum cita)
        {
            await _context.Cita.AddAsync(cita);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Citum cita)
        {
            _context.Cita.Update(cita);
            await _context.SaveChangesAsync();
        }

        public async Task CancelAsync(int id)
        {
            var cita = await _context.Cita.FindAsync(id);
            if (cita != null)
            {
                cita.Estado = "cancelada";
                _context.Cita.Update(cita);
                await _context.SaveChangesAsync();
            }
        }
    }
}
