<%@ Control Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_SinceSample_ContratTypeSelect_Since" Codebehind="ContratTypeSelect_Since.ascx.cs" %>
<div class="l-form">
<div  id="divImgSelect" class="l-group l-group-hasicon  tableh1">
</div>
<table class="mlr"  cellspacing="0" cellpadding="0">
<tr>
<th>
委托类型:
</th>
<td align="left" >
<input type="text" id="Contract_Type" name="Contract_Type" class="l-text l-text-editing" />
</td>
<th>
委托年度:
</th>
<td align="left">
<input type="text" id="Contrat_Year" name="Contrat_Year" class="l-text l-text-editing"  />
</td>
</tr>
<tr>
<th>
监测类型:
</th>
<td align="left" >
<input type="text" id="Monitor_Type" name="Monitor_Type" class="l-text l-text-editing"  />
</td>
<th>
送样频次:</th>
 <td align="left" class="l-table-edit-td">
<input type="text" id="FREQ" name="FREQ" class="l-text l-text-editing" 
         style="text-align: left"  /></td>
</tr>
<tr>
<th>
    送样人:</th>
<td align="left" >
<input type="text" id="SAMPLE_SEND_MAN" name="SAMPLE_SEND_MAN" 
        class="l-text l-text-editing"  style="width:200px" /></td>
 <th>
    接样人:
    </th>
 <td align="left" class="l-table-edit-td">
<input type="text" id="SAMPLE_ACCEPTER" name="SAMPLE_ACCEPTER" class="l-text l-text-editing" 
         style="text-align: left;width:200px"  /></td>
         <td>
<input type="button" value="确认" id="btn_OkSelect" name="btn_OkSelect"  class="l-button l-button-submit" /></td>
</tr>
</table>
</div>