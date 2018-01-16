Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Configuration
Imports System.Drawing
Imports System.IO
Partial Class FrmInquiryPejabatGustu
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (MasterLib.GetAccess("5001", "0101", "010101010000", Session("cIdUser"))) Then
                IsiGrid()
                If (gridViewPejabat.Rows.Count.ToString > 0) Then
                    bntAddPejabat.Visible = False
                End If
            Else
                Response.Redirect("~/Default.aspx")
            End If
        End If
    End Sub

    Private Sub IsiGrid()
        Dim dt As DataTable
        Dim strsql As String = ""
        strsql = "SELECT karyawan.vc_nama_kry FROM Mutu_Signer_Approval mutu INNer join SDMKaryawan karyawan " & _
                 "On mutu.vc_nik = karyawan.vc_nik "


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
        gridViewPejabat.DataSource = dt
        gridViewPejabat.DataBind()
        Cache("dt") = dt
        ViewState("Column_Name") = "Name"
        ViewState("Sort_Order") = "ASC"

    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gridViewPejabat.PageIndex = e.NewPageIndex
        Me.IsiGrid()
    End Sub
    Protected Sub bntAddPejabat_Click(sender As Object, e As EventArgs) Handles bntAddPejabat.Click
        Response.Redirect("~/FrmEntryPejabatMutu.aspx?" + "&gustu=&mode=create")

    End Sub
    Protected Sub gridViewPejabat_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gridViewPejabat.Sorting
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
        gridViewPejabat.DataSource = dt
        gridViewPejabat.DataBind()
        ViewState("Column_Name") = sColimnName
        ViewState("Sort_Order") = sSortOrder
    End Sub

    Private Sub PESAN(ByVal cpesan As String)
        ClientScript.RegisterStartupScript(Me.GetType, "ClientSideScript", "<script type='text/javascript'>window.alert('" & cpesan & "')</script>")
    End Sub
    Protected Sub gridViewSarmutMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridViewPejabat.RowCommand
        If e.CommandName = "EditPejabat" Then


            Response.Redirect("~/FrmEntryPejabatMutu.aspx?&mode=update")
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
        Response.AddHeader("content-disposition", "attachment;filename=PejabatKomiteMutu.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            gridViewPejabat.AllowPaging = False
            Me.IsiGrid()

            gridViewPejabat.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In gridViewPejabat.HeaderRow.Cells
                cell.BackColor = gridViewPejabat.HeaderStyle.BackColor
            Next
            For Each row As GridViewRow In gridViewPejabat.Rows
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gridViewPejabat.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gridViewPejabat.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            gridViewPejabat.RenderControl(hw)
            ''style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using
    End Sub


End Class