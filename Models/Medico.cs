using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediReserva.Models
{
    public partial class Medico
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Debe ser un correo válido")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder 100 caracteres")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Debe ser un número de teléfono válido")]
        [StringLength(15, ErrorMessage = "El teléfono no puede exceder 15 caracteres")]
        public string? Telefono { get; set; }

        [StringLength(50, ErrorMessage = "El consultorio no puede exceder 50 caracteres")]
        public string? Consultorio { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una especialidad válida")]
        public int EspecialidadId { get; set; }

        public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

        public virtual Especialidad Especialidad { get; set; } = null!;
    }
}
