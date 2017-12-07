<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_Result_QHD_QcSetting_QHD" Codebehind="QcSetting_QHD.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
        var url = "QcSetting_QHD.aspx";
        $(document).ready(function () {

        });
        function saveQcValue() {
            $("#status").val("save");
            var isuccess = "0";
            ajaxSubmit(url, function () { return true; }, function (responseText, statusText) {
                isuccess = responseText;
            });
            return isuccess;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="l-form">
        <div class="l-group l-group-hasicon">
            <img src="../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />实验室质控设置
        </div>
        <table width="100%">
            <tr>
                <td align="left">
                    <input type="checkbox" name="chkQcEmpty" />
                    实验室空白
                </td>
                <td align="left">
                    <input type="checkbox" name="chkQcSt" />标准样
                </td>
            </tr>
            <tr>
                <td align="left">
                    <input type="checkbox" name="chkQcAdd" />实验室加标
                </td>
                <td align="left">
                    <input type="checkbox" name="chkQcTwin" />实验室明码平行
                </td>
            </tr>
        </table>
        <div id="divQcEmpty">
        </div>
    </div>
    <input type="hidden" id="resultid" runat="server" />
    <input type="hidden" id="result" runat="server" />
    <input type="hidden" id="emptyBatId" runat="server" />
    <input type="hidden" id="emptycount" runat="server" />
    <input type="hidden" id="status" runat="server" />
    </form>
</body>
</html>
