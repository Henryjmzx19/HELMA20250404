using System;
using System.Collections.Generic;

namespace HELMA20250404.AppMVCCore.Models;

public partial class Materia
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
