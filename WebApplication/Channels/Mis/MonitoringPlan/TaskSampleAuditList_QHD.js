//Create By 胡方扬 
//现场主管审核
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var maingrid = null, maingrid1 = null, maingrid2 = null, vTypeItems = null, vExamTypeItems = null, vEStatusItems = null, vEmployeList = null, vEmployeDept = null;
var vEmployeList = null, vSubEmploye = null, strEmployePostName = "";
var isFisrt = "", gridName = "0";
var strExamType = "", strEmployeNames = "";
var strPlanId = "", strTask_id = "", strWorkTask_id = "", strAskFinishDate = "";
var strSampleUrl = "";
$(document).ready(function () {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetDict&type=administrative_area",
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
        url: "../Contract/MethodHander.ashx?action=GetDict&type=Contract_Type",
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

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetContratYearHistory",
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
        url: "../Contract/MethodHander.ashx?action=GetWebConfigValue&strKey=SampeTaskUrl",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                strSampleUrl = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });


    //================================================
    maingrid1 = $("#maingrid1").ligerGrid({
        columns: [
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 320 },
                { display: '任务下达日期', name: 'PLANDATE', width: 120, minWidth: 60, align: 'left', render: function (items) {
                    if (items.PLAN_YEAR != "") {
                        return items.PLAN_YEAR + '年' + items.PLAN_MONTH + '月' + items.PLAN_DAY + '日';
                    }
                }
                },
                 { display: '要求完成日期', name: 'ASKING_DATE', width: 120, minWidth: 60, align: 'left', render: function (items) {
                     var strDate = "";
                     var v = items.ASKING_DATE;
                     if (v != "") {
                         var contractData = new Date(Date.parse(v.replace(/-/g, '/')))
                         strDate += contractData.getFullYear() + "年";
                         strDate += (contractData.getMonth() + 1) + "月";
                         strDate += contractData.getDate() + "日";
                         return strDate;
                     }
                     return items.ASKING_DATE;
                 }
                 },
                { display: '委托单号', name: 'CONTRACT_CODE', width: 200, minWidth: 60 },
                { display: '任务单号', name: 'TICKET_NUM', width: 200, minWidth: 60 },
                { display: '已选监测类型', name: 'TYPES', width: 160, minWidth: 60, align: 'left', render: function (items) {
                    var strValue = GetContractMonitorType(items.CONTRACT_ID, items.ID);
                    if (strValue.length > 20) {
                        return "<a title=" + strValue + ">" + strValue.substring(0, 20) + "......</a>"
                    }
                    return strValue;
                }
                },
                { display: '受检单位', name: 'COMPANY_NAME', align: 'left', width: 180, minWidth: 60 },
                { display: '所属区域', name: 'AREA_NAME', width: 150, minWidth: 60 },
                { display: '状态', name: 'STATUS', width: 100, minWidth: 60, render: function (items) {
                    return getStatusByTaskID(items.TASK_ID);
                }
                }
                ],
        title: '待审核任务列表',
        width: '99%',
        height: '99%',
        pageSizeOptions: [5, 10, 15, 20, 25, 30],
        pageSize: 25,
        //url: 'MonitoringPlan.ashx?action=GetPendingPlanListDoTask&strType=true&strState=01&strQcStatus=3|8&strHasDone=0|1',
        url: 'MonitoringPlan.ashx?action=GetPendingPlanListDoTask&strType=true&strQcStatus=M12',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: true,
        toolbar: { items: [
            //                { id: 'view', text: '查看', click: ViewData, icon: 'archives' },
            //                { line: true },
               {id: 'sample', text: '审核', click: TopOpen, icon: 'up' },
                { line: true },
                { id: 'excel', text: '导出任务通知单', click: f_ExportTaskExcle, icon: 'excel' },
                { line: true },
                { id: 'srh', text: '查询', click: ShowSrch, icon: 'search' }
                ]
        },
        whenRClickToSelect: true,
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

    function TopOpen() {
        var rowSelected = null, grid = null;
        rowSelected = maingrid1.getSelectedRow()
        grid = maingrid1;
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！');
        } else {
            top.f_addTab("tabManagerAuditSample", '现场室主管审核', "../Channels/Mis/Monitor/sampling/QHD/ManagerAuditSampling.aspx?type=ModifTaskSampleDutyUser&planid=" + rowSelected.ID + "&strTask_Id=" + rowSelected.CONTRACT_ID + "&strQCStatus=" + rowSelected.QC_STATUS + "&strWorkTask_Id=" + rowSelected.TASK_ID + "&strDept=" + rowSelected.STATE);

        }
    }

    function ShowSrch() {
        showDetailSrh1();
    }

    function DoTaskPlan() {
        var rowSelected = null, grid = null;
        rowSelected = maingrid1.getSelectedRow()
        grid = maingrid1;
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！');
        } else {
            strPlanId = rowSelected.ID;
            strTask_id = rowSelected.CONTRACT_ID;
            strWorkTask_id = rowSelected.TASK_ID;
            strAskFinishDate = rowSelected.ASKING_DATE;
            $.ligerDialog.open({ title: '监测任务预约', name: 'winaddtor', width: 700, height: 340, top: 90, url: '../MonitoringPlan/PendingDoTask.aspx?strIfPlan=0&strDate=&strPlanId=' + strPlanId + '&strTaskId=' + strTask_id + '&strWorkTask_id=' + strWorkTask_id + '&strAskFinishDate=' + strAskFinishDate + '&strProjectNmae=' + encodeURI(rowSelected.PROJECT_NAME), buttons: [
                { text: '确定', onclick: f_SaveData },
                { text: '取消', onclick: f_Cancel }
            ]
            });
        }
    }

    function f_ExportTaskExcle() {
        var rowSelected = null, grid = null;
        rowSelected = maingrid1.getSelectedRow()
        grid = maingrid1;
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！');
        } else {
            strPlanId = rowSelected.ID;
            strTask_id = rowSelected.CONTRACT_ID;
            strWorkTask_id = rowSelected.TASK_ID;
            $("#hidTaskId").val(strTask_id);
            $("#hidPlanId").val(strPlanId);
            $("#hidWorkTaskId").val(strWorkTask_id);
            $("#btnExport").click();
        }
    }
    function ViewData() {
        var rowSelected = null, grid = null;
        rowSelected = maingrid1.getSelectedRow()
        grid = maingrid1;
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！');
        } else {
            var sDate = rowSelected.PLAN_YEAR + '-' + rowSelected.PLAN_MONTH + '-' + rowSelected.PLAN_DAY;
            top.f_addTab("tabSample", '查看任务', "../Channels/Mis/MonitoringPlan/PlanCalendar.aspx?strDate=" + sDate);
        }
    };

    function GetContractMonitorType(strtask_id, strplan_id) {
        vMonitorArrList = null;
        strMonitorName = "";
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: 'MonitoringPlan.ashx?action=GetPointMonitorInfor&task_id=' + strtask_id + '&strPlanId=' + strplan_id + '&strIfPlan=1',
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
        if (strMonitorName != "") {
            strMonitorName = strMonitorName.substring(0, strMonitorName.length - 1);
        }
        return strMonitorName;
    }


    function GetDutyUser(strtask_id, strplan_id) {
        var stDutyUser = "";
        if (vMonitorArrList != null) {
            for (var i = 0; i < vMonitorArrList.length; i++) {
                GetContractDutyUser(strtask_id, vMonitorArrList[i].ID, strplan_id);
                if (vDutyUserList != null) {
                    stDutyUser += vDutyUserList[0].MONITOR_TYPE_NAME + '类:' + vDutyUserList[0].REAL_NAME + '；';
                }
            }
        }
        return stDutyUser.substring(0, stDutyUser.length - 1);
    }
    //获取监测计划责任人
    function GetContractDutyUser(strtask_id, mointorid, strplan_id) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=GetContractDutyUser&strMonitorId=" + mointorid + "&strPlanId=" + strplan_id + "&task_id=" + strtask_id,
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

    //设置grid 的弹出查询对话框
    var detailWinSrh1 = null;
    function showDetailSrh1() {
        if (detailWinSrh1) {
            detailWinSrh1.show();
        }
        else {
            //创建表单结构

            var mainform1 = $("#SrhForm1");
            mainform1.ligerForm({
                inputWidth: 170, labelWidth: 90, space: 40,
                fields: [
                     { display: "项目名称", name: "SEA_PROJECT_NAME1", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "委托单号", name: "SEA_CONTRACT_CODE1", newline: false, type: "text" },
                     { display: "合同年度", name: "SEA_CONTRACT_YEAR1", newline: true, type: "select", comboboxName: "SEA_CONTRACT_YEAR_BOX1", options: { valueFieldID: "SEA_CONTRACT_YEAR_OP1", valueField: "ID", textField: "YEAR", data: vAutoYearItem} },
                     { display: "委托类型", name: "SEA_CONTRACT_TYPE1", newline: false, type: "select", comboboxName: "SEA_CONTRACT_TYPE_BOX1", options: { valueFieldID: "SEA_CONTRACT_TYPE_OP1", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vContratTypeItem} },
                     { display: "受检单位", name: "SEA_TEST_COMPANYNAME1", newline: true, type: "text" },
                     { display: "所属区域", name: "SEA_AREA1", newline: false, type: "select", comboboxName: "SEA_AREA_BOX1", options: { valueFieldID: "SEA_AREA_OP1", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vAreaItem} },
                     { display: "任务下达日期", name: "SEA_PLANDATE1", newline: true, type: "date" },
                     { display: "任务单号", name: "SEA_TICKET_NUM1", newline: false, type: "text" }
                    ]
            });

            detailWinSrh1 = $.ligerDialog.open({
                target: $("#detailSrh1"),
                width: 660, height: 240, top: 90, title: "已预约任务查询",
                buttons: [
                  { text: '确定', onclick: function () { search1(); clearSearchDialogValue1(); detailWinSrh1.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue1(); detailWinSrh1.hide(); } }
                  ]
            });
        }

        function search1() {
            var SEA_PROJECT_NAME = encodeURI($("#SEA_PROJECT_NAME1").val());
            var SEA_CONTRACT_CODE = encodeURI($("#SEA_CONTRACT_CODE1").val());
            var SEA_CONTRACT_YEAR_OP = encodeURI($("#SEA_CONTRACT_YEAR_BOX1").val());
            var SEA_CONTRACT_TYPE_OP = $("#SEA_CONTRACT_TYPE_OP1").val();
            var SEA_TEST_COMPANYNAME = encodeURI($("#SEA_TEST_COMPANYNAME1").val());
            var SEA_AREA = $("#SEA_AREA_OP1").val();
            var SEA_PLANDATE = $("#SEA_PLANDATE1").val();
            var SEA_TICKET_NUM = $("#SEA_TICKET_NUM1").val();
            var url = "MonitoringPlan.ashx?action=GetPendingPlanListDoTask&strType=true&strQcStatus=2|3&strProjectName=" + SEA_PROJECT_NAME + "&strContractCode=" + SEA_CONTRACT_CODE + "&strContratYear=" + SEA_CONTRACT_YEAR_OP + "&strContractType=" + SEA_CONTRACT_TYPE_OP + "&strCompanyNameFrim=" + SEA_TEST_COMPANYNAME + "&strAreaIdFrim=" + SEA_AREA + "&strTickeNum=" + encodeURI(SEA_TICKET_NUM) + "&strDate=" + SEA_PLANDATE;
            maingrid1.set('url', url)
        }
    }

    function clearSearchDialogValue1() {
        $("#SEA_PROJECT_NAME1").val("");
        $("#SEA_CONTRACT_CODE1").val("");
        $("#SEA_TEST_COMPANYNAME1").val("");
        $("#SEA_PLANDATE1").val("");
        $("#SEA_TICKET_NUM1").val("");
        $("#SEA_CONTRACT_YEAR_BOX1").ligerGetComboBoxManager().setValue("");
        $("#SEA_CONTRACT_TYPE_BOX1").ligerGetComboBoxManager().setValue("");
        $("#SEA_AREA_BOX1").ligerGetComboBoxManager().setValue("");
    }

})
//获取任务的完成状态
function getStatusByTaskID(strTaskID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "TaskSampleAuditList_QHD.aspx/getStatusByTaskID",
        data: "{'strTaskID':'" + strTaskID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}