<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_MisII_Report_ReportCheck" Codebehind="ReportCheck.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <!--Jquery 基础文件-->
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <!--自动完成-->
    <script src="../../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--提示-->
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <!--LigerUI-->
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="ReportCheck.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#navtab1").ligerTab({ contextmenu: false });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input id="ID" runat="server" type="hidden" />
    <input id="reportId" runat="server" type="hidden" />
    <input id="ContractID" runat="server" type="hidden" />
    <input id="ReportStatus" runat="server" type="hidden" />
     <input id="btnType" runat="server" type="hidden" value="send" />
    <div id="navtab1" style="width: 98%; overflow: hidden; border: 1px solid #A3C0E8;">
        <div tabid="home" title="报告生成" lselected="true" style="height: 100%">
            <table style="width: 100%">
                <tr>
                    <td colspan="100%" style="clear: both; padding-bottom: 7px;">
                        <div class="tableh2">
                            <img src="../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />委托书信息</div>
                        <div id="contractDiv">
                        </div>
                    </td>
                </tr>
            </table>
        
            <div class="l-form">
                <div class="tableh2">
                    <img src="../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />
                    报告
                </div>
                <ul>
                    <li style="padding-left: 7px; padding-right: 10px;" class="divfloat">
                        <input id="dropReport" type="text" style="display: none" /></li>
                    <li>
                        <input type="button" value="报告生成" id="btn_Ok" name="btn_Ok" class="l-button l-button-submit"
                            onclick="ReportClick();" />
                        <input type="button" value="验收报告" id="btnAccept" class="l-button l-button-submit"
                            style="display: none" onclick="downLoad();" /></li>
                </ul>
            </div>
            
        </div>
         <div title="监测信息">
            <table style="width: 100%">
                <tr>
                    <td colspan="100%" style="clear: both; padding-bottom: 7px;">
                        <div class="tableh2">
                            <img src="../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />委托单位</div>
                        <div id="clientDiv">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="100%" style="clear: both; padding-bottom: 7px;">
                        <div class="tableh2">
                            <img src="../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />受检单位</div>
                        <div id="testedDiv">
                        </div>
                    </td>
                </tr>
            </table>
            <div id="divPointItem">
                <div class="tableh2">
                    <img src="../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" />
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
        </div>
    </div>
    </form>
</body>
</html>
