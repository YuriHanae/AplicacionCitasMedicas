Imports System.Data.SqlClient

Public Class frmPaciente
    Inherits System.Web.UI.Page
    Dim bd As New DataBaseHelper

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("Rol") Is Nothing OrElse Session("Rol").ToString() <> "Paciente" Then
                Response.Redirect("Login.aspx")
            End If
            lblNombrePaciente.Text = Session("NombrePaciente")
            OcultarTodosLosPaneles()
        End If
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Response.Redirect("Login.aspx")
    End Sub

    Protected Sub btnCrearCita_Click(sender As Object, e As EventArgs)
        OcultarTodosLosPaneles()
        pnlCrearCita.Visible = True
        CargarDoctores()
    End Sub

    Protected Sub btnEditarCita_Click(sender As Object, e As EventArgs)
        OcultarTodosLosPaneles()
        pnlEditarCitaBuscar.Visible = True
    End Sub

    Protected Sub btnMostrarCitas_Click(sender As Object, e As EventArgs)
        OcultarTodosLosPaneles()
        pnlMostrarCitas.Visible = True
        MostrarCitasPaciente()
    End Sub

    Private Sub OcultarTodosLosPaneles()
        pnlCrearCita.Visible = False
        pnlEditarCitaBuscar.Visible = False
        pnlEditarCitaForm.Visible = False
        pnlMostrarCitas.Visible = False
    End Sub

    Private Sub CargarDoctores()
        ' Llena el DropDownList con los doctores de la base de datos
        Dim dt As New DataTable()
        Using cn As New SqlConnection(ConfigurationManager.ConnectionStrings("Login").ConnectionString)
            cn.Open()
            Dim cmd As New SqlCommand("SELECT Id, Nombre, Especialidad FROM Doctores", cn)
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        End Using
        ddlDoctor.DataSource = dt
        ddlDoctor.DataTextField = "Nombre"
        ddlDoctor.DataValueField = "Id"
        ddlDoctor.DataBind()
    End Sub

    Protected Sub btnGuardarCita_Click(sender As Object, e As EventArgs)
        Try
            Dim idPaciente As Integer = Session("IdPaciente")
            Dim idDoctor As Integer = Integer.Parse(ddlDoctor.SelectedValue)
            Dim fecha As Date = Date.Parse(txtFecha.Text)
            Dim hora As TimeSpan = TimeSpan.Parse(txtHora.Text)
            Dim obs As String = txtObservaciones.Text

            Using cn As New SqlConnection(ConfigurationManager.ConnectionStrings("Login").ConnectionString)
                cn.Open()
                Dim cmd As New SqlCommand("INSERT INTO Citas (Fecha, Hora, IdPaciente, IdDoctor, Observaciones) VALUES (@Fecha, @Hora, @IdPaciente, @IdDoctor, @Obs)", cn)
                cmd.Parameters.AddWithValue("@Fecha", fecha)
                cmd.Parameters.AddWithValue("@Hora", hora)
                cmd.Parameters.AddWithValue("@IdPaciente", idPaciente)
                cmd.Parameters.AddWithValue("@IdDoctor", idDoctor)
                cmd.Parameters.AddWithValue("@Obs", obs)
                cmd.ExecuteNonQuery()
            End Using
            lblCrearCitaError.Visible = False
            pnlCrearCita.Visible = False
            pnlMostrarCitas.Visible = True
            MostrarCitasPaciente()
        Catch ex As Exception
            lblCrearCitaError.Text = "Error: " & ex.Message
            lblCrearCitaError.Visible = True
        End Try
    End Sub

    Protected Sub btnBuscarCita_Click(sender As Object, e As EventArgs)
        Dim idCita As Integer
        If Integer.TryParse(txtBuscarIdCita.Text, idCita) Then
            Using cn As New SqlConnection(ConfigurationManager.ConnectionStrings("Login").ConnectionString)
                cn.Open()
                Dim cmd As New SqlCommand("SELECT Fecha, Hora, Observaciones FROM Citas WHERE Id = @Id AND IdPaciente = @IdPaciente", cn)
                cmd.Parameters.AddWithValue("@Id", idCita)
                cmd.Parameters.AddWithValue("@IdPaciente", Session("IdPaciente"))
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    txtEditFecha.Text = reader("Fecha").ToString()
                    txtEditHora.Text = reader("Hora").ToString()
                    txtEditObservaciones.Text = reader("Observaciones").ToString()
                    pnlEditarCitaForm.Visible = True
                    pnlEditarCitaBuscar.Visible = False
                    Session("CitaEditarId") = idCita
                Else
                    lblEditarCitaError.Text = "No se encontró cita para editar."
                    lblEditarCitaError.Visible = True
                End If
            End Using
        Else
            lblEditarCitaError.Text = "Ingrese un ID de cita válido."
            lblEditarCitaError.Visible = True
        End If
    End Sub

    Protected Sub btnActualizarCita_Click(sender As Object, e As EventArgs)
        Dim idCita As Integer = CInt(Session("CitaEditarId"))
        Using cn As New SqlConnection(ConfigurationManager.ConnectionStrings("Login").ConnectionString)
            cn.Open()
            Dim cmd As New SqlCommand("UPDATE Citas SET Fecha=@Fecha, Hora=@Hora, Observaciones=@Obs WHERE Id=@Id AND IdPaciente=@IdPaciente", cn)
            cmd.Parameters.AddWithValue("@Fecha", Date.Parse(txtEditFecha.Text))
            cmd.Parameters.AddWithValue("@Hora", TimeSpan.Parse(txtEditHora.Text))
            cmd.Parameters.AddWithValue("@Obs", txtEditObservaciones.Text)
            cmd.Parameters.AddWithValue("@Id", idCita)
            cmd.Parameters.AddWithValue("@IdPaciente", Session("IdPaciente"))
            cmd.ExecuteNonQuery()
        End Using
        pnlEditarCitaForm.Visible = False
        pnlMostrarCitas.Visible = True
        MostrarCitasPaciente()
    End Sub

    Private Sub MostrarCitasPaciente()
        Dim dt As New DataTable()
        Using cn As New SqlConnection(ConfigurationManager.ConnectionStrings("Login").ConnectionString)
            cn.Open()
            Dim cmd As New SqlCommand("
                SELECT Citas.Id, Citas.Fecha, Citas.Hora, Doctores.Nombre AS NombreDoctor, Citas.Estado, Citas.Observaciones
                FROM Citas 
                INNER JOIN Doctores ON Citas.IdDoctor = Doctores.Id
                WHERE IdPaciente = @IdPaciente
                ORDER BY Citas.Fecha DESC, Citas.Hora DESC", cn)
            cmd.Parameters.AddWithValue("@IdPaciente", Session("IdPaciente"))
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        End Using
        gvCitas.DataSource = dt
        gvCitas.DataBind()
    End Sub

End Class