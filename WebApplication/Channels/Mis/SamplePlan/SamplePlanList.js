var maingrid = null, vMonitorType = null;
var strSamplePlanId = "", strTask_id = "", strWorkTask_id = "", strSampleTask_id = "", strSubTask_Id = ""; CONTRACT_TYPE = "";TICKET_NUM = "", ASKING_DATE = "";
var strRequestValue = "", isExport = "0";

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

$("#hidTaskId").val(strTask_id);
$(document).ready(function () {
    //$("#layout1").ligerLayout({ height: '100%' });
    //strRequest参数为true表示郑州的 自送样发送 直接发送到 采样后质控 024   胡方扬  2013-07-02
    strRequestValue = $.getUrlVar('strRequest');
    
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetMonitorType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vMonitorType = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetWebConfigValue&strKey=Contract_Export",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                isExport = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    var objToolbar = { items: [
                { id: 'accept', text: '发送', click: AcceptAction, icon: 'page_go' },
                { line: true },
                { id: 'add', text: '增加', click: CreateData, icon: 'add' },
                { line: true },
                { id: 'del', text: '删除', click: DelAction, icon: 'delete' }
                ]
    };
    if (isExport == "1") {
        objToolbar = { items: [
                { id: 'accept', text: '发送', click: AcceptAction, icon: 'page_go' },
                { line: true },
                { id: 'excel', text: '导出任务通知单', click: f_ExportTaskExcle, icon: 'excel' },
                { line: true },
                { id: 'add', text: '增加', click: CreateData, icon: 'add' },
                { line: true },
                { id: 'del', text: '删除', click: DelAction, icon: 'delete' }
                ]
        };
    }
    maingrid = $("#maingrid").ligerGrid({
        columns: [
                { display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 180, minWidth: 120 },
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 340, minWidth: 60, render: function (item) {
                    if (item.FREQ) {
                        return item.PROJECT_NAME + "第<a style='color:Red;font-weight:bold;'>" + item.SAMPLENUM + "</a>次监测";
                    }
                    return item.PROJECT_NAME;
                }
                },
                { display: '委托单位', name: 'COMPANY_NAME', align: 'left', width: 180, minWidth: 140 },
                { display: '委托类型', name: 'CONTRACT_TYPE', width: 150, minWidth: 60, hide: true },
                { display: '监测类别', name: 'TEST_TYPE', align: 'left', width: 180, minWidth: 140, render: function (item) {
                    var strMonitorID = item.TEST_TYPE.split(';');
                    var strMonitorName = "";
                    if (strMonitorID.length > 0) {
                        for (var i = 0; i < strMonitorID.length; i++) {
                            for (var n = 0; n < vMonitorType.length; n++) {
                                if (strMonitorID[i] == vMonitorType[n].ID) {
                                    strMonitorName += vMonitorType[n].MONITOR_TYPE_NAME + ";";
                                }
                            }
                        }
                        return strMonitorName.substring(0, strMonitorName.length - 1);
                    }
                    return item.TEST_TYPE;
                }
                }
                ],
        //   title: '自送样待预约列表',
        width: '100%',
        height: '98%',
        pageSizeOptions: [10, 15, 20, 30, 50],
        pageSize: 50,
        url: 'SamplePlanHandler.ashx?action=GetContractInforUnionSamplePlan',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        alternatingRow: false,
        tree: { columnName: 'TICKET_NUM' },
        checkbox: false,
        autoCheckChildren: false,
        toolbar: objToolbar,
        onAfterShowData: f_hasChildren
    });

    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
})
//数据加载完毕后对有子节点的数据进行初始化折叠  胡方扬
function f_hasChildren(data) {
    if (data.Rows.length > 0) {
        for (var i = 0; i < data.Rows.length; i++) {
            //判断是否有子节点
            if (maingrid.hasChildren(data.Rows[i])) {
                maingrid.collapse(data.Rows[i]);
            }
        }
    }
}
    //增加数据
    function CreateData() {
        $.ligerDialog.open({ title: '自送样监测任务新增', name: 'winaddtor', width: 820, height: 550, top: 30, url: 'SamplePlanAdd.aspx?strPlanId=0&strDate=' + TogetDateForDay(new Date(), false), buttons: [
                { text: '确定', onclick: f_SampleSaveDate },
                { text: '取消', onclick: f_SampleCancel }
            ]
        });
        }

        function f_SampleSaveDate(item, dialog) {

            var fn_frame = dialog.frame.GetIfram || dialog.frame.window.GetIfram;
            var data_frame = fn_frame();
            if (data_frame == "") {
                $.ligerDialog.warn('请生成监测计划！');
                return;
            }
            var fn_1 = dialog.frame.GetSampleRow || dialog.frame.window.GetSampleRow;
            var data_1 = fn_1();

            if (data_1 != "") {
            $.ligerDialog.warn('请设置【<a style="color:Red;font-weight:bold">' + data_1 + '</a>】监测类别的样品！');
                return;
            }


            var fn_item1 = dialog.frame.GetSampleItems || dialog.frame.window.GetSampleItems;
            var data_item1 = fn_item1();
            if (data_item1 != "") {
                $.ligerDialog.warn('请设置【<a style="color:Red;font-weight:bold">' + data_item1 + '</a>】样品的监测项目！');
                return;
            }
            var fn = dialog.frame.GetBaseInfor || dialog.frame.window.GetBaseInfor;
            var data = fn();
            if (data != "") {
                $.ajax({
                    cache: false,
                    async: false, //设置是否为异步加载,此处必须
                    type: "POST",
                    url: "SamplePlanHandler.ashx?action=doPlanTaskForSample" + data,
                    contentType: "application/text; charset=utf-8",
                    dataType: "text",
                    success: function (data) {
                        if (data == "True") {
                            dialog.close();
                            maingrid.loadData();
                            $.ligerDialog.success('数据保存成功！'); return;
                        }
                        else {
                            $.ligerDialog.warn('数据保存失败！'); return;
                        }
                    },
                    error: function (msg) {
                        $.ligerDialog.warn('Ajax加载数据失败！' + msg); return;
                    }
                });

            }
}
function f_SampleCancel(item, dialog) {
    dialog.close();
}


