// Create by 熊卫华 2013-07-11  环境质量采样容器模块【针对功能区噪声、区域环境噪声、道路交通噪声、空气等】

var strUrl = "SampleEnvContainer.aspx";

$(document).ready(function () {
    $("#divLayout").ligerLayout({ topHeight: '50px' });
    var bodyHeight = $(".l-layout-center:first").height();
    var strMonitorType = $("#hiddenMonitorType").val();
    var strYear = $("#hiddenYear").val();
    var strMonth = $("#hiddenMonth").val();

    //功能区噪声
    if (strMonitorType == "functionnoise")
        $("#iFrame").attr("src", "../../../Env/Fill/NoiseFun/FunctionNoiseFill.aspx?strYear=" + strYear + "&strMonth=" + strMonth);
    //区域环境噪声
    if (strMonitorType == "areanoise")
        $("#iFrame").attr("src", "../../../Env/Fill/NoiseArea/AreaNoiseFill.aspx?strYear=" + strYear + "&strMonth=" + strMonth);
    //道路交通噪声
    if (strMonitorType == "envroadnoise")
        $("#iFrame").attr("src", "../../../Env/Fill/NoiseRoad/RoadNoiseFill.aspx?strYear=" + strYear + "&strMonth=" + strMonth);
    //空气
    if (strMonitorType == "envair")
        $("#iFrame").attr("src", "../../../Env/Fill/Air/AirFill.aspx?strYear=" + strYear + "&strMonth=" + strMonth);
});
//完成操作
function btnFinish() {
    $.ligerDialog.confirm('您确定要完成采样环节？', function (yes) {
        if (yes == true) {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/EnvFinish",
                data: "{'strSubTaskId':'" + $("#hiddenSubTaskId").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                        $.ligerDialog.success('发送成功')
                    }
                    else {
                        $.ligerDialog.warn('发送失败，请检查失败原因');
                    }
                }
            });
        }
    });
}