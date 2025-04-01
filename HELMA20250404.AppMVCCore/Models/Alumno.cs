using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HELMA20250404.AppMVCCore.Models;

public partial class Alumno
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [Display(Name = "Nombre")]
    public int? IdUsuario { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    public string Apellido { get; set; } = null!;

    [Required(ErrorMessage = "El NIE")]
    public string Nie { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    [Required(ErrorMessage = "El  es obligatorio.")]
    public string? Encargado { get; set; }

    public byte[]? Imagen { get; set; }

    [Display(Name = "Fecha de nacimiento")]
    public DateOnly YearNacimiento { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
