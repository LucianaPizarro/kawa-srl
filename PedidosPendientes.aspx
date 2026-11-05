<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PedidosPendientes.aspx.cs" Inherits="KawaSRL.PedidosPendientes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Pedidos Pendientes - KAWA</title>

    <style>
        body {
            font-family: Arial;
            background-color: #f3f6f4;
            margin: 40px;
        }

        .contenedor {
            max-width: 1000px;
            margin: auto;
            background: white;
            padding: 30px;
            border-radius: 10px;
        }

        h1 {
            color: #245b43;
        }

        .boton {
            background-color: #245b43;
            color: white;
            padding: 10px 18px;
            border: none;
            cursor: pointer;
            border-radius: 5px;
        }

        .mensaje {
            display: block;
            margin-top: 15px;
            font-weight: bold;
        }

        .tabla {
            margin-top: 20px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <div class="contenedor">

            <h1>Gestión de Pedidos Pendientes</h1>

            <p>
                Registre los pedidos de Órdenes de Compra que todavía
                no fueron recibidos.
            </p>

            <asp:Label ID="lblOrden" runat="server"
                Text="Orden de Compra:"></asp:Label>

            <br /><br />

            <asp:DropDownList ID="ddlDetallesOrden" runat="server"
                Width="400px">
            </asp:DropDownList>

            <br /><br />

            <asp:Label ID="lblCantidad" runat="server"
                Text="Cantidad pendiente:"></asp:Label>

            <br />

            <asp:TextBox ID="txtCantidad" runat="server"
                TextMode="Number"></asp:TextBox>

            <br /><br />

            <asp:Button ID="btnRegistrar" runat="server"
                Text="Registrar pedido pendiente"
                CssClass="boton"
                OnClick="btnRegistrar_Click" />

            <asp:Label ID="lblMensaje" runat="server"
                CssClass="mensaje">
            </asp:Label>

            <h2>Pedidos pendientes registrados</h2>

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

    </form>
</body>
</html>
