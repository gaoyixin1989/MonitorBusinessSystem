<%@ Page Language="C#" AutoEventWireup="True" Inherits="n7.Channels_Mis_Contract_Examine_ContractProgramAudit" Codebehind="ContractExamine.aspx.cs" %>

<%@ Register TagName="UserProgramming" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractProgramming_Info.ascx"%>
<%@ Register TagName="UserProgrammingComPany" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractProgramming_Company.ascx" %>
<%@ Register TagName="UserProgrammingPlan" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractProgrammingPlan.ascx" %>
<%@ Register TagName="UserProgrammingItems" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractProgrammingPointItems.ascx" %>
<%@ Register TagName="UserProgrammingOtherInfor" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractOtherInfor.ascx" %>
<%@ Register TagName="UserProgrammingSubmit" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractProgrammingSubmit.ascx" %>
<%@ Register TagPrefix="UC" TagName="WFControl" Src="~/Sys/WF/UCWFControls.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
          <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
     <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../../CSS/welcome.css" rel="stylesheet" type="text/css" />
     <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
         <!--货币格式-->
    <script src="../../../../Scripts/jquery.formatCurrency-1.4.0.js" type="text/javascript"></script>
     <script src="../../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script> 
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
        <script src="../../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
    <!--业务JS-->
    <title></title>
    <script src="ContractExamine.js" type="text/javascript"></script>
    <script src="ContractExamine_Company.js" type="text/javascript"></script>
    <script src="ContractExamine_Info.js" type="text/javascript"></script>
    <script src="ContractExamine_OtherInfor.js" type="text/javascript"></script>
    <script src="ContractExaminePlan.js" type="text/javascript"></script>
    <script src="ContractExaminePointItems.js" type="text/javascript"></script>
        <script src="ContractExamineSubmit.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="layout1" style="width: 99%;overflow:hidden; border:1px solid #A3C0E8; ">
            <div position="top">
    <div>
        <UC:UserProgramming runat="server" ID="ContratProgramming" />
    </div>
        <div>
        <UC:UserProgrammingComPany runat="server" ID="ContractProCom" />
    </div>
    </div>
    <div position="center">
                                <div>
        <UC:UserProgrammingOtherInfor runat="server" ID="UserProgrammingOtherInfor" />
    </div>
<div>
        <UC:UserProgrammingPlan runat="server" ID="UserProgrammingPlan" />
    </div>
    <div>
        <UC:UserProgrammingItems runat="server" ID="UserProgrammingItems" />
    </div>
<div id="divContratSubmit">
<uc:wfcontrol ID="wfControl" EnableViewState="true" runat="server" />
</div>
</div>
</div>
    <div>
<input type="hidden"  id="hidTaskId" runat="server" />
<input type="hidden"  id="hidBtnType" runat="server" />
<input type="hidden"  id="hidCompanyId" runat="server" />
    </div>
    </form>
</body>
</html>

