// Create by 苏成斌 2013.1.15  "样品交接"功能
var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;
var objFourGrid = null;

var strUrl = "SamplingAllocationSheet.aspx";
var strOneGridTitle = "任务信息";
var strTwoGridTitle = "监测项目信息";
var strThreeGridTitle = "监测项目信息";
var strCCflowWorkId = '';
var strTaskID = "";
var strFlowCode = "duty_analyse";
var strResultStatus = "01";

var strListQc1 = "", strListQc2 = "", strListQc3 = "", strListQc4 = "";
var strQc3Count = "", strQc4count = "";
var objSubTaskID = "", objTaskID = "";
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

    return true;
}

//监测任务管理
$(document).ready(function () {
    objSubTaskID = $("#cphData_SUBTASK_ID").val();
    objTaskID = $("#cphData_TASK_ID").val();
    strCCflowWorkId = $.getUrlVar('WorkID');
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
        url: strUrl + '?type=getOneGridInfo&strSubTaskID=' + objSubTaskID + '&strTaskID=' + objTaskID,
        columns: [
                { display: '任务编号', name: 'TICKET_NUM', align: 'left', width: 180, minWidth: 60 },
                { display: '委托类别', name: 'CONTRACT_TYPE', width: 100, minWidth: 60, render: function (record) {
                    return getDictName(record.CONTRACT_TYPE, "Contract_Type");
                }
                },
                { display: '项目名称', name: 'PROJECT_NAME', width: 300, minWidth: 60 },
                { display: '受检企业', name: 'TESTED_COMPANY_ID', width: 300, minWidth: 60, render: function (record) {
                    return getCompanyName(record.ID, record.TESTED_COMPANY_ID);
                }
                }
                ],
        toolbar: { items: [
                //{ text: '样品交接单', click: Export, icon: 'add' },
                //{ text: '样品编码表', click: ExportCode, icon: 'add' },
                { text: '查看时限要求', click: ChkTimeMgm, icon: 'add' }
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
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID + "&strSubTaskID=" + objSubTaskID);
            //objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID + "&TaskID=" + strTaskID);
            objThreeGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");


    //导出数据
    function Export() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('导出之前请先选择任务信息');
            return;
        }
        $.ligerDialog.confirm('您确定要导出样品交接单吗？', function (yes) {
            if (yes == true) {
                var strPrintId = objOneGrid.getSelectedRow().ID;

                $("#cphData_strPrintId").val(strPrintId);
                $("#cphData_btnImport").click();
            }
        });
    }

    //导出数据
    function ExportCode() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('导出之前请先选择任务信息');
            return;
        }
        $.ligerDialog.confirm('您确定要导出样品编码表吗？', function (yes) {
            if (yes == true) {
                var strPrintId = objOneGrid.getSelectedRow().ID;

                $("#cphData_strPrintId_code").val(strPrintId);
                $("#cphData_btnImportCode").click();
            }
        });
    }

    function ChkTimeMgm() {

        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('导出之前请先选择任务信息');
            return;
        }

        var strPrintId = objOneGrid.getSelectedRow().ID;
        var strPlanId = objOneGrid.getSelectedRow().PLAN_ID;

        $.ligerDialog.open({ title: "时限管理", width: 800, height: 300, buttons:
                [
                { text:
                '关闭', onclick: function (item, dialog) { dialog.close(); }
                }], url: "../ProcessMgm/ProcessMgm.aspx?strPlanId=" + strPlanId
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
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID + "&TaskID=" + strTaskID + '&strCCflowWorkId=' + strCCflowWorkId);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
//监测样品信息
$(document).ready(function () {
    bottomHeight = 3 * $(window).height() / 5 - 35;

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
                    { display: '样品编号', name: 'SAMPLE_CODE', align: 'left', width: 140, minWidth: 60,
                        editor: {
                            type: 'text'
                        }
                    },
                    { display: '样品', name: 'SAMPLE_NAME', align: 'left', width: 180, minWidth: 60 },
                    { display: '样品份数', name: 'SAMPLE_COUNT', align: 'left', width: 80, minWidth: 60,
                        editor: {
                            type: 'text'
                        }
                    },
                    { display: '分析项目', name: 'ITEM_NAME', width: 300, minWidth: 60,
                        render: function (record, rowindex, value) {
                            return "<a href=\"javascript:showTableInfor2('" + value + "')\">" + (value.length > 80 ? value.substring(0, 80) + "......" : value) + "</a> ";
                        }
                    },
                    { display: '样品完好', name: 'IS_OK', width: 100, minWidth: 60 },
                    { display: '交样人', name: 'SAMPLING_MANAGER_NAME', width: 100, minWidth: 60 },
                    { display: '特殊样说明', name: 'RemarkView', align: 'center', width: 100, render: function (items) {
                        if (items.SPECIALREMARK != "") {
                            return "<a href='javascript:showDetailRemarkSrh(\"" + items.ID + "\",\"" + items.SPECIALREMARK + "\",false)'>查看</a>";
                        }
                    }
                    },
                    { display: '子样', name: 'SUBSAMPLE', align: 'center', width: 100, render: function (items) {
                        if (getSubSample(items.ID) != null) {
                            return "<a href='javascript:ShowSubSample(\"" + items.ID + "\")'>明细</a>";
                        }
                    }
                    },
                    { display: '样品状态', name: 'SAMPLE_STATUS', align: 'left', width: 140, minWidth: 60 },
                    { display: '备注', name: 'REMARK1', align: 'left', width: 180, minWidth: 60 }
                ],
        //        toolbar: { items: [
        //                { text: '质控设置', click: addQcInfo, icon: 'add' },
        //                { text: '删除样品', click: deleteSample, icon: 'delete' }
        //                ]
        //        },
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
        $.ligerDialog.confirm('确认删除吗？', function (yes) {
            if (yes == true) {
                var strSampleID = objThreeGrid.getSelectedRow().ID;
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

    //质控信息设置
    //新增质控信息
    function addQcInfo() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择设置质控的样品');
            return;
        }
        var strSampleID = objThreeGrid.getSelectedRow().ID;
        GetSampleItemToList(strSampleID, "#listLeft", "0");
        GetSampleItemToList(strSampleID, "#listQc4", "4");

        //打开设置质控项目的DIV
        mtardiv = $.ligerDialog.open({ target: $("#targerdiv"), height: 386, width: 460, top: -5, title: '质控项目-设置', buttons: [{ text: '确定', onclick: function (item, text) {

            if ($("#listQc1 option").length == 0 && $("#listQc2 option").length == 0 && $("#listQc3 option").length == 0 && $("#listQc4 option").length == 0) {
                $.ligerDialog.warn('请选择监测项目！');
            }
            else {
                GetItemsValue();

                SaveQcData(strSampleID);

                mtardiv.hide();
            }
        }
        }, { text: '返回', onclick: function (item, text) {
            SetControlValue();
            mtardiv.hide();
        }
        }]
        });
    }

    function SetControlValue() {
        $("#listLeft option").remove();
        $("#listQc4 option").remove();
    }

    function GetSampleItemToList(strSampleID, strControlName, strQcType) {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/GetSampleItem",
            data: "{'strSampleID':'" + strSampleID + "','strQcType':'" + strQcType + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d != null) {
                    vItemData = data.d;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });

        //bind data
        var vlist = "";
        //遍历json数据,获取监测项目列表
        jQuery.each(vItemData, function (i, n) {
            vlist += "<option value=" + vItemData[i].ID + ">" + vItemData[i].ITEM_NAME + "</option>";
        });
        //绑定数据到listLeft
        $(strControlName).append(vlist);

        $("#pageloading").hide();
    }

    function GetItemsValue() {
        strListQc4 = "";
        strQc4count = "";
        $("#listQc4 option").each(function () {
            strListQc4 += $(this).val() + ",";
        });
        strQc4Count = $("#listQc4Count").val();
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
        var strSampleCount = e.record.SAMPLE_COUNT
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/saveSampleCode",
            data: "{'id':'" + id + "','strSampleCode':'" + strSampleCode + "','strSampleCount':'" + strSampleCount + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == 1) {
                    objThreeGrid.cancelEdit(e.rowindex);
                }
            }
        });
    }
});

