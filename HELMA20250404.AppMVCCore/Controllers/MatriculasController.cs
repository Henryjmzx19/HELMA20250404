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
    public class MatriculasController : Controller
    {
        private readonly SistemaCalificacionesContext _context;

        public MatriculasController(SistemaCalificacionesContext context)
        {
            _context = context;
        }

        // GET: Matriculas
        public async Task<IActionResult> Index()
        {
            var sistemaCalificacionesContext = _context.Matriculas
                .Include(m => m.IdAlumnoNavigation)
                .Include(m => m.IdProfesorNavigation);

            return View(await sistemaCalificacionesContext.ToListAsync());
        }

        // GET: Matriculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var matricula = await _context.Matriculas
                .Include(m => m.IdAlumnoNavigation)
                .Include(m => m.IdProfesorNavigation)
                .FirstOrDefaultAsync(m => m.IdMatricula == id);

            if (matricula == null) return NotFound();

            return View(matricula);
        }

        // GET: Matriculas/Create
        public IActionResult Create()
        {
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "Id", "Nombre");
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "Id", "Nombre");

            return View();
        }

        // POST: Matriculas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMatricula,IdAlumno,IdProfesor,YearLectivo")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matricula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "Id", "Nombre", matricula.IdAlumno);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "Id", "Nombre", matricula.IdProfesor);

            return View(matricula);
        }

        // GET: Matriculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var matricula = await _context.Matriculas
                .Include(m => m.IdAlumnoNavigation)
                .Include(m => m.IdProfesorNavigation)
                .FirstOrDefaultAsync(m => m.IdMatricula == id);

            if (matricula == null) return NotFound();

            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "Id", "Nombre", matricula.IdAlumno);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "Id", "Nombre", matricula.IdProfesor);

            return View(matricula);
        }

        // POST: Matriculas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMatricula,IdAlumno,IdProfesor,YearLectivo")] Matricula matricula)
        {
            if (id != matricula.IdMatricula) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatriculaExists(matricula.IdMatricula)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "Id", "Nombre", matricula.IdAlumno);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "Id", "Nombre", matricula.IdProfesor);

            return View(matricula);
        }

        // GET: Matriculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var matricula = await _context.Matriculas
                .Include(m => m.IdAlumnoNavigation)
                .Include(m => m.IdProfesorNavigation)
                .FirstOrDefaultAsync(m => m.IdMatricula == id);

            if (matricula == null) return NotFound();

            return View(matricula);
        }

        // POST: Matriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula != null) _context.Matriculas.Remove(matricula);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatriculaExists(int id)
        {
            return _context.Matriculas.Any(e => e.IdMatricula == id);
        }
    }
}
