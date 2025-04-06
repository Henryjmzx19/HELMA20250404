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
        // GET: Matriculas
        public async Task<IActionResult> Index()
        {
            var sistemaCalificacionesContext = _context.Matriculas
                .Include(m => m.IdAlumnoNavigation)
                    .ThenInclude(a => a.IdUsuarioNavigation) // Incluir usuario del alumno
                .Include(m => m.IdProfesorNavigation)
                    .ThenInclude(p => p.IdUsuarioNavigation); // Incluir usuario del profesor

            return View(await sistemaCalificacionesContext.ToListAsync());
        }

        // GET: Matriculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(m => m.IdAlumnoNavigation)
                    .ThenInclude(a => a.IdUsuarioNavigation) // Incluye el usuario del alumno
                .Include(m => m.IdProfesorNavigation)
                    .ThenInclude(p => p.IdUsuarioNavigation) // Incluye el usuario del profesor
                .FirstOrDefaultAsync(m => m.IdMatricula == id);

            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // GET: Matriculas/Create
        public IActionResult Create()
        {
            ViewBag.IdAlumno = new SelectList(_context.Alumnos.Include(a => a.IdUsuarioNavigation), "Id", "IdUsuarioNavigation.NombreUsuario");
            ViewBag.IdProfesor = new SelectList(_context.Profesores.Include(p => p.IdUsuarioNavigation), "Id", "IdUsuarioNavigation.NombreUsuario");
            return View();
        }

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

            ViewBag.IdAlumno = new SelectList(_context.Alumnos.Include(a => a.IdUsuarioNavigation), "Id", "IdUsuarioNavigation.NombreUsuario", matricula.IdAlumno);
            ViewBag.IdProfesor = new SelectList(_context.Profesores.Include(p => p.IdUsuarioNavigation), "Id", "IdUsuarioNavigation.NombreUsuario", matricula.IdProfesor);

            return View(matricula);
        }

        // GET: Matriculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(m => m.IdAlumnoNavigation)
                    .ThenInclude(a => a.IdUsuarioNavigation) // Incluimos al usuario del alumno
                .Include(m => m.IdProfesorNavigation)
                    .ThenInclude(p => p.IdUsuarioNavigation) // Incluimos al usuario del profesor
                .FirstOrDefaultAsync(m => m.IdMatricula == id);

            if (matricula == null)
            {
                return NotFound();
            }

            ViewBag.IdAlumno = new SelectList(_context.Alumnos
                .Include(a => a.IdUsuarioNavigation), "Id", "IdUsuarioNavigation.NombreUsuario", matricula.IdAlumno);

            ViewBag.IdProfesor = new SelectList(_context.Profesores
                .Include(p => p.IdUsuarioNavigation), "Id", "IdUsuarioNavigation.NombreUsuario", matricula.IdProfesor);

            return View(matricula);
        }
        // POST: Matriculas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMatricula,IdAlumno,IdProfesor,YearLectivo")] Matricula matricula)
        {
            if (id != matricula.IdMatricula)
            {
                return NotFound();
            }

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

            ViewBag.IdAlumno = new SelectList(_context.Alumnos
                .Include(a => a.IdUsuarioNavigation), "Id", "IdUsuarioNavigation.NombreUsuario", matricula.IdAlumno);

            ViewBag.IdProfesor = new SelectList(_context.Profesores
                .Include(p => p.IdUsuarioNavigation), "Id", "IdUsuarioNavigation.NombreUsuario", matricula.IdProfesor);

            return View(matricula);
        }

        // GET: Matriculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(m => m.IdAlumnoNavigation)
                .ThenInclude(a => a.IdUsuarioNavigation) // Asegúrate de incluir la navegación al usuario del alumno
                .Include(m => m.IdProfesorNavigation)
                .ThenInclude(p => p.IdUsuarioNavigation) // Asegúrate de incluir la navegación al usuario del profesor
                .FirstOrDefaultAsync(m => m.IdMatricula == id);

            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula); // Pasar el modelo correctamente
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

            return RedirectToAction(nameof(Index)); // Redirige a la vista Index después de eliminar
        }
    }
}
