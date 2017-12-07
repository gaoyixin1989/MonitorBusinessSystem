<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Env_Fill_River_Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
</head>
<body>
    <form id="form1" runat="server">
    河流水导入导出：
    <asp:FileUpload ID="fileUpload" runat="server" />
    <asp:Button ID="btn" runat="server" Text="导入" OnClick="btn_Click" />
    <asp:Button ID="btnExport" runat="server" Text="导出" OnClick="btnExport_Click" />
    <br />
    功能区噪声导入导出:
    <asp:FileUpload ID="fileUpload1" runat="server" />
    <asp:Button ID="btn1" runat="server" Text="导入" OnClick="btn1_Click" />
    <asp:Button ID="btnExport1" runat="server" Text="导出" OnClick="btnExport1_Click" />
    </form>
</body>
</html>