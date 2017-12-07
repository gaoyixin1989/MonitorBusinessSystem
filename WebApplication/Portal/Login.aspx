<%@ Page Language="C#" AutoEventWireup="True" Inherits="Portal_Login" Codebehind="Login.aspx.cs" %>

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
            $("#switchSkin").attr("href", "../App_Themes/" + $.cookie("skin") + "/styles/login2013.css");

            /*
            功能：实现 按钮重置事件
            创建人：胡方扬
            创建时间： 2013-06-13 8：50
            修改人：
            修改时间：
            */
            $("#btnReset").bind("click", function () {
                $("#username").attr("value", "")
                $("#password").attr("value", "")
                $("#username").focus();
            })
        })
    </script>
    <!--防止登录到主页后，点击浏览器后退按钮转向登录页面-->
    <script type="text/javascript" language="javaScript"> 
    javascript: window.history.forward(1); 
    </script> 


</head>
<body>
<div class="login">
<form id="form1" runat="server">
<!--header start-->
<div class="header">
<div class="logo">logo</div>
</div>
<!--header end-->

<!--login_min start-->
<div class="login_min">
<ul>
<li><label for="username" >账号：</label><asp:TextBox ID="username" runat="server" class="input2013"></asp:TextBox></li>
<li><label for="password">密码：</label><asp:TextBox ID="password" TextMode="Password" runat="server" class="input2013" ></asp:TextBox></li>
<li><asp:Button ID="btnSubmit" Text="" runat="server" OnClick="btnSubmit_Click"  class="register mgl42"/><input class="reset mgl0" type="button" id="btnReset" name="重置"/></li>
</ul>
</div>
<!--login_min end-->
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="用户名或密码错误，如果连续错误5次系统将锁定此用户"></asp:Label><br />
    <asp:Label ID="lblCount" Visible="false" Text="" runat="server"></asp:Label>
</form>
</div>
</body>
</html>
