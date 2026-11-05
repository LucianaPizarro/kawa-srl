using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.UI;

namespace KawaSRL
{
    public partial class PedidosPendientes : System.Web.UI.Page
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
                CargarDetallesOrden();
                CargarPedidosPendientes();
            }
        }

        private void CargarDetallesOrden()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = @"
                      SELECT
                      DOC.idDetalleOrdenCompra,
                      OC.idOrdenCompra,
                      M.nombre AS Modelo,
                      DOC.cantidadSolicitada,
                      CONCAT('Orden N.º ', OC.idOrdenCompra,
                      ' - ', M.nombre,
                       ' - Cantidad: ', DOC.cantidadSolicitada) AS Descripcion
                  FROM OrdenCompra OC
                  INNER JOIN DetalleOrdenCompra DOC
                  ON OC.idOrdenCompra = DOC.idOrdenCompra
                  INNER JOIN Modelo M
                  ON DOC.idModelo = M.idModelo
                  WHERE NOT EXISTS
                   (
                    SELECT 1
                    FROM PedidoPendiente PP
                    WHERE PP.idDetalleOrdenCompra = DOC.idDetalleOrdenCompra
                    )
                  ORDER BY OC.idOrdenCompra";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    conexion.Open();

                    ddlDetallesOrden.DataSource = comando.ExecuteReader();
                    ddlDetallesOrden.DataTextField = "Descripcion";
                    ddlDetallesOrden.DataValueField = "idDetalleOrdenCompra";
                    ddlDetallesOrden.DataBind();
                }
            }

            ddlDetallesOrden.Items.Insert(0,
                new System.Web.UI.WebControls.ListItem(
                    "-- Seleccione una Orden de Compra --", ""));
        }

        private void CargarPedidosPendientes()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = @"
                    SELECT
                        PP.idPedidoPendiente,
                        OC.idOrdenCompra,
                        M.nombre AS Modelo,
                        PP.cantidadPendiente,
                        PP.fechaRegistro,
                        PP.estado
                    FROM PedidoPendiente PP
                    INNER JOIN DetalleOrdenCompra DOC
                        ON PP.idDetalleOrdenCompra = DOC.idDetalleOrdenCompra
                    INNER JOIN OrdenCompra OC
                        ON DOC.idOrdenCompra = OC.idOrdenCompra
                    INNER JOIN Modelo M
                        ON DOC.idModelo = M.idModelo
                    WHERE PP.estado = 'Pendiente'
                    ORDER BY PP.idPedidoPendiente DESC";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);

                        gvPedidosPendientes.DataSource = tabla;
                        gvPedidosPendientes.DataBind();
                    }
                }
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            int idDetalleOrdenCompra;
            int cantidad;

            if (!int.TryParse(ddlDetallesOrden.SelectedValue, out idDetalleOrdenCompra))
            {
                lblMensaje.Text = "Seleccione una Orden de Compra.";
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0)
            {
                lblMensaje.Text = "Ingrese una cantidad mayor que cero.";
                return;
            }

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        string consulta = @"
                         SELECT DOC.cantidadSolicitada
                         FROM DetalleOrdenCompra DOC
                         INNER JOIN OrdenCompra OC
                             ON DOC.idOrdenCompra = OC.idOrdenCompra
                         WHERE DOC.idDetalleOrdenCompra = @idDetalle
                         AND OC.estado = 'Emitida'
                         AND NOT EXISTS
                         (
                             SELECT 1
                             FROM PedidoPendiente PP
                             WHERE PP.idDetalleOrdenCompra = @idDetalle
                         )";

                        SqlCommand comandoValidacion =
                            new SqlCommand(consulta, conexion, transaccion);

                        comandoValidacion.Parameters.AddWithValue(
                            "@idDetalle", idDetalleOrdenCompra);

                        object resultado = comandoValidacion.ExecuteScalar();

                        if (resultado == null)
                        {
                            transaccion.Rollback();
                            lblMensaje.Text =
                                "El detalle seleccionado ya tiene un pedido pendiente registrado.";
                            return;
                        }

                        int cantidadSolicitada = Convert.ToInt32(resultado);

                        if (cantidad > cantidadSolicitada)
                        {
                            transaccion.Rollback();
                            lblMensaje.Text =
                                "La cantidad pendiente no puede superar la cantidad solicitada.";
                            return;
                        }

                        string insertar = @"
                            INSERT INTO PedidoPendiente
                                (idDetalleOrdenCompra, cantidadPendiente, fechaRegistro, estado)
                            VALUES
                                (@idDetalle, @cantidad, GETDATE(), 'Pendiente')";

                        SqlCommand comandoInsertar =
                            new SqlCommand(insertar, conexion, transaccion);

                        comandoInsertar.Parameters.AddWithValue(
                            "@idDetalle", idDetalleOrdenCompra);

                        comandoInsertar.Parameters.AddWithValue(
                            "@cantidad", cantidad);

                        comandoInsertar.ExecuteNonQuery();

                        transaccion.Commit();

                        lblMensaje.Text = "Pedido pendiente registrado correctamente.";

                        txtCantidad.Text = "";

                        CargarDetallesOrden();
                        CargarPedidosPendientes();
                    }
                    catch (Exception ex)
                    {
                        if (transaccion.Connection != null)
                        {
                            transaccion.Rollback();
                        }

                        lblMensaje.Text = "Error al registrar: " + ex.Message;
                    }
                }
            }
        }
    }
}