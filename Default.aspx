<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KawaSRL._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="kawa-dashboard">

        <section class="dashboard-header">
            <div class="dashboard-brand">
                <div class="dashboard-logo">K</div>
                <div>
                    <h1>KAWA</h1>
                    <span>SISTEMA DE GESTIÓN</span>
                </div>
            </div>

            <div class="dashboard-welcome">
                <h2>Panel principal</h2>
                <p>Bienvenido/a a la plataforma de gestión.</p>
                <asp:Label ID="lblBienvenida" runat="server"
                    CssClass="welcome-user"></asp:Label>
            </div>
        </section>

        <section class="dashboard-content">

            <div class="section-heading">
                <h2>Gestión del sistema</h2>
                <p>Seleccioná el módulo al que deseás acceder.</p>
            </div>

            <div class="dashboard-grid">

                <asp:HyperLink ID="lnkOrdenesCompra" runat="server"
                    NavigateUrl="~/OrdenesCompra.aspx"
                    CssClass="dashboard-card">
                    <span class="card-icon">OC</span>
                    <span class="card-title">Órdenes de Compra</span>
                    <span class="card-description">
                        Generación y administración de órdenes de compra.
                    </span>
                    <span class="card-action">Ingresar →</span>
                </asp:HyperLink>

                <asp:HyperLink ID="lnkPedidosPendientes" runat="server"
                    NavigateUrl="~/PedidosPendientes.aspx"
                    CssClass="dashboard-card">
                    <span class="card-icon">PP</span>
                    <span class="card-title">Pedidos Pendientes</span>
                    <span class="card-description">
                        Registro y seguimiento de pedidos pendientes.
                    </span>
                    <span class="card-action">Ingresar →</span>
                </asp:HyperLink>

                <asp:HyperLink ID="lnkTalonesEnvio" runat="server"
                    NavigateUrl="~/TalonesEnvio.aspx"
                    CssClass="dashboard-card">
                    <span class="card-icon">TE</span>
                    <span class="card-title">Talones de Envío</span>
                    <span class="card-description">
                        Registro y consulta de talones de envío.
                    </span>
                    <span class="card-action">Ingresar →</span>
                </asp:HyperLink>

                <asp:HyperLink ID="lnkEnviosOrdenCompra" runat="server"
                    NavigateUrl="~/EnviosOrdenCompra.aspx"
                    CssClass="dashboard-card">
                    <span class="card-icon">EO</span>
                    <span class="card-title">Envíos de Órdenes</span>
                    <span class="card-description">
                        Administración de envíos por correo privado o fax.
                    </span>
                    <span class="card-action">Ingresar →</span>
                </asp:HyperLink>

            </div>

            <div class="dashboard-footer">
                <span>Has iniciado sesión correctamente.</span>
                <span>KAWA S.R.L. | Sistema de Gestión</span>
            </div>

        </section>

    </main>

</asp:Content>
