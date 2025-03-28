using System;
using System.Collections.Generic;

namespace HELMA20250404.AppMVCCore.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public virtual Alumno? Alumno { get; set; }

    public virtual Profesore? Profesore { get; set; }
}
