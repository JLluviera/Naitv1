using Microsoft.AspNetCore.Mvc;
using Naitv1.Models;
using Naitv1.Data;
using Naitv1.Helpers;
using System.Net.Http.Json;

namespace Naitv1.Controllers
{
    public class ActividadesController : Controller
    {
        private readonly AppDbContext _context;

        public ActividadesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Visibles()
        {
            List<Actividad> actividades = _context.Actividades
                .Where(a => a.Activa == true)
                .ToList();

            return Json(actividades);
        }

        [HttpPost]
        public IActionResult Index(string mensajeDelAnfitrion, string tipoActividad, float lat, float lon, float? latSuperAdmin, float? lonSuperAdmin)
        {
            Usuario usuario = UsuarioLogueado.Usuario(HttpContext.Session);
            Usuario? usuarioContext = _context.Usuarios
                                             .FirstOrDefault(uc => uc.Id == usuario.Id);
            
            if (usuarioContext == null)
            {
                ViewBag.Error("Debe estar logueado");
                return RedirectToAction("Index", "Home");
            };

            Actividad actividad = new Actividad();

            actividad.MensajeDelAnfitrion = mensajeDelAnfitrion;
            actividad.TipoActividad = tipoActividad;
            if (latSuperAdmin != null && lonSuperAdmin != null && UsuarioLogueado.esSuperAdmin(HttpContext.Session))
            {
                actividad.Lat = (float) latSuperAdmin;
                actividad.Lon = (float) lonSuperAdmin;
            }
            else
            {
                actividad.Lat = lat;
                actividad.Lon = lon;
            }
            actividad.AnfitrionId = usuario.Id;
            actividad.Activa = true;
            

            _context.Actividades.Add(actividad);
            _context.SaveChanges();

            usuarioContext.Anfitrion = true;
            usuarioContext.ActividadesDelUsuario.Add(actividad);
            usuarioContext.idActividadAnfitrion = _context.Actividades.Where(a => a.AnfitrionId == usuario.Id)
                .Select(a => a.Id)
                .FirstOrDefault();

            _context.Usuarios.Update(usuarioContext);

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        
        public IActionResult FinalizarActividad()
        {
            Usuario user = UsuarioLogueado.Usuario(HttpContext.Session);
            Usuario? userContext = _context.Usuarios
                .Where(u => u.Id == user.Id)
                .FirstOrDefault();

            if (userContext == null)
            {
                ViewBag.Error = "No se encontro el Usuario en la BD";
                Console.WriteLine("no se ecnotro usuario");
                return RedirectToAction("Index", "Home");
            }

            if (!userContext.Anfitrion)
            {
                Console.WriteLine("no es anfitrion");
                ViewBag.Error = "Debes ser anfitrion de una actividad";
                return RedirectToAction("Index", "Home");
            }

            Actividad? actividad = _context.Actividades
                .Where(a => a.AnfitrionId == user.Id)
                .FirstOrDefault();

            if (actividad == null)
            {
                Console.Write("No esta la actividad");
                return NotFound("Actividad de usuario no encontrada");
            }
            
            actividad.Activa = false;
            actividad.CambioReciente = true;
            actividad.Activa = false;
            actividad.MensajeDelAnfitrion = actividad.MensajeDelAnfitrion;
            userContext.Anfitrion = false;
            userContext.idActividadAnfitrion = 0;

           
            _context.Actividades.Update(actividad);
            _context.Usuarios.Update(userContext);
            _context.SaveChanges();

            actividad.OnAfterSave(_context);
            return RedirectToAction("NotificacionesIndex", "Admin");
        }



    }
}
