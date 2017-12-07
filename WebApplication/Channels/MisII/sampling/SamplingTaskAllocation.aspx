<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_MisII_sampling_SamplingTaskAllocation"
    CodeBehind="SamplingTaskAllocation.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>采样任务分配</title>
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
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="SamplingTaskAllocation.js" type="text/javascript"></script>
    <script type="text/javascript">



        $(function () {
            var gridHeight = $(window).height() / 2 - 20;

            $("#layout1").ligerLayout({ height: 70, width: '100%' });
            $("#layout2").ligerLayout({ leftWidth: "50%", rightWidth: "50%", width: '100%', height: gridHeight, allowLeftCollapse: false, allowRightCollapse: false, bottomHeight: 30 });
        });
    </script>
    <script src="SamplingTaskAllocation.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 99%">
    <input id="PLAN_ID" runat="server" type="hidden" />
    <input id="SUBTASK_ID" runat="server" type="hidden" />
    <div id="layout1">
        <div title="任务信息">
            <div id="oneFrom">
            </div>
        </div>
    </div>
    <div id="createDiv" style="font-size: small; margin-left: 35px;">
    </div>
    <div id="twoGrid">
        <%--        <div position="center">--%>
        <%--        </div>--%>
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
    <div id="divSugg" style="display: none;">
        <div id="SuggForm">
        </div>
    </div>
    <div style="text-align: center; width: 100%">
        <input type="button" value="任务单" id="btnPrint" name="btnPrint" class="l-button l-button-submit"
            style="display: inline-block; margin-top: 8px;" onclick="Export();" />
        <asp:Button ID="btnImport" runat="server" Style="display: none; margin-top: 8px;"
            OnClick="btnImport_Click" />
        <%--         <asp:HiddenField ID="strSAMPLE_ASK_DATE" runat="server" />
        <asp:HiddenField ID="strSAMPLE_FINISH_DATE" runat="server" />--%>
        <%--        <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" Style="display: none;" />
        --%>
        <asp:Button ID="btn_Ok" runat="server" Text="打 印" CssClass="l-button l-button-submit"
            OnClick="btn_Ok_Click" Height="23px" Width="61px" Style="display: none;" />
    </div>
    </form>
</body>
</html>
