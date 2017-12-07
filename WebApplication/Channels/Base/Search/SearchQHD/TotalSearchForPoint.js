//Creat by ssz 2013-1-3
var firstManager;
var secondManager;
var strContractID;
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});

$(document).ready(function () {
    var topHeight = $(window).height() / 2;
    var gridHeight = $(window).height() / 2;

    $("#layout1").ligerLayout({ height: '100%', topHeight: topHeight });

    strContractID = $.getUrlVar('id');
    //委托书grid
    firstManager = $("#firstgrid").ligerGrid({
        columns: [
        { display: '委托书编号', name: 'REMARK1', width: 150, align: 'left', isSort: false },
        { display: '监测点名称', name: 'POINT_NAME', width: 150, align: 'left', isSort: false },
        { display: '监测类别', name: 'MONITOR_ID', width: 80, align: 'left', isSort: false, render: function (data) {
            return getMonitorName(data.MONITOR_ID);
        }
        },
        { display: '监测频次', name: 'FREQ', width: 60, align: 'left', isSort: false },
        { display: '监测点位置', name: 'ADDRESS', width: 100, align: 'left', isSort: false },
        { display: '经度', name: 'LONGITUDE', width: 100, align: 'left', isSort: false },
        { display: '纬度', name: 'LATITUDE', width: 100, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeight,
        title: "点位信息",
        url: 'TotalSearchForPoint.aspx?action=GetContractPoint&id=' + strContractID,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            //点击的时候加载点位数据
            secondManager.set('url', "TotalSearchForPoint.aspx?action=GetPointItem&point_id=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    //点位项目信息
    secondManager = $("#secondgrid").ligerGrid({
        columns: [
        { display: '监测项目', name: 'ITEM_ID', width: 150, align: 'left', isSort: false, render: function (data) {
            return getItemName(data.ITEM_ID);
        }
        },
        { display: '实验室认可', name: 'LAB_CERTIFICATE', width: 100, align: 'left', isSort: false, render: function (data) {
            if (data.LAB_CERTIFICATE == "1")
                return "是";
            else
                return "否";
        }
        },
        { display: '计量认可', name: 'MEASURE_CERTIFICATE', width: 100, align: 'left', isSort: false, render: function (data) {
            if (data.MEASURE_CERTIFICATE == "1")
                return "是";
            else
                return "否";
        }
        },
        { display: '现场项目', name: 'IS_SAMPLEDEPT', width: 80, align: 'left', isSort: false, render: function (data) {
            if (data.IS_SAMPLEDEPT == "1")
                return "是";
            else
                return "否";
        }
        },
        { display: '监测子项', name: 'IS_SUB', width: 80, align: 'left', isSort: false, render: function (data) {
            if (data.IS_SUB == "1")
                return "是";
            else
                return "否";
        }
        }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeight, heightDiff: -10,
        title: "点位项目",
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});
//获取监测类型
function getContractType(strValue) {
    $.ajax({
        type: "POST",
        async: false,
        url: "TotalSearchForPoint.aspx/getContractType",
        data: "{'strValue':'" + strValue + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != null) {
                return data.d;
            }
        }
    });
}

//获取监测类别信息
function getMonitorName(strMonitorID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "CompanySearch.aspx/getMonitorName",
        data: "{'strValue':'" + strMonitorID + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus) {
            if (data != null) {
                strValue = data.d;
            }
        }
    });
    return strValue;
}

//获取项目名称
function getItemName(strValue) {
    var strResult;
    $.ajax({
        type: "POST",
        async: false,
        url: "TotalSearchForPoint.aspx/getItmeName",
        data: "{'strValue':'" + strValue + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != null) {
                strResult = data.d;
            }
        }
    });
    return strResult;
}