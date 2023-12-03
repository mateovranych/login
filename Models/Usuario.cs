using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrograTF3.Models;

public partial class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }
}
