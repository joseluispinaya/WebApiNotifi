using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Web.Backend.Data
{
    public class Conexion
    {
        // Lee la ruta directamente del Web.config de forma estática y limpia
        public static string RutaConexion = ConfigurationManager.ConnectionStrings["ConexionCadenas"].ConnectionString;
    }
}