using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
using System.Threading;

namespace Proyecto.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Proyectos> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<EstadoTarea> EstadosTareas { get; set; }

        public DbSet<Fase> Fases { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
        }
    }
}
