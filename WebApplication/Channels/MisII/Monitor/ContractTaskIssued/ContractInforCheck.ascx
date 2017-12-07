<%@ Control Language="C#" AutoEventWireup="True" Inherits="n15.Channels_MisII_Contract_ContractInforCheck" Codebehind="ContractInforCheck.ascx.cs" %>
<style type="text/css">
   .mlr { margin-left:7px;}
   .mlr th img{ vertical-align:middle; }
</style>
<div>
<div  class="l-form" >
<div id="divImgContract"  class="l-group l-group-hasicon">
</div>
<table class="mlr">
<tr>
<th>
项目名称：
</th>
<td align="left" >
<input type="text" id="Project_Name" name="Project_Name" class="l-text l-text-editing" style=" width:370px;"  />
</td>
<th>
任务编号：
</th>
<td align="left">
<input type="text" id="Task_Number" name="Task_Number" class="l-text l-text-editing" style=" width:200px;" />
</td>
</tr>
<tr>
<th>
合同编号：
</th>
<td align="left">
<input type="text" id="Contract_Code" name="Contract_Code" class="l-text l-text-editing" style=" width:200px;" />
</td>
<th>
签订日期：
</th>
<td align="left" >
<input type="text" id="Contract_Date" name="Contract_Date" class="l-text l-text-editing"   style=" width:200px;"/>
</td>
</tr>
<tr>
<th >
联系人：
</th>
<td align="left">
<input type="text" id="Contacts_Name" name="Contacts_Name" class="l-text l-text-editing"   style=" width:200px;"/>
</td>
<th>
联系电话：
</th>
<td align="left" >
<input type="text" id="Tel_Phone" name="Tel_Phone" class="l-text l-text-editing" style=" width:200px;" />
</td>
</tr>
<tr>
<th  id="thTYPE">
<%--委托类型--%>监测类型：
</th>
<td align="left" >
<input type="text" id="Contract_Type" name="Contract_Type" class="l-text l-text-editing" />
</td>
<th>
样品来源：
</th>
<td align="left">
<input type="text" id="Sample_Source" name="Sample_Source"  class="l-text l-text-editing" />
</td>
</tr>
<tr>
<th align="left">
任务单号：
</th>
<td>
<input type="text" id="TaskListID" name="TaskListID" class="l-text l-text-editing"  style=" width:200px;"/>
</td>
</tr>
<tr>
<th >
计划采样日期：
</th>
<td align="left">
<input type="text" id="Sample_Plan_Date" name="Sample_Plan_Date" class="l-text l-text-editing"  style=" width:200px;"/>
</td>
<th>
计划采样完成日期：
</th>
<td align="left">
<input type="text" id="Sample_Ask_Date" name="Sample_Ask_Date" class="l-text l-text-editing"  style=" width:200px;"/>
</td>
</tr>
<tr>
<th>
备注：
</th>
<td align="left" style="padding-top:8px" colspan="2" class="heightmin" > 
<textarea  id="txtRemarks" name="txtRemarks" class="l-textarea"  cols="100" rows="4" style="height:60px; width:580px;"></textarea> 
 </td>
</tr>
<tr>
<th>
附件：
</th>
<td align="left">
</td>
</tr>
<%--<tr>
<th>
监测目的：
</th>
<td align="left">
<input type="text" id="Monitor_Purpose" name="Monitor_Purpose" class="l-text l-text-editing" style=" width:370px;"   />
</td>
<th>
报告领取：
</th>
<td align="left">
<input type="text" id="Rpt_Way" name="Rpt_Way" class="l-text l-text-editing"  />
</td>
</tr>
<tr>
<th>备 注：</th>
<td align="left" class="l-table-edit-td" colspan="4" > 
<input type="text" id="Remarks" name="Remarks" class="l-text l-text-editing" /> 
 </td>
</tr>--%>
</table>
</div>
<div id="createDiv" style=" margin:12px;"></div>
 <%--<div  id="divCost"  style="margin: 4px; padding: 0; float: left">
 <table cellpadding="0" cellspacing="0" class="mlr" >
            <tr>
                <th>委托书单号：</th>
                <td align="left" class="l-table-edit-td" style="width:160px"><label id="Contract_Code" style="color:Red; font-size:14px; font-weight:bold"></label></td>
           <td  align="right" class="l-table-edit-td">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
               &nbsp;</td>
                 <th id="thConst"  >费用：<img id="imgIcon"/><label id="constDetail" style="color:Red; font-size:14px; font-weight:bold">0</label><a id="btshowConst" href="#">明细</a>&nbsp;&nbsp;&nbsp; <label id="Contract_Fee" style="color:Red; font-size:14px; font-weight:bold"></label></th>
                
            </tr>
            </table>
            </div>

</div>--%>