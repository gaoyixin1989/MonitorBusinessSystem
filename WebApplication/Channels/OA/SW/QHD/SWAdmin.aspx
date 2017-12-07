<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_OA_SW_QHD_SWAdmin" Codebehind="SWAdmin.aspx.cs" %>

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
    <script src="../../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
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
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenuBar.js"
        type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
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
    <script src="SWAdmin.js" type="text/javascript"></script>
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
        收 文 呈 批 表</h1>
    <div class="tablelayout">
        <div class="tabletop ">
            原文编号：<asp:TextBox ID="FROM_CODE" runat="server" Text=""></asp:TextBox><p>
                日期：<asp:Label ID="SW_REG_DATE" runat="server"></asp:Label>
            </p>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" class="tableedit mlr8">
            <tr>
                <th>
                    文件标题
                </th>
                <td colspan="5">
                    <asp:TextBox ID="SW_TITLE" runat="server" Text="" class="tableedit_title"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    来文单位
                </th>
                <td>
                    <asp:TextBox ID="SW_FROM" runat="server" Text="" Style="width: 160px;"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 55px;">
                    收文份数
                </td>
                <td style="width: 80px;">
                    <asp:TextBox ID="SW_COUNT" runat="server" Text="" Style="width: 40px;"></asp:TextBox>
                    &nbsp;份
                </td>
                <td style="text-align: right; width: 55px;">
                    紧急程度
                </td>
                <td>
                    <asp:DropDownList ID="MJ" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    签收人
                </th>
                <td>
                    <asp:TextBox ID="SW_SIGN_ID" runat="server" Text="" Style="width: 160px;"></asp:TextBox>
                </td>
                <td style="text-align: right;">
                    签收日期
                </td>
                <td>
                    <asp:TextBox ID="SW_SIGN_DATE" runat="server" Text="" Style="width: 100px;"></asp:TextBox>
                </td>
                <td style="text-align: right;">
                    收文字号
                </td>
                <td>
                    <asp:TextBox ID="SW_CODE" runat="server" Text="" Style="width: 100px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    附件信息:
                </th>
                <td align="left" colspan="5">
                    <div id="divDownLoad">
                        <a id="btnFiledownLoad" href="#">点击查看下载附件</a> <a id="btnFileUp" href="javascript:">点击上传</a>
                    </div>
                </td>
            </tr>
      <%--      <tr>
                <th rowspan="2">
                    收文拟办审核
                </th>
                <td colspan="5">
                    <textarea cols="100" rows="3" class="l-textarea" id="SW_PLAN_INFO" runat="server"
                        style="padding: 2px; margin-left: 0; line-height: 20px;" name="S1"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="text-align: right;">
                    <span>办理人：<asp:Label ID="SW_PLAN_ID" runat="server" Text=""></asp:Label></span>
                    <span>办理时间：<asp:Label ID="SW_PLAN_DATE" runat="server" Text=""></span></asp:Label>
                </td>
            </tr>
            <tr>
                <th rowspan="2">
                    收文批办审核
                </th>
                <td colspan="5">
                    <textarea cols="100" rows="3" class="l-textarea" id="SW_PLAN_APP_INFO" runat="server"
                        style="padding: 2px; margin-left: 0; line-height: 20px;" name="S1"></textarea>
                </td>
            </tr> 
            <tr>
                <td colspan="5" style="text-align: right;">
                    <span>办理人：<asp:Label ID="SW_PLAN_APP_ID" runat="server" Text=""></asp:Label></span>
                    <span>办理时间：<asp:Label ID="SW_PLAN_APP_DATE" runat="server" Text=""></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <th rowspan="2">
                    收文办理负责人意见
                </th>
                <td colspan="5">
                    <textarea cols="100" rows="3" class="l-textarea" id="SW_APP_INFO" runat="server"
                        style="padding: 2px; margin-left: 0; line-height: 20px;" name="S1"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="text-align: right;">
                    <span>办理人：<asp:Label ID="SW_APP_ID" runat="server" Text=""></asp:Label></span> <span>
                        办理时间：<asp:Label ID="SW_APP_DATE" runat="server" Text=""></asp:Label></span>
                </td>
            </tr>--%>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="right">
                    <asp:Button ID="btn_Save" runat="server" CssClass="l-button l-button-submit" Text="保存"
                        OnClick="btn_Save_Click" />
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td align="left">
                    <input type="button" value="返回" id="btn_Back" name="btn_Back" class="l-button l-button-submit"
                        onclick="javascript:self.location='SWList.aspx';" />
                </td>
            </tr>
        </table>
        <div>
            <input type="hidden" id="hidTaskId" runat="server" />
            <input type="hidden" id="hidView" value="0" runat="server" />
            <input type="hidden" id="hidTaskProjectName" runat="server" />
            <input type="hidden" id="hidContent" runat="server" />
            <input type="hidden" id="hidDeptOption" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
