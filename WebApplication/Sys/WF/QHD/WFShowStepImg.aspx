<%@ Page Language="C#" AutoEventWireup="True" Inherits="Sys_WF_QHD_WFShowStepImg" Codebehind="WFShowStepImg.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <link href="../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="WFShowStepImg.js"></script>
    <style>
        .tMain
        {
            width: 100%;
        }
        .tMain td
        {
            text-align: center;
            vertical-align: middle;
        }
        .tMain td div
        {
            margin: 0px auto;
            text-align: center;
            border: solid 1px black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="TASK_ID" runat="server" />
    <div id="divTaskInfo" runat="server">
        <table width="100%">
            <tr>
                <td style='width: 99%;'>
                    任务单号：<asp:Label ID="TICKET_NUM" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    委托单号：
                    <asp:Label ID="CONTRACT_CODE" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    项目名称：<asp:Label ID="PROJECT_NAME" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    启动时间：<asp:Label ID="CREATE_DATE" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="100%">
                    <table>
                        <tr>
                            <td style='background-color: #5a8f5a; width: 15px; height: 15px;'>
                            </td>
                            <td>
                                已处理
                            </td>
                            <td style='background-color: #de9a1d; width: 15px; height: 15px;'>
                            </td>
                            <td>
                                待处理
                            </td>
                            <%--                            <td style='background-color: #e34323; width: 15px; height: 15px;'>
                            </td>
                            <td>
                                特殊处理
                            </td>
                            <td style='background-color: #a9a9a9; width: 15px; height: 15px;'>
                            </td>
                            <td>
                                未流转
                            </td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style='height: 5px;'>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div id="dllSelect">
        请选择监测类别：<asp:DropDownList ID="dllMonitor" runat="server" OnSelectedIndexChanged="dllMonitorChange"
            AutoPostBack="true">
        </asp:DropDownList>
    </div>
    <br />
    <div id="divSample" runat="server">
    </div>
    </form>
</body>
</html>
