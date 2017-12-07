// Create by 苏成斌 2012.12.6  "采样任务分配"功能
var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strID = "";

var objOneFrom = null;
var objTwoGrid = null;
var objThreeGrid = null;
var objFourGrid = null;

var strUrl = "SamplingTaskAllocation.aspx";
var strOneGridTitle = "";
var strTwoGridTitle = "采样任务信息";
var strThreeGridTitle = "监测项目信息";

var strFlowCode = "duty_sampling";
var strResultStatus = "01";

var strListQc1 = "", strListQc2 = "", strListQc3 = "", strListQc4 = "";
var strQc3Count = "", strQc4count = "";
var toolArr = null;
var strPlanID = "", strWorkTask_Id = "", strTask_Id = "", strQCStatus = "", strTaskType = "";
var EnvQCsetting = "";

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

//委托书信息
$(document).ready(function () {
    strPlanID = $.getUrlVar('planid');
    strWorkTask_Id = $.getUrlVar('strWorkTask_Id');
    strTask_Id = $.getUrlVar('strTask_Id');
    strQCStatus = $.getUrlVar('strQCStatus');

    strTaskType = $.getUrlVar('strTaskType');

    if (strTaskType) {
        if (strTaskType == "1") {
            strTwoGridTitle = strTwoGridTitle + "-环境质量类";
        }
    }
    //委托书信息
    $.post('SamplingTaskAllocation.aspx?type=getContractInfo&id=' + $("#ID").val() + '&planid=' + strPlanID, function (data) {
        SetContractInfo(data);
    }, 'json');

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../../MonitoringPlan/MonitoringPlan.ashx?action=GetConfigSetting&strConfigKey=EnvQCsetting",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                EnvQCsetting = data;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
});

