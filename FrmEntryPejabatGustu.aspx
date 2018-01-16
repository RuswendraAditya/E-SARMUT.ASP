<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmEntryPejabatGustu.aspx.vb" Inherits="FrmEntryPejabatGustu" MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <div class="page-header">

        <h3>Pejabat Gugus Tugas</h3>

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
                    ForeColor="#FF3300"
                  ValidationGroup="saveMaster"
                  ErrorMessage="* Wajib Memilih Gugus Tugas"
                  Display="Dynamic">
              </asp:RequiredFieldValidator>
          </div>
      </div>

      <div class="form-group">
          <label for="R
              " class="control-label col-xs-2" style="font-size: small">Pejabat</label>
          <div class="col-xs-10">
              <telerik:RadComboBox ID="RadComboNIK" Font-Size="Small" RenderMode="Lightweight" runat="server" Width="220" DropDownAutoWidth="Enabled" EmptyMessage="Pilih Nama Pejabat" class="form-control" Filter="Contains" MarkFirstMatch="true">
              </telerik:RadComboBox>
                <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="RadComboNIK"
                  CssClass="input-error"
                    ForeColor="#FF3300"
                  ValidationGroup="saveMaster"
                  ErrorMessage="* Wajib Memilih Nama Pejabat"
                  Display="Dynamic">
              </asp:RequiredFieldValidator>
          </div>
      </div>
     
      <div class="form-group">
          <div class="col-xs-offset-2 col-xs-10">
                 <asp:LinkButton runat="server" class="btn btn-primary"   OnClick="BtnSaveSigner_Click"  OnClientClick="return getConfirmation(this, 'Konfirmasi','Apakah Anda Yakin akan menyimpan Data?');" Text="Save" ID="BtnSaveSigner" />

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
