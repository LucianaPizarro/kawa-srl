
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrdenesCompra.aspx.cs" Inherits="KawaSRL.OrdenesCompra" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Órdenes de Compra - KAWA</title>
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
                    <h2>Gestión de Órdenes de Compra</h2>
                    <p>Generación y administración de órdenes de compra definitivas.</p>
                </div>
            </header>

            <section class="management-content">

                <div class="management-card">
                    <div class="card-heading">
                        <h3>Generar Orden de Compra</h3>
                        <p>Seleccione una Nota de Pedido aprobada para generar su Orden de Compra definitiva.</p>
                    </div>

                    <div class="management-form">
                        <asp:Label ID="lblNota" runat="server"
                            Text="Nota de Pedido:"
                            CssClass="management-label"></asp:Label>

                        <asp:DropDownList ID="ddlNotasPedido" runat="server"
                            Width="350px" CssClass="management-select">
                        </asp:DropDownList>

                        <asp:Button ID="btnGenerar" runat="server"
                            Text="Generar Orden de Compra"
                            CssClass="management-button"
                            OnClick="btnGenerar_Click" />

                        <asp:Label ID="lblMensaje" runat="server"
                            CssClass="management-message"></asp:Label>
                    </div>
                </div>

                <div class="management-card">
                    <div class="card-heading">
                        <h3>Órdenes de Compra generadas</h3>
                        <p>Listado de órdenes registradas en el sistema.</p>
                    </div>

                    <div class="table-container">
                        <asp:GridView ID="gvOrdenes" runat="server"
                            AutoGenerateColumns="false"
                            CssClass="tabla"
                            EmptyDataText="Todavía no hay Órdenes de Compra generadas.">

                            <Columns>
                                <asp:BoundField DataField="idOrdenCompra"
                                    HeaderText="N.º Orden de Compra" />

                                <asp:BoundField DataField="idNotaPedido"
                                    HeaderText="N.º Nota de Pedido" />

                                <asp:BoundField DataField="fechaEmision"
                                    HeaderText="Fecha de emisión"
                                    DataFormatString="{0:dd/MM/yyyy}" />

                                <asp:BoundField DataField="estado"
                                    HeaderText="Estado" />

                                <asp:BoundField DataField="razonSocial"
                                    HeaderText="Proveedor" />
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