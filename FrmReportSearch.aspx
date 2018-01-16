<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmReportSearch.aspx.vb" Inherits="FrmReportSearch" MasterPageFile="~/MasterPage.master" %>

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

        <h3>Laporan Sasaran Mutu</h3>

    </div>
    </div>

  <form class="form-horizontal" runat="server">  
         <asp:Literal ID="lblMsg" runat="server" />
      <asp:ScriptManager ID="ScriptManager1" runat="server"  ></asp:ScriptManager>
      <div class="form-group">
          <label for="txtPeriode" class="control-label col-xs-2" style="font-size: small">Periode</label>
          <div class="col-xs-10">

              <telerik:RadMonthYearPicker ID="RadMonthYearPeriod" AutoPostBack ="True"
                   DateInput-EmptyMessage="Pilih Periode" RenderMode="Lightweight" DateInput-DisplayDateFormat="MM-yyyy" runat="server">
                 </telerik:RadMonthYearPicker>
                 <asp:RequiredFieldValidator runat="server"
                  ControlToValidate="RadMonthYearPeriod"
                  CssClass="input-error"
                  ErrorMessage=" * wajib diisi"
                  ValidationGroup="save">
              </asp:RequiredFieldValidator>
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
                  Display="Dynamic">
              </asp:RequiredFieldValidator>
            
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
                  Display="Dynamic">
              </asp:RequiredFieldValidator>
          </div>
      </div>
        <div class="form-group">
          <div class="col-xs-offset-2 col-xs-10">
             
              <asp:Button ID="btnViewReport" runat="server"  class="btn btn-primary" Text="View Report" Width="107px" ValidationGroup="save" />
&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="btnKembali" runat="server" Text="Kembali"  class="btn btn-danger" />
             
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
