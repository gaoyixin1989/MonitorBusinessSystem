<%@ Page Language="C#" AutoEventWireup="True" Inherits="n24.Form_Contract_ContractInfo" Codebehind="ContractInfo.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        function Save() {

            return 1;
            //alert('bb');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="委托书内容"></asp:Label>
        <asp:TextBox ID="txtContent" runat="server">一般性委托</asp:TextBox>
        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
    </div>
    <div>
        <asp:TextBox ID="txtApprove" runat="server" Visible="False"></asp:TextBox>
        <asp:Button ID="btnApprove" runat="server" Text="审核" OnClick="btnApprove_Click" Visible="False" />
    </div>
    <div id="uploadDiv" visible="false" runat="server">
        附件上传：<asp:FileUpload ID="FileUpload1" runat="server" />
    </div>
     <div id="attachmentDiv" visible="false" runat="server">
        附件：XX附件
              XXX附件
    </div>
    <div >
        <img src="../TempImg/委托书录入_1.jpg" /><img src="../TempImg/委托书录入_2.jpg" />
    </div>
    <asp:HiddenField ID="hidContractId" runat="server" />
    <asp:HiddenField ID="hidIsNew" runat="server" />
    </form>
</body>
</html>
