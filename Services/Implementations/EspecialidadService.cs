using MediReserva.Data;
using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediReserva.Services.Implementations
{
    public class EspecialidadService : IEspecialidadService
    {
        private readonly ApplicationDbContext _context;

        public EspecialidadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Especialidad>> GetAllAsync()
        {
            return await _context.Especialidads.ToListAsync();
        }

        public async Task<Especialidad?> GetByIdAsync(int id)
        {
            return await _context.Especialidads.FindAsync(id);
        }

        public async Task AddAsync(Especialidad especialidad)
        {
            _context.Especialidads.Add(especialidad);
            await _context.SaveChangesAsync();
        }
    }
}
