<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_Part_ZZ_WorkSubmit" Codebehind="WorkSubmit.aspx.cs" %>
<%@ Register TagPrefix="UC" TagName="WFControl" Src="~/Sys/WF/UCWFControls.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head  id="Head1" runat="server">
    <title></title>

    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"  rel="stylesheet" type="text/css" />
     <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
     <link href="../../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
                  <!--Jquery 基础文件-->
     <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>

     <!--自动完成-->
    <script src="../../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--提示-->
    <script src="../../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
         <!--LigerUI-->
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
            <!--货币格式-->
    <script src="../../../../Scripts/jquery.formatCurrency-1.4.0.js" type="text/javascript"></script>
    <script src="WorkSubmit.js" type="text/javascript"></script>

      <style type="text/css">   
    .l-minheight div{ height:90px; overflow:hidden;}
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <h1 class="h12013">工 作 呈 报 单</h1>
    <div class="tablelayout">
        <br/>
            <table  cellspacing="0" cellpadding="0" align="center" class="tableedit mlr8">
   <tr>
   <th>
    呈报科室:
  </th>
  <td align="left" >
  <input type="text"  id="PlanDept" name="PlanDept" class="l-text l-text-editing"   style=" width:200px"/>
  </td>
   <th>办结日期：</th>
   <td>
   <input type="text" id="ManageDate" name="ManageDate" class="l-text l-text-editing"   style=" width:200px;"/></td> 
   </tr>

    <tr>
    <th>
    物料列表：
    </th>
    <td align="left" colspan="3" style="height:150px;" >
    <div id="maingrid"  style="width:100%; height:100%;"></div>
    </td>
      </tr>
      <tr id="trTestPerson">
        <th>
        经办人：
        </th>
        <td align="left" >
        <input type="text"  id="AgentPerson" name="AgentPerson" class="l-text l-text-editing"   style=" width:200px"/>
        </td>
        <th>日期：</th>
        <td>
        <input type="text" id="AgentDate" name="AgentDate" class="l-text l-text-editing"   style=" width:200px;"/></td>
        </tr>


      <tr id="trTestOption">
<th >
科室意见：
</th>
<td align="left" colspan="8"  class="l-minheight">
<textarea cols="100" rows="5" class="l-textarea" id="TestOption" style="width:600px;  padding:2px; margin-left:0; line-height:20px;" ></textarea>
</td>
</tr>
<tr id="trTestPInfor">
<th>
室主任：
</th>
<td align="left" >
<input type="text"  id="ChiefPerson" name="ChiefPerson" class="l-text l-text-editing"   style=" width:200px"/>
</td>
<th>日期：</th>
<td>
<input type="text" id="ChiefDate" name="ChiefDate" class="l-text l-text-editing"   style=" width:200px;"/></td>
</tr>

<tr id="trTechOption">
<th style="width:100px" >
主管领导意见：
</th>
<td align="left" colspan="8"  class="l-minheight">
<textarea cols="100" rows="5" class="l-textarea" id="TechOption" style="width:600px;  padding:2px; margin-left:0; line-height:20px;" ></textarea>
</td>
</tr>
<tr id="trTechInfor">
<th>
签名：
</th>
<td align="left" >
<input type="text"  id="LeaderName" name="LeaderName" class="l-text l-text-editing"   style=" width:200px"/>
</td>
<th>日期：</th>
<td>
<input type="text" id="LeaderDate" name="LeaderDate" class="l-text l-text-editing"   style=" width:200px;"/></td>
</tr>

<tr id="trOffer">
<th style="width:100px" >
办公室意见：
</th>
<td align="left" colspan="8"  class="l-minheight">
<textarea cols="100" rows="5" class="l-textarea" id="Textarea1" style="width:600px;  padding:2px; margin-left:0; line-height:20px;" ></textarea>
</td>
</tr>
<tr id="trContext">
<th>
签名：
</th>
<td align="left" >
<input type="text"  id="OfferName" name="OfferName" class="l-text l-text-editing"   style=" width:200px"/> 
</td>
<th>日期：</th>
<td>
<input type="text" id="OfferDate" name="OfferDate" class="l-text l-text-editing"   style=" width:200px;"/></td>
</tr>

<tr id="trSerial">
<th style="width:100px" >
站长意见：
</th>
<td align="left" colspan="8"  class="l-minheight">
<textarea cols="100" rows="5" class="l-textarea" id="Textarea2" style="width:600px;  padding:2px; margin-left:0; line-height:20px;" ></textarea>
</td>
</tr>
<tr id="trSerialInfo">
<th>
签名：
</th>
<td align="left" >
<input type="text"  id="SerialName" name="SerialName" class="l-text l-text-editing"   style=" width:200px"/>
</td>
<th>日期：</th>
<td>
<input type="text" id="SerialDate" name="SerialDate" class="l-text l-text-editing"   style=" width:200px;"/></td>
</tr>
</table>
<div id="divContratSubmit" >
<UC:WFControl ID="wfControl" EnableViewState="true" runat="server" />
</div>
</div>

  <div>
<input type="hidden"  id="hidTaskId" runat="server" />
<input type="hidden"  id="hidTaskProjectName"  runat="server" /> 
<input type="hidden"  id="hidOptionContent" runat="server" />
<input type="hidden"  id="hidUserId" runat="server" />
<input type="hidden"  id="hidOptionDate" runat="server" />
<input type="hidden"  id="Task_Order" runat="server" />
</div>
    </form>
</body>
</html>
