namespace Proyecto.Models
{
    public class EstadoTarea
    {
        public int EstadoTareaId { get; set; }
        public string NombreEstado { get; set; }

        // Relación con Tareas
        public ICollection<Tarea> Tareas { get; set; }
    }

}
