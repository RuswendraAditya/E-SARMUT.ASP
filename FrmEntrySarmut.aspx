<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmEntrySarmut.aspx.vb" Inherits="FrmEntrySarmut" MasterPageFile="~/MasterPage.master" %>


<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .form-control {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <div class="page-header">

        <h3>Form Entri Sasaran Mutu</h3>

    </div>
    </div>

  <form class="form-horizontal" runat="server">
      <asp:Literal ID="lblMsg" runat="server" />
      <div id="modalPopUp" class="modal fade" role="dialog">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <button type="button" class="close" data-dismiss="modal">&times;</button>
                      <h4 class="modal-title">
                          <span id="spnTitle"></span>
                      </h4>
                  </div>
                  <div class="modal-body">
                      <p>
                          <span id="spnMsg"></span>.
                      </p>
                  </div>
                  <div class="modal-footer">
                      <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                      <button type="button" id="btnConfirm" class="btn btn-danger">
                          Yes</button>
                  </div>
              </div>
          </div>
      </div>
      <div id="alertPopUp" class="modal fade" role="dialog">
          <div class="modal-dialog">
              <div class="modal-content">
                  <div class="modal-header">
                      <button type="button" class="close" data-dismiss="modal">&times;</button>
                      <h4 class="modal-title">
                          <span id="spnTitleAlert"></span>
                      </h4>
                  </div>
                  <div class="modal-body">
                      <p>
                          <span id="spnMsgAlert"></span>.
                      </p>
                  </div>
                  <div class="modal-footer">
                      <button type="button" id="btnConfirmAlert" class="btn btn-default">
                          OK</button>
                  </div>
              </div>
          </div>
      </div>
      <asp:ScriptManager ID="ScriptManager1" runat="server"  ></asp:ScriptManager>
      <div class="form-group">
          <label for="txtPeriode" class="control-label col-xs-2" style="font-size: small">Periode</label>
          <div class="col-xs-10">

              <telerik:RadMonthYearPicker ID="RadMonthYearPeriod" OnSelectedDateChanged="RadMonthYearPeriod_SelectedDateChanged" AutoPostBack ="True"
                   DateInput-EmptyMessage="Pilih Periode" RenderMode="Lightweight" DateInput-DisplayDateFormat="MM-yyyy" runat="server"></telerik:RadMonthYearPicker>

              <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="RadMonthYearPeriod"
                  CssClass="input-error"
                  ErrorMessage=" * wajib diisi"
                  ValidationGroup="save" ForeColor="#FF3300"></asp:RequiredFieldValidator>
          </div>
      </div>
      <div class="form-group">
          <label for="RadComboGustu" class="control-label col-xs-2" style="font-size: small">Gugus Tugas</label>
          <div class="col-xs-10">

             <telerik:RadComboBox ID="RadComboGustu" AutoPostBack="true" runat="server" EmptyMessage="Pilih Gugus Tugas" class="form-control" Filter="Contains" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Width="250px">
              </telerik:RadComboBox>
              <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="RadComboGustu"
                  CssClass="input-error"
                  ValidationGroup="save"
                  ErrorMessage=" * Gugus Tugas Wajib Dipilih"
                  Display="Dynamic" ForeColor="#FF3300"></asp:RequiredFieldValidator>
            
          </div>
      </div>
      <div class="form-group">
          <label for="txtNamaSarmut" class="control-label col-xs-2" style="font-size: small">Sasaran Mutu</label>
          <div class="col-xs-10">
                           
          <telerik:RadComboBox ID="RadComboSarmut" Width="400px" runat="server" EmptyMessage="Pilih Sasaran Mutu Gugus Tugas" class="form-control" Filter="Contains" MarkFirstMatch="true" DropDownAutoWidth="Enabled" AutoPostBack="True">
              </telerik:RadComboBox>
              <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="RadComboSarmut"
                  CssClass="input-error"
                  ValidationGroup="save"
                  ErrorMessage=" * Sasaran Mutu Wajib Dipilih"
                  Display="Dynamic" ForeColor="#FF3300"></asp:RequiredFieldValidator>
          </div>
      </div>

      <div class="form-group">
          <label for="txtTarget" class="control-label col-xs-2" style="font-size: small">Target</label>
          <div class="col-xs-10">
              <asp:TextBox ID="TxtTarget" runat="server" CssClass="form-control" ReadOnly="True" Width="70px" AutoPostBack="True"></asp:TextBox>

          </div>
      </div>
       <div class="form-group">
          <label for="TxtNotePencapaian" class="control-label col-xs-2" style="font-size: small">Pencapaian</label>
          <div class="col-xs-10">
              <asp:TextBox ID="TxtNotePencapaian" runat="server" CssClass="form-control" Height="78px" TextMode="MultiLine" Width="448px" ></asp:TextBox>

              <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="TxtNotePencapaian"
                  CssClass="input-error"
                  ValidationGroup="save"
                  ErrorMessage=" * Catatan Pencapaian Wajib Diisi Diisi"
                  Display="Dynamic" ForeColor="#FF3300"></asp:RequiredFieldValidator>
          </div>
      </div>
      <div class="form-group">
          <label for="txtPencapaian" class="control-label col-xs-2" style="font-size: small">Skor Pencapaian</label>
          <div class="col-xs-10">
              <asp:TextBox ID="txtPencapaian" OnTextChanged="txtPencapaian_TextChanged" CssClass="form-control" runat="server" Width="70px" AutoPostBack="True"></asp:TextBox>
              <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="txtPencapaian"
                  CssClass="input-error"
                  ValidationGroup="save"
                  ErrorMessage=" * Pencapaian Wajib Diisi"
                  Display="Dynamic" ForeColor="#FF3300"></asp:RequiredFieldValidator>
          </div>
      </div
   
      <div class="form-group">
          <label for="txtPencapaian" class="control-label col-xs-2" style="font-size: small">Hasil</label>
          <div class="col-xs-10">
              <asp:RadioButtonList ID="rbPencapaian" runat="server" Font-Size="Small" Enabled="False">
                  <asp:ListItem Value="1">Tercapai</asp:ListItem>
                  <asp:ListItem Value="0">Tidak Tercapai</asp:ListItem>
              </asp:RadioButtonList>
              <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="rbPencapaian"
                  
                  ValidationGroup="save"
                  ErrorMessage=" * Cek Pencapaian "
                  Display="Dynamic" CssClass="input-error" ForeColor="#FF3300"></asp:RequiredFieldValidator>
          </div>
      </div>
      <div class="form-group">
          <label for="txtMasalah" class="control-label col-xs-2" style="font-size: small">Permasalahan *</label>
          <div class="col-xs-10">
             <asp:TextBox ID="txtMasalah" runat="server" CssClass="form-control" Height="78px" TextMode="MultiLine" Wi dth="448px" Visible="False"></asp:TextBox>


              <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="txtMasalah"
                  CssClass="input-error"
                  ValidationGroup="save"
                  ErrorMessage="Masalah Wajib Diisi jika hasil tidak tercapai"
                  Display="Dynamic" ID="validatorMasalah" ForeColor="#FF3300"></asp:RequiredFieldValidator>
          </div>
      </div>
      <div class="form-group">
          <label for="txtMasalah" class="control-label col-xs-2" style="font-size: small">Tindakan *</label>
          <div class="col-xs-offset-2 col-xs-10">
              <div class="table-responsive">
                  <asp:GridView ID="gridViewTindakan" Visible="false" Width="20%" runat="server" ShowFooter="True" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False"
                      EmptyDataText="There are no data records to display.">
                      <Columns>

                          <asp:TemplateField HeaderText="Tindak Lanjut">
                              <ItemTemplate>
                                  <asp:TextBox ID="txtTindakLanjut" CssClass="form-control" runat="server" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                  <asp:RequiredFieldValidator runat="server"
                                      ControlToValidate="txtTindakLanjut"
                                      CssClass="input-error"
                                      ValidationGroup="save"
                                      ErrorMessage="*"
                                        ForeColor="#FF3300"
                                      Display="Dynamic">
                                  </asp:RequiredFieldValidator>
                              </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="100px" />

                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="PIC">
                              <ItemTemplate>
                                  <asp:TextBox ID="txtPIC" CssClass="form-control" runat="server" Width="150px"></asp:TextBox>
                                  <asp:RequiredFieldValidator runat="server"
                                      ControlToValidate="txtPIC"
                                      CssClass="input-error"
                                      ValidationGroup="save"
                                        ForeColor="#FF3300"
                                      ErrorMessage="*"
                                      Display="Dynamic">
                                  </asp:RequiredFieldValidator>
                              </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="70px" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Due Date">
                              <ItemTemplate>
                                  <telerik:RadDatePicker ID="RadDatePickerDueDate" CssClass="form-control" runat="server">
                                  </telerik:RadDatePicker>
                                  <asp:RequiredFieldValidator runat="server"
                                      ControlToValidate="RadDatePickerDueDate"
                                      CssClass="input-error"
                                      ValidationGroup="save"
                                      ErrorMessage="*"
                                        ForeColor="#FF3300"
                                      Display="Dynamic">
                                  </asp:RequiredFieldValidator>
                              </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="70px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Status">
                              <ItemTemplate>
                                  <asp:TextBox ID="txtStatus" CssClass="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator runat="server"
                                      ControlToValidate="txtStatus"
                                      CssClass="input-error"
                                      ValidationGroup="save"
                                      ForeColor="#FF3300"
                                      ErrorMessage="*"
                                      Display="Dynamic">
                                  </asp:RequiredFieldValidator>
                              </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="70px" />
                              <FooterStyle HorizontalAlign="Right" />
                              <FooterTemplate>
                                  <asp:Button ID="BtnTambahTindakLanjut" runat="server" Text="Tambah Tindak Lanjut" Font-Size="Small" OnClick="BtnAddTindakan_Click" />

                              </FooterTemplate>
                          </asp:TemplateField>
                          <asp:CommandField ShowDeleteButton="True" />
                      </Columns>
                      <EmptyDataTemplate>No Record Available</EmptyDataTemplate>

                      <RowStyle BackColor="#EFF3FB" />

                      <AlternatingRowStyle BackColor="White" />
                  </asp:GridView>
              </div>
          </div>
      </div>
       <div class="form-group">
          <label for="txtAlasanReject" class="control-label col-xs-2" style="font-size: small">Alasan Reject</label>
          <div class="col-xs-10">
              <asp:TextBox ID="txtAlasanReject" runat="server" CssClass="form-control" Height="78px" TextMode="MultiLine" Width="448px" Visible="False"></asp:TextBox>


              <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="txtAlasanReject"
                  CssClass="input-error"
                  ValidationGroup="save"
                  ErrorMessage="Alasan Reject Wajib Diisi"
                  Display="Dynamic" ID="RequiredFieldTxtAlasan" ForeColor="#FF3300"></asp:RequiredFieldValidator>
          </div>
      </div>
      <div class="form-group">
          <div class="col-xs-offset-2 col-xs-10">
              <asp:LinkButton runat="server" class="btn btn-primary" OnClick="btnSave_Click" OnClientClick="return getConfirmation(this, 'Konfirmasi','Apakah Anda Yakin akan menyimpan Data?');" Text="Save" ID="btnSave" />

              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:Button runat="server" class="btn btn-danger" Text="Kembali" ID="btnKembali" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
               <asp:LinkButton runat="server" class="btn btn-success"  OnClick="btnApprove_Click" OnClientClick="return getConfirmation(this, 'Konfirmasi','Apakah Anda Yakin approve data Transaksi?');" Text="Approve" ID="btnApprove" Visible="False" />

              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:LinkButton runat="server" class="btn btn-warning"  OnClick="btnReject_Click" Text="Reject" ID="btnReject" OnClientClick="return getConfirmation(this, 'Konfirmasi','Apakah Anda Yakin Reject Data Transaksi?');" Visible="False" />
          </div>
      </div>


  </form>

    <script type="text/javascript">
        function getConfirmation(sender, title, message) {
            $("#spnTitle").text(title);
            $("#spnMsg").text(message);
            $('#modalPopUp').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
        function getAlert(sender, title, message) {
            $("#spnTitleAlert").text(title);
            $("#spnMsgAlert").text(message);
            $('#alertPopUp').modal('show');
            $('#btnConfirmAlert').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
    </script>

</asp:Content>
