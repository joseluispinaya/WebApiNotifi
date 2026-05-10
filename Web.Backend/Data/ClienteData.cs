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
    public class ClienteData
    {
        public static Respuesta<ClienteDTO> LoginClientes(string NroCi)
        {
            try
            {
                ClienteDTO obj = null;

                using (SqlConnection con = new SqlConnection(Conexion.RutaConexion))
                {
                    using (SqlCommand comando = new SqlCommand("usp_LoginClientes", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@NroCi", NroCi);

                        con.Open();
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                obj = new ClienteDTO
                                {
                                    IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                    NroCi = dr["NroCi"].ToString(),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    ClaveHash = dr["ClaveHash"].ToString(),
                                    Genero = Convert.ToChar(dr["Genero"].ToString()),
                                    Celular = dr["Celular"].ToString()
                                };
                            }
                        }
                    }
                }

                return new Respuesta<ClienteDTO>
                {
                    Estado = obj != null,
                    Data = obj,
                    Mensaje = obj != null ? "Bienvenido usuario" : "Usuario o Contraseña incorrectos."
                };
            }
            catch (Exception)
            {
                return new Respuesta<ClienteDTO>
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error en el servidor. Intente más tarde.",
                    Data = null
                };
            }
        }

        public static void ActualizarToken(int IdCliente, string ExpoPushToken)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.RutaConexion))
                {
                    using (SqlCommand comando = new SqlCommand("usp_ActualizarTokenPush", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@IdCliente", IdCliente);
                        comando.Parameters.AddWithValue("@ExpoPushToken", ExpoPushToken);

                        con.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                // Se captura el error pero no se lanza (throw) para no detener la ejecucion
            }
        }

    }
}