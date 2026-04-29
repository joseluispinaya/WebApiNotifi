using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Web.Backend.Data;

namespace Web.Backend.Controllers
{
    // Habilitamos CORS a nivel de controlador por seguridad
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    // Definimos la ruta base para este controlador
    [RoutePrefix("api/departamentos")]
    public class DepartamentosController : ApiController
    {
        [HttpGet]
        [Route("lista")]
        public IHttpActionResult ObtenerDepartamentos()
        {
            // solo Instanciamos DepartamentoData si el metodo no es estatico
            // DepartamentoData objData = new DepartamentoData();

            // Llamamos a tu método que devuelve Respuesta<List<EDepartamento>>
            // var respuesta = objData.ListaDepartamentos();

            // Llamada directa al método estático
            var respuesta = DepartamentoData.ListaDepartamentos();

            // Siempre devolvemos HTTP 200 (Ok), el Frontend leerá el campo "Estado"
            return Ok(respuesta);
        }
    }
}