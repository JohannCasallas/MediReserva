using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediReserva.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<Medico>>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            var medicos = await _medicoService.GetAllAsync();
            return Ok(new ApiResponse<List<Medico>>(medicos));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Medico>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(int id)
        {
            var medico = await _medicoService.GetByIdAsync(id);
            if (medico == null)
                return NotFound(new ApiResponse<string>($"Médico con id {id} no encontrado.", false));

            return Ok(new ApiResponse<Medico>(medico));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Medico>), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Add(Medico medico)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Datos inválidos en la solicitud.", false));

            await _medicoService.AddAsync(medico);
            return CreatedAtAction(nameof(GetById), new { id = medico.Id }, new ApiResponse<Medico>(medico));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(int id, Medico medico)
        {
            if (id != medico.Id)
                return BadRequest(new ApiResponse<string>("El ID del médico no coincide con el cuerpo de la solicitud.", false));

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Datos inválidos en la solicitud.", false));

            await _medicoService.UpdateAsync(medico);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _medicoService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(ex.Message, false));
            }
        }
    }
}
