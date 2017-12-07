// Create by 熊卫华 2013.01.16  "分析室主任审核"功能
var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;
var objFourGrid = null;
var objFiveGrid = null;

var objOneGrid2 = null;
var objTwoGrid2 = null;
var objThreeGrid2 = null;
var objFourGrid2 = null;
var objFiveGrid2 = null;

var strUrl = "AnalysisResultCheck.aspx";
var strOneGridTitle = "";
var strTwoGridTitle = "监测项目信息";
var strThreeGridTitle = "监测项目信息";

var selectTabId = "0";

//监测任务管理
$(document).ready(function () {
    $("#navtab1").ligerTab({ onAfterSelectTabItem: function (tabid) {

        tab = $("#navtab1").ligerGetTabManager();
        selecttabindex = tab.getSelectedTabItemID();
        changeTab(selecttabindex);
    }
    });
    //Tab标签切换事件
    function changeTab(tabid) {
        switch (tabid) {
            // 未确认                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
            case "tabitem1":
                selectTabId = "0";
                break;
            case "tabitem2":
                selectTabId = "1";
                objOneGrid2.set('url', strUrl + "?type=getOneGridInfo&IsDo=1");
                objTwoGrid2.set('data', emptyArray);
                objThreeGrid2.set("data", emptyArray);
                objFourGrid2.set("data", emptyArray);
                objFiveGrid2.set("data", emptyArray);
                break;
        }
    }

    topHeight = 2 * $(window).height() / 5 - 35;
    //待办
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
        height: topHeight - 15,
        url: strUrl + '?type=getOneGridInfo&IsDo=0',
        columns: [
               { display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 150, minWidth: 60 },
               { display: '退回意见', name: 'REMARK1', align: 'left', width: 180, minWidth: 60,
                   render: function (record, rowindex, value) {
                       return "<a href=\"javascript:showSuggestion('" + value + "')\">" + (value.length > 10 ? value.substring(0, 10) + "......" : value) + "</a> ";
                   }
               }
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
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID + "&IsDo=0");
            objThreeGrid.set("data", emptyArray);
            objFourGrid.set("data", emptyArray);
            objFiveGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    //已办
    objOneGrid2 = $("#oneGrid2").ligerGrid({
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
        height: topHeight - 15,
        url: strUrl + '?type=getOneGridInfo&IsDo=1',
        columns: [
               { display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 150, minWidth: 60 },
               { display: '退回意见', name: 'REMARK1', align: 'left', width: 180, minWidth: 60,
                   render: function (record, rowindex, value) {
                       return "<a href=\"javascript:showSuggestion('" + value + "')\">" + (value.length > 10 ? value.substring(0, 10) + "......" : value) + "</a> ";
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

            //点击的时候加载样品信息
            objTwoGrid2.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID + "&IsDo=1");
            objThreeGrid2.set("data", emptyArray);
            objFourGrid2.set("data", emptyArray);
            objFiveGrid2.set("data", emptyArray);
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
                    url: strUrl + "?type=SendToNext&strTaskId=" + objOneGrid.getSelectedRow().ID,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.result == "1") {
                            objOneGrid.loadData();
                            objTwoGrid.set("data", emptyArray);
                            objThreeGrid.set("data", emptyArray);
                            objFourGrid.set("data", emptyArray);
                            objFiveGrid.set("data", emptyArray);
                            $.ligerDialog.success('任务发送成功，发送至【<a style="color:Red;font-weight:bold">' + data.msg + '</a>】环节');
                        }
                        else {
                            $.ligerDialog.warn('任务发送失败');
                        }
                    }
                });
            }
        });
    }
});
//监测类别管理
$(document).ready(function () {
    bottomHeight = 2 * $(window).height() / 5 - 35;
    //待办
    objTwoGrid = $("#twoGrid").ligerGrid({
        dataAction: 'server',
        usePager: false,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
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
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID + "&IsDo=0");
            objFourGrid.set("data", emptyArray);
            objFiveGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    //已办
    objTwoGrid2 = $("#twoGrid2").ligerGrid({
        dataAction: 'server',
        usePager: false,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
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
            objThreeGrid2.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID + "&IsDo=1");
            objFourGrid2.set("data", emptyArray);
            objFiveGrid2.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
//样品信息
$(document).ready(function () {
    twoHeight = 3 * $(window).height() / 5;
    //待办
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
        pageSizeOptions: [5, 10, 15, 20],
        height: twoHeight - 20,
        columns: [
                { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 120, minWidth: 60 },
                { display: '样品名称', name: 'SAMPLE_NAME', align: 'left', width: 120, minWidth: 60 },
                 { display: '特殊样说明', name: 'RemarkView', align: 'center', width: 80, render: function (items) {
                     if (items.SPECIALREMARK != "") {
                         return "<a href='javascript:showDetailRemarkSrh(\"" + items.ID + "\",\"" + items.SPECIALREMARK + "\",false)'>查看</a>";
                     }
                 }
                 },
                { display: '子样', name: 'SUBSAMPLE', align: 'center', width: 80, render: function (items) {
                    if (getSubSample(items.ID) != null) {
                        return "<a href='javascript:ShowSubSample(\"" + items.ID + "\")'>明细</a>";
                    }
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
            objFourGrid.set('url', strUrl + "?type=getFourGridInfo&threeGridId=" + rowdata.ID + "&IsDo=0");
            objFiveGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    //已办
    objThreeGrid2 = $("#threeGrid2").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: true,
        pageSize: 100,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: twoHeight - 20,
        columns: [
                { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 120, minWidth: 60 },
                { display: '样品名称', name: 'SAMPLE_NAME', align: 'left', width: 120, minWidth: 60 },
                 { display: '特殊样说明', name: 'RemarkView', align: 'center', width: 80, render: function (items) {
                     if (items.SPECIALREMARK != "") {
                         return "<a href='javascript:showDetailRemarkSrh(\"" + items.ID + "\",\"" + items.SPECIALREMARK + "\",false)'>查看</a>";
                     }
                 }
                 },
                { display: '子样', name: 'SUBSAMPLE', align: 'center', width: 80, render: function (items) {
                    if (getSubSample(items.ID) != null) {
                        return "<a href='javascript:ShowSubSample(\"" + items.ID + "\")'>明细</a>";
                    }
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
            objFourGrid2.set('url', strUrl + "?type=getFourGridInfo&threeGridId=" + rowdata.ID + "&IsDo=1");
            objFiveGrid2.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
//监测项目信息
$(document).ready(function () {
    threeHeight = 3 * $(window).height() / 10;
    //待办
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
        pageSizeOptions: [5, 10, 15, 20],
        height: threeHeight,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
                     return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                 }
                 },
                 { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60, render: function (items) {
                     //var isShow = getItemInfoName(items.ITEM_ID, "IS_ANYSCENE_ITEM");
                     //if (isShow == "1") {
                     //    //var strSubTaskId = objThreeGrid.getSelectedRow();
                     //    var strSubTaskId = items.ID;
                     //    return "<a href='javascript:SetTable(\"" + strSubTaskId + "\" ,\"" + items.ITEM_ID + "\")'>原始样结果</a>";
                     //}
                     return items.ITEM_RESULT;
                 }
                 },
                 { display: '质控手段', name: 'QC', align: 'left', width: 300, minWidth: 60, render: function (record) {
                     return getQcName(record.QC);
                 }
                 },
                 { display: '分析方法', name: 'ANALYSIS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '检出限', name: 'RESULT_CHECKOUT', align: 'left', width: 100, minWidth: 60 },
                 { display: '单位', name: 'UNIT', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器', name: 'APPARATUS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '前处理说明', name: 'REMARK_2', align: 'center', width: 80, render: function (items) {
                     if (items.REMARK_2 != "") {
                         return "<a href='javascript:showSampleRemarkSrh(\"" + items.ID + "\",\"" + items.REMARK_2 + "\",false)'>查看</a>";
                     }
                     return items.REMARK_2;
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
                { text: '退回', click: GoToBack, icon: 'add' },
                { text: '原始记录信息', click: SetTable, icon: 'attibutes'}]
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
    //已办
    objFourGrid2 = $("#fourGrid2").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        pageSize: 100,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: threeHeight,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
                     return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                 }
                 },
                 { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60, render: function (items) {
                     return items.ITEM_RESULT;
                 }
                 },
                 { display: '质控手段', name: 'QC', align: 'left', width: 300, minWidth: 60, render: function (record) {
                     return getQcName(record.QC);
                 }
                 },
                 { display: '分析方法', name: 'ANALYSIS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '检出限', name: 'RESULT_CHECKOUT', align: 'left', width: 100, minWidth: 60 },
                 { display: '单位', name: 'UNIT', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器', name: 'APPARATUS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '前处理说明', name: 'REMARK_2', align: 'center', width: 80, render: function (items) {
                     if (items.REMARK_2 != "") {
                         return "<a href='javascript:showSampleRemarkSrh(\"" + items.ID + "\",\"" + items.REMARK_2 + "\",false)'>查看</a>";
                     }
                     return items.REMARK_2;
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
                { text: '原始记录信息', click: SetTable, icon: 'attibutes'}]
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
            objFiveGrid2.set('url', strUrl + "?type=getFiveGridInfo&fourGridId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

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
        if (!objFourGrid.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条监测项目信息退回');
            return;
        }
        $.ligerDialog.prompt('退回意见', '', true, function (yes, value) {

            if (yes) {
                //退回该项目
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=GoToBack&strResultId=" + objFourGrid.getSelectedRow().ID + "&strSuggestion=" + encodeURI(value),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data == "1") {
                            objFourGrid.loadData();
                            objFiveGrid.set("data", emptyArray);
                            $.ligerDialog.success('监测项目退回成功');
                            return;
                        }
                        else {
                            $.ligerDialog.warn('监测项目退回失败');
                            return;
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
    //待办
    objFiveGrid = $("#fiveGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [10, 15, 20, 25],
        height: fourHeight - 20,
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
                  { display: '质控结果', name: 'REMARK', align: 'left', width: 320, minWidth: 60 },
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
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    //已办
    objFiveGrid2 = $("#fiveGrid2").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [10, 15, 20, 25],
        height: fourHeight - 20,
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
                  { display: '质控结果', name: 'REMARK', align: 'left', width: 320, minWidth: 60 },
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
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    objFiveGrid.toggleCol("IS_OK");
    objFiveGrid2.toggleCol("IS_OK");
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

//设置grid 的弹出特殊样说明录入对话框
var detailRemarkWinSrh = null;
function showDetailRemarkSrh(strSubTaskId, oldRemark, isAdd) {
    //创建表单结构

    var mainRemarkform = $("#RemarkForm");
    mainRemarkform.ligerForm({
        inputWidth: 430, labelWidth: 90, space: 40,
        fields: [
                     { display: "特殊样说明", name: "SEA_REMARK", newline: true, type: "textarea" }
                    ]
    });
    $("#SEA_REMARK").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:400px");

    $("#SEA_REMARK").val(oldRemark);
    var ObjButton = [];
    if (!isAdd) {
        $("#SEA_REMARK").attr("disabled", true);
        ObjButton = [
                  { text: '返回', onclick: function () { clearRemarkDialogValue(); detailRemarkWinSrh.hide(); } }
                  ];
    } else {
        $("#SEA_REMARK").attr("disabled", false);
        ObjButton = [
                  { text: '确定', onclick: function () { SaveRemark(strSubTaskId); } },
                  { text: '返回', onclick: function () { clearRemarkDialogValue(); detailRemarkWinSrh.hide(); } }
                  ];
    }
    detailRemarkWinSrh = $.ligerDialog.open({
        target: $("#detailRemark"),
        width: 660, height: 170, top: 90, title: isAdd ? "特殊样说明录入" : "特殊样说明查看",
        buttons: ObjButton
    });
}
function clearRemarkDialogValue() {
    $("#SEA_REMAKR").val("");
}

//设置grid 的弹出特殊样说明录入对话框
var SampleRemarkWinSrh = null;
function showSampleRemarkSrh(strSubTaskId, oldRemark, isAdd) {
    //创建表单结构

    var sampleRemarkform = $("#SampleRemarkForm");
    sampleRemarkform.ligerForm({
        inputWidth: 430, labelWidth: 90, space: 40,
        fields: [
                     { display: "前处理说明", name: "SEA_SAMPLEREMARK", newline: true, type: "textarea" }
                    ]
    });
    $("#SEA_SAMPLEREMARK").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:400px");

    $("#SEA_SAMPLEREMARK").val(oldRemark);
    var ObjButton = [];
    if (!isAdd) {
        $("#SEA_SAMPLEREMARK").attr("disabled", true);
        ObjButton = [
                  { text: '返回', onclick: function () { clearSampleRemarkDialogValue(); SampleRemarkWinSrh.hide(); } }
                  ];
    } else {
        $("#SEA_SAMPLEREMARK").attr("disabled", false);
        ObjButton = [
                  { text: '确定', onclick: function () { SaveSampleRemark(strSubTaskId); } },
                  { text: '返回', onclick: function () { clearSampleRemarkDialogValue(); SampleRemarkWinSrh.hide(); } }
                  ];
    }
    SampleRemarkWinSrh = $.ligerDialog.open({
        target: $("#detailSampleRemark"),
        width: 660, height: 170, top: 90, title: isAdd ? "前处理说明录入" : "前处理说明查看",
        buttons: ObjButton
    });
}
function clearSampleRemarkDialogValue() {
    $("#SEA_SAMPLEREMARK").val("");
}

function SaveSampleRemark(strSubTaskId) {
    var strRemark = $("#SEA_SAMPLEREMARK").val();
    $.ajax({
        cache: false,
        type: "POST",
        url: "AnalysisResult.aspx/SaveRemark",
        data: "{'strValue':'" + strSubTaskId + "','strSampleRemark':'" + strRemark + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "true") {
                objTwoGrid.loadData();
                clearSampleRemarkDialogValue();
                SampleRemarkWinSrh.hide();
                $.ligerDialog.success('数据操作成功');
                return;
            }
            else {
                $.ligerDialog.warn('数据操作失败');
                return;
            }
        }
    });
}

function getSubSample(SampleId) {
    var objItems = null;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../../sampling/QY/SubSample.aspx?action=GetSubSampleList&strSampleId=" + SampleId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != "0") {
                objItems = data.Rows;
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax请求数据失败！');
            return;
        }
    });
    return objItems;
}

function ShowSubSample(strId) {
    $.ligerDialog.open({ title: '子样明细', name: 'winaddtor', width: 700, height: 400, top: 90, url: '../../sampling/QY/SubSample.aspx?strView=true&strSampleId=' + strId, buttons: [
                { text: '返回', onclick: function (item, dialog) { dialog.close() } }
            ]
    });
}
//获取监测项目信息
function getItemInfor(strItemID) {
    var strValue = null;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../../sampling/QY/SamplePoint.aspx?type=getBaseItemInfor&strItemId=" + strItemID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.Total != '0') {
                strValue = data.Rows;
            }
        }
    });
    return strValue;
}

function SetTable() {
    var selectedSample = null;
    var selectedItem = null;
    if (selectTabId == "0") {
        selectedSample = objThreeGrid.getSelectedRow();
        selectedItem = objFourGrid.getSelectedRow();
    } else {
        selectedSample = objThreeGrid2.getSelectedRow();
        selectedItem = objFourGrid2.getSelectedRow();
    }
    if (!selectedSample) {
        $.ligerDialog.warn('请先选择样品！');
        return;
    }
    if (!selectedItem) {
        $.ligerDialog.warn('请先选择监测项目！');
        return;
    }
    //结果类型，Poll：污染源 Air：大气
    var strPA = selectedItem.REMARK_5;
    var ItemInfor = getItemInfor(selectedItem.ITEM_ID);
    var strCataLogName = ItemInfor[0].ORI_CATALOG_TABLEID;
    //获取监测项目的监测类型 废气：000000002
    var strMONITORID = ItemInfor[0].MONITOR_ID;
    var strTitle = "原始记录表", strPageUrl = "", strKeyTableName = "", strBaseTableName = "";
    if (strCataLogName != "" || strMONITORID == "000000002") {
        if (strPA != "Air") {
            //固定污染源原始记录表
            switch (strCataLogName) {
                //烟尘类的 使用该表作为原始记录主表             
                case "T_MIS_MONITOR_DUSTATTRIBUTE":
                    if (ItemInfor[0].ITEM_NAME.indexOf("油烟") != -1) {
                        strTitle = "饮食业油烟分析原始记录表";
                        strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_YY.aspx";
                    }
                    else {
                        strTitle = "固定污染源排气中颗粒物采样分析原始记录表";
                        strPageUrl = "../../sampling/QY/OriginalTable/DustyTable.aspx";
                    }
                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE";
                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                    break;
                //除了标明原始记录表名的监测项目外其他废气的监测项目也使用该表作为原始记录主表  
                case "":
                    strTitle = "污染源采样原始记录表";
                    strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_PM.aspx";
                    strKeyTableName = "";
                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                    break;
                //PM的和总悬浮物项目类的 使用该表作为原始记录主表             
                case "T_MIS_MONITOR_DUSTATTRIBUTE_PM":
                    strTitle = "污染源采样原始记录表";
                    strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_PM.aspx";
                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE_PM";
                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                    break;
                //SO2和NOX类的 使用该表作为原始记录主表             
                case "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX":
                    strTitle = "固定污染源排气中气态污染物采样分析原始记录表";
                    strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_So2OrNox.aspx";
                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX";
                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                    break;
                default:
                    break;
            }
        }
        else {
            strTitle = "大气采样原始记录表";
            strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_Air.aspx";
            strKeyTableName = strCataLogName;
            strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
            OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
        }
    } else {
        return;
    }

//    $.ligerDialog.open({ title: getItemInfoName(strItemId, "ITEM_NAME") + '样品原始记录表', top: 0, width: 1100, height: 680, buttons:
//         [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); }
//         }], url: '../../sampling/QY/OriginalTable/DustyTable.aspx?strSubTask_Id=' + strSubId + '&strIsView=true&strItem_Id=' + strItemId
//    });
}
//strLinkCode环节编号，01：采样环节；02：监测分析环节；03：分析结果复核环节；04：质控审核环节；05：现场项目结果核录环节；06：分析主任审核环节；07：现场结果复核环节；08：现场室主任审核环节
function OpenDialog(Title, PageUrl, KeyTable, BaseTable, ItemID, SubTaskID) {
    $.ligerDialog.open({ Title: Title, top: 0, width: 1100, height: 680, buttons:
         [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: PageUrl + '?strSubTask_Id=' + SubTaskID + '&strIsView=true&strItem_Id=' + ItemID + '&strKeyTableName=' + KeyTable + '&strBaseTableName=' + BaseTable + '&strLinkCode=03'
    });
}

//弹出退回意见框
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
