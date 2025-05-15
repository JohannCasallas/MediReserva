using MediReserva.Data;
using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediReserva.Services.Implementations
{
    public class PacienteService : IPacienteService
    {
        private readonly ApplicationDbContext _context;

        public PacienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Paciente>> GetAllAsync()
        {
            return await _context.Pacientes.ToListAsync();
        }

        public async Task<Paciente?> GetByIdAsync(int id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task AddAsync(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Paciente paciente)
        {
            var existingPaciente = await _context.Pacientes.FindAsync(paciente.Id);
            if (existingPaciente == null)
                throw new KeyNotFoundException($"Paciente con id {paciente.Id} no encontrado.");

            _context.Entry(existingPaciente).CurrentValues.SetValues(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                throw new KeyNotFoundException($"Paciente con id {id} no encontrado.");

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
        }
    }
}
