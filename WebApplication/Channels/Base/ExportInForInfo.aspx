<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Base_ExportInForInfo" Codebehind="ExportInForInfo.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="divItem">
            <table>
                <tr>
                    <td>
                        选择导入的信息：
                        <asp:RadioButtonList ID="SelectExportInfo" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="仪器信息" Value="0"></asp:ListItem>
                            <asp:ListItem Text="企业点位信息" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            请选择导入的Excel文件：
            <asp:TextBox ID="UploadFileName" runat="server" Visible="false"></asp:TextBox>
            <asp:FileUpload ID="FileUpload1" runat="server" Width="50%" CssClass="ac_loading">
            </asp:FileUpload>
            <asp:Button ID="Btn_UpLoadFile_COD" runat="server" Text="确定" CssClass="btnDefault"
                Style="height: 25px" OnClick="Btn_UpLoadFile_COD_Click" />
        </div>
    </div>
    </form>
</body>
</html>
