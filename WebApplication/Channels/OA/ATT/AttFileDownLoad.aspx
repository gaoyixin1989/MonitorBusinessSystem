<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_OA_ATT_AttFileDownLoad" Codebehind="AttFileDownLoad.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="l-form">
        <div class="l-group l-group-hasicon">
            <img src="../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />
            <span>附件信息 <font color="red">注：点击附件名称链接下载附件</font>
        </div>
    </div>
    <table width="450px" style="padding-top: -200px">
        <tr>
            <td align="right" width="100px">
                附件名称：
            </td>
            <td align="left" width="350px">
                <asp:LinkButton ID="btnFileName" runat="server" OnClick="btnFileName_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td align="right">
                上传日期：
            </td>
            <td align="left">
                <asp:Label ID="lblUploadDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                上传人：
            </td>
            <td align="left">
                <asp:Label ID="lblUploadPerson" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                附件说明：
            </td>
            <td align="left">
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidden" runat="server" />
    </form>
</body>
</html>
