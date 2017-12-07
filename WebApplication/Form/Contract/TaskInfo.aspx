<%@ Page Language="C#" AutoEventWireup="True" Inherits="n29.Form_Contract_TaskInfo" Codebehind="TaskInfo.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        function Save() {

            //alert('bb');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        任务单号：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        计划开始日期：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        计划完成日期：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="保存" />
    </div>
    <div>
        <img src="../TempImg/任务下达.jpg" />
    </div>
    <asp:HiddenField ID="hidContractId" runat="server" />
    <asp:HiddenField ID="hidIsNew" runat="server" />
    </form>
</body>
</html>
