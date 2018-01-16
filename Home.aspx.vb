Imports System.Data
Imports System.Web.Configuration
Imports System.Data.SqlClient
Partial Class Home
    Inherits System.Web.UI.Page
    Protected Total As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim cph As ContentPlaceHolder
        'Dim lit As Literal

        'cph = CType(Master.FindControl("ContentPlaceHolder1"), _
        'ContentPlaceHolder)
        'If Not cph Is Nothing Then

        'End If
        If Session("role") = "Admin" Then
            Total = getCountByStatus("In Progress")
        Else
            btnNotif.Visible = False
        End If


    End Sub

    Private Function getCountByStatus(ByVal Status As String) As Integer
        Dim count As Integer = 0

        Dim strsql As String = ""
        strsql = " Select COUNT(*) as jumlah FROM mutu_sarmut_trans " & _
                 " WHERE vc_status = '" & Status & "' "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)


        While dr.Read
            count = dr("jumlah")
        End While
        dr.Close()
        Return count
    End Function

    Protected Sub btnNotif_Click(sender As Object, e As EventArgs) Handles btnNotif.Click
        Response.Redirect("~/FrmInquiryEntrySarmut.aspx")
    End Sub
End Class
