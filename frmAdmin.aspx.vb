Imports System.Data.SqlClient

Public Class frmAdmin
    Inherits System.Web.UI.Page

    Private conexionStr As String = "Data Source=.;Initial Catalog=CitasMedicasDB;Integrated Security=True"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarDoctores()
            OcultarMensaje()
        End If
    End Sub

    Private Sub CargarDoctores()
        Using conexion As New SqlConnection(conexionStr)
            Dim adaptador As New SqlDataAdapter("SELECT Id, Nombre, Especialidad, Telefono, Email, Usuario, Contraseña FROM Doctores", conexion)
            Dim tabla As New DataTable()
            adaptador.Fill(tabla)
            gvDoctores.DataSource = tabla
            gvDoctores.DataBind()
        End Using
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Page.IsValid Then
            Dim nombre = txtNombre.Text.Trim()
            Dim especialidad = txtEspecialidad.Text.Trim()
            Dim telefono = txtTelefono.Text.Trim()
            Dim email = txtEmail.Text.Trim()
            Dim usuario = txtUsuario.Text.Trim()
            Dim contrasena = txtContrasena.Text.Trim()

            Try
                Using conexion As New SqlConnection(conexionStr)
                    conexion.Open()
                    Dim comando As SqlCommand

                    If String.IsNullOrEmpty(hfIdDoctor.Value) Then
                        ' Insertar nuevo doctor
                        comando = New SqlCommand("INSERT INTO Doctores (Nombre, Especialidad, Telefono, Email, Usuario, Contraseña) VALUES (@Nombre, @Especialidad, @Telefono, @Email, @Usuario, @Contrasena)", conexion)
                    Else
                        ' Actualizar doctor existente
                        comando = New SqlCommand("UPDATE Doctores SET Nombre=@Nombre, Especialidad=@Especialidad, Telefono=@Telefono, Email=@Email, Usuario=@Usuario, Contraseña=@Contrasena WHERE Id=@Id", conexion)
                        comando.Parameters.AddWithValue("@Id", hfIdDoctor.Value)
                    End If

                    comando.Parameters.AddWithValue("@Nombre", nombre)
                    comando.Parameters.AddWithValue("@Especialidad", especialidad)
                    comando.Parameters.AddWithValue("@Telefono", telefono)
                    comando.Parameters.AddWithValue("@Email", email)
                    comando.Parameters.AddWithValue("@Usuario", usuario)
                    comando.Parameters.AddWithValue("@Contrasena", contrasena)

                    comando.ExecuteNonQuery()
                End Using

                MostrarMensaje("✅ Datos guardados correctamente.", "success")
                LimpiarFormulario()
                CargarDoctores()
            Catch ex As Exception
                MostrarMensaje("❌ Error al guardar: " & ex.Message, "danger")
            End Try
        End If
    End Sub

    Protected Sub gvDoctores_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvDoctores.RowEditing
        Dim id = gvDoctores.DataKeys(e.NewEditIndex).Value.ToString()

        Using conexion As New SqlConnection(conexionStr)
            Dim comando As New SqlCommand("SELECT * FROM Doctores WHERE Id=@Id", conexion)
            comando.Parameters.AddWithValue("@Id", id)
            conexion.Open()
            Dim lector = comando.ExecuteReader()
            If lector.Read() Then
                hfIdDoctor.Value = lector("Id").ToString()
                txtNombre.Text = lector("Nombre").ToString()
                txtEspecialidad.Text = lector("Especialidad").ToString()
            End If
        End Using

        OcultarMensaje()
    End Sub

    Protected Sub gvDoctores_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvDoctores.RowDeleting
        Dim id = gvDoctores.DataKeys(e.RowIndex).Value.ToString()

        Try
            Using conexion As New SqlConnection(conexionStr)
                Dim comando As New SqlCommand("DELETE FROM Doctores WHERE Id=@Id", conexion)
                comando.Parameters.AddWithValue("@Id", id)
                conexion.Open()
                comando.ExecuteNonQuery()
            End Using

            MostrarMensaje(" Doctor eliminado correctamente.", "success")
            CargarDoctores()
        Catch ex As Exception
            MostrarMensaje("Error al eliminar: " & ex.Message, "danger")
        End Try
    End Sub
    Protected Sub gvDoctores_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvDoctores.RowCancelingEdit
        gvDoctores.EditIndex = -1
        CargarDoctores()
        OcultarMensaje()
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        LimpiarFormulario()
        OcultarMensaje()
    End Sub

    Private Sub LimpiarFormulario()
        txtNombre.Text = ""
        txtEspecialidad.Text = ""
        hfIdDoctor.Value = ""
    End Sub

    Private Sub MostrarMensaje(texto As String, tipo As String)
        lblMensaje.Text = texto
        lblMensaje.CssClass = "alert alert-" & tipo & " d-block"
    End Sub

    Private Sub OcultarMensaje()
        lblMensaje.Text = ""
        lblMensaje.CssClass = "d-none"
    End Sub
End Class