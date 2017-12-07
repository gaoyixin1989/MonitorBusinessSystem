<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Mis_Monitor_Result_QHD_AnalysisResultDataTimeSetting" Codebehind="AnalysisResultDataTimeSetting.aspx.cs" %>

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
        var url = "AnalysisResultDataTimeSetting.aspx";
        $(document).ready(function () {

            $("#AnalysisResultDataTime").ligerDateEditor({ showTime: true });

            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: url + "?type=getAnalysisResultDataTime&strResultId=" + $("#strResultId").val() + "&timeType=" + $("#timeType").val(),
                contentType: "application/json; charset=utf-8",
                dataType: "text",
                success: function (data, textStatus) {
                    $("#AnalysisResultDataTime").val(data);
                }
            });

        });
        function UserSave() {
            $("#Status").val('save');
            var isSuccess = DataSave();
            if (isSuccess == "1") return true;
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td align="right">
                仪器使用时间：
            </td>
            <td align="left">
                <input type="text" id="AnalysisResultDataTime" name="AnalysisResultDataTime" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="Status" runat="server" />
    <input type="hidden" id="timeType" runat="server" />
    <input type="hidden" id="strResultId" runat="server" />
    </form>
</body>
</html>
