<%@ Page Title="Envíos de Órdenes de Compra" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EnviosOrdenCompra.aspx.cs" Inherits="KawaSRL.EnviosOrdenCompra" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="container">

        <h2>Gestión de Envíos de Órdenes de Compra</h2>

        <p>Registre el envío de las Órdenes de Compra aprobadas a los proveedores.</p>

        <div class="form-group">
            <label>Orden de Compra:</label>
            <asp:DropDownList ID="ddlOrdenCompra" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </div>

        <div class="form-group">
            <label>Medio de envío:</label>
            <asp:DropDownList ID="ddlMedioEnvio" runat="server" CssClass="form-control">
                <asp:ListItem Text="Seleccione un medio" Value="" />
                <asp:ListItem Text="Correo privado" Value="Correo privado" />
                <asp:ListItem Text="Fax" Value="Fax" />
            </asp:DropDownList>
        </div>

        <div class="form-group">
            <label>Fecha de envío:</label>
            <asp:TextBox ID="txtFechaEnvio" runat="server"
                TextMode="Date" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Número de seguimiento o referencia:</label>
            <asp:TextBox ID="txtReferencia" runat="server"
                MaxLength="100" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Observaciones:</label>
            <asp:TextBox ID="txtObservaciones" runat="server"
                TextMode="MultiLine" Rows="3" CssClass="form-control" />
        </div>

        <asp:Button ID="btnRegistrar" runat="server"
            Text="Registrar envío"
            CssClass="btn btn-primary"
            OnClick="btnRegistrar_Click" />

        <br /><br />

        <asp:Label ID="lblMensaje" runat="server"></asp:Label>

        <h3>Envíos registrados</h3>

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

    </main>

</asp:Content>