using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Web.Backend.Data;
using Web.Backend.Helpers;
using Web.Backend.Models;
using Web.Backend.Responses;

namespace Web.Backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/clientes")]
    public class ClientesController : ApiController
    {
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult InicioSession(LoginDTO loginDTO)
        {
            try
            {
                // 1. Validación de entrada
                if (loginDTO == null || string.IsNullOrWhiteSpace(loginDTO.Usuario) || string.IsNullOrWhiteSpace(loginDTO.Clave))
                {
                    return Ok(RespuestaError("Debe ingresar su usuario y una contraseña para iniciar sesión."));
                }

                // 2. Consulta a la base de datos
                var respuesta = ClienteData.LoginClientes(loginDTO.Usuario);

                if (!respuesta.Estado || respuesta.Data == null)
                {
                    return Ok(RespuestaError(respuesta.Mensaje ?? "Credenciales incorrectas."));
                }

                var objCliente = respuesta.Data;

                // 3. Verificamos la contraseña (BCrypt)
                bool passCorrecta = Utilidades.Verify(loginDTO.Clave, objCliente.ClaveHash);

                if (!passCorrecta)
                {
                    return Ok(RespuestaError("Usuario o Contraseña incorrectos."));
                }

                // 4. ACTUALIZACIÓN DEL TOKEN PUSH (Si el móvil lo envió)
                if (!string.IsNullOrWhiteSpace(loginDTO.ExpoPushToken))
                {
                    // Llamamos a tu método ActualizarToken
                    ClienteData.ActualizarToken(objCliente.IdCliente, loginDTO.ExpoPushToken);
                }

                // 5. Limpiamos la contraseña por seguridad antes de mandar el JSON
                objCliente.ClaveHash = "";

                // 6. Éxito
                return Ok(new Respuesta<ClienteDTO>
                {
                    Estado = true,
                    Mensaje = "Bienvenido al sistema",
                    Data = objCliente
                });
            }
            catch (Exception ex)
            {
                // Si algo explota (ej: fallo en BCrypt), el cliente recibe un JSON ordenado
                return Ok(RespuestaError("Ocurrió un error inesperado al iniciar sesión: " + ex.Message));
            }
        }

        private Respuesta<ClienteDTO> RespuestaError(string mensaje)
        {
            return new Respuesta<ClienteDTO>
            {
                Estado = false,
                Mensaje = mensaje,
                Data = null
            };
        }

    }
}