using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace FEJEHAudiencias
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si la página se cargó debido a la redirección por inactividad
            if (Request.QueryString["inactivo"] != null && Request.QueryString["inactivo"] == "true")
            {
                // El usuario fue redirigido aquí debido a inactividad, obtener el nombre de usuario de la sesión
                string nombreUsuario = (string)Session["UserName"];
                if (!string.IsNullOrEmpty(nombreUsuario) && (bool?)Session["IsAuthenticated"] == true)
                {
                    try
                    {
                        // Crear una instancia de la clase FEJEHAudienciasConexion.ConexionBD
                        FEJEHAudienciasConexion.ConexionBD conexion = new FEJEHAudienciasConexion.ConexionBD();

                        if (conexion.Conectar())
                        {
                            // Utilizar consulta parametrizada para evitar ataques de inyección SQL
                            string consulta = "UPDATE usuarios SET IsAuthenticated = @isAuthenticated WHERE nombre_usuario = @nombreUsuario";
                            using (SqlCommand cmd = new SqlCommand(consulta, conexion.Connection))
                            {
                                cmd.Parameters.AddWithValue("@isAuthenticated", false);
                                cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected <= 0)
                                {
                                    // Si no se afectaron filas, puede que el usuario no exista en la base de datos
                                    throw new Exception("No se encontró el usuario en la base de datos.");
                                }
                            }
                        }
                        else
                        {
                            // La conexión no se pudo establecer
                            throw new Exception("Error al conectar a la base de datos");
                        }

                        // Eliminar la variable de sesión IsAuthenticated
                        Session.Remove("IsAuthenticated");
                    }
                    catch (Exception ex)
                    {
                        // Manejo de la excepción o registro de errores en un log
                        // Puedes mostrar un mensaje de error al usuario o redirigirlo a una página de error genérica
                        
                    }
                }
            }
        }
    }
}
