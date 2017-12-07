<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Base_Item_SelectItemAnalysis" Codebehind="SelectItemAnalysis.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../../Controls/ligerui/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <script src="../../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../../../Controls/ligerui/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 
    <script src="../../../../Scripts/comm.js" type="text/javascript"></script> 
    <script type="text/javascript"  >
        var g = null;
        $(function () {
            g = $("#maingrid4").ligerGrid({
                columns: [
                { display: '方法依据', name: 'METHOD', width: 150, align: 'left', isSort: false },
                { display: '分析方法', name: 'ANALYSIS_METHOD', width: 300, align: 'left', isSort: false },
                { display: '仪器编号', name: 'APPARATUS_CODE', width: 100, align: 'left', isSort: false },
                { display: '仪器名称', name: 'INSTRUMENT', width: 200, align: 'left', isSort: false },
                { display: '规格型号', name: 'MODEL', width: 100, align: 'left', isSort: false },
                { display: '最低检出限', name: 'LOWER_CHECKOUT', width: 80, align: 'left', isSort: false },
                { display: '单位', name: 'UNIT', width: 80, align: 'left', isSort: false }
                ], width: '100%', pageSize: 10, height: '100%',
                url: 'SelectItemAnalysis.aspx?Action=GetItemAnalysis&ItemID=' + request("ItemID"),
                dataAction: 'server', //服务器排序
                usePager: false,       //服务器分页
                checkbox: true,
                alternatingRow: false,
                onCheckRow: function (checked, rowdata, rowindex) {
                    for (var rowid in this.records)
                        this.unselect(rowid);
                    this.select(rowindex);
                },
                onBeforeCheckAllRow: function (checked, grid, element) { return false; }
            });
            $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
            $("#pageloading").hide();
        });

        function f_select() {
            return g.getSelectedRow();
        }
    </script>
</head>
<body style="padding:1px; overflow:hidden;">
    <div id="maingrid4" style="margin:0; padding:0"></div>
 
  <div style="display:none;">
  <!-- g data total ttt -->
</div>
 
</body>
</html>
