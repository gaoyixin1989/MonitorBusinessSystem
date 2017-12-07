<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_sampling_DefaultUserExaspx2"
    CodeBehind="DefaultUserEx2.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
        var url = "DefaultUserEx.aspx";
        $(document).ready(function () {
            //创建表单结构 
            $("#txtDefaultUser").ligerComboBox({
                width: 200,
                isShowCheckBox: true,
                isMultiSelect: true,
                valueFieldID: 'txtDefaultUserId',
                lable: '协同人',
                split: ',',
                textField: 'REAL_NAME',
                valueField: 'USERID',
                url: url + "?type=getUserInfo&strSubTaskId=" + $("#strSubTaskId").val() + "&strMonitorType=" + $("#strMonitorType").val() + "&strItemId=" + $("#strItemId").val() + "&strResultStatus=" + $("#strResultStatus").val()
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
            <td align="center">
                <input type="text" id="txtDefaultUser" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="Status" runat="server" />
    <input type="hidden" id="strTaskId" runat="server" />
    <input type="hidden" id="strSubTaskId" runat="server" />
    <input type="hidden" id="strMonitorType" runat="server" />
    <input type="hidden" id="strItemId" runat="server" />
    <input type="hidden" id="strResultStatus" runat="server" />
    <input type="hidden" id="strSampleIds" runat="server" />
    </form>
</body>
</html>
