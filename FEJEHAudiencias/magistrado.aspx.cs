using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FEJEHAudiencias
{
    public partial class magistrado : Page
    {
        private string connectionString = "Data Source=BRAYANPC;Initial Catalog=Acusatorio;Integrated Security=True;";
        protected void Page_Load(object sender, EventArgs e)
        {
            string curpDelUsuario = GetCookieValue("curpUsuario");

            if (string.IsNullOrEmpty(curpDelUsuario) || !UsuarioTienePermiso(curpDelUsuario))
            {
                Response.Redirect("Busqueda.aspx");
            }
        }

        private string GetCookieValue(string cookieName)
        {
            if (Request.Cookies[cookieName] != null && !string.IsNullOrEmpty(Request.Cookies[cookieName].Value))
            {
                return HttpUtility.UrlDecode(Request.Cookies[cookieName].Value);  // Decodificamos el valor
            }

            return string.Empty; // Retorna una cadena vacía si la cookie no existe o si su valor es nulo o vacío.
        }

        //CODIGO PARA VALIDACION DE LA PANTALLA PERMISOS
        private bool UsuarioTienePermiso(string curp)
        {
            bool tienePermiso = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT magistrado FROM P_FUsuarios WHERE curp = @curp", connection);
                cmd.Parameters.AddWithValue("@curp", curp);
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value) // Verificamos también que el resultado no sea DBNull
                {
                    tienePermiso = Convert.ToBoolean(result);
                }
            }

            return tienePermiso;
        }


        //CODIGO PARA VALIDACION DE LA PANTALLA PERMISOS

        protected void buscarCausa_Click(object sender, EventArgs e)
        {
            string numeroCausa = Text1.Value;
            DataTable dtAudiencias = GetAudienciasPorNumeroCausa(numeroCausa);
            // Agregar la columna "Causa" al DataTable si no existe
            if (!dtAudiencias.Columns.Contains("Causa"))
            {
                DataColumn causaColumn = new DataColumn("Causa");
                dtAudiencias.Columns.Add(causaColumn);
            }
            // Asignar el valor de "numeroCausa" a la columna "Causa" en cada fila del DataTable
            foreach (DataRow row in dtAudiencias.Rows)
            {
                row["Causa"] = numeroCausa;
            }
            // Llena la tabla con los datos obtenidos
            gridViewAudiencias.DataSource = dtAudiencias;
            gridViewAudiencias.DataBind();
        }
        private DataTable GetAudienciasPorNumeroCausa(string numeroCausa)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string consulta = @"
                    SELECT la.IdRegAudiencia, ta.TipoAudi AS Tipo, la.SalaDescrip, la.FechaAudi, la.FechaCap, v.Titulo, v.VideoAsignado
                    FROM P_ListaAudiencias la
                    INNER JOIN P_CatTipoAudiencias ta ON la.IdTipoAudi = ta.IdTipoAudi
                    INNER JOIN P_Video v ON la.IdRegAudiencia = v.IdRegAudiencias
                    WHERE la.Causa = @NumeroCausa";
                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {
                    cmd.Parameters.AddWithValue("@NumeroCausa", numeroCausa);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }
        protected void gridViewAudiencias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox checkBox = e.Row.FindControl("checkBox") as CheckBox;
                if (checkBox != null)
                {
                    string idRegAudiencia = DataBinder.Eval(e.Row.DataItem, "IdRegAudiencia").ToString();
                    checkBox.Attributes["value"] = idRegAudiencia;
                }
            }
        }
        protected void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            //Response.Write("Método checkBox_CheckedChanged llamado.");
            CheckBox clickedCheckBox = sender as CheckBox;
            if (clickedCheckBox != null && clickedCheckBox.Checked)
            {
                foreach (GridViewRow row in gridViewAudiencias.Rows)
                {
                    CheckBox checkBox = row.FindControl("checkBox") as CheckBox;
                    if (checkBox != null && checkBox != clickedCheckBox)
                    {
                        checkBox.Checked = false;
                    }
                }
                //Response.Write("Método checkBox_CheckedChanged finalizado dentro de if principal.");
            }
        }
        private int GetIdFUsuarioPorCurp(string curp)
        {
            int idFUsuario = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string consulta = "SELECT IdFUsuario FROM P_FUsuarios WHERE curp = @Curp";
                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {
                    cmd.Parameters.AddWithValue("@Curp", curp);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        idFUsuario = Convert.ToInt32(result);
                    }
                }
            }
            //Response.Write("IdFUsuario obtenido: " + idFUsuario);  // Agregado para verificar el valor
            return idFUsuario;
        }
        private string GetSelectedIdRegAudiencia()
        {
            foreach (GridViewRow row in gridViewAudiencias.Rows)
            {
                CheckBox checkBox = row.FindControl("checkBox") as CheckBox;
                if (checkBox != null && checkBox.Checked)
                {
                    return checkBox.Attributes["value"];
                }
            }
            return null;  // Retorna null si no hay ningún checkbox seleccionado.
        }
        protected void btnCURP_Click(object sender, EventArgs e)
        {
            string curpIngresada = inputCURP.Value.Trim();
            int idFUsuario = GetIdFUsuarioPorCurp(curpIngresada);
            string idRegAudienciaSeleccionado = GetSelectedIdRegAudiencia();
            if (idFUsuario > 0)
            {
                if (!string.IsNullOrEmpty(idRegAudienciaSeleccionado))
                {
                    bool insertadoConExito = ActualizarListaAudiencias(idFUsuario, idRegAudienciaSeleccionado);

                    if (insertadoConExito)
                    {
                        MostrarAlerta("Datos insertados con éxito.");
                    }
                    else
                    {
                        MostrarAlerta("Error al insertar los datos.");
                    }
                }
                else
                {
                    MostrarAlerta("Por favor, seleccione una audiencia.");
                }
            }
            else
            {
                MostrarAlerta("CURP no encontrada en la base de datos.");
            }
        }
        private void MostrarModalAlerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showModal", $"mostrarModalAlerta('{mensaje}');", true);
        }
        private void MostrarAlerta(string mensaje)
        {
            string script = $"alert('{mensaje}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "AlertScript", script, true);
        }
        private bool ActualizarListaAudiencias(int idFUsuario, string idRegAudiencia)
        {
            bool exito = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Primer update: Asignar el usuario a la audiencia.
                string consultaAudiencia = @"
        UPDATE P_ListaAudiencias 
        SET IdFUsuarios = @IdFUsuario 
        WHERE IdRegAudiencia = @IdRegAudiencia";

                using (SqlCommand cmd = new SqlCommand(consultaAudiencia, connection))
                {
                    cmd.Parameters.AddWithValue("@IdFUsuario", idFUsuario);
                    cmd.Parameters.AddWithValue("@IdRegAudiencia", idRegAudiencia);
                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            exito = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Aquí puedes manejar o registrar el error según necesites.
                    }
                }

                // Segundo update: Marcar el video como asignado.
                if (exito)  // Solo ejecuta el segundo update si el primer update fue exitoso.
                {
                    string consultaVideo = @"
            UPDATE P_Video 
            SET VideoAsignado = 'true' 
            WHERE IdRegAudiencias = @IdRegAudiencia";

                    using (SqlCommand cmdVideo = new SqlCommand(consultaVideo, connection))
                    {
                        cmdVideo.Parameters.AddWithValue("@IdRegAudiencia", idRegAudiencia);
                        try
                        {
                            cmdVideo.ExecuteNonQuery();
                            // No necesitas verificar exito aquí a menos que quieras saber si el segundo UPDATE también fue exitoso.
                        }
                        catch (Exception ex)
                        {
                            // Aquí puedes manejar o registrar el error según necesites.
                        }
                    }
                }
            }
            return exito;
        }


    }
}