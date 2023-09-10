using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Web;
using System.Web.Optimization;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static FEJEHAudiencias.FEJEHAudienciasConexion;
using static FEJEHAudiencias.FEJEHAudienciasConexion.ConexionBD;
using static System.Net.Mime.MediaTypeNames;

namespace FEJEHAudiencias
{
    public partial class Administrador1 : System.Web.UI.Page
    {
        private string tipoAudiSeleccionado;
        private string numeroCausa;
        //conexion a servidor y base de datos se ocupara en todo el proyecto
        private string connectionString = "Data Source=BRAYANPC;Initial Catalog=Acusatorio;Integrated Security=True;";


        //informacion cuando readload la pagina es recaudadad 
        protected void Page_Load(object sender, EventArgs e)

        {
            if (Session["EsAutenticado"] == null || !(bool)Session["EsAutenticado"])
            {
                // Usuario no autenticado, redireccionar a la página de inicio de sesión
                Response.Redirect("~/LoginMultimedia.aspx");
            }
            if (!IsPostBack)
            {
                DataTable dt = GetUsuarios(); // sustituye "tunumerocausa" por la cadena de consulta que deseas usar.

                // vincula el datatable al dropdownlist
                juzgadoAsignado.DataSource = dt;
                juzgadoAsignado.DataTextField = "Nombre"; // columna del datatable que quieres mostrar en el dropdownlist
                juzgadoAsignado.DataValueField = "IdUsuario"; // columna del datatable que quieres usar como valor en el dropdownlist (en este caso es la misma, pero puede ser diferente si lo necesitas)
                juzgadoAsignado.DataBind();
                // opcional: añade un elemento predeterminado al inicio
                juzgadoAsignado.Items.Insert(0, new ListItem("Quien sube el video: ", ""));


            }
            if (Session["LastActivity"] != null && (DateTime.Now - (DateTime)Session["LastActivity"]).TotalMinutes > 20
                )
            {
                // Cerrar la sesión debido a inactividad
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();

                HttpCookie sessionCookie = new HttpCookie(FormsAuthentication.FormsCookieName);
                sessionCookie.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(sessionCookie);

                Response.Redirect("Login.aspx");
            }

            // Actualizar el tiempo de última actividad en cada carga de página
            Session["LastActivity"] = DateTime.Now;
          
        }

        //funcion para magistrados
        protected void ActualizarMagistrado(object sender, EventArgs e)
        {
            string email = emailMag.Text;  // Usamos .Text ahora

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM P_FUsuarios WHERE email = @Email";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@Email", email);

                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        string updateQuery = "UPDATE P_FUsuarios SET magistrado = 1 WHERE email = @Email";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@Email", email);

