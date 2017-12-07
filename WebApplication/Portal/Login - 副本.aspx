<%@ Page Language="C#" AutoEventWireup="true" Inherits="Portal_Login" Codebehind="Login.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="switchSkin" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-cookie/jquery.cookie.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#username").focus();
            if ($.cookie("skin") == null) {
                $.cookie("skin", "default", { path: '/' });
            }
            $("#switchSkin").attr("href", "../App_Themes/" + $.cookie("skin") + "/styles/login.css");
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="logo">
        &nbsp;</div>
    <div class="login">
        <div class="loginInner">
            <div class="form">
                <span class="name">用户名</span> <span class="ipt">
                    <asp:TextBox ID="username" runat="server" class="input"></asp:TextBox></span>
                <span class="name">密码</span> <span class="ipt">
                    <asp:TextBox ID="password" TextMode="Password" runat="server" class="input"></asp:TextBox></span>
                <span class="name">&nbsp;</span> <span class="btn">
                    <asp:Button ID="btnSubmit" Text="" runat="server" OnClick="btnSubmit_Click" /></span>
            </div>
        </div>
    </div>
    <div class="copyright">
        珠海高凌信息科技有限公司提供技术支持</div>
    <div class="loginBtm">
        <div>
            &nbsp;</div>
    </div>
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="用户名或密码错误，如果连续错误5次系统将锁定此用户"></asp:Label><br />
    <asp:Label ID="lblCount" Visible="false" Text="" runat="server"></asp:Label>
    </form>
</body>
</html>
