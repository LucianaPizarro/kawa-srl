<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KawaSRL._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="container">
    <div class="jumbotron text-center">
        <h1>Sistema de Gestión KAWA</h1>

        <h2>¡Bienvenido!</h2>

        <p class="lead">
            <asp:Label ID="lblBienvenida" runat="server"></asp:Label>
            <br /><br />
         <asp:HyperLink
    ID="lnkOrdenesCompra"
    runat="server"
    NavigateUrl="~/OrdenesCompra.aspx"
    Text="Gestión de Órdenes de Compra" />
            <br />
<asp:HyperLink
    ID="lnkPedidosPendientes"
    runat="server"
    NavigateUrl="~/PedidosPendientes.aspx"
    Text="Gestión de Pedidos Pendientes" />
            <br />
            <asp:HyperLink
    ID="lnkTalonesEnvio"
    runat="server"
    NavigateUrl="~/TalonesEnvio.aspx"
    Text="Gestión de Talones de Envío" />
       <br />
<asp:HyperLink
    ID="lnkEnviosOrdenCompra"
    runat="server"
    NavigateUrl="~/EnviosOrdenCompra.aspx"
    Text="Gestión de Envíos de Órdenes de Compra" />
        </p>

        <p>Has iniciado sesión correctamente.</p>
    </div>
</main>

</asp:Content>
