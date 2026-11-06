
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TalonesEnvio.aspx.cs" Inherits="KawaSRL.TalonesEnvio" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Talones de Envío - KAWA</title>
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
                    <span class="management-eyebrow">MÓDULO DE LOGÍSTICA</span>
                    <h2>Gestión de Talones de Envío</h2>
                    <p>Registro y administración de los comprobantes de envío de Órdenes de Compra.</p>
                </div>
            </header>

            <section class="management-content">

                <div class="management-card">
                    <div class="card-heading">
                        <h3>Registrar Talón de Envío</h3>
                        <p>Complete los datos correspondientes al envío de la Orden de Compra.</p>
                    </div>

                    <div class="management-form">

                        <asp:Label ID="lblOrden" runat="server"
                            Text="Orden de Compra:"
                            CssClass="management-label">
                        </asp:Label>

                        <asp:DropDownList ID="ddlOrdenCompra" runat="server"
                            Width="350px"
                            CssClass="management-select">
                        </asp:DropDownList>

                        <asp:Label ID="lblNumero" runat="server"
                            Text="Número de talón:"
                            CssClass="management-label">
                        </asp:Label>

                        <asp:TextBox ID="txtNumeroTalon" runat="server"
                            MaxLength="50"
                            CssClass="management-input">
                        </asp:TextBox>

                        <asp:Label ID="lblFecha" runat="server"
                            Text="Fecha de envío:"
                            CssClass="management-label">
                        </asp:Label>

                        <asp:TextBox ID="txtFechaEnvio" runat="server"
                            TextMode="Date"
                            CssClass="management-input">
                        </asp:TextBox>

                        <asp:Label ID="lblEmpresa" runat="server"
                            Text="Empresa de transporte:"
                            CssClass="management-label">
                        </asp:Label>

                        <asp:TextBox ID="txtEmpresaCorreo" runat="server"
                            MaxLength="100"
                            CssClass="management-input">
                        </asp:TextBox>

                        <asp:Label ID="lblObservaciones" runat="server"
                            Text="Observaciones:"
                            CssClass="management-label">
                        </asp:Label>

                        <asp:TextBox ID="txtObservaciones" runat="server"
                            TextMode="MultiLine"
                            Rows="3"
                            Columns="40"
                            CssClass="management-textarea">
                        </asp:TextBox>

                        <asp:Button ID="btnRegistrar" runat="server"
                            Text="Registrar talón"
                            CssClass="management-button"
                            OnClick="btnRegistrar_Click" />

                        <asp:Label ID="lblMensaje" runat="server"
                            CssClass="management-message">
                        </asp:Label>

                    </div>
                </div>

                <div class="management-card">
                    <div class="card-heading">
                        <h3>Talones registrados</h3>
                        <p>Listado de comprobantes de envío registrados en el sistema.</p>
                    </div>

                    <div class="table-container">

                        <asp:GridView ID="gvTalones" runat="server"
                            AutoGenerateColumns="false"
                            CssClass="tabla"
                            EmptyDataText="No hay talones registrados.">

                            <Columns>
                                <asp:BoundField DataField="idTalonEnvio"
                                    HeaderText="N.º Talón" />

                                <asp:BoundField DataField="idOrdenCompra"
                                    HeaderText="N.º Orden de Compra" />

                                <asp:BoundField DataField="numeroTalon"
                                    HeaderText="Número de talón" />

                                <asp:BoundField DataField="fechaEnvio"
                                    HeaderText="Fecha de envío"
                                    DataFormatString="{0:dd/MM/yyyy}" />

                                <asp:BoundField DataField="empresaCorreo"
                                    HeaderText="Empresa de transporte" />

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

    </form>
</body>
</html>
