<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Input.master" AutoEventWireup="True" Inherits="Sys_Resource_SerialEdit" Codebehind="SerialEdit.aspx.cs" %>

<%@ Register Assembly="ZLTextBox" Namespace="BaseText" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphInput" runat="Server">
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-validate/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-validate/jquery.metadata.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-validate/messages_cn.js" type="text/javascript"></script>
    </script>
<div class="innerCon01">

    <table width="400px" border="0" cellspacing="1" cellpadding="0">
        <tr>
            <th>
                编码：
            </th>
            <td>
                <asp:TextBox ID="SERIAL_CODE" runat="server" Width="150" class="ipt01" validate="[{required:true, msg:'请输入编码'},{minlength:2,maxlength:32,msg:'录入最小长度为2，最大长度为32'}]"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                名称：
            </th>
            <td>
                <asp:TextBox ID="SERIAL_NAME" runat="server" Width="150" class="ipt01" validate="[{required:true, msg:'请输入名称'},{minlength:2,maxlength:32,msg:'录入最小长度为2，最大长度为32'}]"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                序列号：
            </th>
            <td>
                <cc1:ZLTextBox ID="SERIAL_NUMBER" runat="server" InputType="number" Width="150" class="ipt01"
                    validate="[{required:true, msg:'请输入序列号'},{maxlength:10,msg:'录入最大长度为10'}]">1</cc1:ZLTextBox>
            </td>
        </tr>
        <tr>
            <th>
                长度：
            </th>
            <td>
                <cc1:ZLTextBox ID="LENGTH" runat="server" InputType="number" Width="150" class="ipt01"
                    validate="[{required:true, msg:'请输入长度'}]">9</cc1:ZLTextBox>
            </td>
        </tr>
        <tr>
            <th>
                粒度：
            </th>
            <td>
                <cc1:ZLTextBox ID="GRANULARITY" runat="server" InputType="number" Width="150" class="ipt01"
                    validate="[{required:true, msg:'请输入粒度'}]">1</cc1:ZLTextBox>
            </td>
        </tr>
        <tr>
            <th>
                最小值：
            </th>
            <td>
                <cc1:ZLTextBox ID="MIN" runat="server" InputType="number" Width="150" class="ipt01"
                    validate="[{required:true, msg:'请输入最小值'},{maxlength:9,msg:'录入最大长度为9'}]">1</cc1:ZLTextBox>
            </td>
        </tr>
        <tr>
            <th>
                最大值：
            </th>
            <td>
                <cc1:ZLTextBox ID="MAX" runat="server" InputType="number" Width="150" class="ipt01"
                    validate="[{required:true, msg:'请输入最大值'},{maxlength:9,msg:'录入最大长度为9'}]">999999999</cc1:ZLTextBox>
            </td>
        </tr>
         <tr><td></td>
         <td>
         <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保 存" CssClass="btn03"> </asp:Button>
         <input type="button" value="返 回" id="btnClose" onclick="window.location ='SerialList.aspx'"
        class="btn03" /></td></tr>
    </table>
        </div>
        
</asp:Content>
