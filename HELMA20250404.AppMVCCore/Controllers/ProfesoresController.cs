using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HELMA20250404.AppMVCCore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;

namespace HELMA20250404.AppMVCCore.Controllers
{
   
    public class ProfesoresController : Controller
    {
        private readonly SistemaCalificacionesContext _context;

        public ProfesoresController(SistemaCalificacionesContext context)
        {
            _context = context;
        }

        // GET: Profesores
        public async Task<IActionResult> Index(Profesore profesore, int topRegistro = 10)
        {
            var query = _context.Profesores.AsQueryable();
            query = query.Include(s => s.Usuario);
            if (profesore.Usuario != null && !string.IsNullOrWhiteSpace(profesore.Usuario.NombreUsuario))
                query = query.Where(s => s.Usuario.NombreUsuario.Contains(profesore.Usuario.NombreUsuario));
            if (!string.IsNullOrWhiteSpace(profesore.Apellido))
                query = query.Where(s => s.Apellido.Contains(profesore.Apellido));
            if (!string.IsNullOrWhiteSpace(profesore.Dui))
                query = query.Where(s => s.Dui.Contains(profesore.Dui));
            if (!string.IsNullOrWhiteSpace(profesore.Telefono))
                query = query.Where(s => s.Telefono.Contains(profesore.Telefono));
            if (!string.IsNullOrWhiteSpace(profesore.Direccion))
                query = query.Where(s => s.Direccion.Contains(profesore.Direccion));
            if (topRegistro > 0)
                query = query.Take(topRegistro);
            return View(await query.ToListAsync());
        }


        // GET: Profesores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesore = await _context.Profesores
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesore == null)
            {
                return NotFound();
            }

            return View(profesore);
        }
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario");
            return View(new Usuario { Profesore = new Profesore() });
        }
        // POST: Profesores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            try
            {
                usuario.Password = CalcularHashMD5(usuario.Password);
                var profesor = usuario.Profesore;

                _context.Add(usuario); // Guardar usuario
                await _context.SaveChangesAsync();

                // Asociar el usuario al profesor
                int? idUsuario = profesor.IdUsuario;

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                ModelState.AddModelError("", "Error al crear el profesor: " + ex.Message);
                return View(usuario);
            }
        }

        // GET: Profesores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesore = await _context.Profesores.FindAsync(id);
            if (profesore == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", profesore.IdUsuario);
            return View(profesore);
        }

        // POST: Profesores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,Apellido,Telefono,Dui,Direccion,FechaNacimiento")] Profesore profesore)
        {
            if (id != profesore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesoreExists(profesore.Id))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", profesore.IdUsuario);
            return View(profesore);
        }

        // GET: Profesores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesore = await _context.Profesores
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesore == null)
            {
                return NotFound();
            }

            return View(profesore);
        }

        // POST: Profesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesore = await _context.Profesores.FindAsync(id);
            if (profesore != null)
            {
                _context.Profesores.Remove(profesore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesoreExists(int id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }
        private string CalcularHashMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // "x2" convierte el byte en una cadena hexadecimal de dos caracteres.
                }
                return sb.ToString();
            }
        }
    }
}
