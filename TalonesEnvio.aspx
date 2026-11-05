<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TalonesEnvio.aspx.cs" Inherits="KawaSRL.TalonesEnvio" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar Talón de Envío</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Registrar Talón de Envío</h2>

            <asp:Label ID="lblOrden" runat="server"
                Text="Orden de Compra:"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlOrdenCompra" runat="server"
                Width="350px"></asp:DropDownList>
            <br /><br />

            <asp:Label ID="lblNumero" runat="server"
                Text="Número de talón:"></asp:Label>
            <br />
            <asp:TextBox ID="txtNumeroTalon" runat="server"
                MaxLength="50"></asp:TextBox>
            <br /><br />

            <asp:Label ID="lblFecha" runat="server"
                Text="Fecha de envío:"></asp:Label>
            <br />
            <asp:TextBox ID="txtFechaEnvio" runat="server"
                TextMode="Date"></asp:TextBox>
            <br /><br />

            <asp:Label ID="lblEmpresa" runat="server"
                Text="Empresa de correo:"></asp:Label>
            <br />
            <asp:TextBox ID="txtEmpresaCorreo" runat="server"
                MaxLength="100"></asp:TextBox>
            <br /><br />

            <asp:Label ID="lblObservaciones" runat="server"
                Text="Observaciones:"></asp:Label>
            <br />
            <asp:TextBox ID="txtObservaciones" runat="server"
                TextMode="MultiLine" Rows="3" Columns="40"></asp:TextBox>
            <br /><br />

            <asp:Button ID="btnRegistrar" runat="server"
                Text="Registrar talón"
                OnClick="btnRegistrar_Click" />

            <br /><br />

            <asp:Label ID="lblMensaje" runat="server"></asp:Label>

            <hr />

            <h3>Talones registrados</h3>

            <asp:GridView ID="gvTalones" runat="server"
                AutoGenerateColumns="True"
                EmptyDataText="No hay talones registrados.">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
