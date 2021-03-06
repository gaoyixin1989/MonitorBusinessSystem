﻿<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_MonitoringPlan_ConvenTaskPlanList" Codebehind="ConvenTaskPlanList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
     <!--Jquery 基础文件-->
     <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
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
        <script src="../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
    <script type="text/javascript" src="ConvenTaskPlanList.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
 <div class="l-loading" style="display:block" id="pageloading"></div>
    <div id="navtab1" style="width: 99%; hidden; border:1px solid #A3C0E8; ">
        <div tabid="home" title="待预约任务"  lselected="true" contextmenu="false" >
            <div id="maingrid" style="margin:0px; "></div>
        </div>
        <div  title="待下达任务">
            <div id="maingrid1" style="margin:0px; "></div>
        </div>
                <div  title="已下达任务">
            <div id="maingrid2" style="margin:0px; "></div>
        </div>
    </div>
    <div id="detailSrh" style="display:none;">
        <div id="SrhForm"></div>
    </div>
    <div>
        <input type="hidden"  id="hidWorkTaskId" runat="server" />
        <input type="hidden"  id="hidTaskId" runat="server" />
        <input type="hidden"  id="hidPlanId" runat="server"  />
    <asp:Button ID="btnExport" runat="server" Text="" onclick="btnExport_Click" style="display:none;" />
    </div>
    </form>
</body>
</html>