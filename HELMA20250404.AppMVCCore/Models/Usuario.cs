using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HELMA20250404.AppMVCCore.Models;

public partial class Usuario
{
    public int Id { get; set; }

    [Display(Name = "Nombre de usuario")]
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    public string NombreUsuario { get; set; } = null!;

    [EmailAddress]
    [Required(ErrorMessage = "El email es obligatorio.")]
    public string Email { get; set; } = null!;

    [Display(Name = "Contraseña")]
    [Required(ErrorMessage = "La contraseña es obligatorio.")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "El password debe tener entre 5 y 12 caracteres.")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "El rol es obligatorio.")]
    public string Rol { get; set; } = null!;

    public virtual Alumno? Alumno { get; set; }

    public virtual Profesore? Profesore { get; set; }
}
