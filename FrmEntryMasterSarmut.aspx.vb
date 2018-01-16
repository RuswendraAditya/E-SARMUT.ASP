Imports System.Data
Imports System.Web.Configuration
Imports System.Data.SqlClient
Partial Class FrmEntryMasterSarmut
    Inherits System.Web.UI.Page

    Private Sub GetGugusTugas()
        With Me.RadComboGustu
            .DataTextField = "VC_n_gugus"
            .DataValueField = "VC_k_gugus"
            .DataSource = MasterLib.SetDataSourceGugusSDM
            .DataBind()
        End With


    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            GetGugusTugas()
            If (Request.QueryString("mode") = "update") Then
                loadDataSarmut()
            End If
        End If
    End Sub
    Private Sub loadDataSarmut()
        Dim strsql As String = ""
        strsql = " SELECT in_sarmut_id,sarmut.vc_k_gugus,gugus.VC_n_gugus,sarmut.vc_nama_sarmut,in_target,vc_syarat,dt_effective_start_date,dt_effective_end_date  " & _
                 " FROM  Mutu_Sarmut sarmut join PubGugusSDM gugus " & _
                 " ON gugus.VC_k_gugus = sarmut.vc_k_gugus " & _
                 " WHERE in_sarmut_id = '" & Request.QueryString("sarmut_id") & "' "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)


        While dr.Read
            If (dr("vc_k_gugus").ToString.Length > 0) Then
                Try
                    Me.RadComboGustu.Text = RadComboGustu.FindItemByValue(dr("vc_k_gugus")).Text
                Catch ex As Exception
                    Me.RadComboGustu.Text = ""
                End Try
            End If
            Me.txtNamaSarmut.Text = dr("vc_nama_sarmut")
            Me.txtTarget.Text = dr("in_target")
            rbSyarat.SelectedValue = dr("vc_syarat")
            RadDatePickerStartDate.SelectedDate = dr("dt_effective_start_date")
            If (dr("dt_effective_end_date") <> "31-dec-4712") Then
                RadDatePickerEndDate.SelectedDate = dr("dt_effective_end_date")
            End If
        End While
        dr.Close()
    End Sub
    Protected Sub BtnSaveMaster_Click(sender As Object, e As EventArgs)
        Page.Validate()
        If (Page.IsValid) Then
            If (Request.QueryString("mode") = "update") Then
                updateMaster()
            End If
            If (Request.QueryString("mode") = "create") Then
                simpanMaster()
            End If
        End If





    End Sub

    Private Sub simpanMaster()

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim MyTrans As SqlTransaction

        Dim strsql As String = ""
        Dim start_date As Date = convertDateInput(Format(Me.RadDatePickerStartDate.SelectedDate, "dd/MM/yyyy"))
        Dim end_date As Date = convertDateInput(Format(Me.RadDatePickerEndDate.SelectedDate, "dd/MM/yyyy"))
        Dim syarat As String = Me.rbSyarat.SelectedValue
        If end_date = Nothing Then
            end_date = "31-dec-4712"
        End If
        connection.Open()
        command.Connection = connection
        MyTrans = connection.BeginTransaction()
        command.Transaction = MyTrans
        Try
            strsql = "Insert Into Mutu_sarmut (vc_k_gugus,vc_nama_sarmut,in_target,vc_syarat,dt_effective_start_date,dt_effective_end_date,dt_created_date,vc_created_by,dt_last_updated_date,vc_last_updated_by) " & _
            " VALUES ('" & Me.RadComboGustu.SelectedValue & "','" & Me.txtNamaSarmut.Text & "','" & txtTarget.Text & "','" & syarat & "', " & _
            "'" & start_date & "', '" & end_date & "',  " & _
            "'" & MasterLib.GetCurrentDate & "','" & Session("ssusername") & "','" & MasterLib.GetCurrentDate & "','" & Session("ssusername") & "') "

            command.CommandText = strsql
            command.CommandType = CommandType.Text
            command.ExecuteNonQuery()
            MyTrans.Commit()
            command.Dispose()
            MyTrans.Dispose()
            connection.Close()
            BtnSaveMaster.Enabled = "False"
            lblMsg.Text = "<div class='alert alert-success'><strong><span class='glyphicon glyphicon-send'></span> Data Berhasil Disimpan</strong></div>"
            Threading.Thread.Sleep(1000)
            Response.Redirect("~/FrmInquiryMasterSarmut.aspx")
        Catch ex As Exception
            MyTrans.Rollback()
            Throw New Exception("Error: ", ex)
        End Try

    End Sub

    Private Sub updateMaster()

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim MyTrans As SqlTransaction

        Dim strsql As String = ""
        Dim start_date As Date = convertDateInput(Format(Me.RadDatePickerStartDate.SelectedDate, "dd/MM/yyyy"))
        Dim end_date As Date = convertDateInput(Format(Me.RadDatePickerEndDate.SelectedDate, "dd/MM/yyyy"))
        Dim syarat As String = Me.rbSyarat.SelectedValue
        If end_date = Nothing Then
            end_date = "31-dec-4712"
        End If
        connection.Open()
        command.Connection = connection
        MyTrans = connection.BeginTransaction()
        command.Transaction = MyTrans
        Try
            strsql = " UPDATE Mutu_sarmut set  " & _
                " vc_k_gugus = '" & Me.RadComboGustu.FindItemByText(RadComboGustu.Text).Value & "' " & _
                " ,vc_nama_sarmut = '" & Me.txtNamaSarmut.Text & "' " & _
                " ,in_target = '" & Me.txtTarget.Text & "' " & _
                " ,vc_syarat = '" & syarat & "' " & _
                ",dt_effective_start_date  = '" & start_date & "' " & _
                ",dt_effective_end_date  = '" & end_date & "' " & _
                ",dt_last_updated_date = '" & MasterLib.GetCurrentDate & "' " & _
                ",vc_last_updated_by = '" & Session("ssusername") & "' " & _
                " where in_sarmut_id = '" & Request.QueryString("sarmut_id") & "' "


            command.CommandText = strsql
            command.CommandType = CommandType.Text
            command.ExecuteNonQuery()
            MyTrans.Commit()
            command.Dispose()
            MyTrans.Dispose()
            connection.Close()
            BtnSaveMaster.Enabled = "False"
            lblMsg.Text = "<div Class='alert alert-success'><strong><span class='glyphicon glyphicon-send'></span> Data Berhasil Disimpan</strong></div>"
            Threading.Thread.Sleep(1000)
            Response.Redirect("~/FrmInquiryMasterSarmut.aspx")
        Catch ex As Exception
            MyTrans.Rollback()
            Throw New Exception("Error: ", ex)
        End Try

    End Sub
    Private Function convertDateInput(ByVal tanggal As String) As Date
        Dim tanggal_return As Date
        If (tanggal.Length > 0) Then
            tanggal_return = CDate(MasterLib.xDate(tanggal))
        Else
            tanggal_return = Nothing
        End If
        Return tanggal_return
    End Function


    Private Sub PESAN(ByVal cpesan As String)
        ClientScript.RegisterStartupScript(Me.GetType, "ClientSideScript", "<script type='text/javascript'>window.alert('" & cpesan & "')</script>")
    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("~/FrmInquiryMasterSarmut.aspx")
    End Sub
End Class
