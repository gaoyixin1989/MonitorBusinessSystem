﻿<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Mis_Monitor_sampling_SamplingTaskAllocation_QCStepAudit" Codebehind="SamplingTaskAllocation_QCStepAudit.aspx.cs" %>

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
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
    <script src="../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="SamplingTaskAllocation_QCStepAudit.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var gridHeight = $(window).height() / 2 - 20;

            $("#layout1").ligerLayout({ height: gridHeight, topHeight: 120, width: '100%' });
            $("#layout2").ligerLayout({ leftWidth: "50%", rightWidth: "50%", width: '100%', height: gridHeight, allowLeftCollapse: false, allowRightCollapse: false, bottomHeight: 30 });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width: 99%">
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
        <input type="button" value="发送" id="btn_Ok" name="btn_Ok" class="l-button l-button-submit" style="display: inline-block; margin-top: 8px;" onclick="SendClick();" />
        <input type="button" value="退回" id="btnBack" name="btnBack" class="l-button l-button-submit" style="display: inline-block; margin-top: 8px;" onclick="BackClick();" />
       <%-- <input type="button" value="质控通知单" id="btnPrint" name="btnPrint" class="l-button l-button-submit"
            style="display: inline-block; margin-top: 8px;" onclick="Export();" />--%>
        <input type="hidden" id="hidTaskId" runat="server" />
        <input type="hidden" id="hidPlanId" runat="server" />
        <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" Style="display: none;" />
    </div>
    </div>
    </form>
</body>
</html>