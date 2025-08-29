Imports System.Data.SqlClient

Public Class frmAdmin
    Inherits System.Web.UI.Page

    Private ReadOnly connStr As String = ConfigurationManager.ConnectionStrings("Login").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarDoctores()
            CargarListas()
            CargarCitas()
            LimpiarFormulario()
            LimpiarFormularioCita()
            OcultarMensaje()
            pnlCitasPendientes.Visible = False
            pnlHistorialPaciente.Visible = False
            pnlGestionarCita.Visible = False
            pnlEditarCitaAdmin.Visible = False
        End If
    End Sub

    ' ----------- DOCTORES (NO TOCAR) -----------
    Private Sub CargarDoctores()
        Using cn As New SqlConnection(connStr)
            Dim adaptador As New SqlDataAdapter("SELECT Id, Nombre, Especialidad, Telefono, Email, Usuario, Contraseña FROM Doctores", cn)
            Dim tabla As New DataTable()
            adaptador.Fill(tabla)
            gvDoctores.DataSource = tabla
            gvDoctores.DataBind()
        End Using
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim nombre As String = txtNombre.Text.Trim()
        Dim especialidad As String = txtEspecialidad.Text.Trim()
        Dim telefono As String = txtTelefono.Text.Trim()
        Dim email As String = txtEmail.Text.Trim()
        Dim usuario As String = txtUsuario.Text.Trim()
        Dim wrapper As New Simple3Des("claveclavecita")
        Dim contrasena As String = wrapper.EncryptData(txtContrasena.Text)

        If nombre = "" Or especialidad = "" Or telefono = "" Or email = "" Or usuario = "" Or contrasena = "" Then
            lblMensaje.Text = "Todos los campos son obligatorios."
            lblMensaje.CssClass = "alert alert-danger"
            lblMensaje.Visible = True
            Return
        End If

        Using cn As New SqlConnection(connStr)
            cn.Open()
            Dim trans As SqlTransaction = cn.BeginTransaction()
            Try
                Dim empleado As New Doctor With {
                    .Nombre = nombre,
                    .Especialidad = especialidad,
                    .Telefono = telefono,
                    .Email = email,
                    .Usuario = usuario,
                    .Contraseña = contrasena
                }
                Dim cmdDoctor As New SqlCommand("INSERT INTO Doctores (Nombre, Especialidad, Telefono, Email, Usuario, Contraseña) VALUES (@Nombre, @Especialidad, @Telefono, @Email, @Usuario, @Contrasena)", cn, trans)
                cmdDoctor.Parameters.AddWithValue("@Nombre", empleado.Nombre)
                cmdDoctor.Parameters.AddWithValue("@Especialidad", empleado.Especialidad)
                cmdDoctor.Parameters.AddWithValue("@Telefono", empleado.Telefono)
                cmdDoctor.Parameters.AddWithValue("@Email", empleado.Email)
                cmdDoctor.Parameters.AddWithValue("@Usuario", empleado.Usuario)
                cmdDoctor.Parameters.AddWithValue("@Contrasena", empleado.Contraseña)
                cmdDoctor.ExecuteNonQuery()

                Dim cmdUsuario As New SqlCommand("INSERT INTO Usuarios (Usuario, Contraseña, Rol) VALUES (@Usuario, @Contrasena, 'Admin')", cn, trans)
                cmdUsuario.Parameters.AddWithValue("@Usuario", empleado.Usuario)
                cmdUsuario.Parameters.AddWithValue("@Contrasena", empleado.Contraseña)
                cmdUsuario.ExecuteNonQuery()

                trans.Commit()
                LimpiarFormulario()
                CargarDoctores()
                lblMensaje.Text = "Doctor registrado correctamente."
                lblMensaje.CssClass = "alert alert-success"
                lblMensaje.Visible = True
            Catch ex As SqlException
                trans.Rollback()
                If ex.Number = 2627 Or ex.Number = 2601 Then
                    lblMensaje.Text = "El usuario, correo o teléfono ya está registrado."
                Else
                    lblMensaje.Text = "Error al registrar: " & ex.Message
                End If
                lblMensaje.CssClass = "alert alert-danger"
                lblMensaje.Visible = True
            End Try
        End Using
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        LimpiarFormulario()
        OcultarMensaje()
    End Sub

    Private Sub LimpiarFormulario()
        txtNombre.Text = ""
        txtEspecialidad.Text = ""
        txtTelefono.Text = ""
        txtEmail.Text = ""
        txtUsuario.Text = ""
        txtContrasena.Text = ""
    End Sub

    Private Sub OcultarMensaje()
        lblMensaje.Text = ""
        lblMensaje.Visible = False
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Login.aspx")
    End Sub

    ' ----------- CITAS (SOLO AGREGADO) -----------
    Protected Sub btnMostrarCitasPendientes_Click(sender As Object, e As EventArgs)
        pnlCitasPendientes.Visible = True
        pnlHistorialPaciente.Visible = False
        pnlGestionarCita.Visible = False
        pnlEditarCitaAdmin.Visible = False
        Using cn As New SqlConnection(connStr)
            Dim query As String = "SELECT C.Id, C.Fecha, C.Hora, P.Nombre AS NombrePaciente, D.Nombre AS NombreDoctor, C.Estado, C.Observaciones
                               FROM Citas C
                               INNER JOIN Pacientes P ON C.IdPaciente = P.Id
                               INNER JOIN Doctores D ON C.IdDoctor = D.Id
                               WHERE C.Estado = 'Pendiente'
                               ORDER BY C.Fecha, C.Hora"
            Dim da As New SqlDataAdapter(query, cn)
            Dim dt As New DataTable()
            da.Fill(dt)
            gvCitasPendientes.DataSource = dt
            gvCitasPendientes.DataBind()
        End Using
        LimpiarFormularioCita()
        CargarCitas()
        CargarListas()
    End Sub

    Protected Sub btnBuscarHistorialPaciente_Click(sender As Object, e As EventArgs)
        pnlCitasPendientes.Visible = False
        pnlHistorialPaciente.Visible = True
        pnlGestionarCita.Visible = False
        pnlEditarCitaAdmin.Visible = False
        gvHistorialPaciente.DataSource = Nothing
        gvHistorialPaciente.DataBind()
        lblHistorialPacienteMsg.Visible = False
    End Sub

    Protected Sub btnBuscarDniPaciente_Click(sender As Object, e As EventArgs)
        gvHistorialPaciente.DataSource = Nothing
        gvHistorialPaciente.DataBind()
        lblHistorialPacienteMsg.Visible = False

        Dim dni As String = txtBuscarDniPaciente.Text.Trim()
        If dni = "" Then
            lblHistorialPacienteMsg.Text = "Ingrese un DNI."
            lblHistorialPacienteMsg.Visible = True
            Return
        End If
        Using cn As New SqlConnection(connStr)
            cn.Open()
            Dim cmd As New SqlCommand("SELECT Id FROM Pacientes WHERE DNI = @Dni", cn)
            cmd.Parameters.AddWithValue("@Dni", dni)
            Dim idPacienteObj = cmd.ExecuteScalar()
            If idPacienteObj Is Nothing Then
                lblHistorialPacienteMsg.Text = "No se encontró paciente con ese DNI."
                lblHistorialPacienteMsg.Visible = True
                Return
            End If
            Dim idPaciente As Integer = CInt(idPacienteObj)
            Dim consulta As String = "SELECT Id, Fecha, Hora, Estado, Observaciones FROM Citas WHERE IdPaciente = @IdPaciente ORDER BY Fecha DESC, Hora DESC"
            Dim da As New SqlDataAdapter(consulta, cn)
            da.SelectCommand.Parameters.AddWithValue("@IdPaciente", idPaciente)
            Dim dt As New DataTable()
            da.Fill(dt)
            gvHistorialPaciente.DataSource = dt
            gvHistorialPaciente.DataBind()
        End Using
        LimpiarFormularioCita()
        CargarListas()
    End Sub

    Protected Sub btnGestionarCita_Click(sender As Object, e As EventArgs)
        pnlCitasPendientes.Visible = False
        pnlHistorialPaciente.Visible = False
        pnlGestionarCita.Visible = True
        pnlEditarCitaAdmin.Visible = False
        lblGestionarCitaMsg.Visible = False
    End Sub

    Protected Sub btnBuscarGestionCita_Click(sender As Object, e As EventArgs)
        pnlEditarCitaAdmin.Visible = False
        lblGestionarCitaMsg.Visible = False

        Dim dni As String = txtGestionarDni.Text.Trim()
        Dim idCitaStr As String = txtGestionarIdCita.Text.Trim()
        If dni = "" OrElse idCitaStr = "" Then
            lblGestionarCitaMsg.Text = "Ingrese el DNI y el ID de la cita."
            lblGestionarCitaMsg.Visible = True
            Return
        End If

        Dim idCita As Integer
        If Not Integer.TryParse(idCitaStr, idCita) Then
            lblGestionarCitaMsg.Text = "El ID de cita debe ser un número."
            lblGestionarCitaMsg.Visible = True
            Return
        End If

        Using cn As New SqlConnection(connStr)
            cn.Open()
            Dim cmdPaciente As New SqlCommand("SELECT Id FROM Pacientes WHERE DNI = @Dni", cn)
            cmdPaciente.Parameters.AddWithValue("@Dni", dni)
            Dim idPacienteObj = cmdPaciente.ExecuteScalar()
            If idPacienteObj Is Nothing Then
                lblGestionarCitaMsg.Text = "No se encontró paciente con ese DNI."
                lblGestionarCitaMsg.Visible = True
                Return
            End If
            Dim idPaciente As Integer = CInt(idPacienteObj)

            Dim cmdCita As New SqlCommand("SELECT * FROM Citas WHERE Id = @IdCita AND IdPaciente = @IdPaciente", cn)
            cmdCita.Parameters.AddWithValue("@IdCita", idCita)
            cmdCita.Parameters.AddWithValue("@IdPaciente", idPaciente)
            Dim reader = cmdCita.ExecuteReader()
            If reader.Read() Then
                txtGestionarFecha.Text = Convert.ToDateTime(reader("Fecha")).ToString("yyyy-MM-dd")
                txtGestionarHora.Text = TimeSpan.Parse(reader("Hora").ToString()).ToString("hh\:mm")
                ddlGestionarEstado.SelectedValue = reader("Estado").ToString()
                pnlEditarCitaAdmin.Visible = True
                ViewState("GestionarIdCita") = idCita
            Else
                lblGestionarCitaMsg.Text = "No se encontró esa cita para ese paciente."
                lblGestionarCitaMsg.Visible = True
            End If
            reader.Close()
        End Using
        LimpiarFormularioCita()
        CargarListas()
    End Sub

    Protected Sub btnGuardarGestionarCita_Click(sender As Object, e As EventArgs)
        lblGestionarCitaMsg.Visible = False
        Dim idCita = ViewState("GestionarIdCita")
        If idCita Is Nothing Then
            lblGestionarCitaMsg.Text = "No hay cita seleccionada para gestionar."
            lblGestionarCitaMsg.Visible = True
            Return
        End If
        Dim nuevaFecha As Date
        Dim nuevaHora As TimeSpan
        If Not Date.TryParse(txtGestionarFecha.Text, nuevaFecha) Then
            lblGestionarCitaMsg.Text = "Fecha inválida."
            lblGestionarCitaMsg.Visible = True
            Return
        End If
        If Not TimeSpan.TryParse(txtGestionarHora.Text, nuevaHora) Then
            lblGestionarCitaMsg.Text = "Hora inválida."
            lblGestionarCitaMsg.Visible = True
            Return
        End If
        Dim nuevoEstado As String = ddlGestionarEstado.SelectedValue
        Using cn As New SqlConnection(connStr)
            cn.Open()
            Dim cmd As New SqlCommand("UPDATE Citas SET Fecha=@Fecha, Hora=@Hora, Estado=@Estado WHERE Id=@Id", cn)
            cmd.Parameters.AddWithValue("@Fecha", nuevaFecha)
            cmd.Parameters.AddWithValue("@Hora", nuevaHora)
            cmd.Parameters.AddWithValue("@Estado", nuevoEstado)
            cmd.Parameters.AddWithValue("@Id", idCita)
            cmd.ExecuteNonQuery()
        End Using
        lblGestionarCitaMsg.Text = "Cambios guardados correctamente."
        lblGestionarCitaMsg.CssClass = "text-success mb-2"
        lblGestionarCitaMsg.Visible = True
        pnlEditarCitaAdmin.Visible = False
        btnMostrarCitasPendientes_Click(Nothing, Nothing)
        LimpiarFormularioCita()
        CargarListas()
    End Sub

    Public Sub LimpiarFormularioCita()
        hfIdCita.Value = ""
        txtFecha.Text = ""
        txtHora.Text = ""
        ddlPaciente.ClearSelection()
        ddlDoctor.ClearSelection()
        ddlEstado.ClearSelection()
        txtObservaciones.Text = ""
        lblCitasMensaje.Text = ""
        lblCitasMensaje.CssClass = "alert d-none"
    End Sub

    Public Sub CargarListas()
        Using cn As New SqlConnection(connStr)
            cn.Open()
            ' Pacientes
            Dim cmdPacientes As New SqlCommand("SELECT Id, Nombre FROM Pacientes", cn)
            Dim lectorP = cmdPacientes.ExecuteReader()
            ddlPaciente.Items.Clear()
            ddlPaciente.Items.Add(New ListItem("Seleccione...", ""))
            While lectorP.Read()
                ddlPaciente.Items.Add(New ListItem(lectorP("Nombre").ToString(), lectorP("Id").ToString()))
            End While
            lectorP.Close()
            ' Doctores
            Dim cmdDoctores As New SqlCommand("SELECT Id, Nombre FROM Doctores", cn)
            Dim lectorD = cmdDoctores.ExecuteReader()
            ddlDoctor.Items.Clear()
            ddlDoctor.Items.Add(New ListItem("Seleccione...", ""))
            While lectorD.Read()
                ddlDoctor.Items.Add(New ListItem(lectorD("Nombre").ToString(), lectorD("Id").ToString()))
            End While
        End Using
    End Sub

    Public Sub CargarCitas()
        Using cn As New SqlConnection(connStr)
            Dim consulta As String = "
            SELECT C.Id, C.Fecha, C.Hora, P.Nombre AS NombrePaciente, D.Nombre AS NombreDoctor, C.Estado, C.Observaciones
            FROM Citas C
            INNER JOIN Pacientes P ON C.IdPaciente = P.Id
            INNER JOIN Doctores D ON C.IdDoctor = D.Id"
            Dim adaptador As New SqlDataAdapter(consulta, cn)
            Dim tabla As New DataTable()
            adaptador.Fill(tabla)
            gvCitas.DataSource = tabla
            gvCitas.DataBind()
        End Using
    End Sub

    Protected Sub btnGuardarCita_Click(sender As Object, e As EventArgs)
        If Page.IsValid Then
            Dim fecha = Date.Parse(txtFecha.Text)
            Dim hora = TimeSpan.Parse(txtHora.Text)
            Dim idPaciente = ddlPaciente.SelectedValue
            Dim idDoctor = ddlDoctor.SelectedValue
            Dim estado = ddlEstado.SelectedValue
            Dim observaciones = txtObservaciones.Text.Trim()

            Using cn As New SqlConnection(connStr)
                cn.Open()
                Dim cmd As SqlCommand
                If String.IsNullOrEmpty(hfIdCita.Value) Then
                    cmd = New SqlCommand("INSERT INTO Citas (Fecha, Hora, IdPaciente, IdDoctor, Estado, Observaciones) VALUES (@Fecha, @Hora, @IdPaciente, @IdDoctor, @Estado, @Observaciones)", cn)
                Else
                    cmd = New SqlCommand("UPDATE Citas SET Fecha=@Fecha, Hora=@Hora, IdPaciente=@IdPaciente, IdDoctor=@IdDoctor, Estado=@Estado, Observaciones=@Observaciones WHERE Id=@Id", cn)
                    cmd.Parameters.AddWithValue("@Id", hfIdCita.Value)
                End If
                cmd.Parameters.AddWithValue("@Fecha", fecha)
                cmd.Parameters.AddWithValue("@Hora", hora)
                cmd.Parameters.AddWithValue("@IdPaciente", idPaciente)
                cmd.Parameters.AddWithValue("@IdDoctor", idDoctor)
                cmd.Parameters.AddWithValue("@Estado", estado)
                cmd.Parameters.AddWithValue("@Observaciones", observaciones)
                cmd.ExecuteNonQuery()
            End Using

            lblCitasMensaje.Text = "✅ Cita guardada correctamente."
            lblCitasMensaje.CssClass = "alert alert-success"
            LimpiarFormularioCita()

        End If
    End Sub

    Protected Sub btnCancelarCita_Click(sender As Object, e As EventArgs)
        LimpiarFormularioCita()

    End Sub

    Protected Sub gvCitas_RowEditing(sender As Object, e As GridViewEditEventArgs)
        Dim id = gvCitas.DataKeys(e.NewEditIndex).Value.ToString()

        Using cn As New SqlConnection(connStr)
            Dim cmd As New SqlCommand("SELECT * FROM Citas WHERE Id=@Id", cn)
            cmd.Parameters.AddWithValue("@Id", id)
            cn.Open()
            Dim lector = cmd.ExecuteReader()
            If lector.Read() Then
                hfIdCita.Value = lector("Id").ToString()
                txtFecha.Text = Convert.ToDateTime(lector("Fecha")).ToString("yyyy-MM-dd")
                txtHora.Text = TimeSpan.Parse(lector("Hora").ToString()).ToString("hh\:mm")
                ddlPaciente.SelectedValue = lector("IdPaciente").ToString()
                ddlDoctor.SelectedValue = lector("IdDoctor").ToString()
                ddlEstado.SelectedValue = lector("Estado").ToString()
                txtObservaciones.Text = lector("Observaciones").ToString()
            End If
        End Using

        gvCitas.EditIndex = -1
        CargarCitas()
    End Sub
End Class