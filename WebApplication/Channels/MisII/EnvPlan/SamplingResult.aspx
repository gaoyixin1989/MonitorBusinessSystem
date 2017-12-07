<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_MisII_sampling_SamplingResult" Codebehind="SamplingResult.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>现场项目结果录入</title>
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
    <script src="../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="SamplingResult.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            var gridHeight = $(window).height() / 2 - 20;

            $("#layout1").ligerLayout({ height: gridHeight, topHeight: 120, width: '100%' });
            $("#layout2").ligerLayout({ leftWidth: "50%", rightWidth: "50%", width: '100%', height: gridHeight, allowLeftCollapse: false, allowRightCollapse: false, bottomHeight: 30 });
        });
    </script>
    
</head>
<body >
<form id="form1" runat="server"  style="width:99%">
    <input id="PLAN_ID" runat="server" type="hidden" />
    <input id="SUBTASK_ID" runat="server" type="hidden" />
    <div id="layout1">
        <div position="top" title="任务信息">
            <div id="oneFrom">
            </div>
        </div>
        <div position="center">
            <div id="twoGrid">
            </div>
        </div>
    </div>
    <div id="layout2">
        <div position="left" title="监测点位">
            <div id="threeGrid">
            </div>
        </div>
        <div position="right" title="监测项目">
            <div id="fourGrid">
            </div>
        </div>
    </div>
    <div style="text-align: center; width: 100%">
        
        <asp:HiddenField ID="strSAMPLE_ASK_DATE" runat="server" />
        <asp:HiddenField ID="strSAMPLE_FINISH_DATE" runat="server" />
        
    </div>
    </form>
</body>
</html>
