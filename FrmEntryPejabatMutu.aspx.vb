Imports System.Data
Imports System.Web.Configuration
Imports System.Data.SqlClient
Partial Class FrmEntryPejabatMutu
    Inherits System.Web.UI.Page



    Private Sub GetNamaKaryawan()
        With Me.RadComboNIK
            .DataTextField = "vc_nama_kry"
            .DataValueField = "vc_nik"
            .DataSource = MasterLib.SetDataSourceKaryawan
            .DataBind()
        End With


    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            GetNamaKaryawan()
            If (Request.QueryString("mode") = "update") Then
                loadDataPejabatGustu()
            End If
        End If
    End Sub

    Private Sub loadDataPejabatGustu()
        Dim strsql As String = ""
        strsql = "SELECT mutu.vc_k_gugus,gugus.VC_n_gugus,mutu.vc_nik,sdm.vc_nama_kry " & _
                 "From [yakkumdatabase].[dbo].[Mutu_Mapping_Signer] mutu " & _
                 "INNER Join SDMKaryawan sdm " & _
                 "On mutu.vc_nik =sdm.vc_nik " & _
                 "INNER Join PubGugusSDM gugus " & _
                 "On gugus.VC_k_gugus = mutu.vc_k_gugus " & _
                 "WHERE gugus.VC_k_gugus = '" & Request.QueryString("gustu") & "' "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)


        While dr.Read
            Me.RadComboNIK.Text = RadComboNIK.FindItemByValue(dr("vc_nik")).Text
        End While
        dr.Close()

    End Sub

    Protected Sub BtnSaveSigner_Click(sender As Object, e As EventArgs)
        Page.Validate()
        If (Page.IsValid) Then
            If (Request.QueryString("mode") = "update") Then
                updateSigner()
            End If
            If (Request.QueryString("mode") = "create") Then
                SaveSigner()
            End If

        End If
    End Sub

    Private Sub SaveSigner()
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
            strsql = "Insert Into Mutu_Signer_Approval (vc_nik,dt_created_date,vc_created_by,dt_last_updated_date,vc_last_updated_by) " & _
            " VALUES ('" & Me.RadComboNIK.SelectedValue & "', " & _
            "'" & MasterLib.GetCurrentDate & "','" & Session("ssusername") & "','" & MasterLib.GetCurrentDate & "','" & Session("ssusername") & "') "
            command.CommandText = strsql
            command.CommandType = CommandType.Text
            command.ExecuteNonQuery()
            MyTrans.Commit()
            command.Dispose()
            MyTrans.Dispose()
            connection.Close()
            lblMsg.Text = "<div class='alert alert-success'><strong><span class='glyphicon glyphicon-send'></span> Data Berhasil Disimpan</strong></div>"
            Threading.Thread.Sleep(1000)
            Response.Redirect("~/FrmInquiryPejabatMutu.aspx")
        Catch ex As Exception
            MyTrans.Rollback()
            Throw New Exception("Error: ", ex)
        End Try

    End Sub


    Private Sub updateSigner()

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim MyTrans As SqlTransaction

        Dim strsql As String = ""
        connection.Open()
        command.Connection = connection
        MyTrans = connection.BeginTransaction()
        command.Transaction = MyTrans
        Dim nik As String = Me.RadComboNIK.FindItemByText(RadComboNIK.Text).Value
        Try
            strsql = " UPDATE Mutu_Signer_Approval set  " & _
                " vc_nik = '" & nik & "' " & _
                ",dt_last_updated_date = '" & MasterLib.GetCurrentDate & "' " & _
                ",vc_last_updated_by = '" & Session("ssusername") & "' "

            command.CommandText = strsql
            command.CommandType = CommandType.Text
            command.ExecuteNonQuery()
            MyTrans.Commit()
            command.Dispose()
            MyTrans.Dispose()
            connection.Close()
            BtnSaveSigner.Enabled = "False"
            lblMsg.Text = "<div Class='alert alert-success'><strong><span class='glyphicon glyphicon-send'></span> Data Berhasil Disimpan</strong></div>"
            Threading.Thread.Sleep(1000)
            Response.Redirect("~/FrmInquiryPejabatMutu.aspx")
        Catch ex As Exception
            MyTrans.Rollback()
            Throw New Exception("Error: ", ex)
        End Try

    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("~/FrmInquiryPejabatMutu.aspx")
    End Sub
End Class
