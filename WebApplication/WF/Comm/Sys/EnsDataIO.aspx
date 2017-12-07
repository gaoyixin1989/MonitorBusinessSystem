<%@ Page Language="C#" MasterPageFile="WinOpen.master" AutoEventWireup="true" Inherits="CCFlow_Comm_Sys_EnsDataIO" Title="数据导入导出" Codebehind="EnsDataIO.aspx.cs" %>
<%@ Register src="../UC/Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
		<LINK href="../Style/Table0.css" type="text/css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table style="width:100%;">
<caption><a href="EnsAppCfg.aspx?EnsName=<%=this.EnsName %>&DoType=Adv">基本配置</a>  - 数据导入导出</caption>
<tr>
    <uc1:Pub ID="Pub1" runat="server" />
    <uc1:Pub ID="Pub2" runat="server" />
    </tr>
    </table>
</asp:Content>