function AcceptAction() {
    var rowSelected = maingrid.getSelectedRow();
    if (rowSelected != null) {
        if (rowSelected.children) {
            $.ligerDialog.warn('父节点无法办理！'); return;
        }
        else {
            strWorkTask_id = rowSelected.ID;
            strSubTask_Id = rowSelected.SUBTASK_ID;
            strSamplePlanId = rowSelected.SAMPLE_ID;
            CONTRACT_TYPE = rowSelected.CONTRACT_TYPE;
            TICKET_NUM = rowSelected.TICKET_NUM;
            ASKING_DATE = rowSelected.ASKING_DATE;
            $.ligerDialog.open({ title: '自送样任务完成时间设置', name: 'winaddtorDate', width: 600, height: 400, top: 30, url: 'SampleAskingDate.aspx?&CONTRACT_TYPE=' + CONTRACT_TYPE + '&strWorkTask_id=' + strWorkTask_id + '&TICKET_NUM=' + TICKET_NUM + '&ASKING_DATE=' + ASKING_DATE, buttons: [ 
                { text: '确定', onclick: f_DateSaveData },
                { text: '返回', onclick: f_DateCancel }
            ]
            });
        }
    }
    if (rowSelected == null) {
        $.ligerDialog.warn('请选择一行！'); return;
    }
}
function f_DateSaveData(item, dialog) {
    var fn = dialog.frame.GetFinishDate || dialog.frame.window.GetFinishDate;
    var data = fn();

    var fn1 = dialog.frame.getTASK_CODE || dialog.frame.window.getTASK_CODE;
    var data1 = encodeURI(fn1());

    var fn2 = dialog.frame.getContact_Name || dialog.frame.window.getContact_Name;
    var data2 = fn2();

    var fn3 = dialog.frame.getContact_PHONE || dialog.frame.window.getContact_PHONE;
    var data3 = fn3();

    if (data1 == "" || data1 == "未编号") {
        $.ligerDialog.warn('请先设置任务单号！');
        return;
    }

    //发送完成后，询问是否打印任务单 潘德军 2014-01-07
    var rowSelected = null, grid = null;
    rowSelected = maingrid.getSelectedRow()
    grid = maingrid;
    var strPrintSamplePlanId = rowSelected.SAMPLE_ID;
    var strPrintTask_id = rowSelected.CONTRACT_ID;
    var strPrintWorkTask_id = rowSelected.ID;
    var strPrintSampleTask_id = rowSelected.SUBTASK_ID;

    if (data != "") {
        var strAction = "";
        if (strRequestValue == "true") {
            strAction = "doSubTask";
        }
        else {
            strAction = "doSubTask_QHD";
        }
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "SamplePlanHandler.ashx?action=" + strAction + "&strContactName=" + data2 + "&strContactPhone=" + data3 + "&strTaskNum=" + encodeURI(data1) + "&strWorkTask_Id=" + strWorkTask_id + "&strSubTask_Id=" + strSubTask_Id + "&strRequest=" + strRequestValue + "&strDate=" + data + "&strSamplePlanId=" + strSamplePlanId,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data == "true") {
                    dialog.close();
                    maingrid.loadData();
                    $.ligerDialog.success('发送成功！');
                    //发送完成后，询问是否打印任务单 潘德军 2014-01-07
                    if_PrintTaskExcel(strPrintWorkTask_id, strPrintSamplePlanId, strPrintTask_id, strPrintSampleTask_id); 
                    return;
                }
                else {
                    $.ligerDialog.warn('数据操作失败！'); return;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg); return;
            }
        });
    } else {
        return;
    }
}

