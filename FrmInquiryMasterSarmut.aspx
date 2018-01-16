<%@ Page Language="VB" EnableEventValidation="false" AutoEventWireup="false" CodeFile="FrmInquiryMasterSarmut.aspx.vb" Inherits="FrmInquiryMasterSarmut" MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <div class="page-header">

        <h3>Daftar Sasaran Mutu</h3>

    </div>
    </div>

  <form class="form-horizontal" runat="server">

      <div id="mainContainer" class="container">

          <div class="shadowBox">

              <div class="page-container">
                  <div class="container">

                      <div class="row">

                          <div class="col-lg-12 ">
                              Filter By:
                                   <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True"
                                       OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                       <asp:ListItem Text="Semua"></asp:ListItem>
                                       <asp:ListItem Text="Gugus Tugas"></asp:ListItem>
                                       <asp:ListItem Text="Sasaran Mutu"></asp:ListItem>
                                   </asp:DropDownList>
                              <asp:TextBox ID="txtSearch" runat="server" Enabled ="False"></asp:TextBox>
                              <asp:Button ID="btnSearch" CssClass="bg-info" runat="server" Text="Search" />
                              <br />
                              <br />
                              <div class="table-responsive">
                                  <asp:GridView ID="gridViewSarmutMaster" Width="90%" runat="server" ShowFooter="True" CssClass="table table-hover table-striped" AutoGenerateColumns="False"
                                      EmptyDataText="Tidak Ada Data Sasaran Mutu Yang Ditampilkan" AllowPaging="true" AllowSorting="true"
                                      OnPageIndexChanging="OnPageIndexChanging" PageSize="10" DataKeyNames="in_sarmut_id">
                                      <Columns>
                                          <asp:BoundField DataField="VC_n_gugus" HeaderText="Gugus Tugas" SortExpression="VC_n_gugus" />
                                          <asp:BoundField DataField="vc_nama_sarmut" HeaderText="Sasaran Mutu" SortExpression="vc_nama_sarmut" />
                                          <asp:BoundField DataField="in_target" HeaderText="Target(%)" SortExpression="in_target" />
                                          <asp:BoundField DataField="dt_effective_start_date" HeaderText="Start Date" SortExpression="dt_effective_start_date" />
                                          <asp:BoundField DataField="dt_effective_end_date" HeaderText="End Date" SortExpression="dt_effective_end_date" />
                                          <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                              <ItemTemplate>
                                                  <asp:LinkButton ID="BtnEdit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="EditSarmut" runat="server" CausesValidation="False" CssClass="btn btn-primary" Text="Edit"></asp:LinkButton>
                                              </ItemTemplate>
                                          </asp:TemplateField>
                                      </Columns>
                                      <EmptyDataTemplate>Tidak Ada Data Sasaran Mutu</EmptyDataTemplate>

                                      <RowStyle BackColor="#EFF3FB" />

                                      <AlternatingRowStyle BackColor="White" />

                                  </asp:GridView>

                              </div>

                          </div>
                      </div>
                  </div>
              </div>
          </div>

      </div>

      <div class="form-group">
          <div class="col-xs-offset-2 col-xs-10">
              <asp:Button runat="server" class="btn btn-primary" Text="Tambah Data" ID="btAddsarmut" />

              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
             <asp:Button runat="server" class="btn btn-danger" Text="Kembali" ID="btnKembali" />

              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
             <asp:ImageButton runat="server" class="btn btn-default " ImageUrl="~/Images/excel_download.png" Width="100" Height="100" AlternateText="Download to Excel" ID="BtnDownload" />
          </div>
      </div>


  </form>

</asp:Content>
