<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Env_Fill_FillFlow" Codebehind="FillFlow.aspx.cs" %>
<%@ Register TagPrefix="UC" TagName="WFControl" Src="~/Sys/WF/UCWFControls.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
     <!--Jquery 基础文件-->
     <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>

     <!--自动完成-->
    <script src="../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--提示-->
    <script src="../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
     <!--LigerUI-->
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
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
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
            <!--货币格式-->
    <script src="../../../Scripts/jquery.formatCurrency-1.4.0.js" type="text/javascript"></script>
    <script src="FillFlow.js" type="text/javascript"></script>

    <style type="text/css">
        .l-minheight div
        {
            height: 90px;
            overflow: hidden;
        }
        .style1
        {
            width: 15%;
            height: 22px;
        }
    </style>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h1 class="h12013">
        环 境 数 据 填 报 流 程</h1>
    <br />
    <div class="tablelayout">
        <table border="0" cellpadding="0" cellspacing="0" class="tableedit mlr8">
            <tr>
                <th>
                    标题
                </th>
                <td colspan="5">
                    <asp:Label ID="PF_TITLE" runat="server" Text="" class="tableedit_title"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    年度
                </th>
                <td >
                    <asp:Label ID="PF_YEAR" Width="10px" runat="server" Text=""></asp:Label>
                </td>
                <td style="text-align: right;">
                    月度
                </td>
                <td>
                    <asp:Label ID="PF_MONTH" runat="server"></asp:Label>
                </td>
                <td></td><td></td>
            </tr>
            <tr>
                <th>
                    填报数据
                </th>
                <td align="left" colspan="5" style="height:320px;" >
                    <div id="maingrid"></div>
                </td>
            </tr>
        </table>
        <div id="divContratSubmit">
            <UC:WFControl ID="wfControl" EnableViewState="true" runat="server" />
        </div>
        <div>
            <input type="hidden" id="hidID" runat="server" />
            <input type="hidden" id="hidStatus" value="0" runat="server" />
            <input type="hidden" id="hidTableName" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
