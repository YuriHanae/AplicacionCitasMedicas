Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        If VerificarCredenciales() Then
            Response.Redirect("Default.aspx")

            lblError.Text = "Credenciales inválidas"
            lblError.Visible = True
        End If
    End Sub

    Protected Function VerificarCredenciales() As Boolean
        Dim paciente As New Paciente() With {
            .Email = txtEmail.Text,
            .Contraseña = txtPass.Text
        }

        Dim Helper As New DataBaseHelper()
        Return Helper.VerificarCredenciales(paciente)
    End Function
End Class