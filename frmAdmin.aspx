<%@ Page Title="Administración" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="frmAdmin.aspx.vb" Inherits="AplicacionCitasMedicas.frmAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="d-flex justify-content-end mb-3">
            <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar sesión" CssClass="btn btn-danger" OnClick="btnCerrarSesion_Click" CausesValidation="False" />
        </div>

        <!-- Navegación por pestañas Bootstrap -->
        <ul class="nav nav-tabs mb-4" id="adminTabs" role="tablist">
            <li class="nav-item" role="presentation">
                <button class="nav-link active" id="doctores-tab" data-bs-toggle="tab" data-bs-target="#doctores" type="button" role="tab" aria-controls="doctores" aria-selected="true">👨‍⚕ Doctores</button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="citas-tab" data-bs-toggle="tab" data-bs-target="#citas" type="button" role="tab" aria-controls="citas" aria-selected="false">📅 Citas</button>
            </li>
        </ul>

        <div class="tab-content" id="adminTabsContent">
            <!-- DOCTORES -->
            <div class="tab-pane fade show active" id="doctores" role="tabpanel" aria-labelledby="doctores-tab">
                <div class="card shadow-sm mb-5">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0">👨‍⚕ Registro de Doctor</h4>
                    </div>
                    <div class="card-body">
                        <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false" />
                        <div class="row g-3">
                            <div class="col-md-6">
                                <label for="txtNombre" class="form-label">Nombre:</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-6">
                                <label for="txtEspecialidad" class="form-label">Especialidad:</label>
                                <asp:TextBox ID="txtEspecialidad" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-6">
                                <label for="txtTelefono" class="form-label">Teléfono:</label>
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-6">
                                <label for="txtEmail" class="form-label">Email:</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                            </div>
                            <div class="col-md-6">
                                <label for="txtUsuario" class="form-label">Usuario:</label>
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-6">
                                <label for="txtContrasena" class="form-label">Contraseña:</label>
                                <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password" />
                            </div>
                        </div>
                        <div class="mt-4">
                            <asp:Button ID="btnGuardar" runat="server" Text="Registrar Doctor" CssClass="btn btn-success me-2" OnClick="btnGuardar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary" OnClick="btnCancelar_Click" />
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
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <!-- CITAS -->
            <div class="tab-pane fade" id="citas" role="tabpanel" aria-labelledby="citas-tab">
                <div class="card shadow-sm my-5">
                    <div class="card-header bg-info text-white">
                        <h4 class="mb-0">📅 Gestión de Citas</h4>
                    </div>
                    <div class="card-body">
                        <asp:Label ID="lblCitasMensaje" runat="server" CssClass="alert d-none" />
                        <asp:HiddenField ID="hfIdCita" runat="server" />

                        <div class="row g-3">
                            <div class="col-md-6">
                                <label for="ddlPaciente" class="form-label">Paciente</label>
                                <asp:DropDownList ID="ddlPaciente" runat="server" CssClass="form-select" />
                            </div>
                            <div class="col-md-6">
                                <label for="ddlDoctor" class="form-label">Doctor</label>
                                <asp:DropDownList ID="ddlDoctor" runat="server" CssClass="form-select" />
                            </div>
                            <div class="col-md-6">
                                <label for="txtFecha" class="form-label">Fecha</label>
                                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date" />
                            </div>
                            <div class="col-md-6">
                                <label for="txtHora" class="form-label">Hora</label>
                                <asp:TextBox ID="txtHora" runat="server" CssClass="form-control" TextMode="Time" />
                            </div>
                            <div class="col-md-6">
                                <label for="ddlEstado" class="form-label">Estado</label>
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Seleccione..." Value="" />
                                    <asp:ListItem Text="Pendiente" Value="Pendiente" />
                                    <asp:ListItem Text="Confirmada" Value="Confirmada" />
                                    <asp:ListItem Text="Cancelada" Value="Cancelada" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-12">
                                <label for="txtObservaciones" class="form-label">Observaciones</label>
                                <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                            </div>
                        </div>

                        <div class="mt-4">
                            <asp:Button ID="btnGuardarCita" runat="server" Text="Guardar Cita" CssClass="btn btn-primary me-2" OnClick="btnGuardarCita_Click" />
                            <asp:Button ID="btnCancelarCita" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary" OnClick="btnCancelarCita_Click" />
                        </div>
                    </div>
                </div>

                <div class="mt-5">
                    <h5 class="mb-3">📋 Lista de Citas</h5>
                    <asp:GridView ID="gvCitas" runat="server" DataKeyNames="Id"
                        OnRowEditing="gvCitas_RowEditing"
                        CssClass="table table-hover table-bordered" GridLines="None">
                        
                    </asp:GridView>
                </div>

                <!-- ====== GESTIÓN AVANZADA DE CITAS (NUEVO) ====== -->

                <div class="mb-3">
                    <asp:Button ID="btnMostrarCitasPendientes" runat="server" Text="Citas Pendientes" CssClass="btn btn-primary me-2" OnClick="btnMostrarCitasPendientes_Click"/>
                    <asp:Button ID="btnBuscarHistorialPaciente" runat="server" Text="Historial por DNI" CssClass="btn btn-info me-2" OnClick="btnBuscarHistorialPaciente_Click"/>
                    <asp:Button ID="btnGestionarCita" runat="server" Text="Gestionar Cita" CssClass="btn btn-warning" OnClick="btnGestionarCita_Click"/>
                </div>

                <asp:Panel ID="pnlCitasPendientes" runat="server" Visible="False">
                    <h6>Citas Pendientes</h6>
                    <asp:GridView ID="gvCitasPendientes" runat="server" CssClass="table table-bordered">
                        
                    </asp:GridView>
                </asp:Panel>

                <asp:Panel ID="pnlHistorialPaciente" runat="server" Visible="False">
                    <div class="mb-2">
                        <asp:TextBox ID="txtBuscarDniPaciente" runat="server" CssClass="form-control d-inline-block" style="width:200px;" placeholder="DNI Paciente"/>
                        <asp:Button ID="btnBuscarDniPaciente" runat="server" Text="Buscar" CssClass="btn btn-secondary ms-2" OnClick="btnBuscarDniPaciente_Click"/>
                        <asp:Label ID="lblHistorialPacienteMsg" runat="server" CssClass="ms-2 text-danger" Visible="False"/>
                    </div>
                    <asp:GridView ID="gvHistorialPaciente" runat="server" CssClass="table table-bordered">
                        
                    </asp:GridView>
                </asp:Panel>

                <asp:Panel ID="pnlGestionarCita" runat="server" Visible="False">
                    <div class="row mb-2">
                        <div class="col-md-4">
                            <asp:TextBox ID="txtGestionarDni" runat="server" CssClass="form-control" placeholder="DNI Paciente"/>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtGestionarIdCita" runat="server" CssClass="form-control" placeholder="ID Cita"/>
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnBuscarGestionCita" runat="server" Text="Buscar" CssClass="btn btn-secondary" OnClick="btnBuscarGestionCita_Click"/>
                        </div>
                    </div>
                    <asp:Label ID="lblGestionarCitaMsg" runat="server" CssClass="text-danger" Visible="False"/>
                    <asp:Panel ID="pnlEditarCitaAdmin" runat="server" Visible="False">
                        <div class="row mb-2">
                            <div class="col-md-4">
                                <label>Fecha</label>
                                <asp:TextBox ID="txtGestionarFecha" runat="server" CssClass="form-control" TextMode="Date"/>
                            </div>
                            <div class="col-md-4">
                                <label>Hora</label>
                                <asp:TextBox ID="txtGestionarHora" runat="server" CssClass="form-control" TextMode="Time"/>
                            </div>
                            <div class="col-md-4">
                                <label>Estado</label>
                                <asp:DropDownList ID="ddlGestionarEstado" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="Pendiente" Text="Pendiente"/>
                                    <asp:ListItem Value="Atendida" Text="Atendida"/>
                                    <asp:ListItem Value="Atrasada" Text="Atrasada"/>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:Button ID="btnGuardarGestionarCita" runat="server" Text="Guardar Cambios" CssClass="btn btn-success" OnClick="btnGuardarGestionarCita_Click"/>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>