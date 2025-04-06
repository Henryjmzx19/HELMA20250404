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
                .Include(m => m.Alumno)
                    .ThenInclude(a => a.Usuario) // Incluir usuario del alumno
                .Include(m => m.Profesor)
                    .ThenInclude(p => p.Usuario); // Incluir usuario del profesor

            return View(await sistemaCalificacionesContext.ToListAsync());
        }

        // GET: Matriculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var matricula = await _context.Matriculas
                .Include(m => m.Alumno)
                    .ThenInclude(a => a.Usuario) // Incluye el usuario del alumno
                .Include(m => m.Profesor)
                    .ThenInclude(p => p.Usuario) // Incluye el usuario del profesor
                .FirstOrDefaultAsync(m => m.IdMatricula == id);

            if (matricula == null)
            {
                return NotFound();
            }

            if (matricula == null) return NotFound();

            return View(matricula);
        }

        // GET: Matriculas/Create
        // GET: Matriculas/Create
        public IActionResult Create()
        {
            var alumnos = _context.Alumnos.Include(a => a.Usuario).ToList();
            var profesores = _context.Profesores.Include(p => p.Usuario).ToList();

            if (alumnos == null || !alumnos.Any() || profesores == null || !profesores.Any())
            {
                // Si no hay alumnos o profesores, muestra un mensaje de error
                return View("Error");  // O cualquier vista de error que tengas
            }

            ViewBag.IdAlumno = new SelectList(alumnos, "Id", "Usuario.NombreUsuario");
            ViewBag.IdProfesor = new SelectList(profesores, "Id", "Usuario.NombreUsuario");

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

            // Si hay algún error, volvemos a pasar las listas de selección
            var alumnos = _context.Alumnos.Include(a => a.Usuario).ToList();
            var profesores = _context.Profesores.Include(p => p.Usuario).ToList();

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
                    .ThenInclude(a => a.Usuario) // Incluimos al usuario del alumno
                .Include(m => m.Profesor)
                    .ThenInclude(p => p.Usuario) // Incluimos al usuario del profesor
                .FirstOrDefaultAsync(m => m.IdMatricula == id);

            if (matricula == null)
            {
                return NotFound();
            }

            ViewBag.IdAlumno = new SelectList(_context.Alumnos
                .Include(a => a.Usuario), "Id", "Usuario.NombreUsuario", matricula.IdAlumno);

            ViewBag.IdProfesor = new SelectList(_context.Profesores
                .Include(p => p.Usuario), "Id", "Usuario.NombreUsuario", matricula.IdProfesor);

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

            ViewBag.IdAlumno = new SelectList(_context.Alumnos
                .Include(a => a.Usuario), "Id", "Usuario.NombreUsuario", matricula.IdAlumno);

            ViewBag.IdProfesor = new SelectList(_context.Profesores
                .Include(p => p.Usuario), "Id", "Usuario.NombreUsuario", matricula.IdProfesor);

            return View(matricula);
        }

        // GET: Matriculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var matricula = await _context.Matriculas
                .Include(m => m.Alumno)
                .ThenInclude(a => a.Usuario) // Asegúrate de incluir la navegación al usuario del alumno
                .Include(m => m.Profesor)
                .ThenInclude(p => p.Usuario) // Asegúrate de incluir la navegación al usuario del profesor
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
        public async Task<IActionResult> Delete(int id)
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
