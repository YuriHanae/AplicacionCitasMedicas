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
            CargarDoctores() ' Se carga al inicio para que el dropdown esté listo
            MostrarCitasPaciente() ' Se muestra el historial desde el inicio
            ' Ya no se ocultan los paneles, porque se controlan con pestañas Bootstrap
        End If
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Response.Redirect("Login.aspx")
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
            Session("IdPaciente") = bd.ObtenerIdPaciente(Session("Usuario")) ' Asignamos el IdPaciente desde la base de datos
            Dim idPaciente As Integer = Session("IdPaciente")
            Dim idDoctor As Integer = Integer.Parse(ddlDoctor.SelectedValue)
            Dim fecha As Date = Date.Parse(txtFecha.Text)
            Dim hora As TimeSpan = TimeSpan.Parse(txtHora.Text)
            Dim obs As String = txtObservaciones.Text

            Using cn As New SqlConnection(ConfigurationManager.ConnectionStrings("Login").ConnectionString)
                cn.Open()
                Dim cmd As New SqlCommand("INSERT INTO Citas (Fecha, Hora, IdPaciente, IdDoctor, Estado, Observaciones) VALUES (@Fecha, @Hora, @IdPaciente, @IdDoctor, @Estado, @Obs)", cn)
                cmd.Parameters.AddWithValue("@Fecha", fecha)
                cmd.Parameters.AddWithValue("@Hora", hora)
                cmd.Parameters.AddWithValue("@IdPaciente", idPaciente)
                cmd.Parameters.AddWithValue("@IdDoctor", idDoctor)
                cmd.Parameters.AddWithValue("@Estado", "Pendiente")
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
                Session("IdPaciente") = bd.ObtenerIdPaciente(Session("Usuario"))
                Dim cmd As New SqlCommand("SELECT Fecha, Hora, Estado ,Observaciones FROM Citas WHERE Id = @Id AND IdPaciente = @IdPaciente", cn)
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
            Session("IdPaciente") = bd.ObtenerIdPaciente(Session("Usuario"))
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
            Dim cmd As New SqlCommand("SELECT * FROM Citas", cn)
            'cmd.Parameters.AddWithValue("@IdPaciente", Session("IdPaciente"))
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        End Using
        gvDatos.DataSource = dt
        gvDatos.DataBind()
    End Sub

End Class