﻿<%@ Page Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Channels_Base_Search_ApparatusSearch" Codebehind="ApparatusSearch.aspx.cs" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="ApparatusSearch.js" type="text/javascript"></script>
    <style>
        .l-panel-bbar-inner
        {
            min-width: 500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <div id="layout1">
        <div position="top" title="仪器设备">
            <div id="firstgrid"></div>
        </div>
        <div position="left" title="仪器资料">
            <div id="secondgrid"></div>
        </div>
        <div position="right" title="检定证书">
            <div id="thirdgrid"></div>
        </div>
    </div>
    <div id="detail" style="display: none;">
        <form id="editFirstForm" method="post">
        </form>
    </div>
    <div id="detailSrh" style="display: none;">
        <form id="searchFirstForm" method="post">
        </form>
    </div>
</asp:Content>
