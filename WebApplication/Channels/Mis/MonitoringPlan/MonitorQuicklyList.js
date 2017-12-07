var maingrid = null, vMonitorArrList = null, vDutyUserList = null, vAreaItem = null, vContratTypeItem = null, vContratTypeItem = null;
var task_id = "", strMonitorName = "", strPlanId = "", strCompanyInfoId = "", strWorkTask_Id = "", CONTRACT_TYPE = "", TICKET_NUM = "";
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
                { display: '委托单号', name: 'CONTRACT_CODE', width: 200, minWidth: 60 },
                { display: '任务单号', name: 'TICKET_NUM', width: 200, minWidth: 60 },
                { display: '已选监测类型', name: 'TYPES', width: 160, minWidth: 60, align: 'left', render: function (items) {
                    return GetContractMonitorType(items.CONTRACT_ID, items.ID);
                }
                },
                { display: '受检单位', name: 'COMPANY_NAME', align: 'left', width: 180, minWidth: 60 },
                { display: '所属区域', name: 'AREA_NAME', width: 80, minWidth: 60 },
        //                { display: '已选监测项目负责人', name: 'DUTYUSER', width: 400, minWidth: 60, align: 'left', render: function (items) {
        //                    return GetDutyUser(items.CONTRACT_ID, items.ID);
        //                }
        //                },
            {display: '办理日期', name: 'PLANDATE', width: 120, minWidth: 60, align: 'left', render: function (items) {
                return items.PLAN_YEAR + '年' + items.PLAN_MONTH + '月' + items.PLAN_DAY + '日';
            }
        },
           { display: '委托类型', name: 'CONTRACT_TYPE', width: 150, minWidth: 60, hide: true },
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
            }
                ],
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        url: 'MonitoringPlan.ashx?action=GetPlanListForQuickly&strQulickly=5',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: true,
        toolbar: { items: [
                        { id: 'accept', text: '发送', click: SendTask, icon: 'page_go' },
                { line: true },
                { id: 'add', text: '增加', click: CreateData, icon: 'add' },
                { line: true },
                { id: 'del', text: '删除', click: DeleteData, icon: 'delete' },
                { line: true },
            //                { id: 'addPoint', text: '企业点位', click: AddCompanyPoint, icon: 'database' },
            //                { line: true },
                {id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
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

    //    function AddCompanyPoint() {
    //        $.ligerDialog.open({ title: '企业点位', name: 'winaddtor', width: 360, height: 200, top: 90, url: 'CompanyPointEdit.aspx', buttons: [
    //                { text: '确定', onclick: f_OkData },
    //                { text: '取消', onclick: f_CanceDatal }
    //            ]
    //        });
    //    }

    //    function f_OkData(item, dialog) {
    //        var fn = dialog.frame.GetCompanyIdStr || dialog.frame.window.GetCompanyIdStr;
    //        var datastr = fn();
    //        if (datastr == "") {
    //            return;
    //        } else {
    //            var tabid = "tabidPoint" + strCompanyInfoId;
    //            var surl = '../Channels/Base/Company/PointList.aspx?CompanyID=' + datastr;
    //            top.f_addTab(tabid, '监测点位', surl);
    //        }
    //    }

    //    function f_CanceDatal(item, dialog) {
    //        dialog.close();
    //    }
    //增加数据

    //发送数据

    function SendTask() {
        var rowSelected = maingrid.getSelectedRow();
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行进行操作'); return;
        }
        else {
            strPlanId = rowSelected.ID;
            strWorkTask_Id = rowSelected.TASK_ID;
            CONTRACT_TYPE = rowSelected.CONTRACT_TYPE;
            TICKET_NUM = rowSelected.TICKET_NUM;
            $.ligerDialog.open({ title: '任务设置', name: 'winaddtorSetting', width: 600, height: 400, top: 30, url: '../MonitoringPlan/PendingDoTask_Monitor.aspx?strProjectNmae=' + encodeURI(rowSelected.PROJECT_NAME) + '&CONTRACT_TYPE=' + CONTRACT_TYPE + '&strWorkTask_id=' + strWorkTask_Id + '&TICKET_NUM=' + TICKET_NUM, buttons: [
                { text: '确定', onclick: f_SendData },
                { text: '取消', onclick: f_Cancel }
            ]
            });
        }
    }

    function f_SendData(item, dialog) {
        var fn = dialog.frame.GetRequestInfor || dialog.frame.window.GetRequestInfor;
        var fn_data = fn();

        //潘德军 2013-12-23 任务单号可改，且初始不生成
        var fn1 = dialog.frame.getTASK_CODE || dialog.frame.window.getTASK_CODE;
        var data1 = fn1();

        if (data1 == "" || data1 == "未编号") {
            $.ligerDialog.warn('请先设置任务单号！');
            return;
        }

        if (fn_data != "" && data1!="") {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                //潘德军 2013-12-23 任务单号可改，且初始不生成
//                url: "MonitoringPlan.ashx?action=SetTaskQcStatus&strPlanId=" + strPlanId + "&strWorkTask_Id=" + strWorkTask_Id + fn_data,
                url: "MonitoringPlan.ashx?action=SetTaskQcStatus&strTaskNum=" + data1 + "&strPlanId=" + strPlanId + "&strWorkTask_Id=" + strWorkTask_Id + fn_data,
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    if (data == "True") {
                        dialog.close();
                        maingrid.loadData();
                        $.ligerDialog.success('任务已发送至【<a style="color:Red;font-weight:bold">采样任务分配</a>】环节！');
                        return;
                    }
                },
                error: function (msg) {
                    $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                    return;
                }
            });
        }
    }
    //    function SendTask() {
    //        var rowSelected = maingrid.getSelectedRow();
    //        if (rowSelected == null) {
    //            $.ligerDialog.warn('请选择一行进行操作'); return;
    //        }
    //        else {

    //            $.ajax({
    //                cache: false,
    //                async: false, //设置是否为异步加载,此处必须
    //                type: "POST",
    //                url: "MonitoringPlan.ashx?action=SetTaskQcStatus&strWorkTask_Id=" + rowSelected.TASK_ID + "&strQcStatus=1",
    //                contentType: "application/text; charset=utf-8",
    //                dataType: "text",
    //                success: function (data) {
    //                    if (data == "True") {
    //                        maingrid.loadData();
    //                        $.ligerDialog.success('任务已发送至【<a style="color:Red;font-weight:bold">采样任务分配</a>】环节！');
    //                        return;
    //                    }
    //                },
    //                error: function (msg) {
    //                    $.ligerDialog.warn('Ajax加载数据失败！' + msg);
    //                    return;
    //                }
    //            });
    //        }
    //    }

    function CreateData() {
        $.ligerDialog.open({ title: '监测任务新增', name: 'winaddtor', width: 820, height: 550, top: 30, url: '../MonitoringPlan/Contract_QuicklyPlanAdd.aspx?strPlanId=0&strDate=' + TogetDateForDay(new Date(), false), buttons: [
                { text: '确定', onclick: f_SaveDate },
                { text: '取消', onclick: f_Cancel }
            ]
        });
    }

    function DeleteData() {
        var rowSelected = maingrid.getSelectedRow();
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行进行操作'); return;
        }

        $.ligerDialog.confirm('确定要删除该条记录吗？\r\n', function (result) {
            if (result == true) {
                $.ajax({
                    cache: false,
                    async: false, //设置是否为异步加载,此处必须
                    type: "POST",
                    url: "MonitoringPlan.ashx?action=DelContractPlan&strPlanId=" + rowSelected.ID,
                    contentType: "application/text; charset=utf-8",
                    dataType: "text",
                    success: function (data) {
                        if (data != "") {
                            maingrid.loadData();
                            $.ligerDialog.success('数据操作成功！');
                            return;
                        }
                        else {
                            $.ligerDialog.warn('数据操作失败！');
                            return;
                        }
                    },
                    error: function (msg) {
                        $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                        return;
                    }
                });
            } else {
                return;
            }
        });
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
                     { display: "项目名称", name: "SEA_PROJECT_NAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "合同编号", name: "SEA_CONTRACT_CODE", newline: false, type: "text" },
                     { display: "合同年度", name: "SEA_CONTRACT_YEAR", newline: true, type: "select", comboboxName: "SEA_CONTRACT_YEAR_BOX", options: { valueFieldID: "SEA_CONTRACT_YEAR_OP", valueField: "ID", textField: "YEAR", data: vAutoYearItem} },
                     { display: "委托类型", name: "SEA_CONTRACT_TYPE", newline: false, type: "select", comboboxName: "SEA_CONTRACT_TYPE_BOX", options: { valueFieldID: "SEA_CONTRACT_TYPE_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vContratTypeItem} },
                     { display: "受检单位", name: "SEA_TEST_COMPANYNAME", newline: true, type: "text" },
                     { display: "所属区域", name: "SEA_AREA", newline: false, type: "select", comboboxName: "SEA_AREA_BOX", options: { valueFieldID: "SEA_AREA_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vAreaItem} },
                    { display: "办理日期", name: "SEA_PLANDATE", newline: true, type: "date" }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 660, height: 240, top: 90, title: "监测任务查询",
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
            var url = "MonitoringPlan.ashx?action=GetPlanListForQuickly&strQulickly=5&strProjectName=" + SEA_PROJECT_NAME + "&strContractCode=" + SEA_CONTRACT_CODE + "&strContratYear=" + SEA_CONTRACT_YEAR_OP + "&strContractType=" + SEA_CONTRACT_TYPE_OP + "&strCompanyNameFrim=" + SEA_TEST_COMPANYNAME + "&strAreaIdFrim=" + SEA_AREA + "&strDate=" + SEA_PLANDATE;
            maingrid.set('url', url)
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_PROJECT_NAME").val("");
        $("#SEA_CONTRACT_CODE").val("");
        $("#SEA_TEST_COMPANYNAME").val("");
        $("#SEA_PLANDATE").val("");
        $("#SEA_CONTRACT_YEAR_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_CONTRACT_TYPE_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_AREA_BOX").ligerGetComboBoxManager().setValue("");
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

    function f_Cancel(item, dialog) {
        dialog.close();
    }
    function f_SaveDate(item, dialog) {
        var fn = dialog.frame.SaveDate || dialog.frame.window.SaveDate;
        var data = fn();
        if (data == "1") {
            var fn_PlanId = dialog.frame.getPlanId || dialog.frame.window.getPlanId;
            strPlanId = fn_PlanId();

            var fn_Req = dialog.frame.getReturnPageValue || dialog.frame.window.getReturnPageValue;
            var data_Req = fn_Req();
            AcceptPlan(strPlanId, data_Req);
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
    function AcceptPlan(strPlanId, strRequest) {
        $.post("../MonitoringPlan/PlanAdd.aspx?type=doPlan" + strRequest, function (data) {
            // $.post("../MonitoringPlan/TempTaskPlanAdd.aspx?action=doPlanTask" + strRequest, function (data) {
            if (data == "1") {
                maingrid.loadData();
                $.ligerDialog.success('数据保存成功！'); return
                //top.f_addTab("tabSample", '采样任务分配', "../Channels/Mis/Monitor/sampling/QHD/SamplingTaskAllocation.aspx?planid=" + strPlanId);
            }
            else {
                maingrid.loadData();
                $.ligerDialog.warn('数据保存失败！'); return
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

    //    function GetDutyUser(strtask_id, strplan_id) {
    //        var stDutyUser = "";
    //        if (vMonitorArrList != null) {
    //            for (var i = 0; i < vMonitorArrList.length; i++) {
    //                GetContractDutyUser(strtask_id, vMonitorArrList[i].ID, strplan_id);
    //                if (vDutyUserList != null) {
    //                    stDutyUser += vDutyUserList[0].MONITOR_TYPE_NAME + '类:' + vDutyUserList[0].REAL_NAME + '；';
    //                }
    //            }
    //        }
    //        return stDutyUser.substring(0, stDutyUser.length - 1);
    //    }


    function GetDutyUser(strtask_id, strplan_id) {
        var stDutyUser = "";
        if (vMonitorArrList != null) {
            for (var i = 0; i < vMonitorArrList.length; i++) {
                GetContractDutyUser(strtask_id, vMonitorArrList[i].ID, strplan_id);
                if (vDutyUserList != null) {
                    stDutyUser += vMonitorArrList[i].MONITOR_TYPE_NAME + '类:' + vDutyUserList[0].REAL_NAME + '\r\n';
                }
                else {
                    stDutyUser += vMonitorArrList[i].MONITOR_TYPE_NAME + '类:<a style="color:Red">未设置</a>\r\n';
                }
            }
        }
        return stDutyUser;
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
})