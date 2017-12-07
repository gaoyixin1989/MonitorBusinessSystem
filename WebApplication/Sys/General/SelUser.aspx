<%@ Page Language="C#" AutoEventWireup="True" Inherits="Sys_General_SelUser" Codebehind="SelUser.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script> 
    <script src="../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 

     <script type="text/javascript"  >
         var g = null;
         $(function () {
             var strSrhDept = $("#ddlDept").val();

             g = $("#maingrid4").ligerGrid({
                 columns: [
                { display: '用户', name: 'REAL_NAME', width: 250, align: 'left', isSort: false },
                { display: '办公电话', name: 'PHONE_OFFICE', width: 200, align: 'left', isSort: false },
                { display: '手机号码', name: 'PHONE_MOBILE', width: 200, align: 'left', isSort: false }
                ], width: '100%', pageSize: 10, height: '100%',
                 url: 'SelUser.aspx?Action=GetUsers&strSrhDept=' + strSrhDept,
                 dataAction: 'server', //服务器排序
                 usePager: true,       //服务器分页
                 alternatingRow: false
             });
             $("#pageloading").hide();
         });

         function f_search() {
             var strSrhDept = $("#ddlDept").val();

             g.set('url', "SelUser.aspx?Action=GetUsers&strSrhDept=" + strSrhDept);
             $("#pageloading").hide();
         }

         function f_select() {
             return g.getSelectedRow();
         }
    </script>
</head>
<body>
    <form runat="server">
    <div id="searchbar">
        部门：<asp:DropDownList ID="ddlDept" ClientIDMode="Static" runat ="server"></asp:DropDownList>
        <input id="btnOK" type="button" value="查询" onclick="f_search()" />
    </div>
    <div id="maingrid4" style="margin:0; padding:0"></div>
    </form>
</body>
</html>
