<%@ Control Language="C#" AutoEventWireup="True" Inherits="Channels_MisII_Contract_ContractCompanyAdd" Codebehind="ContractCompanyAdd.ascx.cs" %>


<div class="l-form" > 
<div id="divImgCom"  class="l-group l-group-hasicon tableh1">
</div>
<table class="mlr" cellspacing="0" cellpadding="0">
<tr>
<th >
委托单位：
</th>
<td align="left" >
<input type="text" id="Company_Name" name="Company_Name" class="l-text l-text-editing"  validate="[{required:true, msg:'请选择委托企业！'},{minlength:2,maxlength:40,msg:'企业名称最小长度为2，最大长度为40'}]"  style=" width:300px"/>
</td>
<td style="width:1px; color:Red;">*</td>
<%--<td align="right" class="l-table-edit-td"  style="width:80px;">
行业类别：
</td>
<td align="left">
<input type="text" id="Industry_Name" name="Industry_Name" class="l-text l-text-editing"  />
</td>--%>
<th>
&nbsp;所在区域：
</th>
<td>
<input type="text" id="Area_Name" name="Area_Name" class="l-text l-text-editing"  />
</td>
</tr>
<tr >
</tr>
<tr>
<th >
联系人：
</th>
<td align="left">
<input type="text" id="Contacts_Name" name="Contacts_Name" class="l-text l-text-editing"   style=" width:200px;"/>
</td>
<td style="width:1px;"></td>
<th>
联系电话：
</th>
<td align="left" >
<input type="text" id="Tel_Phone" name="Tel_Phone" class="l-text l-text-editing" style=" width:200px;" />
</td>
</tr>
<tr >
</tr>
<tr>
<th>联系地址：</th>
<td align="left" class="l-table-edit-td" colspan="3"> 
<input type="text" id="Address" name="Address" class="l-text l-text-editing" style="width:450px"  ltype="text" validate="{required:true,maxlength:20}"  /> 
 </td>
</tr>
</table>
</div>