using System;
using System.Data.SqlClient;

namespace FEJEHAudiencias
{
    public partial class Videos : System.Web.UI.Page
    {
        protected string VideoUrl { get; set; }
        protected string TipoVideo { get; set; }
        protected string FechaAudiencia { get; set; }
        protected string Juez { get; set; }
        protected string HoraInicio { get; set; }
        protected string HoraFin { get; set; }
        protected string DescripcionVideo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VideoUrl = SanitizeVideoUrl(Request.QueryString["videoUrl"]);

                // Obtener datos adicionales de la base de datos y asignar a las propiedades
                int idRegAudiencia = ObtenerIdRegAudiencia(VideoUrl);

                if (idRegAudiencia != 0)
                {
                    string connectionString = "Data Source=BRAYANPC;Initial Catalog=Acusatorio;Integrated Security=True;";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Obtener información del video relacionado con el IdRegAudiencia
                        string queryVideo = "SELECT Titulo, HoraInicio, HoraFin, Descripcion FROM P_Video WHERE Url = @VideoUrl";

                        using (SqlCommand cmdVideo = new SqlCommand(queryVideo, connection))
                        {
                            cmdVideo.Parameters.AddWithValue("@VideoUrl", VideoUrl);

                            using (SqlDataReader reader = cmdVideo.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    TipoVideo = reader["Titulo"].ToString();
                                    HoraInicio = reader["HoraInicio"].ToString();
                                    HoraFin = reader["HoraFin"].ToString();
                                    DescripcionVideo = reader["Descripcion"].ToString();
                                }

                                reader.Close();
                            }
                        }

                        // Obtener la FechaAudiencia relacionada con el IdRegAudiencia
                        string queryFechaAudiencia = "SELECT FechaAudi FROM P_ListaAudiencias WHERE IdRegAudiencia = @IdRegAudiencia";

                        using (SqlCommand cmdFechaAudiencia = new SqlCommand(queryFechaAudiencia, connection))
                        {
                            cmdFechaAudiencia.Parameters.AddWithValue("@IdRegAudiencia", idRegAudiencia);

                            using (SqlDataReader fechaAudienciaReader = cmdFechaAudiencia.ExecuteReader())
                            {
                                if (fechaAudienciaReader.Read())
                                {
                                    FechaAudiencia = fechaAudienciaReader["FechaAudi"].ToString();
                                }

                                fechaAudienciaReader.Close();
                            }
                        }

                        // Obtener el nombre del juez asociado al IdRegAudiencia
                        string queryJuez = @"
                            SELECT PatJuez, MatJuez, NomJuez
                            FROM P_Causa
                            WHERE IdExpediente = (SELECT IdCausa FROM P_ListaAudiencias WHERE IdRegAudiencia = @IdRegAudiencia)";

                        using (SqlCommand cmdJuez = new SqlCommand(queryJuez, connection))
                        {
                            cmdJuez.Parameters.AddWithValue("@IdRegAudiencia", idRegAudiencia);

                            using (SqlDataReader juezReader = cmdJuez.ExecuteReader())
                            {
                                if (juezReader.Read())
                                {
                                    Juez = $"{juezReader["PatJuez"]} {juezReader["MatJuez"]} {juezReader["NomJuez"]}";
                                }

                                juezReader.Close();
                            }
                        }
                    }
                }
            }
        }

        protected int ObtenerIdRegAudiencia(string videoUrl)
        {
            int idRegAudiencia = 0;

            string connectionString = "Data Source=BRAYANPC;Initial Catalog=Acusatorio;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryIdRegAudiencia = "SELECT IdRegAudiencias FROM P_Video WHERE Url = @VideoUrl";

                using (SqlCommand cmdIdRegAudiencia = new SqlCommand(queryIdRegAudiencia, connection))
                {
                    cmdIdRegAudiencia.Parameters.AddWithValue("@VideoUrl", videoUrl);

                    object idRegAudienciaObj = cmdIdRegAudiencia.ExecuteScalar();

                    if (idRegAudienciaObj != null && idRegAudienciaObj != DBNull.Value)
                    {
                        idRegAudiencia = Convert.ToInt32(idRegAudienciaObj);
                    }
                }
            }

            return idRegAudiencia;
        }

        public string SanitizeVideoUrl(string videoUrl)
        {
            // Validar la URL y sanitarla si es necesario
            return videoUrl;
        }
    }
}
