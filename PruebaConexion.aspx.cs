using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KawaSRL
{
    public partial class PruebaConexion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cadena = ConfigurationManager
                    .ConnectionStrings["KawaSRLConnectionString"]
                    .ConnectionString;

                using (SqlConnection conexion = new SqlConnection(cadena))
                {
                    try
                    {
                        conexion.Open();
                        Response.Write("¡Conexión exitosa con KawaSRL_DB!");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error de conexión: " + ex.Message);
                    }
                }
            }

        }
    }
}