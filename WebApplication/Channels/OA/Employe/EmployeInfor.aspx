<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_Employe_EmployeInfor" Codebehind="EmployeInfor.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
            <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
     <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/welcome.css" rel="stylesheet" type="text/css" />
     <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
     <script src="../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
        <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
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
        <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
      <script src="EmployeInfor.js" type="text/javascript"></script>
    <title></title>
</head>
<body  style="padding: 0px; overflow: hidden; width: 100%; height: 100%;">
<form id="form1"  runat="server">
<div class="l-loading" style="display:block" id="pageloading"></div>
<div id="layout1">
    <div position="top">
        <div id="divEmployeInfo" ></div>
    </div>
    <div position="center" title="档案信息明细">
        <div id="navtab1" style="width:100%;overflow:hidden; border:1px solid #A3C0E8; ">
        <div tabid="home" title="证书信息" lselected="true"  >
            <div id="maingrid1"  ></div>
        </div>
        <div  title="工作经历"   >
            <div id="maingrid2"></div>
        </div>
        <div  title="业绩成果"   >
            <div id="maingrid3" ></div>
        </div>
        <div  title="质量事故"  >
            <div id="maingrid4" ></div>
        </div>
        <div  title="年度考核"  >
            <div id="maingrid5" ></div>
        </div>
        <div  title="培训履历"  >
            <div id="maingrid6" ></div>
        </div>
    </div>
</div>
</div>
  <div id="detailSrh" style="display:none;">
        <div id="SrhForm"></div>
    </div>
        <input type="hidden" clientidmode="Static" id="hdSrh" runat="server" />
        <asp:Button ID="btnImport" ClientIDMode="Static" runat="server" OnClick="btnImport_Click" Style="display: none;" />
</form>
</body>
</html>
