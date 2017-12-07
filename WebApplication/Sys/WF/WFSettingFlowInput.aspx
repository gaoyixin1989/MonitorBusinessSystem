<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Input.master" AutoEventWireup="True" Inherits="Sys_WF_WFSettingFlowInput" Codebehind="WFSettingFlowInput.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphInput" runat="Server">
    <div class="rightBox">
        <div class="rightMenu">
        </div>
        <!-- class:rightMenu //-->
        <div class="rightMain">
            <div class="tableListBox">
                <div class="CtrlTitle">
                    <ul class="right ctrlTab">
                        <li class="selet"><a href="#"><span class="s"></span></a></li>
                        <li><a href="#"><span class="n"></span></a></li>
                        <li><a href="#"><span class="c"></span></a></li>
                    </ul>
                    <div class="clear">
                    </div>
                </div>
                <!-- class:CtrlTitle //-->
                <div class="editInnerBox" id="editInnerBox">
                    <div class="tableBox">
                        <div class="tableInnerBox" id="tableInnerBox1">
                        <div class="tableh2"><i></i>工作流配置</div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tablelist2012" style=" width:600px; margin: auto 10px;">
                                  <tr>
                                    <th class="ww">
                                        工作流简称:
                                    </th>
                                    <td align="left" colspan="6">
                                        <asp:TextBox ID="WF_CAPTION" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        工作流代号:
                                    </th>
                                    <td align="left" colspan="6">
                                        <asp:TextBox ID="WF_ID" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        类别归属:
                                    </th>
                                    <td align="left" colspan="6">
                                        <asp:DropDownList ID="WF_CLASS_ID" Width="120px" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        工作流描述:
                                    </th>
                                    <td align="left" colspan="6">
                                        <asp:TextBox ID="WF_NOTE" Width="250px" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        首环节转向页面:
                                    </th>
                                    <td align="left" colspan="6">
                                        <asp:TextBox ID="FSTEP_RETURN_URL" Width="250px" runat="server">
                                        </asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        备注:
                                    </th>
                                    <td align="left" colspan="6">
                                        <asp:TextBox ID="REMARK" TextMode="MultiLine" Width="250px" Height="50px" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
 
                            </table>
                            <div class="tablefooter">
                            <ul>
                             <li><asp:Button ID="btnSave" runat="server" Text="提交" CssClass="l-button l-button-submit"
                                            OnClick="btnSave_Click"/></li>
                                         <li>   <input id="btnToList" type="button" class="l-button l-button-submit" onclick="javascript:window.location.href='WFSettingFlowList.aspx'"
                                            value="返回" /><asp:HiddenField ID="ID" runat="server" /></li></ul></div>
                        </div>
                    </div>
                    <!-- class:tableBox //-->
                </div>
                <!-- class:editInnerBox //-->
            </div>
            <!-- class:editBox //-->
        </div>
        <!-- class:rightMain //-->
    </div>
    <!-- class:rightBox //-->
</asp:Content>
