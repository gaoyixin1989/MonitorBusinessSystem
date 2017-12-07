<%@ Page Language="C#" MasterPageFile="~/Masters/SearchEx.master" AutoEventWireup="True" Inherits="Sys_Duty_ResultDutySetting_ForItem" Codebehind="ResultDutySetting_ForItem.aspx.cs" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js"  type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"  type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/jquery-validation/validate.js" type="text/javascript"></script>
    <script src="ResultDutySetting_ForItem.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphData" runat="Server">
    <div id="layout1" style="text-align: left">
        <div position="left" title="监测项目列表">
            <div id="maingrid"> </div>
        </div>
        <div position="right" title="用户列表">
            <div id="divItemUserLst" ></div>
        </div>
    </div>
    <div id="detailSrh" style="display: none;">
        <form id="searchItemForm" method="post"></form>
    </div>

    <div id="targerdiv2" class="l-form"  style="width:350px; margin:3px; display:none;" >
        <ul>
            <li  style="width:37px; text-align:left;"> 部门： </li>
            <li  style="width:240px;text-align:left;">
                <input id="Dept" class="l-text l-text-editing" name="Dept" type="text" />
            </li>
            <li>
                 <table cellpadding="0" cellspacing="0" class="tabletool">
                    <tr>
                        <td align="center" colspan="2"  >
                            <b>未选用户</b>
                            <select size="10"   name="listLeft" multiple="multiple" id="listLeft"    title="双击可实现右移" class="searchb"  style=" width:170px;height:250px"> </select> 
                        </td>
                        <td align="center">&nbsp;</td>
                        <td align="center" >
                            <input type="button" id="btnRight" value=">>"  class="l-button l-button-submit"/><br />
                            <input type="button" id="btnLeft" value="<<" class="l-button l-button-submit"/>
                        </td>
                        <td  align="center" class="l-table-edit-td">&nbsp;</td>
                        <td align="center"  class="l-table-edit-td"> 
                            <b>已选用户</b>
                            <select size="10" multiple="multiple" name="listRight" id="listRight"    title="双击可实现左移"  class="searchb" style=" width:170px;height:250px" ></select>
                        </td>
                    </tr>
                </table>
            </li>
        </ul>
    </div>
</asp:Content>
