<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmPaciente.aspx.vb" Inherits="AplicacionCitasMedicas.frmPaciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="position: absolute; top: 10px; right: 20px;">
        <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="btn btn-danger" OnClick="btnCerrarSesion_Click" />
    </div>

    <h2>Bienvenido, <asp:Label ID="lblNombrePaciente" runat="server" Text=""></asp:Label></h2>



    <!-- Pestañas Bootstrap -->
    <ul class="nav nav-tabs mb-3" id="citaTabs" role="tablist">
        <li class="nav-item" role="presentation">
            <button class="nav-link active" id="crear-tab" data-bs-toggle="tab" data-bs-target="#crear" type="button" role="tab">Crear Cita</button>
        </li>
        <li class="nav-item" role="presentation">
            <button class="nav-link" id="editar-tab" data-bs-toggle="tab" data-bs-target="#editar" type="button" role="tab">Editar Cita</button>
        </li>
        <li class="nav-item" role="presentation">
            <button class="nav-link" id="mostrar-tab" data-bs-toggle="tab" data-bs-target="#mostrar" type="button" role="tab">Historial</button>
        </li>
    </ul>

    <div class="tab-content" id="citaTabsContent">
       <!-- Panel Crear Cita -->
    <div class="tab-pane fade show active" id="crear" role="tabpanel">
        <asp:Panel ID="pnlCrearCita" runat="server">
            <!-- contenido sin Visible="false" -->
        </asp:Panel>
    </div>

    <!-- Panel Editar Cita -->
    <div class="tab-pane fade" id="editar" role="tabpanel">
        <asp:Panel ID="pnlEditarCitaBuscar" runat="server">
            <!-- contenido sin Visible="false" -->
        </asp:Panel>
        <asp:Panel ID="pnlEditarCitaForm" runat="server">
            <!-- contenido sin Visible="false" -->
        </asp:Panel>
    </div>

    <!-- Panel Mostrar Citas -->
    <div class="tab-pane fade" id="mostrar" role="tabpanel">
        <asp:Panel ID="pnlMostrarCitas" runat="server">
            <!-- contenido sin Visible="false" -->
        </asp:Panel>
    </div>
    </div>

</asp:Content>