// Create by 邵世卓 2012.11.28  "项目查询"功能
var firstManager;
var secondManager;
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

    //创建表单结构
    var strContractType = $.getUrlVar('contract_type'); //委托类型
    var strTaskID = $.getUrlVar('task_id'); //任务ID
    //委托书grid
    //自送样
    if (strContractType == "04") {
        firstManager = $("#firstgrid").ligerGrid({
            columns: [
         { display: '样品号', name: 'SAMPLE_CODE', width: 120, align: 'left', isSort: false },
         { display: '样品名称', name: 'SAMPLE_NAME', width: 80, align: 'left', isSort: false },
         { display: '样品类型', name: 'SAMPLE_TYPE', width: 60, align: 'left', isSort: false },
         { display: '质控类型', name: 'QC_TYPE', width: 60, align: 'left', isSort: false, render: function (data) {
             return getQcType(data.QC_TYPE);
         }
         }
        ], width: '100%', pageSizeOptions: [8, 10, 15], height: gridHeight,
            title: "样品信息",
            url: '../../../Mis/Report/ReportSchedule.aspx?type=getSampleInfo&id=' + strTaskID + '&strContractType=' + strContractType + "&QC=true",
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 10,
            alternatingRow: false,
            checkbox: true,
            whenRClickToSelect: true,
            onCheckRow: function (checked, rowdata, rowindex) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
                //点击的时候加载点位数据
                secondManager.set('url', "../../../Mis/Report/ReportSchedule.aspx?type=getItemInfo&sample_id=" + rowdata.ID + "&qc_type=" + rowdata.QC_TYPE);
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }
        });
    }
    //验收类
    else if (strContractType == "05") {
        $.ligerDialog.warn('验收委托不存在样品信息！');
    }
    //常规类
    else {
        firstManager = $("#firstgrid").ligerGrid({
            columns: [
                    { display: '点位', name: 'POINT_NAME', width: 150, align: 'left', isSort: false },
                    { display: '样品号', name: 'SAMPLE_CODE', width: 120, align: 'left', isSort: false },
                    { display: '样品类型', name: 'SAMPLE_TYPE', width: 60, align: 'left', isSort: false },
                    { display: '质控类型', name: 'QC_TYPE', width: 60, align: 'left', isSort: false, render: function (data) {
                        return getQcType(data.QC_TYPE);
                    }
                    }
        ], width: '100%', pageSizeOptions: [8, 10, 15], height: gridHeight,
            title: "样品信息",
            url: 'TotalSearchForSample.aspx?type=getSampleInfo&id=' + strTaskID + '&strContractType=' + strContractType + "&QC=true",
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 10,
            alternatingRow: false,
            checkbox: true,
            whenRClickToSelect: true,
            onCheckRow: function (checked, rowdata, rowindex) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);

                //点击的时候加载项目数据
                secondManager.set('url', "../../../Mis/Report/ReportSchedule.aspx?type=getItemInfo&sample_id=" + rowdata.ID + "&qc_type=" + rowdata.QC_TYPE);
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }
        });
    }
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //项目信息
    secondManager = $("#secondgrid").ligerGrid({
        columns: [
        { display: '监测项目', name: 'ITEM_NAME', width: 150, align: 'left', isSort: false },
        //        { display: '监测结果', name: 'ITEM_RESULT', width: 60, align: 'left', isSort: false, render: function (record) {
        //            if (record.ITEM_RESULT.indexOf("red") >= 0)
        //                return "<span style='color:red'>" + record.ITEM_RESULT.replace("red", "") + "</span>";
        //            else
        //                return record.ITEM_RESULT;
        //        }
        //        },
        {display: '评价标准', name: 'STANDARD_VALUE', width: 60, align: 'left', isSort: false },
        { display: '质控手段', name: 'QC', width: 80, align: 'left', isSort: false, render: function (record) {
            return getQcType(record.QC);
        }
        },
        { display: '分析负责人', name: 'HEAD_USER', width: 80, align: 'left', isSort: false },
        { display: '分析方法', name: 'METHOD_NAME', width: 150, align: 'left', isSort: false },
        { display: '仪器', name: 'APPARATUS_NAME', width: 150, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [8, 10, 15], height: '100%',
        title: "项目信息",
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 10,
        alternatingRow: false,
        checkbox: false,
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").hide(); //隱藏checkAll
});

//获取质控手段
function getQcType(value) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../../Mis/Report/ReportSchedule.aspx/getQcType",
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
//获取质控类型
function getQcType(type) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "TotalSearchForSample.aspx/getQcType",
        data: "{'strValue':'" + type + "'}",
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
