//Create By 胡方扬
//用户控件 委托书录入
var strContratId = "", strProData = "", strOtherAsk = "", strAccording = "", strtxtRemarks = "";
var cacheSelectSave = false;
$(document).ready(function () {
    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="其他要求"/><span>其他要求说明</span>';
    $(strdivImg).appendTo(divImgOther);
    $("#txtAccording").val("依国家规范监测，按照站内规范执行。");

    if (isAdd == false || !strType) {
        SetInputDisable();
    }

    function SetInputDisable() {
        if (isAdd == false || !strType) {
            if (vContractList != null) {
                strProData = vContractList[0].PROVIDE_DATA;
                strOtherAsk = vContractList[0].OTHER_ASKING;
                strAccording = vContractList[0].MONITOR_ACCORDING;
                strtxtRemarks = vContractList[0].REMARK2;
            }
            $("#txtProData").val(strProData);
            $("#txtOtherAsk").val(strOtherAsk);
            $("#txtAccording").val(strAccording);
            $("#txtRemarks").val(strtxtRemarks);
            if (isView == true) {
                $("#txtProData").ligerTextBox({ disabled: true });
                $("#txtOtherAsk").ligerTextBox({ disabled: true });
                $("#txtAccording").attr("disabled", "disabled");
                $("#txtRemarks").attr("disabled", "disabled");
                $("#txtAccording").attr("style", "height:60px;width:580px;background-color:#E0E0E0;");
                $("#txtRemarks").attr("style", "height:60px;width:580px;background-color:#E0E0E0;");
            }
        }
    }
})
function GetOtherInfor() {
    strProData = $("#txtProData").val();
    strOtherAsk = $("#txtOtherAsk").val();
    strAccording = $("#txtAccording").val();
    strtxtRemarks = $("#txtRemarks").val();
}
