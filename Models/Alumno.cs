using System;
using System.Collections.Generic;

namespace TrabajoFinalProgramacion.Models;

public partial class Alumno
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int? Dni { get; set; }

    public string? Calle { get; set; }

 
}
