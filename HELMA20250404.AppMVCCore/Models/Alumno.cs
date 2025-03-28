using System;
using System.Collections.Generic;

namespace HELMA20250404.AppMVCCore.Models;

public partial class Alumno
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public string Apellido { get; set; } = null!;

    public string Nie { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Encargado { get; set; }

    public byte[]? Imagen { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
