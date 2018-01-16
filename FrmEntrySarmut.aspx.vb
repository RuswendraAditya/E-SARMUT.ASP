Imports System.Data
Imports System.Web.Configuration
Imports System.Data.SqlClient


Partial Class FrmEntrySarmut
    Inherits System.Web.UI.Page
    Dim syarat As String = ""
    Dim period As String = ""
    Dim status As String = ""
    Private Sub FirstInitDataRow()

        Dim Table1 As DataTable
        Table1 = New DataTable("TableName")
        '   Dim rowNumber As DataColumn = New DataColumn("RowNumber")
        ' rowNumber.DataType = System.Type.GetType("System.String")
        Dim column1 As DataColumn = New DataColumn("Tindakan")
        column1.DataType = System.Type.GetType("System.String")

        Dim column2 As DataColumn = New DataColumn("Pic")
        column2.DataType = System.Type.GetType("System.String")
        Dim column3 As DataColumn = New DataColumn("DueDate")
        column3.DataType = System.Type.GetType("System.String")
        Dim column4 As DataColumn = New DataColumn("Status")
        column4.DataType = System.Type.GetType("System.String")
        '  Table1.Columns.Add(rowNumber)
        Table1.Columns.Add(column1)
        Table1.Columns.Add(column2)
        Table1.Columns.Add(column3)
        Table1.Columns.Add(column4)
        Dim Row1 As DataRow
        Row1 = Table1.NewRow()
        '  Row1.Item("RowNumber") = 1
        Row1.Item("Tindakan") = String.Empty
        Row1.Item("Pic") = String.Empty
        Row1.Item("DueDate") = String.Empty
        Row1.Item("Status") = String.Empty


        Table1.Rows.Add(Row1)
        ViewState("CurrentTable") = Table1


        gridViewTindakan.DataSource = Table1
        gridViewTindakan.DataBind()
        gridViewTindakan.HeaderRow.TableSection = TableRowSection.TableHeader
    End Sub

    Private Sub addRow()

        Dim rowIndex As Integer = 0
        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            Dim drCurrentRow As DataRow = Nothing
            If dtCurrentTable.Rows.Count > 0 Then
                For i As Integer = 1 To dtCurrentTable.Rows.Count
                    Dim cJumRec As Integer = dtCurrentTable.Rows.Count
                    Dim txtTindakan As TextBox = DirectCast(gridViewTindakan.Rows(rowIndex).Cells(0).FindControl("txtTindakLanjut"), TextBox)
                    Dim txtPIC As TextBox = DirectCast(gridViewTindakan.Rows(rowIndex).Cells(1).FindControl("txtPIC"), TextBox)
                    Dim DueDate As Telerik.Web.UI.RadDatePicker = DirectCast(gridViewTindakan.Rows(rowIndex).Cells(2).FindControl("RadDatePickerDueDate"), Telerik.Web.UI.RadDatePicker)
                    Dim txtStatus As TextBox = DirectCast(gridViewTindakan.Rows(rowIndex).Cells(1).FindControl("txtStatus"), TextBox)
                    drCurrentRow = dtCurrentTable.NewRow()
                    ' dtCurrentTable.Rows(cJumRec - 1)("RowNumber") = dtCurrentTable.Rows.Count + 1
                    dtCurrentTable.Rows(i - 1)("Tindakan") = txtTindakan.Text
                    dtCurrentTable.Rows(i - 1)("Pic") = txtPIC.Text
                    dtCurrentTable.Rows(i - 1)("DueDate") = DueDate.SelectedDate
                    dtCurrentTable.Rows(i - 1)("Status") = txtStatus.Text
                    drCurrentRow = dtCurrentTable.NewRow()
                    rowIndex += 1
                Next
                dtCurrentTable.Rows.Add(drCurrentRow)
                ViewState("CurrentTable") = dtCurrentTable


                gridViewTindakan.DataSource = dtCurrentTable
                gridViewTindakan.DataBind()

            End If
        Else
            Response.Write("ViewState Is null")
        End If
        SetPreviousData()
    End Sub


    Private Sub SetPreviousData()
        Dim rowIndex As Integer = 0
        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim txtTindakan As TextBox = DirectCast(gridViewTindakan.Rows(rowIndex).Cells(0).FindControl("txtTindakLanjut"), TextBox)
                    Dim txtPIC As TextBox = DirectCast(gridViewTindakan.Rows(rowIndex).Cells(1).FindControl("txtPIC"), TextBox)
                    Dim DueDate As Telerik.Web.UI.RadDatePicker = DirectCast(gridViewTindakan.Rows(rowIndex).Cells(2).FindControl("RadDatePickerDueDate"), Telerik.Web.UI.RadDatePicker)
                    Dim txtStatus As TextBox = DirectCast(gridViewTindakan.Rows(rowIndex).Cells(1).FindControl("txtStatus"), TextBox)
                    txtTindakan.Text = dt.Rows(i)("Tindakan").ToString()
                    txtPIC.Text = dt.Rows(i)("Pic").ToString()
                    If (dt.Rows(i)("DueDate").ToString() <> "") Then
                        DueDate.SelectedDate = dt.Rows(i)("DueDate").ToString()
                    End If
                    ' DueDate.SelectedDate = dt.Rows(i)("DueDate").ToString()
                    txtStatus.Text = dt.Rows(i)("Status").ToString()
                    rowIndex += 1
                Next
            End If
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            RequiredFieldTxtAlasan.Enabled = False
            GetGugusTugas()
            Dim check_tindakan As Boolean = False
            FirstInitDataRow()
            If (Request.QueryString("mode") = "update") Then
                loadDataTransSarmut()
                check_tindakan = checkTindakanxists()
                If (check_tindakan) Then
                    FirstInitDataTindakan()
                End If

            End If
            loadComponentbyRole()
        End If
        Me.lblMsg.Text = ""
        'loadSarmutGugus()
    End Sub

    Private Sub loadComponentbyRole()


        If (status = "In Progress" And Session("role") = "User") Then
            btnSave.Visible = True
            btnApprove.Visible = False
            btnReject.Visible = False
            ' DisabledComponent()
            txtAlasanReject.Visible = False

        End If
        If (status = "In Progress" And Session("role") <> "User") Then
            btnSave.Visible = True
            btnApprove.Visible = True
            btnReject.Visible = True
            DisabledComponent()
            txtAlasanReject.Visible = True

        End If
        If (status = "Approved") Then
            btnSave.Visible = False
            btnApprove.Visible = False
            btnReject.Visible = False
            DisabledComponent()
        End If
        If (status = "Rejected") Then
            btnSave.Visible = True
            txtAlasanReject.Visible = True
            txtAlasanReject.ReadOnly = True
        End If
        If (Session("role") = "User") Then
            btnApprove.Visible = False
            btnReject.Visible = False
        End If
    End Sub

    Private Sub DisabledComponent()
        RadMonthYearPeriod.Enabled = False
        RadMonthYearPeriod.AutoPostBack = False
        RadComboGustu.Enabled = False
        RadComboGustu.AutoPostBack = False
        RadComboSarmut.Enabled = False
        RadComboSarmut.AutoPostBack = False
        txtPencapaian.Enabled = False
        TxtNotePencapaian.Enabled = False
        txtMasalah.Enabled = False
        gridViewTindakan.Enabled = False
    End Sub
    Private Function checkTindakanxists() As Boolean
        Dim check As Boolean = False
        Dim strsql As String = ""

        strsql = "SELECT * FROM mutu_tindakan where in_transaction_id ='" & Request.QueryString("trans_id") & "' "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        If (dr.HasRows) Then
            check = True
        Else
            check = False
        End If
        Return check

    End Function

    Private Function checkDuplicateSarmut(ByVal sarmut_id As Integer, ByVal period As Date) As Boolean
        Dim check As Boolean = False
        Dim strsql As String = ""

        strsql = "SELECT * FROM Mutu_Sarmut_Trans where in_sarmut_id ='" & sarmut_id & "' and dt_period ='" & period & "'   "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        If (dr.HasRows) Then
            check = True
        Else
            check = False
        End If
        Return check

    End Function
    Private Sub FirstInitDataTindakan()
        Dim Table1 As DataTable
        Table1 = New DataTable("TableName")
        Dim column1 As DataColumn = New DataColumn("Tindakan")
        column1.DataType = System.Type.GetType("System.String")

        Dim column2 As DataColumn = New DataColumn("Pic")
        column2.DataType = System.Type.GetType("System.String")
        Dim column3 As DataColumn = New DataColumn("DueDate")
        column3.DataType = System.Type.GetType("System.String")
        Dim column4 As DataColumn = New DataColumn("Status")
        column4.DataType = System.Type.GetType("System.String")
        Table1.Columns.Add(column1)
        Table1.Columns.Add(column2)
        Table1.Columns.Add(column3)
        Table1.Columns.Add(column4)
        Dim strsql As String = ""
        strsql = "SELECT * FROM mutu_tindakan where in_transaction_id ='" & Request.QueryString("trans_id") & "' "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()
        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Dim listTindakan As New ArrayList
        Dim listPic As New ArrayList
        Dim listDueDate As New ArrayList
        Dim listStatus As New ArrayList
        While dr.Read
            Dim Row1 As DataRow
            Row1 = Table1.NewRow()
            Row1.Item("Tindakan") = String.Empty
            Row1.Item("Pic") = String.Empty
            Row1.Item("DueDate") = String.Empty
            Row1.Item("Status") = String.Empty

            Table1.Rows.Add(Row1)
            listTindakan.Add(dr("vc_nama_tindakan"))
            listPic.Add(dr("vc_pic"))
            listDueDate.Add(dr("dt_duedate"))
            listStatus.Add(dr("vc_status"))
        End While
        dr.Close()

        ViewState("CurrentTable") = Table1
        gridViewTindakan.DataSource = Table1
        gridViewTindakan.DataBind()
        For i As Integer = 0 To gridViewTindakan.Rows.Count - 1
            Dim txtTindakan As TextBox = DirectCast(gridViewTindakan.Rows(i).Cells(0).FindControl("txtTindakLanjut"), TextBox)
            Dim txtPIC As TextBox = DirectCast(gridViewTindakan.Rows(i).Cells(1).FindControl("txtPIC"), TextBox)
            Dim DueDate As Telerik.Web.UI.RadDatePicker = DirectCast(gridViewTindakan.Rows(i).Cells(2).FindControl("RadDatePickerDueDate"), Telerik.Web.UI.RadDatePicker)
            Dim txtStatus As TextBox = DirectCast(gridViewTindakan.Rows(i).Cells(1).FindControl("txtStatus"), TextBox)
            txtTindakan.Text = listTindakan(i)
            txtPIC.Text = listPic(i)
            DueDate.SelectedDate = listDueDate(i)
            txtStatus.Text = listStatus(i)
            'radDokter.Text = radDokter.FindItemByValue(listNid(i)).Text
        Next
        Table1 = gridViewTindakan.DataSource
        ViewState("CurrentTable") = Table1
    End Sub
    Private Sub loadDataTransSarmut()
        Dim strsql As String = ""
        strsql = " Select in_transaction_id, dt_period, TRANS.in_sarmut_id, " & _
                 " sarmut.vc_nama_sarmut, trans.vc_k_gugus,trans.dt_created_date, " & _
                 " GUGUS.VC_n_gugus, trans.in_target,vc_note_pencapaian, dc_pencapaian,CASE TRANS.bt_hasil " & _
                 " WHEN 1 THEN 'Tercapai'  " & _
                 " Else 'Tidak Tercapai'  " & _
                 " End As bt_hasil,vc_masalah,vc_status,vc_alasan_reject " & _
                 " From Mutu_Sarmut_Trans trans Join Mutu_Sarmut sarmut " & _
                 " On trans.in_sarmut_id = sarmut.in_sarmut_id " & _
                 " Join PubGugusSDM gugus " & _
                 " On gugus.VC_k_gugus = sarmut.vc_k_gugus " & _
                 " WHERE in_transaction_id = '" & Request.QueryString("trans_id") & "' "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)


        While dr.Read
            Me.RadMonthYearPeriod.SelectedDate = dr("dt_period").ToString
            status = dr("vc_status")
            period = Me.RadMonthYearPeriod.SelectedDate
            If (dr("vc_k_gugus").ToString.Length > 0) Then
                Try
                    Me.RadComboGustu.Text = RadComboGustu.FindItemByValue(dr("vc_k_gugus")).Text
                    GetSarmutGustu(dr("vc_k_gugus"), String.Format(dr("dt_created_date"), "yyyy-MM-dd"))
                Catch ex As Exception
                    Me.RadComboGustu.Text = ""
                End Try
            End If
            GetSarmutGustu(dr("vc_k_gugus"), String.Format(dr("dt_created_date"), "yyyy-MM-dd"))
            If IsDBNull(dr("vc_note_pencapaian")) Then
            Else
                TxtNotePencapaian.Text = dr("vc_note_pencapaian")
            End If
            If (dr("bt_hasil") = "Tercapai") Then
                    rbPencapaian.SelectedValue = "1"
                validatorMasalah.Enabled = "false"

            Else
                rbPencapaian.SelectedValue = "0"
                Me.txtMasalah.Visible = True
                gridViewTindakan.Visible = "True"


            End If
            RadComboSarmut.Text = RadComboSarmut.FindItemByValue(dr("in_sarmut_id")).Text
            Me.TxtTarget.Text = dr("in_target")
            txtPencapaian.Text = dr("dc_pencapaian")
            Me.txtMasalah.Text = dr("vc_masalah")
            If (IsDBNull(dr("vc_alasan_reject")) = False) Then
                Me.txtAlasanReject.Text = dr("vc_alasan_reject")
            End If



        End While
        dr.Close()

    End Sub
    Protected Sub BtnAddTindakan_Click(ByVal sender As Object, ByVal e As EventArgs)
        addRow()
    End Sub
    Private Sub setRow()

        Dim rowIndex As Integer = 0
        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            Dim drCurrentRow As DataRow = Nothing
            If dtCurrentTable.Rows.Count > 0 Then
                For i As Integer = 1 To dtCurrentTable.Rows.Count
                    Dim cJumRec As Integer = dtCurrentTable.Rows.Count
                    Dim txtTindakan As TextBox = DirectCast(gridViewTindakan.Rows(rowIndex).Cells(0).FindControl("txtTindakLanjut"), TextBox)
                    Dim txtPIC As TextBox = DirectCast(gridViewTindakan.Rows(rowIndex).Cells(1).FindControl("txtPIC"), TextBox)
                    Dim DueDate As Telerik.Web.UI.RadDatePicker = DirectCast(gridViewTindakan.Rows(rowIndex).Cells(2).FindControl("RadDatePickerDueDate"), Telerik.Web.UI.RadDatePicker)
                    Dim txtStatus As TextBox = DirectCast(gridViewTindakan.Rows(rowIndex).Cells(1).FindControl("txtStatus"), TextBox)
                    drCurrentRow = dtCurrentTable.NewRow()

                    dtCurrentTable.Rows(i - 1)("Tindakan") = txtTindakan.Text
                    dtCurrentTable.Rows(i - 1)("Pic") = txtPIC.Text
                    dtCurrentTable.Rows(i - 1)("DueDate") = DueDate.SelectedDate
                    dtCurrentTable.Rows(i - 1)("Status") = txtStatus.Text
                    drCurrentRow = dtCurrentTable.NewRow()
                    rowIndex += 1
                Next
                ViewState("CurrentTable") = dtCurrentTable
            End If
        Else
            Response.Write("ViewState is null")
        End If

    End Sub
    Protected Sub GridViewTindakan_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridViewTindakan.RowDeleting
        setRow()
        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = ViewState("CurrentTable")
            Dim drCurrentRow As DataRow = Nothing
            Dim rowIndex As Integer = Convert.ToInt32(e.RowIndex)
            If (dt.Rows.Count > 1) Then

                dt.Rows.Remove(dt.Rows(rowIndex))
                drCurrentRow = dt.NewRow()
                ViewState("CurrentTable") = dt
                gridViewTindakan.DataSource = dt
                gridViewTindakan.DataBind()

                SetPreviousData()
            End If
        End If

    End Sub

    Private Sub GetGugusTugas()
        With Me.RadComboGustu
            .DataTextField = "VC_n_gugus"
            .DataValueField = "VC_k_gugus"
            If (Session("role") = "User") Then
                Dim kode_gugus As String = (MasterLib.getKodeGustuSDMFromLogin(Session("ssusername"), Session("cIdUser")))
                .DataSource = MasterLib.SetDataSourceGugusSDMByGustu(Session("ssusername"), Session("cIdUser"))
            Else
                .DataSource = MasterLib.SetDataSourceGugusSDM
            End If

            .DataBind()
        End With


    End Sub
    Protected Sub RadComboGustu_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboGustu.SelectedIndexChanged
        txtPencapaian.Text = ""
        loadSarmutGugus()
    End Sub
    Private Sub loadSarmutGugus()
        If (RadComboGustu.Text.Length > 0) Then
            Dim kode_gugus As String = RadComboGustu.FindItemByText(RadComboGustu.Text).Value
            Me.RadComboSarmut.Text = ""
            GetSarmutGustu(kode_gugus, "")
        End If


    End Sub



    Private Sub GetSarmutGustu(ByVal kode_gugus As String, ByVal create_Date As String)
        If kode_gugus <> "" Then

            With Me.RadComboSarmut
                Dim strSQL As String = ""
                Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
                Dim connection As SqlConnection = New SqlConnection(connectionString)
                Dim con As SqlConnection = New SqlConnection(connectionString)
                Dim adapter As SqlDataAdapter
                If (create_Date <> "") Then
                    adapter = New SqlDataAdapter("Select in_sarmut_id, vc_nama_sarmut FROM mutu_sarmut " & _
                            " where '" & create_Date & "' BETWEEN [dt_effective_start_date] And [dt_effective_end_date] " & _
                            " And vc_k_gugus = '" & kode_gugus & "' ", con)
                Else
                    adapter = New SqlDataAdapter("Select in_sarmut_id, vc_nama_sarmut FROM mutu_sarmut " & _
                            " where CONVERT(varchar(12), GETDATE(), 102) BETWEEN [dt_effective_start_date] And [dt_effective_end_date] " & _
                             " And vc_k_gugus = '" & kode_gugus & "' ", con)
                End If
                Dim links As DataTable = New DataTable()
                adapter.Fill(links)
                .DataTextField = "vc_nama_sarmut"
                .DataValueField = "in_sarmut_id"
                .DataSource = links
                .DataBind()
            End With
        End If
    End Sub


    Protected Sub RadComboSarmut_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboSarmut.SelectedIndexChanged
        If (RadComboSarmut.Text <> "") Then
            syarat = ""
            Dim sarmut_id As Integer = RadComboSarmut.SelectedValue
            TxtTarget.Text = GetTargetFromSarmut(sarmut_id)

        End If

        ' Pesan(sarmut_id)
    End Sub


    Private Function GetTargetFromSarmut(ByVal sarmut_id As Integer) As Integer
        Dim target As Integer = 0
        Dim strsql As String = ""

        strsql = " SELECT in_target FROM mutu_sarmut where in_sarmut_id =  '" & sarmut_id & "'  "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()
        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            target = dr("in_target")
        End While
        dr.Close()

        Return target
    End Function

    Private Function GetSyaratFromSarmut(ByVal sarmut_id As Integer) As String

        Dim strsql As String = ""

        strsql = " SELECT vc_syarat FROM mutu_sarmut where in_sarmut_id =  '" & sarmut_id & "'  "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()
        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            syarat = dr("vc_syarat")
        End While
        dr.Close()

        Return syarat
    End Function

    Private Sub PESAN(ByVal cpesan As String)
        ClientScript.RegisterStartupScript(Me.GetType, "ClientSideScript", "<script type='text/javascript'>window.alert('" & cpesan & "')</script>")
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Page.Validate()
        If (Page.IsValid) Then
            Dim sarmut_id As Integer = RadComboSarmut.FindItemByText(RadComboSarmut.Text).Value
            Dim period As Date = RadMonthYearPeriod.SelectedDate



            If (Request.QueryString("mode") = "update") Then
                updateSarmut("In Progress")
            End If
            If (Request.QueryString("mode") = "create") Then
                simpanSarmut()
            End If
        End If






    End Sub

    Protected Sub btnApprove_Click(sender As Object, e As EventArgs)
        Dim approver As String = ""
        Dim strsql As String = ""

        strsql = " SELECT vc_nik FROM  [yakkumdatabase].[dbo].[Mutu_Signer_Approval]  "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()
        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            approver = dr("vc_nik")
        End While
        dr.Close()
        updateStatus("Approved", approver, "")
    End Sub


    Protected Sub btnReject_Click(sender As Object, e As EventArgs)
        If (Me.txtAlasanReject.Text.Length < 1) Then
            RequiredFieldTxtAlasan.Enabled = True
            RequiredFieldTxtAlasan.Validate()
        Else
            updateStatus("Rejected", "", txtAlasanReject.Text)
        End If


    End Sub
    Private Function getDataPejabatGustu(ByVal kode_gugus As String) As String
        Dim strsql As String = ""
        Dim nik As String = ""
        strsql = " SELECT vc_nik FROM  Mutu_Mapping_Signer " & _
                 " where vc_k_gugus ='" & kode_gugus & "' "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            nik = dr("vc_nik")
        End While
        dr.Close()
        Return nik
    End Function
    Private Sub simpanSarmut()

        Dim sarmut_id As Integer = RadComboSarmut.FindItemByText(RadComboSarmut.Text).Value
        Dim gugus_tugas As String = RadComboGustu.FindItemByText(RadComboGustu.Text).Value
        Dim check As Boolean = checkDuplicateSarmut(sarmut_id, RadMonthYearPeriod.SelectedDate)
        Dim nik As String = getDataPejabatGustu(gugus_tugas)
        If (check) Then
            lblMsg.Text = "<div Class='alert alert-danger'><strong><span class='glyphicon glyphicon-send'></span> Data sasaran mutu atas periode ini sudah ada, mohon dicek kembali</strong></div>"

        Else
            Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            Dim command As SqlCommand = New SqlCommand()
            Dim MyTrans As SqlTransaction
            Dim id As Integer
            Dim strsql As String = ""
            Dim delete_sql As String = ""
            connection.Open()
            command.Connection = connection
            MyTrans = connection.BeginTransaction()
            command.Transaction = MyTrans
            Try
                strsql = "Insert Into Mutu_sarmut_trans (dt_period,in_sarmut_id,vc_k_gugus,in_target,vc_note_pencapaian, " & _
                         "dc_pencapaian,bt_hasil,vc_masalah, vc_status,vc_alasan_reject, " & _
                         "vc_nik_pejabat,dt_created_date,vc_created_by,dt_last_updated_date,vc_last_updated_by) " & _
                         "VALUES ('" & Me.RadMonthYearPeriod.SelectedDate.Value & "','" & sarmut_id & "','" & gugus_tugas & "','" & Me.TxtTarget.Text & "', '" & Me.TxtNotePencapaian.Text & "', " & _
                         "'" & Me.txtPencapaian.Text & "',  '" & rbPencapaian.SelectedValue & "','" & txtMasalah.Text & "', 'In Progress','',  " & _
                         "'" & nik & "','" & MasterLib.GetCurrentDate & "','" & Session("ssusername") & "','" & MasterLib.GetCurrentDate & "','" & Session("ssusername") & "'); " & _
                         " Select SCOPE_IDENTITY() "

                command.CommandText = strsql
                command.CommandType = CommandType.Text
                'command.ExecuteNonQuery()
                id = command.ExecuteScalar()
                ' PESAN(id)
                If (id > 0) Then
                    delete_sql = "Delete From mutu_tindakan Where in_transaction_id = '" & id & "'"
                    command.CommandText = delete_sql
                    command.CommandType = CommandType.Text
                    command.ExecuteNonQuery()
                End If
                If (rbPencapaian.SelectedValue = 0 And id > 0) Then
                    Dim rowIndex As Integer = 0
                    If ViewState("CurrentTable") IsNot Nothing Then
                        Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
                        If dt.Rows.Count > 0 Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                rowIndex += 1
                            Next
                        End If
                        For i As Integer = 0 To rowIndex - 1
                            Dim txtTindakan As TextBox = DirectCast(gridViewTindakan.Rows(i).Cells(0).FindControl("txtTindakLanjut"), TextBox)
                            Dim txtPIC As TextBox = DirectCast(gridViewTindakan.Rows(i).Cells(1).FindControl("txtPIC"), TextBox)
                            Dim DueDate As Telerik.Web.UI.RadDatePicker = DirectCast(gridViewTindakan.Rows(i).Cells(2).FindControl("RadDatePickerDueDate"), Telerik.Web.UI.RadDatePicker)
                            Dim txtStatus As TextBox = DirectCast(gridViewTindakan.Rows(i).Cells(1).FindControl("txtStatus"), TextBox)

                            Try
                                Dim strsql_2 As String = ""
                                strsql_2 = "Insert Into  mutu_tindakan(in_transaction_id,vc_nama_tindakan,vc_pic,dt_duedate,vc_status) " & _
                                           " VALUES ('" & id & "','" & txtTindakan.Text & "','" & txtPIC.Text & "','" & DueDate.SelectedDate & "','" & txtStatus.Text & "') "
                                command.CommandText = strsql_2
                                command.CommandType = CommandType.Text
                                command.ExecuteNonQuery()

                            Catch ex As Exception

                            End Try
                        Next
                    End If
                End If

                MyTrans.Commit()
                command.Dispose()
                MyTrans.Dispose()
                connection.Close()
                lblMsg.Text = "<div Class='alert alert-success'><strong><span class='glyphicon glyphicon-send'></span> Data Berhasil Disimpan</strong></div>"
                Threading.Thread.Sleep(1000)
                Response.Redirect("~/FrmInquiryEntrySarmut.aspx")
            Catch ex As Exception
                MyTrans.Rollback()
                Throw New Exception("Error: ", ex)
            End Try
        End If
    End Sub

    Private Sub updateSarmut(ByVal status As String)

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim MyTrans As SqlTransaction

        Dim strsql As String = ""
        Dim delete_sql As String = ""
        Dim sarmut_id As Integer = RadComboSarmut.FindItemByText(RadComboSarmut.Text).Value
        Dim gugus_tugas As String = RadComboGustu.FindItemByText(RadComboGustu.Text).Value
        Dim nik As String = getDataPejabatGustu(gugus_tugas)
        Dim masalah As String = ""
        If (Me.rbPencapaian.SelectedValue = 0) Then
            masalah = Me.txtMasalah.Text
        End If
        connection.Open()
        command.Connection = connection
        MyTrans = connection.BeginTransaction()
        command.Transaction = MyTrans
        Try
            strsql = " UPDATE Mutu_sarmut_trans set  " & _
                " dt_period = '" & Me.RadMonthYearPeriod.SelectedDate & "' " & _
                ",in_sarmut_id = '" & sarmut_id & "' " & _
                ",vc_k_gugus = '" & gugus_tugas & "' " & _
                ",in_target = '" & Me.TxtTarget.Text & "' " & _
                ",vc_note_pencapaian = '" & Me.TxtNotePencapaian.Text & "' " & _
                ",dc_pencapaian = '" & Me.txtPencapaian.Text & "' " & _
                ",bt_hasil = '" & Me.rbPencapaian.SelectedValue & "' " & _
                ",vc_masalah = '" & masalah & "' " & _
                ",vc_status = '" & status & "' " & _
                ",vc_alasan_reject = '' " & _
                ",vc_nik_pejabat='" & nik & "' " & _
                ",dt_last_updated_date = '" & MasterLib.GetCurrentDate & "' " & _
                ",vc_last_updated_by = '" & Session("ssusername") & "' " & _
                " where in_transaction_id = '" & Request.QueryString("trans_id") & "' "


            command.CommandText = strsql
            command.CommandType = CommandType.Text
            command.ExecuteNonQuery()

            'delete data tindakan lama
            If (String.IsNullOrEmpty(Request.QueryString("trans_id")) = False) Then
                delete_sql = "Delete From mutu_tindakan Where in_transaction_id = '" & Request.QueryString("trans_id") & "'"
                command.CommandText = delete_sql
                command.CommandType = CommandType.Text
                command.ExecuteNonQuery()
            End If
            If (String.IsNullOrEmpty(Request.QueryString("trans_id")) = False And rbPencapaian.SelectedValue = 0) Then
                Dim rowIndex As Integer = 0
                If ViewState("CurrentTable") IsNot Nothing Then
                    Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            rowIndex += 1
                        Next
                    End If
                    For i As Integer = 0 To rowIndex - 1
                        Dim txtTindakan As TextBox = DirectCast(gridViewTindakan.Rows(i).Cells(0).FindControl("txtTindakLanjut"), TextBox)
                        Dim txtPIC As TextBox = DirectCast(gridViewTindakan.Rows(i).Cells(1).FindControl("txtPIC"), TextBox)
                        Dim DueDate As Telerik.Web.UI.RadDatePicker = DirectCast(gridViewTindakan.Rows(i).Cells(2).FindControl("RadDatePickerDueDate"), Telerik.Web.UI.RadDatePicker)
                        Dim txtStatus As TextBox = DirectCast(gridViewTindakan.Rows(i).Cells(1).FindControl("txtStatus"), TextBox)

                        Try
                            Dim strsql_2 As String = ""
                            strsql_2 = "Insert Into  mutu_tindakan(in_transaction_id,vc_nama_tindakan,vc_pic,dt_duedate,vc_status) " & _
                                       " VALUES ('" & Request.QueryString("trans_id") & "','" & txtTindakan.Text & "','" & txtPIC.Text & "','" & DueDate.SelectedDate & "','" & txtStatus.Text & "') "
                            command.CommandText = strsql_2
                            command.CommandType = CommandType.Text
                            command.ExecuteNonQuery()

                        Catch ex As Exception

                        End Try
                    Next
                End If
            End If

            MyTrans.Commit()
            command.Dispose()
            MyTrans.Dispose()
            connection.Close()
            lblMsg.Text = "<div Class='alert alert-success'><strong><span class='glyphicon glyphicon-send'></span> Data Berhasil Disimpan</strong></div>"
            Threading.Thread.Sleep(1000)
            Response.Redirect("~/FrmInquiryEntrySarmut.aspx")
        Catch ex As Exception
            MyTrans.Rollback()
            Throw New Exception("Error: ", ex)
        End Try

    End Sub

    Private Sub updateStatus(ByVal status As String, ByVal approver As String, ByVal reason As String)

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim MyTrans As SqlTransaction

        Dim strsql As String = ""

        connection.Open()
        command.Connection = connection
        MyTrans = connection.BeginTransaction()
        command.Transaction = MyTrans
        Try
            strsql = " UPDATE Mutu_sarmut_trans set  " & _
                "vc_status = '" & status & "' " & _
                ",vc_approver= '" & approver & "' " & _
                ",vc_alasan_reject= '" & reason & "' " & _
                ",dt_last_updated_date = '" & MasterLib.GetCurrentDate & "' " & _
                ",vc_last_updated_by = '" & Session("ssusername") & "' " & _
                " where in_transaction_id = '" & Request.QueryString("trans_id") & "' "


            command.CommandText = strsql
            command.CommandType = CommandType.Text
            command.ExecuteNonQuery()
            MyTrans.Commit()
            command.Dispose()
            MyTrans.Dispose()
            connection.Close()
            lblMsg.Text = "<div Class='alert alert-success'><strong><span class='glyphicon glyphicon-send'></span> Data Berhasil diubah Statusnya</strong></div>"
            Threading.Thread.Sleep(1000)
            Response.Redirect("~/FrmInquiryEntrySarmut.aspx")
        Catch ex As Exception
            MyTrans.Rollback()
            Throw New Exception("Error: ", ex)
        End Try

    End Sub


    Protected Sub txtPencapaian_TextChanged(sender As Object, e As EventArgs)
        If (RadComboSarmut.Text <> "") Then
            syarat = ""
            Dim sarmut_id As Integer = RadComboSarmut.FindItemByText(RadComboSarmut.Text).Value ' RadComboSarmut.SelectedValue
            TxtTarget.Text = GetTargetFromSarmut(sarmut_id)
            syarat = GetSyaratFromSarmut(sarmut_id)
            If (syarat = ">=") Then
                If (Convert.ToDecimal(txtPencapaian.Text) >= Convert.ToDecimal(TxtTarget.Text)) Then
                    rbPencapaian.SelectedValue = "1"
                    gridViewTindakan.Visible = "false"
                    txtMasalah.Visible = "False"
                    txtMasalah.Text = ""
                    validatorMasalah.Enabled = "false"
                Else
                    rbPencapaian.SelectedValue = "0"
                    gridViewTindakan.Visible = "True"
                    txtMasalah.Visible = "True"
                    txtMasalah.Text = ""
                    validatorMasalah.Enabled = "true"
                End If
            End If
            If (syarat = "<=") Then
                'PESAN(txtPencapaian.Text)
                If (Convert.ToDecimal(txtPencapaian.Text) < Convert.ToDecimal(TxtTarget.Text)) Then
                    rbPencapaian.SelectedValue = "1"
                    gridViewTindakan.Visible = "False"
                    txtMasalah.Visible = "False"
                    txtMasalah.Text = ""
                    validatorMasalah.Enabled = "false"
                    gridViewTindakan.DataSource = Nothing
                    gridViewTindakan.DataBind()
                Else
                    txtMasalah.Text = ""
                    rbPencapaian.SelectedValue = "0"
                    gridViewTindakan.Visible = "True"
                    txtMasalah.Visible = "True"
                    validatorMasalah.Enabled = "true"
                    gridViewTindakan.DataSource = Nothing
                    gridViewTindakan.DataBind()
                    FirstInitDataRow()
                End If
            End If
        End If


    End Sub


    Protected Sub RadMonthYearPeriod_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs)
        If (RadComboSarmut.Text <> "") Then
            Dim sarmut_id As Integer = RadComboSarmut.FindItemByText(RadComboSarmut.Text).Value

            Dim check As Boolean = checkDuplicateSarmut(sarmut_id, e.NewDate)
            If (check) Then
                lblMsg.Text = "<div Class='alert alert-danger'><strong><span class='glyphicon glyphicon-send'></span> Data sasaran mutu atas periode ini sudah ada, mohon dicek kembali</strong></div>"
                btnSave.Enabled = False
                btnSave.Visible = False
            Else
                btnSave.Enabled = True
                btnSave.Visible = True
            End If
        End If


    End Sub
    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("~/FrmInquiryEntrySarmut.aspx")
    End Sub
End Class

