﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HELMA20250404.AppMVCCore.Models;

public partial class Materia
{
    public int Id { get; set; }
   
    [Required(ErrorMessage = "El nombre de la materia es obligatorio.")]
    public string Nombre { get; set; } = null!;

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
