<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_MonitoringPlan_PlanAttend" Codebehind="PlanAttend.aspx.cs" %>


<%@ Register TagName="MonthCalendar" TagPrefix="UC"  Src="~/Channels/Mis/Calendar/MonthCalendar.ascx"%>

<%@ Register TagName="WeekCalendar" TagPrefix="UC"  Src="~/Channels/Mis/Calendar/WeekCalendar.ascx" %>
<%@ Register TagName="DayCalendar" TagPrefix="UC"  Src="~/Channels/Mis/Calendar/DayCalendar.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../CSS/table/tableStyle.css" rel="stylesheet" type="text/css" />
            <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
     <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />

     <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
     <script src="../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
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
         <script src="../Calendar/MonthCalendar.js" type="text/javascript"></script>
                  <script src="../Calendar/WeekCalendar.js" type="text/javascript"></script>
                  <script src="../Calendar/DayCalendar.js" type="text/javascript"></script>
                  <script src="PlanCalendar.js" type="text/javascript"></script>
                  
</head>
<body>
    <form id="form1" runat="server">
        <div style=" padding:0 10px;">
            <table width="100%">
                <tr align="center" style="height:50px; ">
                    <td align="left" style=" width:60px;"><label id="lbdate">转到日期:</label></td>
                    <td align="left" style=" width:135px;" class="l-table-edit-td"><input type="text"  id="txtDate"  class="l-text l-text-editing" /></td>
                    <td align="left" class="l-table-edit-td"><input type="button"  id="btnToday" value="今天" class="l-button l-button-submit"/></td>
                    <td colspan="4"  style="width:400px;font-weight:bold;font-size:15px"></td>
                    <td style="width:50px"><input type="button"  id="btnDay" value="日" class="l-button l-button-submit"/></td>
                    <td style="width:50px"><input type="button"  id="btnWeek" value="周" class="l-button l-button-submit"/></td>
                    <td style="width:50px"><input type="button"  id="btnMonth" value="月" class="l-button l-button-submit"/></td>
                </tr>
            </table>
        </div>
         <div style=" padding:0 10px;">
        <div id="MonthDiv" style="display:none;" >
            <UC:MonthCalendar runat="server" ID="MonthCalendar" />
        </div>
        <div id="WeekDiv" style="display:none;">
            <UC:WeekCalendar runat="server" ID="WeekCalendar" />
        </div>
        <div id="DayDiv" style="display:none; width:99%;">
            <UC:DayCalendar runat="server" ID="DayCalendar" />
        </div>
        </div>
    </form>
</body>
</html>
