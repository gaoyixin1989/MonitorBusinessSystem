<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Masters/SearchEx.master" Inherits="Sys_WF_WFSettingFlowListForJS" Codebehind="WFSettingFlowListForJS.aspx.cs" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>

    <script src="../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>

    <script src="../../Scripts/comm.js" type="text/javascript"></script>
    <script src="WFSettingFlowList.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <div id="objGrid"></div>
</asp:Content>