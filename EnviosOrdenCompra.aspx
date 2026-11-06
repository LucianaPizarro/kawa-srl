
<%@ Page Title="Envíos de Órdenes de Compra" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EnviosOrdenCompra.aspx.cs" Inherits="KawaSRL.EnviosOrdenCompra" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

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
                <span class="management-eyebrow">MÓDULO DE LOGÍSTICA</span>
                <h2>Gestión de Envíos de Órdenes de Compra</h2>
                <p>Registro y seguimiento de los envíos de Órdenes de Compra a proveedores.</p>
            </div>
        </header>

        <section class="management-content">

            <div class="management-card">
                <div class="card-heading">
                    <h3>Registrar envío</h3>
                    <p>Complete la información correspondiente al envío de la Orden de Compra.</p>
                </div>

                <div class="management-form">

                    <asp:Label ID="lblOrden" runat="server"
                        Text="Orden de Compra:"
                        CssClass="management-label" />

                    <asp:DropDownList ID="ddlOrdenCompra" runat="server"
                        CssClass="management-select">
                    </asp:DropDownList>

                    <asp:Label ID="lblMedio" runat="server"
                        Text="Medio de envío:"
                        CssClass="management-label" />

                    <asp:DropDownList ID="ddlMedioEnvio" runat="server"
                        CssClass="management-select">
                        <asp:ListItem Text="Seleccione un medio" Value="" />
                        <asp:ListItem Text="Correo privado" Value="Correo privado" />
                        <asp:ListItem Text="Fax" Value="Fax" />
                    </asp:DropDownList>

                    <asp:Label ID="lblFecha" runat="server"
                        Text="Fecha de envío:"
                        CssClass="management-label" />

                    <asp:TextBox ID="txtFechaEnvio" runat="server"
                        TextMode="Date"
                        CssClass="management-input" />

                    <asp:Label ID="lblReferencia" runat="server"
                        Text="Número de seguimiento o referencia:"
                        CssClass="management-label" />

                    <asp:TextBox ID="txtReferencia" runat="server"
                        MaxLength="100"
                        CssClass="management-input" />

                    <asp:Label ID="lblObservaciones" runat="server"
                        Text="Observaciones:"
                        CssClass="management-label" />

                    <asp:TextBox ID="txtObservaciones" runat="server"
                        TextMode="MultiLine"
                        Rows="3"
                        CssClass="management-textarea" />

                    <asp:Button ID="btnRegistrar" runat="server"
                        Text="Registrar envío"
                        CssClass="management-button"
                        OnClick="btnRegistrar_Click" />

                    <asp:Label ID="lblMensaje" runat="server"
                        CssClass="management-message" />

                </div>
            </div>

            <div class="management-card">
                <div class="card-heading">
                    <h3>Envíos registrados</h3>
                    <p>Consulta de los envíos realizados a los proveedores.</p>
                </div>

                <div class="table-container">

                    <asp:GridView ID="gvEnvios" runat="server"
                        AutoGenerateColumns="false"
                        CssClass="tabla"
                        EmptyDataText="Todavía no hay envíos registrados.">

                        <Columns>
                            <asp:BoundField DataField="idEnvio"
                                HeaderText="N.º de Envío" />

                            <asp:BoundField DataField="idOrdenCompra"
                                HeaderText="N.º de Orden de Compra" />

                            <asp:BoundField DataField="Proveedor"
                                HeaderText="Proveedor" />

                            <asp:BoundField DataField="medioEnvio"
                                HeaderText="Medio de Envío" />

                            <asp:BoundField DataField="fechaEnvio"
                                HeaderText="Fecha de Envío"
                                DataFormatString="{0:dd/MM/yyyy}" />

                            <asp:BoundField DataField="referencia"
                                HeaderText="Referencia" />

                            <asp:BoundField DataField="observaciones"
                                HeaderText="Observaciones" />
                        </Columns>

                    </asp:GridView>

                </div>
            </div>

            <footer class="management-footer">
                KAWA S.R.L. | Sistema de Gestión
            </footer>

        </section>

    </main>

</asp:Content>
