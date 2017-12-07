<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Channels_Env_ZZ_Fill_Rain_RainFill" Codebehind="RainFill.aspx.cs" %>

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
    <script src="../../Scripts/json2.js" type="text/javascript"></script>
    <script src="../../Scripts/ajaxForm.js" type="text/javascript"></script>
    <script src="../../Scripts/ImportExport.js" type="text/javascript"></script>
    <script src="RainFill.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#layout1").ligerLayout({ height: "100%" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" Runat="Server">
    <div id="layout1">
        <div id="grid">
        </div>
    </div>
    <%--查询表单开始--%>
    <div id="searchDiv" style="display: none">
        <form id="searchForm" method="post">
        </form>
    </div>
    <%--查询表单结束--%>
    <%--隐藏域开始--%>
    <input type="hidden" id="hidPointIdToSave" />
    <div id="dataExportDiv" style="display: none">
        <form id="dataExportForm" method="post" enctype="multipart/form-data" runat =server>
        </form>
    </div>
    <%--隐藏域结束--%>
</asp:Content>

