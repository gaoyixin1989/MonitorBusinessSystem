<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Channels_Env_Fill_NoiseRoad_RoadNoiseFill" Codebehind="RoadNoiseFill.aspx.cs" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
    <%--<script src="../../Scripts/json2.js" type="text/javascript"></script>
    <script src="../../Scripts/ajaxForm.js" type="text/javascript"></script>
    <script src="../../Scripts/ImportExport.js" type="text/javascript"></script>--%>
    <script src="RoadNoiseFill.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#layout1").ligerLayout({ height: "100%", topHeight: "18%", bottomHeight: "82%" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <div id="layout1">
        <div position="top">
            <div id="gridSummary">
            </div>
        </div>
        <div position="bottom">
            <div id="gridFill">
            </div>
        </div>
    </div>
    <%--查询表单开始--%>
    <div id="searchDiv" style="display: none;">
        <div id="searchForm">
        </div>
    </div>
    <%--查询表单结束--%>
    <%--导出月度选择开始--%>
    <div id="ExportMonthDiv" style="display: none;">
        <div id="monthForm">
        </div>
    </div>
    <%--导出月度选择结束--%>
    <%--流程表单开始--%>
    <div id="flowDiv" style="display: none">
        <div id="flowForm">
        </div>
    </div>
    <%--流程表单结束--%>
</asp:Content>
