<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Rpt_Sign_SignatureEdit" Codebehind="SignatureEdit.aspx.cs" %>

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
    <script src="../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script src="../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        function upLoadFile() {
            $("#btnUpload").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divEdit">
        <div class="l-form">
            <div class="l-group l-group-hasicon">
                <img src="../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />
                <span>印章信息</span>
            </div>
        </div>
        <table width="450px" style="padding-top: -200px">
            <tr>
                <td align="right" width="100px">
                    印章名：
                </td>
                <td align="left" width="350px">
                    <asp:TextBox ID="MARK_NAME" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    上传附件：
                </td>
                <td align="left">
                    <asp:FileUpload ID="fileUpload" runat="server" Width="300px" />
                </td>
            </tr>
        </table>
        <input type="hidden" id="fileType" runat="server" />
        <input type="hidden" id="fileId" runat="server" />
        <div style="display: none">
            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Width="0" Height="0" />
        </div>
    </div>
    </form>
</body>
</html>
