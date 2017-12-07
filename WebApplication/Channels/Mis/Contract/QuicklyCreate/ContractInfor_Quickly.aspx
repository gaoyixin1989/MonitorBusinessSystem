<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Contract_QuicklyCreate_ContractInfor_Quickly" Codebehind="ContractInfor_Quickly.aspx.cs" %>

<%@ Register TagName="UserAdd" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractCompanyAdd.ascx" %>
<%@ Register TagName="UserComfrim" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractCompanyComfrim.ascx" %>
<%@ Register TagName="UserProgrammingPlan" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractProgrammingPlan.ascx" %>
<%@ Register TagName="UserContratSelect" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContratTypeSelect.ascx" %>
<%@ Register TagName="UserContratCheck" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractInforCheck.ascx" %>
<%@ Register TagName="UserContratSubmit" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractInforSubmit.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <!--Jquery 基础文件-->
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
     <!--Jquery 表单验证-->
     <script src="../../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <!--自动完成-->
    <script src="../../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--LigerUI-->
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"   type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"    type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script type="text/javascript" src="ContractInfor_Quickly.js"></script>
    <script src="ContractCompanyAdd.js" type="text/javascript"></script>
    <script src="ContractCompanyComfrim.js" type="text/javascript"></script>
    <script src="    ContractProgramAuditPlan.js" type="text/javascript"></script>
    <script src="ContractInforCheck.js" type="text/javascript"></script>
    <script src="ContractInforSubmit.js" type="text/javascript"></script>
    <script src="ContratTypeSelect.js" type="text/javascript"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="">
        <div id="layout1" style="width: 99%;overflow:hidden; border:1px solid #A3C0E8; " >
   <div position="top"> 
    <div>
        <UC:UserAdd runat="server" ID="ContratAdd" />
    </div>
        <div>
        <UC:UserComfrim runat="server" ID="ContratConfrim" />
    </div>
    </div>
    <div position="center">
        <div>
        <UC:UserContratSelect runat="server" ID="ContratSelect" />
    </div>
        <div id="divContractPlan" style="display: none;" >
        <UC:UserProgrammingPlan runat="server" ID="UserProgrammingPlan" />
    </div>
    <div id="divContratCheck" style="display: none;" >
        <UC:UserContratCheck runat="server" ID="ContratCheck" />
    </div>
<div id="divContratSubmit" style="display: none; ">
        <UC:UserContratSubmit runat="server" ID="ContratSubmit" />
</div>
</div>
</div>
<div>
<input type="hidden"  id="hidTaskId" runat="server" />
<input type="hidden"  id="hidTaskCode" runat="server"  />
<input type="hidden"  id="hidTaskProjectName"  runat="server" />
</div>
    </form>
</body>
</html>
