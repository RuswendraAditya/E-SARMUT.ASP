Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Configuration
Imports System.Drawing
Imports System.IO
Partial Class FrmInquiryMasterSarmut
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (MasterLib.GetAccess("5001", "0101", "010101010000", Session("cIdUser"))) Then
                IsiGrid()
            Else
                Response.Redirect("~/Default.aspx")
            End If
        End If
    End Sub

    Private Sub IsiGrid()
        Dim dt As DataTable
        Dim strsql As String = ""
        strsql = " SELECT in_sarmut_id,sarmut.vc_k_gugus,gugus.VC_n_gugus,sarmut.vc_nama_sarmut,in_target,  " & _
                 " CONVERT(varchar(11),dt_effective_start_date,105) dt_effective_start_date , " & _
                 " Case CONVERT(varchar(11),dt_effective_end_date,105) When '31-12-4712' then ''  else  CONVERT(varchar(11),dt_effective_end_date,105) " & _
                 " End dt_effective_end_date  " & _
                 " FROM  Mutu_Sarmut sarmut join PubGugusSDM gugus " & _
                 " On gugus.VC_k_gugus = sarmut.vc_k_gugus "


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
        gridViewSarmutMaster.DataSource = dt
        gridViewSarmutMaster.DataBind()
        Cache("dt") = dt
        ViewState("Column_Name") = "Name"
        ViewState("Sort_Order") = "ASC"

    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gridViewSarmutMaster.PageIndex = e.NewPageIndex
        Me.IsiGrid()
    End Sub
    Protected Sub btAddsarmut_Click(sender As Object, e As EventArgs) Handles btAddsarmut.Click
        Response.Redirect("~/FrmEntryMasterSarmut.aspx?" + "&sarmut_id=&mode=create")

    End Sub
    Protected Sub gridViewSarmutMaster_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gridViewSarmutMaster.Sorting
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
        gridViewSarmutMaster.DataSource = dt
        gridViewSarmutMaster.DataBind()
        ViewState("Column_Name") = sColimnName
        ViewState("Sort_Order") = sSortOrder
    End Sub

    Private Sub PESAN(ByVal cpesan As String)
        ClientScript.RegisterStartupScript(Me.GetType, "ClientSideScript", "<script type='text/javascript'>window.alert('" & cpesan & "')</script>")
    End Sub
    Protected Sub gridViewSarmutMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridViewSarmutMaster.RowCommand
        If e.CommandName = "EditSarmut" Then

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim sarmut_id As String = gridViewSarmutMaster.DataKeys(index).Value.ToString
            Response.Redirect("~/FrmEntryMasterSarmut.aspx?" + "&sarmut_id=" + sarmut_id + "&mode=update")
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
        Response.AddHeader("content-disposition", "attachment;filename=SasaranMutuMasterExport.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            gridViewSarmutMaster.AllowPaging = False
            Me.IsiGrid()

            gridViewSarmutMaster.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In gridViewSarmutMaster.HeaderRow.Cells
                cell.BackColor = gridViewSarmutMaster.HeaderStyle.BackColor
            Next
            For Each row As GridViewRow In gridViewSarmutMaster.Rows
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gridViewSarmutMaster.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gridViewSarmutMaster.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            gridViewSarmutMaster.RenderControl(hw)
            ''style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using
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
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindDataWithParam(ddlSearchBy.SelectedItem.Text, txtSearch.Text)
    End Sub

    Private Sub bindDataWithParam(ByVal Param As String, ByVal filter As String)
        Dim dt As DataTable
        Dim strsql As String = ""
        strsql = " SELECT in_sarmut_id,sarmut.vc_k_gugus,gugus.VC_n_gugus,sarmut.vc_nama_sarmut,in_target,  " & _
                 " CONVERT(varchar(11),dt_effective_start_date,105) dt_effective_start_date , " & _
                 " Case CONVERT(varchar(11),dt_effective_end_date,105) When '31-12-4712' then ''  else  CONVERT(varchar(11),dt_effective_end_date,105) " & _
                 " End dt_effective_end_date  " & _
                 " FROM  Mutu_Sarmut sarmut join PubGugusSDM gugus " & _
                 " On gugus.VC_k_gugus = sarmut.vc_k_gugus "

        If (Param = "Gugus Tugas") Then
            strsql = strsql & ("WHERE UPPER(VC_n_gugus) like '%" & filter.ToUpper & "%' ")
        End If
        If (Param = "Sasaran Mutu") Then
            strsql = strsql & ("WHERE UPPER(vc_nama_sarmut) like '%" & filter.ToUpper & "%' ")
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
        gridViewSarmutMaster.DataSource = dt
        gridViewSarmutMaster.DataBind()
        Cache("dt") = dt
        ViewState("Column_Name") = "Name"
        ViewState("Sort_Order") = "ASC"
    End Sub
End Class
