using MediReserva.Data;
using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediReserva.Services.Implementations
{
    public class MedicoService : IMedicoService
    {
        private readonly ApplicationDbContext _context;

        public MedicoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Medico>> GetAllAsync()
        {
            return await _context.Medicos.ToListAsync();
        }

        public async Task<Medico?> GetByIdAsync(int id)
        {
            return await _context.Medicos.FindAsync(id);
        }

        public async Task AddAsync(Medico medico)
        {
            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Medico medico)
        {
            var existingMedico = await _context.Medicos.FindAsync(medico.Id);
            if (existingMedico == null)
                throw new KeyNotFoundException($"Médico con id {medico.Id} no encontrado.");

            _context.Entry(existingMedico).CurrentValues.SetValues(medico);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
                throw new KeyNotFoundException($"Médico con id {id} no encontrado.");

            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();
        }
    }
}
