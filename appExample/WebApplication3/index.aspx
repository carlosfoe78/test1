<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication3.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" ID="lblId" Text="Id"></asp:Label>
        <asp:TextBox runat="server" ID="txtId"></asp:TextBox>
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click" />
        <br />
        <asp:Label runat="server" ID="lblNombre" Text="Nombre"></asp:Label>
        <asp:TextBox runat="server" ID="txtNombre"></asp:TextBox><br />
        <asp:Label runat="server" ID="lblApelldio" Text="Apellido"></asp:Label>
        <asp:TextBox runat="server" ID="txtApellido"></asp:TextBox><br />
        <asp:Label runat="server" ID="lblEdad" Text="Edad"></asp:Label>
        <asp:TextBox runat="server" ID="txtEdad"></asp:TextBox><br />
        <asp:Label runat="server" ID="lblTelefono" Text="Telefono"></asp:Label>
        <asp:TextBox runat="server" ID="txtTelefono"></asp:TextBox><br />
        <asp:Label runat="server" ID="lblDireccion" Text="Direccion"></asp:Label>
        <asp:TextBox runat="server" ID="txtDireccion"></asp:TextBox><br />
        <asp:Button runat="server" ID="btnEnviar" Text="Enviar" OnClick="btnEnviar_Click" />
    </div>

    <div>
        <asp:GridView runat="server" ID="grvDatos"></asp:GridView>
    </div>
    </form>
    <br />
    
        
    <asp:Literal runat="server" ID="litError"></asp:Literal>
</body>

</html>
