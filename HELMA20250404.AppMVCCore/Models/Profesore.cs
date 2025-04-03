using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HELMA20250404.AppMVCCore.Models;

public partial class Profesore
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }
    [Required(ErrorMessage = "El apellido es obligatorio.")]
    public string Apellido { get; set; } = null!;

    [Required(ErrorMessage = "El Teléfono es obligatorio.")]
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    public string? Dui { get; set; }

    [Required(ErrorMessage = "La Dirección es obligatorio.")]
    [Display(Name = "Dirección")]
    public string? Direccion { get; set; }

    [Display(Name = "Fecha de Nacimiento")]
    public DateOnly FechaNacimiento { get; set; }

    [Display(Name = "Nombre del Profesor")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
