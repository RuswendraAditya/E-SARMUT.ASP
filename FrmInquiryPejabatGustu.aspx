<%@ Page Language="VB" EnableEventValidation="false" AutoEventWireup="false" CodeFile="FrmInquiryPejabatGustu.aspx.vb" Inherits="FrmInquiryPejabatGustu" MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <div class="page-header">

        <h3>Daftar Pejabat Gugus Tugas</h3>

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
                                       <asp:ListItem Text="Nama"></asp:ListItem>
                                   </asp:DropDownList>
                              <asp:TextBox ID="txtSearch" runat="server" Enabled ="False"></asp:TextBox>
                              <asp:Button ID="btnSearch" CssClass="bg-info" runat="server" Text="Search" />
                              <br />
                              <br />
                              <div class="table-responsive">
                                  <asp:GridView ID="gridViewPejabat" Width="90%" runat="server" ShowFooter="True" CssClass="table table-hover table-striped" AutoGenerateColumns="False"
                                      EmptyDataText="Tidak Ada Data Yang Ditampilkan" AllowPaging="true" AllowSorting="true"
                                      OnPageIndexChanging="OnPageIndexChanging" PageSize="10" DataKeyNames="vc_k_gugus">
                                      <Columns>
                                          <asp:BoundField DataField="VC_n_gugus" HeaderText="Gugus Tugas" SortExpression="VC_n_gugus" />
                                          <asp:BoundField DataField="vc_nama_kry" HeaderText="Nama" SortExpression="vc_nama_kry" />
                                          <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                              <ItemTemplate>
                                                  <asp:LinkButton ID="BtnEdit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="EditPejabat" runat="server" CausesValidation="False" CssClass="btn btn-primary" Text="Edit"></asp:LinkButton>
                                              </ItemTemplate>
                                          </asp:TemplateField>
                                      </Columns>
                                      <EmptyDataTemplate>Tidak Ada Data Yang Ditampilkan</EmptyDataTemplate>

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
              <asp:Button runat="server" class="btn btn-primary" Text="Tambah Data" ID="bntAddPejabat" />

              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
             <asp:Button runat="server" class="btn btn-danger" Text="Kembali" ID="btnKembali" />

              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
             <asp:ImageButton runat="server" class="btn btn-default " ImageUrl="~/Images/excel_download.png" Width="100" Height="100" AlternateText="Download to Excel" ID="BtnDownload" />
          </div>
      </div>


  </form>

</asp:Content>
