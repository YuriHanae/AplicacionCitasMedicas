Imports System.Data.SqlClient

Public Class Registrar
    Inherits System.Web.UI.Page

    Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs)
        Dim paciente As New Paciente With {
            .Nombre = txtNombre.Text.Trim(),
            .Apellido = txtApellido.Text.Trim(),
            .DNI = txtDNI.Text.Trim(),
            .Telefono = txtTelefono.Text.Trim(),
            .Email = txtEmail.Text.Trim(),
            .Usuario = txtUsuario.Text.Trim(),
            .Contraseña = txtContrasena.Text.Trim()
        }
        Dim nombre As String = paciente.Nombre
        Dim apellido As String = paciente.Apellido
        Dim dni As String = paciente.DNI
        Dim telefono As String = paciente.Telefono
        Dim email As String = paciente.Email
        Dim usuario As String = paciente.Usuario
        Dim wrapper As New Simple3Des("claveclavecita")
        Dim contrasena As String = wrapper.EncryptData(paciente.Contraseña)

        If nombre = "" Or apellido = "" Or dni = "" Or telefono = "" Or email = "" Or usuario = "" Or contrasena = "" Then
            lblMensaje.Text = "Todos los campos son obligatorios."
            lblMensaje.Visible = True
            Return
        End If

        ' Insertar en Pacientes y en Usuarios con transacción
        Dim connStr As String = ConfigurationManager.ConnectionStrings("Login").ConnectionString
        Using cn As New SqlConnection(connStr)
            cn.Open()
            Dim trans As SqlTransaction = cn.BeginTransaction()
            Try

                ' Insertar en Pacientes
                Dim cmdPaciente As New SqlCommand("INSERT INTO Pacientes (Nombre, Apellido, DNI, Telefono, Email, Usuario, Contraseña) VALUES (@Nombre, @Apellido, @DNI, @Telefono, @Email, @Usuario, @Contrasena)", cn, trans)
                cmdPaciente.Parameters.AddWithValue("@Nombre", nombre)
                cmdPaciente.Parameters.AddWithValue("@Apellido", apellido)
                cmdPaciente.Parameters.AddWithValue("@DNI", dni)
                cmdPaciente.Parameters.AddWithValue("@Telefono", telefono)
                cmdPaciente.Parameters.AddWithValue("@Email", email)
                cmdPaciente.Parameters.AddWithValue("@Usuario", usuario)
                cmdPaciente.Parameters.AddWithValue("@Contrasena", contrasena)
                cmdPaciente.ExecuteNonQuery()

                ' Insertar en Usuarios (solo Usuario y Contraseña, y Rol = 'Paciente')
                Dim cmdUsuario As New SqlCommand("INSERT INTO Usuarios (Usuario, Contraseña, Rol) VALUES (@Usuario, @Contrasena, 'Paciente')", cn, trans)
                cmdUsuario.Parameters.AddWithValue("@Usuario", usuario)
                cmdUsuario.Parameters.AddWithValue("@Contrasena", contrasena)
                cmdUsuario.ExecuteNonQuery()

                trans.Commit()
                Response.Redirect("Login.aspx?registro=ok")
            Catch ex As SqlException
                trans.Rollback()
                If ex.Number = 2627 Or ex.Number = 2601 Then 'Clave duplicada
                    lblMensaje.Text = "El usuario, correo, DNI o teléfono ya está registrado."
                Else
                    lblMensaje.Text = "Error al registrar: " & ex.Message
                End If
                lblMensaje.Visible = True
            End Try
        End Using
    End Sub
End Class