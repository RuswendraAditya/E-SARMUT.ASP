<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" MasterPageFile="~/MasterPageBlank.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=1,initial-scale=1,user-scalable=1" />



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
     <br />
    <div class="col-md-4 col-md-offset-4 form-login">
        <div class="outter-form-login">
            <form action="check-login.php" class="inner-login">
                <section>
                    <p style="text-align: center;">
                        <img src="Images/logo_bethesda.jpg" />
                    </p>
                </section>
                <section>
                    <h3 style="text-align: center;">Sasaran Mutu Online</h3>
                </section>
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Silahkan Login</h3>
                    </div>
                    <div class="panel-body">
                        <div class="input-group">
                            <span class="input-group-addon" id="basic-addon1"><span class="glyphicon glyphicon-user" aria-hidden="true"></span></span>
                            <asp:TextBox class="form-control" ID="txtUserName" placeholder="Masukan User Name" runat="server" Width="100%"></asp:TextBox>
                        </div>
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon" id="basic-addon2"><span class="glyphicon glyphicon-lock" aria-hidden="true"></span></span>
                            <asp:TextBox class="form-control" ID="txtpwd" placeholder="Masukan Password" runat="server" TextMode="Password" Width="100%"></asp:TextBox>
                        </div>
                        <br />
                        <br />
                        <asp:Button CssClass="btn btn-block btn-primary" ID="btnLogin" runat="server" Text="Login"></asp:Button>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
