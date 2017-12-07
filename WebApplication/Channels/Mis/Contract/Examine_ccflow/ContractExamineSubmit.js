//Create By 胡方扬 2012-12-11

//引用父页面vContractInfor变量
$(document).ready(function () {
    //获取当前页面URL参数（包含了流程信息的参数）暂时不用
    strRequest = window.location.search;
    strRequest = strRequest.substring(1, strRequest.length);
})

function SendSave() {
    $("#hidTaskId").val(task_id);
    $("#hidBtnType").val("send");
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
}
function BackSend() {
    $("#hidTaskId").val(task_id);
    $("#hidBtnType").val("back");
    //防止多次点击按钮产生多个任务
    $("#divContratSubmit")[0].style.display = "none";
}