<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeBehind="Draft.aspx.cs" Inherits="CCFlow.WF.Draft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%
   //获取待办。
   System.Data.DataTable dt = BP.WF.Dev2Interface.DB_GenerDraftDataTable();
   // 输出结果
   %> 
   <table style="width:90%; border:1px" ><caption>草稿 (<%=dt.Rows.Count%>) </caption>
   <tr>
   <th>序</th>
   <th>流程</th>
   <th>标题</th>
   <th>时间</th>
   </tr>
      <%
    // 生成timke 方式浏览器打开旧的界面，方式界面缓存.      
    string t = DateTime.Now.ToString("MMddhhmmss");
    int idx = 0;
    foreach (System.Data.DataRow dr in dt.Rows)
    {
        idx++;
        %>
        <tr>
        <td class="Idx"><%= idx%></td>
        <td nowarp=true><a target="_blank" href='/WF/MyFlow.aspx?FK_Flow=<%=dr["FK_Flow"] %>&WorkID=<%=dr["WorkID"] %>&FID=0&T=<%=t %>' ><%=dr["Title"].ToString()%> </a> </td>
        <td><%=dr["FlowName"]%></td>
        <td><%=dr["RDT"]%></td>
        </tr>
   <% } %> 
   </table>
</asp:Content>
