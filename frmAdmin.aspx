<%@ Page Title="Administración de Doctores" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="frmAdmin.aspx.vb" Inherits="AplicacionCitasMedicas.frmAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="card shadow-sm">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0">👨‍⚕️ Gestión de Doctores</h4>
            </div>
            <div class="card-body">
                <asp:Label ID="lblMensaje" runat="server" CssClass="alert d-none" />

                <asp:HiddenField ID="hfIdDoctor" runat="server" />

                <div class="row g-3">
                    <div class="col-md-6">
                        <label for="txtNombre" class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="valNombre" runat="server" ControlToValidate="txtNombre"
                            ErrorMessage="* Campo obligatorio" CssClass="text-danger small" Display="Dynamic" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtEspecialidad" class="form-label">Especialidad</label>
                        <asp:TextBox ID="txtEspecialidad" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="valEspecialidad" runat="server" ControlToValidate="txtEspecialidad"
                            ErrorMessage="* Campo obligatorio" CssClass="text-danger small" Display="Dynamic" />
                    </div>
                    <div class="col-md-6">
    <label for="txtTelefono" class="form-label">Teléfono</label>
    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
    <asp:RequiredFieldValidator ID="valTelefono" runat="server" ControlToValidate="txtTelefono"
        ErrorMessage="* Campo obligatorio" CssClass="text-danger small" Display="Dynamic" />
</div>

<div class="col-md-6">
    <label for="txtEmail" class="form-label">Email</label>
    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
    <asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="txtEmail"
        ErrorMessage="* Campo obligatorio" CssClass="text-danger small" Display="Dynamic" />
</div>

<div class="col-md-6">
    <label for="txtUsuario" class="form-label">Usuario</label>
    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" />
    <asp:RequiredFieldValidator ID="valUsuario" runat="server" ControlToValidate="txtUsuario"
        ErrorMessage="* Campo obligatorio" CssClass="text-danger small" Display="Dynamic" />
</div>

<div class="col-md-6">
    <label for="txtContrasena" class="form-label">Contraseña</label>
    <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password" />
    <asp:RequiredFieldValidator ID="valContrasena" runat="server" ControlToValidate="txtContrasena"
        ErrorMessage="* Campo obligatorio" CssClass="text-danger small" Display="Dynamic" />
</div>
                </div>

                <div class="mt-4">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success me-2" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary" />
                </div>
            </div>
        </div>

        <div class="mt-5">
            <h5 class="mb-3">📋 Lista de Doctores</h5>
            <asp:GridView ID="gvDoctores" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                CssClass="table table-hover table-bordered" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                    <asp:BoundField DataField="Contraseña" HeaderText="Contraseña" />

                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>