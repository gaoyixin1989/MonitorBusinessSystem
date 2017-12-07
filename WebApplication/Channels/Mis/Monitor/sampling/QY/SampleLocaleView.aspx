<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_sampling_QY_SampleLocaleView" Codebehind="SampleLocaleView.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
    <script src="../../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="../../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="SampleLocaleView.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#layout4").ligerLayout({ leftWidth: 300, allowLeftCollapse: false, height: 480 });
        });
        ///附件上传
        function upLoadFile() {
            $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
                buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        $("iframe")[0].contentWindow.upLoadFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../../OA/ATT/AttFileUpload.aspx?filetype=SubTask&id=' + $("#SUBTASK_ID").val()
        });
        return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input id="SUBTASK_ID" runat="server" type="hidden" />
    <div id="layout4">
    <div position="center" title="监测项目信息">
    <div>
        打印报表：<asp:LinkButton ID="LinkButton1" Text="《现场采样记录单》" runat="server" OnClientClick="javascript:return upLoadFile();"></asp:LinkButton>
        </div>
        <div id="oneFrom">
            </div>
            
        <div id="divWeather" runat="server">
            </div>
    </div>
    </div>
    </form>
</body>
</html>
