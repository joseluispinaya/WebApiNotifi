using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Backend.Responses
{
    public class Respuesta<T>
    {
        public bool Estado { get; set; }
        public string Valor { get; set; }
        public string Mensaje { get; set; }
        public T Data { get; set; }
    }
}