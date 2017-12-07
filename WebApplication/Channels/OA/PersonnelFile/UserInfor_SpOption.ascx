<%@ Control Language="C#" AutoEventWireup="True" Inherits="Channels_OA_PersonnelFile_UserInfor_SpOption" Codebehind="UserInfor_SpOption.ascx.cs" %>

<style type="text/css">
    .style1
    {
        width: 303px;
    }
    .l-tableheight div{ height:185px; margin:5px 0;}
</style> 

<div class="l-form" > 
<div id="divImgOption"  class="l-group l-group-hasicon tableh1">
</div>
<table class="mlr" cellspacing="0" cellpadding="0">
<tr>
<th style="text-align: right" >
考核等级
</th>
<td align="left" colspan="3" >
<input type="text" id="ExamLevel" name="ExamLevel" class="l-text l-text-editing"   style=" width:200px"/>
</td>
</tr>
<tr>
<th style="text-align: right" >

</th>
<td align="left" class="l-table-edit-td l-tableheight" colspan="3"> 
<textarea cols="100" rows="5" class="l-textarea" id="ExamOption" style="padding:2px; width:620px; margin-left:8px; line-height:20px; height:180px; " ></textarea>
</td>
</tr>
<tr>
<th style="text-align: right" >
单位负责人
</th>
<td align="left" >
<input type="text" id="ExamLeader" name="ExamLeader" class="l-text l-text-editing"   style=" width:200px"/>
</td>
<th>
审批时间
</th>
<td align="left" class="style1" >
<input type="text" id="LeaderDate" name="LeaderDate" class="l-text l-text-editing"   style=" width:200px"/>
</td>
</tr>
</table>
</div>