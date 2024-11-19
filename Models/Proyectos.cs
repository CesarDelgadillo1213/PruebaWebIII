using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class Proyectos
    {
        [Key]
        public int ProyectoId { get; set; }

        [Required]
        public string NombreProyecto { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Fin")]
        public DateTime FechaFin { get; set; }

        // Relación con Tareas
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
