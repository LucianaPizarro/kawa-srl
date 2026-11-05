using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace KawaSRL
{
    public partial class EnviosOrdenCompra : System.Web.UI.Page
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
                CargarOrdenesCompra();
                CargarEnviosRegistrados();
            }
        }

        private void CargarOrdenesCompra()
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
                    AND (
                        NOT EXISTS (
                            SELECT 1
                            FROM EnvioOrdenCompra e
                            WHERE e.idOrdenCompra = oc.idOrdenCompra
                              AND e.medioEnvio = 'Correo privado'
                        )
                        OR
                        NOT EXISTS (
                            SELECT 1
                            FROM EnvioOrdenCompra e
                            WHERE e.idOrdenCompra = oc.idOrdenCompra
                              AND e.medioEnvio = 'Fax'
                        )
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
                new ListItem("Seleccione una Orden de Compra", ""));
        }

        private void CargarEnviosRegistrados()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = @"
                    SELECT
                        e.idEnvio,
                        e.idOrdenCompra,
                        p.razonSocial AS Proveedor,
                        e.medioEnvio,
                        e.fechaEnvio,
                        e.referencia,
                        e.observaciones
                    FROM EnvioOrdenCompra e
                    INNER JOIN OrdenCompra oc
                        ON e.idOrdenCompra = oc.idOrdenCompra
                    INNER JOIN NotaPedido np
                        ON oc.idNotaPedido = np.idNotaPedido
                    INNER JOIN Proveedor p
                        ON np.idProveedor = p.idProveedor
                    ORDER BY e.fechaEnvio DESC";

                using (SqlDataAdapter adaptador =
                    new SqlDataAdapter(consulta, conexion))
                {
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    gvEnvios.DataSource = tabla;
                    gvEnvios.DataBind();
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

            if (string.IsNullOrWhiteSpace(ddlMedioEnvio.SelectedValue))
            {
                lblMensaje.Text = "Seleccione el medio de envío.";
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
                lblMensaje.Text = "La fecha de envío no es válida.";
                return;
            }

            int idOrdenCompra = Convert.ToInt32(ddlOrdenCompra.SelectedValue);

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = @"
                    INSERT INTO EnvioOrdenCompra
                    (idOrdenCompra, medioEnvio, fechaEnvio, referencia, observaciones)
                    SELECT
                        @idOrdenCompra,
                        @medioEnvio,
                        @fechaEnvio,
                        @referencia,
                        @observaciones
                    WHERE EXISTS (
                        SELECT 1
                        FROM OrdenCompra
                        WHERE idOrdenCompra = @idOrdenCompra
                          AND estado = 'Emitida'
                    )
                    AND NOT EXISTS (
                        SELECT 1
                        FROM EnvioOrdenCompra
                        WHERE idOrdenCompra = @idOrdenCompra
                          AND medioEnvio = @medioEnvio
                    )";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.Add("@idOrdenCompra", SqlDbType.Int).Value =
                        idOrdenCompra;

                    comando.Parameters.Add("@medioEnvio", SqlDbType.VarChar, 30).Value =
                        ddlMedioEnvio.SelectedValue;

                    comando.Parameters.Add("@fechaEnvio", SqlDbType.Date).Value =
                        fechaEnvio.Date;

                    comando.Parameters.Add("@referencia", SqlDbType.VarChar, 100).Value =
                        string.IsNullOrWhiteSpace(txtReferencia.Text)
                        ? (object)DBNull.Value
                        : txtReferencia.Text.Trim();

                    comando.Parameters.Add("@observaciones", SqlDbType.VarChar, 500).Value =
                        string.IsNullOrWhiteSpace(txtObservaciones.Text)
                        ? (object)DBNull.Value
                        : txtObservaciones.Text.Trim();

                    conexion.Open();

                    int filas = comando.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        lblMensaje.Text = "Envío registrado correctamente.";

                        txtFechaEnvio.Text = "";
                        txtReferencia.Text = "";
                        txtObservaciones.Text = "";
                        ddlMedioEnvio.SelectedIndex = 0;

                        CargarOrdenesCompra();
                        CargarEnviosRegistrados();
                    }
                    else
                    {
                        lblMensaje.Text =
                            "No se pudo registrar el envío. Verifique que la orden esté emitida y que no exista un envío por ese medio.";
                    }
                }
            }
        }
    }
}