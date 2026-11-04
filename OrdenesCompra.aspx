<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrdenesCompra.aspx.cs" Inherits="KawaSRL.OrdenesCompra" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Gestión de Órdenes de Compra - KAWA</title>
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

            <h1>Gestión de Órdenes de Compra</h1>

            <p>Seleccione una Nota de Pedido aprobada para generar su Orden de Compra definitiva.</p>

            <asp:Label ID="lblNota" runat="server"
                Text="Nota de Pedido:"></asp:Label>

            <br /><br />

            <asp:DropDownList ID="ddlNotasPedido" runat="server"
                Width="350px">
            </asp:DropDownList>

            <br /><br />

            <asp:Button ID="btnGenerar" runat="server"
                Text="Generar Orden de Compra"
                CssClass="boton"
                OnClick="btnGenerar_Click" />

            <asp:Label ID="lblMensaje" runat="server"
                CssClass="mensaje"></asp:Label>

            <h2>Órdenes de Compra generadas</h2>

            <asp:GridView ID="gvOrdenes" runat="server"
                AutoGenerateColumns="true"
                CssClass="tabla"
                EmptyDataText="Todavía no hay Órdenes de Compra generadas.">
            </asp:GridView>

        </div>
    </form>
</body>
</html>
