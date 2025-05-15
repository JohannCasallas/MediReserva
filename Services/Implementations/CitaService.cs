using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediReserva.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitaController : ControllerBase
    {
        private readonly ICitaService _citaService;

        public CitaController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var citas = await _citaService.GetAllAsync();
            // Retornar datos con estructura estándar
            return Ok(new ApiResponse<List<Citum>>(citas));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cita = await _citaService.GetByIdAsync(id);
            if (cita == null)
                return NotFound(new ApiResponse<string>($"Cita con id {id} no encontrada.", false));

            return Ok(new ApiResponse<Citum>(cita));
        }

        [HttpGet("por-fecha/{fecha}")]
        public async Task<IActionResult> GetByFecha(string fecha)
        {
            if (!DateOnly.TryParse(fecha, out var parsedFecha))
                return BadRequest(new ApiResponse<string>("Formato de fecha inválido. Usa AAAA-MM-DD.", false));

            var citas = await _citaService.GetByFechaAsync(parsedFecha);
            return Ok(new ApiResponse<List<Citum>>(citas));
        }

        [HttpGet("por-paciente/{pacienteId}")]
        public async Task<IActionResult> GetByPaciente(int pacienteId)
        {
            var citas = await _citaService.GetByPacienteAsync(pacienteId);
            return Ok(new ApiResponse<List<Citum>>(citas));
        }

        [HttpGet("por-medico/{medicoId}")]
        public async Task<IActionResult> GetByMedico(int medicoId)
        {
            var citas = await _citaService.GetByMedicoAsync(medicoId);
            return Ok(new ApiResponse<List<Citum>>(citas));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Citum cita)
        {
            await _citaService.AddAsync(cita);
            return CreatedAtAction(nameof(GetById), new { id = cita.Id }, new ApiResponse<Citum>(cita));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Citum cita)
        {
            if (id != cita.Id)
                return BadRequest(new ApiResponse<string>("El id de la cita no coincide.", false));

            await _citaService.UpdateAsync(cita);
            return NoContent();
        }

        [HttpPut("cancelar/{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            await _citaService.CancelAsync(id);
            return NoContent();
        }
    }
}
