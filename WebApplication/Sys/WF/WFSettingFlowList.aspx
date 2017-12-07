<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Search.master" AutoEventWireup="True" Inherits="Sys_WF_WFSettingFlowList" Codebehind="WFSettingFlowList.aspx.cs" %>

<%@ Register TagPrefix="Webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphData" runat="Server">
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <div class="l-form l-toolbar" style="height: 28px;">
        <ul class="applistt">
            <li>
                <input id="btnAddNewFlow" value="添加流程" type="button" class="add" onclick="javascript:window.location.href='WFSettingFlowInput.aspx';" /></li>
            <li>
                <asp:Button ID="btnEdit" runat="server" Text="修改流程" OnClick="btnEdit_Click" class="edit" /></li>
            <li>
                <asp:Button ID="btnStep" runat="server" Text="编辑环节" OnClick="btnStep_Click" class="alter" /></li>
        </ul>
    </div>
    <asp:GridView ID="grdList" runat="server" CssClass="applisttitle" CellPadding="0"
        DataKeyNames="ID" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="编号" Visible="false" />
            <asp:TemplateField HeaderText="编号" ItemStyle-Width="50px">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                <ItemTemplate>
                    <%# pager.PageSize * (pager.CurrentPageIndex - 1) + Container.DataItemIndex + 1%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="chkbx" OnCheckedChanged="myCheckChanged" AutoPostBack="true" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="WF_ID" HeaderText="流程代码" />
            <asp:BoundField DataField="WF_CAPTION" HeaderText="流程简称" />
            <asp:TemplateField HeaderText="所属分类">
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# GetClassName(Eval("WF_CLASS_ID")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作时间">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetLastTime(Eval("CREATE_DATE"),Eval("DEAL_DATE")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="修改内容" Visible="false" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="WFSettingFlowInput.aspx?ID=<%#Eval("ID") %>">修改内容</a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑环节" Visible="false" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="WFSettingTaskInput.aspx?WF_ID=<%#Eval("WF_ID") %>">编辑环节</a>
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
     
</asp:Content>
