//Creat by 潘德军 数据可追溯性历史记录 2013-7-6
var resultLogGrid;
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    $("#layout1").ligerLayout({ height: '100%' });

    strResultID = request('resultid');
    //委托书grid
    resultLogGrid = $("#resultLogGrid").ligerGrid({
        columns: [
        { display: '旧结果数据', name: 'OLD_RESULT', width: 100, align: 'center', isSort: false },
        { display: '旧原始单号', name: 'REMARK1', width: 150, align: 'center', isSort: false },
        { display: '修改后数据', name: 'NEW_RESULT', width: 100, align: 'center', isSort: false },
        { display: '修改后原始单号', name: 'REMARK2', width: 150, align: 'center', isSort: false },
        { display: '修改人员', name: 'HEAD_USERID', width: 100, align: 'left', isSort: false, render: function (data) {
            return getUserName(data.HEAD_USERID);
        }
        },
        { display: '修改时间', name: 'FINISH_DATE', width: 150, align: 'left', isSort: false, render: function (data) {
            return data.FINISH_DATE.toString().replace("0:00:00", "").replace("00:00:00", "");
        } 
        }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: '100%',
        title: "",
        url: 'ResultLogInfo.aspx?action=GetLogInfo&resultid=' + strResultID,
        dataAction: 'server', //服务器排序
        usePager: false,       //服务器分页
        pageSize: 10,
        alternatingRow: false,
        checkbox: false,
        whenRClickToSelect: true
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});

//获取姓名
function getUserName(value) {
    var strReturn;
    if (value.length == 0)
        return "";
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url:  "ResultLogInfo.aspx/getUserName",
        data: "{'strValue':'" + value + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
        }
    });
    return strReturn;
}