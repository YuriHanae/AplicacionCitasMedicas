Imports System.Data.SqlClient

Public Class DataBaseHelper
    Private ReadOnly connectionString As String = ConfigurationManager.ConnectionStrings("Login").ConnectionString

    Public Function VerificarCredenciales(usuarios As String, contrasena As String, rol As String) As Boolean
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As New SqlCommand("SELECT  Usuario,Contraseña  FROM Usuarios WHERE Usuario = @Usuario AND Contraseña = @Contraseña AND ROL = @Rol", connection)
            command.Parameters.AddWithValue("@Usuario", usuarios)
            command.Parameters.AddWithValue("@Contraseña", contrasena)
            command.Parameters.AddWithValue("@Rol", rol)
            Dim reader As SqlDataReader = command.ExecuteReader()
            Return reader.HasRows
        End Using
    End Function

    Public Function ObtenerRolDeBaseDeDatos(usuario, contrasena) As String
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim cmd As New SqlCommand("SELECT Rol FROM Usuarios WHERE Usuario = @Usuario AND Contraseña = @Contraseña", conn)
            cmd.Parameters.AddWithValue("@Usuario", usuario)
            cmd.Parameters.AddWithValue("@Contraseña", contrasena)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Return reader("Rol").ToString()
            Else
                Return Nothing ' o String.Empty
            End If
        End Using
    End Function

    Public Function RegistrarUsuario(paciente As Paciente) As Boolean
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As New SqlCommand("INSERT INTO Usuarios (Usuario, Contraseña, Rol) VALUES (@Usuario, @Contraseña, @Rol)", connection)
            command.Parameters.AddWithValue("@Usuario", paciente.Usuario)
            command.Parameters.AddWithValue("@Contraseña", paciente.Contraseña)
            command.Parameters.AddWithValue("@Rol", paciente.Rol)
            Dim rowsAffected As Integer = command.ExecuteNonQuery()
            Return rowsAffected > 0
        End Using
    End Function

    Public Function ObtenerIdPaciente(usuario As String) As Integer?
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As New SqlCommand("  SELECT P.Id FROM Pacientes P INNER JOIN Usuarios U ON U.Usuario = P.Usuario AND U.Contraseña = P.Contraseña WHERE U.Usuario = @Usuario", connection)
            command.Parameters.AddWithValue("@Usuario", usuario)
            Dim result As Object = command.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return Convert.ToInt32(result)
            Else
                Return Nothing
            End If
        End Using
    End Function
End Class