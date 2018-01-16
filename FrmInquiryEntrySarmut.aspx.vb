Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Configuration
Imports System.Drawing
Imports System.IO
Partial Class FrmInquiryEntrySarmut
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (MasterLib.GetAccess("5001", "0101", "010102010000", Session("cIdUser"))) Then
                IsiGrid()
            Else
                Response.Redirect("~/Default.aspx")
            End If
        End If


    End Sub

    Protected Sub ddlSearchBy_SelectedIndexChanged(sender As Object, e As EventArgs)
        If (ddlSearchBy.SelectedItem.Text = "Semua") Then
            txtSearch.Text = String.Empty
            txtSearch.Enabled = False
            bindDataWithParam(ddlSearchBy.SelectedItem.Text, txtSearch.Text)
        Else
            txtSearch.Enabled = True
            txtSearch.Text = String.Empty
            txtSearch.Focus()

        End If

    End Sub


    Private Sub bindDataWithParam(ByVal Param As String, ByVal filter As String)
        Dim dt As DataTable
        Dim strsql As String = ""
        Dim strsql_2 As String = ""
        Dim kode_gugus As String = (MasterLib.getKodeGustuSDMFromLogin(Session("ssusername"), Session("cIdUser")))

        strsql = " SELECT in_transaction_id,CONVERT(VARCHAR(2),MONTH(dt_period)) + '-' +  " & _
                 " Convert(VARCHAR(4), Year(dt_period)) As dt_period ,TRANS.in_sarmut_id, " & _
                 " sarmut.vc_nama_sarmut, trans.vc_k_gugus, " & _
                 " GUGUS.VC_n_gugus, trans.in_target, dc_pencapaian,CASE TRANS.bt_hasil " & _
                 " WHEN 1 THEN 'Tercapai'  " & _
                 " Else 'Tidak Tercapai'  " & _
                 " End As bt_hasil,vc_status " & _
                 " From Mutu_Sarmut_Trans trans Join Mutu_Sarmut sarmut " & _
                 " On trans.in_sarmut_id = sarmut.in_sarmut_id " & _
                 " Join PubGugusSDM gugus " & _
                 " On gugus.VC_k_gugus = sarmut.vc_k_gugus "
        strsql_2 = "WHERE sarmut.vc_k_gugus IN(  SELECT signer.vc_k_gugus FROM pde_user users JOIN SDMKaryawan sdm  " & _
                   "On SDM.vc_nik = users.vc_nik JOIN Mutu_Mapping_Signer signer   " & _
                   "ON signer.vc_nik = sdm.vc_nik    where vc_username = '" & Session("ssusername") & "' and vc_id = '" & Session("cIdUser") & "' ) "

        If (Session("role") = "User") Then
            strsql = strsql & strsql_2
            If (Param = "Gugus Tugas") Then
                strsql = strsql & (" AND UPPER(VC_n_gugus) like '%" & filter.ToUpper & "%' ")
            End If
            If (Param = "Sasaran Mutu") Then
                strsql = strsql & (" AND UPPER(vc_nama_sarmut) like '%" & filter.ToUpper & "%' ")
            End If
            If (Param = "Periode") Then
                strsql = strsql & (" AND UPPER(dt_period) like '%" & filter.ToUpper & "%' ")
            End If
            If (Param = "Status") Then
                strsql = strsql & (" AND UPPER(vc_status) like '%" & filter.ToUpper & "%' ")
            End If
        Else
            If (Param = "Gugus Tugas") Then
                strsql = strsql & (" WHERE UPPER(VC_n_gugus) like '%" & filter.ToUpper & "%' ")
            End If
            If (Param = "Sasaran Mutu") Then
                strsql = strsql & (" WHERE UPPER(vc_nama_sarmut) like '%" & filter.ToUpper & "%' ")
            End If
            If (Param = "Periode") Then
                strsql = strsql & (" WHERE UPPER(dt_period) like '%" & filter.ToUpper & "%' ")
            End If
            If (Param = "Status") Then
                strsql = strsql & (" WHERE UPPER(vc_status) like '%" & filter.ToUpper & "%' ")
            End If
        End If




        Dim sConstr As String = ConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Using conn As SqlConnection = New SqlConnection(sConstr)
            Using comm As SqlCommand = New SqlCommand(strsql, conn)
                conn.Open()
                Using da As SqlDataAdapter = New SqlDataAdapter(comm)
                    dt = New DataTable("tbl")
                    da.Fill(dt)
                End Using
            End Using
        End Using
        gridViewSarmutTrans.DataSource = dt
        gridViewSarmutTrans.DataBind()
        Cache("dt") = dt
        ViewState("Column_Name") = "Name"
        ViewState("Sort_Order") = "ASC"
    End Sub

    Private Sub IsiGrid()
        Dim dt As DataTable
        Dim strsql As String = ""
        Dim strsql_2 As String = ""
        Dim kode_gugus As String = (MasterLib.getKodeGustuSDMFromLogin(Session("ssusername"), Session("cIdUser")))
        strsql = " SELECT in_transaction_id,CONVERT(VARCHAR(2),MONTH(dt_period)) + '-' +  " & _
                 " Convert(VARCHAR(4), Year(dt_period)) As dt_period ,TRANS.in_sarmut_id, " & _
                 " sarmut.vc_nama_sarmut, trans.vc_k_gugus, " & _
                 " GUGUS.VC_n_gugus, trans.in_target, dc_pencapaian,CASE TRANS.bt_hasil " & _
                 " WHEN 1 THEN 'Tercapai'  " & _
                 " Else 'Tidak Tercapai'  " & _
                 " End As bt_hasil,vc_status " & _
                 " From Mutu_Sarmut_Trans trans Join Mutu_Sarmut sarmut " & _
                 " On trans.in_sarmut_id = sarmut.in_sarmut_id " & _
                 " Join PubGugusSDM gugus " & _
                 " On gugus.VC_k_gugus = sarmut.vc_k_gugus "

        strsql_2 = "WHERE sarmut.vc_k_gugus IN(  SELECT signer.vc_k_gugus FROM pde_user users JOIN SDMKaryawan sdm  " & _
                   "On SDM.vc_nik = users.vc_nik JOIN Mutu_Mapping_Signer signer   " & _
                   "ON signer.vc_nik = sdm.vc_nik    where vc_username = '" & Session("ssusername") & "' and vc_id = '" & Session("cIdUser") & "' ) "

        If (Session("role") = "User") Then
            strsql = strsql & strsql_2
        End If




        Dim sConstr As String = ConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Using conn As SqlConnection = New SqlConnection(sConstr)
            Using comm As SqlCommand = New SqlCommand(strsql, conn)
                conn.Open()
                Using da As SqlDataAdapter = New SqlDataAdapter(comm)
                    dt = New DataTable("tbl")
                    da.Fill(dt)
                End Using
            End Using
        End Using
        gridViewSarmutTrans.DataSource = dt
        gridViewSarmutTrans.DataBind()
        Cache("dt") = dt
        ViewState("Column_Name") = "Name"
        ViewState("Sort_Order") = "ASC"

    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gridViewSarmutTrans.PageIndex = e.NewPageIndex
        Me.IsiGrid()
    End Sub
    Protected Sub btAddEntrySarmut_Click(sender As Object, e As EventArgs) Handles btAddEntrySarmut.Click
        Response.Redirect("~/FrmEntrySarmut.aspx?" + "&trans_id=&mode=create")

    End Sub
    Protected Sub gridViewSarmutTrans_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gridViewSarmutTrans.Sorting
        If e.SortExpression = ViewState("Column_Name").ToString() Then
            If ViewState("Sort_Order").ToString() = "ASC" Then
                RebindData(e.SortExpression, "DESC")
            Else
                RebindData(e.SortExpression, "ASC")
            End If

        Else
            RebindData(e.SortExpression, "ASC")
        End If
    End Sub

    Private Sub RebindData(sColimnName As String, sSortOrder As String)
        Dim dt As DataTable = CType(Cache("dt"), DataTable)
        dt.DefaultView.Sort = sColimnName + " " + sSortOrder
        gridViewSarmutTrans.DataSource = dt
        gridViewSarmutTrans.DataBind()
        ViewState("Column_Name") = sColimnName
        ViewState("Sort_Order") = sSortOrder
    End Sub

    Private Sub PESAN(ByVal cpesan As String)
        ClientScript.RegisterStartupScript(Me.GetType, "ClientSideScript", "<script type='text/javascript'>window.alert('" & cpesan & "')</script>")
    End Sub
    Protected Sub gridViewSarmutTrans_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridViewSarmutTrans.RowCommand
        If e.CommandName = "EditTransSarmut" Then

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim in_trans_id As String = gridViewSarmutTrans.DataKeys(index).Value.ToString
            Response.Redirect("~/FrmEntrySarmut.aspx?" + "&trans_id=" + in_trans_id + "&mode=update")
        End If
    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("~/Home.aspx")
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As System.Web.UI.Control)
        ' Do NOT call MyBase.VerifyRenderingInServerForm
    End Sub
    Protected Sub BtnDownload_Click(sender As Object, e As ImageClickEventArgs) Handles BtnDownload.Click
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=TransaksiSasaranMutu.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            gridViewSarmutTrans.AllowPaging = False
            Me.IsiGrid()

            gridViewSarmutTrans.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In gridViewSarmutTrans.HeaderRow.Cells
                cell.BackColor = gridViewSarmutTrans.HeaderStyle.BackColor
            Next
            For Each row As GridViewRow In gridViewSarmutTrans.Rows
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gridViewSarmutTrans.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gridViewSarmutTrans.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            gridViewSarmutTrans.RenderControl(hw)
            ''style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindDataWithParam(ddlSearchBy.SelectedItem.Text, txtSearch.Text)
    End Sub
End Class
