<%@ Page Language="C#" AutoEventWireup="True" Inherits="n28.Form_Contract_TaskDistribute" Codebehind="TaskDistribute.aspx.cs" %>

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
        1、水 1-2号 administrator<br />
        2、气 1-2号 administrator<br />
        3、气 3-4号 administrator<br />
        4、气 1-2号 llw<br />
        <br />
        <br />
        <asp:Button ID="btnDistribute" runat="server" Text="分配" 
            onclick="btnDistribute_Click" />
    </div>
    <div>
        <img src="../TempImg/采样任务分配.jpg" />
    </div>
    <asp:HiddenField ID="hidContractId" runat="server" />
    <asp:HiddenField ID="hidIsNew" runat="server" />
    </form>
</body>
</html>
