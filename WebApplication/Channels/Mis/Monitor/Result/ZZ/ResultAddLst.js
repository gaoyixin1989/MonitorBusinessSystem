// Create by 黄进军 2014.05.26  "监测结果补录"功能
var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;

var strUrl = "ResultAddLst.aspx";
var strOneGridTitle = "任务信息";

//监测任务管理
$(document).ready(function () {
    objOneGrid = $("#oneGrid").ligerGrid({
        title: strOneGridTitle,
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '250px',
        enabledSort: false,
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 150, minWidth: 60 },
                { display: '合同编号', name: 'CONTRACT_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '委托类别', name: 'CONTRACT_TYPE', width: 100, minWidth: 60, render: function (record) {
                    return getDictName(record.CONTRACT_TYPE, "Contract_Type");
                }
                },
                { display: '项目名称', name: 'PROJECT_NAME', width: 300, minWidth: 60 },
                { display: '受检企业', name: 'TESTED_COMPANY_ID', width: 300, minWidth: 60, render: function (record) {
                    return getCompanyName(record.ID, record.TESTED_COMPANY_ID);
                }
                },
                { display: '报告要求完成时间', name: 'ASKING_DATE', width: 100, minWidth: 60 }
                ],
//        toolbar: { items: [
//                { text: '退回', click: GoToBack, icon: 'add' },
//                { text: '发送', click: SendToNext, icon: 'add' },
//                { text: '下载汇总表', click: DownLoadSummaryTable, icon: 'excel' }
//                ]
//        },
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
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });

    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
     
//样品信息
$(document).ready(function () {
    objTwoGrid = $("#twoGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '100%',
        enabledSort: false,
        columns: [
                { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '全程质控', name: 'ENTIRE_QC', align: 'left', width: 50, minWidth: 60, render: function (record) {
                    if (record.ENTIRE_QC == "1")
                        return "是";
                    else
                        return "否 ";
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
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});

//监测项目信息
$(document).ready(function () {

    //设置修改原因页面
    $("#UpdateBulu").ligerForm({
        fields: [
                    { display: "修改原因", name: "MODIFY_SUGGESTION", newline: true, type: "ListBox", width: 300, height: 300 }
                ]
    });

    objThreeGrid = $("#threeGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        pageSize: 50,
        alternatingRow: false,
        checkbox: true,
        enabledEdit: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: 460,
        enabledSort: false,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, render: function (record) {
                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                     return strItemName;
                 }
                 },
                 { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 200, minWidth: 60,
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

    //弹出修改原因窗口
    var searchDialog = null;
    function AfterEdit(e) {
        var id = e.record.ID;
        var strItemResult = e.record.ITEM_RESULT;
        if (e.record["__status"] != "nochanged") {
            searchDialog = $.ligerDialog.open({
                target: $("#Bulu"),
                width: 500, height: 150, top: 50, isHidden: false, title: "修改原因",
                buttons: [
                { text: '确定', onclick: function () {
                    $.ajax({
                        cache: false,
                        async: false,
                        type: "POST",
                        url: strUrl + "/saveResult",
                        data: "{'id':'" + id + "','strItemResult':'" + strItemResult + "','MODIFY_SUGGESTION':'" + $("#MODIFY_SUGGESTION").val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data, textStatus) {
                            if (data.d == 1) {
                                objThreeGrid.cancelEdit(e.rowindex);
                            }
                        }
                    });
                    searchDialog.hide();
                } 
                },
                { text: '取消', onclick: function () { searchDialog.hide(); } }
                ]
            });    
           
        }
    }

});

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