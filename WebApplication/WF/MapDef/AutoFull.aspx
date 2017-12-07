<%@ Page Title="表单扩展设置" Language="C#" MasterPageFile="WinOpen.master" AutoEventWireup="true"
    Inherits="CCFlow.WF.MapDef.WF_MapDef_AutoFull" CodeBehind="AutoFull.aspx.cs" %>

<%@ Register Src="Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Comm/Style/CommStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/EasyUIUtility.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="easyui-layout" data-options="fit:true">
        <div data-options="region:'west',split:true,title:'功能列表'" style="width: 200px">
            <uc1:Pub ID="Left" runat="server" />
        </div>
        <div data-options="region:'center',title:'数据获取'" style="padding: 5px">
            <uc1:Pub ID="Pub1" runat="server" />
        </div>
    </div>
</asp:Content>
