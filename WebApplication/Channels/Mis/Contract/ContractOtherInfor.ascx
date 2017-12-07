<%@ Control Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Contract_ContractOtherInfor" Codebehind="ContractOtherInfor.ascx.cs" %>
<style type="text/css">
   .mlr { margin-left:7px;}
   .mlr th img{ vertical-align:middle; }
   .heightmin div{ height:60px;}
</style>
<div  class="l-form" >
<div id="divImgOther"  class="l-group l-group-hasicon tableh1">
</div>
<table class="mlr">
<tr>
<th>
提供资料：
</th>
<td align="left">
<input type="text" id="txtProData" name="txtProData" class="l-text l-text-editing" style="width:580px;"   />
</td>
</tr>
<tr>
<th>
其他要求：
</th>
<td align="left">
<input type="text" id="txtOtherAsk" name="txtOtherAsk" class="l-text l-text-editing" style=" width:580px;"  />
</td>
</tr>
<tr>
<th>
监测依据：
</th>
<td align="left" style="padding-top:5px" colspan="2" class="heightmin">
<textarea id="txtAccording" name="txtAccording" class="l-textarea"  cols="100" rows="4" style="height:60px; width:580px;"></textarea>
</td>
</tr>
<tr>
<th>备 注：</th>
<td align="left" style="padding-top:8px" colspan="2" class="heightmin" > 
<textarea  id="txtRemarks" name="txtRemarks" class="l-textarea"  cols="100" rows="4" style="height:60px; width:580px;"></textarea> 
 </td>
</tr>
</table>
</div>