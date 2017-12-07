var strUrl = "FillMaterials.aspx";
var isSend = false;
var maingrid1 = null, objItems = null;
var wf_inst_task_id = "", wf_inst_id = "", wf_id = "", vFlowInfo = null;
var isAdd = true, isView = false, strView = "", strType = "", strWf = "", strWFfirst = "", strIsWfStart = "1";
var strtaskID = "", vPartPlanDetail = null, strUserList = null, arrUserDetail = null, strRealName = "", strUserID = "", strDeptName = "", strPartId = "", strPartPlanId = "", task_order = "", strDept = "", strDate = "", strBt = "", strStatus = '0'; DeptItems = "";
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
    $("#PlanBt").attr("validate", "[{required:true, msg:'请输入申请主题'},{minlength:2,msg:'申请主题最小长度为2!'}]");
    $("#PlanDept").attr("validate", "[{required:true, msg:'请输入申请部门'},{minlength:2,msg:'申请部门最小长度为2!'}]");
    $("#PlanDate").attr("validate", "[{required:true, msg:'请输入申请日期'}]");
    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: 1000 });
    $("#PlanDept").ligerComboBox({ data: Depts(), width: 200, valueFieldID: 'PlanDept_ID', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false }); //申请部门
    $("#PlanDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 }); //申请时间
    $("#TestDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 }); //科室主任日期
    $("#TechDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 }); //仓库管理员日期
    //获取用户名
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
    //获取用户信息
//    function GetUser() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../PartHandler.ashx?action=GetLoginUserInfor",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null&&data.Total>0) {
                    strUserList = data.Rows;
                    strRealName = strUserList[0].REAL_NAME;
                    strUserID = strUserList[0].ID;
                    getDeptName(strUserID);
                }
                else {
                    $.ligerDialog.warn('获取申请人失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
//    }
    strType = true;
    strType = $.getUrlVar('type');
    strWf = $.getUrlVar('WF_ID');
    //    strtaskID = $.getUrlVar('hidTaskId');
    if (!strType) {
        //获取流程业务参数ID，得到业务ID 和流程ID
        wf_inst_task_id = $.getUrlVar("WF_INST_TASK_ID");
        wf_inst_id = $.getUrlVar("WF_INST_ID");
        wf_id = $.getUrlVar("WF_ID");
        task_order = $.getUrlVar('TASK_ORDER');
        if (wf_inst_task_id != "" & wf_inst_id != "") {
            //第二个环节科主任
            if (task_order != "" && task_order == "2") {
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
                            $("#Task_Order").val(task_order);
                            GetPartPlanInfor();
                            SetVauleModify();
                        }
                    },
                    error: function (msg) {
                        $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                    }
                });
                $("#trTechOption").attr("style", "display:none"); //仓管意见
                $("#trTechInfor").attr("style", "display:none"); //仓管的时间和签名
                $("#PlanDate").ligerGetDateEditorManager().setDisabled(); //申请时间
                $("#PlanDept").ligerGetDateEditorManager().setDisabled(); //申请部门
                $("#PlanBt").ligerTextBox().setDisabled(); //申请主题
                $("#PlanPerson").ligerTextBox().setDisabled(); //申请人
                $("#TestDate").ligerGetDateEditorManager().setDisabled(); //日期
                $("#TestName").ligerTextBox().setDisabled(); //签名
            }
            //第三个环节仓管员
            if (task_order != "" && task_order == "3") {
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
                            $("#Task_Order").val(task_order);
                            GetPartPlanInfor();
                            SetVauleModify();
                        }
                    },
                    error: function (msg) {
                        $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                    }
                });
                $("#PlanDate").ligerGetDateEditorManager().setDisabled(); //申请时间
                $("#PlanDept").ligerGetDateEditorManager().setDisabled(); //申请部门
                $("#PlanBt").ligerTextBox().setDisabled(); //申请主题
                $("#PlanPerson").ligerTextBox().setDisabled(); //申请人
                $("#TestDate").ligerGetDateEditorManager().setDisabled(); //日期
                $("#TestName").ligerTextBox().setDisabled(); //签名
                $("#TestOption").ligerTextBox().setDisabled(); //科室意见
                $("#TechName").ligerTextBox().setDisabled(); //仓管员签名
                $("#TechDate").ligerTextBox().setDisabled(); //仓管员意见
            }
        }
        else {
            $.ligerDialog.warn('流程业务参数错误！'); return;
        }
    }
    else {
        if (strType == "true") {
            isAdd = true;
            //            task_order == "1"
            $("#PlanDate").ligerGetDateEditorManager().setDisabled(); //申请时间
            $("#PlanPerson").ligerTextBox().setDisabled(); //申请人
            $("#PlanPerson").val(strRealName);
            $("#trTestOption").attr("style", "display:none");
            $("#trTestPInfor").attr("style", "display:none");
            $("#trTechOption").attr("style", "display:none");
            $("#trTechInfor").attr("style", "display:none");
        }
        else {
            isAdd = false;
            strtaskID = $.getUrlVar('strtaskID');
            strView = $.getUrlVar('view');
            GetPartPlanInfor();
            if (strView == "true") {
                isView = true;
            }
            SetVauleModify();
        }
    }
    function SetVauleModify() {
        $("#PlanBt").val(vPartPlanDetail[0].APPLY_TITLE);
        $("#PlanDept").ligerGetComboBoxManager().setValue(vPartPlanDetail[0].APPLY_DEPT_ID);
        $("#PlanDate").ligerGetDateEditorManager().setValue(toDateFormart(vPartPlanDetail[0].APPLY_DATE));
        $("#TestName").val(strRealName);
        $("#TechName").val(strRealName);
        if (vPartPlanDetail[0].APPLY_USER_ID != "") {
            getUserName(vPartPlanDetail[0].APPLY_USER_ID);
            $("#PlanPerson").val(strRealName);
        }
        //科室主任的审核意见、时间和办理人
        if (vPartPlanDetail[0].APP_DEPT_DATE != "") {
            $("#TestDate").ligerGetDateEditorManager().setValue(toDateFormart(vPartPlanDetail[0].APP_DEPT_DATE));
        }
        if (vPartPlanDetail[0].APP_DEPT_ID != "") {
            getUserName(vPartPlanDetail[0].APP_DEPT_ID);
            $("#TestName").val(strRealName);
        }
        $("#TestOption").val(vPartPlanDetail[0].APP_DEPT_INFO);
        //仓管员的审核意见、时间和办理人
        if (vPartPlanDetail[0].APP_MANAGER_DATE != "") {
            $("#TechDate").ligerGetDateEditorManager().setValue(toDateFormart(vPartPlanDetail[0].APP_MANAGER_DATE));
        }
        if (vPartPlanDetail[0].APP_MANAGER_ID != "") {
            getUserName(vPartPlanDetail[0].APP_MANAGER_ID);
            $("#TechName").val(strRealName);
        }
        $("#TechOption").val(vPartPlanDetail[0].APP_MANAGER_INFO);
        if (isView == true) {
            objItems = null;
            $("#PlanBt").ligerTextBox({ disabled: true });
            $("#PlanDept").ligerTextBox({ disabled: true });
            $("#PlanPerson").ligerTextBox({ disabled: true });
            $("#PlanDate").ligerGetDateEditorManager().setDisabled();
            $("#TestName").ligerTextBox({ disabled: true });
            $("#TestOption").ligerTextBox({ disabled: true });
            $("#TestDate").ligerGetDateEditorManager().setDisabled();
            $("#TechName").ligerTextBox({ disabled: true });
            $("#TechOption").ligerTextBox({ disabled: true });
            $("#TechDate").ligerGetDateEditorManager().setDisabled();
            $("#divContratSubmit").remove();
        }
    }
    GetPartNeedList(); //显示物料信息
    $("#btnFileUp").bind("click", function () {
        SaveSWDate();
    })
    $("#btnFiledownLoad").bind("click", function () {
        downLoadFile();
    })
});

