<%@ Page Language="C#" AutoEventWireup="True" Inherits="ccflow_ShareWorkList" Codebehind="ShareWorkList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>共享任务列表</title>
    <link href="../App_Themes/default/styles/main.css" rel="stylesheet" type="text/css" />
    <link href="../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/ligerui.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="ShareWorkList.js" type="text/javascript"></script>
</head>
<body>
 <table style="width:100%;height:100%;" border="0" cellpadding="0" cellspacing="0">
<tr>
    <td >
    <form method="post" id="searchForm" action="" onsubmit="return search(this)">
    <dl style="float:left;">
        <dd style="float:left;">
            流程名称:
        </dd>
        <dt style="float:left;">
            <input name="flowName" class="" id="flowName"/>
        </dt>
    </dl>
    <dl style="float:left;">
        <dd style="float:left;">
            完成日期:
        </dd>
        <dt style="float:left;">
            <input name="endRDTStart" class="Wdate" readonly onfocus="WdatePicker()" id="endRDTStart"/>-
            <input name="endRDTEnd" class="Wdate" readonly onfocus="WdatePicker()" id="endRDTEnd"/>
        </dt>
    </dl>
     <dl style="float:left;">
        <dd style="float:left;"><button type="submit">查 询</button>
        &nbsp;<button type="reset">清 空</button>
         </dd>
        
     </dl>   
    </form>
    </td>
</tr>
<tr>
<td>
<div id="tab" class="l-tab">
    
        <div tabid="isObtained" title="已领" style="width:100%;height:100%;"><div id="todoGrid" style="border:0px;"></div> </div>
        <div tabid="unObtained" title="未领" style="width:100%;height:100%;"><div id="grid2" style="border:0px;"></div> </div>
   
</div>
</td>
</tr>
    
</table>
</body>
</html>
