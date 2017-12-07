<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Search.master" AutoEventWireup="True" Inherits="Sys_WF_WFDeletePage" Codebehind="WFDeletePage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphData" runat="Server">
    <div>
        <table>
            <tr>
                <td>
                    <div id="dvWFPic" style="width: 400px;">
                        <span id="spShowPic" style="text-align: left; width: 100%;" runat="server"></span>
                    </div>
                </td>
                <td>
                    <div id="dvTreeView">
                    </div>
                    <div id="dvOperate">
                    </div>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdID" runat="server" />
        <asp:HiddenField ID="hdType" runat="server" />
    </div>
</asp:Content>