function SaveSWDate() {
    if (strtaskID == "" || strtaskID == "undefined") {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: strUrl + "/saveSWData",
            data: GetSWInputtInfo(),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    strtaskID = data;
                    $("#hidTaskId_Load").val(strtaskID);
                    upLoadFile();
                }
                else {
                    return;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('AJAX数据请求失败！');
                return;
            }
        });
    }
    else {
        upLoadFile();
    }
}
//得到基本信息保存参数
function GetSWInputtInfo() {
    var strData = "";
    strData += "{";
    strData += "'Remark5':'上传时插入一条数据'"; //备注
    strData += "}";

    return strData;
}
///附件上传
function upLoadFile() {
    if (strtaskID == "") {
        return;
    }
    $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
        buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        dialog.frame.upLoadFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileUpload.aspx?filetype=PartFile&id=' + strtaskID
    });
}
///附件下载
function downLoadFile() {
    if (strtaskID == "") {
        $.ligerDialog.warn('业务ID参数错误');
        return;
    }
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?filetype=PartFile&id=' + strtaskID
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
    //获取部门
function Depts() {
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
    return DeptItems;
}

//显示物料信息
    function GetPartNeedList() {
        maingrid1 = $("#maingrid").ligerGrid({
            columns: [
            { display: '物料名称', name: 'PART_NAME', align: 'left', width: 120, minWidth: 60 },
            { display: '规格型号', name: 'MODELS', align: 'left', width: 80, minWidth: 60 },
            { display: '需求数量', name: 'NEED_QUANTITY', width: 80, minWidth: 60 },
//            { display: '技术要求', name: 'REQUEST', align: 'left', width: 150, minWidth: 60 }, 
            { display: '要求交货期', name: 'DELIVERY_DATE', width: 150, minWidth: 60 },
//            { display: '用途', name: 'USEING', align: 'left', width: 200, minWidth: 60 },
            { display: '本次采购用途', name: 'USERDO', align: 'left', width: 100, minWidth: 60 },
            { display: '计划资金', name: 'BUDGET_MONEY', width: 80, minWidth: 60, render: function (item) {
                return "￥" + item.BUDGET_MONEY;
            }
            }
          ],
            width: '100%', height: '200',
            pageSizeOptions: [5, 10],
            url: "../PartHandler.ashx?action=GetPartPlanList&strGetType=1&strtaskID=" + strtaskID,
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            rownumbers: false,//是否启用索引行
            toolbar: { items: [{ id: 'add', text: '新增', click: f_save, icon: 'add' }, { line: true }, { id: 'modify', text: '修改', click: f_modify, icon: 'modify' }, { line: true }, { id: 'del', text: '删除', click: f_delete, icon: 'delete'}] },
            whenRClickToSelect: true, 
            onCheckRow: function (data, rowindex, rowobj) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
                strFileId = rowindex.ID;
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }
        });
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
    }

