using System.Threading;

namespace Proyecto.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }

        // Relación con Rol
        public int RolId { get; set; }
        public Rol Rol { get; set; }

        // Relación con Tareas
        public ICollection<Tarea> Tareas { get; set; }
    }

}
