<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/InputEx.master" AutoEventWireup="True" Inherits="Sys_General_PersonUserEdit" Codebehind="PersonUserEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $(document).ready(function () {
                $("#txtBIRTHDAY").ligerDateEditor();
            });
        })
    </script>

    <style type="text/css">
        body{ font-size:12px;}
        .l-table-edit {}
        .l-table-edit-td{ padding:4px;}
        .l-button-submit,.l-button-test{width:80px; float:left; margin-left:10px; padding-bottom:2px;}
        .l-verify-tip{ left:230px; top:120px;}
    </style>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphInput" runat="Server">
    <div class="pall">
        <table class="tablelist2012">
             <tr>
                <th class="m10">登录名</th>
                <td >
                    <asp:Label ID="USER_NAME" name="USER_NAME" runat="server" Text=""></asp:Label>
                 </td>
               <th class="m10" >姓名</th>
                 <td  class="m15"><asp:TextBox name="REAL_NAME" type="text"  runat="server" id="REAL_NAME"></asp:TextBox></td>
            </tr>
             <tr>
                <th >性别</th>
                <td >
                <asp:DropDownList ID="SEX" runat="server" class="slct01">
                    <asp:ListItem>男</asp:ListItem>
                    <asp:ListItem>女</asp:ListItem>
                </asp:DropDownList>
                </td><th >出生日期</th>
                <td ><input type="text" name="BIRTHDAY" id="BIRTHDAY" runat="server" /></td>
            </tr>   
             <tr>
                <th >办公电话</th>
                <td ><asp:TextBox name="PHONE_OFFICE" id="PHONE_OFFICE"   runat="server"></asp:TextBox></td>
               <th >手机(Mobile)</th>
                 <td ><asp:TextBox name="PHONE_MOBILE"  id="PHONE_MOBILE"   runat="server"></asp:TextBox></td>
            </tr>              
            <tr>
                <th >家庭电话</th>
                <td ><asp:TextBox name="PHONE_HOME" id="PHONE_HOME"   runat="server"></asp:TextBox></td>
                <th >邮箱（E-mail)</th>
                <td ><asp:TextBox name="EMAIL" id="EMAIL"  runat="server" validate="{required:true,email:true}"></asp:TextBox></td>
            </tr>
            <tr>
                    <th >详细地址</th>
                    <td > 
                    <asp:TextBox cols="100" rows="4" class="l-textarea" id="ADDRESS" name="ADDRESS"   runat="server"  style="width:400px" validate="{required:true}" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <th>邮编</th>
                    <td ><asp:TextBox name="POSTCODE" id="POSTCODE"  runat="server"   ltype="text" validate="{minlength:0,maxlength:7}"></asp:TextBox></td>
                   
            </tr>

        </table>
    </div>
    <div class="ptb2012">
       <asp:Button id="btnSave"  name="btnSave"  Text="提交"  class="btn03" runat="server"  onclick="btnSave_Click" /></asp:Button> <input type="button" id="btnRef"  value="重置"  name="btnRef" onclick="javascript:window.location.href = window.location.href;" class="btn03"/>
</div>
</asp:Content>



