
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Sub mnu010101010000_click(ByVal sender As Object, ByVal e As EventArgs)
        If (MasterLib.GetAccess("5001", "0101", "010101010000", Session("cIdUser"))) Then
            Response.Redirect("~/FrmInquiryMasterSarmut.aspx")
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "onload", "<script language='javascript'>alert('Anda tidak berhak mengakses Menu Master Sarmut!...');</script>")
        End If

        'Response.Redirect("~/FrmEntryMasterSarmut.aspx")
    End Sub


    Sub mnu010101020000_click(ByVal sender As Object, ByVal e As EventArgs)
        If (MasterLib.GetAccess("5001", "0101", "010101010000", Session("cIdUser"))) Then
            Response.Redirect("~/FrmInquiryPejabatGustu.aspx")
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "onload", "<script language='javascript'>alert('Anda tidak berhak mengakses Pejabat Gugus Tugas!...');</script>")
        End If

        'Response.Redirect("~/FrmEntryMasterSarmut.aspx")
    End Sub

    Sub mnu010101030000_click(ByVal sender As Object, ByVal e As EventArgs)
        If (MasterLib.GetAccess("5001", "0101", "010101010000", Session("cIdUser"))) Then
            Response.Redirect("~/FrmInquiryPejabatMutu.aspx")
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "onload", "<script language='javascript'>alert('Anda tidak berhak mengakses Menu Pejabat Komite Mutu!...');</script>")
        End If

        'Response.Redirect("~/FrmEntryMasterSarmut.aspx")
    End Sub


    Sub mnu010102020000_click(ByVal sender As Object, ByVal e As EventArgs)
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "onload", "<script language='javascript'>alert('Menu Masih Dalam Tahap Pengembangan');</script>")
        Response.Redirect("~/FrmReportSearch.aspx")
    End Sub

    Sub mnu010102010000_click(ByVal sender As Object, ByVal e As EventArgs)
        If (MasterLib.GetAccess("5001", "0101", "010102010000", Session("cIdUser"))) Then
            Response.Redirect("~/FrmInquiryEntrySarmut.aspx")
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "onload", "<script language='javascript'>alert('Anda tidak berhak mengakses Menu Entry Sarmut!...');</script>")
        End If


        ' Response.Redirect("~/FrmEntrySarmut.aspx")
    End Sub

    Sub mnuLogout_click(ByVal sender As Object, ByVal e As EventArgs)
        Session.Clear()
        Response.Redirect("~/Default.aspx")

        ' Response.Redirect("~/FrmEntrySarmut.aspx")
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("ssusername") Is Nothing Then
            Response.Redirect("~/Default.aspx")
        Else
            lblUser.Text = Session("ssusername")

        End If



    End Sub
End Class

