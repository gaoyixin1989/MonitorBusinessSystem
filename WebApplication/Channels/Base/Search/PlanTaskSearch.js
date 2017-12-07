//update by ssz source hfy
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var maingrid = null, vAutoYearItem = null, vContratTypeItem = null, vAreaItem = null, vDutyUserList = null;

$(document).ready(function () {

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "PlanTaskSearch.aspx?action=GetContratYearHistory",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vAutoYearItem = data.Rows;
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
        url: "PlanTaskSearch.aspx?action=GetDict&type=administrative_area",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vAreaItem = data;
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
        url: "PlanTaskSearch.aspx?action=GetDict&type=Contract_Type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vContratTypeItem = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    maingrid = $("#firstgrid").ligerGrid({
        columns: [
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 220 },
                { display: '委托单号', name: 'CONTRACT_CODE', width: 200, minWidth: 80 },
                { display: '任务单号', name: 'TICKET_NUM', width: 150, minWidth: 80 },
                { display: '任务下达日期', name: 'PLANDATE', width: 80, minWidth: 60, align: 'left', render: function (items) {
                    if (items.PLAN_YEAR != "") {
                        return items.PLAN_YEAR + '-' + items.PLAN_MONTH + '-' + items.PLAN_DAY;
                    }
                }
                },
                { display: '采样要求完成时间', name: 'SAMPLE_FINISH_DATE', width: 100, minWidth: 60, align: 'left'
                },
                { display: '任务要求完成日期', name: 'TASK_ASKING_DATE', width: 100, minWidth: 60, align: 'left'
                },
                { display: '已选监测类型', name: 'TYPES', width: 160, minWidth: 60, align: 'left', render: function (items) {
                    var strValue = GetContractMonitorType(items.CONTRACT_ID, items.ID);
                    if (strValue.length > 20) {
                        return "<a title=" + strValue + ">" + strValue.substring(0, 20) + "......</a>"
                    }
                    return strValue;
                }
                },
                { display: '受检单位', name: 'COMPANY_NAME', align: 'left', width: 180, minWidth: 60 },
                { display: '所属区域', name: 'AREA_NAME', width: 80, minWidth: 60 },
                { display: '已选监测项目负责人', name: 'DUTYUSER', width: 400, minWidth: 60, align: 'left', render: function (items) {
                    var strValue = GetDutyUser(items.CONTRACT_ID);
                    if (strValue.length > 30) {
                        return "<a title=" + strValue + ">" + strValue.substring(0, 30) + "......</a>"
                    }
                    return strValue;
                }
                }
                ],
        title: '预监测任务查询',
        width: '99%',
        height: '99%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        url: 'PlanTaskSearch.aspx?action=GetPendingPlanList&strType=true',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                 { id: 'srhAll', text: '所有记录', click: itemclick_OfToolbar_UnderItem, icon: 'refresh' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclick_OfToolbar_UnderItem, icon: 'search' }
                ]
        },
        onDblClickRow: function (data, rowindex, rowobj) {
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    function GetContractMonitorType(strtask_id, strplan_id) {
        vMonitorArrList = null;
        strMonitorName = "";
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: 'PlanTaskSearch.aspx?action=GetPointMonitorInfor&task_id=' + strtask_id + '&strPlanId=' + strplan_id + '&strIfPlan=1',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vMonitorArrList = data.Rows;
                    for (var i = 0; i < vMonitorArrList.length; i++) {
                        strMonitorName += vMonitorArrList[i].MONITOR_TYPE_NAME + ";";
                    }
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });

        return strMonitorName;
    }
});

//监测项目grid 的Toolbar click事件
function itemclick_OfToolbar_UnderItem(item) {
    switch (item.id) {
        case 'srh':
            showDetailSrh();
            break;
        case 'srhAll':
            maingrid.set('url', "PlanTaskSearch.aspx?action=GetPendingPlanList&strType=true");
            break;
        default:
            break;
    }
}

