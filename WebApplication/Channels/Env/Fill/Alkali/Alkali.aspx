<%@ Page Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Channels_Env_Fill_Alkali_Alkali" Codebehind="Alkali.aspx.cs" %>

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
    <script src="../../Scripts/json2.js" type="text/javascript"></script>
    <script src="../../Scripts/ajaxForm.js" type="text/javascript"></script>
    <script src="Alkali.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#layout1").ligerLayout({ height: "100%" });
        });
//        // Excel导出方法
//        function Export() {
//            document.getElementById("btnExcelOut").click();
//        } 
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <div id="Seachdetail" style="display: none;">
        <div id="Seachdiv">
        </div>
    </div>
    <div id="layout1">
        <div position="center">
            <div id="grid">
            </div>
        </div>
    </div>
    <%--表单开始--%>
    <div id="searchDiv" style="display: none;">
        <form id="searchForm" method="post">
        </form>
    </div>
    <%--表单结束--%>
    <%--数据导入表单开始--%>
    <div id="dataImportDiv" style="display: none">
        <form id="dataImportForm" method="post" enctype="multipart/form-data">
        <div style="margin: 10px">
            <div style="height: 35px; margin-left: 24px">
                <div style="float: left">
                    规则：
                </div>
                <div style="float: left">
                    <select id="ddlImportConfig" name="ddlImportConfig">
                    </select>
                </div>
            </div>
            <div style="height: 35px;">
                数据文件：
                <input type="file" id="importFile" name="importFile" style="width: 300px" />
            </div>
        </div>
        <input type="hidden" id="action" name="action" value="DataImport" />
        </form>
    </div>
    <%--数据导入表单结束--%>
    <%--数据导出表单开始--%>
    <div id="dataExportDiv" style="display: none">
        <form id="dataExportForm" method="post" enctype="multipart/form-data" runat =server>
        <div style="margin: 10px">
            <div style="height: 35px; margin-left: 24px">
                <div style="float: left">
                    规则：
                </div>
                <div style="float: left">
                    <select id="ddlExportConfig" name="ddlExportConfig">
                    </select>
                </div>
            </div>
        </div>
        <input type="hidden" id="action" name="action" value="DataExport" />
        <input type="hidden" id="hidMonthToExport" name="hidMonthToExport" />
        <input type="hidden" id="hidYearToExport" name="hidYearToExport" />
<%--         <asp:Button ID="btnExcelOut" runat="server" OnClick="btnExcelOut_Click" Style="display: none" />--%>
        </form>
    </div>
    <%--数据导出表单结束--%>
    <%--流程表单开始--%>
    <div id="flowDiv" style="display: none">
        <div id="flowForm">
        </div>
    </div>
    <%--流程表单结束--%>
</asp:Content>

