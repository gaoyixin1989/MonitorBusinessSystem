<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_MonitoringPlan_PendingDoTask_Monitor" Codebehind="PendingDoTask_Monitor.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
     <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
          <link href="../../../Controls/ligerui/lab/lab.css" rel="stylesheet" type="text/css" />

     <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
        <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
            <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
    <script src="PendingDoTask_Monitor.js" type="text/javascript"></script>

     <style type="text/css"> 

            body{ padding:5px; margin:0; padding-bottom:15px;}
            #layout1{  width:100%;margin:0; padding:0;  }  
            h4{ margin:20px;}
                </style>

    <title></title>
</head>
<body>

<form  id="form1" runat="server" class="">
    <div id="layout1">

    <table>
        <tr>
            <td align="right" style="padding:3px;"  class="l-table-edit-td">&nbsp;&nbsp;&nbsp;&nbsp;项目名称: </td>
            <td align="left"  style="padding:3px "  class="l-table-edit-td" colspan="4">
                <input type="text" id="txtProjectName" name="txtProjectName" style="width:400px"  class="l-text l-text-editing"/>
            </td>
        </tr>
        <tr>
            <td align="right"  style="padding:3px"   class="l-table-edit-td">&nbsp;&nbsp;任务下达日期</td>
            <td align="left"  style="padding:3px"   class="l-table-edit-td">
                <input type="text" id="txtDate" name="txtDate"  class="l-text l-text-editing"/>
            </td>
            <td align="left"  style="padding:3px"   class="l-table-edit-td">
                要求完成时间</td>
            <td align="left"  style="padding:3px"   class="l-table-edit-td">
                <input type="text" id="txtFinishDate" name="txtFinishDate" class="l-text l-text-editing"/>
            </td>
        </tr>
        <%-- 潘德军 2013-12-23 任务单号可改，且初始不生成--%>
        <tr>
            <td align="right"  style="padding:3px"   class="l-table-edit-td">&nbsp;&nbsp;联系人</td>
            <td align="left"  style="padding:3px"   class="l-table-edit-td">  <input type="text" id="Contact_Name" name="txtDate"  class="l-text l-text-editing"/> </td>
            <td align="left"  style="padding:3px"   class="l-table-edit-td"> 联系电话</td>
            <td align="left"  style="padding:3px"   class="l-table-edit-td"><input type="text" id="PHONE" name="txtFinishDate"  class="l-text l-text-editing"/></td>
        </tr>
        <tr>
            <td align="right"  class="l-table-edit-td" style=" padding-top:5px">&nbsp;&nbsp;&nbsp;任务单号: </td>
            <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                <input type="text" id="txtTASK_CODE" name="txtTASK_CODE"  class="l-text l-text-editing" style="width:200px"/>
            </td>
                         <td><input id="BtnSearch" type="button" value="单号查询" style="width:80px;" /></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
<%--                            <tr id="trQcRow">
                    <td id="tdQcLab" align="right"  style="padding:3px;"   class="l-table-edit-td" >是否质控</td>
                    <td  align="left"  style="padding:3px;"   class="l-table-edit-td">
                     <input type="text" id="txtQcSet" name="txtQcSet"  class="l-text l-text-editing"/></td>
                    <td id="tdAllQcLab" align="left"  style="padding:3px;"   class="l-table-edit-td" >全程质控</td>
                    <td  align="left"  style="padding:3px;"   class="l-table-edit-td">
                     <input type="text" id="txtAllQcSet" name="txtAllQcSet"  class="l-text l-text-editing"/></td>
                </tr>--%>
    </table>

    </div>
    </form>
</body>
</html>
