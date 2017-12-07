<%@ Control Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Contract_ContractQCStep" Codebehind="ContractQCStep.ascx.cs" %>
<style type="text/css">
   .mlr { margin-left:7px;}
   .mlr th img{ vertical-align:middle; }
   .heightmin div{ height:60px;}
</style>
<div  class="l-form" >
<div id="divImgQC"  class="l-group l-group-hasicon tableh1">
</div>
<table class="mlr">
<tr>
<th>
质控样:
</th>
<td align="left"  style="padding-top:5px">
<input type="text" id="txtQC" name="txtQC" class="l-text l-text-editing" style=" width:525px;"  />
</td>
</tr>
<tr>
<th>
质控措施：
</th>
<td align="left" style="padding-top:5px" colspan="2" class="heightmin">
<textarea id="txtQcStep" name="txtQcStep" class="l-textarea"  cols="100" rows="4" style="height:160px; width:580px;"></textarea>
</td>
</tr>
</table>
</div>