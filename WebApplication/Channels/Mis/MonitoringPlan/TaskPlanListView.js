var maingrid = null, vMonitorArrList = null, vDutyUserList = null, vAreaItem = null, vContratTypeItem = null, vContratTypeItem = null;
var task_id = "", strMonitorName = "", strPlanId = "";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
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
    maingrid = $("#maingrid").ligerGrid({
        columns: [
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 220 },
                { display: '任务下达日期', name: 'PLANDATE', width: 120, minWidth: 60, align: 'left', render: function (items) {
                    return items.PLAN_YEAR + '年' + items.PLAN_MONTH + '月' + items.PLAN_DAY + '日';
                }
                },
                { display: '委托单号', name: 'CONTRACT_CODE', width: 200, minWidth: 60 },
                { display: '任务单号', name: 'TICKET_NUM', width: 200, minWidth: 60 },
                { display: '已选监测类型', name: 'TYPES', width: 160, minWidth: 60, align: 'left', hide: true, render: function (items) {
                    return GetContractMonitorType(items.CONTRACT_ID, items.ID);
                }
                },
                { display: '受检单位', name: 'COMPANY_NAME', align: 'left', width: 180, hide: true, minWidth: 60 },
                { display: '所属区域', name: 'AREA_NAME', width: 80, minWidth: 60 },
                { display: '已选监测项目负责人', name: 'DUTYUSER', width: 400, minWidth: 60, align: 'left', render: function (items) {
                    return GetDutyUser(items.CONTRACT_ID);
                }
                }
                ],
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        url: 'MonitoringPlan.ashx?action=GetPlanListForQuickly',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: true,
        toolbar: { items: [
                { id: 'view', text: '查看', click: ViewDetail, icon: 'archives' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
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
                     { display: "项目名称", name: "SEA_PROJECT_NAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "合同编号", name: "SEA_CONTRACT_CODE", newline: false, type: "text" },
                     { display: "合同年度", name: "SEA_CONTRACT_YEAR", newline: true, type: "select", comboboxName: "SEA_CONTRACT_YEAR_BOX", options: { valueFieldID: "SEA_CONTRACT_YEAR_OP", valueField: "ID", textField: "YEAR", data: vAutoYearItem} },
                     { display: "委托类型", name: "SEA_CONTRACT_TYPE", newline: false, type: "select", comboboxName: "SEA_CONTRACT_TYPE_BOX", options: { valueFieldID: "SEA_CONTRACT_TYPE_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vContratTypeItem} },
                     { display: "受检单位", name: "SEA_TEST_COMPANYNAME", newline: true, type: "text" },
                     { display: "所属区域", name: "SEA_AREA", newline: false, type: "select", comboboxName: "SEA_AREA_BOX", options: { valueFieldID: "SEA_AREA_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vAreaItem} },
                     { display: "任务下达日期", name: "SEA_PLANDATE", newline: true, type: "date" },
                     { display: "任务单号", name: "SEA_TICKET_NUM", newline: false, type: "text" }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 660, height: 240, top: 90, title: "预约任务查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SEA_PROJECT_NAME = encodeURI($("#SEA_PROJECT_NAME").val());
            var SEA_CONTRACT_CODE = encodeURI($("#SEA_CONTRACT_CODE").val());
            var SEA_CONTRACT_YEAR_OP = encodeURI($("#SEA_CONTRACT_YEAR_BOX").val());
            var SEA_CONTRACT_TYPE_OP = $("#SEA_CONTRACT_TYPE_OP").val();
            var SEA_TEST_COMPANYNAME = encodeURI($("#SEA_TEST_COMPANYNAME").val());
            var SEA_AREA = $("#SEA_AREA_OP").val();
            var SEA_PLANDATE = $("#SEA_PLANDATE").val();
            var SEA_TICKET_NUM = $("#SEA_TICKET_NUM").val();
            var url = "MonitoringPlan.ashx?action=GetPlanListForQuickly&strProjectName=" + SEA_PROJECT_NAME + "&strContractCode=" + SEA_CONTRACT_CODE + "&strContratYear=" + SEA_CONTRACT_YEAR_OP + "&strContractType=" + SEA_CONTRACT_TYPE_OP + "&strCompanyNameFrim=" + SEA_TEST_COMPANYNAME + "&strAreaIdFrim=" + SEA_AREA + "&strTickeNum=" + encodeURI(SEA_TICKET_NUM) + "&strDate=" + SEA_PLANDATE;
            maingrid.set('url', url);
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_PROJECT_NAME").val("");
        $("#SEA_CONTRACT_CODE").val("");
        $("#SEA_TEST_COMPANYNAME").val("");
        $("#SEA_PLANDATE").val("");
        $("#SEA_TICKET_NUM").val("");
        $("#SEA_CONTRACT_YEAR_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_CONTRACT_TYPE_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_AREA_BOX").ligerGetComboBoxManager().setValue("");
    }

    function ViewDetail() {
        var rowSelected = maingrid.getSelectedRow();
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行进行查看！'); return;
        }
        else {
            var PlanDate = rowSelected.PLAN_YEAR + "-" + rowSelected.PLAN_MONTH + "-" + rowSelected.PLAN_DAY
            $.ligerDialog.open({ title: '监测任务预约查看', name: 'winaddtor', width: 700, height: 540, top: 0, url: '../MonitoringPlan/PlanView.aspx?isView=true&strPlanId=' + rowSelected.ID + '&strDate=' + PlanDate
            });
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
                strD = thisYear + "-" + thisMonth + "月" + thisDay + '日';
            }
            else {
                strD = thisYear + "-" + thisMonth + "-" + thisDay;
            }
        }
        return strD;
    }

    function f_Cancel(item, dialog) {
        dialog.close();
    }
    function f_SaveDate(item, dialog) {
        var fn = dialog.frame.SaveDate || dialog.frame.window.SaveDate;
        var data = fn();
        if (data == "1") {
            var fn_PlanId = dialog.frame.getPlanId || dialog.frame.window.getPlanId;
            strPlanId = fn_PlanId();
            AcceptPlan(strPlanId);
            dialog.close();
            return;
        }
        if (data == "") {
            $.ligerDialog.warn('数据保存失败！'); return;
        }
        else {
            return;
        }
    }

    //以下是对指定日期的某条监测计划进行办理、编辑、删除操作
    //办理操作
    function AcceptPlan(strPlanId) {
        $.post("../MonitoringPlan/PlanAdd.aspx?type=doPlan&strPlanId=" + strPlanId, function (data) {
            if (data == "1") {
                maingrid.loadData();
                $.ligerDialog.success('办理成功！'); return
            }
            else {
                maingrid.loadData();
                $.ligerDialog.warn('办理失败！'); return
            }
        });
    }
    function GetContractMonitorType(strtask_id, strplan_id) {
        vMonitorArrList = null;
        strMonitorName = "";
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: 'MonitoringPlan.ashx?action=GetPointMonitorInfor&task_id=' + strtask_id + '&strPlanId=' + strplan_id,
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


    function GetDutyUser(strtask_id) {
        var stDutyUser = "";
        if (vMonitorArrList != null) {
            for (var i = 0; i < vMonitorArrList.length; i++) {
                GetContractDutyUser(strtask_id, vMonitorArrList[i].ID);
                if (vDutyUserList != null) {
                    stDutyUser += vDutyUserList[0].MONITOR_TYPE_NAME + '类:' + vDutyUserList[0].REAL_NAME + '\r\n';
                }
            }
        }
        return stDutyUser;
    }
    //获取监测计划责任人
    function GetContractDutyUser(strtask_id, mointorid) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=GetContractDutyUser&strMonitorId=" + mointorid + "&task_id=" + strtask_id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vDutyUserList = data.Rows;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

})
