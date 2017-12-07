<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Search.master" AutoEventWireup="True" Inherits="Sys_WF_WFTaskHistory" Codebehind="WFTaskHistory.aspx.cs" %>

<%@ Register TagPrefix="Webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphData" runat="Server">
    <div style="text-align: left;">
        <label>
            已办理任务列表：</label>
        <asp:GridView ID="grdDownList" runat="server" CssClass="innerTable01" CellPadding="0"
            DataKeyNames="ID" AutoGenerateColumns="False">
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
                <asp:BoundField DataField="INST_TASK_STARTTIME" HeaderText="产生时间" />
                <asp:BoundField DataField="INST_TASK_ENDTIME" HeaderText="完成时间" />
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
                <asp:TemplateField HeaderText="查看历史" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="WFViewHistroyPage.aspx?WF_SERIAL_NO=<%#Eval("WF_SERIAL_NO") %>">查看历史</a>
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
