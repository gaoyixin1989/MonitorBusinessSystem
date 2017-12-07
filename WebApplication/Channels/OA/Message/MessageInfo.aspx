<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_Message_MessageInfo" Codebehind="MessageInfo.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/json2.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="MessageInfo.js" type="text/javascript"></script>
    <style type="text/css">
        h1
        {
            color: Green;
        }
        #listLeft
        {
            width: 160px;
            height: 260px;
            text-align: right;
        }
        
        #listRight
        {
            width: 160px;
            height: 260px;
            text-align: left;
        }
        .normal
        {
            font-size: 12px;
            width: 10px;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divEdit">
    </div>
    <table>
        <tr>
            <td align="right" class="l-table-edit-td" style="width:91px;">消息内容：</td>
            <td align="left" class="l-table-edit-td"> 
                <textarea id="MESSAGE_CONTENT" name="MESSAGE_CONTENT" cols="100" rows="4" class="l-textarea" style="width:438px"></textarea>
            </td>
        </tr>
    </table>
    </form>
    <div>
        <input runat="server" type="hidden" id="UserRealName" />
        <input runat="server" type="hidden" id="UserID" />
    </div>
    <div id="targerdiv" class="l-form" style="width: 350px; margin: 3px; display: none;">
        <ul>
            <li style="width: 37px; text-align: left;">部门： </li>
            <li style="width: 240px; text-align: left;">
                <input id="Dept" class="l-text l-text-editing" name="Dept" type="text" />
            </li>
            <li>
                <table cellpadding="0" cellspacing="0" class="l-table-edit">
                    <tr>
                        <td align="center" colspan="2">
                            <div>
                                <b>未选用户</b>
                                <select size="10" name="listLeft" multiple="multiple" id="listLeft" class="normal"
                                    title="双击可实现右移">
                                </select>
                            </div>
                        </td>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="center">
                            <input type="button" id="btnRight" value=">>" class="l-button l-button-submit" /><br />
                            <input type="button" id="btnLeft" value="<<" class="l-button l-button-submit" />
                        </td>
                        <td align="center" class="l-table-edit-td">
                            &nbsp;
                        </td>
                        <td align="center" class="l-table-edit-td">
                            <b>已选用户</b>
                            <select size="10" multiple="multiple" name="listRight" id="listRight" class="normal"
                                title="双击可实现左移">
                            </select>
                        </td>
                    </tr>
                </table>
            </li>
        </ul>
    </div>
</body>
</html>
