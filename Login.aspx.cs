using System;
using System.Configuration;
using System.Data.SqlClient;

namespace KawaSRL
{
    public partial class Login : System.Web.UI.Page
    {
        private const int MaxIntentos = 5;
        private const int MinutosBloqueo = 15;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMensaje.Text = "";
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(password))
            {
                lblMensaje.Text = "Ingresá tu usuario y contraseña.";
                return;
            }

            string connectionString =
                ConfigurationManager.ConnectionStrings[
                    "KawaSRLConnectionString"].ConnectionString;

            using (SqlConnection conexion =
                new SqlConnection(connectionString))
            {
                conexion.Open();

                // Reiniciar bloqueos que ya hayan vencido.
                string reiniciar = @"
                    UPDATE Usuario
                    SET intentosFallidos = 0,
                        bloqueadoHasta = NULL
                    WHERE nombreUsuario = @usuario
                    AND bloqueadoHasta IS NOT NULL
                    AND bloqueadoHasta <= GETDATE()";

                using (SqlCommand comando = new SqlCommand(
                    reiniciar, conexion))
                {
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.ExecuteNonQuery();
                }

                // Buscar el usuario.
                string consulta = @"
                    SELECT idUsuario, nombre, apellido,
                           nombreUsuario, passwordHash, rol,
                           bloqueadoHasta
                    FROM Usuario
                    WHERE nombreUsuario = @usuario
                    AND activo = 1";

                int idUsuario;
                string nombreCompleto;
                string nombreUsuario;
                string passwordHash;
                string rol;
                bool bloqueado;

                using (SqlCommand comando =
                    new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@usuario", usuario);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (!lector.Read())
                        {
                            lblMensaje.Text =
                                "Usuario o contraseña incorrectos.";
                            return;
                        }

                        idUsuario = Convert.ToInt32(
                            lector["idUsuario"]);

                        nombreCompleto =
                            lector["nombre"].ToString() + " " +
                            lector["apellido"].ToString();

                        nombreUsuario =
                            lector["nombreUsuario"].ToString();

                        passwordHash =
                            lector["passwordHash"].ToString();

                        rol = lector["rol"].ToString();

                        bloqueado =
                            lector["bloqueadoHasta"] != DBNull.Value;
                    }
                }

                if (bloqueado)
                {
                    lblMensaje.Text =
                        "La cuenta está bloqueada temporalmente. " +
                        "Intentá nuevamente en 15 minutos.";
                    return;
                }

                // Verificar contraseña.
                if (PasswordHelper.VerifyPassword(
                    password, passwordHash))
                {
                    string reiniciarIntentos = @"
                        UPDATE Usuario
                        SET intentosFallidos = 0,
                            bloqueadoHasta = NULL
                        WHERE idUsuario = @idUsuario";

                    using (SqlCommand comando = new SqlCommand(
                        reiniciarIntentos, conexion))
                    {
                        comando.Parameters.AddWithValue(
                            "@idUsuario", idUsuario);

                        comando.ExecuteNonQuery();
                    }

                    Session["idUsuario"] = idUsuario;
                    Session["nombreUsuario"] = nombreUsuario;
                    Session["nombreCompleto"] = nombreCompleto;
                    Session["rol"] = rol;

                    Response.Redirect("Default.aspx");
                    return;
                }
                else
                {
                    // Registrar el intento fallido y bloquear al quinto.
                    string registrarIntento = @"
                        UPDATE Usuario
                        SET intentosFallidos = intentosFallidos + 1,
                            bloqueadoHasta =
                                CASE
                                    WHEN intentosFallidos + 1 >= @maxIntentos
                                    THEN DATEADD(MINUTE, @minutos, GETDATE())
                                    ELSE NULL
                                END
                        WHERE idUsuario = @idUsuario";

                    int intentos;

                    using (SqlCommand comando = new SqlCommand(
                        registrarIntento, conexion))
                    {
                        comando.Parameters.AddWithValue(
                            "@maxIntentos", MaxIntentos);

                        comando.Parameters.AddWithValue(
                            "@minutos", MinutosBloqueo);

                        comando.Parameters.AddWithValue(
                            "@idUsuario", idUsuario);

                        comando.ExecuteNonQuery();
                    }

                    string consultarIntentos = @"
                        SELECT intentosFallidos
                        FROM Usuario
                        WHERE idUsuario = @idUsuario";

                    using (SqlCommand comando = new SqlCommand(
                        consultarIntentos, conexion))
                    {
                        comando.Parameters.AddWithValue(
                            "@idUsuario", idUsuario);

                        intentos = Convert.ToInt32(
                            comando.ExecuteScalar());
                    }

                    if (intentos >= MaxIntentos)
                    {
                        lblMensaje.Text =
                            "Cuenta bloqueada durante 15 minutos.";
                    }
                    else
                    {
                        lblMensaje.Text =
                            "Usuario o contraseña incorrectos. " +
                            "Intentos restantes: " +
                            (MaxIntentos - intentos);
                    }
                }
            }
        }
    }
}