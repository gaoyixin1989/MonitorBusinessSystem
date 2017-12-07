<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Base_Search_SearchQHD_TaskFlow_Rpt" Codebehind="TaskFlow_Rpt.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js"  type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"  type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"  type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <link href="../../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script src="../../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
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
    <style type="text/css">
        body
        {
            font-size: 12px;
        }
        ul, li
        {
            list-style: none;
        }
        ul, li, h1, h2, h3, h4, p
        {
            padding: 0;
            margin: 0;
        }
        .layout20121
        {
            margin: 0 auto;
            width: 330px;
            overflow: hidden;
            color: #333;
        }
        
        .listgreen
        {
            line-height: 22px;
            text-align: center;
        }
        .listgreen p
        {
            border: 1px solid #bfe0bf;
            border-top: none;
            background: #e9f9e9;
            padding: 5px;
            text-align: left;
        }
        .listgreen p span
        {
            width: 5em;
        }
        .listgreen h2
        {
            background: #5a8f5a;
            color: #fff;
            font-size: 12px;
        }
        .listgreen img
        {
            margin: 4px;
        }
        
        .listyellow
        {
            line-height: 22px;
            text-align: center;
        }
        .listyellow p
        {
            border: 1px solid #e5d7bc;
            border-top: none;
            background: #f5ecdb;
            padding: 5px;
            text-align: left;
        }
        .listyellow p span
        {
            width: 5em;
        }
        .listyellow h2
        {
            background: #de9a1d;
            color: #fff;
            font-size: 12px;
        }
        .listyellow img
        {
            margin: 4px;
        }
        
        .listred
        {
            line-height: 22px;
            text-align: center;
        }
        .listred p
        {
            border: 1px solid #ebc8bf;
            border-top: none;
            background: #f8e6e2;
            padding: 5px;
            text-align: left;
        }
        .listred p span
        {
            width: 5em;
        }
        .listred h2
        {
            background: #e34323;
            color: #fff;
            font-size: 12px;
        }
        .listred img
        {
            margin: 4px;
        }
        
        .listgray
        {
            line-height: 22px;
            text-align: center;
        }
        .listgray p
        {
            border: 1px solid #d4d4d4;
            border-top: none;
            background: #eaeaea;
            padding: 5px;
            text-align: left;
        }
        .listgray p span
        {
            width: 5em;
        }
        .listgray h2
        {
            background: #a9a9a9;
            color: #fff;
            font-size: 12px;
        }
        .listgray img
        {
            margin: 4px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="TASK_ID" runat="server" />
    <input type="hidden" id="Monitor_ID" runat="server" />
    <input type="hidden" id="step_type" runat="server" />
    <div id="divTaskInfo" runat="server">
        <table width="100%">
            <tr>
                <td colspan="100%">
                    <table>
                        <tr>
                            <td>图例：</td>
                            <td style='background-color: #5a8f5a; width: 15px; height: 15px;'></td>
                            <td>已处理</td>
                            <td style='background-color: #de9a1d; width: 15px; height: 15px;'></td>
                            <td>待处理</td>
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
    <br />
    <div id="divRpt" runat="server"> </div>
    </form>
</body>
</html>

