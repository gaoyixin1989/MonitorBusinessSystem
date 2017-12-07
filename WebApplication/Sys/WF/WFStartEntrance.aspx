<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Search.master" AutoEventWireup="True" Inherits="Sys_WF_WFStartEntrance" Codebehind="WFStartEntrance.aspx.cs" %>

<%@ Register TagPrefix="Webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphData" runat="Server">
    <asp:GridView ID="grdList" runat="server" CssClass="applisttitle" CellPadding="0"
        DataKeyNames="ID" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="唯一编号" Visible="false" />
            <asp:BoundField DataField="WF_ID" HeaderText="流程代码" />
            <asp:BoundField DataField="WF_CAPTION" HeaderText="流程简称" />
            <asp:TemplateField HeaderText="所属分类">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetClassName(Eval("WF_CLASS_ID")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作时间">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetLastTime(Eval("CREATE_DATE"),Eval("DEAL_DATE")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="启动流程" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="WFStartPage.aspx?WF_ID=<%#Eval("WF_ID") %>">启动流程</a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="padding:8px; text-align:left;">
    <Webdiyer:AspNetPager ID="pager" OnPageChanged="pager_PageChanged" runat="server"
        BackColor="#edf9ff" AlwaysShow="False" ShowCustomInfoSection="Right" Font-Size="13px"
        CurrentPageButtonPosition="Center" PageIndexBoxType="DropDownList" ShowPageIndexBox="Always"
        SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到第"
        FirstPageText="首页" LastPageText="末页" NextPageText="下一页" PrevPageText="上一页" CustomInfoHTML="每页%PageSize%条，共%PageCount%页，<font color='red'><b>%RecordCount%</b></font>条记录"
        PageSize="10">
    </Webdiyer:AspNetPager>
    </div>
</asp:Content>
