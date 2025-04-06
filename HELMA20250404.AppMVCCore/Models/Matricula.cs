using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HELMA20250404.AppMVCCore.Models;

public partial class Matricula
{
    [Display(Name = "ID de Matrícula")]
    public int IdMatricula { get; set; }

    [Required]
    [Display(Name = "Nombre de alumno")]
    public int? IdAlumno { get; set; }

    [Required]
    [Display(Name = "Nombre de profesor")]
    public int? IdProfesor { get; set; }

    [Required]
    [Range(1000, 9999, ErrorMessage = "El año lectivo debe tener 4 dígitos.")]
    [Display(Name = "Año Lectivo")]
    public int YearLectivo { get; set; }
    [Display(Name = "Nombre de alumno")]
    public virtual Alumno? Alumno { get; set; }
    [Display(Name = "Nombre de profesor")]
    public virtual Profesore? Profesor { get; set; }

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
