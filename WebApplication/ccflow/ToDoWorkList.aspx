<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Masters/Searchex.master" Inherits="ccflow_ToDoWorkList" Codebehind="ToDoWorkList.aspx.cs" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <link href="../App_Themes/default/styles/main.css" rel="stylesheet" type="text/css" />
    <link href="../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Controls/ligerui/lib/ligerUI/js/ligerui.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="ToDoWorkList.js" type="text/javascript"></script>
    <style>
        .l-dialog-content
        {
            padding:0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphData" runat="Server">
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
            接收日期:
        </dd>
        <dt style="float:left;">
            <input name="ADTStart" class="Wdate" readonly onfocus="WdatePicker()" id="ADTStart"/>-
            <input name="ADTEnd" class="Wdate" readonly onfocus="WdatePicker()" id="ADTEnd"/>
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
</asp:Content>
