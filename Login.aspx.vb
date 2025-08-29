Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page
    Dim bd As New DataBaseHelper
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Dim wrapper As New Simple3Des("claveclavecita")
        Dim pass As String = wrapper.EncryptData(txtPass.Text)
        If VerificarCredenciales() Then
            ' Asignar el rol y el usuario a la sesión
            Session("Rol") = bd.ObtenerRolDeBaseDeDatos(txtUsuario.Text, pass)
            Session("Usuario") = txtUsuario.Text.Trim()

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

        Dim wrapper As New Simple3Des("claveclavecita")
        Dim pass As String = wrapper.EncryptData(txtPass.Text)
        Dim rol As String = bd.ObtenerRolDeBaseDeDatos(txtUsuario.Text.Trim(), pass)

        Return bd.VerificarCredenciales(txtUsuario.Text.Trim(), pass, rol)
    End Function


End Class