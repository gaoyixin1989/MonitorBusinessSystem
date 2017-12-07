/// 站务管理--计划申请单步骤流程(站长审核)
/// 创建时间：2014-06-13
/// 创建人：魏林
var wf_inst_task_id = "", wf_inst_id = "", wf_id = "", vFlowInfo = null, vSubEmployeList = null, DeptItems = null;
var strtaskID = "", vTrainDetail = null, strTempEmployeName = "", objItems = null, strUserList = null, arrUserDetail = null, strTempUserName = "";
var strRealName = "", strUserID = "", strDeptName = "", vPartPlanDetail = null;
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
$(document).ready(function () {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../Employe/EmployeHander.ashx?action=GetDict&type=dept",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                DeptItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    function getUserName(strUserID) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../PartHandler.ashx?action=GetUserInfor&strUserID=" + strUserID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    arrUserDetail = data.Rows;
                    strRealName = arrUserDetail[0].REAL_NAME;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        })
    }
    //获取部门信息
    function getDeptName(strUserID) {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../../../../Sys/General/UserList.aspx/getDeptName",
            data: "{'strValue':'" + strUserID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strDeptName = data.d;
            }
        });
    }
    function GetLoginName() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../PartHandler.ashx?action=GetLoginUserInfor",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    strUserList = data.Rows;
                    strRealName = strUserList[0].REAL_NAME;
                    strUserID = strUserList[0].ID;
                    getDeptName(strUserID);
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    //JS 获取当前时间
    function currentTime() {

        var d = new Date(), str = '';
        str += d.getFullYear() + '-';
        str += d.getMonth() + 1 + '-';
        str += d.getDate();
        return str;
    }

    function toDateFormart(v) {
        var createDate = new Date(Date.parse(v.replace(/-/g, '/')))
        var strData = createDate.getFullYear() + "-";
        strData += (createDate.getMonth() + 1) + "-";
        strData += createDate.getDate();

        return strData;
    }

    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: 1000 });

    $("#PlanDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 });
    
    $("#TechDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 });
    $("#AdminDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 });
    $("#PlanDept").ligerComboBox({ data: DeptItems, width: 200, valueFieldID: 'PlanDept_ID', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false });


    //获取流程业务参数ID，得到业务ID 和流程ID
    wf_inst_task_id = $.getUrlVar("WF_INST_TASK_ID");
    wf_inst_id = $.getUrlVar("WF_INST_ID");
    wf_id = $.getUrlVar("WF_ID");


    if (wf_inst_task_id != "" & wf_inst_id != "") {

        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../../../Mis/Contract/ProgrammingHandler.ashx?action=GetFlowTaskInfo&wf_inst_task_id=" + wf_inst_task_id + "&wf_inst_id=" + wf_inst_id + "&service_key_name=task_id",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vFlowInfo = data.Rows;
                    strtaskID = vFlowInfo[0].SERVICE_KEY_VALUE;
                    $("#hidTaskId").val(strtaskID);
                    GetTrainInfor();
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    else {
        $.ligerDialog.warn('流程业务参数错误！'); return;
    }




    function GetTrainInfor() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../PartHandler.ashx?action=GetPartPushViewList&strtaskID=" + strtaskID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    vPartPlanDetail = data.Rows;
                    SetVauleModify();
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    function SetVauleModify() {

        $("#trTechOption").attr("style", "display:");
        $("#trTechInfor").attr("style", "display:");
        $("#trAdminOption").attr("style", "display:");
        $("#trAdminInfor").attr("style", "display:");

        $("#PlanBt").val(vPartPlanDetail[0].APPLY_TITLE);
        $("#PlanDept").ligerGetComboBoxManager().setValue(vPartPlanDetail[0].APPLY_DEPT_ID);

        $("#PlanDate").ligerGetDateEditorManager().setValue(toDateFormart(vPartPlanDetail[0].APPLY_DATE));
        getUserName(vPartPlanDetail[0].APPLY_USER_ID);
        $("#PlanPerson").val(strRealName);
        
        if (vPartPlanDetail[0].APP_DEPT_DATE != "") {
            $("#TechDate").ligerGetDateEditorManager().setValue(toDateFormart(vPartPlanDetail[0].APP_DEPT_DATE));
        }
        getUserName(vPartPlanDetail[0].APP_MANAGER_ID);
        $("#TechName").val(strRealName);
        $("#TechOption").val(vPartPlanDetail[0].APP_MANAGER_INFO);


        GetPartNeedList();

        GetLoginName();
        $("#AdminName").val(strRealName);

        $("#PlanBt").ligerTextBox({ disabled: true });
        $("#PlanDept").ligerTextBox({ disabled: true });
        $("#PlanPerson").ligerTextBox({ disabled: true });
        $("#PlanDate").ligerGetDateEditorManager().setDisabled();

        $("#TechName").ligerTextBox({ disabled: true });
        $("#TechOption").ligerTextBox({ disabled: true });
        $("#TechDate").ligerGetDateEditorManager().setDisabled();
    }

    function GetPartNeedList() {
        maingrid1 = $("#maingrid").ligerGrid({
            columns: [
            { display: '物料名称', name: 'PART_NAME', align: 'left', width: 160, minWidth: 60 },
            { display: '规格型号', name: 'MODELS', align: 'left', width: 80, minWidth: 60 },
            { display: '需求数量', name: 'NEED_QUANTITY', width: 80, minWidth: 60 },
            { display: '技术要求', name: 'REQUEST', align: 'left', width: 200, minWidth: 60 },
            { display: '要求交货期', name: 'DELIVERY_DATE', width: 200, minWidth: 60 },
            { display: '用途', name: 'USEING', align: 'left', width: 200, minWidth: 60 },
            { display: '计划资金', name: 'BUDGET_MONEY', width: 80, minWidth: 60, render: function (item) {
                return "￥" + item.BUDGET_MONEY;
            }
            }
            ],
            width: '100%', height: '70%',
            pageSizeOptions: [5, 10],
            url: "../PartHandler.ashx?action=GetPartPlanList&strtaskID=" + strtaskID,
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            toolbar: objItems,
            whenRClickToSelect: true,
            onCheckRow: function (data, rowindex, rowobj) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }

        });
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
        maingrid1.toggleCol("USERDO");
        maingrid1.toggleCol("BUDGET_MONEY");
    }
})

function SendSave() {
    $("#hidOptionContent").val($("#AdminOption").val());
    $("#hidUserId").val(strUserID);
    $("#hidOptionDate").val($("#AdminDate").val());
}

//退回
function BackSend() {
    $("#hidBtnType").val("back");
}