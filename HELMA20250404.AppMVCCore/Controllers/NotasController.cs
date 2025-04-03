using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HELMA20250404.AppMVCCore.Models;

namespace HELMA20250404.AppMVCCore.Controllers
{
    public class NotasController : Controller
    {
        private readonly SistemaCalificacionesContext _context;

        public NotasController(SistemaCalificacionesContext context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var sistemaCalificacionesContext = _context.Notas.Include(n => n.IdAulaNavigation).Include(n => n.IdMateriaNavigation).Include(n => n.IdMatriculaNavigation);
            return View(await sistemaCalificacionesContext.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.IdAulaNavigation)
                .Include(n => n.IdMateriaNavigation)
                .Include(n => n.IdMatriculaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // GET: Notas/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                // Incluyendo las relaciones necesarias para matriculas
                var matriculas = await _context.Matriculas
                    .Include(m => m.IdAlumnoNavigation)        // Cargando el Alumno relacionado
                    .Include(m => m.IdProfesorNavigation)      // Cargando el Profesor relacionado
                    .ToListAsync();

                // Cargar los datos de alumnos y profesores
                ViewBag.IdAlumno = new SelectList(
                    _context.Alumnos.Include(a => a.IdUsuarioNavigation),
                    "Id",
                    "IdUsuarioNavigation.NombreUsuario"
                );

                ViewBag.IdProfesor = new SelectList(
                    _context.Profesores.Include(p => p.IdUsuarioNavigation),
                    "Id",
                    "IdUsuarioNavigation.NombreUsuario"
                );

                // Configurar el resto de datos en ViewData
                ViewBag.IdMatricula = new SelectList(matriculas, "IdMatricula", "IdMatricula");
                ViewData["IdAula"] = new SelectList(_context.Aulas, "Id", "Nombre");
                ViewData["IdMateria"] = new SelectList(_context.Materias, "Id", "Nombre");

                return View();  // Asegúrate de devolver la vista después de configurar todo
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine(ex.Message);
                ViewBag.IdMatricula = new SelectList(Enumerable.Empty<object>());
                return View();  // Regresar la vista incluso si hubo un error
            }
        }


        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }
            ViewData["IdAula"] = new SelectList(_context.Aulas, "Id", "Nombre", nota.IdAula);
            ViewData["IdMateria"] = new SelectList(_context.Materias, "Id", "Nombre", nota.IdMateria);
            ViewData["IdMatricula"] = new SelectList(_context.Matriculas, "IdMatricula", "IdMatricula", nota.IdMatricula);
            return View(nota);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdMatricula,IdAula,IdMateria,Trimestre1,Trimestre2,Trimestre3,Promedio,Estado")] Nota nota)
        {
            if (id != nota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaExists(nota.Id))
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
            ViewData["IdAula"] = new SelectList(_context.Aulas, "Id", "Nombre", nota.IdAula);
            ViewData["IdMateria"] = new SelectList(_context.Materias, "Id", "Nombre", nota.IdMateria);
            ViewData["IdMatricula"] = new SelectList(_context.Matriculas, "IdMatricula", "IdMatricula", nota.IdMatricula);
            return View(nota);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.IdAulaNavigation)
                .Include(n => n.IdMateriaNavigation)
                .Include(n => n.IdMatriculaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                _context.Notas.Remove(nota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaExists(int id)
        {
            return _context.Notas.Any(e => e.Id == id);
        }
    }
}
