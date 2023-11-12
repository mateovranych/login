using System;
using System.Collections.Generic;

namespace PrograTF3.Models;

public partial class Alumno
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int? Dni { get; set; }

}
