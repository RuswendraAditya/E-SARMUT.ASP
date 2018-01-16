<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeFile="Home.aspx.vb" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div class="container">
        <br />
        <form class="form-signin" role="form" runat="server">
            <br />
             <br />
            <section>
                <p style="text-align: center;">
                    <img src="Images/logo_login.jpg" />
            </section>
            <div class="jumbotron" style="margin-top: 15px;background-image: url(images/back_blue.jpg)">
                <h1  id="Hallo" style="font-family: Verdana; color: #ffffff; font-weight: bold;">Sasaran Mutu Online</h1>
                <p style="color:#ffd800;font-weight: bold;">Selamat Datang di Sasaran Mutu Online</p>
                <p style="color:#ffd800;font-weight: bold;">Silahkan menggunakan pilihan menu diatas   </p>
                <asp:LinkButton ID="btnNotif" class="btn btn-sm btn-danger" role="Button" runat="server" Text="Menunggu Approval "><span id="spnMyTask" runat="server" class="badge"><% Response.Write(Total) %></span></asp:LinkButton>
            </div>
        </form>

    </div>
</asp:Content>
