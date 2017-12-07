<%@ Page Language="C#" AutoEventWireup="True" Inherits="ccflow_StartHistoryList" Codebehind="StartHistoryList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户抄送任务列表</title>
    <link href="../App_Themes/default/styles/main.css" rel="stylesheet" type="text/css" />
    <link href="../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/ligerui.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script language="javascript">
        var flowId = "<%=Request["flowId"] %>";
    </script>
    <script src="StartHistoryList.js" type="text/javascript"></script>
    
</head>
<body>
    <table style="width:100%;height:100%;" border="0" cellpadding="0" cellspacing="0">
<tr>
    <td >
    <form method="post" id="searchForm" action="" onsubmit="return search(this)">
    
    <dl style="float:left;">
        <dd style="float:left;">
            发起时间:
        </dd>
        <dt style="float:left;">
            <input name="RDTStart" class="Wdate" readonly onfocus="WdatePicker()" id="RDTStart"/>-
            <input name="RDTEnd" class="Wdate" readonly onfocus="WdatePicker()" id="RDTEnd"/>
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
    <div id="todoGrid" style="border:0px;"></div>  
</td>
</tr>
    
</table>
</body>
</html>
