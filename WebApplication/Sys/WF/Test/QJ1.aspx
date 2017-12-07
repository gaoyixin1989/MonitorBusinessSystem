<%@ Page Title="" Language="C#" AutoEventWireup="True" Inherits="Sys_WF_Test_QJ1" Codebehind="QJ1.aspx.cs" %>
<%@ Register TagPrefix="UC" TagName="WFControl" Src="~/Sys/WF/UCWFControls.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
     <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />

     <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>

    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
<script type="text/javascript">
    function Save() {
    }
</script>

</head>
<body>
<form runat="server">
    <div style="width: 100%; height: 100px;">
        以下是业务数据填充区域1
    </div>
    <div style="width: 100%; height: 100px;">
        以下是业务数据填充区域2
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    </div>
    <div style="width: 100%; height: 100px;">
        以下是业务数据填充区域3
    </div>
    <UC:WFControl ID="wfControl" EnableViewState="true" runat="server" />
    </form>
</body>
</html>
