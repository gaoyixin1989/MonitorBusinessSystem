<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Input.master" AutoEventWireup="True" Inherits="Sys_WF_WFSettingTaskOrderDetail" Codebehind="WFSettingTaskOrderDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphInput" runat="Server">
    <div  class="l-toolbar" style="text-align: left;">
        <table>
            <tr>
                <td class="l-table-edit-td">
                    <input type="button" value="流程列表" onclick="javascript:window.location.href='WFSettingFlowList.aspx'"
                        id="btnGoBack"  class="l-table-edit-td"/>
                </td>
                <td>
                    <asp:Button ID="btnGoto" runat="server" OnClick="btnGoto_Click" Text="环节列表" class="l-table-edit-td" />
                </td>
                <td>
                    <asp:Button ID="btnAddTaskDetail" runat="server" OnClick="btnAddTaskDetail_Click"
                        Text="增加环节3333" class="l-table-edit-td" />
                </td>
                <td>
                    <asp:Button ID="btnUpTo" runat="server" OnClick="btnUpTo_Click" Text="提前一步" class="l-table-edit-td" />
                </td>
                <td>
                    <asp:Button ID="btnDownTo" runat="server" OnClick="btnDownTo_Click" Text="后退一步" class="l-table-edit-td" />
                </td>
                <td>
                    <asp:HiddenField ID="WF_ID" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCurFlowName" Visible="false" runat="server"></asp:Label>
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
            <asp:BoundField DataField="TASK_ORDER" HeaderText="执行顺序" />
            <asp:TemplateField ShowHeader="False" Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkbtnUp" CommandArgument='<%#Eval("ID") %>' runat="server" CausesValidation="false"
                        CommandName="iUp" Text="向前"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkbtnDown" CommandArgument='<%#Eval("ID") %>' runat="server"
                        CausesValidation="false" CommandName="iDown" Text="往后"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
