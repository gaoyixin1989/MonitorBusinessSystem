<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_SamplePlan_SamplePlanAdd" Codebehind="SamplePlanAdd.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
      <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
     <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
     <link href="../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script>
    <!--Jquery 表单验证-->
     <script src="../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <!--自动完成-->
    <script src="../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
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
               <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
                <script src="../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
                 <script src="../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>

    <script src="../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="SamplePlanAdd.js" type="text/javascript"></script>
    <title></title>
         <style type="text/css"> 

            body{ padding:5px; margin:0; padding-bottom:15px;}
            #layout1{  width:100%;margin:0; padding:0;  }  
            h4{ margin:20px;}
                </style>
</head>
<body style="padding: 6px; overflow: hidden; width: 100%; height: 100%;">
    <div class="l-loading"></div>
    <div id="layout1"  style="width: 99%;overflow:hidden; border:1px solid #A3C0E8; ">
    <div position="top">
    <table>
    <tr>
                    <td align="right"  class="l-table-edit-td" style=" padding-top:5px">&nbsp;&nbsp;&nbsp;&nbsp;委托方: </td>
                    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                        <input type="text" id="Company_Name" name="Company_Name"  class="l-text l-text-editing" style="width:200px"/>
                    </td>
                     <td align="right"  class="l-table-edit-td" style=" padding-top:5px">&nbsp;&nbsp;受检方: </td>
                    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                        <input type="text" id="Company_NameFrim" name="Company_NameFrim"  class="l-text l-text-editing" style="width:200px"/>
                    </td>
                </tr>
    <tr >
<td align="right"  class="l-table-edit-td" style=" padding-top:5px">&nbsp;&nbsp;&nbsp;&nbsp;委托年度: </td>
<td align="left"  class="l-table-edit-td" style=" padding-top:5px">
<input type="text" id="Contrat_Year" name="Contrat_Year"  class="l-text l-text-editing"/>
</td>
<td align="right"  class="l-table-edit-td" style=" padding-top:5px">&nbsp;&nbsp;委托类别: </td>
<td align="left"  class="l-table-edit-td" style=" padding-top:5px">
<input type="text" id="Contract_Type" name="Contract_Type"  class="l-text l-text-editing"/>
</td>
</tr>
    <tr  >
                         <td align="right"  class="l-table-edit-td" style=" padding-top:5px">&nbsp;&nbsp;监测类别: </td>
                    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                        <input type="text" id="txtType" name="txtType"  class="l-text l-text-editing"  />
                    </td>
                    <td align="right"  class="l-table-edit-td" style=" padding-top:5px" >&nbsp;&nbsp;&nbsp;&nbsp;项目名称: </td>
                    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                        <input type="text" id="txtProjectName" name="txtProjectName"  class="l-text l-text-editing" style="width:200px"/>
                    </td>
</tr>
                    <tr>
                     <td align="right"  class="l-table-edit-td" style=" padding-top:5px">&nbsp;&nbsp;要求完成日期: </td>
                    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                        <input type="text" id="txtDate" name="txtDate"  class="l-text l-text-editing"  />
                    </td>
                    <td align="right"  class="l-table-edit-td" style=" padding-top:5px">&nbsp;&nbsp;送样人: </td>
                    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
                        <input type="text" id="txtSendMan" name="txtSendMan"  class="l-text l-text-editing" style="width:200px" />
                    </td>
                     <td align="center"  class="l-table-edit-td" style=" padding-top:5px; width:100px">
                        <input type="button" id="btnOk" name="btnOk" value="确 定"  class="l-button l-button-submit" />
                    </td>
                </tr>
    </table>
    </div>
        <div position="left" title="样品列表">
        <div id="createDiv" class="l-messagebox-btn-inner" ></div>
    </div>
    <div>
            <input type="hidden"  id="hidContractID" />
            <input type="hidden"  id="hidCompanyId" />
        </div>
        </div>
</body>
</html>