//采样任务分配信息
$(document).ready(function () {
    var gridHeight = $(window).height() / 2 - 150;

    toolArr = { items: [
        { text: '发送', click: SendToNext, icon: 'add'}]
    };
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
        url: strUrl + '?type=getTwoGridInfo&planid=' + strPlanID,
        columns: [
                   { display: '监测类型', name: 'MONITOR_ID', align: 'left', width: 150, minWidth: 60, render: function (record) {
                       return getMonitorTypeName1(record.MONITOR_ID);
                   }
                   },
                   { display: '默认负责人', name: 'SAMPLING_MANAGER_ID', align: 'left', width: 200, minWidth: 60,
                       render: function (record, rowindex, value) {
                           var strSubTaskId = record.ID;
                           var strMonitorType = record.MONITOR_ID;
                           var strUserName = "";
                           strUserName = record.SAMPLING_MANAGER_ID;
                           if (strTask_Id && strTask_Id != "" && strUserName == "") {
                               strUserName = GetContractDutyUser(strTask_Id, strMonitorType);
                           }
                           // 胡方扬 2013-08-22 修改  释放 05  验收监测 不可选择其他项目负责人
//                           var strContractType = getContractType(record.TASK_ID);

//                           if (strContractType == "05") {
//                               return strUserName;
//                           }
//                           else {
                               if (strUserName.length == 0)
                                   strUserName = "请选择";
                               return "<a href=\"javascript:getDefaultUserName('" + strSubTaskId + "','" + strMonitorType + "')\">" + strUserName + "</a> ";
                          // }
                       }
                   },
                   { display: '采样协同人', name: 'SAMPLING_MAN', align: 'left', width: 200, minWidth: 60,
                       render: function (record, rowindex, value) {
                           var strSubTaskId = record.ID;
                           var strMonitorType = record.MONITOR_ID;
                           var strUserName = record.SAMPLING_MAN;
                           if (strUserName.length == 0)
                               strUserName = "请选择";
                           return "<a href=\"javascript:getDefaultUserExName('" + strSubTaskId + "','" + strMonitorType + "')\">" + strUserName + "</a> ";
                       }
                   },
                  { display: '退回意见', name: 'REMARK1', align: 'left', width: 110, minWidth: 60,
                      render: function (record, rowindex, value) {
                          return "<a href=\"javascript:showSuggestion('" + value + "')\">" + (value.length > 6 ? value.substring(0, 6) + "......" : value) + "</a> ";
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
    //发送到下一环节
    function SendToNext() {
        $.ligerDialog.confirm('您确定要将该任务发送至下一环节吗？', function (yes) {
            if (yes == true) {
                if (SendToNextFlow() == "1") {
                    toolArr = null;
                    //objTwoGrid.loadData();
                    $.ligerDialog.confirmWF('任务发送成功！', '提示', function (result) {
                        if (result) {
                            top.f_removeSelectedTabs();
                        }
                    });
                    return;
                }
                else {
                    $.ligerDialog.warn('任务发送失败');
                    return;
                }
            }
        });
    }
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
        columns: [
                    { display: '监测点位', name: 'POINT_ID', align: 'left', width: 160, minWidth: 60, render: function (record) {
                        return getMonitorPointName(record.POINT_ID);
                    }
                    },
                    { display: '样品名', name: 'SAMPLE_NAME', align: 'left', width: 260, minWidth: 60 },
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
//        toolbar: { items: [
//                { text: '现场平行', click: addQcTwinInfo, icon: 'add' },
//                { text: '密码平行', click: addQcSecretTwinInfo, icon: 'add' },
//                { text: '现场空白', click: addQcEmptyInfo, icon: 'add' },
//                { text: '现场加标', click: addQcAddInfo, icon: 'add' },
//                { text: '删除监测点', click: deleteSample, icon: 'delete' }
//                ]
//        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //现场平行质控添加
    function addQcTwinInfo() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择设置质控的监测点位');
            return;
        }
        if (objThreeGrid.getSelectedRow().QC_TYPE != "0") {
            $.ligerDialog.warn('只有原始样可以设置质控');
            return;
        }
        var strSampleID = objThreeGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: "现场平行设置", width: 450, height: 400, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.QcSave() == "1") {
                dialog.close();
                $.ligerDialog.success('质控保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('质控保存失败');
            }
            objThreeGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "QcTwinSetting.aspx?strSampleId=" + strSampleID + "&strQcType=3"
        });
    }
    //密码平行质控添加
    function addQcSecretTwinInfo() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择设置质控的监测点位');
            return;
        }
        if (objThreeGrid.getSelectedRow().QC_TYPE != "0") {
            $.ligerDialog.warn('只有原始样可以设置质控');
            return;
        }
        var strSampleID = objThreeGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: "密码平行设置", width: 450, height: 400, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.QcSave() == "1") {
                dialog.close();
                $.ligerDialog.success('质控保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('质控保存失败');
            }
            objThreeGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "QcTwinSetting.aspx?strSampleId=" + strSampleID + "&strQcType=4"
        });
    }
    //现场空白质控添加
    function addQcEmptyInfo() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择设置质控的监测点位');
            return;
        }
        if (objThreeGrid.getSelectedRow().QC_TYPE != "0") {
            $.ligerDialog.warn('只有原始样可以设置质控');
            return;
        }
        var strSampleID = objThreeGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: "现场空白设置", width: 450, height: 400, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.QcSave() == "1") {
                dialog.close();
                $.ligerDialog.success('质控保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('质控保存失败');
            }
            objThreeGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "QcEmptySetting.aspx?strSampleId=" + strSampleID + "&strQcType=1"
        });
    }
    //现场加标信息添加
    function addQcAddInfo() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择设置质控的监测点位');
            return;
        }
        if (objThreeGrid.getSelectedRow().QC_TYPE != "0") {
            $.ligerDialog.warn('只有原始样可以设置质控');
            return;
        }
        var strSampleID = objThreeGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: "现场加标设置", width: 450, height: 500, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.QcSave() == "1") {
                dialog.close();
                $.ligerDialog.success('质控保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('质控保存失败');
            }
            objThreeGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "QcAddSetting.aspx?strSampleId=" + strSampleID + "&strQcType=2"
        });
    }
    function deleteSample() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请先选择需要删除的监测点');
            return false;
        }
        $.ligerDialog.confirm('您确认删除已经选择的监测点吗？', function (yes) {
            if (yes == true) {
                var strSampleID = objThreeGrid.getSelectedRow().ID;
                var strQcType = objThreeGrid.getSelectedRow().QC_TYPE;
                if (strQcType == "0") {
                    $.ligerDialog.warn('原始样不允许删除');
                    return false;
                }
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: strUrl + "/deleteSample",
                    data: "{'strSampleID':'" + strSampleID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objThreeGrid.loadData();
                            $.ligerDialog.success('删除数据成功')
                        }
                        else {
                            $.ligerDialog.warn('删除数据失败');
                        }
                    }
                });
            }
        });
    }
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
                    }
                ],
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
//初始委托书信息模块
function SetContractInfo(data) {
    objOneFrom = $("#oneFrom").ligerForm({
        inputWidth: 160, labelWidth: 120, space: 30, labelAlign: 'right',
        fields: [
                { name: "ID", type: "hidden" },
                { display: "项目名称", name: "PROJECT_NAME", newline: true, type: "text", width: 400, group: "任务信息", groupicon: groupicon },
                { display: "任务单号", name: "TICKET_NUM", newline: true, width: 120, type: "text" },
                { display: "开始时间", name: "SAMPLE_ASK_DATE", newline: false, width: 120, type: "date" },
                { display: "采样要求完成时间", name: "SAMPLE_FINISH_DATE", newline: false, width: 120, type: "date" }
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
function getDefaultUserName(strSubTaskId, strMonitorType) {
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
        }], url: "../DefaultUser.aspx?strSubTaskId=" + strSubTaskId + "&strMonitorType=" + strMonitorType
    });
}
//弹出选择默认协同人
function getDefaultUserExName(strSubTaskId, strMonitorType) {
    $.ligerDialog.open({ title: "选择采样协同人", width: 400, height: 250, buttons:
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
        }], url: "../DefaultUserEx.aspx?strSubTaskId=" + strSubTaskId + "&strMonitorType=" + strMonitorType
    });
}
function SendClick() {
    var objSelectRows = objTwoGrid.data.Rows;
    for (var i = 0; i < objSelectRows.length; i++) {
        if (objSelectRows[i].SAMPLING_MANAGER_ID == "" || objSelectRows[i].SAMPLING_MANAGER_ID == "请选择") {
            var strMonitorName = getMonitorTypeName1(objSelectRows[i].MONITOR_ID)
            $.ligerDialog.warn("请选择【<a style='color:Red;font-weight:bold;'>" + strMonitorName + "</a>】类默认负责人！");
            return;
        }
    }
    $.ligerDialog.confirm('确定发送至下一环节吗？', function (yes) {
        if (yes == true) {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/btnSendClick",
                data: "{'strWorkTaskId':'" + strWorkTask_Id + "'}",
                //data: "{'strPlanID':'" + strPlanID + "','strSampleAskDate':'" + $("#SAMPLE_ASK_DATE").val() + "','strSampleFinishDate':'" + $("#SAMPLE_FINISH_DATE").val() + "','strTicketNum':'" + $("#TICKET_NUM").val() + "','strQCStatus':'" + strQCStatus + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    strValue = data.d;
                    var msg = "采样";
                    
                    if (strValue == "1") {
                        $.ligerDialog.confirmWF('任务发送成功！已发送到【<a style="color:Red;font-weight:bold">' + msg + '</a>】环节！', '提示', function (result) {
                            if (result) {
                                top.f_removeSelectedTabs();
                            }
                        });
                        return;
                    }
                    else {
                        $.ligerDialog.warn('发送失败！');
                        return;
                    }

                }
            });
        }
    });
}

