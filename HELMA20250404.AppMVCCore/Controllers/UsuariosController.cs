using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HELMA20250404.AppMVCCore.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HELMA20250404.AppMVCCore.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly SistemaCalificacionesContext _context;

        public UsuariosController(SistemaCalificacionesContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index(Usuario usuario, int topRegistro = 10)
        {
            var query = _context.Usuarios.AsQueryable();
            if (!string.IsNullOrWhiteSpace(usuario.NombreUsuario))
                query = query.Where(s => s.NombreUsuario.Contains(usuario.NombreUsuario));
            if (!string.IsNullOrWhiteSpace(usuario.Email))
                query = query.Where(s => s.Email.Contains(usuario.Email));
            if (!string.IsNullOrWhiteSpace(usuario.Rol))
                query = query.Where(s => s.Rol.Contains(usuario.Rol));
            if (topRegistro > 0)
                query = query.Take(topRegistro);
            return View(await query.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreUsuario,Email,Password,Rol")] Usuario usuario)
        {

            usuario.Password = CalcularHashMD5(usuario.Password);
            _context.Add(usuario);
                await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreUsuario,Email,Rol")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            try
            {
                var usuarioData = await _context.Usuarios.FirstOrDefaultAsync(s => s.Id == usuario.Id);
                if (usuarioData != null)
                {
                    usuarioData.Email = usuario.Email;
                    usuarioData.NombreUsuario = usuario.NombreUsuario;
                    usuarioData.Rol = usuario.Rol;
                    _context.Update(usuarioData);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
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

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        public IActionResult Login ()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] Usuario usuario)
        {
            try
            {
                usuario.Password = CalcularHashMD5(usuario.Password);
                var usuarioAuth = await _context.Usuarios
                    .FirstOrDefaultAsync(s => s.Email == usuario.Email && s.Password == usuario.Password);
                if (usuarioAuth != null && usuarioAuth.Id > 0)
                {
                    var claims = new[] {
                    new Claim(ClaimTypes.Name, usuarioAuth.NombreUsuario),
                    new Claim("Id", usuarioAuth.Id.ToString()),
                     new Claim("Email", usuarioAuth.Email),
                    new Claim(ClaimTypes.Role, usuarioAuth.Rol)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    throw new Exception("El email o password son incorrectos");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(usuario);
            }
        }
        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
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
