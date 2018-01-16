Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If MasterLib.CheckUser(Me.txtUserName.Text, Me.txtpwd.Text, "") Then
            Session("ssusername") = UCase(txtUserName.Text.ToString().Trim())
            Session("source") = "E-Sarmut"
            Session("cIdUser") = MasterLib.ShowData("vc_username", "vc_id", "pde_user", UCase(Me.txtUserName.Text), "")
            Dim role As String = MasterLib.GetRoleFromAccess("5001", "0000", "010000000000", Session("cIdUser"))
            If (role <> "") Then
                If (role = "Manager") Then
                    Session("role") = "Admin"
                Else
                    Session("role") = "User"
                End If
                Response.Redirect("Home.aspx")
            Else
                Session.Clear()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "onload", "<script language='javascript'>alert('Anda tidak berhak mengakses Aplikasi E-Sarmut!...');</script>")
            End If
        Else
            txtUserName.Focus()
        End If


    End Sub



End Class
