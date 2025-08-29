<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Registrar.aspx.vb" Inherits="AplicacionCitasMedicas.Registrar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Registro de Paciente</h2>
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
        <div class="mb-3">
            <label>Nombre:</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Apellido:</label>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>DNI:</label>
            <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Teléfono:</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Usuario:</label>
            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Contraseña:</label>
            <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
        </div>
        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
        <a href="Login.aspx" class="btn btn-link">Volver al Login</a>
    </div>
</asp:Content>