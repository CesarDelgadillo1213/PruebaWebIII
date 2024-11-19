using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class Tarea
    {
        [Key]
        public int TareaId { get; set; }
        public string NombreTarea { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaLimite { get; set; }

        public int EstadoTareaId { get; set; }
        public EstadoTarea EstadoTarea { get; set; }

        public int ProyectoId { get; set; }
        public Proyectos Proyecto { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int? FaseId { get; set; } // Puede ser nullable si no todas las tareas tienen fase
        public virtual Fase Fase { get; set; }

    }

}
