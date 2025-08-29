<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmPaciente.aspx.vb" Inherits="AplicacionCitasMedicas.frmPaciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="position: absolute; top: 10px; right: 20px;">
        <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="btn btn-danger" OnClick="btnCerrarSesion_Click" />
    </div>

    <h2>Bienvenido, <asp:Label ID="lblNombrePaciente" runat="server" Text=""></asp:Label></h2>

    <div class="d-flex justify-content-center my-4 gap-4">
        <asp:Button ID="btnCrearCita" runat="server" Text="Crear Cita Médica" CssClass="btn btn-primary btn-lg" OnClick="btnCrearCita_Click" />
        <asp:Button ID="btnEditarCita" runat="server" Text="Editar Cita Médica" CssClass="btn btn-warning btn-lg" OnClick="btnEditarCita_Click" />
        <asp:Button ID="btnMostrarCitas" runat="server" Text="Mostrar Citas" CssClass="btn btn-info btn-lg" OnClick="btnMostrarCitas_Click" />
    </div>

    <!-- Panel para Crear Cita -->
    <asp:Panel ID="pnlCrearCita" runat="server" Visible="false">
        <h4>Crear Nueva Cita Médica</h4>
        <asp:Label ID="lblCrearCitaError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
        <div class="mb-3">
            <label>Fecha:</label>
            <asp:TextBox ID="txtFecha" runat="server" TextMode="Date"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Hora:</label>
            <asp:TextBox ID="txtHora" runat="server" TextMode="Time"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Doctor:</label>
            <asp:DropDownList ID="ddlDoctor" runat="server"></asp:DropDownList>
        </div>
        <div class="mb-3">
            <label>Observaciones:</label>
            <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
        <asp:Button ID="btnGuardarCita" runat="server" Text="Guardar Cita" CssClass="btn btn-success" OnClick="btnGuardarCita_Click" />
    </asp:Panel>

    <!-- Panel para Editar Cita -->
    <asp:Panel ID="pnlEditarCitaBuscar" runat="server" Visible="false">
        <h4>Editar Cita Médica</h4>
        <asp:Label ID="lblEditarCitaError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
        <div class="mb-3">
            <label>ID de la cita:</label>
            <asp:TextBox ID="txtBuscarIdCita" runat="server"></asp:TextBox>
            <asp:Button ID="btnBuscarCita" runat="server" Text="Buscar" CssClass="btn btn-secondary" OnClick="btnBuscarCita_Click" />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlEditarCitaForm" runat="server" Visible="false">
        <h4>Editar Información de Cita</h4>
        <!-- Aquí puedes mostrar los campos para editar la cita -->
        <div class="mb-3">
            <label>Fecha:</label>
            <asp:TextBox ID="txtEditFecha" runat="server" TextMode="Date"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Hora:</label>
            <asp:TextBox ID="txtEditHora" runat="server" TextMode="Time"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Observaciones:</label>
            <asp:TextBox ID="txtEditObservaciones" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
        <asp:Button ID="btnActualizarCita" runat="server" Text="Actualizar Cita" CssClass="btn btn-success" OnClick="btnActualizarCita_Click" />
    </asp:Panel>

    <!-- Panel para Mostrar Citas -->
    <asp:Panel ID="pnlMostrarCitas" runat="server" Visible="false">
        <h4>Historial de Citas</h4>
        <asp:GridView ID="gvDatos" runat="server"
            DataKeyNames ="Id">  

        </asp:GridView>
    </asp:Panel>

</asp:Content>