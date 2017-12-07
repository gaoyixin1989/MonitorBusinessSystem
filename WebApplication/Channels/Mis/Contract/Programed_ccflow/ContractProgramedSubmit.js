//Create By 胡方扬 2012-12-11
//委托编制阶段数据保存提交

//引用父页面vContractInfor变量
$(document).ready(function () {
    //获取当前页面URL参数（包含了流程信息的参数）
    $("#btnSend").val("办结");
})

function SendSave() {
    $("#hidTaskId").val(task_id);
    $("#hidBtnType").val("send");
    $("#hidCompanyId").val(vContractInfor[0].TESTED_COMPANY_ID);
    $("#btnSend").attr("disabled", "disabled");
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
}
function BackSend() {
    $("#hidTaskId").val(task_id);
    $("#hidBtnType").val("back");
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
}