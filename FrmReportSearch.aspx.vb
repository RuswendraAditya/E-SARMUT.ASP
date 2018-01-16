Imports System.Data
Imports System.Web.Configuration
Imports System.Data.SqlClient
Partial Class FrmReportSearch
    Inherits System.Web.UI.Page

    Protected Sub btnViewReport_Click(sender As Object, e As EventArgs) Handles btnViewReport.Click
        '        Response.Redirect("~/ReportViewer.aspx?" + "&month=&mode=create"")
        ' Response.Redirect("~/ReportViewer.aspx??" + "month=" + RadMonthYearPeriod.SelectedDate.Value.Month + "&year=" + RadMonthYearPeriod.SelectedDate.Value.Year + "&trans_id=")
        Dim kodeGugus As String = RadComboGustu.FindItemByText(RadComboGustu.Text).Value
        Dim period As Date = RadMonthYearPeriod.SelectedDate.Value
        Dim sarmut_id As Integer = RadComboSarmut.FindItemByText(RadComboSarmut.Text).Value
        Dim trans_id As Integer = (getTransIdSarmut(kodeGugus, period, sarmut_id))
        If (trans_id = 0) Then
            lblMsg.Text = "<div Class='alert alert-danger'><strong><span class='glyphicon glyphicon-send'></span> Data sasaran mutu atas periode ini tidak ada, mohon pastikan anda sudah entri </strong></div>"
        Else
            Response.Redirect("~/ReportViewer.aspx?&month=" + RadMonthYearPeriod.SelectedDate.Value.Month.ToString + "&year=" + RadMonthYearPeriod.SelectedDate.Value.Year.ToString + "&trans_id=" + trans_id.ToString + "&sarmut_id=" + sarmut_id.ToString)
        End If
    End Sub
    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("~/Home.aspx")
    End Sub

    Private Function getTransIdSarmut(ByVal kodeGugus As String, ByVal period As Date, ByVal sarmut_id As Integer) As Integer
        Dim trans_id As Integer = 0
        Dim strsql As String = "SELECT  in_transaction_id,dt_period ,TRANS.in_sarmut_id,  " & _
                  "sarmut.vc_nama_sarmut, trans.vc_k_gugus, " & _
                  "GUGUS.VC_n_gugus, trans.in_target, dc_pencapaian,Case TRANS.bt_hasil   " & _
                  "When 1 Then 'Tercapai'   " & _
                  "Else 'Tidak Tercapai'   " & _
                  "End As bt_hasil,vc_status   " & _
                  "From Mutu_Sarmut_Trans trans Join Mutu_Sarmut sarmut   " & _
                  "On trans.in_sarmut_id = sarmut.in_sarmut_id   " & _
                  "Join PubGugusSDM gugus   " & _
                  "On gugus.VC_k_gugus = sarmut.vc_k_gugus " & _
                   "where gugus.VC_k_gugus = '" & kodeGugus & "'And trans.in_sarmut_id ='" & sarmut_id & "'  And dt_period ='" & period & "' "
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()
        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            trans_id = dr("in_transaction_id")
        End While
        dr.Close()

        Return trans_id

    End Function



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            GetGugusTugas()
        End If
    End Sub

    Private Sub GetGugusTugas()
        With Me.RadComboGustu
            .DataTextField = "VC_n_gugus"
            .DataValueField = "VC_k_gugus"
            If (Session("role") = "User") Then
                .DataSource = MasterLib.SetDataSourceGugusSDMByGustu(Session("ssusername"), Session("cIdUser"))
            Else
                .DataSource = MasterLib.SetDataSourceGugusSDM
            End If
            .DataBind()
        End With
    End Sub

    Protected Sub RadComboGustu_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboGustu.SelectedIndexChanged
        loadSarmutGugus()
    End Sub
    Private Sub loadSarmutGugus()
        If (RadComboGustu.Text.Length > 0) Then
            Dim kode_gugus As String = RadComboGustu.FindItemByText(RadComboGustu.Text).Value
            Me.RadComboSarmut.Text = ""
            GetSarmutGustu(kode_gugus)
        End If


    End Sub

    Private Sub PESAN(ByVal cpesan As String)
        ClientScript.RegisterStartupScript(Me.GetType, "ClientSideScript", "<script type='text/javascript'>window.alert('" & cpesan & "')</script>")
    End Sub

    Private Sub GetSarmutGustu(ByVal kode_gugus As String)
        If kode_gugus <> "" Then
            With Me.RadComboSarmut
                Dim strSQL As String = ""
                Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
                Dim connection As SqlConnection = New SqlConnection(connectionString)
                Dim con As SqlConnection = New SqlConnection(connectionString)
                Dim adapter As SqlDataAdapter = New SqlDataAdapter("Select in_sarmut_id, vc_nama_sarmut FROM mutu_sarmut " & _
                            " where vc_k_gugus = '" & kode_gugus & "' ", con)
                Dim links As DataTable = New DataTable()
                adapter.Fill(links)
                .DataTextField = "vc_nama_sarmut"
                .DataValueField = "in_sarmut_id"
                .DataSource = links
                .DataBind()
            End With
        End If
    End Sub
End Class
