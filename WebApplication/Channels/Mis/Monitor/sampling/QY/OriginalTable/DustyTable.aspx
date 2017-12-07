﻿<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Monitor_sampling_QY_OriginalTable_DustyTable" Codebehind="DustyTable.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
     <!--Jquery 基础文件-->
     <script src="../../../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>

     <!--自动完成-->
    <script src="../../../../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--提示-->
    <script src="../../../../../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script> 
    <script src="../../../../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
     <!--LigerUI-->
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
                 <script src="../../../../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script> 
    <script src="../../../../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
    <script src="DustyTable.js" type="text/javascript"></script>

     <style type="text/css"> 

            body{ padding:5px; margin:0; padding-bottom:15px;}
            #layout1{  width:100%;margin:0; padding:0;  }  
            h4{ margin:20px;}
            
                </style>

    <title></title>
</head>
<body>
    <div style="display:block" id="pageloading"></div>
    <div id="layout1">
    <div position="top">
    <table>
    <tr >
    <th style=" padding-top:5px;">方法依据:</th>
<td align="left"  class="l-table-edit-td" style=" padding-top:5px">
<input type="text" id="txtMethold" name="txtMethold"  class="l-text l-text-editing" style="width:200px"/>
</td>
<th style=" padding-top:5px;">监测目的:</th>
<td align="left"  class="l-table-edit-td" style=" padding-top:5px">
<input type="text" id="txtPurPress" name="txtPurPress"  class="l-text l-text-editing" style="width:200px"/>
</td>
<th style=" padding-top:5px;">采样日期:</th>
<td align="left"  class="l-table-edit-td" style=" padding-top:5px" colspan="3">
<input type="text" id="txtSampleDate" name="txtSampleDate"  class="l-text l-text-editing" style="width:200px"/>
</td>
</tr>
        <tr >
    <th style=" padding-top:5px;">单位名称:</th>
<td align="left"  class="l-table-edit-td" style=" padding-top:5px">
<input type="text" id="txtCompany" name="txtCompany"  class="l-text l-text-editing" style="width:200px"/>
</td>
<th style=" padding-top:5px;">锅炉名称/蒸吨:</th>
<td align="left"  class="l-table-edit-td" style=" padding-top:5px">
<input type="text" id="txtBOILER_NAME" name="txtBOILER_NAME"  class="l-text l-text-editing" style="width:200px"/>
</td>
<th style=" padding-top:5px;">燃料种类:</th>
<td align="left"  class="l-table-edit-td" style=" padding-top:5px" colspan="3">
<input type="text" id="txtFUEL_TYPE" name="txtFUEL_TYPE"  class="l-text l-text-editing" style="width:200px"/>
</td>
</tr>
<tr>
        <th style=" padding-top:5px;">烟囱高度:</th>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        <input type="text" id="txtHEIGHT" name="txtHEIGHT"  class="l-text l-text-editing" style="width:50px"/>m</td>
     <th style=" padding-top:5px;">采样位置:</th>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        <input type="text" id="txtPOSITION" name="txtPOSITION"  class="l-text l-text-editing" style="width:200px"/>
        </td>
             <th style=" padding-top:5px;">断面直径:</th>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        <input type="text" id="txtSECTION_DIAMETER" name="txtSECTION_DIAMETER"  class="l-text l-text-editing" style="width:50px"/> 
        m</td>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        断面面积:</td>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        <input type="text" id="txtSECTION_AREA" name="txtSECTION_AREA"  class="l-text l-text-editing" style="width:50px"/>( 
        ㎡)</td>
    </tr>
    <tr>
        <th style=" padding-top:5px;">治理设施:</th>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        <input type="text" id="txtGOVERM_METHOLD" name="txtGOVERM_METHOLD"  class="l-text l-text-editing" style="width:200px"/></td>
     <th style=" padding-top:5px;">风机风量:</th>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        <input type="text" id="txtMECHIE_WIND_MEASURE" name="txtMECHIE_WIND_MEASURE"  class="l-text l-text-editing" style="width:50px"/>m³/h</td>
             <th style=" padding-top:5px;">烟气含湿量:</th>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        <input type="text" id="txtHUMIDITY_MEASURE" name="txtHUMIDITY_MEASURE"  class="l-text l-text-editing" style="width:50px"/> 
        %</td>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        折算系数:</td>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        <input type="text" id="txtMODUL_NUM" name="txtMODUL_NUM"  class="l-text l-text-editing" style="width:50px"/></td>
    </tr>

        <tr>
        <th style=" padding-top:5px;">仪器型号:</th>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        <input type="text" id="txtMECHIE_MODEL" name="txtMECHIE_MODEL"  class="l-text l-text-editing" style="width:200px"/></td>
     <th style=" padding-top:5px;">仪器编号:</th>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        <input type="text" id="txtMECHIE_CODE" name="txtMECHIE_CODE"  class="l-text l-text-editing" style="width:200px"/></td>
             <th style=" padding-top:5px;">采样嘴直径:</th>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        <input type="text" id="txtSAMPLE_POSITION_DIAMETER" name="txtSAMPLE_POSITION_DIAMETER"  class="l-text l-text-editing" style="width:50px"/> 
        mm</td>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        环境温度:</td>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        <input type="text" id="txtENV_TEMPERATURE" name="txtENV_TEMPERATURE"  class="l-text l-text-editing" style="width:50px"/>℃</td>
        <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        大气压力:</td>
    <td align="left"  class="l-table-edit-td" style=" padding-top:5px">
        <input type="text" id="txtAIR_PRESSURE" name="txtAIR_PRESSURE"  class="l-text l-text-editing" style="width:50px" />KPa</td>
                         <td align="center"  class="l-table-edit-td" style=" padding-top:5px; width:100px">
                        <input type="button" id="btnOk" name="btnOk" value="生成原始记录" class="l-button l-button-submit" />
                    </td>
    </tr>
    </table>
    </div>
        <div position="left" title="点位列表">
        <div id="CreateDiv" ></div>
    </div>
    </div>
    <div>
    <input type="hidden" id="hidMECHIE_MODEL" name="hidMECHIE_MODEL" />
    </div>
</body>
</html>
