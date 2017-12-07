// Create by 熊卫华 2013.03.11  "数据审核"功能

var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;
var objFourGrid = null;
var objFiveGrid = null;

var strUrl = "AnalysisResultAudit.aspx";
var strOneGridTitle = "";
var strTwoGridTitle = "监测项目信息";
var strThreeGridTitle = "监测项目信息";

//监测任务管理
$(document).ready(function () {
    topHeight = 2 * $(window).height() / 5 - 35;
    objOneGrid = $("#oneGrid").ligerGrid({
        title: strOneGridTitle,
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: 230,
        enabledSort: false,
        url: strUrl + '?type=getOneGridInfo&Sample=' + getQueryString("Sample"),
        columns: [
               { display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 150, minWidth: 60 }
                ],
        toolbar: { items: [
                { text: '发送', click: SendToNext, icon: 'add'}]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载样品信息
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID);
            objThreeGrid.set("data", emptyArray);
            objFourGrid.set("data", emptyArray);
            objFiveGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //发送到下一环节
    function SendToNext() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('发送之前请先选择一条任务');
            return;
        }
        $.ligerDialog.confirm('您确定将该任务发送至下一环节？', function (yes) {
            if (yes == true) {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=SendToNext&strTaskId=" + objOneGrid.getSelectedRow().ID + "&Sample=" + getQueryString("Sample"),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data == "1") {
                            objOneGrid.loadData();
                            objTwoGrid.set("data", emptyArray);
                            objThreeGrid.set("data", emptyArray);
                            objFourGrid.set("data", emptyArray);
                            objFiveGrid.set("data", emptyArray);
                            $.ligerDialog.success('任务发送成功')
                        }
                        else {
                            $.ligerDialog.warn('任务发送成功');
                        }
                    }
                });
            }
        });
    }
});
//监测类别管理
$(document).ready(function () {
    bottomHeight = 3 * $(window).height() / 5 - 35;
    objTwoGrid = $("#twoGrid").ligerGrid({
        dataAction: 'server',
        usePager: false,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        enabledSort: false,
        height: bottomHeight,
        columns: [
                    { display: '监测类别', name: 'MONITOR_ID', align: 'left', width: 150, minWidth: 60, render: function (record) {
                        return getMonitorTypeName(record.MONITOR_ID);
                    }
                    }
                ],
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载监测项目信息
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID + "&strMonitorID=" + rowdata.MONITOR_ID + "&Sample=" + getQueryString("Sample"));
            objFourGrid.set("data", emptyArray);
            objFiveGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
//样品信息
$(document).ready(function () {
    twoHeight = 3 * $(window).height() / 5;
    objThreeGrid = $("#threeGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: true,
        pageSize: 100,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        enabledSort: false,
        pageSizeOptions: [5, 10, 15, 20],
        height: twoHeight,
        columns: [
                { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 150, minWidth: 60 }
                ],
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载监测项目信息
            objFourGrid.set('url', strUrl + "?type=getFourGridInfo&threeGridId=" + rowdata.ID + "&strMonitorID=" + objTwoGrid.getSelectedRow().MONITOR_ID + "&Sample=" + getQueryString("Sample"));
            objFiveGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
//监测项目信息
$(document).ready(function () {
    threeHeight = 3 * $(window).height() / 10;
    objFourGrid = $("#fourGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        pageSize: 100,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        enabledSort: false,
        pageSizeOptions: [5, 10, 15, 20],
        height: threeHeight,
        columns: [
                 { display: '分析项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
                     return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                 }
                 },
        //                 { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60 },
        //                 {display: '质控手段', name: 'QC', align: 'left', width: 300, minWidth: 60, render: function (record) {
        //                     return getQcName(record.QC);
        //                 }
        //             },
                 {display: '分析方法', name: 'ANALYSIS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '检出限', name: 'LOWER_CHECKOUT', align: 'left', width: 100, minWidth: 60 },
                 { display: '单位', name: 'UNIT', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器名称及编码', name: 'APPARATUS_NAME', align: 'left', width: 200, minWidth: 60,
                     render: function (record, rowindex, value) {
                         if (record.APPARATUS_NAME != "" || record.APPARATUS_CODE != "")
                             return "" + record.APPARATUS_NAME + "(" + record.APPARATUS_CODE + ")";
                     }
                 },
                 { display: '要求完成时间', name: 'ASKING_DATE', align: 'left', width: 100, minWidth: 60 },
                 { display: '实际完成时间', name: 'FINISH_DATE', align: 'left', width: 100, minWidth: 60 },
                 { display: '分析负责人', name: 'HEAD_USERID', align: 'left', width: 100, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUserName(record.HEAD_USERID);
                 }
                 },
                 { display: '分析协同人', name: 'ASSISTANT_USERID', align: 'left', width: 200, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUseExName(record.ASSISTANT_USERID);
                 }
                 }
                ],
        toolbar: { items: [
                { text: '发送', click: SendToNext, icon: 'add' },
                { text: '退回', click: GoToBack, icon: 'add'}]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载实验室质控信息
            objFiveGrid.set('url', strUrl + "?type=getFiveGridInfo&fourGridId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    //将结果发送到下一环节
    function SendToNext() {
        if (objFourGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('发送之前请先选择监测项目信息');
            return;
        }
        $.ligerDialog.confirm('您确定要将该监测项目发送至下一环节吗？', function (yes) {
            if (yes == true) {
                if (SendResultToNextFlow(objFourGrid.getSelectedRow().ID) == "1") {
                    objFourGrid.loadData();
                    objFiveGrid.set("data", emptyArray);
                    $.ligerDialog.success('任务发送成功')
                }
                else {
                    $.ligerDialog.warn('任务发送失败');
                }
            }
        });
    }
    //将结果发送至下一个环节
    function SendResultToNextFlow(strResultId) {
        var isSuccess = false;
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "?type=SendResultToNext&strResultId=" + strResultId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                isSuccess = data;
            }
        });
        return isSuccess;
    }
    //退回监测监测项目 
    function GoToBack() {
        if (!objThreeGrid.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条监测项目信息退回');
            return;
        }
        $.ligerDialog.confirm('您确认退回该监测项目吗？', function (yes) {
            if (yes == true) {
                //退回该项目
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=GoToBack&strResultId=" + objFourGrid.getSelectedRow().ID,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data == "1") {
                            objFourGrid.loadData();
                            objFiveGrid.set("data", emptyArray);
                            $.ligerDialog.success('监测项目退回成功')
                        }
                        else {
                            $.ligerDialog.warn('监测项目退回失败');
                        }
                    }
                });
            }
        });
    }
});
//实验室质控信息
$(document).ready(function () {
    fourHeight = 3 * $(window).height() / 10;
    objFiveGrid = $("#fiveGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [10, 15, 20, 25],
        height: fourHeight,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
                     return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                 }
                 },
                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 200, minWidth: 60, render: function (record) {
                     return getQcName(record.QC_TYPE);
                 }
                 }
                ],
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});

//发送之前判断是否满足发送条件
function IsCanSendTaskCheckToNextFlow(strTaskId) {
    var isCanBack = false;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=IsCanSendTaskCheckToNextFlow&strTaskId=" + strTaskId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            isCanBack = data;
        }
    });
    return isCanBack;
}

//获取质控手段名称
function getQcName(strQcId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getQcName",
        data: "{'strQcId':'" + strQcId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取默认负责人用户名称
function getAjaxUserName(strUserId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDefaultUserName",
        data: "{'strUserId':'" + strUserId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取默认协同人名称
function getAjaxUseExName(strUserId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDefaultUserExName",
        data: "{'strUserId':'" + strUserId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}