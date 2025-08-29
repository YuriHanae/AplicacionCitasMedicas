Imports System.Data.SqlClient

Public Class frmAdmin
    Inherits System.Web.UI.Page

    Private connStr As String = ConfigurationManager.ConnectionStrings("Login").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarDoctores()
            CargarListas()
            CargarCitas()
            LimpiarFormulario()
            LimpiarFormularioCita()
            OcultarMensaje()
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
        Dim contrasena As String = txtContrasena.Text.Trim()

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
                Dim cmdDoctor As New SqlCommand("INSERT INTO Doctores (Nombre, Especialidad, Telefono, Email, Usuario, Contraseña) VALUES (@Nombre, @Especialidad, @Telefono, @Email, @Usuario, @Contrasena)", cn, trans)
                cmdDoctor.Parameters.AddWithValue("@Nombre", nombre)
                cmdDoctor.Parameters.AddWithValue("@Especialidad", especialidad)
                cmdDoctor.Parameters.AddWithValue("@Telefono", telefono)
                cmdDoctor.Parameters.AddWithValue("@Email", email)
                cmdDoctor.Parameters.AddWithValue("@Usuario", usuario)
                cmdDoctor.Parameters.AddWithValue("@Contrasena", contrasena)
                cmdDoctor.ExecuteNonQuery()

                Dim cmdUsuario As New SqlCommand("INSERT INTO Usuarios (Usuario, Contraseña, Rol) VALUES (@Usuario, @Contrasena, 'Admin')", cn, trans)
                cmdUsuario.Parameters.AddWithValue("@Usuario", usuario)
                cmdUsuario.Parameters.AddWithValue("@Contrasena", contrasena)
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
    Private Sub CargarListas()
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

    Private Sub CargarCitas()
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
        End Using
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
            CargarCitas()
        End If
    End Sub

    Protected Sub btnCancelarCita_Click(sender As Object, e As EventArgs)
        LimpiarFormularioCita()
    End Sub

    Private Sub LimpiarFormularioCita()
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
End Class