namespace Proyecto.Models
{
    public class Fase
    {
        public int FaseId { get; set; }
        public string NombreFase { get; set; }

        // Relación con Tarea
        public virtual ICollection<Tarea> Tareas { get; set; }
    }

}
