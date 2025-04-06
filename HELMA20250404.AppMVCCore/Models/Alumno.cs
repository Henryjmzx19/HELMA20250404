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

    [Required(ErrorMessage = "El NIE es obligatorio")]
    public string Nie { get; set; } = null!;

    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [Display(Name = "Dirección")]
    public string? Direccion { get; set; }

    [Required(ErrorMessage = "El encargado es obligatorio.")]
    public string? Encargado { get; set; }

    [Display(Name = "Imagen")]
    public byte[]? ImagenBytes { get; set; }

    [Display(Name = "Fecha de nacimiento")]
    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    public DateOnly YearNacimiento { get; set; }

    [Display(Name = "Nombre de alumno")]
    public virtual Usuario? Usuario { get; set; }

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
