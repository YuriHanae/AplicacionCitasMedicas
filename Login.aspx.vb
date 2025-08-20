Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page
    Dim bd As New DataBaseHelper
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        If VerificarCredenciales() Then
            Response.Redirect("Default.aspx")
        Else
            lblError.Text = "Credenciales inválidas"
            lblError.Visible = True
        End If
    End Sub

    Protected Function VerificarCredenciales() As Boolean

        Dim paciente As New Paciente With {
            .Usuario = txtUsuario.Text.Trim(),
            .Contraseña = txtPass.Text.Trim(),
            .Rol = bd.ObtenerRolDeBaseDeDatos(paciente)
        }
        If (Session("Rol") = "Admin") Then
            Response.Redirect("frmAdmin.aspx")

        ElseIf (Session("Rol") = "Paciente") Then
            Response.Redirect("frmPaciente.aspx")
        End If


        Dim helper As New DataBaseHelper()
        Return helper.VerificarCredenciales(paciente)
    End Function


End Class