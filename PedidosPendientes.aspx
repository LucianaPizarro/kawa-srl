
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PedidosPendientes.aspx.cs" Inherits="KawaSRL.PedidosPendientes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Pedidos Pendientes - KAWA</title>
    <link href="<%= ResolveUrl("~/Content/Site.css") %>" rel="stylesheet" />
</head>

<body class="kawa-page">
    <form id="form1" runat="server">

        <main class="kawa-management">

            <header class="management-header">
                <div class="management-brand">
                    <div class="management-logo">K</div>
                    <div>
                        <h1>KAWA</h1>
                        <span>SISTEMA DE GESTIÓN</span>
                    </div>
                </div>

                <div class="management-heading">
                    <span class="management-eyebrow">MÓDULO DE COMPRAS</span>
                    <h2>Gestión de Pedidos Pendientes</h2>
                    <p>Registro y seguimiento de pedidos de Órdenes de Compra pendientes de recepción.</p>
                </div>
            </header>

            <section class="management-content">

                <div class="management-card">
                    <div class="card-heading">
                        <h3>Registrar pedido pendiente</h3>
                        <p>Seleccione el detalle de una Orden de Compra e indique la cantidad pendiente de recepción.</p>
                    </div>

                    <div class="management-form">

                        <asp:Label ID="lblOrden" runat="server"
                            Text="Orden de Compra:"
                            CssClass="management-label">
                        </asp:Label>

                        <asp:DropDownList ID="ddlDetallesOrden" runat="server"
                            Width="400px"
                            CssClass="management-select">
                        </asp:DropDownList>

                        <asp:Label ID="lblCantidad" runat="server"
                            Text="Cantidad pendiente:"
                            CssClass="management-label">
                        </asp:Label>

                        <asp:TextBox ID="txtCantidad" runat="server"
                            TextMode="Number"
                            CssClass="management-input">
                        </asp:TextBox>

                        <asp:Button ID="btnRegistrar" runat="server"
                            Text="Registrar pedido pendiente"
                            CssClass="management-button"
                            OnClick="btnRegistrar_Click" />

                        <asp:Label ID="lblMensaje" runat="server"
                            CssClass="management-message">
                        </asp:Label>

                    </div>
                </div>

                <div class="management-card">
                    <div class="card-heading">
                        <h3>Pedidos pendientes registrados</h3>
                        <p>Consulta de los pedidos pendientes almacenados en el sistema.</p>
                    </div>

                    <div class="table-container">

                        <asp:GridView ID="gvPedidosPendientes" runat="server"
                            AutoGenerateColumns="false"
                            CssClass="tabla"
                            EmptyDataText="Todavía no hay pedidos pendientes registrados.">

                            <Columns>
                                <asp:BoundField DataField="idPedidoPendiente"
                                    HeaderText="N.º Pedido Pendiente" />

                                <asp:BoundField DataField="idOrdenCompra"
                                    HeaderText="N.º Orden de Compra" />

                                <asp:BoundField DataField="Modelo"
                                    HeaderText="Modelo" />

                                <asp:BoundField DataField="cantidadPendiente"
                                    HeaderText="Cantidad Pendiente" />

                                <asp:BoundField DataField="fechaRegistro"
                                    HeaderText="Fecha de Registro"
                                    DataFormatString="{0:dd/MM/yyyy}" />

                                <asp:BoundField DataField="estado"
                                    HeaderText="Estado" />
                            </Columns>

                        </asp:GridView>

                    </div>
                </div>

                <footer class="management-footer">
                    KAWA S.R.L. | Sistema de Gestión
                </footer>

            </section>

        </main>

    </form>
</body></html>