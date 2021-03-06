﻿<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_Part_PartPlanAcceptDetail" Codebehind="PartPlanAcceptDetail.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
     <!--Jquery 基础文件-->
     <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>

     <!--自动完成-->
    <script src="../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--提示-->
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
     <!--LigerUI-->
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="PartPlanAcceptDetail.js" type="text/javascript"></script> 

            <style type="text/css">
        #menu1,.l-menu-shadow{top:10px; left:20px;}
        #menu1{  width:200px;}
    </style>
    <title></title>
</head>
<body style="padding:0px; overflow:hidden;">
<form id="form1" runat="server">
    <div class="l-loading" style="display:block" id="pageloading"></div>
    <div id="navtab1" style="width: 100%;overflow:hidden; border:1px solid #A3C0E8; ">
        <div tabid="home" title="待验收列表"  lselected="true" contextmenu="false" >
            <div id="maingrid" style="margin:0px; "></div>
        </div>
        <div  title="已验收列表">
            <div id="maingrid1" style="margin:0px; "></div>
        </div>
    </div>
    <div id="detailSrh" style="display:none;">
        <div id="SrhForm"></div>
    </div>
        <div id="detailSrh1" style="display:none;">
        <div id="SrhForm1"></div>
    </div>
        <div >
                <input type="hidden" id="hidGrid" runat="server" />
        <input type="hidden" id="hidExportDate" runat="server" />
       <asp:Button ID="btnExport" runat="server" Text="" onclick="btnExport_Click" />
</div>
    <asp:HiddenField ID="hidFwId" runat="server" />
    <asp:Button ID="btnExports" runat="server" OnClick="btnImport_Click" />
</form>
</body>
</html>
