<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/InputEx.master" AutoEventWireup="True" Inherits="Sys_WF_WFTaskDetailView" Codebehind="WFTaskDetailView.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphInput" runat="Server">
    <div>
        <table>
            <tr>
                <td colspan="2">
                    流程概述
                </td>
            </tr>
            <tr>
                <td>
                    流程名称
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    启动时间
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    处理状态
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    当前所在环节
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    流程详述
                </td>
            </tr>
             <tr>
                <td>
                    上一环节
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    本环节启动时间
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    本环节认领信息
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    本环节完成信息
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                </td>
            </tr>
           
        </table>
    </div>
</asp:Content>
