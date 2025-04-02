using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HELMA20250404.AppMVCCore.Models;

public partial class Matricula
{
    
    public int IdMatricula { get; set; }
    
    public int? IdAlumno { get; set; }
    
    public int? IdProfesor { get; set; }
    [Display(Name = "Año Lectivo")]
    public int YearLectivo { get; set; }
    [Display(Name = "IdAlumno")]
    public virtual Alumno? IdAlumnoNavigation { get; set; }
    [Display(Name = "IdProfesor")]
    public virtual Profesore? IdProfesorNavigation { get; set; }

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
