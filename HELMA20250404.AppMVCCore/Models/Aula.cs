using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HELMA20250404.AppMVCCore.Models;

public partial class Aula
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del aula es obligatorio.")]
    public string Nombre { get; set; } = null!;

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
