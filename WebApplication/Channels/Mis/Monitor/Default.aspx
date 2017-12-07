<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="项目名称："></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" Width="330px"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="是否分段验收："></asp:Label>
        <asp:CheckBox ID="CheckBox1" runat="server" Text="是" />
        <asp:CheckBox ID="CheckBox2" runat="server" Text="否" />
        <br />
        <asp:Label ID="Label3" runat="server" Text="分段验收经过审批："></asp:Label>
        <asp:CheckBox ID="CheckBox3" runat="server" Text="是" />
        <asp:CheckBox ID="CheckBox4" runat="server" Text="否" />
        <br />
        本次验收生产负荷：
        <br />
        1、主要生产设计产量（批复要求）__________________
        <br />
        2、主要产品实际产量______________________________
        <br />
        3、实际产量占设计产量的比例______________________
        <br />
        本次验收污染治理情况：
        <br />
        <asp:Label ID="Label4" runat="server" Text="废水处理设施："></asp:Label>
        <asp:CheckBox ID="CheckBox5" runat="server" Text="有" />
        <asp:CheckBox ID="CheckBox6" runat="server" Text="无" />
        <br />
        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Text="如有，是否运行正常："></asp:Label>
        <asp:CheckBox ID="CheckBox9" runat="server" Text="是" />
        <asp:CheckBox ID="CheckBox10" runat="server" Text="否" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label7" runat="server" Text="排污口是否规范："></asp:Label>
        <asp:CheckBox ID="CheckBox11" runat="server" Text="是" />
        <asp:CheckBox ID="CheckBox12" runat="server" Text="否" />
        <br />
        <asp:Label ID="Label8" runat="server" Text="废气处理设施："></asp:Label>
        <asp:CheckBox ID="CheckBox13" runat="server" Text="有" />
        <asp:CheckBox ID="CheckBox14" runat="server" Text="无" />
        <br />
        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label9" runat="server" Text="如有，是否运行正常："></asp:Label>
        <asp:CheckBox ID="CheckBox15" runat="server" Text="是" />
        <asp:CheckBox ID="CheckBox16" runat="server" Text="否" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label10" runat="server" Text="排污口是否规范："></asp:Label>
        <asp:CheckBox ID="CheckBox17" runat="server" Text="是" />
        <asp:CheckBox ID="CheckBox18" runat="server" Text="否" />
        <br />
        废气烟道共________条
        <br />
    </div>
    </form>
</body>
</html>
