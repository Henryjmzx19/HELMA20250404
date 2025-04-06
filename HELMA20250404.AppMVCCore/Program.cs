using Microsoft.AspNetCore.Authentication.Cookies;
using HELMA20250404.AppMVCCore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar DbContext
builder.Services.AddDbContext<SistemaCalificacionesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn"));
});

// Configurar autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Usuarios/Login");  // Ruta de inicio de sesión
        options.AccessDeniedPath = new PathString("/Usuarios/Login");  // Ruta de acceso denegado
        options.ExpireTimeSpan = TimeSpan.FromHours(8);  // Establecer la duración de la cookie
        options.SlidingExpiration = true;  // Activar la expiración deslizante
        options.Cookie.HttpOnly = true;  // Asegurarse de que la cookie sea solo accesible por HTTP
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Usar autenticación y autorización
app.UseAuthentication();  // Asegúrate de que el middleware de autenticación se registre
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
