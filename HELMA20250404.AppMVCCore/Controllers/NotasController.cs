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
        // GET: Notas/Create
        public IActionResult Create()
        {
            // Obtener alumnos con el nombre del usuario
            ViewData["IdMatricula"] = new SelectList(
                _context.Matriculas
                    .Include(m => m.IdAlumnoNavigation)
                    .ThenInclude(a => a.IdUsuarioNavigation),
                "IdMatricula",
                "IdAlumnoNavigation.IdUsuarioNavigation.NombreUsuario"
            );

            // Obtener aulas y materias
            ViewData["IdAula"] = new SelectList(_context.Aulas, "Id", "Nombre");
            ViewData["IdMateria"] = new SelectList(_context.Materias, "Id", "Nombre");

            return View();
        }

        // POST: Notas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMatricula,IdAula,IdMateria,Trimestre1,Trimestre2,Trimestre3")] Nota nota)
        {
            // Validar si los trimestres tienen valor
            if (nota.Trimestre1 == 0 || nota.Trimestre2 == 0 || nota.Trimestre3 == 0)
            {
                ModelState.AddModelError("", "Todos los trimestres deben tener un valor mayor que 0.");
            }

            // Si el modelo no es válido, recargar las listas para los campos del formulario
          

            try
            {
                // Asegurarse de que los trimestres sean válidos antes de realizar el cálculo
                decimal trimestre1 = nota.Trimestre1;
                decimal trimestre2 = nota.Trimestre2;
                decimal trimestre3 = nota.Trimestre3;

                // Cálculo del promedio de los trimestres
                var promedio = (trimestre1 + trimestre2 + trimestre3) / 3;
                nota.Promedio = promedio;

                // Calcular el estado basado en el promedio
                nota.Estado = (nota.Promedio >= 6) ? "Aprobado" : "Reprobado";

                // Guardar la nota en la base de datos
                _context.Add(nota);
                await _context.SaveChangesAsync();

                // Redirigir a la acción Index después de guardar la nueva nota
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Si ocurre un error durante el guardado, mostrar el error
                ModelState.AddModelError("", "Hubo un error al guardar los datos: " + ex.Message);

                // Recargar las listas para los campos del formulario
                ViewData["IdMatricula"] = new SelectList(_context.Matriculas.Include(m => m.IdAlumnoNavigation).ThenInclude(a => a.IdUsuarioNavigation),
                    "IdMatricula", "IdAlumnoNavigation.IdUsuarioNavigation.NombreUsuario", nota.IdMatricula);
                ViewData["IdAula"] = new SelectList(_context.Aulas, "Id", "Nombre", nota.IdAula);
                ViewData["IdMateria"] = new SelectList(_context.Materias, "Id", "Nombre", nota.IdMateria);

                return View(nota);
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
