﻿<%@ Page Title="子流程" Language="C#" MasterPageFile="../WinOpen.Master" AutoEventWireup="true" CodeBehind="SubFlow.aspx.cs" Inherits="CCFlow.WF.WorkOpt.SubFlow" %>
<%@ Register src="../SDKComponents/SubFlowDtl.ascx" tagname="SubFlowDtl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script language="JavaScript" src="../Comm/JScript.js" />
    <script type="text/javascript">
        function NoSubmit(ev) {
            if (window.event.srcElement.tagName == "TEXTAREA")
                return true;
            if (ev.keyCode == 13) {
                window.event.keyCode = 9;
                ev.keyCode = 9;
                return true;
            }
            return true;
        }
</script>
    <link href="../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
    <link href="../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


 <table style=" text-align:left; width:100%">
<caption>您好:<%=BP.WF.Glo.GenerUserImgSmallerHtml(BP.Web.WebUser.No,BP.Web.WebUser.Name) %>  - 该节点启动的子流程运行信息</caption>
<tr>
<td>

    <uc1:SubFlowDtl ID="SubFlowDtl1" runat="server" />

    </td>
</tr>
</table>

    
</asp:Content>
