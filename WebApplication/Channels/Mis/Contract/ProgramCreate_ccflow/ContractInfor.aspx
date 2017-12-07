<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Contract_ProgramCreate_ContractInfor" Codebehind="ContractInfor.aspx.cs" %>

<%@ Register TagName="UserAdd" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractCompanyAdd.ascx" %>
<%@ Register TagName="UserComfrim" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractCompanyComfrim.ascx" %>
<%@ Register TagName="UserContratSelect" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContratTypeSelect.ascx" %>
<%@ Register TagName="UserContratCheck" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractInforCheck.ascx" %>
<%@ Register TagName="UserContratSubmit" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractInforSubmit.ascx" %>
<%@ Register TagName="UserContractOtherInfor" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractOtherInfor.ascx" %>
<%@ Register TagPrefix="UC" TagName="WFControl" Src="~/Sys/WF/UCWFControls.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
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
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <!--货币格式-->
    <script src="../../../../Scripts/jquery.formatCurrency-1.4.0.js" type="text/javascript"></script>
    <script type="text/javascript" src="ContractInfor.js"></script>
    <script src="ContractCompanyAdd.js" type="text/javascript"></script>
    <script src="ContractCompanyComfrim.js" type="text/javascript"></script>
    <script src="ContractOtherInfor.js" type="text/javascript"></script>
    <script src="ContractInforCheck.js" type="text/javascript"></script>
    <script src="ContractInforSubmit.js" type="text/javascript"></script>
    <script src="ContratTypeSelect.js" type="text/javascript"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="">
    <div id="layout1" style="width: 99%; overflow: hidden; border: 1px solid #A3C0E8;">
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
                <UC:UserContractOtherInfor runat="server" ID="UserContractOtherInfor" />
            </div>
            <div>
                <UC:UserContratSelect runat="server" ID="ContratSelect" />
            </div>
            <div id="divContratCheck" style="display: none;">
                <UC:UserContratCheck runat="server" ID="ContratCheck" />
            </div>
            <div id="divContratSubmit" style="display: block">
                <%--<UC:WFControl ID="wfControl" EnableViewState="true" runat="server" />--%>
                <asp:Button ID="btnSave" runat="server" Visible="true" Text="保存" CssClass="l-button l-button-submit"
                    OnClientClick="return Save();" />
            </div>
        </div>
    </div>
    <div>
        <input type="hidden" id="hidTaskId" runat="server" />
        <input type="hidden" id="hidTaskCode" runat="server" />
        <input type="hidden" id="hidTaskProjectName" runat="server" />
        <input type="hidden" id="hidBtnType" runat="server" />
        <input type="hidden" id="hidCompanyId" runat="server" />
        <input type="hidden" id="hidIsNew" runat="server" />
        <asp:Button ID="btnExport" runat="server" Text="" OnClick="btnExport_Click" Style="display: none;" />
        <asp:Button ID="btnExport_QY" runat="server" Text="" OnClick="btnExport_QY_Click"
            Style="display: none;" />
        <asp:Button ID="btnExport_QY_FEE" runat="server" Text="" OnClick="btnExport_QY_FEE_Click"
            Style="display: none;" />
    </div>
    </form>
</body>
</html>
