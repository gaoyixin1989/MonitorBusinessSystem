<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Search.master" AutoEventWireup="True" Inherits="Sys_WF_WFTaskClear" Codebehind="WFTaskClear.aspx.cs" %>

<%@ Register TagPrefix="Webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphData" runat="Server">
    <div style="text-align: left;">
        <label>
            待处理任务列表：</label>
        <asp:GridView ID="grdList" runat="server" CssClass="innerTable01" CellPadding="0"
            DataKeyNames="ID" AutoGenerateColumns="False" OnRowCommand="grdList_OnRowCommand">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="唯一编号" Visible="false" />
                <asp:TemplateField HeaderText="编号" ItemStyle-Width="50px">
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemTemplate>
                        <%# pager.PageSize*(pager.CurrentPageIndex-1) + Container.DataItemIndex+1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="WF_ID" HeaderText="流程代码" />
                <asp:BoundField DataField="WF_SERVICE_NAME" HeaderText="业务名称" />
                <asp:BoundField DataField="WF_STARTTIME" HeaderText="流程启动时间" />
                <asp:TemplateField HeaderText="流程简称">
                    <ItemTemplate>
                        <asp:Label ID="lblWF_CAPTION" runat="server" Text='<%# GetWFName(Eval("WF_ID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="环节名称">
                    <ItemTemplate>
                        <asp:Label ID="lblWF_TASK_CAPTION" runat="server" Text='<%# GetTaskName(Eval("WF_TASK_ID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="挂起流程" ItemStyle-HorizontalAlign="Center" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnHold" CommandArgument='<%#Eval("ID") %>' runat="server"
                            CausesValidation="false" OnClientClick="javascript:return confirm('挂起流程后本流程实例将无法流转，你确定要挂起流程么?');"
                            CommandName="cHold" Text="挂起" Visible='<%# GetHoldVisble(Eval("WF_STATE")) %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="恢复流程" ItemStyle-HorizontalAlign="Center" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnNormal" CommandArgument='<%#Eval("ID") %>' runat="server"
                            CausesValidation="false" CommandName="cReNormal" Text="恢复" Visible='<%# GetNormalVisble(Eval("WF_STATE")) %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="销毁流程" ItemStyle-HorizontalAlign="Center" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnKillWF" CommandArgument='<%#Eval("ID") %>' runat="server"
                            CausesValidation="false" CommandName="cKillWF" Text="销毁" OnClientClick="javascript:return confirm('清除任务将清除本流程的所有数据，你确定要清除任务么?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="重置流程" ItemStyle-HorizontalAlign="Center" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnReStartWF" CommandArgument='<%#Eval("ID") %>' runat="server"
                            CausesValidation="false" CommandName="cReStartWF" Text="返元" OnClientClick="javascript:return confirm('返元任务将会把任务转换为最初环节，你确定要返元本任务么?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="任务追踪" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="WFShowStepDetail.aspx?ID=<%#Eval("ID") %>">追踪</a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
    </div>
</asp:Content>
