using System;
using System.Collections.Generic;

namespace MediReserva.Models;

public partial class Medico
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public string? Consultorio { get; set; }

    public int EspecialidadId { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual Especialidad Especialidad { get; set; } = null!;
}
