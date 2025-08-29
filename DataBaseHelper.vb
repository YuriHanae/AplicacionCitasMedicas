Imports System.Data.SqlClient

Public Class DataBaseHelper
    Private ReadOnly connectionString As String = ConfigurationManager.ConnectionStrings("Login").ConnectionString

    Public Function VerificarCredenciales(paciente As Paciente) As Boolean
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As New SqlCommand("SELECT  Usuario,Contraseña  FROM Usuarios WHERE Usuario = @Usuario AND Contraseña = @Contraseña AND ROL = @Rol", connection)
            command.Parameters.AddWithValue("@Usuario", paciente.Usuario)
            command.Parameters.AddWithValue("@Contraseña", paciente.Contraseña)
            command.Parameters.AddWithValue("@Rol", paciente.Rol)
            Dim reader As SqlDataReader = command.ExecuteReader()
            Return reader.HasRows
        End Using
    End Function

    Public Function ObtenerRolDeBaseDeDatos(usuario As String, contra As String) As String
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim cmd As New SqlCommand("SELECT Rol FROM Usuarios WHERE Usuario = @Usuario AND Contraseña = @Contraseña", conn)
            cmd.Parameters.AddWithValue("@Usuario", usuario)
            cmd.Parameters.AddWithValue("@Contraseña", contra)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Return reader("Rol").ToString()
            Else
                Return Nothing ' o String.Empty
            End If
        End Using
    End Function
End Class
