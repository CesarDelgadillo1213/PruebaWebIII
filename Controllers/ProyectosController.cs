using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;
using Proyecto.Filters;


namespace Proyecto.Controllers
{
    public class ProyectosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProyectosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proyectos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Proyectos.ToListAsync());
        }

        // GET: Proyectos/Details/5
        public async Task<IActionResult> Details(int? proyectoId)
        {
            if (proyectoId == null)
            {
                return NotFound();
            }

            // Filtrar el proyecto que corresponde al proyectoId, incluyendo sus tareas
            var proyecto = await _context.Proyectos
                .Include(p => p.Tareas) // Incluir las tareas relacionadas
                .FirstOrDefaultAsync(p => p.ProyectoId == proyectoId);

            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto); // Pasar el proyecto completo a la vista
        }

        // GET: Proyectos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proyectos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProyectoId,NombreProyecto,Descripcion,FechaInicio,FechaFin")] Proyectos proyectos)
        {
            // Validar que las fechas no sean anteriores a la fecha actual
            if (proyectos.FechaInicio < DateTime.Now.Date)
            {
                ModelState.AddModelError("FechaInicio", "La fecha de inicio no puede ser anterior a la fecha actual.");
            }

            if (proyectos.FechaFin < DateTime.Now.Date)
            {
                ModelState.AddModelError("FechaFin", "La fecha de fin no puede ser anterior a la fecha actual.");
            }

            if (true) // Asegúrate de que ModelState sea válido
            {
                _context.Add(proyectos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proyectos);
        }

        // GET: Proyectos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyectos = await _context.Proyectos.FindAsync(id);
            if (proyectos == null)
            {
                return NotFound();
            }
            return View(proyectos);
        }

        // POST: Proyectos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProyectoId,NombreProyecto,Descripcion,FechaInicio,FechaFin")] Proyectos proyectos)
        {
            if (id != proyectos.ProyectoId)
            {
                return NotFound();
            }

            // Validar que las fechas no sean anteriores a la fecha actual
            if (proyectos.FechaInicio < DateTime.Now.Date)
            {
                ModelState.AddModelError("FechaInicio", "La fecha de inicio no puede ser anterior a la fecha actual.");
            }

            if (proyectos.FechaFin < DateTime.Now.Date)
            {
                ModelState.AddModelError("FechaFin", "La fecha de fin no puede ser anterior a la fecha actual.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyectos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectosExists(proyectos.ProyectoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proyectos);
        }

        // GET: Proyectos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyectos = await _context.Proyectos
                .FirstOrDefaultAsync(m => m.ProyectoId == id);
            if (proyectos == null)
            {
                return NotFound();
            }

            return View(proyectos);
        }

        // POST: Proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proyectos = await _context.Proyectos.FindAsync(id);
            if (proyectos != null)
            {
                _context.Proyectos.Remove(proyectos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectosExists(int id)
        {
            return _context.Proyectos.Any(e => e.ProyectoId == id);
        }

        // NUEVA SECCIÓN: Funcionalidades para manejar Tareas

        // GET: Proyectos/Tareas/5 - Ver las tareas de un proyecto
        public async Task<IActionResult> Tareas(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.Tareas) // Incluir las tareas relacionadas
                .FirstOrDefaultAsync(m => m.ProyectoId == id);

            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto.Tareas); // Pasar las tareas a la vista
        }

        // GET: Proyectos/CreateTarea/5 - Formulario para crear nueva tarea
        public IActionResult CreateTarea(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.ProyectoId = id; // Enviar el id del proyecto a la vista
            return View();
        }

        // POST: Proyectos/CreateTarea - Crear nueva tarea
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTarea([Bind("TareaId,NombreTarea,Descripcion,FechaLimite,ProyectoId")] Tarea tarea)
        {
            // Validar que la FechaLimite no sea anterior a la fecha actual
            if (tarea.FechaLimite < DateTime.Now.Date)
            {
                ModelState.AddModelError("FechaLimite", "La fecha límite no puede ser anterior al día actual.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Tareas), new { id = tarea.ProyectoId });
            }
            ViewBag.ProyectoId = tarea.ProyectoId;
            return View(tarea);
        }
    }
}
