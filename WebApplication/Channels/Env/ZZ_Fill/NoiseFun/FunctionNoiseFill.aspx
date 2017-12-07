<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Channels_Env_ZZ_Fill_NoiseFun_FunctionNoiseFill" Codebehind="FunctionNoiseFill.aspx.cs" %>

<asp:Content ID="head" ContentPlaceHolderID="head" Runat="Server">
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
    <script src="FunctionNoiseFill.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#layout1").ligerLayout({ height: "100%", topHeight: "16%", bottomHeight: "84%" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" Runat="Server">
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
    <%--填报时用于设置日期的表单开始--%>
    <div id="fillSetDateDiv" style="display: none;">
        <div id="fillSetDateForm">
        </div>
    </div>
    <%--填报时用于设置日期的表单结束--%>

    <input type="hidden" id="hidMonth" value="1" />
</asp:Content>

