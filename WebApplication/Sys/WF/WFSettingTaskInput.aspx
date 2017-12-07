<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Search.master" AutoEventWireup="True" Inherits="Sys_WF_WFSettingTaskInput" Codebehind="WFSettingTaskInput.aspx.cs" %>

<%@ Register TagPrefix="Webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphData" runat="Server">
    <div class=" l-toolbar">
        <table class="margin8">
            <tr>
                <td class="l-table-edit-td">
                    <div style="text-align: left;">
                        <input type="button" value="流程列表" class="l-button l-button-submit" onclick="javascript:window.location.href='WFSettingFlowList.aspx'"
                            id="btnGoBack" />
                    </div>
                </td>
                <td class="l-table-edit-td">
                </td>
                <td align=" right" class="l-table-edit-td">
                    <asp:Button ID="btnAddTaskDetail" CssClass="l-button l-button-submit" runat="server"
                        OnClick="btnAddTaskDetail_Click" Text="增加环节" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td align=" left" class="l-table-edit-td">
                    <asp:Button ID="btnOrderTask" runat="server" CssClass="l-button l-button-submit"
                        OnClick="btnOrderTask_Click" Text="环节排序" />
                </td>
                <td>
                    <asp:Button ID="btnEdit" runat="server" CssClass="l-button l-button-submit" OnClick="btnEdit_Click"
                        Text="编辑环节" />
                </td>
                <td>
                    <asp:Button ID="btnDeleteTask" runat="server" OnClientClick="javascript:return confirm(' 将删除节点所有信息，确认删除么');"
                        CssClass="l-button l-button-submit" OnClick="btnDeleteTask_Click" Text="删除环节" />
                </td>
                <td>
                    <asp:HiddenField ID="WF_ID" runat="server" />
                </td>
            </tr>
        </table>
    
    </div>
    <asp:GridView ID="grdList" runat="server" CssClass="applisttitle" CellPadding="0"
        DataKeyNames="ID" OnRowCommand="grdList_Command" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="唯一编号" Visible="false" />
            <asp:TemplateField HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="chkbx" OnCheckedChanged="myCheckChanged" AutoPostBack="true" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="WF_ID" HeaderText="流程代码" />
            <asp:BoundField DataField="WF_TASK_ID" HeaderText="节点编码" />
            <asp:BoundField DataField="TASK_CAPTION" HeaderText="节点简称" />
            <asp:TemplateField HeaderText="命令集" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblCMDName" runat="server" Text='<%# GetCMDName(Eval("COMMAND_NAME")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="附加功能" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblFUNList" runat="server" Text='<%# GetFUNCName(Eval("FUNCTION_LIST")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑环节" Visible="false" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="WFSettingTaskInputDetail.aspx?WF_ID=<%#Eval("WF_ID") %>&WF_TASK_ID=<%#Eval("WF_TASK_ID")  %>">
                        编辑环节</a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField Visible="false" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandArgument='<%#Eval("ID") %>'
                        OnClientClick="javascript:return confirm(' 将删除节点所有信息，确认删除么');" CommandName="iDelete"
                        Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <Webdiyer:AspNetPager ID="pager" OnPageChanged="pager_PageChanged" runat="server"
        BackColor="#edf9ff" AlwaysShow="False" ShowCustomInfoSection="Right" Font-Size="13px"
        CurrentPageButtonPosition="Center" PageIndexBoxType="DropDownList" ShowPageIndexBox="Always"
        SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到第"
        FirstPageText="首页" LastPageText="末页" NextPageText="下一页" PrevPageText="上一页" CustomInfoHTML="每页%PageSize%条，共%PageCount%页，<font color='red'><b>%RecordCount%</b></font>条记录"
        PageSize="10">
    </Webdiyer:AspNetPager>
</asp:Content>
