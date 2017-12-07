//Create By 胡方扬 2012-12-10
//任务下达 环节 获取委托书 基本信息
var MonitorTypeName = "", ContractTypeName = "", Remarks = "", ConstSum = '0', strConsUrl = "", ConstIdent = "0";
var vMonitorType = null, vContratTypeItem = null, vRemarkItems = null, vConstItems = null;
$(document).ready(function () {

    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="项目信息"/><span>项目信息</span>';
    $(strdivImg).appendTo(divImgInfo);

//    $("#txtPLAN_DATE").ligerDateEditor({});
//    $("#txtFINISH_DATE").ligerDateEditor({});
})

function GetInivalue() {

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../../Mis/Contract/MethodHander.ashx?action=GetMonitorType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vMonitorType = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../../Mis/Contract/MethodHander.ashx?action=GetDict&type=Contract_Type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vContratTypeItem = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });


    //获取核对委托信息的 备注信息
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../../Mis/Contract/MethodHander.ashx?action=GetDict&type=Contract_Remarks",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vRemarkItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    SetLabelValue();

}


function SetLabelValue() {
    GetIdValueName();
    $("#Project_Name").val(vContractInfor[0].PROJECT_NAME);
    $("#Contract_Type").val(ContractTypeName);
    $("#Contrat_Year").val(vContractInfor[0].CONTRACT_YEAR);
    $("#ContractCode").val(vContractInfor[0].CONTRACT_CODE);
    $("#ContractConst").val(vContractInfor[0].REMARK1);
    //$("#ContractConst").attr("style", "background-image:url(../../../../Images/Icons/money_yen.png);background-position:left bottom;background-repeat: no-repeat; padding-left:15px;background-color:#E0E0E0; width:180px");
    $("#ContractConst").formatCurrency();
    $("#Monitor_Type").val(MonitorTypeName);
    var v = vContractInfor[0].ASKING_DATE;
    var strDate = "";
    if (v != "") {
        var contractData = new Date(Date.parse(v.replace(/-/g, '/')))
        strDate += contractData.getFullYear() + "-";
        strDate += (contractData.getMonth() + 1) + "-";
        strDate += contractData.getDate();
    }
    $("#Contract_Date").val(strDate);
    $("#Monitor_Purpose").val(vContractInfor[0].TEST_PURPOSE);
    $("#Remarks").val(Remarks);

    setDisabled();
}


function setDisabled() {
    $("#Project_Name").ligerTextBox({ disabled: true });
    $("#Contract_Type").ligerTextBox({ disabled: true });
    $("#Contrat_Year").ligerTextBox({ disabled: true });
    $("#ContractCode").ligerTextBox({ disabled: true });
    $("#ContractConst").ligerTextBox({ disabled: true });
    $("#Monitor_Type").ligerTextBox({ disabled: true });
    $("#Contract_Date").ligerTextBox({ disabled: true });
    $("#Monitor_Purpose").ligerTextBox({ disabled: true });
    $("#Remarks").ligerTextBox({ disabled: true });
}
function GetIdValueName() {
    ContractTypeName = "";
    if (vContractInfor[0].CONTRACT_TYPE != "") {
        for (var i = 0; i < vContratTypeItem.length; i++) {
            if (vContractInfor[0].CONTRACT_TYPE == vContratTypeItem[i].DICT_CODE) {
                ContractTypeName = vContratTypeItem[i].DICT_TEXT;
            }
        }
    }
    MonitorTypeName = "";
    if (vContractInfor[0].TEST_TYPES != "") {
        var MonitorArr = null;
        MonitorArr = vContractInfor[0].TEST_TYPES.split(';');
        if (MonitorArr != null) {
            for (var i = 0; i < MonitorArr.length; i++) {
                for (var n = 0; n < vMonitorType.length; n++) {
                    if (MonitorArr[i] == vMonitorType[n].ID) {
                        MonitorTypeName += vMonitorType[n].MONITOR_TYPE_NAME + ";";
                    }
                }
            }
            MonitorTypeName = MonitorTypeName.substring(0, MonitorTypeName.length - 1);
        }
    }

    Remarks = "";
    if (vContratTypeItem != null) {
        if (vContractInfor[0].AGREE_OUTSOURCING == '1') {
            for (var i = 0; i < vRemarkItems.length; i++) {
                if (vRemarkItems[i].DICT_CODE == 'accept_subpackage') {
                    Remarks += vRemarkItems[i].DICT_TEXT + ";"
                }
            }
        }
        if (vContractInfor[0].AGREE_METHOD == '1') {
            for (var i = 0; i < vRemarkItems.length; i++) {
                if (vRemarkItems[i].DICT_CODE == 'accept_useMonitorMethod') {
                    Remarks += vRemarkItems[i].DICT_TEXT + ";"
                }
            }
        }
        if (vContractInfor[0].AGREE_NONSTANDARD == '1') {
            for (var i = 0; i < vRemarkItems.length; i++) {
                if (vRemarkItems[i].DICT_CODE == 'accept_usenonstandard') {
                    Remarks += vRemarkItems[i].DICT_TEXT + ";"
                }
            }
        }
        if (vContractInfor[0].AGREE_OTHER == '1') {
            for (var i = 0; i < vRemarkItems.length; i++) {
                if (vRemarkItems[i].DICT_CODE == 'accept_other') {
                    Remarks += vRemarkItems[i].DICT_TEXT + ";"
                }
            }
        }
        Remarks = Remarks.substring(0, Remarks.length - 1);
    }

    if (vConstItems != null) {
        ConstSum = vConstItems[0].INCOME;
    }
}

