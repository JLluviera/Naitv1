using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Naitv1.Data;
using Naitv1.Helpers;

namespace Naitv1.Models
{
    public class Actividad
    {
        public int Id { get; set; }
        public string MensajeDelAnfitrion { get; set; }
        public int AnfitrionId { get; set; }
        public Usuario? Anfitrion { get; set; }
        public string? TipoActividad { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public bool Activa { get; set; } = true;

        public bool CambioReciente = false;

        public static List<string> TiposActividad = new List<string>
        {
            "Tomar una",
            "Matear",
            "Fumar algo",
            "Bajonear algo",
            "Musica en vivo",
            "Jugar a algo",
            "Filosofar",
            "Asado",
            "Trabajar"
        };

        public void OnAfterSave(AppDbContext context)
        {
            if  (!Activa && CambioReciente)
            {
                PlanificadorNotificaciones planificado = PlanificadorNotificaciones.ObtenerInstancia(context);
                planificado.CrearRegistroPostEvento(this.Id, DateTime.Now.AddHours(2));
            }
        }
    }
}