$(document).ready(function () {
    function moveQc1left() {
        var vSelect = $("#listQc1 option:selected");
        vSelect.remove();
    }
    function moveQc2left() {
        var vSelect = $("#listQc2 option:selected");
        vSelect.remove();
    }
    function moveQc3left() {
        var vSelect = $("#listQc3 option:selected");
        vSelect.remove();
    }
    function moveQc4left() {
        var vSelect = $("#listQc4 option:selected");
        vSelect.remove();
    }

    function moveQc1right() {
        //数据option选中的数据集合赋值给变量vSelect
        var vSelect = $("#listLeft option:selected");
        //克隆数据添加到listQc1中
        if ($("#listQc1 option").length > 0) {
            $("#listQc1 option").each(function () {
                for (var i = 0; i < vSelect.length; i++) {
                    if ($(this).val() == vSelect[i].value) {
                        $(this).remove();
                    }
                }
            });
        }
        vSelect.clone().appendTo("#listQc1");
    }
    function moveQc2right() {
        //数据option选中的数据集合赋值给变量vSelect
        var vSelect = $("#listLeft option:selected");
        //克隆数据添加到listQc2中
        if ($("#listQc2 option").length > 0) {
            $("#listQc2 option").each(function () {
                for (var i = 0; i < vSelect.length; i++) {
                    if ($(this).val() == vSelect[i].value) {
                        $(this).remove();
                    }
                }
            });
        }
        vSelect.clone().appendTo("#listQc2");
    }
    function moveQc3right() {
        //数据option选中的数据集合赋值给变量vSelect
        var vSelect = $("#listLeft option:selected");
        //克隆数据添加到listQc3中
        if ($("#listQc3 option").length > 0) {
            $("#listQc3 option").each(function () {
                for (var i = 0; i < vSelect.length; i++) {
                    if ($(this).val() == vSelect[i].value) {
                        $(this).remove();
                    }
                }
            });
        }
        vSelect.clone().appendTo("#listQc3");
    }
    function moveQc4right() {
        //数据option选中的数据集合赋值给变量vSelect
        var vSelect = $("#listLeft option:selected");
        //克隆数据添加到listQc4中
        if ($("#listQc4 option").length > 0) {
            $("#listQc4 option").each(function () {
                for (var i = 0; i < vSelect.length; i++) {
                    if ($(this).val() == vSelect[i].value) {
                        $(this).remove();
                    }
                }
            });
        }
        vSelect.clone().appendTo("#listQc4");
    }


    //double click to move left
    $("#listQc1").dblclick(function () {
        moveQc1left();
    });
    //left move
    $("#btnQc1Left").click(function () {
        moveQc1left();
    });
    //right move
    $("#btnQc1Right").click(function () {
        moveQc1right();
    });

    //double click to move left
    $("#listQc2").dblclick(function () {
        moveQc2left();
    });
    //left move
    $("#btnQc2Left").click(function () {
        moveQc2left();
    });
    //right move
    $("#btnQc2Right").click(function () {
        moveQc2right();
    });

    //double click to move left
    $("#listQc3").dblclick(function () {
        moveQc3left();
    });
    //left move
    $("#btnQc3Left").click(function () {
        moveQc3left();
    });
    //right move
    $("#btnQc3Right").click(function () {
        moveQc3right();
    });

    //double click to move left
    $("#listQc4").dblclick(function () {
        moveQc4left();
    });
    //left move
    $("#btnQc4Left").click(function () {
        moveQc4left();
    });
    //right move
    $("#btnQc4Right").click(function () {
        moveQc4right();
    });
});


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


