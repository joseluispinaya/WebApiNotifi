using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Web.Backend.Models;
using Web.Backend.Responses;

namespace Web.Backend.Data
{
    public class DepartamentoData
    {
        public static Respuesta<List<EDepartamento>> ListaDepartamentos()
        {
            // 1. Iniciamos la respuesta por defecto en "Error" por si algo falla
            Respuesta<List<EDepartamento>> rpt = new Respuesta<List<EDepartamento>>()
            {
                Estado = false,
                Data = new List<EDepartamento>(), // Lista vacía, no nula
                Mensaje = "Error desconocido"
            };

            try
            {
                // Usamos la cadena limpia del Web.config
                using (SqlConnection con = new SqlConnection(Conexion.RutaConexion))
                {
                    using (SqlCommand comando = new SqlCommand("usp_ListarDepartamentos", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rpt.Data.Add(new EDepartamento
                                {
                                    IdDepartamento = Convert.ToInt32(dr["IdDepartamento"]),
                                    NombreDep = dr["NombreDep"].ToString(),
                                    NroProvi = Convert.ToInt32(dr["NroProvi"])
                                });
                            }
                        }
                    }
                }

                // Si todo salió bien, actualizamos la respuesta
                rpt.Estado = true;
                rpt.Mensaje = "Lista obtenida correctamente";
            }
            catch (Exception ex)
            {
                // Si hay error, el frontend sabrá exactamente qué pasó
                rpt.Estado = false;
                rpt.Mensaje = $"Error en BD: {ex.Message}";
            }

            return rpt;
        }
    }
}