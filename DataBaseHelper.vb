Imports System.Data.SqlClient

Public Class DataBaseHelper
    Private ReadOnly connectionString As String = ConfigurationManager.ConnectionStrings("Login").ConnectionString

    'Public Function GetPacientes() As List(Of Paciente)
    '    Dim pacientes As New List(Of Paciente)()
    '    Using connection As New SqlConnection(connectionString)
    '        connection.Open()
    '        Dim command As New SqlCommand("SELECT * FROM Pacientes", connection)
    '        Using reader As SqlDataReader = command.ExecuteReader()
    '            While reader.Read()
    '                Dim paciente As New Paciente()
    '                paciente.IdPaciente = reader("PacienteId")
    '                paciente.Nombre = reader("Nombre")
    '                paciente.Email = reader("Email")
    '                paciente.Telefono = reader("Telefono")
    '                paciente.Contrasena = reader("Contrasena")
    '                pacientes.Add(paciente)
    '            End While
    '        End Using
    '    End Using
    '    Return clientes
    'End Function

    'Public Sub InsertarCliente(cliente As Cliente)
    '    Using connection As New SqlConnection(connectionString)
    '        connection.Open()
    '        Dim command As New SqlCommand("INSERT INTO Clientes (Nombre, Email, Telefono) VALUES (@Nombre, @Email, @Telefono)", connection)
    '        command.Parameters.AddWithValue("@Nombre", cliente.Nombre)
    '        command.Parameters.AddWithValue("@Email", cliente.Email)
    '        command.Parameters.AddWithValue("@Telefono", cliente.Telefono)
    '        command.ExecuteNonQuery()
    '    End Using
    'End Sub

    'Public Sub ActualizarCliente(cliente As Cliente)
    '    Using connection As New SqlConnection(connectionString)
    '        connection.Open()
    '        Dim command As New SqlCommand("UPDATE Clientes SET Nombre = @Nombre, Email = @Email, Telefono = @Telefono WHERE IdCliente = @IdCliente", connection)
    '        command.Parameters.AddWithValue("@IdCliente", cliente.IdCliente)
    '        command.Parameters.AddWithValue("@Nombre", cliente.Nombre)
    '        command.Parameters.AddWithValue("@Email", cliente.Email)
    '        command.Parameters.AddWithValue("@Telefono", cliente.Telefono)
    '        command.ExecuteNonQuery()
    '    End Using
    'End Sub

    'Public Sub EliminarCliente(idCliente As Integer)
    '    Using connection As New SqlConnection(connectionString)
    '        connection.Open()
    '        Dim command As New SqlCommand("DELETE FROM Clientes WHERE IdCliente = @IdCliente", connection)
    '        command.Parameters.AddWithValue("@IdCliente", idCliente)
    '        command.ExecuteNonQuery()
    '    End Using
    'End Sub

    Public Function VerificarCredenciales(paciente As Paciente) As Boolean
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As New SqlCommand("SELECT  Usuario,Contraseña  FROM Usuarios WHERE Usuario = @Usuario AND Contraseña = @Contraseña", connection)
            command.Parameters.AddWithValue("@Usuario", paciente.Email)
            command.Parameters.AddWithValue("@Contraseña", paciente.Contraseña)
            Dim reader As SqlDataReader = command.ExecuteReader()
            Return reader.HasRows
        End Using
    End Function
End Class
