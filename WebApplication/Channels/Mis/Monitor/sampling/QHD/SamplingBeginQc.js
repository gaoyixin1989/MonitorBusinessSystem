// Create by 熊卫华 2013.07.31  "采样前质控"功能
var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;
var objFourGrid = null;

var strUrl = "SamplingBeginQc.aspx";
var strOneGridTitle = "";
var strTwoGridTitle = "监测项目信息";
var strThreeGridTitle = "监测项目信息";

var strFlowCode = "duty_analyse";
var strResultStatus = "01";

//监测任务管理
$(document).ready(function () {
    topHeight = 2 * $(window).height() / 5;

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
        height: 220,
        enabledSort: false,
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 150, minWidth: 60 },
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 300, minWidth: 60 },
                 { display: '要求完成时间', name: 'ASKING_DATE', align: 'left', width: 100, minWidth: 60 }
                ],
        toolbar: { items: [
                { text: '发送', click: SendToNext, icon: 'add' },
                { text: '导出监测任务单', click: ExportExcel, icon: 'excel' },
                { text: '方案下载', click: downLoadFile, icon: 'filedown' }
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
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载监测类别信息
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID);
            objThreeGrid.set("data", emptyArray);
            objFourGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //发送到下一环节
    function SendToNext() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('发送之前请先选择任务信息');
            return;
        }
        $.ligerDialog.confirm('您确定要将该任务发送至下一环节吗？', function (yes) {
            if (yes == true) {
                if (SendToNextFlow(objOneGrid.getSelectedRow().ID) == "1") {
                    objOneGrid.loadData();
                    objTwoGrid.set("data", emptyArray);
                    objThreeGrid.set("data", emptyArray);
                    objFourGrid.set("data", emptyArray);
                    $.ligerDialog.success('发送成功,任务已发送至【<a style="color:Red;font-weight:bold">技术室主管审核</a>】环节！')
                }
                else {
                    $.ligerDialog.warn('任务发送失败');
                }
            }
        });
    }
});

///附件下载
function downLoadFile() {
    if (objOneGrid.getSelectedRow() == null) {
        $.ligerDialog.warn('请先选择任务信息');
        return;
    }
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270, top: 50,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../../OA/ATT/AttFileDownLoad.aspx?filetype=Scheme&id=' + objOneGrid.getSelectedRow().ID
    });
}

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
        //                    ,
        //                    { display: '默认负责人', name: 'SAMPLING_MANAGER_ID', align: 'left', width: 200, minWidth: 60,
        //                        render: function (record, rowindex, value) {
        //                            var strSubTaskId = record.ID;
        //                            var strMonitorType = record.MONITOR_ID;
        //                            var strUserName = "";
        //                            strUserName = record.SAMPLING_MANAGER_ID;
        //                            if (strTask_Id && strTask_Id != "" && strUserName == "") {
        //                                strUserName = GetContractDutyUser(strTask_Id, strMonitorType);
        //                            }
        //                            if (strUserName.length == 0)
        //                                strUserName = "请选择";
        //                            return strUserName;
        //                        }
        //                    },
        //                { display: '分析协同人', name: 'SAMPLING_MAN', align: 'left', width: 200, minWidth: 60,
        //                    render: function (record, rowindex, value) {
        //                        var strSubTaskId = record.ID;
        //                        var strMonitorType = record.MONITOR_ID;
        //                        var strUserName = record.SAMPLING_MAN;
        //                        if (strUserName.length == 0)
        //                            strUserName = "请选择";
        //                        return strUserName;
        //                    }
        //                }
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
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID);
            objFourGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});

//样品号管理
$(document).ready(function () {
    bottomHeight = 3 * $(window).height() / 5 - 35;

    objThreeGrid = $("#threeGrid").ligerGrid({
        dataAction: 'server',
        usePager: false,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        enabledSort: false,
        height: bottomHeight,
        enabledEdit: true,
        columns: [
                    { display: '监测点', name: 'POINT_ID', align: 'left', width: 150, minWidth: 60, render: function (record) {
                        return getPointName(record.POINT_ID);
                    }
                    },
                    { display: '样品名称', name: 'SAMPLE_NAME', align: 'left', width: 200, minWidth: 60, editor: {
                        type: 'text'
                    }
                    },
                    { display: '质控类别', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
                        return getQcName(record.QC_TYPE);
                    }
                    }
                ],
        toolbar: { items: [
                //{ text: '增加样品', click: AddSample, icon: 'add' },
                { text: '现场平行', click: addQcTwinInfo, icon: 'add' },
                { text: '现场空白', click: addQcEmptyInfo, icon: 'add' },
                { text: '删除监测点', click: deleteSample, icon: 'delete'}]
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
            //点击的时候加载监测项目信息
            objFourGrid.set('url', strUrl + "?type=getFourGridInfo&threeGridId=" + rowdata.ID);
        },
        onAfterEdit: SampleEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    function SampleEdit(e) {
        var strSampleId = e.record.ID;
        var strSampleName = e.record.SAMPLE_NAME
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/saveSample",
            data: "{'strSampleId':'" + strSampleId + "','strSampleName':'" + strSampleName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {

            }
        });
    }

    function AddSample() {
        $.ligerDialog.confirm('您确定要增加样品吗？', function (yes) {
            if (yes == true) {
                if (objThreeGrid.getSelectedRow() == null) {
                    $.ligerDialog.warn('增加样品之前请先选择原始样');
                    return;
                }

                var strSampleID = objThreeGrid.getSelectedRow().ID;
                var strQcType = objThreeGrid.getSelectedRow().QC_TYPE;

                if (strQcType != "0") {
                    $.ligerDialog.warn('质控样不允许增加');
                    return false;
                }

                $.ajax({
                    cache: false,
                    type: "POST",
                    url: strUrl + "/AddSample",
                    data: "{'strSampleID':'" + strSampleID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        objThreeGrid.loadData();
                    }
                });
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
        }], url: "QcTwinSetting.aspx?strSampleId=" + strSampleID + "&strQcType=3&iSampleCode=N"
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
        }], url: "QcEmptySetting.aspx?strSampleId=" + strSampleID + "&strQcType=1&iSampleCode=N"
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
                            objFourGrid.set("data", emptyArray);
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
    bottomHeight = 3 * $(window).height() / 5 - 35;

    objFourGrid = $("#fourGrid").ligerGrid({
        dataAction: 'server',
        usePager: false,
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        enabledSort: false,
        pageSizeOptions: [5, 8, 10],
        height: bottomHeight,
        columns: [
                    { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, render: function (record) {
                        return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
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

//发送任务
function SendToNextFlow(strTaskId) {
    var isSuccess = false;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=SendToNext&strTaskId=" + strTaskId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            isSuccess = data;
        }
    });
    return isSuccess;
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

function getPointName(strPointId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getPointName",
        data: "{'strPointId':'" + strPointId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
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
}
function ExportExcel() {
    var selected = objOneGrid.getSelectedRow();
    if (!selected) {
        $.ligerDialog.warn('请先选择一条记录！');
        return;
    }

    $("#cphData_hidFINISH_DATE").val(selected.ASKING_DATE);
    $("#cphData_hidPlanId").val(selected.PLAN_ID);
    $("#cphData_hidMonitorId").val("");
    $.ligerDialog.confirm('您确定要导出监测工作任务通知单吗？', function (yes) {
        if (yes == true) {
            $("#cphData_btnExport").click();
        }
    });
}