//设置grid 的弹出查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构

        var mainform = $("#SrhForm");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40,
            fields: [
                     { display: "项目名称", name: "SEA_PROJECT_NAME1", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "委托单号", name: "SEA_CONTRACT_CODE1", newline: false, type: "text" },
                     { display: "合同年度", name: "SEA_CONTRACT_YEAR1", newline: true, type: "select", comboboxName: "SEA_CONTRACT_YEAR_BOX1", options: { valueFieldID: "SEA_CONTRACT_YEAR_OP1", valueField: "ID", textField: "YEAR", data: vAutoYearItem} },
                     { display: "任务单号", name: "SEA_TASK_CODE", newline: false, type: "text" },
                     { display: "受检单位", name: "SEA_TEST_COMPANYNAME1", newline: true, type: "text" },
                     { display: "委托类型", name: "SEA_CONTRACT_TYPE1", newline: false, type: "select", comboboxName: "SEA_CONTRACT_TYPE_BOX1", options: { valueFieldID: "SEA_CONTRACT_TYPE_OP1", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vContratTypeItem} },
                     { display: "任务下达日期", name: "SEA_PLANDATE1", newline: true, type: "date" },
                     { display: "所属区域", name: "SEA_AREA1", newline: false, type: "select", comboboxName: "SEA_AREA_BOX1", options: { valueFieldID: "SEA_AREA_OP1", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vAreaItem}}]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 660, height: 240, top: 90, title: "预监测任务查询",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SEA_PROJECT_NAME = encodeURI($("#SEA_PROJECT_NAME1").val());
        var SEA_CONTRACT_CODE = encodeURI($("#SEA_CONTRACT_CODE1").val());
        var SEA_CONTRACT_YEAR_OP = encodeURI($("#SEA_CONTRACT_YEAR_BOX1").val());
        var SEA_CONTRACT_TYPE_OP = $("#SEA_CONTRACT_TYPE_OP1").val();
        var SEA_TEST_COMPANYNAME = encodeURI($("#SEA_TEST_COMPANYNAME1").val());
        var SEA_AREA = $("#SEA_AREA_OP1").val();
        var SEA_PLANDATE = $("#SEA_PLANDATE1").val();
        var SEA_TASK_CODE = encodeURI($("#SEA_TASK_CODE").val());
        var url = "PlanTaskSearch.aspx?action=GetPendingPlanList&strType=true&strProjectName=" + SEA_PROJECT_NAME + "&strContractCode=" + SEA_CONTRACT_CODE + "&strContratYear=" + SEA_CONTRACT_YEAR_OP + "&strContractType=" + SEA_CONTRACT_TYPE_OP + "&strCompanyNameFrim=" + SEA_TEST_COMPANYNAME + "&strAreaIdFrim=" + SEA_AREA + "&strDate=" + SEA_PLANDATE + "&strTaskCode=" + SEA_TASK_CODE;
        maingrid.set('url', url)
    }
}

function clearSearchDialogValue() {
    $("#SEA_PROJECT_NAME1").val("");
    $("#SEA_CONTRACT_CODE1").val("");
    $("#SEA_TEST_COMPANYNAME1").val("");
    $("#SEA_PLANDATE1").val("");
    $("#SEA_CONTRACT_YEAR_BOX1").ligerGetComboBoxManager().setValue("");
    $("#SEA_CONTRACT_TYPE_BOX1").ligerGetComboBoxManager().setValue("");
    $("#SEA_AREA_BOX1").ligerGetComboBoxManager().setValue("");
}

////////////////////////////////////////////////////////////监测项目负责人/////////////////////////////////////////////////////////////////////////////
function GetDutyUser(strtask_id) {
    var stDutyUser = "";
    if (vMonitorArrList != null) {
        for (var i = 0; i < vMonitorArrList.length; i++) {
            GetContractDutyUser(strtask_id, vMonitorArrList[i].ID);
            if (vDutyUserList != null) {
                stDutyUser += vDutyUserList[0].MONITOR_TYPE_NAME + '类:' + vDutyUserList[0].REAL_NAME + '；';
            }
        }
    }
    return stDutyUser.substring(0, stDutyUser.length - 1);
}

//获取监测计划责任人
function GetContractDutyUser(strtask_id, mointorid) {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "PlanTaskSearch.aspx?action=GetContractDutyUser&strMonitorId=" + mointorid + "&task_id=" + strtask_id,
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
}
////////////////////////////////////////////////////////////监测项目负责人/////////////////////////////////////////////////////////////////////////////