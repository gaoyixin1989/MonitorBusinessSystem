<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_SamplePlan_SampleAskingDate" Codebehind="SampleAskingDate.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
        <script src="SampleAskingDate.js" type="text/javascript"></script>

            <title></title>
                 <style type="text/css"> 

            body{ padding:2px; margin:0;}
            #layout1{  width:100%;margin:0; padding:0;  }  
            h4{ margin:10px;}
                </style>
</head>
<body style="padding: 0px; overflow: hidden; width: 100%; height: 100%;">
    <div id="pageloading"></div>
    <div id="layout1"  style="width: 99%;overflow:hidden; border:0px solid #A3C0E8; ">
    <div position="top">
    <table>
        <tr>
            <td align="right"  class="l-table-edit-td" style=" padding-top:5px">&nbsp;&nbsp;&nbsp;要求完成日期: </td>
            <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                <input type="text" id="txtDate" name="txtDate"  class="l-text l-text-editing" style="width:200px"/>
            </td>
        </tr>
        <tr>  
            <td align="right"  style="padding:3px"   class="l-table-edit-td">&nbsp;&nbsp;联系人</td>
            <td align="left"  style="padding:3px"   class="l-table-edit-td">  <input type="text" id="Contact_Name" name="txtDate"  class="l-text l-text-editing"/> </td>
        </tr>
        <tr>
                 <td align="right"class="l-table-edit-td" style=" padding-top:5px">&nbsp;&nbsp;&nbsp;&nbsp; 联系电话</td>
            <td align="left"  style="padding:3px"   class="l-table-edit-td"><input type="text" id="PHONE" name="txtFinishDate"  class="l-text l-text-editing"/></td>
        </tr>
        <tr>
            <td align="right"  class="l-table-edit-td" style=" padding-top:5px">&nbsp;&nbsp;&nbsp;任务单号: </td>
            <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                <input type="text" id="txtTASK_CODE" name="txtTASK_CODE"  class="l-text l-text-editing" style="width:133px"/>   
            </td>
              <td><input id="BtnSearch" type="button" value="单号查询" style="width:80px;" /></td>
        </tr>
    </table>
    </div>
    <div>
        </div>
        </div>
</body>
</html>
