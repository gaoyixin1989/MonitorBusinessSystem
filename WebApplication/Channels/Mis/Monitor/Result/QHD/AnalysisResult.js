// Create by 熊卫华 2012.12.05  "分析结果录入"功能
var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;

var strUrl = "AnalysisResult.aspx";
var strOneGridTitle = "样品号";
var strResultStatus = "02";

//定义分析方法数据集对象
var ComboBoxValue = "";
var ComboBoxText = "";

//样品号
$(document).ready(function () {
    objOneGrid = $("#oneGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        enabledSort: false,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [10, 15, 20],
        height: '100%',
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 90, minWidth: 60 },
                { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 160, minWidth: 60 }
                ],
        toolbar: { items: [
                { text: '发送', click: SendToNext, icon: 'add' }
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
            //for (var rowid in this.records)
            //    this.unselect(rowid);
            //this.select(rowindex);

            //点击的时候加载监测项目信息
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID);
            objThreeGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    function SendToNext() {
        if (objOneGrid.getSelectedRows().length == 0) {
            $.ligerDialog.warn('发送之前请先选择样品');
            return;
        }
        var strSampleIDs = "";
        for (var i = 0; i < objOneGrid.getSelectedRows().length; i++) {
            strSampleIDs += objOneGrid.getSelectedRows()[i].ID + ",";
        }

        $.ligerDialog.confirm('你确定要发送该样品至下一环节？', function (yes) {
            if (yes == true) {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=SendToNext&strSimpleIds=" + strSampleIDs,
                    contentType: "application/json; charset=utf-8",
                    dataType: "text",
                    success: function (data, textStatus) {
                        var obj = eval(data)[0];
                        if (obj.result == "1") {
                            objOneGrid.loadData();
                            objTwoGrid.set("data", emptyArray);
                            objThreeGrid.set("data", emptyArray);
                            $.ligerDialog.success('样品发送成功')
                        }
                        else {
                            $.ligerDialog.warn('样品发送失败,' + obj.msg);
                        }
                    }
                });
            }
        });
    }
});
//监测项目信息
$(document).ready(function () {
    twoHeight = 3 * $(window).height() / 5;

    var ResultCompleteJson = [{ ResultValue: '0', ResultName: '未完成' }, { ResultValue: '1', ResultName: '完成'}]; ;

    objTwoGrid = $("#twoGrid").ligerGrid({
        dataAction: 'server',
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        height: twoHeight,
        usePager: false,
        enabledEdit: true,
        enabledSort: false,
        toolbar: { items: [
                { text: '发送', click: SendToNext, icon: 'add' },
                { text: '退回', click: GoToBack, icon: 'add' },
                { text: '完成分析', click: FinishAllItem, icon: 'up' }
                ]
        },
        columns: [
                 { display: '分析项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
                     return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                 }
                 },
                 { display: '分析完成情况', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60,
                     editor: {
                         type: 'select', valueColumnName: 'ResultValue', displayColumnName: 'ResultName',
                         ext:
                            function (rowdata) {
                                return {
                                    selectBoxWidth: 200,
                                    data: ResultCompleteJson,
                                    onSelected: function (value, text) {
                                        rowdata.ITEM_RESULT = value;
                                        rowdata.REMARK1 = text;

                                        if (this.data != null) {
                                            for (var i = 0; i < this.data.length; i++) {
                                                if (value == this.data[i]['ResultValue']) {
                                                    rowdata.REMARK1 = this.data[i]['ResultName'];
                                                }
                                            }
                                        }
                                    }
                                };
                            }
                     },
                     render: function (item) {
                         if (item.REMARK1 == "完成")
                             return "<span style=\"color:Red\">" + item.REMARK1 + "</span>";
                         else
                             return item.REMARK1;
                     }
                 },
                 { display: '分析方法', name: 'ANALYSIS_METHOD_ID', align: 'left', width: 200, minWidth: 60,
                     editor: {
                         type: 'select', valueColumnName: 'ID', displayColumnName: 'ANALYSIS_NAME',
                         ext:
                            function (rowdata) {
                                return {
                                    selectBoxWidth: 200,
                                    url: strUrl + "?type=getAnalysisByItemId&strItemId=" + rowdata.ITEM_ID,
                                    onSelected: function (value, text) {
                                        rowdata.ANALYSIS_METHOD_ID = value;
                                        rowdata.ANALYSIS_NAME = text;
                                        //查询最低检出线和仪器
                                        if (this.data != null) {
                                            for (var i = 0; i < this.data.length; i++) {
                                                if (value == this.data[i]['ID']) {
                                                    rowdata.LOWER_CHECKOUT = this.data[i]['LOWER_CHECKOUT'];
                                                    rowdata.APPARATUS_CODE = this.data[i]['APPARATUS_CODE'];
                                                    rowdata.APPARATUS_NAME = this.data[i]['INSTRUMENT_NAME'];
                                                    rowdata.METHOD_CODE = this.data[i]['METHOD_CODE'];
                                                }
                                            }
                                        }
                                    }
                                };
                            }
                     },
                     render: function (item) {
                         return item.ANALYSIS_NAME;
                     }
                 },
                 { display: '方法依据', name: 'METHOD_CODE', align: 'left', width: 200, minWidth: 60 },
                 { display: '检出限', name: 'LOWER_CHECKOUT', align: 'left', width: 100, minWidth: 60 },
                 { display: '单位', name: 'UNIT', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器名称及编码', name: 'APPARATUS_NAME', align: 'left', width: 200, minWidth: 60,
                     render: function (record, rowindex, value) {
                         if (record.APPARATUS_NAME != "" || record.APPARATUS_CODE != "")
                             return "" + record.APPARATUS_NAME + "(" + record.APPARATUS_CODE + ")";
                     }
                 },
                 { display: '仪器开始使用时间', name: 'APPARTUS_START_TIME', align: 'left', width: 150, minWidth: 60,
                     render: function (record, rowindex, value) {
                         var strResultId = record.ID;
                         var strBeginTime = getAnalysisResultDataTime(strResultId, "getStartTime");
                         var strAskingDateTemp = "";
                         if (strBeginTime != "")
                             strAskingDateTemp = strBeginTime;
                         else
                             strAskingDateTemp = "请选择";
                         return "<a href=\"javascript:ShowAppartusTime('" + strResultId + "','getStartTime')\">" + strAskingDateTemp + "</a> ";
                     }
                 },
                 { display: '仪器结束使用时间', name: 'APPARTUS_END_TIME', align: 'left', width: 150, minWidth: 60,
                     render: function (record, rowindex, value) {
                         var strResultId = record.ID;
                         var strBeginTime = getAnalysisResultDataTime(strResultId, "getEndTime");
                         var strAskingDateTemp = "";
                         if (strBeginTime != "")
                             strAskingDateTemp = strBeginTime;
                         else
                             strAskingDateTemp = "请选择";
                         return "<a href=\"javascript:ShowAppartusTime('" + strResultId + "','getEndTime')\">" + strAskingDateTemp + "</a> ";
                     }
                 },
                 { display: '要求完成时间', name: 'ASKING_DATE', align: 'left', width: 100, minWidth: 60 },
                 { display: '分析负责人', name: 'HEAD_USERID', align: 'left', width: 100, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUserName(record.HEAD_USERID);
                 }
                 },
                 { display: '分析协同人', name: 'ASSISTANT_USERID', align: 'left', width: 150, minWidth: 60,
                     render: function (record, rowindex, value) {
                         var objUserArray = getAjaxUseExNameEdit(record.ID);
                         var strUserName = "";
                         if (objUserArray.length > 0)
                             strUserName = objUserArray[0]["UserName"];
                         else
                             strUserName = "请选择";
                         return "<a href=\"javascript:getDefaultUserExName('" + record.ID + "')\">" + strUserName + "</a> ";
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

            //点击的时候加载内控信息
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID);
        },
        onAfterEdit: AfterEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    //发送到下一环节
    function SendToNext() {
        if (objTwoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('发送之前请先选择监测项目信息');
            return;
        }
        $.ligerDialog.confirm('您确定要将该监测项目发送至下一环节吗？', function (yes) {
            if (yes == true) {
                if (SendResultToNextFlow(objTwoGrid.getSelectedRow().ID) == "1") {
                    objTwoGrid.loadData();
                    objThreeGrid.set("data", emptyArray);
                    $.ligerDialog.success('任务发送成功')
                }
                else {
                    $.ligerDialog.warn('任务发送失败,请确定分析是否已完成');
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
    function GoToBack() {
        if (objTwoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('退回之前请先选择监测项目信息');
            return;
        }
        $.ligerDialog.confirm('您确认退回该监测项目吗？', function (yes) {
            if (yes == true) {
                //退回该项目
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=GoToBack&strResultId=" + objTwoGrid.getSelectedRow().ID,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data == "1") {
                            objTwoGrid.loadData();
                            objThreeGrid.set("data", emptyArray);
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
    function AfterEdit(e) {
        var id = e.record.ID;
        var strItemResult = e.record.ITEM_RESULT;
        var strAnalysisMethod = e.record.ANALYSIS_METHOD_ID;
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/saveItemInfo",
            data: "{'id':'" + id + "','strItemResult':'" + strItemResult + "','strAnalysisMethod':'" + strAnalysisMethod + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == 1) {
                    objTwoGrid.cancelEdit(e.rowindex);
                }
            }
        });
    }
});
//实验室质控信息
$(document).ready(function () {
    threeHeight = 2 * $(window).height() / 5 - 35;

    objThreeGrid = $("#threeGrid").ligerGrid({
        dataAction: 'server',
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        height: threeHeight,
        usePager: false,
        enabledSort: false,
        toolbar: { items: [
                { text: '实验室质控设置', click: QcSetting, icon: 'add' }
                ]
        },
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
        onAfterEdit: AfterEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //实验室质控设置
    function QcSetting() {
        if (!objTwoGrid.getSelectedRow()) {
            $.ligerDialog.warn('请先选择需要设置的监测项目');
            return;
        }
        $.ligerDialog.open({ title: "实验室质控设置", width: 250, height: 200, buttons:
        [
        { text:
        '保存', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.saveQcValue()) {
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
        }], url: "QcSetting_QHD.aspx?resultid=" + objTwoGrid.getSelectedRow().ID + "&result=" + objTwoGrid.getSelectedRow().ITEM_RESULT
        });
    }
    function AfterEdit(e) {

    }
});
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
//获取默认协同人名称
function getAjaxUseExNameEdit(strResultId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDefaultUserExNameEdit",
        data: "{'strResultId':'" + strResultId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return jQuery.parseJSON(strValue);
}
//获取分析仪器的使用时间
function getAnalysisResultDataTime(strResultId, type) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getAnalysisResultDataTime",
        data: "{'strResultId':'" + strResultId + "','type':'" + type + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//弹出选择分析完成时间
function ShowAppartusTime(strResultId, type) {
    var strTitle = "";
    if (type == "getStartTime")
        strTitle = "仪器开始使用时间";
    else
        strTitle = "仪器结束使用时间";
    $.ligerDialog.open({ title: strTitle, width: 400, height: 300, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.UserSave()) {
                dialog.close();
                objTwoGrid.loadData();
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
        }], url: "AnalysisResultDataTimeSetting.aspx?strResultId=" + strResultId + "&timeType=" + type
    });
}

//弹出选择默认协同人
function getDefaultUserExName(strResultId) {
    $.ligerDialog.open({ title: "选择分析协同人", width: 400, height: 250, buttons:
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
        }], url: "../DefaultUserEx.aspx?strResultId=" + strResultId
    });
}

//把所选样品的某个项目变成已完成
function FinishAllItem() {
    if (objOneGrid.getSelectedRows().length == 0) {
        $.ligerDialog.warn('请先选择样品');
        return;
    }
    if (objTwoGrid.getSelectedRow() == null) {
        $.ligerDialog.warn('请先选择监测项目');
        return;
    }

    var strSampleIDs = "";
    var strItemID = objTwoGrid.getSelectedRow().ITEM_ID;
    for (var i = 0; i < objOneGrid.getSelectedRows().length; i++) {
        strSampleIDs += objOneGrid.getSelectedRows()[i].ID + ",";
    }

    $.ligerDialog.confirm('你确定要把所选样品的【' + getItemInfoName(strItemID, "ITEM_NAME") + '】项目都改为已完成？', function (yes) {
        if (yes == true) {
            
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/FinishAllItem",
                data: "{'strSampleIDs':'" + strSampleIDs + "','strItemID':'" + strItemID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                        objTwoGrid.loadData();
                        $.ligerDialog.success('修改成功')
                    }
                    else {
                        $.ligerDialog.warn('修改失败');
                    }
                }
            });
        }
    });


}