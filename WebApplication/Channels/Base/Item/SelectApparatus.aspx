<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Base_Item_SelectApparatus" Codebehind="SelectApparatus.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 
    <script type="text/javascript"  >
        var g = null;
        $(function () {
            g = $("#maingrid4").ligerGrid({
                columns: [
                { display: '序列号', name: 'ID', width: 100, align: 'left', isSort: false },
                { display: '仪器编号', name: 'APPARATUS_CODE', width: 150, align: 'left', isSort: false },
                { display: '仪器名称', name: 'NAME', width: 200, align: 'left', isSort: false },
                { display: '规格型号', name: 'MODEL', width: 150, align: 'left', isSort: false }
                ], width: '100%', pageSize: 10, height: '100%',
                url: 'SelectApparatus.aspx?Action=GetApparatus',
                dataAction: 'server', //服务器排序
                usePager: true,       //服务器分页
                alternatingRow: false
            });
            $("#pageloading").hide();
        });

        function f_search() {
            var strSrhApparatus_CODE = $("#txtSrhApparatus_CODE").val();
            var strSrhName = $("#txtSrhName").val();

            g.set('url', "SelectApparatus.aspx?Action=GetApparatus&strSrhApparatus_CODE=" + escape(strSrhApparatus_CODE) + "&strSrhName=" + escape(strSrhName));
            $("#pageloading").hide();
        }

        function f_select() {
            return g.getSelectedRow();
        }
    </script>
</head>
<body style="padding:1px; overflow:hidden;">
   <div id="searchbar">
        仪器编号：<input id="txtSrhApparatus_CODE" type="text" />
        仪器名称：<input id="txtSrhName" type="text" />
        <input id="btnOK" type="button" value="查询" onclick="f_search()" />
    </div>
    <div id="maingrid4" style="margin:0; padding:0"></div>
 


  <div style="display:none;">
  <!-- g data total ttt -->
</div>
 
</body>
</html>
