// Create by 苏成斌 2013.1.15  "样品交接"功能
var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;
var objFourGrid = null;

var strUrl = "SamplingAllocationSheet.aspx";
var strOneGridTitle = "任务信息";
var strTwoGridTitle = "监测项目信息";
var strThreeGridTitle = "监测项目信息";

var strTaskID = "";
var strFlowCode = "duty_analyse";
var strResultStatus = "01";

var strListQc1 = "", strListQc2 = "", strListQc3 = "", strListQc4 = "";
var strQc3Count = "", strQc4count = "";

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
        height: topHeight,
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 150, minWidth: 60 },
                { display: '委托类别', name: 'CONTRACT_TYPE', width: 100, minWidth: 60, render: function (record) {
                    return getDictName(record.CONTRACT_TYPE, "Contract_Type");
                }
                },
                { display: '项目名称', name: 'PROJECT_NAME', width: 300, minWidth: 60 },
                { display: '受检企业', name: 'TESTED_COMPANY_ID', width: 300, minWidth: 60, render: function (record) {
                    return getCompanyName(record.ID, record.TESTED_COMPANY_ID);
                }
                }
        //                ,
        //                { display: '报告要求完成时间', name: 'ASKING_DATE', width: 100, minWidth: 60 }
                ],
        toolbar: { items: [
                { text: '发送', click: SendToNext, icon: 'add' }//,
                //{ text: '样品交接单', click: Export, icon: 'add' },
                //{ text: '样品编码表', click: ExportCode, icon: 'add' }
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

            strTaskID = rowdata.ID;
            $("#cphData_strPrintId").val(strTaskID);
            $("#cphData_strPrintId_code").val(strTaskID);

            //点击的时候加载监测类别信息
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID);
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID + "&TaskID=" + strTaskID);
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
                    $.ligerDialog.success('任务发送成功')
                }
                else {
                    $.ligerDialog.warn('任务发送失败');
                }
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
        width: 200,
        height: bottomHeight,
        columns: [
                    { display: '监测类别', name: 'MONITOR_ID', align: 'left', width: 150, minWidth: 60, render: function (record) {
                        return getMonitorTypeName(record.MONITOR_ID);
                    }
                    }
                ],
        toolbar: { items: [
                { text: '交接单', click: Export, icon: 'add' },
                { text: '编码表', click: ExportCode, icon: 'add' }
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

            //点击的时候加载监测项目信息
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID + "&TaskID=" + strTaskID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});

//导出数据
function Export() {
    if (objTwoGrid.getSelectedRow() == null) {
        $.ligerDialog.warn('导出之前请先选择监测类别');
        return;
    }
    $.ligerDialog.confirm('您确定要导出样品交接单吗？', function (yes) {
        if (yes == true) {
            var strPrintId = objTwoGrid.getSelectedRow().ID;

            $("#cphData_strPrintId").val(strPrintId);
            $("#cphData_btnImport").click();
        }
    });
}
//导出数据
function ExportCode() {
    if (objTwoGrid.getSelectedRow() == null) {
        $.ligerDialog.warn('导出之前请先选择监测类别');
        return;
    }
    $.ligerDialog.confirm('您确定要导出样品编码表吗？', function (yes) {
        if (yes == true) {
            var strPrintId = objTwoGrid.getSelectedRow().ID;
            
            $("#cphData_strPrintId_code").val(strPrintId);
            $("#cphData_btnImportCode").click();
        }
    });
}

