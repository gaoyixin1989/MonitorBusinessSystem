<%@ Page Language="C#" AutoEventWireup="True" Inherits="Channels_Base_Item_SelectMethod" Codebehind="SelectMethod.aspx.cs" %>
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
            g = $("#maingrid4").ligerGrid({
                columns: [
                { display: '序列号', name: 'ID', width: 100, align: 'left', isSort: false },
                { display: '方法依据', name: 'METHOD_CODE', width: 250, align: 'left', isSort: false },
                { display: '分析方法', name: 'ANALYSIS_NAME', width: 300, align: 'left', isSort: false }
                ], width: '100%', pageSize: 10, height: '100%',
                url: 'SelectMethod.aspx?Action=GetMthods&MethodSel_ItemID='+request("MethodSel_ItemID"),
                dataAction: 'server', //服务器排序
                usePager: true,       //服务器分页
                alternatingRow: false,
                checkbox: true,
                isChecked: f_isChecked,
                onCheckRow: f_onCheckRow,
                onCheckAllRow: f_onCheckAllRow
            });
            $("#pageloading").hide();
        });

        function f_search() {
            var strSrhMethod_Code = $("#txtSrhMethod_Code").val();

            g.set('url', "SelectMethod.aspx?Action=GetMthods&strSrhMethod_Code=" + escape(strSrhMethod_Code)+"&MethodSel_ItemID="+request("MethodSel_ItemID"));
            $("#pageloading").hide();
        }

        var checkedID = [];
        var checkedName = []; 
        if (request("METHOD_ID") != undefined && request("METHOD_ID") != "")
            checkedID = request("METHOD_ID").split(';');
        if (request("METHOD_NAME") != undefined && request("METHOD_NAME") != "")
            checkedName = request("METHOD_NAME").split(';');

        function findCheckedCustomer(ID) {
            for (var i = 0; i < checkedID.length; i++) {
                if (checkedID[i] == ID) return i;
            }
            return -1;
        }

        function f_isChecked(rowdata) {
            if (findCheckedCustomer(rowdata.ID) == -1)
                return false;
            return true;
        }

        function f_onCheckAllRow(checked) {
            for (var rowid in this.records) {
                if (checked)
                    addCheckedCustomer(this.records[rowid]['ID'], this.records[rowid]['METHOD_CODE']);
                else
                    removeCheckedCustomer(this.records[rowid]['ID']);
            }
        }

        function f_onCheckRow(checked, data) {
            if (checked)
                addCheckedCustomer(data.ID, data.METHOD_CODE);
            else
                removeCheckedCustomer(data.ID);
        }

        function addCheckedCustomer(ID, NAME) {
            if (findCheckedCustomer(ID) == -1) {
                checkedID.push(ID);
                checkedName.push(NAME);
            }
        }
        function removeCheckedCustomer(ID) {
            var i = findCheckedCustomer(ID);
            if (i == -1) return;
            checkedID.splice(i, 1);
            checkedName.splice(i, 1);
        }

        function f_select() {
            return g.getSelectedRow();
        }
        function f_selects() {
            return checkedID.join(';') + "|" + checkedName.join(';');
        }
    </script>
</head>
<body style="padding:1px; overflow:hidden;">
   <div id="searchbar">
        方法依据：<input id="txtSrhMethod_Code" type="text" />
        <input id="btnOK" type="button" value="查询" onclick="f_search()" />
    </div>
    <div id="maingrid4" style="margin:0; padding:0"></div>
 


  <div style="display:none;">
  <!-- g data total ttt -->
</div>
 
</body>
</html>
