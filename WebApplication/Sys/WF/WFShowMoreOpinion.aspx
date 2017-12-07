<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Search.master" AutoEventWireup="True" Inherits="Sys_WF_WFShowMoreOpinion" Codebehind="WFShowMoreOpinion.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="~/Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="~/Controls/ligerui/lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet"
        type="text/css" />
    <script src="~/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="~/Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="~/Controls/ligerui/lib/json2.js" type="text/javascript"></script>
    <script src="~/Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="~/Controls/ligerui/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="~/Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="~/Controls/ligerui/lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphData" runat="Server">
    <div style="text-align: center;">
        本流程全部评论
        <asp:HiddenField ID="WF_INST_ID" runat="server" />
        <asp:HiddenField ID="WF_INST_TASK_ID" runat="server" />
    </div>
    <div>
        <table style="text-align:center;">
            <tr>
                <td style="text-align: right;">
                    流程名称：
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="lblWFName" runat="server" Text=""></asp:Label>
                </td>
                <td style="text-align: right;">
                    当前环节：
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="lblStepName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    业务单号：
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="lblServiceCode" runat="server" Text=""></asp:Label>
                </td>
                <td style="text-align: right;">
                    产生时间：
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="lblCreateTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: left;">
                    <asp:DataList ID="DataList1" HorizontalAlign="Left" Width="100%" runat="server">
                        <ItemTemplate>
                            <div>
                                <%# GetUserName(Eval("WF_IT_OPINION_USER"))%>[<%# Eval("WF_IT_OPINION_TIME")%>]：<%# Eval("WF_IT_OPINION") %> </div>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td>
                    评论内容：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtOpinionText" runat="server" Rows="3" TextMode="MultiLine" Width="400px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center;">
                    <asp:Button ID="btnAddOpinion" runat="server" CssClass="l-text l-text-editing" Text="评论"
                        OnClick="btnAddOpinion_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div>
    </div>
</asp:Content>
