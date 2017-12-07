<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Mis_Contract_Acceptance_AcceptanceApproval" Codebehind="AcceptanceApproval.aspx.cs" %>

<%@ Register TagName="WFControl" TagPrefix="UC" Src="~/Sys/WF/UCWFControls.ascx" %>
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
    <script src="../../../../Controls/ligerui/lib/draggable.js" type="text/javascript"></script>
    <!--业务JS-->
    <script type="text/javascript" src="AcceptanceSchedule.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="hdnContracID" runat="server" />
    <div v id="layout1">
        <table style="width: 100%">
            <tr>
                <td colspan="100%" style="clear: both; padding-bottom: 7px;">
                    <div class="tableh2">
                        <img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />委托书信息</div>
                    <div id="divContractInfo">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="100%" style="clear: both; padding-bottom: 7px;">
                    <div class="tableh2">
                        <img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />企业信息</div>
                    <div id="divClientCompanyInfo">
                    </div>
                    <div id="divTestedCompanyInfo">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="100%" style="clear: both; padding-bottom: 7px;">
                    <div class="tableh2">
                        <img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />方案附件</div>
                    <div style="clear: both; margin-left: 40px;">
                        方案附件： <a onclick="downLoad();" style="cursor: pointer" href="#">下载</a>
                    </div>
                </td>
            </tr>
        </table>
        <%--信息提交--%>
        <div id="divContractSubmit">
            <UC:WFControl ID="wfControl" EnableViewState="true" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
