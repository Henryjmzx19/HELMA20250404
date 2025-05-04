using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HELMA20250404.AppMVCCore.Models
{
    public partial class Nota
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un alumno.")]
        [ForeignKey("Matricula")]
        [Display(Name = "Nombre del alumno")]
        public int? IdMatricula { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una aula.")]
        [ForeignKey("Aula")]
        [Display(Name = "Grado")]
        public int? IdAula { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una materia.")]
        [ForeignKey("Materia")]
        [Display(Name = "Materia")]
        public int? IdMateria { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10.")]
        [Display(Name = " Primer Trimestre")]
        public decimal Trimestre1 { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10.")]
        [Display(Name = " Segundo Trimestre")]
        public decimal Trimestre2 { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10.")]
        [Display(Name = "Tercer Trimestre")]
        public decimal Trimestre3 { get; set; }

        // Propiedad calculada para el promedio
        [Display(Name = "Promedio")]
        public decimal Promedio { get; set; }


        // Propiedad calculada para el estado basado en el promedio
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        // Propiedades de navegación
        [Display(Name = "Grado")]
        public virtual Aula? Aula { get; set; }

        [Display(Name = "Materia")]
        public virtual Materia? Materia { get; set; }

        [Display(Name = "Nombre del alumno")]
        public virtual Matricula? Matricula { get; set; }
    }
}

