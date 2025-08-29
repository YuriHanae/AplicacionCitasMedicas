<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmDoctores.aspx.vb" Inherits="AplicacionCitasMedicas.frmDoctores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card shadow-lg p-4" style="max-width: 400px; width: 100%;">
        <div class="card-body">
            <h2 class="h4 mb-3 text-center">Agregar Usuarios</h2>

            <div class="form-floating">
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Name"></asp:TextBox>
                <label for="MainContent_txtNombre">Nombre</label>
            </div>

            <div class="form-floating">
                <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Name"></asp:TextBox>
                <label for="MainContent_txtApellidos">Apellidos</label>
            </div>

            <div class="form-floating">
                <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Name"></asp:TextBox>
                <label for="MainContent_txtDNI">DNI</label>
            </div>

            <div class="form-floating">
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Name"></asp:TextBox>
                <label for="MainContent_txtTelefono">telefono</label>
            </div>

            <div class="form-floating">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Email"></asp:TextBox>
                <label for="MainContent_txtEmail">Email address</label>
            </div>

            <div class="form-floating">
                <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Name"></asp:TextBox>
                <label for="MainContent_txtUser">Usuario</label>
            </div>


            <div class="form-floating">
                <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password"></asp:TextBox>
                <label for="MainContent_txtPass">Password</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPass"
                    ControlToValidate="txtPass"
                    Display="Dynamic"
                    ErrorMessage="La contraseña es requerida"
                    runat="server" />
            </div>

            <asp:Button CssClass="btn btn-primary w-100 py-2" ID="btnRegistrar" runat="server" Text="Registrarse" OnClick="btnRegistrar_Click" />
        </div>
    </div>
        <asp:Label ID="lblError" runat="server" Text="" CssClass="error"></asp:Label>
</asp:Content>
