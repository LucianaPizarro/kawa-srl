using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace KawaSRL
{
    public partial class OrdenesCompra : System.Web.UI.Page
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
                CargarNotasAprobadas();
                CargarOrdenesCompra();
            }
        }

        private void CargarNotasAprobadas()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = @"
                    SELECT NP.idNotaPedido,
                           'Nota N° ' + CAST(NP.idNotaPedido AS VARCHAR(10))
                           + ' - ' + P.razonSocial AS descripcion
                    FROM NotaPedido NP
                    INNER JOIN Proveedor P
                        ON NP.idProveedor = P.idProveedor
                    WHERE NP.estado = 'Aprobada'
                      AND NP.fechaAprobacion IS NOT NULL
                      AND NOT EXISTS (
                          SELECT 1
                          FROM OrdenCompra OC
                          WHERE OC.idNotaPedido = NP.idNotaPedido
                      )
                    ORDER BY NP.idNotaPedido";

                SqlDataAdapter adaptador =
                    new SqlDataAdapter(consulta, conexion);

                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                ddlNotasPedido.DataSource = tabla;
                ddlNotasPedido.DataTextField = "descripcion";
                ddlNotasPedido.DataValueField = "idNotaPedido";
                ddlNotasPedido.DataBind();

                ddlNotasPedido.Items.Insert(
                    0, new System.Web.UI.WebControls.ListItem(
                        "-- Seleccione una Nota de Pedido --", ""));
            }
        }

        private void CargarOrdenesCompra()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = @"
                    SELECT OC.idOrdenCompra,
                           OC.idNotaPedido,
                           OC.fechaEmision,
                           OC.estado,
                           P.razonSocial
                    FROM OrdenCompra OC
                    INNER JOIN NotaPedido NP
                        ON OC.idNotaPedido = NP.idNotaPedido
                    INNER JOIN Proveedor P
                        ON NP.idProveedor = P.idProveedor
                    ORDER BY OC.idOrdenCompra DESC";

                SqlDataAdapter adaptador =
                    new SqlDataAdapter(consulta, conexion);

                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                gvOrdenes.DataSource = tabla;
                gvOrdenes.DataBind();
            }
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlNotasPedido.SelectedValue))
            {
                lblMensaje.Text = "Seleccione una Nota de Pedido.";
                return;
            }

            int idNotaPedido = Convert.ToInt32(ddlNotasPedido.SelectedValue);

            string connectionString =
                ConfigurationManager.ConnectionStrings["KawaSRLConnectionString"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        // Verificar que la Nota de Pedido esté aprobada.
                        string consultaValidacion = @"
                    SELECT COUNT(*)
                    FROM NotaPedido
                    WHERE idNotaPedido = @idNotaPedido
                    AND estado = 'Aprobada'
                    AND fechaAprobacion IS NOT NULL";

                        SqlCommand comandoValidacion =
                            new SqlCommand(consultaValidacion, conexion, transaccion);

                        comandoValidacion.Parameters.AddWithValue("@idNotaPedido", idNotaPedido);

                        int notaAprobada = Convert.ToInt32(comandoValidacion.ExecuteScalar());

                        if (notaAprobada == 0)
                        {
                            transaccion.Rollback();
                            lblMensaje.Text = "La Nota de Pedido no está aprobada.";
                            return;
                        }

                        // Verificar que tenga detalles.
                        string consultaDetalles = @"
                    SELECT COUNT(*)
                    FROM DetalleNotaPedido
                    WHERE idNotaPedido = @idNotaPedido";

                        SqlCommand comandoDetalles =
                            new SqlCommand(consultaDetalles, conexion, transaccion);

                        comandoDetalles.Parameters.AddWithValue("@idNotaPedido", idNotaPedido);

                        int cantidadDetalles = Convert.ToInt32(comandoDetalles.ExecuteScalar());

                        if (cantidadDetalles == 0)
                        {
                            transaccion.Rollback();
                            lblMensaje.Text = "La Nota de Pedido no tiene detalles cargados.";
                            return;
                        }

                        // Generar la cabecera de la Orden de Compra.
                        string consultaOrden = @"
                    INSERT INTO OrdenCompra
                        (idNotaPedido, fechaEmision, estado)
                    VALUES
                        (@idNotaPedido, GETDATE(), 'Emitida');

                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        SqlCommand comandoOrden =
                            new SqlCommand(consultaOrden, conexion, transaccion);

                        comandoOrden.Parameters.AddWithValue("@idNotaPedido", idNotaPedido);

                        int idOrdenCompra =
                            Convert.ToInt32(comandoOrden.ExecuteScalar());

                        // Copiar automáticamente los detalles de la Nota de Pedido.
                        string consultaInsertarDetalles = @"
                    INSERT INTO DetalleOrdenCompra
                        (idOrdenCompra, idModelo, cantidadSolicitada)
                    SELECT
                        @idOrdenCompra, idModelo, cantidad
                    FROM DetalleNotaPedido
                    WHERE idNotaPedido = @idNotaPedido";

                        SqlCommand comandoInsertarDetalles =
                            new SqlCommand(consultaInsertarDetalles, conexion, transaccion);

                        comandoInsertarDetalles.Parameters.AddWithValue("@idOrdenCompra", idOrdenCompra);
                        comandoInsertarDetalles.Parameters.AddWithValue("@idNotaPedido", idNotaPedido);

                        comandoInsertarDetalles.ExecuteNonQuery();

                        transaccion.Commit();

                        lblMensaje.Text =
                            "Orden de Compra N.º " + idOrdenCompra + " generada correctamente.";

                        CargarNotasAprobadas();
                        CargarOrdenesCompra();
                    }
                    catch (SqlException ex)
                    {
                        transaccion.Rollback();

                        if (ex.Number == 2601 || ex.Number == 2627)
                        {
                            lblMensaje.Text =
                                "Esta Nota de Pedido ya tiene una Orden de Compra.";
                        }
                        else
                        {
                            lblMensaje.Text = "Error al generar la Orden de Compra: " + ex.Message;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        lblMensaje.Text = "Error: " + ex.Message;
                    }
                }
            }
        }
    }
}