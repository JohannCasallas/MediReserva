using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediReserva.Implementations
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

        // ----- Consultas -----

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<Citum>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var citas = await _citaService.GetAllAsync();
            return Ok(new ApiResponse<List<Citum>>(citas));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Citum>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var cita = await _citaService.GetByIdAsync(id);
            if (cita == null)
                return NotFound(new ApiResponse<string>($"Cita con id {id} no encontrada.", false));

            return Ok(new ApiResponse<Citum>(cita));
        }

        [HttpGet("por-fecha/{fecha}")]
        [ProducesResponseType(typeof(ApiResponse<List<Citum>>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetByFecha(string fecha)
        {
            if (!DateOnly.TryParse(fecha, out var parsedFecha))
                return BadRequest(new ApiResponse<string>("Formato de fecha inválido. Usa AAAA-MM-DD.", false));

            var citas = await _citaService.GetByFechaAsync(parsedFecha);
            return Ok(new ApiResponse<List<Citum>>(citas));
        }

        [HttpGet("por-paciente/{pacienteId}")]
        [ProducesResponseType(typeof(ApiResponse<List<Citum>>), 200)]
        public async Task<IActionResult> GetByPaciente(int pacienteId)
        {
            var citas = await _citaService.GetByPacienteAsync(pacienteId);
            return Ok(new ApiResponse<List<Citum>>(citas));
        }

        [HttpGet("por-medico/{medicoId}")]
        [ProducesResponseType(typeof(ApiResponse<List<Citum>>), 200)]
        public async Task<IActionResult> GetByMedico(int medicoId)
        {
            var citas = await _citaService.GetByMedicoAsync(medicoId);
            return Ok(new ApiResponse<List<Citum>>(citas));
        }

        // ----- Comandos -----

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Citum>), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add(Citum cita)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Datos inválidos para crear la cita.", false));

            await _citaService.AddAsync(cita);
            return CreatedAtAction(nameof(GetById), new { id = cita.Id }, new ApiResponse<Citum>(cita));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, Citum cita)
        {
            if (id != cita.Id)
                return BadRequest(new ApiResponse<string>("El ID de la cita no coincide.", false));

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Datos inválidos para actualizar la cita.", false));

            try
            {
                await _citaService.UpdateAsync(cita);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(ex.Message, false));
            }
        }

        [HttpPut("cancelar/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                await _citaService.CancelAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(ex.Message, false));
            }
        }
    }
}
