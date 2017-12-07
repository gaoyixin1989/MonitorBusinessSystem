<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_ATT_AttFileUpload" Codebehind="AttFileUpload.aspx.cs" %>

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
        function upLoadFile() {
            $("#btnUpload").click();
        }
        function getUpLoadStatus() {
            return $("#status").val();
        }
        function isDeletAttFile() {
            $.ligerDialog.confirm('确定删除附件信息吗？', function (type) {
                if (type == true)
                    $("#btnFileClear").click();
            });
            return false;
        }
        function getFileName(path) {
            var pos1 = path.lastIndexOf('/');
            var pos2 = path.lastIndexOf('\\');
            var pos = Math.max(pos1, pos2)
            var strFilename = "";
            if (pos < 0)
                strFilename= path;
            else
                strFilename = path.substring(pos + 1);
            if (strFilename.length > 0) {
                strFilename = strFilename.substring(0, strFilename.lastIndexOf('.') );
            }

            return strFilename;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="l-form">
        <div class="l-group l-group-hasicon">
            <img src="../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />
            <span>附件信息</span>
        </div>
    </div>
    <table width="450px" style="padding-top: -200px">
        <tr>
            <td align="right" width="100px">
                附件名称：
            </td>
            <td align="left" width="350px">
                <asp:TextBox ID="ATTACH_NAME" runat="server" Width="200px"></asp:TextBox>
                <asp:Button ID="btnClear" runat="server" Text="清除附件" Width="70px" OnClientClick="javascript:return isDeletAttFile()" />
            </td>
        </tr>
        <tr>
            <td align="right">
                上传附件：
            </td>
            <td align="left">
                <asp:FileUpload ID="fileUpload" onchange="$('#ATTACH_NAME').val(getFileName(this.value))" runat="server" Width="350px" />
            </td>
        </tr>
        <tr>
            <td align="right">
                附件说明：
            </td>
            <td align="left">
                <asp:TextBox ID="DESCRIPTION" runat="server" Rows="5" TextMode="MultiLine" Width="350px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input type="hidden" id="fileType" runat="server" />
    <input type="hidden" id="fileId" runat="server" />
    <div style="display: none">
        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Width="0" Height="0" />
        <asp:Button ID="btnFileClear" runat="server" OnClick="btnFileClear_Click" Width="0"
            Height="0" />
        <asp:HiddenField ID="status" runat="server" />
    </div>
    </form>
</body>
</html>
