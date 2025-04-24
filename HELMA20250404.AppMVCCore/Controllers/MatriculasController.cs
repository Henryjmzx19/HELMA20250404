using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HELMA20250404.AppMVCCore.Models;
using Microsoft.AspNetCore.Authorization;

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
                .Include(m => m.Alumno)
                    .ThenInclude(a => a.Usuario)
                .Include(m => m.Profesor)
                    .ThenInclude(p => p.Usuario);

            return View(await sistemaCalificacionesContext.ToListAsync());
        }

        // GET: Matriculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var matricula = await _context.Matriculas
                .Include(m => m.Alumno)
                    .ThenInclude(a => a.Usuario)
                .Include(m => m.Profesor)
                    .ThenInclude(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.IdMatricula == id);

            if (matricula == null) return NotFound();

            return View(matricula);
        }

        // GET: Matriculas/Create
        public IActionResult Create()
        {
            var alumnos = _context.Alumnos
                .Include(a => a.Usuario)
                .Where(a => a.Usuario != null)
                .ToList();

            var profesores = _context.Profesores
                .Include(p => p.Usuario)
                .Where(p => p.Usuario != null)
                .ToList();

            if (!alumnos.Any() || !profesores.Any())
            {
                return View("Error"); // Puedes crear una vista Error personalizada si quieres
            }

            ViewBag.IdAlumno = new SelectList(alumnos, "Id", "Usuario.NombreUsuario");
            ViewBag.IdProfesor = new SelectList(profesores, "Id", "Usuario.NombreUsuario");

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

            var alumnos = _context.Alumnos
                .Include(a => a.Usuario)
                .Where(a => a.Usuario != null)
                .ToList();

            var profesores = _context.Profesores
                .Include(p => p.Usuario)
                .Where(p => p.Usuario != null)
                .ToList();

            ViewBag.IdAlumno = new SelectList(alumnos, "Id", "Usuario.NombreUsuario", matricula.IdAlumno);
            ViewBag.IdProfesor = new SelectList(profesores, "Id", "Usuario.NombreUsuario", matricula.IdProfesor);

            return View(matricula);
        }

        // GET: Matriculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var matricula = await _context.Matriculas
                .Include(m => m.Alumno)
                    .ThenInclude(a => a.Usuario)
                .Include(m => m.Profesor)
                    .ThenInclude(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.IdMatricula == id);

            if (matricula == null) return NotFound();

            var alumnos = _context.Alumnos
                .Include(a => a.Usuario)
                .Where(a => a.Usuario != null)
                .ToList();

            var profesores = _context.Profesores
                .Include(p => p.Usuario)
                .Where(p => p.Usuario != null)
                .ToList();

            ViewBag.IdAlumno = new SelectList(alumnos, "Id", "Usuario.NombreUsuario", matricula.IdAlumno);
            ViewBag.IdProfesor = new SelectList(profesores, "Id", "Usuario.NombreUsuario", matricula.IdProfesor);

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
                    if (!_context.Matriculas.Any(e => e.IdMatricula == matricula.IdMatricula))
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

            var alumnos = _context.Alumnos
                .Include(a => a.Usuario)
                .Where(a => a.Usuario != null)
                .ToList();

            var profesores = _context.Profesores
                .Include(p => p.Usuario)
                .Where(p => p.Usuario != null)
                .ToList();

            ViewBag.IdAlumno = new SelectList(alumnos, "Id", "Usuario.NombreUsuario", matricula.IdAlumno);
            ViewBag.IdProfesor = new SelectList(profesores, "Id", "Usuario.NombreUsuario", matricula.IdProfesor);

            return View(matricula);
        }

        // GET: Matriculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var matricula = await _context.Matriculas
                .Include(m => m.Alumno)
                    .ThenInclude(a => a.Usuario)
                .Include(m => m.Profesor)
                    .ThenInclude(p => p.Usuario)
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
            if (matricula != null)
            {
                _context.Matriculas.Remove(matricula);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