//监测样品信息
$(document).ready(function () {
    bottomHeight = 3 * $(window).height() / 5 - 35;
    var SampleCheckResultJson = [{ SampleCheckValue: '0', SampleCheckName: '不合格' }, { SampleCheckValue: '1', SampleCheckName: '合格'}];

    objThreeGrid = $("#threeGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        enabledEdit: true,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [5, 8, 10],
        height: bottomHeight,
        columns: [
                    { display: '采样点位', name: 'SAMPLE_NAME', width: 200, minWidth: 60 },
                    { display: '分析项目', name: 'ITEM_NAME', width: 300, minWidth: 60 },
                    { display: '采样容器', name: 'INSTRUMENT_NAME', width: 180, minWidth: 60 },
                    //{ display: '样品状态', name: 'IS_OK', width: 100, minWidth: 60,
                    //    render: function (item) {
                            //if (item.QC_TYPE == '0')
                    //            return item.IS_OK;
                            //else
                            //    return "";
                    //    }
                    //},
                    { display: '样品核查结果', name: 'REMARK1', align: 'left', width: 100, minWidth: 60,
                        editor: {
                            type: 'select', valueColumnName: 'SampleCheckValue', displayColumnName: 'SampleCheckName',
                            ext:
                            function (rowdata) {
                                return {
                                    selectBoxWidth: 100,
                                    data: SampleCheckResultJson,
                                    onSelected: function (value, text) {
                                        rowdata.REMARK1 = value;
                                        rowdata.SAMPLE_CHECK_NAME = text;
                                        if (this.data != null) {
                                            for (var i = 0; i < this.data.length; i++) {
                                                if (value == this.data[i]['SampleCheckValue']) {
                                                    rowdata.SAMPLE_CHECK_NAME = this.data[i]['SampleCheckName'];
                                                }
                                            }
                                        }
                                    }
                                };
                            }
                        },
                        render: function (item) {
                            if (item.SAMPLE_CHECK_NAME == "合格")
                                return "<span style=\"color:Red\">" + item.SAMPLE_CHECK_NAME + "</span>";
                            else
                                return item.SAMPLE_CHECK_NAME;
                        }
                    },
                    { display: '样品编码', name: 'SAMPLE_CODE', align: 'left', width: 100, minWidth: 60,
                        editor: {
                            type: 'text'
                        }
                    },
                    { display: '采样人', name: 'SAMPLING_MANAGER_NAME', width: 100, minWidth: 60,
                        render: function (item) {
                            //if (item.QC_TYPE == '0')
                                return item.SAMPLING_MANAGER_NAME;
                            //else
                            //    return "";
                        }
                    }
                ],
        toolbar: { items: [
                { text: '现场平行', click: addQcTwinInfo, icon: 'add' },
                { text: '现场空白', click: addQcEmptyInfo, icon: 'add' },
                { text: '样品加标', click: addQcAddInfo, icon: 'add' },
                { text: '实验室平行', click: addQcTwinInfo_QHD, icon: 'add' },
                { text: '空白加标', click: addQcEmptyAdd_QHD, icon: 'add' },
                { text: '删除样品', click: deleteSample, icon: 'delete' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onAfterEdit: AfterEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    function deleteSample() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请先选择需要删除的样品');
            return false;
        }
        $.ligerDialog.confirm('您确认删除已经选择的样品吗？', function (yes) {
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
        }], url: "QcTwinSetting.aspx?strSampleId=" + strSampleID + "&strQcType=3&iSampleCode=Y"
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
        }], url: "QcEmptySetting.aspx?strSampleId=" + strSampleID + "&strQcType=1&iSampleCode=Y"
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
        $.ligerDialog.open({ title: "样品加标设置", width: 500, height: 500, isHidden: false, buttons:
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
    //质控平行增加
    function addQcTwinInfo_QHD() {
        if (objTwoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择监测类别信息');
            return;
        }
        var strSubTaskId = objTwoGrid.getSelectedRow().ID;

        $.ligerDialog.open({ title: "实验室平行设置", width: 450, height: 400, isHidden: false, buttons:
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
        }], url: "QcTwinSetting_QHD.aspx?strSubTaskId=" + strSubTaskId + "&strQcType=9"
        });
    }
    //空白加标增加
    function addQcEmptyAdd_QHD() {
        if (objTwoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择监测类别信息');
            return;
        }
        var strSubTaskId = objTwoGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: "空白加标设置", width: 450, height: 400, isHidden: false, buttons:
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
        }], url: "QcEmptyAddSetting_QHD.aspx?strSubTaskId=" + strSubTaskId + "&strQcType=10"
        });
    }
    function SaveQcData(strSampleID) {
        $.ajax({
            cache: false,
            type: "POST",
            url: strUrl + "/saveQc",
            data: "{'strSampleID':'" + strSampleID + "','strListQc1':'" + strListQc1 + "','strListQc2':' " + strListQc2 + "','strListQc3':' " + strListQc3 + "','strListQc4':' " + strListQc4 + "','strQc3Count':' " + strQc3Count + "','strQc4Count':' " + strQc4Count + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    objThreeGrid.loadData();
                    $.ligerDialog.success('保存数据成功')
                }
                else {
                    $.ligerDialog.warn('保存数据失败');
                }
            }
        });
    }
    function AfterEdit(e) {
        var id = e.record.ID;
        var strSampleCode = e.record.SAMPLE_CODE;
        var strSampleCheckOk = e.record.REMARK1;

        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/saveSampleCode",
            data: "{'id':'" + id + "','strSampleCode':'" + strSampleCode + "','strSampleCheckOk':'" + strSampleCheckOk + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == 1) {
                    //objThreeGrid.cancelEdit(e.rowindex);
                }
            }
        });
    }
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