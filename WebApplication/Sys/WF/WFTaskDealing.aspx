<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Search.master" AutoEventWireup="True" Inherits="Sys_WF_WFTaskDealing" Codebehind="WFTaskDealing.aspx.cs" %>

<%@ Register TagPrefix="Webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css"
        rel="stylesheet" type="text/css" />
    <link href="~/Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Controls/AutoComplete/css/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <!--Jquery 基础文件-->
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <!--自动完成-->
    <script src="../../Controls/AutoComplete/jquery.autocomplete.js" type="text/javascript"></script>
    <!--提示-->
    <script src="../../Controls/ligerui/lib/jquery-validation/jquery.validate.min.js"
        type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/jquery-validation/jquery.metadata.js"
        type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <!--LigerUI-->
    <script src="../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerResizable.js"
        type="text/javascript"></script>
    <script src="~/Controls/ligerui/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js"
        type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script type="text/javascript">
        function OpenGoDetail(strId,strHeight) {
            var strUrl = ("WFShowStepDetail.aspx?ID=" + strId);
            $(document).ready(function () { $.ligerDialog.open({ title: '任务追踪', url: strUrl, width: 375, height: strHeight, modal: false }); });
        }

        
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphData" runat="Server">
    
  
    <div style="text-align: left; width: 100%;">
       
        <asp:GridView ID="grdList2" runat="server" CssClass="applisttitle" CellPadding="0"
            DataKeyNames="ID" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="唯一编号" Visible="false" />
                <asp:TemplateField HeaderText="编号" ItemStyle-Width="50px">
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemTemplate>
                        <%# pager2.PageSize*(pager2.CurrentPageIndex-1) + Container.DataItemIndex+1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="WF_STARTTIME" HeaderText="流程启动时间" />
                <asp:BoundField DataField="WF_ID" HeaderText="流程代码" />
                <asp:BoundField DataField="WF_SERVICE_NAME" HeaderText="业务名称" />
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
                <asp:TemplateField HeaderText="任务追踪" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%--                        <a href="WFShowStepDetail.aspx?ID=<%#Eval("ID") %>">追踪</a>--%>
                        <a href="#" onclick="OpenGoDetail('<%#Eval("ID") %>','<%# GetStepHeight(Eval("WF_ID")) %>');">追踪</a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div style=" padding:8px;">
        <Webdiyer:AspNetPager ID="pager2" OnPageChanged="pager2_PageChanged" runat="server"
            BackColor="#edf9ff" AlwaysShow="False" ShowCustomInfoSection="Right" Font-Size="13px"
            CurrentPageButtonPosition="Center" PageIndexBoxType="DropDownList" ShowPageIndexBox="Always"
            SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到第"
            FirstPageText="首页" LastPageText="末页" NextPageText="下一页" PrevPageText="上一页" CustomInfoHTML="每页%PageSize%条，共%PageCount%页，<font color='red'><b>%RecordCount%</b></font>条记录"
            PageSize="10">
        </Webdiyer:AspNetPager>
        </div>
    </div>

    <div>
    </div>
</asp:Content>
