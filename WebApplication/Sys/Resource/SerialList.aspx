<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Search.master" AutoEventWireup="True" Inherits="Sys_Resource_SerialList" Codebehind="SerialList.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%--<%@ Register TagPrefix="uc1" TagName="MenuCtrl" Src="~/Controls/UserControls/MenuControl.ascx" %>--%>
<asp:Content ID="Content4" ContentPlaceHolderID="cphData" runat="Server">
    <script type="text/javascript">
        //新增
        function AddNew() {
            window.location.href = "SerialEdit.aspx";
        }
        //编辑
        function Edit() {
            if (chkEdit()) {
                window.location.href = "SerialEdit.aspx" + "?id=" + document.getElementById("cphData_txtHidden").value;
            }
        }
        //删除
        function Delete() {
            if (chkDelete()) {
                if (confirm("确实要删除这些数据吗？"))
                    return true;

                else
                    return false;
            }
            return false;
        }
    </script>

    <div runat="server" id="divsearch" >

            <table style=" margin:0 8px;">
            <tr>
                <td>
                    名称：
                </td>
                <td>
                    <asp:TextBox ID="txtSerialName" runat="server" Width="150" class="input"></asp:TextBox>
                </td>
                <td>
                <asp:Button ID="btnSearch" runat="server" Text="检 索" CssClass="btn03" Style=" margin:0;" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
            <p class="ctrlBtn ctrlBtnleft">
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="lk_del" onfocus="this.blur()"
                OnClientClick="return Delete();"  OnClick="btnDelete_Click">删除</asp:LinkButton>

            <a class="lk_edit" href="#" onfocus="this.blur()" onclick="Edit();">编辑</a>
            <a class="lk_add" href="#" onfocus="this.blur()" onclick="AddNew();">新增</a>
        </p>
    <asp:GridView ID="grdList" class="applisttitle" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
        OnRowDeleting="grdList_RowDeleting">
        <Columns>
           <asp:TemplateField HeaderText="&lt;input name='chk' type='checkbox'onclick='chkSelectAll(this);' value='0'&gt;">
             <ItemTemplate>
            <input type="checkbox" name="chk" value='<%# DataBinder.Eval(Container.DataItem,"ID")%>'>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Width="30">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# (this.pager.CurrentPageIndex-1)  * this.pager.PageSize + this.grdList.Rows.Count + 1%>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField HeaderText="ID" DataField="ID" Visible="false" />
            <asp:BoundField HeaderText="名称" DataField="SERIAL_NAME" />
            <asp:BoundField HeaderText="编码" DataField="SERIAL_CODE" />
            <asp:BoundField HeaderText="序列号" DataField="SERIAL_NUMBER" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="长度" DataField="LENGTH" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="粒度" DataField="GRANULARITY" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="最大值" DataField="MAX" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="最小值" DataField="MIN" ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>
    <div style=" text-align:left; padding:8px;">
    <webdiyer:AspNetPager ID="pager" OnPageChanged="pager_PageChanged" runat="server"
        BackColor="#edf9ff" AlwaysShow="False" ShowCustomInfoSection="Right" Font-Size="13px"
        CurrentPageButtonPosition="Center" PageIndexBoxType="DropDownList" ShowPageIndexBox="Always"
        SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到第"
        FirstPageText="首页" LastPageText="末页" NextPageText="下一页" PrevPageText="上一页" CustomInfoHTML="每页%PageSize%条，共%PageCount%页，<font color='red'><b>%RecordCount%</b></font>条记录">
    </webdiyer:AspNetPager>
    <asp:HiddenField ID="txtHidden" runat="server" />
    </div>
        </div>
</asp:Content>

