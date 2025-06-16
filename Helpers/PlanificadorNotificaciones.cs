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

        public bool CrearRegistro(Notificaciones notificacion)
        {
            _context.Notificaciones.Add(notificacion);
            _context.SaveChanges();

            return true;
        }

        public string CrearRegistroPostEvento(int Id, DateTime fechaHora)
        {
            Actividad? actividad = _context.Actividades.Find(Id);

            if (actividad == null)
            {
                return "Actividad no encontrada"; 
            }

            Notificaciones notificacion = new Notificaciones
            {
                Titulo = "Para que no te olvides esa noche especial",
                Mensaje = "Mira el seguimiento de la actividad",
                FechaHoraProgramada = fechaHora,
                CriterioSegmento = Notificaciones.CrearCriterio(new List<int> { actividad.CiudadId }, new List<string> { actividad.TipoActividad }),
                EstadoNotificacion = "Pendiente"
            }
        }
    }
}
