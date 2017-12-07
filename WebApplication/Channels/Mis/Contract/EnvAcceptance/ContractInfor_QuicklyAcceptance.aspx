<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Mis_Contract_EnvAcceptance_ContractInfor_QuicklyAcceptance" Codebehind="ContractInfor_QuicklyAcceptance.aspx.cs" %>

<%@ Register TagName="UserAdd" TagPrefix="UC" Src="../ContractCompanyAdd.ascx" %>
<%@ Register TagName="UserComfrim" TagPrefix="UC" Src="../ContractCompanyComfrim.ascx" %>
<%@ Register TagName="UserContratSubmit" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractInforSubmit.ascx" %>
<%@ Register TagName="UserProgrammingPlan" TagPrefix="UC" Src="~/Channels/Mis/Contract/ContractProgrammingPlan.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <!--Jquery 基础文件-->
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <!--自动完成-->
    <script src="../../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--提示-->
    <script src="../../../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
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
    <script src="../../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script type="text/javascript" src="ContractInforQuickly.js"></script>
    <script type="text/javascript" src="ContractInfor_QuicklyAcceptance.js"></script>
    <script type="text/javascript" src="ContractProgramAuditPlan.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="CONTRACT_ID" runat="server" />
    <input type="hidden" id="Contract_Type" runat="server" />
    <%--委托单位信息--%>
    <div>
        <UC:UserAdd runat="server" ID="ContratAdd" />
    </div>
    <%--受检单位信息--%>
    <div>
        <UC:UserComfrim runat="server" ID="ContratConfrim" />
    </div>
    <%--选择委托类型--%>
    <table>
        <tr>
            <td>
                <div id="divContractType">
                </div>
            </td>
            <td style="text-align: left; vertical-align: bottom; padding-bottom: 1px; padding-left: 3px">
                <input id="btn_OkSelect" type="button" value="确认" class="l-button l-button-submit" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="100%" style="clear: both; padding-bottom: 7px;">
                <div id="divContractPlan" style="display: none;">
                    <UC:UserProgrammingPlan runat="server" ID="UserProgrammingPlan" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <%--委托书信息--%>
                <div id="divContractInfo" style="display: none">
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 35px; padding-top: 10px">
                <div id="divContractCode" style="display: none;">
                    委托书单号：<label id="Contract_Code" style="color: Red; font-weight: bold"></label>
                </div>
            </td>
        </tr>
    </table>
    <div id="divContratSubmit" style="display: none">
        <UC:UserContratSubmit runat="server" ID="ContratSubmit" />
    </div>
    </form>
</body>
</html>