//导出数据
function Export() {
    $.ligerDialog.confirm('您确定要导出监测工作任务通知单吗？', function (yes) {
        if (yes == true) {
            $("#strSAMPLE_ASK_DATE").val($("#SAMPLE_ASK_DATE").val());
            $("#strSAMPLE_FINISH_DATE").val($("#SAMPLE_FINISH_DATE").val());
            $("#btnImport").click();
        }
    });
}
//发送任务
function SendToNextFlow() {
    var isSuccess = false;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=SendToNext&planid=" + strPlanID + "&strSampleAskDate=" + $("#SAMPLE_ASK_DATE").val() + "&strSampleFinishDate=" + $("#SAMPLE_FINISH_DATE").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            isSuccess = data;
        }
    });
    return isSuccess;
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

//导出数据
function Export() {
    $.ligerDialog.confirm('您确定要导出工作任务通知单吗？', function (yes) {
        if (yes == true) {
            $("#hidTaskId").val(strTask_Id);
            $("#hidPlanId").val(strPlanID);
            $("#btnImport").click();
        }
    });
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
//设置grid 的弹出退回意见对话框
var SuggestionDialog = null;
function showSuggestion(value) {
    //创建表单结构
    $("#SuggForm").ligerForm({
        inputWidth: 430, labelWidth: 90, space: 40,
        fields: [
                     { display: "退回意见", name: "Suggestion", newline: true, type: "textarea" }
                    ]
    });
    $("#Suggestion").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:320px");

    $("#Suggestion").val(value);
    var ObjButton = [];

    $("#Suggestion").attr("disabled", true);
    ObjButton = [
                  { text: '返回', onclick: function () { SuggestionDialog.hide(); } }
                  ];
    SuggestionDialog = $.ligerDialog.open({
        target: $("#divSugg"),
        width: 560, height: 170, top: 90, title: "退回意见查看",
        buttons: ObjButton
    });
}