function getSubSample(SampleId) {
    var objItems = null;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../../Mis/Monitor/sampling/QY/SubSample.aspx?action=GetSubSampleList&strSampleId=" + SampleId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != "0") {
                objItems = data.Rows;
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax请求数据失败！');
        }
    });
    return objItems;
}

function ShowSubSample(strId) {
    $.ligerDialog.open({ title: '子样明细', name: 'winaddtor', width: 700, height: 400, top: 90, url: '../../Mis/Monitor/sampling/QY/SubSample.aspx?strView=true&strSampleId=' + strId, buttons: [
                { text: '返回', onclick: function (item, dialog) { objThreeGrid.loadData(); dialog.close() } }
            ]
    });
}

//弹出监测项目信息 huangjinjun add 20150916
var tableDialog2 = null;
function showTableInfor2(value) {
    //创建表单结构
    $("#TableForm2").ligerForm({
        inputWidth: 430, labelWidth: 90, space: 40,
        fields: [
                     { display: "分析项目", name: "TableInfor2", newline: true, type: "textarea" }
                    ]
    });
    $("#TableInfor2").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:320px");

    $("#TableInfor2").val(value);
    var ObjButton = [];

    //$("#TableInfor2").attr("disabled", true);
    ObjButton = [
                  { text: '返回', onclick: function () { tableDialog2.hide(); } }
                  ];
    tableDialog2 = $.ligerDialog.open({
        target: $("#divTable2"),
        width: 560, height: 170, top: 90, title: "分析项目",
        buttons: ObjButton
    });
}


