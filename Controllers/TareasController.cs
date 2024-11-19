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
    public class TareasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TareasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tareas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tareas
                .Include(t => t.EstadoTarea)
                .Include(t => t.Proyecto)
                .Include(t => t.Usuario)
                .Include(t => t.Fase); // Asegúrate de incluir Fase si está relacionado
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tareas/Details
        public async Task<IActionResult> Details(int? proyectoId)
        {
            if (proyectoId == null)
            {
                return NotFound();
            }

            var tareas = await _context.Tareas
                .Include(t => t.EstadoTarea)
                .Include(t => t.Proyecto)
                .Include(t => t.Usuario)
                .Include(t => t.Fase) // Asegúrate de incluir Fase si está relacionado
                .Where(t => t.ProyectoId == proyectoId)
                .ToListAsync();

            if (tareas == null || !tareas.Any())
            {
                return NotFound();
            }

            ViewBag.ProyectoId = proyectoId;
            return View(tareas);
        }

        // GET: Tareas/Create
        public IActionResult Create()
        {
            ViewData["EstadoTareaId"] = new SelectList(_context.EstadosTareas, "EstadoTareaId", "NombreEstado");
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "ProyectoId", "NombreProyecto");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre");
            ViewData["FaseId"] = new SelectList(_context.Fases, "FaseId", "NombreFase"); // Asegúrate de tener un modelo Fase
            return View();
        }

        // POST: Tareas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TareaId,NombreTarea,Descripcion,FechaCreacion,FechaLimite,EstadoTareaId,ProyectoId,UsuarioId,FaseId")] Tarea tarea)
        {
            if (true)
            {
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoTareaId"] = new SelectList(_context.EstadosTareas, "EstadoTareaId", "EstadoTareaId", tarea.EstadoTareaId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "ProyectoId", "ProyectoId", tarea.ProyectoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", tarea.UsuarioId);
            ViewData["FaseId"] = new SelectList(_context.Fases, "FaseId", "NombreFase", tarea.FaseId); // Asegúrate de pasar el FaseId
            return View(tarea);
        }
        // POST: Tareas/ActualizarEstado/5
[HttpPost]
public async Task<IActionResult> ActualizarEstado(int id, [FromBody] NuevoEstadoViewModel modelo)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var tarea = await _context.Tareas.FindAsync(id);
    if (tarea == null)
    {
        return NotFound();
    }

    // Encontrar el estado correspondiente al nuevo estado
    var nuevoEstado = await _context.EstadosTareas.FirstOrDefaultAsync(e => e.NombreEstado == modelo.NuevoEstado);
    if (nuevoEstado == null)
    {
        return BadRequest("Estado no válido.");
    }

    // Actualizar el estado de la tarea
    tarea.EstadoTareaId = nuevoEstado.EstadoTareaId;
    
    try
    {
        _context.Update(tarea);
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!TareaExists(tarea.TareaId))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    return Ok(new { mensaje = "Estado actualizado correctamente." });
}


        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            ViewData["EstadoTareaId"] = new SelectList(_context.EstadosTareas, "EstadoTareaId", "NombreEstado", tarea.EstadoTareaId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "ProyectoId", "NombreProyecto", tarea.ProyectoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre", tarea.UsuarioId);
            ViewData["FaseId"] = new SelectList(_context.Fases, "FaseId", "NombreFase", tarea.FaseId); // Asegúrate de pasar el FaseId

            return View(tarea);
        }

        // POST: Tareas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TareaId,NombreTarea,Descripcion,FechaCreacion,FechaLimite,EstadoTareaId,ProyectoId,UsuarioId,FaseId")] Tarea tarea)
        {
            if (id != tarea.TareaId)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.TareaId))
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
            ViewData["EstadoTareaId"] = new SelectList(_context.EstadosTareas, "EstadoTareaId", "EstadoTareaId", tarea.EstadoTareaId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "ProyectoId", "ProyectoId", tarea.ProyectoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", tarea.UsuarioId);
            ViewData["FaseId"] = new SelectList(_context.Fases, "FaseId", "NombreFase", tarea.FaseId); // Asegúrate de pasar el FaseId
            return View(tarea);
        }

        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.EstadoTarea)
                .Include(t => t.Proyecto)
                .Include(t => t.Usuario)
                .Include(t => t.Fase) // Asegúrate de incluir Fase si está relacionado
                .FirstOrDefaultAsync(m => m.TareaId == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.TareaId == id);
        }
    }
}
