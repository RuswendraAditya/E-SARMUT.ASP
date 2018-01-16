<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmEntryMasterSarmut.aspx.vb" Inherits="FrmEntryMasterSarmut" MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <div class="page-header">

        <h3>Sasaran Mutu Gugus Tugas</h3>

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
      <asp:ScriptManager ID="ScriptManager1" runat="server"   ></asp:ScriptManager>
      <div class="form-group">
          <label for="R
              " class="control-label col-xs-2" style="font-size: small">Gugus Tugas</label>
          <div class="col-xs-10">

              <telerik:RadComboBox ID="RadComboGustu" Font-Size="Small" RenderMode="Lightweight" runat="server" Width="220" DropDownAutoWidth="Enabled" EmptyMessage="Pilih Gugus Tugas" class="form-control" Filter="Contains" MarkFirstMatch="true">
              </telerik:RadComboBox>
                <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="RadComboGustu"
                  CssClass="input-error"
                  ValidationGroup="saveMaster"
                    ForeColor="#FF3300"
                  ErrorMessage="* Wajib Memilih Gugus Tugas"
                  Display="Dynamic">
              </asp:RequiredFieldValidator>
          </div>
      </div>
      <div class="form-group">
          <label for="txtNamaSarmut" class="control-label col-xs-2" style="font-size: small">Sasaran Mutu</label>
          <div class="col-xs-10">
              <asp:TextBox ID="txtNamaSarmut" CssClass="form-control" class="form-control" Font-Size="Small" runat="server" TextMode="MultiLine" Width="419px" Height="99px"></asp:TextBox>
              <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="txtNamaSarmut"
                  CssClass="input-error"
                  ValidationGroup="saveMaster"
                  ForeColor="#FF3300"
                  ErrorMessage=" * Nama Sasaran Mutu wajib diisi"
                  Display="Dynamic">
              </asp:RequiredFieldValidator>
          </div>
      </div>
      <div class="form-group">
          <label for="txtTarget" class="control-label col-xs-2" style="font-size: small">Target(%)</label>
          <div class="col-xs-10">
              <asp:TextBox ID="txtTarget" type="number" CssClass="form-control" runat="server" Width="67px"></asp:TextBox>
              <asp:RangeValidator runat="server"
                  ID="valrNumberOfPreviousOwners"
                  ControlToValidate="txtTarget"
                  ValidationGroup="saveMaster"
                  Type="Integer"
                  MinimumValue="0"
                  MaximumValue="100"
                  CssClass="input-error"
                  ForeColor="#FF3300"
                  ErrorMessage="Masukan Angka 0 s/d 100"
                  Display="Dynamic">
              </asp:RangeValidator>
                 <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="txtTarget"
                  ValidationGroup="saveMaster"
                  CssClass="input-error"
                     ForeColor="#FF3300"
                  ErrorMessage="* Target Wajib Diisi"
                  Display="Dynamic"/>
          </div>
      </div>
      <div class="form-group">
          <label for="txtTarget" class="control-label col-xs-2" style="font-size: small">Syarat Pencapaian</label>
          <div class="col-xs-10">
          <asp:RadioButtonList ID="rbSyarat" runat="server" Font-Size="Small">
            <asp:ListItem Value="<=">Sama/Kurang dari target</asp:ListItem>
            <asp:ListItem Value=">=">Sama/Lebih dari target</asp:ListItem>
        </asp:RadioButtonList>
               <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="rbSyarat"
                   ValidationGroup="saveMaster"
                  CssClass="input-error"
                   ForeColor="#FF3300"
                  ErrorMessage="* Wajib Memilih Syarat Pencapaian"
                  Display="Dynamic">
              </asp:RequiredFieldValidator>
          </div>
      </div>
      <div class="form-group">
          <label for="radDatePickerStartDate" class="control-label col-xs-2" style="font-size: small">Start Date</label>
          <div class="col-xs-10">
              <telerik:RadDatePicker CssClass="form-control" ID="RadDatePickerStartDate" runat="server" DateInput-DisplayDateFormat="dd/MM/yyyy" DateInput-DateFormat="dd/MM/yyyy">
              </telerik:RadDatePicker>
               <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="RadDatePickerStartDate"
                  CssClass="input-error"
                   ForeColor="#FF3300"
                   ValidationGroup="saveMaster"
                  ErrorMessage="* Wajib Mengisi Start Date"
                  Display="Dynamic">
              </asp:RequiredFieldValidator>
               <asp:CompareValidator ID="cvStartDate" runat="server" ControlToCompare="radDatePickerEndDate" ErrorMessage="Start Date hrs < End Date"
                            ControlToValidate="RadDatePickerStartDate" CssClass="labelError" ForeColor="#FF3300" ToolTip="Start Date hrs < End Date"   ValidationGroup="saveMaster"
                            Operator="LessThan" Type="Date"></asp:CompareValidator>           
          </div>

      </div>
      <div class="form-group">
          <label for="RadDatePickerEndDate" class="control-label col-xs-2" style="font-size: small">End Date</label>
          <div class="col-xs-10">
              <telerik:RadDatePicker CssClass="form-control" ID="RadDatePickerEndDate" runat="server" DateInput-DisplayDateFormat="dd/MM/yyyy" DateInput-DateFormat="dd/MM/yyyy">
              </telerik:RadDatePicker>
               <asp:CompareValidator ID="cvEndDate" runat="server" ControlToCompare="radDatePickerStartDate" ErrorMessage="End Date hrs > Start Date"
                            ControlToValidate="RadDatePickerEndDate" CssClass="labelError" ToolTip="End Date hrs > Start Date"   ValidationGroup="saveMaster" ForeColor="#FF3300"
                            Operator="GreaterThan" Type="Date"></asp:CompareValidator>                        
          </div>

      </div>
      <div class="form-group">
          <div class="col-xs-offset-2 col-xs-10">
                 <asp:LinkButton runat="server" class="btn btn-primary"   OnClick="BtnSaveMaster_Click"  OnClientClick="return getConfirmation(this, 'Konfirmasi','Apakah Anda Yakin akan menyimpan Data?');" Text="Save" ID="BtnSaveMaster" />

              &nbsp;  &nbsp;   &nbsp;
                <asp:Button runat="server" class="btn btn-danger" Text="Kembali" ID="btnKembali" />

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
