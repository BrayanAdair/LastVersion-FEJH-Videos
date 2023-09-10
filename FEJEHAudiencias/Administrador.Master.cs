using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static FEJEHAudiencias.FEJEHAudienciasConexion.ConexionBD;
namespace FEJEHAudiencias
{
    public partial class Administrador : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtener el nombre de usuario autenticado
            //string nombreUsuario = User.Identity.Name;

            string nombreUsuario = Session["NombreUsuario"].ToString();

            // Obtener información del usuario
            FEJEHAudienciasConexion.ConexionBD conexionBD = new FEJEHAudienciasConexion.ConexionBD();
            Usuario usuario = conexionBD.GetUserInfo(nombreUsuario);

            // Mostrar la información en las etiquetas del modal
            if (usuario != null)
            {
                nombreUsuarioS.Text = usuario.Distrito;
                distrito.Text = usuario.Nombre;
                cargo.Text = usuario.Cargo;
            }
            //}
        }
    }
}