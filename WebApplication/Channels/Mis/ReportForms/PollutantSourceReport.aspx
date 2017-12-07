<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_ReportForms_PollutantSourceReport" Codebehind="PollutantSourceReport.aspx.cs" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/style.css" rel="stylesheet" type="text/css" />
        <link href="../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
     <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
     <!--Jquery 表单验证-->
     <script src="../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <!--自动完成-->
    <script src="../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
        <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
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
    <script src="PollutantSourceReport.js" type="text/javascript"></script>
    <title></title>
                <style type="text/css">
    #menu1,.l-menu-shadow{top:10px; left:20px;}
    #menu1{  width:200px;}
    </style>
</head>
<body >
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    </div>
<div id="SrhForm">
<table>
<tr>
<th>年度:</th>
<td><input  type="text" id="SEA_YEAR" class="l-table-edit-td" style="width:200px;" /></td>
<th>委托类别:</th>
<td><input  type="text" id="SEA_CONTRACT_TYPE" class="l-table-edit-td"  style="width:200px;"  /></td>
<th>监测类型:</th>
<td><input  type="text" id="SEA_MONITORTYPE" class="l-table-edit-td"  style="width:200px;"  /></td>
</tr>
<tr>
<th>监测点位:</th>
<td><input  type="text" id="SEA_POINTNAME" class="l-table-edit-td"  style="width:200px;" /></td>
<th>污染源:</th>
<td><input  type="text" id="SEA_PSOURCE" class="l-table-edit-td"  style="width:200px;" /></td>
<th>污染源企业:</th>
<td><input  type="text" id="SEA_COMPANY" class="l-table-edit-td"  style="width:200px;" /></td>
<td><input type="button" value="检 索" id="btnSubmit" runat="server" class="l-button l-button-submit" /> </td>
</tr>
</table>
</div>
<div id="divreport" >
    <rsweb:ReportViewer ID="reportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt" BackColor="#EAF2FE" ShowFindControls="False"  >
        <LocalReport ReportPath="Channels\Mis\ReportForms\ReportTemple\PollutantSourceReportData.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>
</div>
    </form>
</body>
</html>
