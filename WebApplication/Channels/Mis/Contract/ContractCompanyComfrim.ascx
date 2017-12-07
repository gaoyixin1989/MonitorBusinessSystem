<%@ Control Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Contract_ContractCompanyComfrim" Codebehind="ContractCompanyComfrim.ascx.cs" %>

<div class="l-form" >
<div id="divImgComFrim"  class="l-group l-group-hasicon tableh1">
</div> 
<table class="mlr">
<tr>
<th>
受检单位：
</th>
<td align="left" >
<input type="text" id="Company_NameFrim" name="Company_NameFrim" class="l-text l-text-editing"  validate="[{required:true, msg:'请选择受检企业！'},{minlength:2,maxlength:40,msg:'企业名称最小长度为2，最大长度为40'}]"   style="width:300px"  />
</td>
<td style="width:1px; color:Red;">*</td>
<%--<td align="right" class="l-table-edit-td" style="width:80px;">
行业类别：
</td>
<td align="left">
<input type="text" id="Industry_NameFrim" name="Industry_NameFrim" class="l-text l-text-editing"  />
</td>--%>
<th>
    所在区域：
</th>
<td align="left">
<input type="text" id="Area_NameFrim" name="Area_NameFrim" class="l-text l-text-editing"   />
</td>
</tr>
<tr>
<th>
联系人：
</th>
<td align="left">
<input type="text" id="Contacts_NameFrim" name="Contacts_NameFrim" class="l-text l-text-editing"  style="width:200px"  />
</td>
<td style="width:1px;"></td>
<th>
联系电话：
</th>
<td align="left" >
<input type="text" id="Tel_PhoneFrim" name="Tel_PhoneFrim" class="l-text l-text-editing"  style="width:200px"  />
</td>
</tr>
<tr>
<th>监测地址：</th>
<td align="left" class="l-table-edit-td" colspan="3" > 
<input type="text" id="AddressFrim" name="AddressFrim" class="l-text l-text-editing" style="width:450px; float:left;"  /> 
 <input type="button" value="复制委托单位信息"  id="btn_Copy" name="btn_Copy" class="l-button l-button-submit" style="width:120px; float:left; margin-left:10px;"  />
 </td>
</tr>
</table>
</div>