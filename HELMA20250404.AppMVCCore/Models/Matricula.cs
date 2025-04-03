using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HELMA20250404.AppMVCCore.Models;

public partial class Matricula
{
    [Display(Name = "ID de Matrícula")]
    public int IdMatricula { get; set; }

    [Required]
    [Display(Name = "Nombre del Alumno")]
    public int? IdAlumno { get; set; }

    [Required]
    [Display(Name = "Nombre del Profesor")]
    public int? IdProfesor { get; set; }

    [Required]
    [Range(1000, 9999, ErrorMessage = "El año lectivo debe tener 4 dígitos.")]
    [Display(Name = "Año Lectivo")]
    public int YearLectivo { get; set; }
    [Display(Name = "Nombre del Alumno")]
    public virtual Alumno? IdAlumnoNavigation { get; set; }
    [Display(Name = "Nombre del profesor")]
    public virtual Profesore? IdProfesorNavigation { get; set; }

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
