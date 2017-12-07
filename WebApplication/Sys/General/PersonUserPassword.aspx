<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/InputEx.master" AutoEventWireup="True" Inherits="Sys_General_PersonUserPassword" Codebehind="PersonUserPassword.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        body{ font-size:12px;}
        .l-table-edit {}
        .l-table-edit-td{ padding:4px;}
        .l-button-submit,.l-button-test{width:80px; float:left; margin-left:10px; padding-bottom:2px;}
        .l-verify-tip{ left:230px; top:120px;}
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphInput" runat="Server">
    <div class="pall">
        <table class="tablelist2012" style="width:600px;">
            <tr>
                <th style=" width:80px;">
                    登录名：
                </th>
                <td align="left" class="l-table-edit-td">
                    <asp:Label ID="USER_NAME" runat="server" Text="" CssClass="label"></asp:Label>
                    <asp:Label ID="ID" runat="server" Visible="false" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    姓名：
                </th>
                <td align="left" class="l-table-edit-td">
                    <asp:Label ID="REAL_NAME" runat="server" Text="" CssClass="label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    原密码：
                </th>
                <td colspan="3" align="left" class="l-table-edit-td">
                    <asp:TextBox ID="USER_PWD_ORG" TextMode="Password" runat="server" Width="150" class="ipt01"
                        validate="[{required:true, msg:'请输入原密码'},{minlength:2,maxlength:16,msg:'录入最小长度为2，最大长度为16'}]"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    新密码：
                </th>
                <td align="left" class="l-table-edit-td">
                    <asp:TextBox ID="USER_PWD" TextMode="Password" runat="server" Width="150" class="ipt01"
                        validate="[{required:true, msg:'请输入新密码'},{minlength:2,maxlength:16,msg:'录入最小长度为2，最大长度为16'}]">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    确认新密码：
                </th>
                <%--<td align="left" class="l-table-edit-td">
                    <asp:TextBox ID="USER_PWD_CONFIRM" TextMode="Password" runat="server" Width="150"
                        class="ipt01" validate="[{required:true, msg:'请确认新密码'},{minlength:2,maxlength:16,msg:'录入最小长度为2，最大长度为16'}]">
                    </asp:TextBox>
                </td>--%>
                <td align="left" class="l-table-edit-td">
                    <asp:TextBox ID="USER_PWD_CONFIRM" TextMode="Password" runat="server" Width="150" >
                    </asp:TextBox>
                    <asp:Label ID="lbMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <div  style=" padding-left:90px; height:30px;">       <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn03" Text="保存" OnClientClick="return $('#form1').validate()"></asp:Button>
                    <asp:Button ID="btnRef" runat="server" CssClass="btn03" Text="重置" CausesValidation="false"></asp:Button></div>
    </div>
</asp:Content>
