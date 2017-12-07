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
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [10, 15, 20],
        height: '100%',
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 250, minWidth: 60 }
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
                            $.ligerDialog.success('样品发送成功！')
                        }
                        else {
                            $.ligerDialog.warn('样品发送失败');
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

    objTwoGrid = $("#twoGrid").ligerGrid({
        dataAction: 'server',
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        height: twoHeight,
        usePager: false,
        enabledEdit: true,
        toolbar: { items: [
                { text: '退回', click: GoToBack, icon: 'add' }
                ]
        },
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
                     return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                 }
                 },
                 { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60,
                     editor: {
                         type: 'text'
                     }
                 },
        //                 { display: '质控手段', name: 'QC', align: 'left', width: 300, minWidth: 60, render: function (record) {
        //                     return getQcName(record.QC);
        //                 }
        //                 },
                 {display: '分析方法', name: 'ANALYSIS_METHOD_ID', align: 'left', width: 200, minWidth: 60,
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
                 { display: '仪器', name: 'APPARATUS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '要求完成时间', name: 'ASKING_DATE', align: 'left', width: 100, minWidth: 60 },
                 { display: '分析负责人', name: 'HEAD_USERID', align: 'left', width: 100, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUserName(record.HEAD_USERID);
                 }
                 },
                 { display: '分析协同人', name: 'ASSISTANT_USERID', align: 'left', width: 200, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUseExName(record.ASSISTANT_USERID);
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
        toolbar: { items: [
                { text: '实验室质控设置', click: QcSetting, icon: 'add' }
                ]
        },
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 100, minWidth: 60, frozen: true, render: function (record) {
                     return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                 }
                 },
                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
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
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //实验室质控设置
    function QcSetting() {
        if (!objTwoGrid.getSelectedRow()) {
            $.ligerDialog.warn('请先选择需要设置的监测项目');
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
        }], url: "QcSetting.aspx?resultid=" + objTwoGrid.getSelectedRow().ID + "&result=" + objTwoGrid.getSelectedRow().ITEM_RESULT
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