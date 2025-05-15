using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace MediReserva.Implementations
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadController : ControllerBase
    {
        private readonly IEspecialidadService _EspecialidadService;

        public EspecialidadController(IEspecialidadService especialidadService)
        {
            _EspecialidadService = especialidadService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<Especialidad>>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            var especialidad = await _EspecialidadService.GetAllAsync();
            return Ok(new ApiResponse<List<Especialidad>>(especialidad));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Especialidad>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(int id)
        {
            var especialidad = await _EspecialidadService.GetByIdAsync(id);
            if (especialidad == null)
                return NotFound(new ApiResponse<string>($"Especialidad con id {id} no encontrada.", false));

            return Ok(new ApiResponse<Especialidad>(especialidad));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Especialidad>), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Add(Especialidad especialidad)
        {
            await _EspecialidadService.AddAsync(especialidad);
            return CreatedAtAction(nameof(GetById), new { id = especialidad.Id }, new ApiResponse<Especialidad>(especialidad));
        }
    }
}
