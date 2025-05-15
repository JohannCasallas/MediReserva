using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediReserva.Implementations
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<Paciente>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var pacientes = await _pacienteService.GetAllAsync();
            return Ok(new ApiResponse<List<Paciente>>(pacientes));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Paciente>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var paciente = await _pacienteService.GetByIdAsync(id);
            if (paciente == null)
                return NotFound(new ApiResponse<string>($"El paciente con ID {id} no fue encontrado.", false));

            return Ok(new ApiResponse<Paciente>(paciente));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Paciente>), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add(Paciente paciente)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Datos inválidos para crear el paciente.", false));

            await _pacienteService.AddAsync(paciente);
            return CreatedAtAction(nameof(GetById), new { id = paciente.Id }, new ApiResponse<Paciente>(paciente));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, Paciente paciente)
        {
            if (id != paciente.Id)
                return BadRequest(new ApiResponse<string>("El ID del paciente no coincide con el de la URL.", false));

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Datos inválidos para actualizar el paciente.", false));

            try
            {
                await _pacienteService.UpdateAsync(paciente);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(ex.Message, false));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _pacienteService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(ex.Message, false));
            }
        }
    }
}
