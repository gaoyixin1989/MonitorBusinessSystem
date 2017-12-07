//Create By 胡方扬
//用户控件 委托书录入
var strContratId = "", strQCList = "", strQCStep = "";
var objQcList = null;
var cacheSelectSave = false;
var strType =
$(document).ready(function () {

    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../MethodHander.ashx?action=GetDict&type=QC_SET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                objQcList = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax请求数据失败！');
        }
    });
    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="质控要求"/><span>其他要求说明</span>';
    $(strdivImg).appendTo(divImgQC);

    $("#txtQC").ligerComboBox({ data: objQcList, width: 525, valueFieldID: 'QCLIST_BOX', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isShowCheckBox: true, isMultiSelect: true });

    SetInputDisable();


    function SetInputDisable() {
        if (vContractInfor != null) {
            strQCStep = vContractInfor[0].QC_STEP;
            strQCList = vContractInfor[0].QCRULE;
            $("#txtQC").ligerGetComboBoxManager().setValue(strQCList);
        }

        $("#txtQcStep").val(strQCStep);


        //        $("#txtQcStep").ligerTextBox({ disabled: true });
        //        $("#txtQC").ligerTextBox({ disabled: true });

    }
})
function GetQCInfor() {
    strQCStep = $("#txtQcStep").val();
    strQCList = $("#txtQC").val();
}
