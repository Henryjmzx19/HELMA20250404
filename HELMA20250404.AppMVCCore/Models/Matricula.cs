using System;
using System.Collections.Generic;

namespace HELMA20250404.AppMVCCore.Models;

public partial class Matricula
{
    public int IdMatricula { get; set; }

    public int? IdAlumno { get; set; }

    public int? IdProfesor { get; set; }

    public int YearLectivo { get; set; }

    public virtual Alumno? IdAlumnoNavigation { get; set; }

    public virtual Profesore? IdProfesorNavigation { get; set; }

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
