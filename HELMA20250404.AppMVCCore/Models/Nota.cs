using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HELMA20250404.AppMVCCore.Models
{
    public partial class Nota
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Matricula")]
        [Display(Name = "Nombre del alumno")]
        public int? IdMatricula { get; set; }

        [ForeignKey("Aula")]
        [Display(Name = "Grado")]
        public int? IdAula { get; set; }

        [ForeignKey("Materia")]
        [Display(Name = "Materia")]
        public int? IdMateria { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10.")]
        [Display(Name = "Trimestre 1")]
        public decimal Trimestre1 { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10.")]
        [Display(Name = "Trimestre 2")]
        public decimal Trimestre2 { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10.")]
        [Display(Name = "Trimestre 3")]
        public decimal Trimestre3 { get; set; }

        // Propiedad calculada para el promedio
        [Display(Name = "Promedio")]
        public decimal Promedio { get; set; }


        // Propiedad calculada para el estado basado en el promedio
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        // Propiedades de navegación
        public virtual Aula? IdAulaNavigation { get; set; }
        public virtual Materia? IdMateriaNavigation { get; set; }
        public virtual Matricula? IdMatriculaNavigation { get; set; }
    }
}

