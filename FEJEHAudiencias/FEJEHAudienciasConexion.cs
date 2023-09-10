using System.Data.SqlClient;
using System;
using System.Data;
using System.Diagnostics;

namespace FEJEHAudiencias
{
    public class FEJEHAudienciasConexion
    {
        public class ConexionBD
        {
            private readonly string connectionString;
            public SqlConnection Connection { get; private set; }

            public ConexionBD()
            {
                // Cadena de conexión a la base de datos. Reemplaza los valores con los de tu base de datos.
                connectionString = "Data Source=BRAYANPC;Initial Catalog=Acusatorio;Integrated Security=True;";
                Connection = new SqlConnection(connectionString);
            }

            public bool Conectar()
            {
                try
                {
                    Connection.Open();
                    Debug.WriteLine("Conexión exitosa");
                    return true;
                }
                catch (Exception ex)
                {
                    // Manejo de la excepción o registro de errores en un log
                    Debug.WriteLine("Error al conectar a la base de datos: " + ex.Message);
                    return false;
                }
            }

            public void Desconectar()
            {
                if (Connection != null && Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
            }



            public bool AuthLoginMultimedia(string nombreUsuario, string contrasena)
            {
                try
                {
                    if (Conectar())
                    {
                        // Utilizar consultas parametrizadas para evitar ataques de inyección SQL
                        string consulta = "SELECT COUNT(*) FROM P_Usuarios WHERE (Usuario = @nombreUsuario AND Contrasena =  @contrasena ) AND (Tipo = 'o')";

                        using (SqlCommand cmd = new SqlCommand(consulta, Connection))
                        {
                            cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                            cmd.Parameters.AddWithValue("@contrasena", contrasena);

                            int count = Convert.ToInt32(cmd.ExecuteScalar());

                            return count > 0;
                        }
                    }
                    else
                    {
                        // La conexión no se pudo establecer
                        Debug.WriteLine("Error al conectar a la base de datos");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de la excepción o registro de errores en un log
                    Debug.WriteLine("Ha ocurrido un error al verificar las credenciales: " + ex.Message);
                    return false;
                }
                finally
                {
                    // Asegurarse de que siempre se desconecte la base de datos
                    Desconectar();
                }


            }



            public class Usuario
            {
                public string Nombre { get; set; }
                public string Distrito { get; set; }
                public string Cargo { get; set; }
               
            }
            public Usuario GetUserInfo(string nombreUsuario)
            {
                try
                {
                    if (Conectar())
                    {
                        string consulta = "SELECT Usuario, Nombre, NomUser FROM P_Usuarios WHERE Usuario = @nombreUsuario";
                        using (SqlCommand cmd = new SqlCommand(consulta, Connection))
                        {
                            cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Usuario usuario = new Usuario
                                    {
                                        Nombre = reader["Usuario"].ToString(),
                                        Distrito = reader["Nombre"].ToString(),
                                        Cargo = reader["NomUser"].ToString(),
                                        
                                    };
                                    return usuario;
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Error al conectar a la base de datos");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Ha ocurrido un error al obtener la información del usuario: " + ex.Message);
                }
                finally
                {
                    Desconectar();
                }

                return null;
            }
        }
    }
}