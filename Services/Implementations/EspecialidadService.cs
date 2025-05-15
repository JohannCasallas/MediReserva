using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        await _context.Especialidads.AddAsync(especialidad);
        await _context.SaveChangesAsync();
    }
}
