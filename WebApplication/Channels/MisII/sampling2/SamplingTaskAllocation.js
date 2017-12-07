// Create by 苏成斌 2012.12.6  "采样任务分配"功能
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
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
var strPlanID = "", strWorkTask_Id = "", strTask_Id = "", strTaskType = "";
var CCFLOW_WORKID = "";
var objTaskInfo = null;
var strSubtaskID = "";
var strContractPointId = "", strUpdateID = "";

var strCurrentMonitorId = '';
var strCurrentSubTaskId = '';

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

    alert('dddd');
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
    $.post('SamplingTaskAllocation.aspx?type=getContractInfo&id=' + $("#ID").val() + '&planid=' + strPlanID + '&strWorkTask_Id=' + strWorkTask_Id, function (data) {
        SetContractInfo(data);
    }, 'json');

});

//采样任务分配信息
$(document).ready(function () {
    var gridHeight = $(window).height() / 2 - 200;

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
        url: strUrl + '?type=getTwoGridInfo&planid=' + strPlanID + '&strWorkTask_Id=' + strWorkTask_Id,
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

                                var strContractType = getContractType(record.TASK_ID);
                                //if (strContractType == "05") {
                                //return strUserName;
                                //}
                                //else {
                                if (strUserName.length == 0)
                                    strUserName = "请选择";
                                return "<a href=\"javascript:getDefaultUserName('" + strWorkTask_Id + "','" + strSubTaskId + "','" + strMonitorType + "')\">" + strUserName + "</a> ";
                                //}
                            }
                        },
                        { display: '采样协同人', name: 'SAMPLING_MAN', align: 'left', width: 200, minWidth: 60,
                            render: function (record, rowindex, value) {
                                var strSubTaskId = record.ID;
                                var strMonitorType = record.MONITOR_ID;
                                var strUserName = record.SAMPLING_MAN;
                                if (strUserName.length == 0)
                                    strUserName = "请选择";
                                return "<a href=\"javascript:getDefaultUserExName('" + strWorkTask_Id + "','" + strSubTaskId + "','" + strMonitorType + "')\">" + strUserName + "</a> ";
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

            strSubtaskID = rowdata.ID;
            strCurrentMonitorId = rowdata.MONITOR_ID;
            strCurrentSubTaskId = rowdata.ID;

            //点击的时候加载监测点位信息
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID + '&planid=' + strPlanID + '&strTicketNum=' + encodeURI($("#TICKET_NUM").val()));
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
        //                    { display: '样品条码', name: 'SAMPLE_BARCODE', align: 'left', width: 100, minWidth: 60,
        //                        editor: {
        //                            type: 'text'
        //                        }
        //                    },
                    {display: '质控类别', name: 'REMARK1', align: 'left', width: 80, minWidth: 60 },
                    { display: '主采人', name: 'REMARK5', align: 'left', width: 100, minWidth: 60,
                        render: function (record, rowindex, value) {
                            var strSubTaskId = record.SUBTASK_ID;
                            var strMonitorType = strCurrentMonitorId;
                            var strUserName = "";
                            strUserName = record.REMARK5;
                            //if (strTask_Id && strTask_Id != "" && strUserName == "") {
                            //    strUserName = GetContractDutyUser(strTask_Id, strMonitorType);
                            //}

                            //var strContractType = getContractType(record.TASK_ID);
                            //主采人
                            if (strUserName.length == 0)
                                strUserName = "请选择";


                            return "<a href=\"javascript:getDefaultUserName2('" + strWorkTask_Id + "','" + strSubTaskId + "','" + strMonitorType + "','" + record.ID + "')\">" + strUserName + "</a> ";

                        }
                    },
                     { display: '协同人', name: 'REMARK4', align: 'left', width: 150, minWidth: 60,
                         render: function (record, rowindex, value) {
                             var strSubTaskId = record.SUBTASK_ID;
                             var strMonitorType = strCurrentMonitorId;
                             var strUserName = record.REMARK4;
                             if (strUserName.length == 0)
                                 strUserName = "请选择";
                             return "<a href=\"javascript:getDefaultUserExName2('" + strWorkTask_Id + "','" + strSubTaskId + "','" + strMonitorType + "','" + record.ID + "')\">" + strUserName + "</a> ";
                         }
                     }
                ],
        toolbar: { items: [
                { text: '点位增加', click: createData, icon: 'add' },
                { text: '点位修改', click: updateData, icon: 'modify' },
                { text: '现场平行', click: addQcTwinInfo, icon: 'add' },
                { text: '现场空白', click: addQcEmptyInfo, icon: 'add' },
                { text: '现场加标', click: addQcAddInfo, icon: 'add' },
                { text: '现场密码', click: addQcBlindInfo, icon: 'add' },
                { text: '删除监测点', click: deleteSample, icon: 'delete' },
                  { text: '设置主采人', click: setMain, icon: 'add' },
                   { text: '设置协同人', click: setCoordination, icon: 'add' }
            //                { text: '打印条码', click: printBarCode, icon: 'delete' }
                ]
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
        onSelectRow: function (rowdata, rowid, rowobj) {
            //strContractPointId = rowdata.POINT_ID;
            //strUpdateID = rowdata.SUBTASK_ID;
            //点击的时候加载监测项目信息
            //objFourGrid.set('url', strUrl + "?type=getFourGridInfo&ThreeGridId=" + rowdata.ID + "&QcType=" + rowdata.QC_TYPE + '&planid=' + strPlanID);
        },
        onUnSelectRow: function (rowdata, rowid, rowobj) {

        },
        onCheckRow: function (checked, rowdata, rowindex) {
            //for (var rowid in this.records)
            //  this.unselect(rowid);
            //this.select(rowindex);

            strContractPointId = rowdata.POINT_ID;
            strUpdateID = rowdata.SUBTASK_ID;
            //点击的时候加载监测项目信息
            objFourGrid.set('url', strUrl + "?type=getFourGridInfo&ThreeGridId=" + rowdata.ID + "&QcType=" + rowdata.QC_TYPE + '&planid=' + strPlanID);
        },
        onAfterEdit: AfterEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return true; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //修改事件 黄进军 添加 20150417
    function AfterEdit(e) {
        var id = e.record.ID;
        var SAMPLE_CODE = e.record.SAMPLE_CODE;
        var SAMPLE_BARCODE = e.record.SAMPLE_BARCODE;
        //alert(id + "aaaaa")
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/saveDataInfo",
            data: "{'id':'" + id + "','SAMPLE_CODE':'" + SAMPLE_CODE + "','SAMPLE_BARCODE':'" + SAMPLE_BARCODE + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

            }
        });
    }

    //黄飞  20150820  采样任务分配中  点位增加修改
    //增加点位
    function createData() {
        var strMonitor_ID = "";
        //alert(strSubtaskID);
        $.ligerDialog.open({ title: '点位信息增加', top: 0, width: 780, height: 520, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../../Mis/Monitor/sampling/QY/SamplePointEdit.aspx?strSubtaskID=' + strSubtaskID + '&strMonitorID=' + strMonitor_ID
        });
    }
    //黄飞  20150820  采样任务分配中  点位增加修改
    //修改数据
    function updateData() {
        var strPointId = strContractPointId;
        //alert(strPointId + "|" + strUpdateID);
        //        var strSubtaskID = objThreeGrid.getSelectedRow().ID;
        if (strPointId == "") {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: '点位信息编辑', top: 0, width: 780, height: 520, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../../Mis/Monitor/sampling/QY/SamplePointEdit.aspx?strid=' + strPointId + '&strSubtaskID=' + strUpdateID
        });

    }

    //黄飞  20150820  采样任务分配中  点位增加修改
    //save函数
    function f_SaveDate(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();
        //alert(strdata);
        $.ajax({
            cache: false,
            type: "POST",
            url: "SamplingTaskAllocation.aspx/SaveData",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    $.ligerDialog.success('数据保存成功');
                    dialog.close();
                    objThreeGrid.loadData();
                }
                else {
                    $.ligerDialog.warn('数据保存失败');
                }
                objThreeGrid.loadData();
            }
        });
    }




    //现场平行质控添加
    function addQcTwinInfo() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择设置质控的样品');
            return;
        }
        if (objThreeGrid.getSelectedRow().QC_TYPE != "0") {
            $.ligerDialog.warn('只有原始样可以设置质控');
            return;
        }
        var strSampleID = objThreeGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: "现场平行设置", width: 450, height: 400, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[1].contentWindow.QcSave() == "1") {
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
        }], url: "../../Mis/Monitor/sampling/QY/QcTwinSetting.aspx?strSampleId=" + strSampleID + "&strQcType=3"
        });
    }

    //密码平行质控添加
    function addQcSecretTwinInfo() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择设置质控的样品');
            return;
        }
        if (objThreeGrid.getSelectedRow().QC_TYPE != "0") {
            $.ligerDialog.warn('只有原始样可以设置质控');
            return;
        }
        var strSampleID = objThreeGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: "平行密码设置", width: 450, height: 400, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[1].contentWindow.QcSave() == "1") {
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
        }], url: "../../Mis/Monitor/sampling/QY/QcTwinSetting.aspx?strSampleId=" + strSampleID + "&strQcType=4"
        });
    }

    //现场空白质控添加
    function addQcEmptyInfo() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择设置质控的样品');
            return;
        }
        if (objThreeGrid.getSelectedRow().QC_TYPE != "0") {
            $.ligerDialog.warn('只有原始样可以设置质控');
            return;
        }
        var strSampleID = objThreeGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: "现场空白设置", width: 450, height: 400, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[1].contentWindow.QcSave() == "1") {
                dialog.close();
                $.ligerDialog.success('质控保存成功');
            }
            else {
                dialog.close();
                $.ligerDialog.warn('质控保存失败');
            }
            objThreeGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../Mis/Monitor/sampling/QY/QcEmptySetting.aspx?strSampleId=" + strSampleID + "&strQcType=1"
        });
    }

    //现场加标信息添加
    function addQcAddInfo() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择设置质控的样品');
            return;
        }
        if (objThreeGrid.getSelectedRow().QC_TYPE != "0") {
            $.ligerDialog.warn('只有原始样可以设置质控');
            return;
        }
        var strSampleID = objThreeGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: "现场加标设置", width: 450, height: 500, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[1].contentWindow.QcSave() == "1") {
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
        }], url: "../../Mis/Monitor/sampling/QY/QcAddSetting.aspx?strSampleId=" + strSampleID + "&strQcType=2"
        });
    }
    //标准盲样添加
    function addQcBlindInfo() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择监测类别信息');
            return;
        }
        var strSubTaskId = objThreeGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: "现场密码设置", width: 450, height: 500, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[1].contentWindow.QcSave() == "1") {
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
        }], url: "../../Mis/Monitor/sampling/QY/QcBlindSetting.aspx?strSubTaskId=" + strSubTaskId + "&strQcType=11"
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
                            $.ligerDialog.success('删除数据成功');
                            return;
                        }
                        else {
                            $.ligerDialog.warn('删除数据失败');
                            return
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
        toolbar: { items: [
                { id: 'setting', text: '监测项目设置', click: SetData_Item, icon: 'database_wrench' }
                ]
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

//初始委托书信息模块
function SetContractInfo(data) {
    objOneFrom = $("#oneFrom").ligerForm({
        inputWidth: 160, labelWidth: 100, space: 30, labelAlign: 'right',
        fields: [
                { name: "ID", type: "hidden" },
                { display: "项目名称", name: "PROJECT_NAME", newline: true, type: "text", width: 300, group: "任务信息", groupicon: groupicon },
                { display: "任务单号", name: "TICKET_NUM", newline: false, width: 300, type: "text" }
        //                { display: "开始时间", name: "SAMPLE_ASK_DATE", newline: false, width: 120, type: "date" },
        //                { display: "要求完成时间", name: "SAMPLE_FINISH_DATE", newline: false, width: 120, type: "date" }
                ]
    });
    //赋值
    if (data) {
        $("#PROJECT_NAME").val(data.PROJECT_NAME);
        $("#TICKET_NUM").val(data.TICKET_NUM);
        //        var strAskFinishDate = "";
        //        if (data.ASKING_DATE != "") {
        //            strAskFinishDate = new Date(Date.parse(data.ASKING_DATE.replace(/-/g, '/')));
        //            strAskFinishDate = TogetDate(strAskFinishDate);
        //        }
        //        else {
        //            var d1 = new Date();
        //            d1.setDate(d1.getDate() + 7);
        //            strAskFinishDate = TogetDate(d1);

        //        }
        //        $("#SAMPLE_FINISH_DATE").val(strAskFinishDate);
    }

    $("#PROJECT_NAME").ligerGetComboBoxManager().setDisabled();
    $("#TICKET_NUM").ligerGetComboBoxManager().setDisabled();
    //    $("#SAMPLE_ASK_DATE").val(currentTime())

    //    //JS 获取当前时间
    //    function currentTime() {
    //        var d = new Date(), str = '';
    //        str += d.getFullYear() + '-';
    //        str += d.getMonth() + 1 + '-';
    //        str += d.getDate();
    //        return str;
    //    }
    //    function currentAskingTime() {
    //        var b = new Date();
    //        var c = new Date(b.getFullYear(), b.getMonth(), b.getDate() + 7);
    //        str = c.getFullYear() + "-" + (c.getMonth() + 1) + "-" + c.getDate()
    //        return str;
    //    }
}
//弹出选择默认负责人
function getDefaultUserName(strTaskId, strSubTaskId, strMonitorType) {
    $.ligerDialog.open({ title: "选择采样负责人", width: 400, height: 250, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[1].contentWindow.UserSave()) {
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
function getDefaultUserName2(strTaskId, strSubTaskId, strMonitorType, strSampleIds) {
    $.ligerDialog.open({ title: "选择主采人", width: 400, height: 250, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[1].contentWindow.UserSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }

            objThreeGrid.loadData();

        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../Mis/Monitor/sampling/DefaultUser2.aspx?strSubTaskId=" + strSubTaskId + "&strMonitorType=" + strMonitorType + "&strTaskId=" + strTaskId + "&strSampleIds=" + strSampleIds
    });
}


//弹出选择默认协同人
function getDefaultUserExName(strTaskId, strSubTaskId, strMonitorType) {
    $.ligerDialog.open({ title: "选择采样协同人", width: 400, height: 250, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[1].contentWindow.UserSave()) {
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

//弹出选择默认协同人
function getDefaultUserExName2(strTaskId, strSubTaskId, strMonitorType, strSampleIds) {
    $.ligerDialog.open({ title: "选择采样协同人", width: 400, height: 250, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[1].contentWindow.UserSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功');
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据保存失败');
                return;
            }
            objThreeGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../Mis/Monitor/sampling/DefaultUserEx2.aspx?strSubTaskId=" + strSubTaskId + "&strMonitorType=" + strMonitorType + "&strTaskId=" + strTaskId + "&strSampleIds=" + strSampleIds
    });
}

//导出数据
function Export() {
    $.ligerDialog.confirm('您确定要导出监测工作任务通知单吗？', function (yes) {
        if (yes == true) {
            //$("#strSAMPLE_ASK_DATE").val($("#SAMPLE_ASK_DATE").val());
            //$("#strSAMPLE_FINISH_DATE").val($("#SAMPLE_FINISH_DATE").val());
            $("#PLAN_ID").val(strPlanID);
            $("#btnImport").click();
        }
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

//设置监测项目
function SetData_Item() {
    var selectedPoint = objThreeGrid.getSelectedRow();
    if (!selectedPoint) {
        $.ligerDialog.warn('请先选择监测点位！');
        return;
    }

    if (objThreeGrid.getSelecteds().length > 1) {

        $.ligerDialog.warn('设置监测项目时，请选择一个监测点位进行设置！');

        $(objThreeGrid.getSelecteds()).each(function (index, item) {

            objThreeGrid.unselect(item);
        });

        return;
    }

    // alert(selectedPoint.ID  + "|" + selectedPoint.SUBTASK_ID);
    $.ligerDialog.open({ title: '设置监测项目', top: 0, width: 500, height: 380, buttons:
        [{ text: '确定', onclick: f_SaveDateItem },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../../Mis/Monitor/sampling/SamplePointItemEdit.aspx?PointID=' + selectedPoint.ID + '&SubtaskID=' + selectedPoint.SUBTASK_ID
    });
}


//save函数
function f_SaveDateItem(item, dialog) {
    var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
    var strdata = fn();

    strdata = "{'strSubtaskID':'" + strSubtaskID + "'," + strdata + "}";
    $.ajax({
        cache: false,
        type: "POST",
        url: "SamplingTaskAllocation.aspx/SaveDataItem",
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                var selectedPoint = objThreeGrid.getSelectedRow();
                $.ligerDialog.success('数据保存成功');
                objFourGrid.loadData();
                dialog.close();
                //ItemGrid.set('url', "SamplePoint.aspx?type=GetItems&selPointID=" + selectedPoint.POINT_ID + "&strSampleID=" + selectedPoint.ID);

            }
            else {
                $.ligerDialog.warn('数据保存失败');
            }
        }
    });
}






function printBarCode() {
    if (objThreeGrid.getSelectedRow() == null) {
        $.ligerDialog.warn('请先选择需要打印的监测点');
        return false;
    }
    var strProjectName = $("#TICKET_NUM").val();
    var strSampleName = objThreeGrid.getSelectedRow().SAMPLE_NAME;
    var strSampleCode = objThreeGrid.getSelectedRow().SAMPLE_CODE;
    var strBarCode = objThreeGrid.getSelectedRow().SAMPLE_BARCODE;
    //alert(strProjectName + '|' + strSampleName + '|' + strSampleCode + '|' + strBarCode);
    $("#btn_Ok").click();
    //    $.ajax({
    //        cache: false,
    //        async: false, //设置是否为异步加载,此处必须
    //        type: "POST",
    //        url: strUrl + "?type=BarCodePrint&strProjectName=" + strProjectName + "&strSampleCode=" + strSampleCode + "&strBarCode=" + strBarCode,
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "text",
    //        success: function (data) {
    //            
    //        },
    //        error: function (msg) {
    //            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
    //        }
    //    });


    //    $.ajax({
    //        cache: false,
    //        async: false,
    //        type: "POST",
    //        url: strUrl + "/btnPrintClick",
    //        data: "{'strProjectName':'" + strProjectName + "','strSampleName':'" + strSampleName + "','strSampleCode':'" + strSampleCode + "','strBarCode':'" + strBarCode + "'}",
    //        contentType: "application/json; charset=utf-8",
    //        success: function (data, textStatus) {
    //            
    //        }
    //    });
}


var nFreshCount = 0;
$(document).ready(function () {
    //by yinchengyi 2015-9-16 时限信息
    if (0 == nFreshCount) {
        var newDiv = '<iframe frameborder="0" height="200" width="800" name="showmessagefgfgf" src= "../ProcessMgm/ProcessMgm.aspx?strPlanId=' + strPlanID + '"></iframe>';
        $(newDiv).appendTo(createDiv);

        nFreshCount = 1;
    }

});


function setMain() {

    if (objThreeGrid.getSelecteds().length == 0) {

        $.ligerDialog.warn('请选择点位！');
        return;
    }

    var strSampleIds = '';

    $(objThreeGrid.getSelecteds()).each(function (index, item) {

        strSampleIds += item.ID + ',';

    });

    getDefaultUserName2(strWorkTask_Id, strCurrentSubTaskId, strCurrentMonitorId, strSampleIds);

}

function setCoordination() {

    if (objThreeGrid.getSelecteds().length == 0) {

        $.ligerDialog.warn('请选择点位！');
        return;
    }

    var strSampleIds = '';

    $(objThreeGrid.getSelecteds()).each(function (index, item) {

        strSampleIds += item.ID + ',';

    });

    getDefaultUserExName2(strWorkTask_Id, strCurrentSubTaskId, strCurrentMonitorId, strSampleIds);

}

