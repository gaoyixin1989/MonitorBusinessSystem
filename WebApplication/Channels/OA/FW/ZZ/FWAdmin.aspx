<%@ Page Language="C#" AutoEventWireup="True" Inherits="n19.Channels_OA_FW_QHD_FWAdmin" Codebehind="FWAdmin.aspx.cs" %>

<%@ Register TagPrefix="UC" TagName="WFControl" Src="~/Sys/WF/UCWFControls.ascx" %>
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
        <script src="../../../../Scripts/comm.js"  type="text/javascript"></script>
    <script src="FWAdmin.js" type="text/javascript"></script>
    <style type="text/css">
        .l-minheight div
        {
            height: 90px;
            overflow: hidden;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h1 class="h12013">
        发 文 处 理 流 程</h1>
    <div class="tablelayout">
        <div class="tabletop ">
            编号：<asp:TextBox ID="YWNO" runat="server" Text="" Enabled="false"></asp:TextBox><p> 
                日期：<asp:Label ID="FW_DATE" runat="server"></asp:Label>
            </p>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" class="tableedit mlr8">
            <tr>
                <th>
                    文件标题
                </th>
                <td colspan="5">
                    <asp:TextBox ID="FW_TITLE" runat="server" Text="" class="tableedit_title"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    附件
                </th>
                <td align="left" colspan="5">
                    <div id="divDownLoad">
                    <%--    <a id="btnFiledownLoad" href="#">点击查看下载附件</a> --%>
                        <a id="btnFileUp" runat="server" href="javascript:"> 
                            点击上传</a>
                    </div>
                </td>
            </tr>
            <tr>
                <th>
                    主办单位
                </th>
                <td>
                    <asp:TextBox ID="ZB_DEPT" runat="server" Text="" Style="width: 240px;"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 55px;">
                    拟搞人
                </td>
                <td style="width: 80px;">
                    <asp:Label ID="DRAFT_NAME" runat="server"></asp:Label>
                </td>
                <td style="text-align: right; width: 55px;">
                    密级
                </td>
                <td>
                    <asp:DropDownList ID="MJ" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    主送单位
                </th>
                <td colspan="5">
                    <asp:TextBox ID="ZS_DEPT" runat="server" Text="" class="tableedit_title"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    抄报单位
                </th>
                <td>
                    <asp:TextBox ID="CB_DEPT" runat="server" Text="" Style="width: 240px;"></asp:TextBox>
                </td>
                <td style="text-align: right;">
                    抄送单位
                </td>
                <td colspan="3">
                    <asp:TextBox ID="CS_DEPT" runat="server" Text="" Style="width: 240px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    说明
                </th>
                <td colspan="5">
                    <asp:TextBox ID="REMARK1" runat="server" Text="" class="tableedit_title"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    主题词
                </th>
                <td colspan="5">
                    <asp:TextBox ID="SUBJECT_WORD" runat="server" Text="" class="tableedit_title"></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
                <th>
                    办理期限
                </th>
                <td>
                    <asp:TextBox ID="START_DATE" runat="server" Text=""></asp:TextBox>
                    至
                    <div style="margin-left: 140px; margin-top: -20px; clear: right;">
                        <asp:TextBox ID="END_DATE" runat="server" Text=""> </asp:TextBox></div>
                </td>
                <td style="text-align: right;">
                    发文字号
                </td>
                <td colspan="3">
                    <asp:TextBox ID="FWNO" runat="server" Text="" Style="width: 270px;"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <th rowspan="2">
                    办公室审核
                </th>
                <td colspan="5">
                    <textarea cols="100" rows="8" class="l-textarea" id="APP_INFO" runat="server" style="padding: 2px;
                        margin-left: 0; line-height: 20px;" name="S1"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="text-align: right;">
                    <span>办理人：<asp:Label ID="APP_ID" runat="server" Text=""></asp:Label></span> <span>办理时间：<asp:Label
                        ID="APP_DATE" runat="server" Text=""></asp:Label></span>
                </td>
            </tr>
            <tr>
                <th rowspan="2">
                    分管领导审核
                </th>
                <td colspan="5">
                    <textarea cols="100" rows="8" class="l-textarea" id="CTS_INFO" runat="server" style="padding: 2px;
                        margin-left: 0; line-height: 20px;" name="S1"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="text-align: right;">
                    <span>办理人：<asp:Label ID="CTS_ID" runat="server" Text=""></asp:Label></span> <span>办理时间：<asp:Label
                        ID="CTS_DATE" runat="server" Text=""></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <th rowspan="2">
                    主要领导签发
                </th>
                <td colspan="5">
                    <textarea cols="100" rows="8" class="l-textarea" id="ISSUE_INFO" runat="server" style="padding: 2px;
                        margin-left: 0; line-height: 20px;" name="S1"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="text-align: right;">
                    <span>办理人：<asp:Label ID="ISSUE_ID" runat="server" Text=""></asp:Label></span> <span>
                        办理时间：<asp:Label ID="ISSUE_DATE" runat="server" Text=""></asp:Label>
                    </span>
                </td>
            </tr>
        </table>
        <div id="divContratSubmit" runat="server">
        <UC:WFControl ID="wfControl" EnableViewState="true" runat="server" />                 
        </div>
        <!--huangjinjun 20140509-->
        <div id="divBack" runat="server" class="l-form listfooter2012">
            <asp:Button ID="Button1" Text="打印" runat="server" class="l-button l-button-submit" onclick="btn_Print_Click" OnClientClick="return btnPrint();"/>
            <!--<input type="button" value="返回" id="btn_Back" name="btn_Back" class="l-button l-button-submit" style="display: inline-block; margin-top: 8px;" />-->     
        </div>
        <%--end--%>
        <div>
            <input type="hidden" id="hidTaskId" runat="server" />
            <input type="hidden" id="hidView" value="0" runat="server" />
            <input type="hidden" id="hidTaskProjectName" runat="server" />
            <input type="hidden" id="hidContent" runat="server" />
            <input type="hidden" id="hidDeptOption" runat="server" />
            <input type="hidden" id="hid_FwId" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