function GetPartPlanInfor() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../PartHandler.ashx?action=GetPartPushViewList&strtaskID=" + strtaskID,
        contentType: "application/josn; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vPartPlanDetail = data.Rows;
            }
            else {
                $.ligerDialog.warn('数据保存失败！'); return;
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载数据失败！'); return
        }
    });
}
function GetBaseData() {
    var sDate = "";
    var isTrue = $("#form1").validate();
    if (isTrue) {
        strDate = $("#PlanDate").val();
        strBt = encodeURI($("#PlanBt").val());
        strDept = $("#PlanDept_ID").val();
        sDate += "&strUserID=" + strUserID;
        sDate += "&strDate=" + strDate;
        sDate += "&strBt=" + strBt;
        sDate += "&strDept=" + strDept;
        sDate += "&strStatus=" + strStatus;
        sDate += "&strtaskID=" + strtaskID;
    }
    return sDate;
}
function f_add() {
    var isTrue = $("#form1").validate();
    if (isTrue) {
        $.ligerDialog.open({ title: '采购信息增加', top: 90, width: 800, height: 400, buttons:
        [{ text: '确定', onclick: f_SaveDateGrid },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../PartPushAddEdit.aspx?strtaskID=' + strtaskID + '&strPartPlanId=' + strPartPlanId
        });
    }
}
//新增
function f_save() {
    var strRequest = GetBaseData();
    if (strRequest != "") {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../PartHandler.ashx?action=SaveBaseData" + strRequest + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    strtaskID = data;
                    $("#hidTaskId").val(strtaskID);
                    maingrid1.loadData();
                    if (isSend == false) {
                        f_add();
                    }
                    return;
                }
                else {
                    $.ligerDialog.warn('数据保存失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！'); return
            }
        });
    }
}
//修改
function f_modify() {
    var rowSelected = maingrid1.getSelectedRow();
    if (rowSelected == null) {
        $.ligerDialog.warn("请选择一行");
        return;
    } else {
        $.ligerDialog.open({ title: '采购信息修改', top: 90, width: 800, height: 400, buttons:
        [{ text: '确定', onclick: f_SaveDateGrid },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../PartPushAddEdit.aspx?strtaskID=' + strtaskID + '&strPartPlanId=' + rowSelected.ID
        });
    }
}
//删除
function f_delete() {
    var rowSelected = maingrid1.getSelectedRow();
    if (rowSelected == null) {
        $.ligerDialog.warn("请选择一行");
        return;
    } else {
        $.ligerDialog.confirm('确定要删除该条记录？\r\n', function (result) {
            if (result == true) {
                $.ajax({
                    cache: false,
                    async: false, //设置是否为异步加载,此处必须
                    type: "POST",
                    url: "../PartHandler.ashx?action=DeletePartBuyRequstLstInfor&strPartPlanId=" + rowSelected.ID,
                    contentType: "application/text; charset=utf-8",
                    dataType: "text",
                    success: function (data) {
                        if (data != "") {
                            maingrid1.loadData();
                        }
                        else {
                            $.ligerDialog.warn('删除失败！');
                        }
                    },
                    error: function (msg) {
                        $.ligerDialog.warn('Ajax访问失败！' + msg);
                    }
                });
            }
            else {
                return;
            }
        })
    } 
}
//保存数据
function f_SaveDateGrid(item, dialog) {

    var fnSave = dialog.frame.GetBaseInfoStr || dialog.frame.window.GetBaseInfoStr;
    var strReques = fnSave();
    if (strReques != "") {
        SaveDataGrid(strReques);
        dialog.close();
    }
}
function SaveDataGrid(strReques) {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../PartHandler.ashx?action=SavePartPlanDate" + strReques + "",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                maingrid1.set('url', '../PartHandler.ashx?action=GetPartPlanList&strtaskID=' + strtaskID)
                $.ligerDialog.success('数据保存成功！');
                return;
            }
            else {
                $.ligerDialog.warn('数据保存失败！'); return;
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载数据失败！'); return
        }
    });
}
function toDateFormart(v) {
    var createDate = new Date(Date.parse(v.replace(/-/g, '/')))
    var strData = createDate.getFullYear() + "-";
    strData += (createDate.getMonth() + 1) + "-";
    strData += createDate.getDate();
    return strData;
}

function SendSave() {
    if (task_order == "") {//第一个环节
        $("#hidTaskProjectName").val($("#PlanBt").val());
        $("#Task_Order").val(task_order);
        isSend = true;
        f_save();
    }else if (task_order != "" && task_order == "2") {//第二个环节
        $("#hidOptionContent").val($("#TestOption").val());
        $("#hidUserId").val(strUserID);
        $("#hidOptionDate").val($("#TestDate").val());
    } else  if (task_order != "" && task_order == "3") {//第三个环节
        $("#hidOptionContent").val($("#TechOption").val());
        $("#hidUserId").val(strUserID);
        $("#hidOptionDate").val($("#TechDate").val());
    }
   
}