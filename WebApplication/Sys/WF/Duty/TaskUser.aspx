<%@ Page Language="C#" AutoEventWireup="True" Inherits="Sys_WF_Duty_TaskUser" Codebehind="TaskUser.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
                { display: '用户姓名', name: 'REAL_NAME', width: 150, align: 'left', isSort: false },
                { display: '职位', name: 'Post', align: 'left', width: 200, isSort: false, render: function (record) {
                    return getPostName(record.ID);
                }
                },
                 { display: '部门', name: 'Dept', align: 'left', width: 200, isSort: false, render: function (record) {
                     return getDeptName(record.ID);
                 }
                 },
                { display: '序号', name: 'ORDER_ID', align: 'left', width: 50, isSort: false }
                ], width: '99%', height: '99%',
                url: 'TaskUser.aspx?Action=GetUsers',
                 checkbox: true,
                 dataAction: 'local', 
                 pageSize: 10,
                 alternatingRow: false,
                 isChecked: f_isChecked,
                 onCheckRow: f_onCheckRow,
                 onBeforeCheckAllRow: function (checked, grid, element) { return false; }
             });
             $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
         });

         /*
         表单分页多选
         即利用onCheckRow将选中的行记忆下来，并利用isChecked将记忆下来的行初始化选中
         */
         var checkedCustomer = [];
         var checkedCustomerName = [];
         var strSelUserId = request("selUserId");
         var strSelUser = request("selUser");
         if (strSelUserId != "undefined" && strSelUserId.length > 0) {
             checkedCustomer = strSelUserId.split(";");
             checkedCustomerName = strSelUser.split(";");
         }

         function findCheckedCustomer(CustomerID) {
             for (var i = 0; i < checkedCustomer.length; i++) {
                 if (checkedCustomer[i] == CustomerID) return i;
             }
             return -1;
         }
         
         function f_isChecked(rowdata) {
             if (findCheckedCustomer(rowdata.ID) == -1)
                 return false;
             return true;
         }

         function f_onCheckRow(checked, data) {
             if (checked) addCheckedCustomer(data.ID, data.REAL_NAME);
             else removeCheckedCustomer(data.ID, data.REAL_NAME);
         }
      
         function addCheckedCustomer(CustomerID,RealName) {
             if (findCheckedCustomer(CustomerID) == -1) {
                 checkedCustomer.push(CustomerID);
                 checkedCustomerName.push(RealName);
             }
         }
         function removeCheckedCustomer(CustomerID, RealName) {
             var i = findCheckedCustomer(CustomerID);
             if (i == -1) return;
             checkedCustomer.splice(i, 1);
             checkedCustomerName.splice(i, 1);
         }
         
         function f_select() {
             return checkedCustomer.join(';') + '|' + checkedCustomerName.join(';');
         }

         //获取职位信息
         function getPostName(strUserID) {
             var strValue = "";
             $.ajax({
                 cache: false,
                 async: false,
                 type: "POST",
                 url: "TaskUser.aspx/getPostName",
                 data: "{'strValue':'" + strUserID + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (data, textStatus) {
                     strValue = data.d;
                 }
             });
             return strValue;
         }

         //获取部门信息
         function getDeptName(strUserID) {
             var strValue = "";
             $.ajax({
                 cache: false,
                 async: false,
                 type: "POST",
                 url: "TaskUser.aspx/getDeptName",
                 data: "{'strValue':'" + strUserID + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (data, textStatus) {
                     strValue = data.d;
                 }
             });
             return strValue;
         }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <div id="maingrid4" style="margin:0; padding:0"></div>
   
    </form>
</body>
</html>

