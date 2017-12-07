<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Base_Item_SelectCompanyPoint" Codebehind="SelectCompanyPoint.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 
    <script src="../../../Scripts/comm.js" type="text/javascript"></script> 
    <script type="text/javascript"  >
        var g = null;
        $(function () {
            g = $("#maingrid").ligerGrid({
                columns: [
                { display: '点位名称', name: 'POINT_NAME', width: 300, align: 'left', isSort: false }
                ], width: '100%', height: '100%',
                url: 'SelectCompanyPoint.aspx?Action=GetPoints&COMPANY_ID=' + request("COMPANY_ID"),
                dataAction: 'server', //服务器排序
                usePager: false,       //服务器分页
                checkbox: true,
                alternatingRow: false
            });
            $("#pageloading").hide();
        });

        function f_search() {
            var strSrhPointName = $("#txtSrhPointName").val();

            g.set('url', "SelectCompanyPoint.aspx?Action=GetPoints&strSrhPointName=" + escape(strSrhPointName) + "&COMPANY_ID=" + request("COMPANY_ID"));
            $("#pageloading").hide();
        }

        function f_select() {
            var select = g.getSelectedRows();
            var points = "";
            for (var i = 0; i < select.length; i++) {
                if (i == select.length - 1) {
                    points += select[i].POINT_NAME;
                }
                else {
                    points += select[i].POINT_NAME + ";";
                }
            }
            return points;
        }
    </script>
</head>
<body style="padding:1px; overflow:hidden;">
   <div id="searchbar">
        点位名称：<input id="txtSrhPointName" type="text" />
        <input id="btnOK" type="button" value="查询" onclick="f_search()" />
    </div>
    <div id="maingrid" style="margin:0; padding:0"></div>
 
  <div style="display:none;">
  <!-- g data total ttt -->
</div>
 
</body>
</html>
