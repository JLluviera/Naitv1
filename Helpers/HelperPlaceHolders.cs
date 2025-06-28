using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Naitv1.Models;

namespace Naitv1.Helpers
{
    public static class HelperPlaceHolders
    {
        public static string ProcesarTemplate(string mensaje, Dictionary<string, string> valores)
        {
            if (string.IsNullOrEmpty(mensaje))
                return mensaje;

            if (!ValidarTemplate(mensaje))
                throw new Exception("Error en la plantilla: número de llaves desbalanceado");

            // Obtener todos los placeholders encontrados
            var placeholders = Regex.Matches(mensaje, @"\{\{(.*?)\}\}")
                                    .Cast<Match>()
                                    .Select(m => m.Groups[1].Value.Trim())
                                    .Distinct()
                                    .ToList();

            // 3. Verificar que todas las claves existan en el diccionario
            var desconocidos = placeholders.Where(p => !valores.ContainsKey(p)).ToList();

            if (desconocidos.Any())
                throw new Exception("Error en la plantilla: variables no reconocidas -> " + string.Join(", ", desconocidos));

            // 4. Hacer el reemplazo
            var mensajeFinal = Regex.Replace(mensaje, @"\{\{(.*?)\}\}", match =>
            {
                var key = match.Groups[1].Value.Trim();
                return valores.TryGetValue(key, out var value) ? value : match.Value;
            });

            return mensajeFinal;
        }

        public static bool ValidarTemplate(string mensaje)
        {
            // Validar que el número de llaves sea correcto
            int apertura = Regex.Matches(mensaje, @"\{\{").Count;
            int cierre = Regex.Matches(mensaje, @"\}\}").Count;

            return (apertura != cierre);
        }

        public static Dictionary<string, string> DiccionarioUsuario(Usuario user)
        {
            var diccUsuario = new Dictionary<string, string>
            {
                { "usuario.nombre" , user.Nombre },
                { "usuario.email" , user.Email },
                { "usuario.tipoUsuario" , user.TipoUsuario }
            };
            return diccUsuario;
        }
    }
}
