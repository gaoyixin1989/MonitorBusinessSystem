<%@ Page Language="C#" MasterPageFile="~/Masters/SearchEx.master"  AutoEventWireup="True" Inherits="Channels_Mis_Monitor_sampling_QY_SamplingAllocationSheet" Codebehind="SamplingAllocationSheet.aspx.cs" %>


<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js"
        type="text/javascript"></script>
    <script src="../../../../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script src="../../../../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="../../../../../Scripts/comm.js" type="text/javascript"></script>
    <script src="SamplingAllocationSheet.js" type="text/javascript"></script>
    <script type="text/javascript">
        var topHeight, bottomHeight;

        $(function () {
            topHeight = 2 * $(window).height() / 5 - 35;
            bottomHeight = 3 * $(window).height() / 5 - 35;

            $("#layout1").ligerLayout({ topHeight: 2 * $(window).height() / 5, leftWidth: 200, allowLeftCollapse: false, height: '100%' });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <form id="form1" runat="server">
    <div id="layout1">
        <div position="top">
            <div id="oneGrid">
            </div>
        </div>
        <div position="left" title="监测类别">
            <div id="twoGrid">
            </div>
        </div>
        <div position="center" title="监测样品">
            <div id="threeGrid">
            </div>
        </div>
    </div>
    <div id="divSugg" style="display:none;">
        <div id="SuggForm"></div>
    </div>
    <!--质控监测项目设置-->
    <div id="targerdiv" class="l-form" style="display: none;">
        <div style="float: left;" class="l-group l-group-hasicon">
            <img src="../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif" /><span>质控项目设置 - <asp:Label ID="PointName" runat="server"></asp:Label></span>
        </div>
        <ul>
            <li>
                <table class="tblMain">
                    <tr>
                        <td style="width: 13%" align="center" valign="top" rowspan="5">
                            <b>
                                
                                监测项目</b><br />
                            <select size="20" name="listLeft" multiple="multiple" id="listLeft" title="双击可实现左移"
                                style="width: 170px; height: 250px">
                            </select>
                        </td>
                        <td valign="top" style="width: 8%" rowspan="5">
                            &nbsp;
                        </td>
                        <td valign="top" style="width: 8%" rowspan="5">
                            <br />
                            <br />
                            <br />
                            <br />
                            <input type="button" id="btnQc4Right" name="btnQc4Right" value=">>" class="l-button l-button-submit" /><br />
                            <input type="button" id="btnQc4Left" name="btnQc4Left" value="<<" class="l-button l-button-submit" />
                        </td>
                        <td valign="top" style="width: 8%" rowspan="5">
                            &nbsp;
                        </td>
                        <td class="w01" valign="bottom" width="40%" rowspan="5">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" width="60%">
                            <b style="text-align: center">密码平行</b>
                            质控数量：<asp:DropDownList ID="listQc4Count" runat="server">
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <select size="8" name="listQc4" id="listQc4" multiple="multiple" title="双击可实现移出"
                                style="width: 170px; height: 250px;">
                            </select>
                        </td>
                    </tr>
                </table>
            </li>
        </ul>
    </div>
               <div id="detailRemark" style="display:none;">
    <div id="RemarkForm"></div>
    </div>
    <asp:HiddenField ID="strPrintId" runat="server" />
    <asp:HiddenField ID="strPrintId_code" runat="server" />
    <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" Style="display: none;" />
    <asp:Button ID="btnImportCode" runat="server" OnClick="btnImportCode_Click" Style="display: none;" />
    </form>
</asp:Content>