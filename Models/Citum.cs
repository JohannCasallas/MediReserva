using System;
using System.Collections.Generic;

namespace MediReserva.Models;

public partial class Citum
{
    public int Id { get; set; }

    public int PacienteId { get; set; }

    public int MedicoId { get; set; }

    public DateOnly FechaCita { get; set; }

    public TimeOnly Hora { get; set; }

    public string? Motivo { get; set; }

    public string? Estado { get; set; }

    public virtual Medico Medico { get; set; } = null!;

    public virtual Paciente Paciente { get; set; } = null!;
}
