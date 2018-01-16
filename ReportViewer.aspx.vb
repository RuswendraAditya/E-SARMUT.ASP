Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms

Partial Class ReportViewer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


    End Sub

    Protected Sub Page_Init(ByVal sender As Object, _
                ByVal e As System.EventArgs) Handles Me.Init

        If Not Page.IsPostBack Then
            loadDataSarmutToReport()

        End If
    End Sub

    Private Function GetData(query As String, tableName As String) As SarmutDataSet
        Dim conString As String = ConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim cmd As New SqlCommand(query)
        Using con As New SqlConnection(conString)
            Using sda As New SqlDataAdapter()
                cmd.Connection = con

                sda.SelectCommand = cmd
                Using dsSarmuts As New SarmutDataSet()
                    sda.Fill(dsSarmuts, tableName)
                    Return dsSarmuts
                End Using
            End Using
        End Using
    End Function

    Private Sub loadDataSarmutToReport()
        Dim month As Integer = CInt(Request.QueryString("month"))
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SarmutReport.rdlc")
        '   Dim strsql As String = "SELECT SUBSTRING(Convert(Varchar(50),dt_period,106),3,10) as dt_period FROM Mutu_Sarmut_Trans WHERE in_transaction_id = 1 "
        Dim strsql As String = ""
        If (month <= 6) Then
            strsql = queryTrendSemeter1()
        Else
            strsql = queryTrendSemeter2()
        End If
        Dim dsSarmut As SarmutDataSet = GetData(strsql, "TableSarmutTrend")
        Dim datasource As New ReportDataSource("DataSet1", dsSarmut.Tables(1))

        'Dim strsql_2 As String = "SELECT SUBSTRING(Convert(Varchar(50),dt_period,106),3,10) as dt_period FROM Mutu_Sarmut_Trans WHERE in_transaction_id = 1 "

        Dim strsql_2 As String = " Select in_transaction_id, SUBSTRING(Convert(Varchar(50),dt_period,106),3,10) as dt_period, TRANS.in_sarmut_id, " & _
                 " sarmut.vc_nama_sarmut,SUBSTRING(Convert(Varchar(50),dt_effective_start_date,106),4,10) as dt_effective_start_date, trans.vc_k_gugus,trans.dt_created_date, " & _
                 " GUGUS.VC_n_gugus, trans.in_target, vc_note_pencapaian,dc_pencapaian,TRANS.bt_hasil, " & _
                 " vc_masalah,vc_status,vc_alasan_reject,karyawan.vc_nama_kry pejabat,(SELECT karyawan.vc_nama_kry FROM Mutu_Sarmut_Trans mutu INNer join SDMKaryawan karyawan " & _
                 " On mutu.vc_approver = karyawan.vc_nik WHERE mutu.in_transaction_id =trans.in_transaction_id  ) approver  " & _
                 " From Mutu_Sarmut_Trans trans Join Mutu_Sarmut sarmut " & _
                 " On trans.in_sarmut_id = sarmut.in_sarmut_id " & _
                 " Join PubGugusSDM gugus " & _
                 " On gugus.VC_k_gugus = sarmut.vc_k_gugus " & _
                 " Left JOIN SDMKaryawan karyawan " & _
                 " On vc_nik_pejabat = karyawan.vc_nik   " & _
                 " WHERE in_transaction_id = '" & Request.QueryString("trans_id") & "' "

        Dim strsql_3 As String = "SELECT vc_nama_tindakan,vc_pic,Convert(Varchar(50),dt_duedate,106) dt_duedate,vc_status FROM Mutu_Tindakan " & _
                   "  where in_transaction_id = '" & Request.QueryString("trans_id") & "' "

        Dim dsSarmut_2 As SarmutDataSet = GetData(strsql_2, "TableSarmut")
        Dim datasource_2 As New ReportDataSource("DataSet2", dsSarmut_2.Tables(0))
        Dim dsSarmut_3 As SarmutDataSet = GetData(strsql_3, "TableSarmutTindakan")
        Dim datasource_3 As New ReportDataSource("DataSet3", dsSarmut_3.Tables(2))
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(datasource)
        ReportViewer1.LocalReport.DataSources.Add(datasource_2)
        ReportViewer1.LocalReport.DataSources.Add(datasource_3)
        ReportViewer1.LocalReport.Refresh()
        Me.ReportViewer1.ShowPrintButton = True
    End Sub

    'Private Function queryTrendSemeter1() As String
    '    Dim month As String = Request.QueryString("month")
    '    Dim year As String = Request.QueryString("year")
    '    Dim Sql As String = " Select period, in_target target, dc_pencapaian pencapaian FROM ( " & _
    '    "Select 'Jan'  Period,'1' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
    '    "Left Join Mutu_Sarmut_Trans trans " & _
    '    "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
    '    "And Convert(Varchar(50),dt_period,105) =  '01-01-" + year + "' " & _
    '    "UNION " & _
    '    "Select 'Feb' Period,'2' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
    '    "Left Join Mutu_Sarmut_Trans trans " & _
    '    "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
    '    "And Convert(Varchar(50),dt_period,105) =  '01-02-" + year + "' " & _
    '    "UNION " & _
    '    "Select 'Mar' Period,'3' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
    '    "left Join Mutu_Sarmut_Trans trans " & _
    '    "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
    '    "And Convert(Varchar(50), dt_period, 105) = '01-03-" + year + "' " & _
    '    "UNION " & _
    '    "Select 'Apr' Period,'4' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut   " & _
    '    "Left Join Mutu_Sarmut_Trans trans " & _
    '    "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
    '    "And Convert(Varchar(50), dt_period,105) =  '01-04-" + year + "' " & _
    '    "UNION " & _
    '    "Select 'May' Period,'5' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut   " & _
    '    "Left Join Mutu_Sarmut_Trans trans " & _
    '    "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
    '    "And Convert(Varchar(50),dt_period,105) = '01-05-" + year + "' " & _
    '    "UNION " & _
    '    "Select 'Jun' Period, '6' seq,sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
    '    "Left Join Mutu_Sarmut_Trans trans " & _
    '    "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
    '    "And Convert(Varchar(50),dt_period,105) =  '01-06-" + year + "' ) temp " & _
    '    "where temp.in_sarmut_id = '" & Request.QueryString("sarmut_id") & "' order by seq asc "

    '    Return Sql
    'End Function
    Private Function queryTrendSemeter1() As String
        Dim month As Integer = CInt(Request.QueryString("month"))
        Dim year As String = Request.QueryString("year")
        Dim Sql As String = " Select period, in_target target, dc_pencapaian pencapaian FROM ( " & _
        "Select 'Jan'  Period,'1' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
        "Left Join Mutu_Sarmut_Trans trans " & _
        "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
        "And Convert(Varchar(50),dt_period,105) =  '01-01-" + year + "' "

        If (month = 2 Or month >= 2) Then
            Sql = Sql + "UNION " & _
                "Select 'Feb' Period,'2' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
                "Left Join Mutu_Sarmut_Trans trans " & _
                "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
                "And Convert(Varchar(50),dt_period,105) =  '01-02-" + year + "' "
        End If
        If (month = 3 Or month >= 3) Then
            Sql = Sql + "UNION " & _
                "Select 'Mar' Period,'3' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
                "left Join Mutu_Sarmut_Trans trans " & _
                "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
                "And Convert(Varchar(50), dt_period, 105) = '01-03-" + year + "' "
        End If
        If (month = 4 Or month >= 4) Then
            Sql = Sql + "UNION " & _
                "Select 'Apr' Period,'4' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
                "left Join Mutu_Sarmut_Trans trans " & _
                "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
                "And Convert(Varchar(50), dt_period, 105) = '01-04-" + year + "' "
        End If
        If (month = 5 Or month >= 5) Then
            Sql = Sql + "UNION " & _
                "Select 'May' Period,'5' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
                "left Join Mutu_Sarmut_Trans trans " & _
                "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
                "And Convert(Varchar(50), dt_period, 105) = '01-05-" + year + "' "
        End If
        If (month = 6 Or month >= 6) Then
            Sql = Sql + "UNION " & _
                "Select 'Jun' Period,'6' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
                "left Join Mutu_Sarmut_Trans trans " & _
                "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
                "And Convert(Varchar(50), dt_period, 105) = '01-06-" + year + "' "
        End If
        Sql = Sql + ") temp " & _
        "where temp.in_sarmut_id = '" & Request.QueryString("sarmut_id") & "' order by seq asc "
        Return Sql
    End Function

    Private Function queryTrendSemeter2() As String
        Dim month As Integer = CInt(Request.QueryString("month"))
        Dim year As String = Request.QueryString("year")
        Dim Sql As String = " Select period, in_target target, dc_pencapaian pencapaian FROM ( " & _
        "Select 'Jul'  Period,'1' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
        "Left Join Mutu_Sarmut_Trans trans " & _
        "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
        "And Convert(Varchar(50),dt_period,105) =  '01-07-" + year + "' "

        If (month = 8 Or month >= 8) Then
            Sql = Sql + "UNION " & _
                "Select 'Aug' Period,'2' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
                "Left Join Mutu_Sarmut_Trans trans " & _
                "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
                "And Convert(Varchar(50),dt_period,105) =  '01-08-" + year + "' "
        End If
        If (month = 9 Or month >= 9) Then
            Sql = Sql + "UNION " & _
                "Select 'Sep' Period,'3' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
                "left Join Mutu_Sarmut_Trans trans " & _
                "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
                "And Convert(Varchar(50), dt_period, 105) = '01-09-" + year + "' "
        End If
        If (month = 10 Or month >= 10) Then
            Sql = Sql + "UNION " & _
                "Select 'Oct' Period,'4' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
                "left Join Mutu_Sarmut_Trans trans " & _
                "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
                "And Convert(Varchar(50), dt_period, 105) = '01-10-" + year + "' "
        End If
        If (month = 11 Or month >= 11) Then
            Sql = Sql + "UNION " & _
                "Select 'Nov' Period,'5' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
                "left Join Mutu_Sarmut_Trans trans " & _
                "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
                "And Convert(Varchar(50), dt_period, 105) = '01-11-" + year + "' "
        End If
        If (month = 12 Or month >= 12) Then
            Sql = Sql + "UNION " & _
                "Select 'Dec' Period,'6' seq, sarmut.in_sarmut_id,sarmut.in_target,trans.dc_pencapaian FROM Mutu_Sarmut sarmut  " & _
                "left Join Mutu_Sarmut_Trans trans " & _
                "On sarmut.in_sarmut_id = trans.in_sarmut_id " & _
                "And Convert(Varchar(50), dt_period, 105) = '01-12-" + year + "' "
        End If
        Sql = Sql + ") temp " & _
        "where temp.in_sarmut_id = '" & Request.QueryString("sarmut_id") & "' order by seq asc "
        Return Sql
    End Function
    Private Sub PESAN(ByVal cpesan As String)
        ClientScript.RegisterStartupScript(Me.GetType, "ClientSideScript", "<script type='text/javascript'>window.alert('" & cpesan & "')</script>")
    End Sub
End Class
