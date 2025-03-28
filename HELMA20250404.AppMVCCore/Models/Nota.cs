using System;
using System.Collections.Generic;

namespace HELMA20250404.AppMVCCore.Models;

public partial class Nota
{
    public int Id { get; set; }

    public int? IdMatricula { get; set; }

    public int? IdAula { get; set; }

    public int? IdMateria { get; set; }

    public decimal Trimestre1 { get; set; }

    public decimal Trimestre2 { get; set; }

    public decimal Trimestre3 { get; set; }

    public decimal? Promedio { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Aula? IdAulaNavigation { get; set; }

    public virtual Materia? IdMateriaNavigation { get; set; }

    public virtual Matricula? IdMatriculaNavigation { get; set; }
}
