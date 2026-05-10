using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Backend.Helpers
{
    public class Utilidades
    {
        public static bool Verify(string password, string hash)
        {
            // Validamos que ninguno de los dos sea nulo o vacío
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                return false;

            // Verifica si la contraseña en texto plano coincide con el hash de la BD
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}