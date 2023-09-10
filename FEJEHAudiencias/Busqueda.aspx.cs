using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;


namespace FEJEHAudiencias
{
    public partial class Busqueda1 : System.Web.UI.Page
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 1. Obtener los datos de las cookies
                var nombre = GetCookieValue("nombreUsuario");
                var email = GetCookieValue("emailUsuario");
                var curp = GetCookieValue("curpUsuario");
                var departamento = GetCookieValue("departamentoUsuario");

                // 2. Verificar que todos los datos son válidos
                if (!string.IsNullOrEmpty(nombre) &&
                    !string.IsNullOrEmpty(email) &&
                    !string.IsNullOrEmpty(curp) &&
                    !string.IsNullOrEmpty(departamento))
                {
                    // 3. Insertar datos en la base de datos
                    InsertIntoDatabase(nombre, email, curp, departamento);
                }
            }
        }

        private string GetCookieValue(string cookieName)
        {
            if (Request.Cookies[cookieName] != null)
                return HttpUtility.UrlDecode(Request.Cookies[cookieName].Value);  // Aquí decodificamos el valor
            else
                return string.Empty; // Retorna una cadena vacía si la cookie no existe
        }

        private void InsertIntoDatabase(string nombre, string email, string curp, string departamento)
        {
            string connectionString = "Data Source=BRAYANPC;Initial Catalog=Acusatorio;Integrated Security=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        MERGE INTO P_FUsuarios AS Target
                        USING (VALUES (@curp)) AS Source (Curp)
                        ON Target.Curp = Source.Curp
                        WHEN MATCHED THEN
                            UPDATE SET Target.Nombre = @nombre, 
                                       Target.Email = @email, 
                                       Target.Departamento = @departamento
                        WHEN NOT MATCHED THEN
                            INSERT (Nombre, Email, Curp, Departamento) 
                            VALUES (@nombre, @email, @curp, @departamento);";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", nombre);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@curp", curp);
                        command.Parameters.AddWithValue("@departamento", departamento);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar el error como mejor te parezca. Por ahora, simplemente lo omitiré.
            }
        }




        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string curp = curpInput.Value; // Obtener el valor del campo CURP

            DataTable videoDataTable = FiltrarVideosPorCURP(curp);

            string tablaHTML = GenerarTablaHTML(videoDataTable);

            divTablaVideos.InnerHtml = tablaHTML;

        }

        protected string ObtenerUrlSeleccionado()
        {
            string selectedVideoUrl = "";
            var radioButtons = Request.Form.GetValues("videoRadio");

            if (radioButtons != null && radioButtons.Length > 0)
            {
                selectedVideoUrl = radioButtons[0];
            }

            return selectedVideoUrl;
        }

        private DataTable FiltrarVideosPorCURP(string curp)
        {
            string connectionString = "Data Source=BRAYANPC;Initial Catalog=Acusatorio;Integrated Security=True;";
            DataTable videoDataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
        SELECT v.IdVideos, v.Titulo, v.Url, a.Causa, a.FechaAudi
        FROM P_Video v
        INNER JOIN P_ListaAudiencias a ON v.IdRegAudiencias = a.IdRegAudiencia
        INNER JOIN P_FUsuarios u ON a.IdFUsuarios = u.IdFUsuario
        WHERE u.curp = @CurpUsuario";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CurpUsuario", curp);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(videoDataTable);
                }
            }

            return videoDataTable;
        }

        private string GenerarTablaHTML(DataTable dt)
        {
            string tablaHTML = "<table class='table table-striped'>";
            tablaHTML += "<thead><tr><th></th><th>Título del Video</th><th>Causa</th><th>Fecha de Audiencia</th></tr></thead>";
            tablaHTML += "<tbody>";

            foreach (DataRow row in dt.Rows)
            {
                string titulo = row["Titulo"].ToString();
                string causa = row["Causa"].ToString();
                string fechaAudi = row["FechaAudi"].ToString();
                string videoUrl = row["Url"].ToString(); // Obtener la URL del video desde la columna "Url"

                tablaHTML += $"<tr><td><input type='radio' name='videoRadio' value='{videoUrl}'></td><td>{titulo}</td><td>{causa}</td><td>{fechaAudi}</td></tr>";
            }

            tablaHTML += "</tbody></table>";

            return tablaHTML;
        }

    }
}
