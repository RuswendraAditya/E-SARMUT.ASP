Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
'Imports iTextSharp.text
'Imports iTextSharp.text.pdf
'Imports iTextSharp.text.html
'Imports iTextSharp.text.html.simpleparser


Public Class MasterLib

    Public Shared Function SetDataSourceRuang() As DataSet
        Dim strSQL As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)


        strSQL = "SELECT vc_k_ruang, vc_n_ruang " & _
                 "FROM RMRuang " & _
                 "UNION " & _
                 "Select TOP(1) '000X' as vc_k_ruang, 'SEMUA' AS vc_n_RUANG " & _
                 "FROM dbo.rmRUANG " & _
                 "ORDER BY vc_k_ruang"


        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim conn As New SqlConnection(connectionString)
        conn.Open()
        Dim comm As SqlCommand = New SqlCommand(strSQL, conn)
        da.SelectCommand = comm
        da.Fill(ds)
        conn.Close()
        Return ds

    End Function

    Public Shared Function SetDataSourceObat() As DataSet
        Dim strSQL As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)


        strSQL = " SELECT vc_kodeobat,vc_namaobat FROM fa_obat "


        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim conn As New SqlConnection(connectionString)
        conn.Open()
        Dim comm As SqlCommand = New SqlCommand(strSQL, conn)
        da.SelectCommand = comm
        da.Fill(ds)
        conn.Close()
        Return ds

    End Function

    Public Shared Function SetDataSourceGugus() As DataSet
        Dim strSQL As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)

        strSQL = "Select vc_k_gugus, vc_n_gugus " & _
                 "FROM pubgugus " & _
                 "union " & _
                 "Select TOP(1) '000A' AS vc_k_gugus, '' AS vc_n_gugus " & _
                 "FROM pubgugus " & _
                 "ORDER BY vc_n_gugus"


        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim conn As New SqlConnection(connectionString)
        conn.Open()
        Dim comm As SqlCommand = New SqlCommand(strSQL, conn)
        da.SelectCommand = comm
        da.Fill(ds)
        conn.Close()
        Return ds
    End Function

    Public Shared Function SetDataSourceGugusSDM() As DataSet
        Dim strSQL As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)

        strSQL = "SELECT vc_k_gugus, vc_n_gugus " & _
                 "FROM pubgugusSDM " & _
                 "ORDER BY vc_n_gugus"


        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim conn As New SqlConnection(connectionString)
        conn.Open()
        Dim comm As SqlCommand = New SqlCommand(strSQL, conn)
        da.SelectCommand = comm
        da.Fill(ds)
        conn.Close()
        Return ds
    End Function

    Public Shared Function SetDataSourceKaryawan() As DataSet
        Dim strSQL As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        strSQL = "select vc_nik,vc_nama_kry from sdmkaryawan where bt_aktif = 1 order by vc_nama_kry asc"


        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim conn As New SqlConnection(connectionString)
        conn.Open()
        Dim comm As SqlCommand = New SqlCommand(strSQL, conn)
        da.SelectCommand = comm
        da.Fill(ds)
        conn.Close()
        Return ds
    End Function


    Public Shared Function GetRoleFromAccess(ByVal modul As String, ByVal apps As String, ByVal kodemenu As String, user_id As String) As String
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim strsql As String = ""


        Dim role As String = ""

        strsql = "SELECT     dbo.pde_user_apps.vc_id, dbo.pde_user_apps.vc_owner, dbo.pde_user_apps.vc_apps_code, dbo.pde_user_apps.vc_userid, dbo.pde_user_apps.in_userlevel,pde_user_level.vc_nlevel, " & _
                 "dbo.pde_user_apps_akses.vc_id_parent, dbo.pde_user_apps_akses.vc_codemenu, dbo.pde_user_apps_akses.bt_enable, dbo.pde_user_apps_akses.bt_visible, " & _
                 "dbo.pde_user_apps_akses.bt_access " & _
                 "FROM  dbo.pde_user_apps INNER JOIN  " & _
                 "dbo.pde_user_apps_akses ON dbo.pde_user_apps.vc_id = dbo.pde_user_apps_akses.vc_id_parent INNER JOIN pde_user_level on vc_klevel = in_userlevel " & _
                 "where dbo.pde_user_apps.vc_owner='" & modul & "' and dbo.pde_user_apps.vc_userid = '" & user_id & "' " & _
                 "and dbo.pde_user_apps.vc_apps_code = '" & apps & "' and dbo.pde_user_apps_akses.bt_visible = 1 " & _
                 "and pde_user_apps_akses.vc_codemenu = '" & kodemenu & "' "


        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)


        While dr.Read()


            If dr("VC_codemenu").ToString = kodemenu Then
                role = dr("vc_nlevel").ToString
            End If
        End While
        dr.Close()

        Return role
    End Function


    Public Shared Function GetAccess(ByVal modul As String, ByVal apps As String, ByVal kodemenu As String, user_id As String) As Boolean
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim strsql As String = ""


        Dim isHaveAccess As Boolean = False

        strsql = "SELECT     dbo.pde_user_apps.vc_id, dbo.pde_user_apps.vc_owner, dbo.pde_user_apps.vc_apps_code, dbo.pde_user_apps.vc_userid, dbo.pde_user_apps.in_userlevel,pde_user_level.vc_nlevel, " & _
                 "dbo.pde_user_apps_akses.vc_id_parent, dbo.pde_user_apps_akses.vc_codemenu, dbo.pde_user_apps_akses.bt_enable, dbo.pde_user_apps_akses.bt_visible, " & _
                 "dbo.pde_user_apps_akses.bt_access " & _
                 "FROM  dbo.pde_user_apps INNER JOIN  " & _
                 "dbo.pde_user_apps_akses ON dbo.pde_user_apps.vc_id = dbo.pde_user_apps_akses.vc_id_parent INNER JOIN pde_user_level on vc_klevel = in_userlevel " & _
                 "where dbo.pde_user_apps.vc_owner='" & modul & "' and dbo.pde_user_apps.vc_userid = '" & user_id & "' " & _
                 "and dbo.pde_user_apps.vc_apps_code = '" & apps & "' and dbo.pde_user_apps_akses.bt_visible = 1 " & _
                 "and pde_user_apps_akses.vc_codemenu = '" & kodemenu & "' "


        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)


        While dr.Read()


            If dr("VC_codemenu").ToString = kodemenu Then
                isHaveAccess = True
            End If
        End While
        dr.Close()

        Return isHaveAccess
    End Function


    Public Shared Function SetDataSourceGugusSDMByGustu(ByVal user_name As String, ByVal user_id As String) As DataSet
        Dim strSQL As String = ""
        Dim sql_in As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)



        strSQL = " SELECT vc_k_gugus, vc_n_gugus  " & _
                 "From pubgugusSDM Where vc_k_gugus In ( " & _
                 "Select signer.vc_k_gugus FROM pde_user users JOIN SDMKaryawan sdm  " & _
                 "On SDM.vc_nik = users.vc_nik JOIN Mutu_Mapping_Signer signer   " & _
                 "On signer.vc_nik = sdm.vc_nik  where vc_username =  '" & user_name & "'    And  vc_id =   '" & user_id & "' )" & _
                 "ORDER BY vc_n_gugus "


        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim conn As New SqlConnection(connectionString)
        conn.Open()
        Dim comm As SqlCommand = New SqlCommand(strSQL, conn)
        da.SelectCommand = comm
        da.Fill(ds)
        conn.Close()
        Return ds
    End Function


    Public Shared Function SetDataSourceKlinik() As DataSet
        Dim strSQL As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)

        strSQL = "Select vc_k_klinik, vc_n_klinik " & _
                 "FROM rmklinik ORDER BY vc_k_klinik"

        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim conn As New SqlConnection(connectionString)
        conn.Open()
        Dim comm As SqlCommand = New SqlCommand(strSQL, conn)
        da.SelectCommand = comm
        da.Fill(ds)
        conn.Close()
        Return ds
    End Function

    Public Shared Function SetDataSourceDokterKlinik(ByVal cKlinik As String) As DataSet
        Dim strSQL As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)

        If MasterLib.lCekDokterKlinik(cKlinik) Then
            strSQL = "Select  dbo.RMDrKlinik.vc_NID, dbo.SDMDOKTER.vc_nama_kry " & _
                     "FROM dbo.RMDrKlinik INNER JOIN " & _
                     "dbo.SDMDOKTER On dbo.RMDrKlinik.vc_NID = dbo.SDMDOKTER.vc_nid " & _
                     "where dbo.RMDrKlinik.vc_k_klinik = '" & cKlinik & "' " & _
                     "UNION Select TOP(1) '000X' as vc_NID, ' SEMUA' AS vc_nama_kry " & _
                     "FROM dbo.RMDrKlinik"

        Else
            strSQL = "SELECT vc_nid, vc_nama_kry FROM sdmdokter " & _
                     "UNION Select top(1) '000X' as vc_nid, ' SEMUA' as vc_nama_kry " & _
                     "FROM sdmdokter ORDER BY vc_nama_kry "
        End If

        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim conn As New SqlConnection(connectionString)
        conn.Open()
        Dim comm As SqlCommand = New SqlCommand(strSQL, conn)
        da.SelectCommand = comm
        da.Fill(ds)
        conn.Close()
        Return ds
    End Function

    Public Shared Function getKodeGustuSDMFromLogin(ByVal user_name As String, ByVal user_id As String) As String
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim strsql As String

        Dim kode_gugus As String = ""

        strsql = "SELECT gugus.vc_k_gugus FROM pde_user users JOIN SDMKaryawan sdm " & _
                 "ON SDM.vc_nik = users.vc_nik JOIN PubGugusSDM gugus " & _
                 "ON gugus.VC_k_gugus = sdm.vc_kd_pubgugussdm " & _
                 " where vc_username =  '" & user_name & "'    And  vc_id =   '" & user_id & "'  "

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            kode_gugus = dr("vc_k_gugus")
            Exit While
        End While
        dr.Close()
        Return kode_gugus
    End Function


    Public Shared Function getKodeGustuPejabatFromLogin(ByVal user_name As String, ByVal user_id As String) As ArrayList
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim strsql As String

        Dim kode_gugus As ArrayList = New ArrayList

        strsql = "SELECT signer.vc_k_gugus FROM pde_user users JOIN SDMKaryawan sdm " & _
                 "ON SDM.vc_nik = users.vc_nik JOIN Mutu_Mapping_Signer signer " & _
                 "ON signer.vc_nik = sdm.vc_nik" & _
                 " where vc_username =  '" & user_name & "'    And  vc_id =   '" & user_id & "'  "

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            kode_gugus.Add(dr("vc_k_gugus"))
            Exit While
        End While
        dr.Close()
        Return kode_gugus
    End Function

    Public Shared Function lCekDokterKlinik(ByVal cKodeKlinik As String) As Boolean
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim strsql As String

        lCekDokterKlinik = False

        strsql = "Select  dbo.RMDrKlinik.vc_NID, dbo.SDMDOKTER.vc_nama_kry " & _
                     "FROM dbo.RMDrKlinik INNER JOIN " & _
                     "dbo.SDMDOKTER On dbo.RMDrKlinik.vc_NID = dbo.SDMDOKTER.vc_nid " & _
                     "where dbo.RMDrKlinik.vc_k_klinik = '" & cKodeKlinik & "' " & _
                     "ORDER BY dbo.SDMDOKTER.vc_nama_kry "

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim objdatareader As SqlClient.SqlDataReader
        objdatareader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        While objdatareader.Read
            lCekDokterKlinik = True
            Exit While
        End While
        objdatareader.Close()
    End Function

    Public Shared Function SetDataSourcePejabat() As DataSet
        Dim strSQL As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)

        strSQL = "SELECT vc_kode_jbt, vc_nik, vc_Nama_jbt " & _
                 "FROM SKRJabatan " & _
                 "ORDER BY vc_Nama_jbt"

        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim conn As New SqlConnection(connectionString)
        conn.Open()
        Dim comm As SqlCommand = New SqlCommand(strSQL, conn)
        da.SelectCommand = comm
        da.Fill(ds)
        conn.Close()
        Return ds
    End Function

    Public Shared Function SetDataSourcePejabatGustu(ByVal cGustu As String) As DataSet
        Dim strSQL As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)

        strSQL = "SELECT SKRJabatan.vc_kode_jbt, SKRJabatan.vc_Nama_jbt " & _
             "FROM SKRJabatan inner join sdmkaryawan on SKRJabatan.vc_nik = sdmkaryawan.vc_nik " & _
             "where vc_kd_gugus = '" & cGustu & "' " & _
             "ORDER BY vc_Nama_jbt"

        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim conn As New SqlConnection(connectionString)
        conn.Open()
        Dim comm As SqlCommand = New SqlCommand(strSQL, conn)
        da.SelectCommand = comm
        da.Fill(ds)
        conn.Close()
        Return ds
    End Function

    Public Shared Function SetDataSourceKelompokSuratMasuk() As DataSet
        Dim strSQL As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)

        strSQL = "SELECT SKRKelompokSurat.vc_kd_kelompok, '[' + SKRKelompokSurat.vc_kd_kelompok +']   '+ SKRKelompokSurat.vc_Nm_kelompok as vc_Nm_kelompok " & _
                 "FROM SKRKelompokSURAT ORDER BY vc_kd_kelompok"

        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim conn As New SqlConnection(connectionString)
        conn.Open()
        Dim comm As SqlCommand = New SqlCommand(strSQL, conn)
        da.SelectCommand = comm
        da.Fill(ds)
        conn.Close()
        Return ds
    End Function


    Public Shared Function SetDataSourcePejabatGustuNonPendispo(ByVal cGustu As String, ByVal cKodependispo As String) As DataSet
        Dim strSQL As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)

        strSQL = "SELECT SKRJabatan.vc_kode_jbt, SKRJabatan.vc_Nama_jbt " & _
                 "FROM SKRJabatan inner join sdmkaryawan on SKRJabatan.vc_nik = sdmkaryawan.vc_nik " & _
                 "where vc_kd_gugus = '" & cGustu & "' " & _
                 "and SKRJabatan.vc_kode_jbt <> '" & cKodependispo & "' " & _
                 "ORDER BY vc_Nama_jbt"

        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim conn As New SqlConnection(connectionString)
        conn.Open()
        Dim comm As SqlCommand = New SqlCommand(strSQL, conn)
        da.SelectCommand = comm
        da.Fill(ds)
        conn.Close()
        Return ds
    End Function


    Public Shared Function ShowData(ByVal cField1 As String, ByVal cField2 As String, _
    ByVal cTable As String, ByVal cKode As String, ByVal cCaption As String) As String
        Dim strsql As String = ""
        Dim lAda As Boolean
        ShowData = ""
        lAda = False
        strsql = "select " & cField1 & ", " & cField2 & " from " & cTable & " where " & cField1 & "='" & cKode & "' "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        ShowData = ""

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            lAda = True
            If IsDBNull(dr(1)) Then
                ShowData = ""
            Else
                ShowData = Trim(dr(1))
            End If

        End While
        dr.Close()

    End Function

    Public Shared Function BatasTgl() As Integer
        Dim strsql As String = ""
        Dim lAda As Boolean
        BatasTgl = 0
        lAda = False

        strsql = "select VC_KODE, in_tgl from sdm_batas WHERE vc_kode = 'JP' "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            BatasTgl = dr("in_tgl")
        End While
        dr.Close()

    End Function


    Public Shared Function GetEntriData(ByVal cNoAgenda As String, ByRef cUserName As String, ByRef sTglInput As Date, ByRef nUrut As String) As String
        Dim strsql As String = ""
        Dim lAda As Boolean
        GetEntriData = ""
        lAda = False
        strsql = "select vc_no_agenda, dt_tgl_input, vc_urut from skrsuratD where vc_no_agenda = '" & cNoAgenda & "' and vc_user = '" & cUserName & "' "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()


        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            nUrut = dr("vc_urut")
            sTglInput = dr("dt_tgl_input")
        End While
        dr.Close()

    End Function

    Public Shared Function ENCrypt(ByVal Text As String) As String
        ' Encrypts/decrypts the passed string using
        ' a simple ASCII value-swapping algorithm
        Dim strTempChar As String = "", i As Integer
        For i = 1 To Len(Text)
            If Asc(Mid$(Text, i, 1)) < 128 Then
                strTempChar = _
          CType(Asc(Mid$(Text, i, 1)) + 128, String)
            ElseIf Asc(Mid$(Text, i, 1)) > 128 Then
                strTempChar = _
          CType(Asc(Mid$(Text, i, 1)) - 128, String)
            End If
            Mid$(Text, i, 1) = _
                Chr(CType(strTempChar, Integer))
        Next i
        Return Text
    End Function

    Public Shared Function cekusername(ByVal cField1 As String, ByVal cField2 As String, _
    ByVal cTable As String, ByVal cKode As String, ByVal cKodeJabatan As String, ByVal cCaption As String) As String
        Dim strsql As String = ""
        Dim lAda As Boolean
        cekusername = ""
        lAda = False
        strsql = "select " & cField1 & ", " & cField2 & " from " & cTable & " where " & cField1 & "='" & cKode & "' and vc_kode_jbt = '" & cKodeJabatan & "' "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        cekusername = ""

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            lAda = True
            cekusername = Trim(dr(1))
        End While
        dr.Close()

    End Function


    Public Shared Function GetCurrentDate() As DateTime
        ' fungsi untuk mengembalikan tanggal dari database server saat ini

        Dim tgl As DateTime = Nothing

        Try

            Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            Dim command As SqlCommand = New SqlCommand()

            Dim strsql As String = "select getdate() as tgl"
            Dim sqlCommand As New SqlClient.SqlCommand(strsql)
            sqlCommand.Connection = connection
            connection.Open()

            Dim dr As SqlClient.SqlDataReader
            dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)

            While dr.Read
                tgl = dr("tgl")
            End While
            dr.Close()
        Catch ex As Exception
        Finally
            GetCurrentDate = tgl
        End Try


    End Function



    Public Shared Function hitUmur(ByVal dtgllahir As Date, ByRef nTh As Integer, _
                         ByRef nBL As Integer, ByRef nHr As Integer)
        Dim ntglhasil As Integer
        hitUmur = ""
        Dim dTglNow As Date
        dTglNow = SqlDate(GetCurrentDate)
        If IsDate(dtgllahir) Then
            ntglhasil = CDate(dTglNow).Subtract(CDate(dtgllahir)).TotalDays.ToString()
            nTh = Int(ntglhasil / 365)
            ntglhasil = ntglhasil Mod 365
            nBL = Int(ntglhasil / 31)
            ntglhasil = ntglhasil Mod 31
            nHr = ntglhasil
        End If
    End Function

    Public Shared Function SqlDate(ByVal dtmTanggal As Date) As String
        Dim strHasil As String = ""
        strHasil = strHasil & dtmTanggal.Month
        strHasil = strHasil & "/"
        strHasil = strHasil & dtmTanggal.Day
        strHasil = strHasil & "/"
        strHasil = strHasil & dtmTanggal.Year
        strHasil = Trim(strHasil)
        SqlDate = strHasil
    End Function

    Public Shared Function xDate(ByVal dtmTanggal As String) As String
        Dim strHasil As String = ""
        strHasil = strHasil & Mid(dtmTanggal, 4, 2)     'BULAN
        strHasil = strHasil & "/"
        strHasil = strHasil & Mid(dtmTanggal, 1, 2)     'TANGGAL
        strHasil = strHasil & "/"
        strHasil = strHasil & Mid(dtmTanggal, 7, 4)     'TAHUN
        strHasil = Trim(strHasil)
        xDate = strHasil
    End Function


    Public Shared Function ValidTglLahir(ByVal nUmurTh As Integer, ByVal cTgl As String) As String
        Dim nTahun As Integer = GetCurrentDate.Year.ToString
        Dim dTglLahir As Date
        If nUmurTh > 0 Or cTgl = "__/__/____" Then
            nTahun = (nTahun - nUmurTh)
            dTglLahir = "01/07/" & Str(nTahun)
            ValidTglLahir = Format(dTglLahir, "MM/dd/yyyy")
        Else
            ValidTglLahir = "__/__/____"
        End If
    End Function


    Public Shared Function lGetData(ByVal cField As String, ByVal cTable As String, _
                      ByVal cKode As String) As Boolean
        Dim strsql As String = ""
        lGetData = False


        strsql = "select " & cField & " from " & cTable & " where " & cField & "='" & Trim(cKode) & "'"
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim objdatareader As SqlClient.SqlDataReader
        objdatareader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)


        While objdatareader.Read()
            lGetData = True
        End While
        objdatareader.Close()
    End Function


    Public Shared Function replicate(ByVal n As Integer, ByVal cCharacter As String) As String
        Dim i As Integer
        replicate = ""
        i = 1
        While i <= n
            replicate = replicate + cCharacter
            i = i + 1
        End While
    End Function


    Public Shared Function lCekNikJP(ByVal cNik As String, ByVal cKode As String, ByVal nbulan As Integer, ByVal ntahun As Integer) As Boolean
        Dim strsql As String = ""
        lCekNikJP = False

        strsql = "select vc_nik, vc_kd_kel, IN_BULAN, IN_TAHUN from sdm_temp where vc_nik = '" & cNik & "' and vc_kd_kel = '" & cKode & "' and in_bulan = " & nbulan & " and in_tahun = " & ntahun & " "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            lCekNikJP = True
            Exit While
        End While
        dr.Close()

    End Function

    Public Shared Function lCekNikJP2(ByVal cNik As String, ByVal cKode As String, ByVal nbulan As Integer, ByVal ntahun As Integer, ByRef command As SqlCommand) As Boolean
        Dim strsql As String = ""
        lCekNikJP2 = False

        strsql = "select vc_nik, vc_kd_kel, IN_BULAN, IN_TAHUN from sdm_temp where vc_nik = '" & cNik & "' and vc_kd_kel = '" & cKode & "' and in_bulan = " & nbulan & " and in_tahun = " & ntahun & " "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        'Dim command As SqlCommand = New SqlCommand()

        'Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        'sqlCommand.Connection = connection
        connection.Open()

        command.CommandText = strsql
        command.CommandType = CommandType.Text

        Dim dr As SqlClient.SqlDataReader
        dr = command.ExecuteReader
        While dr.Read
            lCekNikJP2 = True
            Exit While
        End While
        dr.Close()

    End Function

    Public Shared Function lGetNik(ByVal cNik As String, ByRef command As SqlCommand) As Boolean
        Dim strsql As String = ""
        lGetNik = False

        strsql = "select vc_nik from sdmkaryawan where vc_nik = '" & cNik & "' and bt_aktif = 1 "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        'Dim command As SqlCommand = New SqlCommand()

        'Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        'sqlCommand.Connection = connection
        connection.Open()

        command.CommandText = strsql
        command.CommandType = CommandType.Text

        Dim dr As SqlClient.SqlDataReader
        dr = command.ExecuteReader
        While dr.Read
            lGetNik = True
            Exit While
        End While
        dr.Close()

    End Function



    Public Shared Function lGetNikNonCom(ByVal cNik As String) As Boolean
        Dim strsql As String = ""
        lGetNikNonCom = False

        strsql = "select vc_nik from sdmkaryawan where vc_nik = '" & cNik & "' and bt_aktif = 1 "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            lGetNikNonCom = True
            Exit While
        End While
        dr.Close()

    End Function

    Public Shared Function CheckUser(ByVal username As String, ByVal password As String, _
                              Optional ByRef cUserID As String = "") As Boolean
        ' fungsi untuk mengecek user apakah ada yang atau tidak
        Dim strSQL As String
        Dim strUser As String = ""
        Dim strDecPass1 As String = password
        Dim strDecPass2 As String = ""
        Dim i As Integer

        strSQL = "select vc_id, vc_username, vc_password from pde_user " & _
                 "where (((vc_username)= '" & Trim(username) & "'));"

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strSQL)
        sqlCommand.Connection = connection
        connection.Open()

        Dim objReader As SqlClient.SqlDataReader
        objReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)

        While objReader.Read()
            strUser = UCase(IIf(Not IsDBNull(objReader("vc_username")), objReader("vc_username").ToString(), ""))
            strDecPass2 = ConvertPass(IIf(Not IsDBNull(objReader("vc_password")), objReader("vc_password").ToString(), ""), "D")
            cUserID = IIf(Not IsDBNull(objReader("vc_id")), objReader("vc_id").ToString, "")
            If strDecPass1.Length <> strDecPass2.Length Then
                Return False
            Else
                For i = 0 To strDecPass2.Length - 1
                    If strDecPass1.Chars(i) <> strDecPass2.Chars(i) Then
                        Return False
                    End If
                Next
            End If
            Return True
        End While
        objReader.Close()

        Return False
    End Function

    Public Shared Function ConvertPass(ByVal ktsandi As String, ByVal cStatus As String) As String
        ' fungsi untuk melakukan konversi nilai password (modif. dari mainlib lama)
        Dim Hasil As String
        Dim I As Integer
        Dim Posisi As String = ""
        Dim Simpan As String = ""
        Dim validasi As String = ""
        Dim nAsc As Integer = 0

        validasi = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.- "
        Hasil = ""
        For I = 1 To Len(Trim(ktsandi))
            Posisi = Mid(Trim(ktsandi), I, 1)
            If cStatus = "E" Then
                Hasil = Chr(Asc(Posisi) + Asc("}"))
            ElseIf cStatus = "D" Then
                nAsc = Asc(Posisi) - Asc("}")
                If (nAsc > 255) Or (nAsc < 0) Then
                    Hasil = Chr(Asc(Posisi))
                Else
                    Hasil = Chr(nAsc)
                End If
            End If
            Simpan = Simpan + Hasil
        Next
        Return Simpan
    End Function


    Public Shared Function GenCounterKode(ByVal ckode As String, ByRef command As SqlCommand) As String
        Dim strsql As String
        Dim nUrut As Integer = 0
        Dim lada As Boolean = False
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)

        strsql = "select vc_prefik, in_counter " & _
                 "from inos_PubCounter " & _
                 "where vc_prefik = '" & ckode & "' "

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            nUrut = dr("in_counter")
            lada = True
        End While
        dr.Close()
        Try
            If lada Then
                strsql = "update inos_PubCounter set in_counter = " & nUrut + 1 & " WHERE vc_prefik = '" & ckode & "' "
            Else
                nUrut = 1
                strsql = "Insert into Inos_Pubcounter (vc_prefik, in_counter) values ('" & ckode & "', " & nUrut & ")"
            End If
            command.CommandText = strsql
            command.CommandType = CommandType.Text
            command.ExecuteNonQuery()
            connection.Close()
            GenCounterKode = replicate((5 - Len(Trim(Str(nUrut + 1)))), "0") + Trim(Trim(Str(nUrut + 1)))
        Catch ex As Exception
            GenCounterKode = ""
            MsgBox("Nomor Urut gagal dibuat!...", MsgBoxStyle.Critical, "Pesan")
        End Try
    End Function

    Public Shared Function SetDataSourceInfeksi() As DataSet
        Dim strSQL As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)

        strSQL = "SELECT vc_kd_infeksi, vc_nm_infeksi " & _
                 "FROM inos_master_infeksi ORDER BY vc_kd_infeksi"

        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim conn As New SqlConnection(connectionString)
        conn.Open()
        Dim comm As SqlCommand = New SqlCommand(strSQL, conn)
        da.SelectCommand = comm
        da.Fill(ds)
        conn.Close()
        Return ds
    End Function


    Public Shared Function lCekSurveilans(ByVal BULAN As Integer, ByVal tahun As Integer, ByVal KodeKamar As String, ByRef command As SqlCommand) As Boolean
        Dim strsql As String = ""
        lCekSurveilans = False

        strsql = "select vc_k_ruang, in_bulan, IN_TAHUN from Inos_Surveilans_hasil where vc_k_ruang = '" & KodeKamar & "' and in_bulan = " & BULAN & " and in_tahun = " & tahun & " "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        'Dim command As SqlCommand = New SqlCommand()

        'Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        'sqlCommand.Connection = connection
        connection.Open()

        command.CommandText = strsql
        command.CommandType = CommandType.Text

        Dim dr As SqlClient.SqlDataReader
        dr = command.ExecuteReader
        While dr.Read
            lCekSurveilans = True
            Exit While
        End While
        dr.Close()

    End Function

    Public Shared Function Export2excel(ByVal gridview1 As GridView, ByVal label2 As Label, ByVal cFileName As String)
        Try

            Export2excel = ""

            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.Buffer = True

            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" & cFileName & ".xls")
            HttpContext.Current.Response.Charset = ""
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
            Dim sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            gridview1.AllowPaging = False
            'gridview1.DataBind()

            Dim frm As New HtmlForm
            label2.Parent.Controls.Add(frm)

            'detil
            gridview1.Parent.Controls.Add(frm)

            frm.Controls.Add(label2)

            frm.Controls.Add(gridview1)
            frm.RenderControl(hw)


            'style to format numbers to string 
            Dim style As String = "<style> .textmode { mso-number-format:\@; } </style>"
            HttpContext.Current.Response.Write(style)
            HttpContext.Current.Response.Output.Write(sw.ToString())
            HttpContext.Current.Response.Flush()
            HttpContext.Current.Response.End()
        Catch ex As Exception

        End Try

    End Function

    Public Shared Function Export2word(ByVal gridview1 As GridView, ByVal label2 As Label, ByVal cFileName As String)
        Try

            Export2word = ""
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.Buffer = True
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" & cFileName & ".doc")
            HttpContext.Current.Response.Charset = ""
            HttpContext.Current.Response.ContentType = "application/vnd.ms-word "
            Dim sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)
            gridview1.AllowPaging = False
            'gridview1.DataBind()

            Dim frm As New HtmlForm
            gridview1.Parent.Controls.Add(frm)
            frm.Controls.Add(label2)
            frm.Controls.Add(gridview1)
            frm.RenderControl(hw)

            HttpContext.Current.Response.Output.Write(sw.ToString())
            HttpContext.Current.Response.Flush()
            HttpContext.Current.Response.[End]()
        Catch ex As Exception
            'pesan("Data Gagal di eksport ke word...")
        End Try

    End Function
End Class
