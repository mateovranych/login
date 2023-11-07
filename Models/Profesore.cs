using System;
using System.Collections.Generic;

namespace TrabajoFinalProgramacion.Models;

public partial class Profesore
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Sexo { get; set; }

}
