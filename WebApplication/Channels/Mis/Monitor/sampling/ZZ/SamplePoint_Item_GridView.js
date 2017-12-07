// Create by 苏成斌 2012.12.14  "采样-点位信息管理的监测项目列表"功能

var strItemId = "";
var ItemGrid;
var strUrl = "SamplePointView.aspx"

//点位信息管理的监测项目管理功能
$(document).ready(function () {
    //分析方法grid
    ItemGrid = $("#ItemGrid").ligerGrid({
        columns: [
            { display: '监测点', name: 'Point_Name', width: 180, align: 'left', isSort: false, render: function (record) {
                return getPointName(record.TASK_POINT_ID);
            }
            },
            { display: '监测项目', name: 'ITEM_NAME', width: 140, align: 'left', isSort: false, render: function (record) {
                return getItemName(record.ITEM_ID);
            }
            }
        ], width: '100%', pageSizeOptions: [5, 8, 10], height: '95%',
        url: strUrl + '?Action=GetItems',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});

//获取监测点位信息
function getPointName(strPointID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getPointName",
        data: "{'strValue':'" + strPointID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取监测项目信息
function getItemName(strItemID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getItemName",
        data: "{'strValue':'" + strItemID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}