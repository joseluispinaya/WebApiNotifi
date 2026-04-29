using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Backend.Models
{
    public class EDepartamento
    {
        public int IdDepartamento { get; set; }
        public string NombreDep { get; set; }
        public int NroProvi { get; set; }
        public string CantProvi =>
            NroProvi == 0
            ? "0 Provs"
            : NroProvi == 1
                ? "1 Prov"
                : $"{NroProvi} Provs";
    }
}