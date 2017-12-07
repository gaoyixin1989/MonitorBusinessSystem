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
    $("#PlanDept").attr("validate", "[{required:true, msg:'请输入呈报科室'},{minlength:2,msg:'申请科室最小长度为2!'}]");
    $("#ManageDate").attr("validate", "[{required:true, msg:'请输入办理日期'}]");
    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: 1000 });
    $("#PlanDept").ligerComboBox({ data: Depts(), width: 200, valueFieldID: 'PlanDept_ID', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false }); //呈报科室
    $("#ManageDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 }); //办理日期
    $("#AgentDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 }); //经办人日期
    $("#ChiefDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 }); //科室日期
    $("#LeaderDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 }); //主管领导日期
    $("#OfferDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 }); //办公室日期
    $("#SerialDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 }); //站长日期
    //获取用户信息 
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
                }
                else {
                    $.ligerDialog.warn('获取申请人失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    strType = true;
    strType = $.getUrlVar('type');
    strWf = $.getUrlVar('WF_ID');
    if (strType) {
        if (strType == "true") {
            isAdd = true;
            $("#AgentPerson").val(strRealName);
            $("#trTestOption").attr("style", "display:none");
            $("#trTestPInfor").attr("style", "display:none");
            $("#trTechOption").attr("style", "display:none");
            $("#trTechInfor").attr("style", "display:none");
            $("#trOffer").attr("style", "display:none");
            $("#trContext").attr("style", "display:none");
            $("#trSerial").attr("style", "display:none");
            $("#trSerialInfo").attr("style", "display:none");
            $("#AgentDate").ligerGetDateEditorManager().setDisabled(); //经办人
            $("#AgentPerson").ligerTextBox().setDisabled(); //经办日期
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
    else {
        //获取流程业务参数ID，得到业务ID 和流程ID、序列号
        wf_inst_task_id = $.getUrlVar("WF_INST_TASK_ID");
        wf_inst_id = $.getUrlVar("WF_INST_ID");
        wf_id = $.getUrlVar("WF_ID");
        task_order = $.getUrlVar('TASK_ORDER');
        if (wf_inst_task_id != "" & wf_inst_id != "") {
            if (task_order != "" && task_order == "2") {//科室意见
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
                $("#PlanDept").ligerGetDateEditorManager().setDisabled(); //申请部门
                $("#ManageDate").ligerGetDateEditorManager().setDisabled(); //办理时间
                $("#AgentDate").ligerGetDateEditorManager().setDisabled(); //经办人
                $("#AgentPerson").ligerTextBox().setDisabled(); //经办日期
                $("#ChiefPerson").ligerTextBox().setDisabled(); //科室签名
                $("#ChiefDate").ligerGetDateEditorManager().setDisabled(); //科室日期
                $("#trTechOption").attr("style", "display:none"); //主管意见
                $("#trTechInfor").attr("style", "display:none"); //主管签名和时间
                $("#trOffer").attr("style", "display:none"); //办公室意见
                $("#trContext").attr("style", "display:none"); //办公室签名、时间
                $("#trSerial").ligerTextBox().setDisabled(); //领导意见
                $("#trSerial").attr("style", "display:none"); //领导意见
                $("#trSerialInfo").attr("style", "display:none"); //领导签名、时间
            }
            else if (task_order != "" && task_order == "3") {//主管领导
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
                $("#PlanDept").ligerGetDateEditorManager().setDisabled(); //申请部门
                $("#ManageDate").ligerGetDateEditorManager().setDisabled(); //办理时间
                $("#AgentDate").ligerGetDateEditorManager().setDisabled(); //经办人
                $("#AgentPerson").ligerTextBox().setDisabled(); //经办日期
                $("#TestOption").ligerTextBox().setDisabled(); //科室意见
                $("#ChiefDate").ligerGetDateEditorManager().setDisabled(); //科室日期
                $("#ChiefPerson").ligerTextBox().setDisabled(); //科室签名
                $("#LeaderDate").ligerGetDateEditorManager().setDisabled(); //主管日期
                $("#LeaderName").ligerTextBox().setDisabled(); //主管签名
                $("#trOffer").attr("style", "display:none"); //办公室意见
                $("#trContext").attr("style", "display:none"); //办公室签名、时间
                $("#trSerial").attr("style", "display:none"); //领导意见
                $("#trSerialInfo").attr("style", "display:none"); //领导签名、时间
            }
            else if (task_order != "" && task_order == "4") {//办公室
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
                $("#PlanDept").ligerGetDateEditorManager().setDisabled(); //申请部门
                $("#ManageDate").ligerGetDateEditorManager().setDisabled(); //办理时间
                $("#AgentDate").ligerGetDateEditorManager().setDisabled(); //经办人
                $("#AgentPerson").ligerTextBox().setDisabled(); //经办日期
                $("#TestOption").ligerTextBox().setDisabled(); //科室意见
                $("#ChiefDate").ligerGetDateEditorManager().setDisabled(); //科室日期
                $("#ChiefPerson").ligerTextBox().setDisabled(); //科室签名
                $("#TechOption").ligerTextBox().setDisabled(); //主管意见
                $("#LeaderDate").ligerGetDateEditorManager().setDisabled(); //主管日期
                $("#LeaderName").ligerTextBox().setDisabled(); //主管签名
                $("#OfferDate").ligerGetDateEditorManager().setDisabled(); //办公室时间
                $("#OfferName").ligerTextBox().setDisabled(); //办公室签名
                $("#trSerial").attr("style", "display:none"); //领导意见
                $("#trSerialInfo").attr("style", "display:none"); //领导签名、时间
            }
            else if (task_order != "" && task_order == "5") { //领导
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
                $("#PlanDept").ligerGetDateEditorManager().setDisabled(); //申请部门
                $("#ManageDate").ligerGetDateEditorManager().setDisabled(); //办理时间
                $("#AgentDate").ligerGetDateEditorManager().setDisabled(); //经办人
                $("#AgentPerson").ligerTextBox().setDisabled(); //经办日期
                $("#TestOption").ligerTextBox().setDisabled(); //科室意见
                $("#ChiefDate").ligerGetDateEditorManager().setDisabled(); //科室日期
                $("#ChiefPerson").ligerTextBox().setDisabled(); //科室签名
                $("#TechOption").ligerTextBox().setDisabled(); //主管意见
                $("#LeaderDate").ligerGetDateEditorManager().setDisabled(); //主管日期
                $("#LeaderName").ligerTextBox().setDisabled(); //主管签名
                $("#Textarea1").ligerTextBox().setDisabled(); //办公室意见
                $("#OfferDate").ligerGetDateEditorManager().setDisabled(); //办公室时间
                $("#OfferName").ligerTextBox().setDisabled(); //办公室签名
                $("#SerialDate").ligerGetDateEditorManager().setDisabled(); //领导日期
                $("#SerialName").ligerTextBox().setDisabled(); //领导签名
            }
        }
        else {
            $.ligerDialog.warn('流程业务参数错误！'); return;
        }
    }
    GetPartNeedList();
});
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
        width: '100%', height: '150', 
        pageSizeOptions: [5, 10],
        url: "../PartHandler.ashx?action=GetPartPlanList&strGetType=1&strtaskID=" + strtaskID,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        rownumbers: false, //是否启用索引行
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
function GetBaseData() {
    var sDate = "";
    var isTrue = $("#form1").validate();
    if (isTrue) {
        strDate = $("#ManageDate").val();
//        strBt = encodeURI($("#PlanDept").val());//主题
        strDept = encodeURI($("#PlanDept_ID").val()); //部门
        sDate += "&strUserID=" + strUserID;
        sDate += "&strDate=" + strDate;
        sDate += "&strBt=" + strBt;
        sDate += "&strDept=" + strDept;
        sDate += "&strStatus=" + strStatus;
        sDate += "&strtaskID=" + strtaskID;
    }
    return sDate;
}
//发送时保存
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
//新增
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
//修改
function f_modify() {
    var rowSelected = maingrid1.getSelectedRow();
    if (rowSelected == null) {
        $.ligerDialog.warn("请选择一行");
        return;
    } else {
        $.ligerDialog.open({ title: '呈报信息修改', top: 90, width: 800, height: 400, buttons:
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
 function SetVauleModify() {
     $("#PlanDept").ligerGetComboBoxManager().setValue(vPartPlanDetail[0].APPLY_DEPT_ID); //呈报科室 
     $("#AgentPerson").val(strRealName); //经办人
     $("#ChiefPerson").val(strRealName); //室主任名称
     $("#LeaderName").val(strRealName);
     $("#OfferName").val(strRealName);
     $("#SerialName").val(strRealName);
     if (vPartPlanDetail[0].APP_DEPT_DATE != "") {
         $("#ChiefDate").ligerGetDateEditorManager().setValue(toDateFormart(vPartPlanDetail[0].APP_DEPT_DATE));//科室日期
     }
     $("#TestOption").val(vPartPlanDetail[0].APP_DEPT_INFO); //科室意见
     if (vPartPlanDetail[0].APP_DEPT_ID != "") {
         getUserName(vPartPlanDetail[0].APP_DEPT_ID)
         $("#ChiefPerson").val(strRealName); //室主任名称
     }
     //主管领导的审核意见、时间和办理人
     if (vPartPlanDetail[0].APP_MANAGER_DATE != "") {
         $("#LeaderDate").ligerGetDateEditorManager().setValue(toDateFormart(vPartPlanDetail[0].APP_MANAGER_DATE));
     }
     if (vPartPlanDetail[0].APP_MANAGER_ID != "") {
         getUserName(vPartPlanDetail[0].APP_MANAGER_ID);
         $("#LeaderName").val(strRealName);
     }
     $("#TechOption").val(vPartPlanDetail[0].APP_MANAGER_INFO);
     //办公室领导的审核意见、时间和办理人
     if (vPartPlanDetail[0].APP_OFFER_TIME != "") {
         $("#OfferDate").ligerGetDateEditorManager().setValue(toDateFormart(vPartPlanDetail[0].APP_OFFER_TIME));
     }
     if (vPartPlanDetail[0].APP_OFFER_ID != "") {
         getUserName(vPartPlanDetail[0].APP_OFFER_ID);
         $("#OfferName").val(strRealName);
     }
     $("#Textarea1").val(vPartPlanDetail[0].APP_OFFER_INFO);
     //领导的审核意见、时间和办理人
     if (vPartPlanDetail[0].APP_LEADER_DATE != "") {
         $("#SerialDate").ligerGetDateEditorManager().setValue(toDateFormart(vPartPlanDetail[0].APP_LEADER_DATE));
     }
     if (vPartPlanDetail[0].APP_LEADER_ID != "") {
         getUserName(vPartPlanDetail[0].APP_LEADER_ID);
         $("#SerialName").val(strRealName);
     }
     $("#Textarea2").val(vPartPlanDetail[0].APP_LEADER_INFO);

     if (isView == true) {
         objItems = null;
         $("#Textarea2").ligerTextBox().setDisabled(); //领导意见
         $("#TechOption").ligerTextBox().setDisabled(); //主管意见
         $("#Textarea1").ligerTextBox().setDisabled(); //办公室意见
         $("#TestOption").ligerTextBox().setDisabled(); //科室意见
         $("#PlanDept").ligerTextBox({ disabled: true });
         $("#ManageDate").ligerGetDateEditorManager().setDisabled();
         $("#AgentPerson").ligerTextBox({ disabled: true });
         $("#AgentDate").ligerGetDateEditorManager().setDisabled();
         $("#ChiefPerson").ligerTextBox({ disabled: true });
         $("#ChiefDate").ligerGetDateEditorManager().setDisabled();
         $("#LeaderName").ligerTextBox({ disabled: true });
         $("#LeaderDate").ligerGetDateEditorManager().setDisabled();
         $("#OfferName").ligerTextBox({ disabled: true });
         $("#OfferDate").ligerGetDateEditorManager().setDisabled();
         $("#SerialName").ligerTextBox({ disabled: true });
         $("#SerialDate").ligerGetDateEditorManager().setDisabled();
         $("#divContratSubmit").remove();
     }
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
 function SendSave() {
     if (task_order == "") {//第一个环节
         $("#hidTaskProjectName").val($("#PlanDept").val());
         $("#Task_Order").val(task_order);
         isSend = true;
         f_save();
     } else if (task_order != "" && task_order == "2") {//第二个环节
         $("#hidOptionContent").val($("#TestOption").val());
         //$("#hidUserId").val($("#ChiefPerson").val());//室主任的值
         $("#hidUserId").val(strUserID); //strUserID
         $("#hidOptionDate").val($("#ChiefDate").val());
     } else if (task_order != "" && task_order == "3") {//第三个环节
         $("#hidOptionContent").val($("#TechOption").val());
//         $("#hidUserId").val($("#LeaderName").val()); //主管领导名字
         $("#hidUserId").val(strUserID); //strUserID
         $("#hidOptionDate").val($("#LeaderDate").val());
     } else if (task_order != "" && task_order == "4") {//第四个环节
         $("#hidOptionContent").val($("#Textarea1").val());
//         $("#hidUserId").val($("#OfferName").val()); //办公室名字
         $("#hidUserId").val(strUserID); //strUserID
         $("#hidOptionDate").val($("#OfferDate").val());
     } else if (task_order != "" && task_order == "5") {//第五个环节
         $("#hidOptionContent").val($("#Textarea2").val());
//         $("#hidUserId").val($("#SerialName").val()); 
         $("#hidUserId").val(strUserID); //strUserID
         $("#hidOptionDate").val($("#SerialDate").val());
     }
 }

 function toDateFormart(v) {
     var createDate = new Date(Date.parse(v.replace(/-/g, '/')))
     var strData = createDate.getFullYear() + "-";
     strData += (createDate.getMonth() + 1) + "-";
     strData += createDate.getDate();
     return strData;
 }