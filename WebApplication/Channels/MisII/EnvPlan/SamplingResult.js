// Create by 魏林 2015.03.27  "送样现场项目结果录入"功能
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strID = "";

var objOneFrom = null;
var objTwoGrid = null;
var objThreeGrid = null;
var objFourGrid = null;

var strUrl = "SamplingResult.aspx";
var strOneGridTitle = "";
var strTwoGridTitle = "任务信息";
var strThreeGridTitle = "监测项目信息";

var toolArr = null;
var strPlanID = "", strWorkTask_Id = "", strTask_Id = "", strTaskType = "";
var CCFLOW_WORKID = "";
var objTaskInfo = null;

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


function Save() {

    return 1;
}

//委托书信息
$(document).ready(function () {
    CCFLOW_WORKID = $.getUrlVar('WorkID');

    if (objTaskInfo == null) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../../Mis/Contract/MethodHander.ashx?action=GetTaskInfor&strCCFLOW_WORKID=" + CCFLOW_WORKID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    objTaskInfo = data.Rows;
                }
                else {
                    $.ligerDialog.warn('数据加载错误！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    strPlanID = objTaskInfo[0].PLAN_ID;
    strWorkTask_Id = objTaskInfo[0].ID;
    strTask_Id = objTaskInfo[0].CONTRACT_ID;
    strTaskType = objTaskInfo[0].TASK_TYPE;

    if (strTaskType) {
        if (strTaskType == "1") {
            strTwoGridTitle = strTwoGridTitle + "-环境质量类";
        }
    }
    //委托书信息
    $.post('SamplingResult.aspx?type=getContractInfo&id=' + $("#ID").val() + '&planid=' + strPlanID + '&strWorkTask_Id=' + strWorkTask_Id, function (data) {
        SetContractInfo(data);
    }, 'json');

});

//采样任务分配信息
$(document).ready(function () {
    var gridHeight = $(window).height() / 2 - 150;

    objTwoGrid = $("#twoGrid").ligerGrid({
        title: strTwoGridTitle,
        dataAction: 'server',
        usePager: false,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: gridHeight,
        url: strUrl + '?type=getTwoGridInfo&planid=' + strPlanID + '&strWorkTask_Id=' + strWorkTask_Id,
        columns: [
                { display: '监测类型', name: 'MONITOR_ID', align: 'left', width: 150, minWidth: 60, render: function (record) {
                    return getMonitorTypeName1(record.MONITOR_ID);
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

            //点击的时候加载监测点位信息
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID + '&planid=' + strPlanID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });

});



//监测点位信息
$(document).ready(function () {
    var gridHeight = $(window).height() / 2 - 50;

    objThreeGrid = $("#threeGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 6,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [6, 10, 15],
        height: gridHeight,
        enabledEdit: true,
        columns: [
                    { display: '监测点位', name: 'POINT_ID', align: 'left', width: 160, minWidth: 60, render: function (record) {
                        return getMonitorPointName(record.POINT_ID);
                    }
                },
                    { display: '样品编码', name: 'SAMPLE_CODE', align: 'left', width: 100, minWidth: 60,
                        editor: {
                            type: 'text'
                        }
                    },
                    { display: '样品名称', name: 'SAMPLE_NAME', align: 'left', width: 100, minWidth: 60 },
                    { display: '样品条码', name: 'SAMPLE_BARCODE', align: 'left', width: 100, minWidth: 60,
                     editor: {
                        type: 'text'
                    }
                    },
                    { display: '质控类别', name: 'REMARK1', align: 'left', width: 80, minWidth: 60 }
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
            objFourGrid.set('url', strUrl + "?type=getFourGridInfo&ThreeGridId=" + rowdata.ID + "&QcType=" + rowdata.QC_TYPE + '&planid=' + strPlanID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    
});

//监测项目信息
$(document).ready(function () {
    var gridHeight = $(window).height() / 2 - 50;

    objFourGrid = $("#fourGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 6,
        alternatingRow: false,
        checkbox: false,
        onRClickToSelect: true,
        width: '100%',
        enabledEdit: true,
        pageSizeOptions: [6, 10, 15],
        height: gridHeight,
        columns: [
                    { display: '监测点位', name: 'TASK_POINT_ID', align: 'left', width: 180, minWidth: 60, render: function (record) {
                        return getMonitorPointName(record.TASK_POINT_ID);
                    }
                    },
                    { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 140, minWidth: 60, render: function (record) {
                        return getMonitorItemName(record.ITEM_ID);
                    }
                    },
                    { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60,
                        editor: {
                            type: 'text'
                        }
                    }

                ],
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onAfterEdit: AfterEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});

function AfterEdit(e) {
    var id = e.record.ID;
    var strItemResult = e.record.ITEM_RESULT;
    
    var columnname = "", value = "";
    columnname = e.column.columnname;
    value = e.value;
    if (e.record["__status"] != "nochanged") {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/saveItemInfo",
            data: "{'id':'" + id + "','strColumnName':'" + columnname + "','strValue':'" + value + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == 1) {
                    //objTwoGrid.cancelEdit(e.rowindex);
                }
            }
        });

    }

}

//初始委托书信息模块
function SetContractInfo(data) {
    objOneFrom = $("#oneFrom").ligerForm({
        inputWidth: 160, labelWidth: 100, space: 30, labelAlign: 'right',
        fields: [
                { name: "ID", type: "hidden" },
                { display: "项目名称", name: "PROJECT_NAME", newline: true, type: "text", width: 400, group: "任务信息", groupicon: groupicon },
                { display: "任务单号", name: "TICKET_NUM", newline: true, width: 120, type: "text" },
                { display: "开始时间", name: "SAMPLE_ASK_DATE", newline: false, width: 120, type: "date" },
                { display: "要求完成时间", name: "SAMPLE_FINISH_DATE", newline: false, width: 120, type: "date" }
                ]
    });
    //赋值
    if (data) {
        $("#PROJECT_NAME").val(data.PROJECT_NAME);
        $("#TICKET_NUM").val(data.TICKET_NUM);
        var strAskFinishDate = "";
        if (data.ASKING_DATE != "") {
            strAskFinishDate = new Date(Date.parse(data.ASKING_DATE.replace(/-/g, '/')));
            strAskFinishDate = TogetDate(strAskFinishDate);
        }
        else {
            var d1 = new Date();
            d1.setDate(d1.getDate() + 7);
            strAskFinishDate = TogetDate(d1);

        }
        $("#SAMPLE_FINISH_DATE").val(strAskFinishDate);
    }

    $("#PROJECT_NAME").ligerGetComboBoxManager().setDisabled();
    $("#TICKET_NUM").ligerGetComboBoxManager().setDisabled();
    $("#SAMPLE_ASK_DATE").val(currentTime())

    //JS 获取当前时间
    function currentTime() {
        var d = new Date(), str = '';
        str += d.getFullYear() + '-';
        str += d.getMonth() + 1 + '-';
        str += d.getDate();
        return str;
    }
    function currentAskingTime() {
        var b = new Date();
        var c = new Date(b.getFullYear(), b.getMonth(), b.getDate() + 7);
        str = c.getFullYear() + "-" + (c.getMonth() + 1) + "-" + c.getDate()
        return str;
    }
}
//弹出选择默认负责人
function getDefaultUserName(strTaskId, strSubTaskId, strMonitorType) {
    $.ligerDialog.open({ title: "选择采样负责人", width: 400, height: 250, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.UserSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }
            objTwoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../Mis/Monitor/sampling/DefaultUser.aspx?strSubTaskId=" + strSubTaskId + "&strMonitorType=" + strMonitorType + "&strTaskId=" + strTaskId
    });
}
//弹出选择默认协同人
function getDefaultUserExName(strTaskId, strSubTaskId, strMonitorType) {
    $.ligerDialog.open({ title: "选择采样协同人", width: 400, height: 250, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.UserSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功');
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据保存失败');
                return;
            }
            objTwoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../Mis/Monitor/sampling/DefaultUserEx.aspx?strSubTaskId=" + strSubTaskId + "&strMonitorType=" + strMonitorType + "&strTaskId=" + strTaskId
    });
}


//获取监测点位信息
function getMonitorPointName(strPointID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getMonitorPointName",
        data: "{'strTaskPointId':'" + strPointID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取监测项目信息
function getMonitorItemName(strTaskItemId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getMonitorItemName",
        data: "{'strTaskItemId':'" + strTaskItemId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取监测类别信息
function getMonitorTypeName1(strMonitorTypeId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getMonitorTypeName",
        data: "{'strMonitorTypeId':'" + strMonitorTypeId + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
function TogetDate(date) {
    var strD = "";
    var thisYear = date.getYear();
    thisYear = (thisYear < 1900) ? (1900 + thisYear) : thisYear;
    var thisMonth = date.getMonth() + 1;
    //如果月份长度是一位则前面补0    
    if (thisMonth < 10) thisMonth = "0" + thisMonth;
    var thisDay = date.getDate();
    //如果天的长度是一位则前面补0    
    if (thisDay < 10) thisDay = "0" + thisDay;
    {

        strD = thisYear + "-" + thisMonth + "-" + thisDay;
    }
    return strD;
}
//获取监测计划责任人
function GetContractDutyUser(strtask_id, mointorid) {
    var vDutyUserList = null;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../../MonitoringPlan/MonitoringPlan.ashx?action=GetContractDutyUser&strMonitorId=" + mointorid + "&task_id=" + strtask_id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vDutyUserList = data.Rows;
            } else {
                vDutyUserList = null;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    if (vDutyUserList != null) {
        return vDutyUserList[0].REAL_NAME;
    }
    return "";
}

//根据任务ID获取委托类别
function getContractType(strTaskId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getContractType",
        data: "{'strTaskId':'" + strTaskId + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
