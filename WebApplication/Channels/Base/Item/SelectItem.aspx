<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Base_Item_SelectItem" Codebehind="SelectItem.aspx.cs" %>
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
                { display: '监测项目', name: 'ITEM_NAME', width: 250, align: 'left', isSort: false }
                ], width: '100%', pageSize: 10, height: '100%',
                url: 'SelectItem.aspx?Action=GetItems' + '&monitorId=' + request('monitorId'),
                dataAction: 'server', //服务器排序
                usePager: true,       //服务器分页
                alternatingRow: false
            });
            $("#pageloading").hide();
        });

        function f_search() {
            var strSrhItemName = $("#txtSrhItemName").val();

            g.set('url', "SelectItem.aspx?Action=GetItems&strSrhItemName=" + escape(strSrhItemName) + '&monitorId=' + request('monitorId'));
            $("#pageloading").hide();
        }

        function f_select() {
            return g.getSelectedRow();
        }

        function request(strParame) {
            var args = new Object();
            var query = location.search.substring(1);

            var pairs = query.split("&"); // Break at ampersand 
            for (var i = 0; i < pairs.length; i++) {
                var pos = pairs[i].indexOf('=');
                if (pos == -1) continue;
                var argname = pairs[i].substring(0, pos);
                var value = pairs[i].substring(pos + 1);
                value = decodeURIComponent(value);
                args[argname] = value;
            }
            return args[strParame];
        } 
    </script>
</head>
<body style="padding:1px; overflow:hidden;">
   <div id="searchbar">
        监测项目：<input id="txtSrhItemName" type="text" />
        <input id="btnOK" type="button" value="查询" onclick="f_search()" />
    </div>
    <div id="maingrid4" style="margin:0; padding:0"></div>
 


  <div style="display:none;">
  <!-- g data total ttt -->
</div>
 
</body>
</html>
