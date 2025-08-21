Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page
    Dim bd As New DataBaseHelper
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        If VerificarCredenciales() Then
            ' Asignar el rol a la sesión
            Session("Rol") = bd.ObtenerRolDeBaseDeDatos(txtUsuario.Text, txtPass.Text)

            If (Session("Rol") = "Admin") Then
                Response.Redirect("frmAdmin.aspx")
            ElseIf (Session("Rol") = "Paciente") Then
                Response.Redirect("frmPaciente.aspx")
            End If
        Else
            lblError.Text = "Credenciales inválidas"
            lblError.Visible = True
        End If
    End Sub

    Protected Function VerificarCredenciales() As Boolean

        Dim rol As String = bd.ObtenerRolDeBaseDeDatos(txtUsuario.Text, txtPass.Text)
        Dim paciente As New Paciente With {
            .Usuario = txtUsuario.Text.Trim(),
            .Contraseña = txtPass.Text.Trim(),
            .Rol = rol
        }
        Return bd.VerificarCredenciales(paciente)
    End Function


End Class