//发送完成后，询问是否打印任务单 潘德军 2014-01-07
function if_PrintTaskExcel(strPrintWorkTask_id, strPrintSamplePlanId, strPrintTask_id, strPrintSampleTask_id) {
    $.ligerDialog.confirm('是否打印任务通知单？\r\n', function (result) {
        if (result == true) {
            $("#hidTaskId").val(strPrintTask_id);
            $("#hidPlanId").val(strPrintSamplePlanId);
            $("#hidWorkTaskId").val(strPrintWorkTask_id);
            $("#hidSubTaskId").val(strPrintSampleTask_id);

            $("#btnExport").click();
        } else {
            return;
        }
    });
}
//function AcceptAction() {
//    var rowSelected = maingrid.getSelectedRow();
//    if (rowSelected != null) {
//        if (rowSelected.children) {
//            $.ligerDialog.warn('父节点无法办理！'); return;
//        }
//        else {
//            strWorkTask_id = rowSelected.TASK_ID;
//            strSubTask_Id = rowSelected.SUBTASK_ID;
//            strSamplePlanId = rowSelected.SAMPLE_ID;

////            $.ligerDialog.open({ title: '任务设置', name: 'winaddtorSetting', width: 600, height: 350, top: 30, url: 'SampleDoTask.aspx?strProjectNmae=' + encodeURI(rowSelected.PROJECT_NAME), buttons: [
////                { text: '确定', onclick: f_DateSaveData },
////                { text: '取消', onclick: f_DateCancel }
////            ]
////            });
//            $.ligerDialog.open({ title: '自送样任务完成时间设置', name: 'winaddtorDate', width: 400, height: 300, top: 30, url: 'SampleAskingDate.aspx', buttons: [
//                { text: '确定', onclick: f_DateSaveData },
//                { text: '返回', onclick: f_DateCancel }
//            ]
//            });
//        }
//    }
//    if (rowSelected == null) {
//        $.ligerDialog.warn('请选择一行！'); return;
//    }
//}


//function f_DateSaveData(item,dialog) {
//    var fn = dialog.frame.GetRequestInfor || dialog.frame.window.GetRequestInfor;
//    var fn_data = fn();
//    if (fn_data != "") {
//        $.ajax({
//            cache: false,
//            async: false, //设置是否为异步加载,此处必须
//            type: "POST",
//            url: "SamplePlanHandler.ashx?action=doSubTask&strWorkTask_Id=" + strWorkTask_id + "&strSubTask_Id=" + strSubTask_Id + "&strRequest=" + strRequestValue + "&strSamplePlanId=" + strSamplePlanId+fn_data,
//            contentType: "application/text; charset=utf-8",
//            dataType: "text",
//            success: function (data) {
//                if (data == "true") {
//                    dialog.close();
//                    maingrid.loadData();
//                    $.ligerDialog.success('任务发送成功！'); return;
//                }
//                else {
//                    $.ligerDialog.warn('任务发送失败！'); return;
//                }
//            },
//            error: function (msg) {
//                $.ligerDialog.warn('Ajax加载数据失败！' + msg); return;
//            }
//        });
//    } else {
//    return;
//    }
//}

function f_DateCancel(item, dialog) {
    dialog.close();
}
function DelAction() {
    var rowSelected = maingrid.getSelectedRow();
    if (rowSelected != null) {
        //如果不存在子节点 则表示父节点 不允许删除
        if (rowSelected.children) {
            $.ligerDialog.warn('父节点不允许删除！'); return;
        }

        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "SamplePlanHandler.ashx?action=DelSamplePlan&strContratId=" + rowSelected.ID + "&strSamplePlanId=" + rowSelected.SAMPLE_ID,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    maingrid.loadData();
                    $.ligerDialog.success('数据删除成功！'); return;
                }
//                else {
//                    $.ligerDialog.warn('最后一条预约计划不允许删除！'); return;  
//                    //老需求  熊 2013-07-18 要求去掉   胡方扬 改
//                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg); return;
            }
        });
    } if (rowSelected == null) {
        $.ligerDialog.warn('请选择一行！'); return;
    }
}

function f_ExportTaskExcle() {
    var rowSelected = null, grid = null;
    rowSelected = maingrid.getSelectedRow()
    grid = maingrid;
    if (rowSelected == null) {
        $.ligerDialog.warn('请选择一条记录！');
    } else {
        strSamplePlanId = rowSelected.SAMPLE_ID;
        strTask_id = rowSelected.CONTRACT_ID;
        strWorkTask_id = rowSelected.ID;
        strSampleTask_id = rowSelected.SUBTASK_ID;
        $("#hidTaskId").val(strTask_id);
        $("#hidPlanId").val(strSamplePlanId);
        $("#hidWorkTaskId").val(strWorkTask_id);
        $("#hidSubTaskId").val(strSampleTask_id);
        $("#btnExport").click();
    }
}

function TogetDateForDay(date, formart) {
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

        if (formart) {
            strD = thisYear + "年" + thisMonth + "月" + thisDay + '日';
        }
        else {
            strD = thisYear + "-" + thisMonth + "-" + thisDay;
        }
    }
    return strD;
}