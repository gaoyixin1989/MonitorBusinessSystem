// Create by 熊卫华 2013.05.08  "分析结果录入"功能
var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;

var strUrl = "AnalysisResult.aspx";
var strOneGridTitle = "样品号";
var strResultStatus = "02";

//定义分析方法数据集对象
var ComboBoxValue = "";
var ComboBoxText = "";

//var strPlanID = "", strTask_Id = "";

//样品号
$(document).ready(function () {
    objOneGrid = $("#oneGrid").ligerGrid({
        dataAction: 'server',
        usePager: false,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        height: '100%',
        enabledSort: false,
        pageSizeOptions: [10, 15, 20],
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                 { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 130, minWidth: 60 },
                 { display: '全程质控', name: 'ENTIRE_QC', align: 'left', width: 50, minWidth: 60, render: function (record) {
                     if (record.ENTIRE_QC == "1")
                         return "是";
                     else
                         return "否 ";
                 }
                 },
                 { display: '样品描述', name: 'REMARK3', align: 'left', width: 130, minWidth: 60 }
        //{ display: '领取', name: 'REMARK1', align: 'left', width: 50, minWidth: 60, render: function (record) {
        //    if (getReceiveSampleStatus(record.ID) == "1")
        //        return "<font color='red'>已领取</font>";
        //    else
        //        return "未领取";
        // }
        //}
                ],
        toolbar: { items: [
            //{ text: '发送', click: SendToNext, icon: 'add' },
            //{ text: '领取', click: setReceiveSample, icon: 'add' }
                {text: '查看质控要求', click: ZKYQ, icon: 'add' }
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
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID);
            objThreeGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //质控要求
    function ZKYQ() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请先选择样品');
            return;
        }

        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "?type=ZKYQ&strSimpleId=" + objOneGrid.getSelectedRow().ID,
            contentType: "application/json; charset=utf-8",
            dataType: "text",
            success: function (data, textStatus) {
                if (data !=null ) {
                    $.ligerDialog.open({ title: '监测质控要求', name: 'winaddtor', width: 700, height: 300, top: 90, url: '../../sampling/SamplingQCStep.aspx?strTask_Id=' + eval(data)[0].TASK_ID + '&strPlanId=' + eval(data)[0].PLAN_ID, buttons: [
                    { text: '取消', onclick: f_Cancel }
                    ]
                    });
                }
            }
        });
    }

    function SendToNext() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('发送之前请先选择样品');
            return;
        }
        $.ligerDialog.confirm('你确定要发送该样品至下一环节？', function (yes) {
            if (yes == true) {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=SendToNext&strSimpleId=" + objOneGrid.getSelectedRow().ID,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data == "1") {
                            objOneGrid.loadData();
                            objTwoGrid.set("data", emptyArray);
                            objThreeGrid.set("data", emptyArray);
                            $.ligerDialog.success('样品发送成功')
                        }
                        else {
                            $.ligerDialog.warn('样品发送失败,请检查结果是否已录入');
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
                { text: '退回', click: GoToBack, icon: 'add' }
                ]
        },
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, render: function (record) {
                     return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                 }
                 },
        //                 { display: '原始单号', name: 'REMARK_2', align: 'left', width: 150, minWidth: 60,
        //                     editor: {
        //                         type: 'text'
        //                     }
        //                 },
                 {display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60,
                 editor: {
                     type: 'text'
                 }
             },
                 { display: '方法依据', name: 'METHOD_CODE', align: 'left', width: 200, minWidth: 60 },
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
                 { display: '检出限', name: 'LOWER_CHECKOUT', align: 'left', width: 100, minWidth: 60 },
                 { display: '单位', name: 'UNIT', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器编号', name: 'APPARATUS_CODE', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器', name: 'APPARATUS_NAME', align: 'left', width: 200, minWidth: 60 },
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
            //            for (var rowid in this.records)
            //                this.unselect(rowid);
            //            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            //            for (var rowid in this.records)
            //                this.unselect(rowid);
            //            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            //            for (var rowid in this.records)
            //                this.unselect(rowid);
            //            this.select(rowindex);

            //点击的时候加载内控信息
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID);
        },
        onAfterEdit: AfterEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return true; }
    });
    //$(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    //发送到下一环节
    function SendToNext() {
        if (objTwoGrid.getSelectedRows().length == 0) {
            $.ligerDialog.warn('发送之前请先选择监测项目信息');
            return;
        }
        var IDs = '';
        for (var i = 0; i < objTwoGrid.getSelectedRows().length; i++) {
            if (i == objTwoGrid.getSelectedRows().length - 1)
                IDs += objTwoGrid.getSelectedRows()[i].ID;
            else
                IDs += objTwoGrid.getSelectedRows()[i].ID + ",";
        }

        $.ligerDialog.confirm('您确定要将该监测项目发送至下一环节吗？', function (yes) {
            if (yes == true) {
                if (SendResultToNextFlow(IDs) == "1") {
                    objTwoGrid.loadData();
                    if (objTwoGrid.rows.length == 0) {
                        objOneGrid.loadData();
                    }
                    objThreeGrid.set("data", emptyArray);
                    $.ligerDialog.success('任务发送成功！已发送到【<a style="color:Red;font-weight:bold">主任复核</a>】环节！')
                }
                else {
                    $.ligerDialog.warn('任务发送失败,请检查结果是否已录入');
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
        var strAnalysisTaskSheetNum = e.record.REMARK_2;
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/saveItemInfo",
            data: "{'id':'" + id + "','strItemResult':'" + strItemResult + "','strAnalysisMethod':'" + strAnalysisMethod + "','strAnalysisTaskSheetNum':'" + strAnalysisTaskSheetNum + "'}",
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
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, render: function (record) {
                     return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                 }
                 },
                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 200, minWidth: 60, render: function (record) {
                     return getQcName(record.QC_TYPE);
                 }
                 },
                 { display: '结果值', name: 'ITEM_RESULT', align: 'left', width: 50, minWidth: 60 },
                 { display: '质控结果', name: 'REMARK', align: 'left', width: 200, minWidth: 60 },
                 { display: '是否合格', name: 'IS_OK', align: 'left', width: 50, minWidth: 60, render: function (record) {
                     if (record.IS_OK == "0")
                         return "<span style='color:red'>否</span>";
                     else
                         return "是";
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
    //$(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //实验室质控设置
    function QcSetting() {
        if (!objTwoGrid.getSelectedRow()) {
            $.ligerDialog.warn('请先选择需要设置的样品');
            return;
        }
        $.ligerDialog.open({ title: "实验室质控设置", width: 500, height: 450, buttons:
        [
        { text:
        '计算', onclick: function (item, dialog) {
            $("iframe")[0].contentWindow.calculate()
        }
        },
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
        }], url: "../QcSetting.aspx?resultid=" + objTwoGrid.getSelectedRow().ID + "&result=" + objTwoGrid.getSelectedRow().ITEM_RESULT
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
//对未领取的样品设置领取
function setReceiveSample() {
    if (objOneGrid.getSelectedRow() == null) {
        $.ligerDialog.warn('领取之前请先选择样品');
        return;
    }
    $.ligerDialog.confirm('您确认领取该样品吗？', function (yes) {
        if (yes == true) {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/setReceiveSample",
                data: "{'strSampleId':'" + objOneGrid.getSelectedRow().ID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                        objOneGrid.updateCell('REMARK1', '已领取', objOneGrid.getSelectedRow());
                    }
                }
            });
        }
    });
}
//对未领取的监测项目设置领取
function setReceiveItem(strResultId) {
    $.ligerDialog.confirm('您确认领取该监测项目吗？', function (yes) {
        if (yes == true) {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/setReceiveItem",
                data: "{'strResultId':'" + strResultId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                        objTwoGrid.loadData();
                        objThreeGrid.set("data", emptyArray);
                    }
                }
            });
        }
    });
}

//获取质控手段名称
function getReceiveSampleStatus(strSampleId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getReceiveSampleStatus",
        data: "{'strSampleId':'" + strSampleId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//取消
function f_Cancel(item, dialog) {
    dialog.close();
}
