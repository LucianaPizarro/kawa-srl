using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace KawaSRL
{
    public partial class TalonesEnvio : System.Web.UI.Page
    {
        private string cadenaConexion =
            ConfigurationManager.ConnectionStrings["KawaSRLConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarOrdenesDisponibles();
                CargarTalonesRegistrados();
            }
        }

        private void CargarOrdenesDisponibles()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = @"
            SELECT
                oc.idOrdenCompra,
                'Orden N.º ' + CAST(oc.idOrdenCompra AS VARCHAR(10))
                + ' - ' + p.razonSocial AS descripcion
            FROM OrdenCompra oc
            INNER JOIN NotaPedido np
                ON oc.idNotaPedido = np.idNotaPedido
            INNER JOIN Proveedor p
                ON np.idProveedor = p.idProveedor
            WHERE oc.estado = 'Emitida'
            AND NOT EXISTS (
                SELECT 1
                FROM TalonEnvio te
                WHERE te.idOrdenCompra = oc.idOrdenCompra
            )
            ORDER BY oc.idOrdenCompra";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    conexion.Open();

                    ddlOrdenCompra.DataSource = comando.ExecuteReader();
                    ddlOrdenCompra.DataTextField = "descripcion";
                    ddlOrdenCompra.DataValueField = "idOrdenCompra";
                    ddlOrdenCompra.DataBind();
                }
            }

            ddlOrdenCompra.Items.Insert(0,
                new System.Web.UI.WebControls.ListItem(
                    "Seleccione una Orden de Compra", ""));
        }
        private void CargarTalonesRegistrados()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = @"
                    SELECT
                        te.idTalonEnvio,
                        te.idOrdenCompra,
                        te.numeroTalon,
                        te.fechaEnvio,
                        te.empresaCorreo,
                        te.observaciones
                    FROM TalonEnvio te
                    ORDER BY te.fechaEnvio DESC";

                using (SqlDataAdapter adaptador =
                    new SqlDataAdapter(consulta, conexion))
                {
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    gvTalones.DataSource = tabla;
                    gvTalones.DataBind();
                }
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ddlOrdenCompra.SelectedValue))
            {
                lblMensaje.Text = "Seleccione una Orden de Compra.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumeroTalon.Text))
            {
                lblMensaje.Text = "Ingrese el número de talón.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFechaEnvio.Text))
            {
                lblMensaje.Text = "Ingrese la fecha de envío.";
                return;
            }

            DateTime fechaEnvio;

            if (!DateTime.TryParse(txtFechaEnvio.Text, out fechaEnvio))
            {
                lblMensaje.Text = "La fecha ingresada no es válida.";
                return;
            }

            int idOrdenCompra = Convert.ToInt32(ddlOrdenCompra.SelectedValue);

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = @"
            INSERT INTO TalonEnvio
            (idOrdenCompra, numeroTalon, fechaEnvio, empresaCorreo, observaciones)
            SELECT @idOrdenCompra, @numeroTalon, @fechaEnvio, @empresaCorreo, @observaciones
            WHERE EXISTS (
                SELECT 1
                FROM OrdenCompra
                WHERE idOrdenCompra = @idOrdenCompra
                  AND estado = 'Emitida'
            )
            AND NOT EXISTS (
                SELECT 1
                FROM TalonEnvio
                WHERE idOrdenCompra = @idOrdenCompra
            )";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.Add("@idOrdenCompra", SqlDbType.Int).Value = idOrdenCompra;
                    comando.Parameters.Add("@numeroTalon", SqlDbType.VarChar, 50).Value = txtNumeroTalon.Text.Trim();
                    comando.Parameters.Add("@fechaEnvio", SqlDbType.Date).Value = fechaEnvio.Date;

                    comando.Parameters.Add("@empresaCorreo", SqlDbType.VarChar, 100).Value =
                        string.IsNullOrWhiteSpace(txtEmpresaCorreo.Text)
                        ? (object)DBNull.Value
                        : txtEmpresaCorreo.Text.Trim();

                    comando.Parameters.Add("@observaciones", SqlDbType.VarChar, 500).Value =
                        string.IsNullOrWhiteSpace(txtObservaciones.Text)
                        ? (object)DBNull.Value
                        : txtObservaciones.Text.Trim();

                    conexion.Open();

                    int filas = comando.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        lblMensaje.Text = "Talón de envío registrado correctamente.";

                        txtNumeroTalon.Text = "";
                        txtFechaEnvio.Text = "";
                        txtEmpresaCorreo.Text = "";
                        txtObservaciones.Text = "";

                        CargarOrdenesDisponibles();
                        CargarTalonesRegistrados();
                    }
                    else
                    {
                        lblMensaje.Text = "No se pudo registrar el talón. Verifique que la orden esté emitida y no tenga otro talón.";
                    }
                }
            }
        }
    }
}