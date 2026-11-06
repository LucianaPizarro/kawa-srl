
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="KawaSRL.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>KAWA | Iniciar sesión</title>

    <link href="<%= ResolveUrl("~/Content/Site.css") %>" rel="stylesheet" />
</head>

<body class="login-page" style="background-color: #142b3a !important; color: white;">

    <form id="form1" runat="server">
        <div class="login-container">

            <div class="login-card">

                <div class="login-brand">
                    <div class="login-logo"></div>
                    <h1>KAWA</h1>
                    <p>GESTIÓN EMPRESARIAL</p>
                </div>

                <div class="login-heading">
                    <h2>Bienvenido</h2>
                    <p>Ingresá tus credenciales para continuar</p>
                </div>

                <div class="login-form">

                    <asp:Label ID="lblUsuario" runat="server"
                        Text="Usuario"
                        AssociatedControlID="txtUsuario"
                        CssClass="login-label">
                    </asp:Label>

                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="login-input"
                        placeholder="Ingresá tu usuario">
                    </asp:TextBox>

                    <asp:Label ID="lblPassword" runat="server"
                        Text="Contraseña"
                        AssociatedControlID="txtPassword"
                        CssClass="login-label">
                    </asp:Label>

                    <asp:TextBox ID="txtPassword" runat="server"
                        TextMode="Password"
                        CssClass="login-input"
                        placeholder="Ingresá tu contraseña">
                    </asp:TextBox>

                    <asp:Button ID="btnIngresar" runat="server"
                        Text="INGRESAR"
                        OnClick="btnIngresar_Click"
                        CssClass="login-button" />

                    <asp:Label ID="lblMensaje" runat="server"
                        CssClass="login-message">
                    </asp:Label>

                </div>

                <div class="login-footer">
                    KAWA S.R.L. | Sistema de gestión interna
                </div>

            </div>
</div>
</form>
</body></html>