                        int rowsUpdated = updateCommand.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            ShowAlert("Se ha asignado como magistrado.");
                        }
                        else
                        {
                            ShowAlert("El email existe, o esta mal escrito.");
                        }
                    }
                    else
                    {
                        ShowAlert("El correo electrónico ingresado no se encontró en la base de datos.");
                    }
                }
                catch (Exception ex)
                {
                    ShowAlert("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void ShowAlert(string message)
        {
            string script = $"alert('{message}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }
        //Metodos para obtener informacion de base de datos 

        protected void btnCerrarSesion(object sender, EventArgs e)
        {
            // Cerrar la sesión del usuario actual
            Session.Clear();
            Session.Abandon();
            // Cerrar sesión en el servidor
            FormsAuthentication.SignOut();

            // Eliminar la cookie de sesión del navegador
            HttpCookie sessionCookie = new HttpCookie(FormsAuthentication.FormsCookieName);
            sessionCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(sessionCookie);
            Response.Redirect("Login.aspx");
        }

        private void GenerarInfoTableEditarVideos(string numeroCausa)


        {
            // Obtener los datos de la tabla P_Video

            DataTable dtVideos = GetInfoTableVideos(numeroCausa);

            string tablaVideos = "";

            foreach (DataRow row in dtVideos.Rows)
            {
                string tituloVideo = row["Titulo"].ToString();
                string descripcionVideo = row["Descripcion"].ToString();
                string fechaGrabacionVideo = row["FechaGrabacion"].ToString();
                string idRegAudienciasVideo = row["IdRegAudiencias"].ToString();
                string idUsuarioVideo = row["IdUsuario"].ToString();
                string videoAsignado = row["videoAsignado"].ToString();
                //string uri = row["Url"].ToString();
                string horaInicio = row["HoraInicio"].ToString();
                string horaFin = row["HoraFin"].ToString();


                // Crear el contenido HTML para cada video
                string videoAsignadoHtml;
                string tituloVideoHtml = $"<td>{tituloVideo}</td>";
                string descripcionVideoHtml = $"<td>{descripcionVideo}</td>";
                string fechaGrabacionVideoHtml = $"<td>{fechaGrabacionVideo}</td>";
                string idRegAudienciasVideoHtml = $"<td>{idRegAudienciasVideo}</td>";
                string idUsuarioVideoHtml = $"<td>{idUsuarioVideo}</td>";
                //string uriHtml = $"<td>{uri}</td>";
                string horaInicioHtml = $"<td>{horaInicio}</td>";
                string horaFinHtml = $"<td>{horaFin}</td>";
                if (videoAsignado == "True")
                {
                    videoAsignadoHtml = "<td>Asignado</td>";
                }
                else
                {
                    videoAsignadoHtml = "<td>No Asignado</td>";
                }


                // Agregar el contenido HTML del video a la tablaVideos
                tablaVideos += tituloVideoHtml + descripcionVideoHtml + fechaGrabacionVideoHtml + idRegAudienciasVideoHtml + idUsuarioVideoHtml + videoAsignadoHtml + horaInicioHtml + horaFinHtml + "<td><a href='#' class='edit' data-bs-toggle='modal' data-bs-target='#modalEditar' ><i class='material-icons' data-toggle='tooltip' title='Edit'>&#xE254;</i></a></td></tr><tr>";
            }



            // Asignar el contenido HTML generado al div donde deseas mostrarlo
            divInfoVideosEditar.InnerHtml = tablaVideos;
        }
        private void GenerarInfoTableEliminarVideos(string numeroCausa)
        {
            // Obtener los datos de la tabla P_Video

            DataTable dtVideos = GetInfoTableVideos(numeroCausa);

            string tablaVideos = "";

            foreach (DataRow row in dtVideos.Rows)
            {
                string idVideo = row["IdVideos"].ToString();
                string tituloVideo = row["Titulo"].ToString();
                string descripcionVideo = row["Descripcion"].ToString();
                string fechaGrabacionVideo = row["FechaGrabacion"].ToString();
                string idRegAudienciasVideo = row["IdRegAudiencias"].ToString();
                string idUsuarioVideo = row["IdUsuario"].ToString();
                string videoAsignado = row["videoAsignado"].ToString();
                //string uri = row["Url"].ToString();
                string horaInicio = row["HoraInicio"].ToString();
                string horaFin = row["HoraFin"].ToString();

                // Crear el contenido HTML para cada video

                string videoAsignadoHtml;




                string idVideoHtml = $"<td>{idVideo}</td>";
                string tituloVideoHtml = $"<td>{tituloVideo}</td>";
                string descripcionVideoHtml = $"<td>{descripcionVideo}</td>";
                string fechaGrabacionVideoHtml = $"<td>{fechaGrabacionVideo}</td>";
                string idRegAudienciasVideoHtml = $"<td>{idRegAudienciasVideo}</td>";
                string idUsuarioVideoHtml = $"<td>{idUsuarioVideo}</td>";
                //string uriHtml = $"<td style='width: 200px; height: 50px;  border: 1px solid #ccc; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; '>{uri}</td>";
                string horaInicioHtml = $"<td>{horaInicio}</td>";
                string horaFinHtml = $"<td>{horaFin}</td>";
                if (videoAsignado == "True")
                {
                    videoAsignadoHtml = "<td>Asignado</td>";
                }
                else
                {
                    videoAsignadoHtml = "<td>No Asignado</td>";
                }


                // Agregar el contenido HTML del video a la tablaVideos
                tablaVideos += idVideoHtml + tituloVideoHtml + descripcionVideoHtml + fechaGrabacionVideoHtml + idRegAudienciasVideoHtml + idUsuarioVideoHtml + videoAsignadoHtml + horaInicioHtml + horaFinHtml + $"</td></tr><tr> <td></tr><tr>";
                Debug.WriteLine("tu elemento esssss" + videoAsignado);

            }



            // Asignar el contenido HTML generado al div donde deseas mostrarlo
            divInfoVideosEliminar.InnerHtml = tablaVideos;
        }

        protected void BtnEliminarIdVideo_Click(object sender, EventArgs e)
        {
            try
            {

                int IdVideo = Convert.ToInt32(inputIdEliminarVideo.Value);
                EliminarVideoById(IdVideo);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Peticion exitosa!, Recarga tu sitio para verificar');", true);
                inputIdEliminarVideo.Value = "";



            }
            catch (FormatException)
            {
                // Capturar excepción si la conversión no es válida (no es un número)
                // Aquí puedes mostrar un mensaje de error o realizar otra acción adecuada.
                // Por ejemplo:
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, ingresa un ID válido para eliminar el video.');", true);
            }
            catch (Exception ex)
            {
                // Capturar otras excepciones no esperadas
                // Puedes mostrar un mensaje de error o realizar otra acción adecuada.
                // Por ejemplo:
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error: " + ex.Message + "');", true);
            }
        }


        private void InsertarDataVideos(string titulo, string url, string descripcion, string idRegAudiencias, string idUsuario, string videoAsignado, DateTime horaIncio, DateTime horaFin)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string consulta = @"IF NOT EXISTS(SELECT 1 FROM P_Video WHERE IdRegAudiencias = @IdRegAudiencias) BEGIN INSERT INTO P_Video(Titulo, Url, Descripcion, FechaGrabacion, IdRegAudiencias, IdUsuario, VideoAsignado, HoraInicio, HoraFin) VALUES(@Titulo, @Url, @Descripcion, GETDATE(), @IdRegAudiencias, @IdUsuario, @VideoAsignado, @HoraInicio, @HoraFin); END; ";

                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {

                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                    cmd.Parameters.AddWithValue("@Url", url);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);

                    cmd.Parameters.AddWithValue("@IdRegAudiencias", idRegAudiencias);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@VideoAsignado", videoAsignado);
                    cmd.Parameters.AddWithValue("@HoraInicio", horaIncio);
                    cmd.Parameters.AddWithValue("@HoraFin", horaFin);

                    int rowsAffected = cmd.ExecuteNonQuery(); // Ejecutar la consulta de inserción

                    if (rowsAffected > 0)
                    {
                        Debug.WriteLine("Datos insertados correctamente, .");
                    }
                    else
                    {
                        Debug.WriteLine("No se pudieron insertar los datos.");
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(' No se pudieron insertar los datos .');", true);
                    }
                }
            }
        }


        public void EliminarVideoById(int idEliminarVideo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string consulta = @"DELETE FROM P_Video WHERE IdVideos = @IdVideo";

                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {
                    cmd.Parameters.AddWithValue("@IdVideo", idEliminarVideo);

                    int rowsAffected = cmd.ExecuteNonQuery(); // Ejecutar la consulta de eliminación

                    if (rowsAffected > 0)
                    {
                        Debug.WriteLine($"Se eliminó correctamente el video con IdVideos: {idEliminarVideo}");
                    }
                    else
                    {
                        Debug.WriteLine($"No se encontró ningún video con IdVideos: {idEliminarVideo}, no se realizó la eliminación.");
                    }
                }
            }
        }

        private DataTable GetInfoTableVideos(string numeroCausa)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string consulta = @"SELECT [IdVideos], [Titulo], [Url], [Descripcion], [FechaGrabacion], a.[IdRegAudiencias], [IdUsuario], [videoAsignado], [HoraInicio], [HoraFin] FROM P_Video a INNER JOIN P_ListaAudiencias b ON a.IdRegAudiencias = b.IdRegAudiencia WHERE b.Causa = @NumeroCausa;";

                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {
                    cmd.Parameters.AddWithValue("@NumeroCausa", numeroCausa);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            // Mostrar los datos en la consola de salida
            //Debug.WriteLine("Contenido del DataTable:");
            //foreach (DataRow row in dt.Rows)
            //{
            //    Debug.WriteLine("Titulo: " + row["Titulo"].ToString());
            //    Debug.WriteLine("Descripcion: " + row["Descripcion"].ToString());
            //    Debug.WriteLine("FechaGrabacion: " + row["FechaGrabacion"].ToString());
            //    Debug.WriteLine("IdRegAudiencias: " + row["IdRegAudiencias"].ToString());
            //    Debug.WriteLine("IdUsuario: " + row["IdUsuario"].ToString());
            //    Debug.WriteLine("videoAsignado: " + row["videoAsignado"].ToString());

            //}

            return dt;
        }
        private DataTable GetAudienciasPorNumeroCausa(string numeroCausa)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string consulta = @"
                    SELECT ta.TipoAudi AS Tipo, la.SalaDescrip, la.FechaAudi, la.FechaCap
                    FROM P_ListaAudiencias la
                    INNER JOIN P_CatTipoAudiencias ta ON la.IdTipoAudi = ta.IdTipoAudi
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

        private DataTable GetPartesPorNumeroCausa(string numeroCausa)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string consulta = @"
                    SELECT pe.Nombre, pe.APaterno, pe.AMaterno, pe.Genero, pe.Tipo
                    FROM S_PartesExpe pe
                    INNER JOIN S_Expediente e ON pe.IdExpediente = e.IdExpediente
                    WHERE e.NumExpediente = @NumeroCausa";

                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {
                    cmd.Parameters.AddWithValue("@NumeroCausa", numeroCausa);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        private DataTable GetDelitosPorNumeroCausa(string numeroCausa)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string consulta = @"
                    SELECT ss.Nombre
                    FROM S_SubSerie ss
                    INNER JOIN S_ExpSubSeries ess ON ss.IdSubSerie = ess.IdSubSerie
                    INNER JOIN S_Expediente e ON ess.IdExpediente = e.IdExpediente
                    WHERE e.NumExpediente = @NumeroCausa";

                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {
                    cmd.Parameters.AddWithValue("@NumeroCausa", numeroCausa);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        private DataTable GetTipoAudiencia(string numeroCausa)
        {

            DataTable dt = new DataTable();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string consulta = @"SELECT b.TipoAudi, a.SalaDescrip, a.IdRegAudiencia FROM P_ListaAudiencias a JOIN P_CatTipoAudiencias b ON a.IdTipoAudi = b.IdTipoAudi WHERE a.Causa = @NumeroCausa";

                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {
                    cmd.Parameters.AddWithValue("@NumeroCausa", numeroCausa);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }


            return dt;
        }


        private void GenerarTablasPartes(DataTable dtPartes)
        {
            string tablaVictimas = "<div class=\"col-sm-12 col-md-6 col-lg-4 \">" +
                                   "<table class=\"table tablaDatosGeneral\" >" +
                                   "<thead><tr><th class=\"thDelitos\">Victima(s)</th></tr></thead>" +
                                   "<tbody>";

            string tablaImputados = "<div class=\"col-sm-12 col-md-6 col-lg-4\">" +
                                    "<table class=\"table tablaDatosGeneral\">" +
                                    "<thead><tr><th class=\"thDelitos\">Imputado(s)</th></tr></thead>" +
                                    "<tbody>";

            foreach (DataRow row in dtPartes.Rows)
            {
                string nombre = row["Nombre"].ToString();
                string apaterno = row["APaterno"].ToString();
                string amaterno = row["AMaterno"].ToString();
                string genero = row["Genero"].ToString();
                string tipo = row["Tipo"].ToString();

                string parteHtml = $"<tr><td>{nombre} {apaterno} {amaterno} - {genero}</td></tr>";

                if (tipo == "O")
                {
                    tablaVictimas += parteHtml;
                }
                else if (tipo == "I")
                {
                    tablaImputados += parteHtml;
                }
            }

            tablaVictimas += "</tbody></table></div>";
            tablaImputados += "</tbody></table></div>";

            divVictimas.InnerHtml = tablaVictimas;
            divImputados.InnerHtml = tablaImputados;
        }

        private void GenerarTablaDelitos(DataTable dtDelitos)
        {
            string tablaDelitos = "<div class=\"col-sm-12 col-md-6 col-lg-4\">" +
                                  "<table class=\"table tablaDatosGeneral\">" +
                                  "<thead><tr><th class=\"thDelitos\">Delito(s)</th></tr></thead>" +
                                  "<tbody>";

            foreach (DataRow row in dtDelitos.Rows)
            {
                string nombreDelito = row["Nombre"].ToString();

                string delitoHtml = $"<tr><td>{nombreDelito}</td></tr>";

                tablaDelitos += delitoHtml;
            }

            tablaDelitos += "</tbody></table></div>";

            divDelitos.InnerHtml = tablaDelitos;
        }

        protected void gridViewInfoVideos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                DateTime fechaAudi = (DateTime)rowView["FechaAudi"];
                DateTime fechaCap = (DateTime)rowView["FechaCap"];

                Label lblFechaAudi = (Label)e.Row.FindControl("lblFechaAudi");
                Label lblFechaCap = (Label)e.Row.FindControl("lblFechaCap");

                lblFechaAudi.Text = fechaAudi.ToString("dd/MM/yy HH:mm");
                lblFechaCap.Text = fechaCap.ToString("dd/MM/yyyy HH:mm");
            }

        }

        protected void ddlTipoAudiencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlTipoAudiencia = (DropDownList)sender;
            tipoAudiSeleccionado = ddlTipoAudiencia.SelectedValue;
            // Obtener númeroCausa de algún lugar (puede ser a través de inputNumeroCausa.Value u otro método)
            numeroCausa = inputNumeroCausa.Value;
            DropDownList juzgadoAsignado = (DropDownList)sender;
            tipoAudiSeleccionado = juzgadoAsignado.SelectedValue;
            // Obtener númeroCausa de algún lugar (puede ser a través de inputNumeroCausa.Value u otro método)
            numeroCausa = inputNumeroCausa.Value;


        }


        //añadir video


        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            string numeroCausa = inputNumeroCausa.Value;
            //variable a ocupar despues
            Session["NumeroCausa"] = numeroCausa;


            DataTable dtAudiencias = GetAudienciasPorNumeroCausa(numeroCausa);
            DataTable dtPartes = GetPartesPorNumeroCausa(numeroCausa);
            DataTable dtDelitos = GetDelitosPorNumeroCausa(numeroCausa);
            DataTable dtTipoAudiencia = GetTipoAudiencia(numeroCausa);

            DataTable dtsala = GetTipoAudiencia(numeroCausa);
            //GetInfoTableVideos();


            gridViewAudiencias.DataSource = dtAudiencias;
            gridViewAudiencias.DataBind();


            GenerarTablasPartes(dtPartes);
            GenerarTablaDelitos(dtDelitos);
            GuardarVariableNumeroCausa();
            GetTipoAudienciaByNumeroCausa(numeroCausa);

            //GetInfoTableVideos();
            GenerarInfoTableEditarVideos(numeroCausa);
            GenerarInfoTableEliminarVideos(numeroCausa);
            //GenerarSalaYFecha(dtsala);

            Session["NombreJ"] = juzgadoAsignado.SelectedValue;
            string MPidRegAudi = Session["NombreJ"] as string;
            Debug.WriteLine("Número selected: " + MPidRegAudi);
        }




        protected void btnIdRegAudiencia(object sender, EventArgs e)
        {
            try
            {
                string titulo = tvdTitulo.Value;
                string url = tvdUrl.Value;
                string descripcion = tvdDescripcion.Value;
                DateTime fechaGrabacion = DateTime.Parse(tvdFechaGrabacion.Value);
                DateTime horaInicio = DateTime.Parse(tvdHoraInicio.Value);
                DateTime horaFin = DateTime.Parse(tvdHoraFin.Value);

                string idRegAudi = Session["TipoAudienciaSeleccionado"] as string;
                string selectedItemText = ddlTipoAudiencia.SelectedItem.Text;
                string numerocausaG = Session["numerocausa"] as string;
                string idRegAudiString = Session["TipoAudienciaSeleccionado"] as string;

                GetIdRegAudi(numerocausaG, selectedItemText);
                string selectedjuzgadoItem = juzgadoAsignado.SelectedValue;
                string MPidRegAudi = Session["IdTipoAudi"] as string;

                CreateVideo(titulo, url, descripcion, fechaGrabacion, MPidRegAudi, selectedjuzgadoItem, horaInicio, horaFin);

                tvdTitulo.Value = string.Empty;
                tvdUrl.Value = string.Empty;
                tvdDescripcion.Value = string.Empty;
                tvdFechaGrabacion.Value = string.Empty;
                tvdHoraInicio.Value = string.Empty;
                tvdHoraFin.Value = string.Empty;
            }
            catch (Exception ex)
            {
                // Mostrar una alerta de error utilizando JavaScript
                string errorScript = "alert('Ocurrió un error al procesar el formulario vuelva a intentarlo ');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", errorScript, true);
            }
        }


        protected void GuardarVariableNumeroCausa()
        {
            string numerocausa = Session["numerocausa"] as string;


            Debug.WriteLine("variable en memoria essssss: " + numerocausa);

            DataTable dt = GetTipoAudienciaByNumeroCausa(numerocausa); // sustituye "tunumerocausa" por la cadena de consulta que deseas usar.

            // vincula el datatable al dropdownlist
            ddlTipoAudiencia.DataSource = dt;
            ddlTipoAudiencia.DataTextField = "tipoaudi"; // columna del datatable que quieres mostrar en el dropdownlist
            ddlTipoAudiencia.DataValueField = "tipoaudi"; // columna del datatable que quieres usar como valor en el dropdownlist (en este caso es la misma, pero puede ser diferente si lo necesitas)
            ddlTipoAudiencia.DataBind();
            // opcional: añade un elemento predeterminado al inicio
            ddlTipoAudiencia.Items.Insert(0, new ListItem("selecciona un tipo de audiencia", ""));
            //debug.writeline("valor de tipo de audiencias es:" + ddltipoaudiencia.selecteditem.text);
            //session["selectedtipoaudi"] = ddltipoaudiencia.selectedvalue;
            string selecteditemtext = ddlTipoAudiencia.SelectedValue;
            //debug.writeline("texto seleccionado de tipo de audiencias es: " + selecteditemtext);

            Session["selectedtipoaudi"] = ddlTipoAudiencia.SelectedValue;

        }





        private DataTable GetTipoAudienciaByNumeroCausa(string numeroCausa)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //string numeroCausa = inputNumeroCausa.Value; // Obtén el valor del input
                Session["NumeroCausa"] = numeroCausa; // Almacena en la sesión

                //string tipoAudiSeleccionado = (string)Session["TipoAudienciaSeleccionado"];
                string consulta = @"
                    SELECT b.TipoAudi 
                    FROM P_ListaAudiencias a 
                    JOIN P_CatTipoAudiencias b ON a.IdTipoAudi = b.IdTipoAudi 
                    WHERE a.Causa = @NumeroCausa 
                    AND a.IdRegAudiencia NOT IN (SELECT IdRegAudiencias FROM P_Video)";


                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {
                    cmd.Parameters.AddWithValue("@NumeroCausa", numeroCausa);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            // Mostrar los datos en la consola de salida
            Console.WriteLine("Contenido del DataTable:");
            foreach (DataRow row in dt.Rows)
            {
                Debug.WriteLine("TipoAudi: " + row["TipoAudi"].ToString());


            }

            return dt;
        }

        protected void BtnUpdateInfoVideo_Click(object sender, EventArgs e)
        {
            try
            {
                string columna = dvColumna.Value;
                string txtdato = inputUpdateVideo.Value;
                int IdUpdateVideo = Convert.ToInt32(inputIdUpdateVideo.Value);

                UpdateInfoVideoAudiencias(columna, txtdato, IdUpdateVideo);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Los datos fueron cambiados.');", true);

                // Limpiar los valores de los elementos después de la petición
                dvColumnaSelected.ClearSelection(); // Limpiar la selección del DropDownList
                dvColumna.Value = ""; // Limpiar el valor oculto
                inputUpdateVideo.Value = ""; // Limpiar el valor del input
                inputIdUpdateVideo.Value = ""; // Limpiar el valor del input
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Verifica si tu peticion fue modificada...');", true);
            }
            catch (FormatException)
            {
                // Capturar excepción si la conversión no es válida (no es un número)
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, ingresa un ID válido.');", true);
            }
            catch (Exception ex)
            {
                // Capturar otras excepciones no esperadas
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error: " + ex.Message + "');", true);
            }
        }



        //Session["IdUser"] = row["IdUser"].ToString();
        //Session["Nomr"] = row["Nombre"].ToString();


        private DataTable GetUsuarios()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                string consulta = @"SELECT IdUsuario ,Nombre FROM P_Usuarios";

                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            // Mostrar los datos en la consola de salida
            Console.WriteLine("Contenido del DataTable:");
            foreach (DataRow row in dt.Rows)
            {
                Session["idUsuariosP"] = row["IdUsuario"].ToString();
                Session["NombreP"] = row["Nombre"].ToString();
                //string usuariosP = Session["idUsuariosP"] as string;
                //string salaDescrip = Session["NombreP"] as string;
                //Debug.WriteLine("Datos extraidos: " + usuariosP + salaDescrip);
            }

            return dt;
        }



        private DataTable GetIdRegAudi(string numeroCausa, string tipoAudiSeleccionado)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //string numeroCausa = inputNumeroCausa.Value; // Obtén el valor del input
                Session["NumeroCausa"] = numeroCausa; // Almacena en la sesión
                Session["TipoAudienciaSeleccionado"] = tipoAudiSeleccionado;
                //string tipoAudiSeleccionado = (string)Session["TipoAudienciaSeleccionado"];
                string consulta = @"SELECT a.SalaDescrip,  a.IdRegAudiencia FROM P_ListaAudiencias a JOIN P_CatTipoAudiencias b ON a.IdTipoAudi = b.IdTipoAudi WHERE a.Causa = @NumeroCausa AND b.TipoAudi = @TipoAudi";

                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {
                    cmd.Parameters.AddWithValue("@NumeroCausa", numeroCausa);
                    cmd.Parameters.AddWithValue("@TipoAudi", tipoAudiSeleccionado);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            // Mostrar los datos en la consola de salida
            Console.WriteLine("Contenido del DataTable:");
            foreach (DataRow row in dt.Rows)
            {
                Session["IdTipoAudi"] = row["IdRegAudiencia"].ToString();


            }

            return dt;
        }



        public void UpdateInfoVideoAudiencias(string columna, string txtdato, int IdUpdateVideo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string consulta = $"UPDATE P_Video SET {columna} = @txtdato WHERE IdVideos = @IdVideo;";

                using (SqlCommand command = new SqlCommand(consulta, connection))
                {
                    command.Parameters.AddWithValue("@txtdato", txtdato);
                    command.Parameters.AddWithValue("@IdVideo", IdUpdateVideo);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Mostrar un mensaje de éxito en la interfaz de usuario
                        System.Diagnostics.Debug.WriteLine("Actualización exitosa: " + rowsAffected + " fila(s) afectada(s).");
                    }
                    else
                    {
                        // Mostrar un mensaje de que no se encontró el registro
                        System.Diagnostics.Debug.WriteLine("No se encontró el registro con el ID proporcionado.");
                    }
                }
            }
        }
        public void CreateVideo(string txtTitulo, string txtUrl, string txtDescripcion, DateTime txtFechaGrabacion, string idRegAudiencia, string txtIdUsuario, DateTime txtHoraInicio, DateTime txtHoraFin)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string consulta = @"IF NOT EXISTS (SELECT 1 FROM P_Video WHERE IdRegAudiencias = @IdRegAudiencias) BEGIN INSERT INTO P_Video (Titulo, Url, Descripcion, FechaGrabacion, IdRegAudiencias, IdUsuario, VideoAsignado, HoraInicio, HoraFin) VALUES (@Titulo, @Url, @Descripcion, @FechaGrabacion, @IdRegAudiencias, @IdUsuario, 'False', @HoraInicio, @HoraFin) END";

                try
                {
                    using (SqlCommand command = new SqlCommand(consulta, connection))
                    {
                        command.Parameters.AddWithValue("@Titulo", txtTitulo);
                        command.Parameters.AddWithValue("@Url", txtUrl);
                        command.Parameters.AddWithValue("@Descripcion", txtDescripcion);
                        command.Parameters.AddWithValue("@FechaGrabacion", txtFechaGrabacion);
                        command.Parameters.AddWithValue("@IdRegAudiencias", idRegAudiencia);
                        command.Parameters.AddWithValue("@IdUsuario", txtIdUsuario);
                        command.Parameters.AddWithValue("@HoraInicio", txtHoraInicio);
                        command.Parameters.AddWithValue("@HoraFin", txtHoraFin);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, Verifica si la petición fue correcta');", true);
                        }
                        else
                        {
                            Debug.WriteLine("La consulta no afectó ninguna fila.");
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No afectó ninguna fila, no puedes añadir un video con un numero de expediente igual. De ser a si primero debes eliminarlo');", true);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // Capturar excepciones específicas de SQL
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error de SQL: " + ex.Message + "');", true);
                }
                catch (Exception ex)
                {
                    // Capturar otras excepciones no esperadas
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error: " + ex.Message + "');", true);
                }


                // Resto del código
                Debug.WriteLine("Valores pasados al método CreateVideo:");

                Debug.WriteLine("titulo: " + txtTitulo);
                Debug.WriteLine("url: " + txtUrl);
                Debug.WriteLine("descripcion: " + txtDescripcion);
                Debug.WriteLine("fechaGrabacion: " + txtFechaGrabacion);
                Debug.WriteLine("juzgadoItem: " + idRegAudiencia);
                Debug.WriteLine("idUsuario: " + txtIdUsuario);
                Debug.WriteLine("horaInicio: " + txtHoraInicio);
                Debug.WriteLine("horaFin: " + txtHoraFin);
            }
        }

    }
}

