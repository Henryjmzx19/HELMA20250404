using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HELMA20250404.AppMVCCore.Models;

namespace HELMA20250404.AppMVCCore.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly SistemaCalificacionesContext _context;

        public AlumnosController(SistemaCalificacionesContext context)
        {
            _context = context;
        }
        public async Task<byte[]?> GenerarByteImage(IFormFile? file, byte[]? bytesImage = null)
        {
            byte[]? bytes = bytesImage;
            if (file != null && file.Length > 0)
            {
                // Construir la ruta del archivo               
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    bytes = memoryStream.ToArray(); // Devuelve los bytes del archivo
                }
            }
            return bytes;
        }
        // GET: Alumnos
        public async Task<IActionResult> Index(Alumno alumno, int topRegistro = 10)
        {
            var query = _context.Alumnos.AsQueryable();
            query = query.Include(s => s.IdUsuarioNavigation);
            if (!string.IsNullOrWhiteSpace(alumno.Apellido))
                query = query.Where(s => s.Apellido.Contains(alumno.Apellido));
            if (alumno.IdUsuarioNavigation!=null && !string.IsNullOrWhiteSpace(alumno.IdUsuarioNavigation.NombreUsuario))
                query = query.Where(s => s.IdUsuarioNavigation.NombreUsuario.Contains(alumno.IdUsuarioNavigation.NombreUsuario));
            if (!string.IsNullOrWhiteSpace(alumno.Telefono))
                query = query.Where(s => s.Telefono.Contains(alumno.Telefono));
            if (!string.IsNullOrWhiteSpace(alumno.Direccion))
                query = query.Where(s => s.Direccion.Contains(alumno.Direccion));
           
            if (topRegistro > 0)
                query = query.Take(topRegistro);
            return View(await query.ToListAsync());
        }

        // GET: Alumnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .Include(a => a.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumnos/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario");
            return View(new Usuario { Alumno=new Alumno() });
        }

        // POST: Alumnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,IdUsuario,Apellido,Nie,Telefono,Direccion,Encargado,ImagenBytes,FechaNacimiento")] Alumno alumno, IFormFile? file = null)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (file != null) // Si hay archivo, convertirlo en bytes
        //        {
        //            alumno.ImagenBytes = await GenerarByteImage(file);
        //        }

        //        _context.Add(alumno);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", alumno.IdUsuario);
        //    return View(alumno);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario, IFormFile? file = null)
        {
            try
            {
                var alumno = usuario.Alumno;
                if (file != null) // Si hay archivo, convertirlo en bytes
                {
                    alumno.ImagenBytes = await GenerarByteImage(file);
                }

                _context.Add(usuario);
                int result = await _context.SaveChangesAsync();
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                return View(alumno);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(usuario);
            }

        }
        // GET: Alumnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", alumno.IdUsuario);
            return View(alumno);
        }

        // POST: Alumnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,Apellido,Nie,Telefono,Direccion,Encargado,ImagenBytes,YearNacimiento")] Alumno alumno, IFormFile? file = null)
        {
            if (id != alumno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var byteImagesAnterior = await _context.Alumnos
                         .Where(s => s.Id == alumno.Id)
                         .Select(s => s.ImagenBytes).FirstOrDefaultAsync();

                    alumno.ImagenBytes = await GenerarByteImage(file, byteImagesAnterior);
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumno.Id))
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

            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", alumno.IdUsuario);
            return View(alumno);
        }


        // GET: Alumnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .Include(a => a.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // POST: Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Bind("Id,IdUsuario,Apellido,Nie,Telefono,Direccion,Encargado,ImagenBytes,YearNacimient")] Alumno alumno, IFormFile? file = null)
        {

            alumno.ImagenBytes = await GenerarByteImage(file);
            _context.Add(alumno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
       
            return View(alumno);
        }
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno != null)
            {
                _context.Alumnos.Remove(alumno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.Id == id);
        }
        public async Task<IActionResult> EliminarImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _context.Alumnos.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }
            marca.ImagenBytes = null;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
