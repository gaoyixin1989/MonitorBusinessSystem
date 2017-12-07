<%@ Page Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Channels_MisII_Monitor_Result_AnalysisTaskAllocation" Codebehind="AnalysisTaskAllocation.aspx.cs" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script src="../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="AnalysisTaskAllocation.js" type="text/javascript"></script>
    <script type="text/javascript">
        var topHeight, bottomHeight;

        $(function () {
            $("#layout1").ligerLayout({ leftWidth: 450, rightWidth: '50%', allowLeftCollapse: false, allowRightCollapse: false, height: '100%' });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <div id="layout1">
        <div position="left" title="样品信息列表">
            <div id="SampleGrid">
            </div>
        </div>
        <div position="center" title="监测项目信息">
            <div id="ItemGrid">
            </div>
        </div>
    </div>
    <input id="SUBTASK_ID" runat="server" type="hidden" />
    <input id="TASK_ID" runat="server" type="hidden" />
    <input id="PLAN_ID" runat="server" type="hidden" />
</asp:Content>
