<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="KawaSRL.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
    <h2>Iniciar sesión</h2>

    <asp:Label ID="lblUsuario" runat="server" Text="Usuario:"></asp:Label>
    <br />
    <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>

    <br /><br />

    <asp:Label ID="lblPassword" runat="server" Text="Contraseña:"></asp:Label>
    <br />
    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>

    <br /><br />

    <asp:Button ID="btnIngresar" runat="server"
    Text="Ingresar"
    OnClick="btnIngresar_Click" />

    <br /><br />

    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
</div>
    </form>
</body>
</html>
