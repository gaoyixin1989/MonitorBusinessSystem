//Create By 胡方扬 2012-12-11
//委托编制阶段数据保存提交
//引用父页面vContractInfor变量
$(document).ready(function () {
})
function SendSave() {
    $("#hidTaskId").val(task_id);
    $("#hidBtnType").val("send");
}
function BackSend() {
    $("#hidTaskId").val(task_id);
    $("#hidBtnType").val("back");
}