﻿<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_MonitoringPlan_PlanAdd_QY" Codebehind="PlanAdd_QY.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

    <script src="../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="PlanAdd_QY.js" type="text/javascript"></script>

     <style type="text/css"> 

            body{ padding:5px; margin:0; padding-bottom:15px;}
            #layout1{  width:100%;margin:0; padding:0;  }  
            h4{ margin:20px;}
                </style>

    <title></title>
</head>
<body>
    <div class="l-loading" style="display:block" id="pageloading"></div>
    <div id="layout1">
    <div position="top">
    <table>
    <tr>
                    <td align="right"  class="l-table-edit-td">&nbsp;&nbsp;委托类别: </td>
                    <td align="left"  class="l-table-edit-td">
                        <input type="text" id="txtContractType" name="txtContractType"  class="l-text l-text-editing"/>
                    </td>
                    <td align="right"  class="l-table-edit-td">&nbsp;&nbsp;项目名称: </td>
                    <td align="left"  class="l-table-edit-td">
                        <input type="text" id="txtProjectName" name="txtProjectName"  class="l-text l-text-editing"/>
                    </td>
                     <td align="right"  class="l-table-edit-td">&nbsp;&nbsp;任务下达日期: </td>
                    <td align="left"  class="l-table-edit-td">
                        <input type="text" id="txtDate" name="txtDate"  class="l-text l-text-editing"/>
                    </td>
                    <td align="right"  class="l-table-edit-td">&nbsp;&nbsp;要求完成日期: </td>
                    <td align="left"  class="l-table-edit-td">
                        <input type="text" id="txtAskingDate" name="txtAskingDate"  class="l-text l-text-editing"/>
                    </td>
                </tr>
    </table>
    </div>
    <div position="left" title="待预约排口列表">
    <div id="gridItems"></div>
    </div>
    <div position="right" title="选择项目负责人">
    <div>
            <table id="tabCombox"></table>
    </div>
    </div>
    </div>
    <div style="text-align: center; width: 100%">
        <input type="button" value="发 送" id="btnSave" name="btnSave" class="l-button l-button-submit" style="display:inline-block; margin-top:5px;"/>
    </div>
            <div>
            <input type="hidden"  id="hidContractID" />
            <input type="hidden"  id="hidCompanyId" />
        </div>
</body>
</html>