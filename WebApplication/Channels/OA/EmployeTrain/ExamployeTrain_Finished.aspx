<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_EmployeTrain_ExamployeTrain_Finished" Codebehind="ExamployeTrain_Finished.aspx.cs" %>
<%@ Register TagPrefix="UC" TagName="WFControl" Src="~/Sys/WF/UCWFControls.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
     <!--Jquery 基础文件-->
     <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>

     <!--自动完成-->
    <script src="../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--提示-->
    <script src="../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
     <!--LigerUI-->
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="ExamployeTrain_Finished.js" type="text/javascript"></script>

    <style type="text/css">
    
    .l-minheight div{ height:90px; overflow:hidden;}
    
</style>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    	<h1 class="h12013">年 度 培 训 计 划 登 记</h1>
<div class="tablelayout"> 
<br/>
<table  cellspacing="0" cellpadding="0" align="center" class="tableedit mlr8">
<tr>
<th>
培训主题:
</th>
<td align="left" colspan="3">
<input type="text"  id="TrainBt" name="TrainBt" class="l-text l-text-editing"   style=" width:600px"/>
</td>
</tr>
<tr>
<th >
计划年度:
</th>
<td align="left" >
<input type="text" id="TrainYear" name="TrainYear" class="l-text l-text-editing"   style=" width:245px"/>
</td>
<th>
培训分类:
</th>
<td>
<input type="text" id="TrainType" name="TrainType" class="l-text l-text-editing"   style=" width:245px"/>
</td>
</tr>
<tr>
<th >
培训对象:
</th>
<td align="left" colspan="3">
<input type="text" id="TrainTo" name="TrainTo" class="l-text l-text-editing"   style=" width:600px;"/>
</td>
</tr>
<tr>
<th >
培训内容:
</th>
<td align="left" colspan="3" class="l-minheight" >
<textarea cols="100" rows="5" class="l-textarea" id="TrainInfor" style="width:600px;  padding:2px; margin-left:0; line-height:20px;" ></textarea>
</td>
</tr>
<tr>
<th >
培训目标:
</th>
<td align="left" colspan="3" class="l-minheight" style=" padding-top:5px">
<textarea cols="100" rows="5" class="l-textarea" id="TrainTarger" style="width:600px;  padding:2px; margin-left:0; line-height:20px; " ></textarea>
</td>
</tr>
<tr>
<th >
培训时间:
</th>
<td align="left" >
<input type="text" id="TrainDate" name="TrainDate" class="l-text l-text-editing"   style=" width:245px"/>
</td>
<th>
负责部门:
</th>
<td>
<input type="text" id="TrainDept" name="TrainDept" class="l-text l-text-editing"   style=" width:245px"/>
</td>
</tr>
<tr>
<th >
考核方式:
</th>
<td align="left" >
<input type="text" id="ExamType" name="ExamType" class="l-text l-text-editing"   style=" width:245px"/>
</td>
<th></th>
<td></td>
</tr>
<tr id="trOption" >
<th >
审核意见:
</th>
<td align="left" colspan="3"  class="l-minheight">
<textarea cols="100" rows="5" class="l-textarea" id="TrainOption" style="width:600px;  padding:2px; margin-left:0; line-height:20px;" ></textarea>
</td>
</tr>
<tr>
<th>
附件信息:
</th>
<td align="left" colspan="3"  >
<div id="divDownLoad" style="float:left; margin-right:10px; width:100px; overflow:hidden;"  >
<a id="btnFiledownLoad" href="javascript:">点击查看下载附件</a>
</div>
 <div id="divFileUp" style="float:left; margin-right:10px; width:100px; overflow:hidden; " >
<a id="btnFileUp"  href="javascript:">点击上传</a>
</div>
</td>
</tr>
</table>
<div id="divContratSubmit" >
<UC:WFControl ID="wfControl" EnableViewState="true" runat="server" />
</div>
</div>
<div>
<input type="hidden" id="hidEmployeID" />
<input type="hidden" id="hidLeaderOption" runat="server" />
<input type="hidden"  id="hidTaskId" runat="server" />
<input type="hidden"  id="hidTaskProjectName"  runat="server" />
</div>
    </form>
</body>
</html>
