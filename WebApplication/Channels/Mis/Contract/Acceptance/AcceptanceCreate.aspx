<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="Channels_Mis_Contract_Acceptance_AcceptanceCreate" Codebehind="AcceptanceCreate.aspx.cs" %>

<%@ Register TagPrefix="UC" TagName="WFControl" Src="~/Sys/WF/UCWFControls.ascx" %>
<%@ Register TagName="UserAdd" TagPrefix="UC" Src="../ContractCompanyAdd.ascx" %>
<%@ Register TagName="UserComfrim" TagPrefix="UC" Src="../ContractCompanyComfrim.ascx" %>
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
    <script type="text/javascript" src="ContractInfor.js"></script>
    <script type="text/javascript" src="AcceptanceCreate.js"></script>
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
        <tr>
            <td style="text-align: center">
                <%--信息提交--%>
                <div id="divContractSubmit" style="display: none">
                    <UC:WFControl ID="wfControl" EnableViewState="true" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    <div>
    <input type="hidden"  id="hidTaskId" runat="server" />
    <asp:Button ID="btnExport" runat="server" Text="" onclick="btnExport_Click"  style="display:none;" />
    </div>
    </form>
</body>
</html>
