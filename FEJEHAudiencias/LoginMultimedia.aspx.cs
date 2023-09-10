using System;
using System.Data.SqlClient;
using System.Web.Security;

namespace FEJEHAudiencias
{
    public partial class LoginMultimedia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Simulando datos ingresados por el usuario
            string nombreUsuario = txtUsername.Text;
            string contrasena = txtPassword.Text;

            // Crear una instancia de la clase FEJEHAudienciasConexion.ConexionBD
            FEJEHAudienciasConexion.ConexionBD conexion = new FEJEHAudienciasConexion.ConexionBD();

            try
            {
                // Utilizar el nuevo método AuthLoginMultimedia de la instancia creada
                bool esAutenticado = conexion.AuthLoginMultimedia(nombreUsuario, contrasena);

                if (esAutenticado)
                {
                    // Las credenciales son válidas, el usuario está autenticado

                    lblMessage.Text = "Inicio de sesión exitoso";
                    string userName = nombreUsuario; // Obtén el nombre de usuario del usuario autenticado
                    Session["NombreUsuario"] = nombreUsuario;
                    ActualizarEstadoAutenticacionEnBD(userName, true); // Actualiza el campo EsAutenticado en la base de datos

                    // Establecer la variable de sesión EsAutenticado en true
                    Session["EsAutenticado"] = true;

                    // Establecer la variable de sesión UserName con el nombre de usuario autenticado
                    Session["UserName"] = userName;

                    Response.Redirect("administrador.aspx");
                }
                else
                {
                    // Las credenciales no son válidas, el inicio de sesión falló
                    lblMessage.Text = "Usuario o contraseña incorrectos";
                }
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                lblMessage.Text = "Ha ocurrido un error: " + ex.Message;
            }
        }

        private void ActualizarEstadoAutenticacionEnBD(string nombreUsuario, bool isAuthenticated)
        {
            try
            {
                // Crear una instancia de la clase FEJEHAudienciasConexion.ConexionBD
                FEJEHAudienciasConexion.ConexionBD conexion = new FEJEHAudienciasConexion.ConexionBD();

                if (conexion.Conectar())
                {
                    // Utilizar consulta parametrizada para evitar ataques de inyección SQL
                    string consulta = "UPDATE P_Usuarios SET EsAutenticado = @isAuthenticated WHERE Usuario = @nombreUsuario";
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion.Connection))
                    {
                        cmd.Parameters.AddWithValue("@isAuthenticated", isAuthenticated);
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
            }
            catch (Exception ex)
            {
                // Manejo de la excepción o registro de errores en un log
                throw new Exception("Ha ocurrido un error al actualizar el estado de autenticación: " + ex.Message);
            }
        }
    }
}
