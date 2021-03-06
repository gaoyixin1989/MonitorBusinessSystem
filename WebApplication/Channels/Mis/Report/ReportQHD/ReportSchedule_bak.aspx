﻿<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Mis_Report_ReportQHD_ReportSchedule_bak" Codebehind="ReportSchedule_bak.aspx.cs" %>

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
    <script src="ReportSchedule_bak.js" type="text/javascript"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <input id="ID" runat="server" type="hidden" />
    <input id="reportId" runat="server" type="hidden" />
    <input id="ContractID" runat="server" type="hidden" />
    <div id="layout1">
        <table style="width: 100%">
            <tr>
                <td colspan="100%" style="clear: both; padding-bottom: 7px;">
                    <div class="tableh2">
                        <img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />委托书信息</div>
                    <div id="contractDiv">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="100%" style="clear: both; padding-bottom: 7px;">
                    <div class="tableh2">
                        <img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />委托单位</div>
                    <div id="clientDiv">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="100%" style="clear: both; padding-bottom: 7px;">
                    <div class="tableh2">
                        <img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />受检单位</div>
                    <div id="testedDiv">
                    </div>
                </td>
            </tr>
        </table>
        <div id="divPointItem">
            <div class="tableh2">
                <img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />
                样品项目
            </div>
            <div style="margin-left: 7px; padding-left: 5px;">
                <table style="margin-bottom: 3px;">
                    <tr>
                        <td align="right">
                            监测类别：
                        </td>
                        <td align="left">
                            <input id="dropItemType" type="text" />
                        </td>
                        <td align="right">
                            &nbsp; &nbsp;点位图下载：
                        </td>
                        <td align="left">
                            <a id="btnDownLoad" href="#" onclick="btnDownLoad_Click();">噪声点位图</a>
                        </td>
                    </tr>
                </table>
            </div>
            <div position="bottom" style="padding: 12px;">
                <div id="divSample" style="float: left; width: 30%;">
                    <div id="sampleDiv">
                    </div>
                </div>
                <div style="float: right; width: 70%">
                    <div id="itemDiv">
                    </div>
                </div>
            </div>
        </div>
        <div class="l-form">
            <div class="tableh2">
                <img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />
                报告管理
            </div>
            <div style="height: 20px; clear: both; margin-left: 40px;">
                <a onclick="upload();" style="cursor: pointer" href="#">报告上传</a> <a onclick="downLoad();"
                    style="cursor: pointer" href="#">报告下载</a>
            </div>
            
        </div>
        <div class="l-form" style="padding: 10px 10px 10px 10px; vertical-align: top; text-align: center">
            <input id="btnSave" type="button" onclick="SendSave();" value="完成" class="l-button l-button-submit"
                style="display: inline;" />
        </div>
    </div>
    </form>
</body>
</html>

