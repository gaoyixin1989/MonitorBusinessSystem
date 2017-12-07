// Create by 潘德军 2012.11.19  "在线用户"功能

var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var objGrid = null;
var detailWinSrh = null;

//用户管理功能
$(document).ready(function () {
    //grid的菜单
    objGrid = $("#mainGrid").ligerGrid({
        title: '',
        dataAction: 'server',
        usePager: true,
        pageSize: 15,
        alternatingRow: false,
        checkbox: false,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [10, 15, 20, 50],
        height: '100%',
        url: 'UserOnline.aspx?type=getData',
        columns: [
                 { display: '用户姓名', name: 'REAL_NAME', align: 'left', width: 100, render: function (record) {
                     return getUserRealName(record.USER_ID);
                 }
                 },
                 { display: '用户登录名', name: 'USER_NAME', align: 'left', width: 100, render: function (record) {
                     return getUserName(record.USER_ID);
                 }
                 },
                 { display: '访问时间', name: 'LAST_OPTIME', align: 'left', width: 200 },
                 { display: '访问页面名称', name: 'LAST_OPERATION', align: 'left', width: 100 },
                 { display: '访问页面路径', name: 'LAST_PAGE', align: 'left', isSort: false, width: 300 }
                ],
        toolbar: { items: [
                { id: 'srh', text: '查询', click: searchData, icon: 'search' }
                ]
        }
    });

    //查询数据
    function searchData() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构
            var srhform = $("#searchForm");
            srhform.ligerForm({
                inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
                fields: [
                      { display: "用户", name: "srhUSER_NAME", newline: true, type: "text", group: "查询信息", groupicon: groupicon }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 350, height: 150, top: 90, title: "查询用户",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var srhUSER_NAME = $("#srhUSER_NAME").val();

            objGrid.set('url', "UserOnline.aspx?type=getData&strUserName=" + escape(srhUSER_NAME));
        }
    }

    //查询对话框元素的值 清除
    function clearSearchDialogValue() {
        $("#SrhDept_ID").val("");
        $("#SrhDept_ID1").val("");
    }
});

function getUserRealName(strUserID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "UserOnline.aspx/getUserRealName",
        data: "{'strValue':'" + strUserID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

function getUserName(strUserID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "UserOnline.aspx/getUserName",
        data: "{'strValue':'" + strUserID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}