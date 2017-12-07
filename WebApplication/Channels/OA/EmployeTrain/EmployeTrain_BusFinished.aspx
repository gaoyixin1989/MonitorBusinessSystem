<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_EmployeTrain_EmployeTrain_BusFinished" Codebehind="EmployeTrain_BusFinished.aspx.cs" %>
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
    <script src="EmployeTrain_BusFinished.js" type="text/javascript"></script>

    <style type="text/css">
    
    .l-minheight div{ height:90px; overflow:hidden;}
    </style>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    	<h1 class="h12013">员 工 培 训 计 划 登 记</h1>
<div class="tablelayout"> 
<br/>
<table  cellspacing="0" cellpadding="0" align="center" class="tableedit mlr8">
    <tr>
<th>
培训主题:
</th>
<td align="left" colspan="12">
<input type="text"  id="TrainBt" name="TrainBt" class="l-text l-text-editing"   style=" width:600px"/>
</td>
</tr>
    <tr>
<th>
申请部门:
</th>
<td align="left" colspan="12">
<input type="text"  id="TrainDept" name="TrainDept" class="l-text l-text-editing"   style=" width:600px"/>
</td>
</tr>
    <tr>
<th >
 培训机构:
</th>
<td align="left" colspan="12">
<input type="text" id="TrainCompany" name="TrainCompany" class="l-text l-text-editing"   style=" width:600px;"/>
</td>
    </tr>
    <tr>
<th >
培训内容:
</th>
<td align="left" colspan="12" class="l-minheight" >
<textarea cols="100" rows="5" class="l-textarea" id="TrainInfor" 
        style="width:600px;  padding:2px; margin-left:0; line-height:20px;" name="S1" ></textarea>
</td>
    </tr>
    <tr>
<th >
拟培训人员:
</th>
<td align="left" colspan="12">
<input type="text" id="TrainTo" name="TrainTo" class="l-text l-text-editing"   
        style=" width:600px;"/>
</td>
    </tr>
<tr>
<th >
培训时间:
</th>
<td align="left" colspan="12">
<input type="text" id="TrainDate" name="TrainDate" class="l-text l-text-editing"   style=" width:245px"/></td>
</tr>
    <tr id="trTchOption">
<th style="width:90px">
技术负责人意见:
</th>
<td align="left" colspan="12" class="l-minheight" style=" padding-top:5px">
<textarea cols="100" rows="5" class="l-textarea" id="TrainThOption" style="width:600px;  padding:2px; margin-left:0; line-height:20px; " ></textarea>
</td>
</tr>
    <tr id="trTch">
<th >
负责人签名:
</th>
<td colspan="6" >
<input type="text" id="TchName" name="TchName" class="l-text l-text-editing"   style=" width:200px"/>
</td>
<th>
签名日期:
</th>
<td>
<input type="text" id="TchDate" name="TchDate" class="l-text l-text-editing"   style=" width:200px"/>
</td>
</tr>
    <tr id="trOption">
<th >
    站长意见:
</th>
<td align="left" colspan="12"  class="l-minheight">
<textarea cols="100" rows="5" class="l-textarea" id="TrainOption" 
        style="width:600px;  padding:2px; margin-left:0; line-height:20px;" name="S2" ></textarea>
</td>
    </tr>
    <tr id="trLeader">
<th >
签名:
</th>
<td colspan="6" >
<input type="text" id="LeaderName" name="LeaderName" class="l-text l-text-editing"   style=" width:200px"/>
</td>
<th>
签名日期:
</th>
<td>
<input type="text" id="LeaderDate" name="LeaderDate" class="l-text l-text-editing"   style=" width:200px"/>
</td>
</tr>
    <tr id="trResult" >
<th >
培训结果:
</th>
<td align="left" colspan="12"  class="l-minheight">
<textarea cols="100" rows="5" class="l-textarea" id="TrainResult" style="width:600px;  padding:2px; margin-left:0; line-height:20px;" ></textarea>
</td>
</tr>
<tr id="trFileInfor">
<th>
附件信息:
</th>
<td align="left" >
<div id="divDownLoad" >
<a id="btnFiledownLoad" href="#" >点击查看下载附件</a>
</div>
</td>
<td align="left" >
<a id="btnFileUp"  href="javascript:">点击上传</a>
</td>
<td align="left" >
    &nbsp;</td>
<td align="left" >
    &nbsp;</td>
<td align="left" >
    &nbsp;</td>
<td align="left" >
    &nbsp;</td>
<td align="left" >
 <div id="divFileUp" >
</div>
</td>
<td align="left" >
<td align="left" >
    &nbsp;</td>
<td align="left" >
    &nbsp;</td>
<td align="left" >
    &nbsp;</td>
<td align="left">
&nbsp;</td>
</tr>
</table>
<div id="divContratSubmit" >
<UC:WFControl ID="wfControl" EnableViewState="true" runat="server" />
</div>
</div>
<div>
<input type="hidden" id="hidEmployeID" />
<input type="hidden" id="hidLeaderOption" runat="server" />
<input type="hidden" id="hidTchOption" runat="server" />
<input type="hidden" id="hidResult" runat="server" />
<input type="hidden"  id="hidTaskId" runat="server" />
<input type="hidden"  id="hidTaskProjectName"  runat="server" />
</div>
    </form>
</body>
</html>