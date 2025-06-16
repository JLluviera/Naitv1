using Naitv1.Models;
using Naitv1.Data;

namespace Naitv1.Helpers
{
    public sealed class PlanificadorNotificaciones
    {
        private static PlanificadorNotificaciones? unicaInstancia = null;

        private readonly AppDbContext _context;

        private PlanificadorNotificaciones(AppDbContext context)
        {
            _context = context;
        }

        public static PlanificadorNotificaciones ObtenerInstancia(AppDbContext contexto)
        {
            if (unicaInstancia == null)
            {
                unicaInstancia = new PlanificadorNotificaciones(contexto);
            }
            return unicaInstancia;
        }

        public bool CrearRegisto(Notificaciones notificacion)
        {
            _context.Notificaciones.Add(notificacion);
            _context.SaveChanges();

            return true;
        }
    }
}
