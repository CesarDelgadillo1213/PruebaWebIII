namespace Proyecto.Models
{
    public class Rol
    {
        public int RolId { get; set; }
        public string NombreRol { get; set; }

        // Relación con Usuarios
        public ICollection<Usuario> Usuarios { get; set; }
    }

}
