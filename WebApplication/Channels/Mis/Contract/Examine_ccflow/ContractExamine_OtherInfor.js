//Create By 胡方扬
//用户控件 委托书录入
var strContratId = "", strProData = "", strOtherAsk = "", strAccording = "", strtxtRemarks = "";
var cacheSelectSave = false;
var strType =
$(document).ready(function () {
    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="其他要求"/><span>其他要求说明</span>';
    $(strdivImg).appendTo(divImgOther);
    $("#txtAccording").val("依国家规范监测，按照站内规范执行。");

    SetInputDisable();


    function SetInputDisable() {
        if (vContractInfor != null) {
            strProData = vContractInfor[0].PROVIDE_DATA;
            strOtherAsk = vContractInfor[0].OTHER_ASKING;
            strAccording = vContractInfor[0].MONITOR_ACCORDING;
            strtxtRemarks = vContractInfor[0].REMARK2;
        }
        $("#txtProData").val(strProData);
        $("#txtOtherAsk").val(strOtherAsk);
        $("#txtAccording").val(strAccording);
        $("#txtRemarks").val(strtxtRemarks);

        $("#txtProData").ligerTextBox({ disabled: true });
        $("#txtOtherAsk").ligerTextBox({ disabled: true });
        $("#txtAccording").ligerTextBox({ disabled: true });
        $("#txtRemarks").ligerTextBox({ disabled: true });
    }
})
function GetOtherInfor() {
    strProData = $("#txtProData").val();
    strOtherAsk = $("#txtOtherAsk").val();
    strAccording = $("#txtAccording").val();
    strtxtRemarks = $("#txtRemarks").val();
}
