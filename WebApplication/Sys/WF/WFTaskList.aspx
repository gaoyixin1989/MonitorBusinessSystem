<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Search.master" AutoEventWireup="True" Inherits="Sys_WF_WFTaskList" Codebehind="WFTaskList.aspx.cs" %>

<%@ Register TagPrefix="Webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <script type="text/javascript">
        $(document).ready(function () {
        })

        function TaskOpen(strId, isbool) {
            var surl = "", urltitle = "", tabid = "";
            tabid = isbool ? "tabidTaskOpenNew" : "tabidTaskOpenNew_Step";
            surl = isbool ? "../SYS/WF/WFDealPage.aspx" : "../SYS/WF/WFShowStepDetail.aspx";
            surl += "?ID=" + strId + "";
            urltitle = isbool ? "业务办理" : "业务追踪";
            top.f_addTab(tabid, urltitle, surl);
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphData" runat="Server">
    <div style="text-align: left; width: 100%; height: 190px;">
        <asp:GridView ID="grdList" runat="server" CssClass="applisttitle" CellPadding="0"
            DataKeyNames="ID" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="唯一编号" Visible="false" />
             
                <asp:TemplateField HeaderText="编号" ItemStyle-Width="50px" >
                    <ItemStyle HorizontalAlign="center" Wrap="false" />
                    <ItemTemplate>
                        <%# pager1.PageSize*(pager1.CurrentPageIndex-1) + Container.DataItemIndex+1%>
                    </ItemTemplate>
                </asp:TemplateField>
              
                <asp:BoundField DataField="WF_SERVICE_NAME" HeaderText="任务名称" ItemStyle-Width="50%" />
                <asp:TemplateField HeaderText="流程简称" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblWF_CAPTION" runat="server" Text='<%# GetWFName(Eval("WF_ID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="当前环节"  ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblWF_TASK_CAPTION" runat="server" Text='<%# GetTaskName(Eval("WF_TASK_ID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发送人"  ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# GetUserName(Eval("SRC_USER")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="INST_TASK_STARTTIME" HeaderText="发送时间"  ItemStyle-Width="20%" />
                <asp:TemplateField HeaderText="办理" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%--   <a href="WFDealPage.aspx?ID=<%#Eval("ID") %>">办理</a>--%>
                        <a href="#" onclick="TaskOpen('<%#Eval("ID") %>',true);">办理</a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div style="padding:8px;">
        <Webdiyer:AspNetPager ID="pager1" OnPageChanged="pager1_PageChanged" runat="server"
            BackColor="#edf9ff" AlwaysShow="False" ShowCustomInfoSection="Right" Font-Size="13px"
            CurrentPageButtonPosition="Center" PageIndexBoxType="DropDownList" ShowPageIndexBox="Always"
            SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到第"
            FirstPageText="首页" LastPageText="末页" NextPageText="下一页" PrevPageText="上一页" CustomInfoHTML="每页%PageSize%条，共%PageCount%页，<font color='red'><b>%RecordCount%</b></font>条记录"
            PageSize="10">
        </Webdiyer:AspNetPager>
        </div>
    </div>
    <br />
    
</asp:Content>
