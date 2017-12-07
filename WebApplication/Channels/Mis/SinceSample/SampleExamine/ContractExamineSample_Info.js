//Create By 胡方扬 2012-12-10
//委托书编制 环节 获取委托书 基本信息
var MonitorTypeName = "", ContractTypeName = "", Remarks = "", ConstSum = '0';
var vMonitorType = null, vContratTypeItem = null, vRemarkItems = null, vConstItems = null;
$(document).ready(function () {
    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="委托书信息"/><span>委托书信息</span>';
    $(strdivImg).appendTo(divImgInfo);

    var strbtnConstImg = ' <img id="btshowConst"  src="../../../../Images/Icons/magnifier.png" alt="查看费用明细" style="position:bottom" >';
    $(strbtnConstImg).appendTo(divConstbtn);
    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: 1000 });
    $("#btshowConst").bind("click", function () {
        $.ligerDialog.open({ title: '监测费用明细', top: 0, width: 700, height: 550, buttons:
        [{ text: '确定', onclick: f_SaveConstData },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../../Contract/ProgramInforDetail/ContractConstDetail.aspx?strContractId=' + task_id
        });
    })

})

function f_SaveConstData(item, dialog) {


    var fn = dialog.frame.GetChildInputValue || dialog.frame.window.GetChildInputValue;
    var strRequestdata = fn();
    SaveConstData(strRequestdata);
}

function SaveConstData(strRequestdata) {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../Contract/MethodHander.ashx?action=UpdateConstractFeeCount&strContratId=" + task_id + strRequestdata,
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                $.ligerDialog.success('数据保存成功！');
                GetConstractFeeCount();
                $("#ContractConst").val(vConstItems[0].INCOME);
            }
            else {
                parent.$.ligerDialog.warn('数据保存失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}
function GetInivalue() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../Contract/MethodHander.ashx?action=GetMonitorType",
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
        url: "../../Contract/MethodHander.ashx?action=GetDict&type=Contract_Type",
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
        url: "../../Contract/MethodHander.ashx?action=GetDict&type=Contract_Remarks",
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


    GetConstractFeeCount();
    SetLabelValue();

}

function SetLabelValue() {
    GetIdValueName();
    $("#Project_Name").val(vContractInfor[0].PROJECT_NAME);
    $("#Contract_Type").val(ContractTypeName);
    $("#Contrat_Year").val(vContractInfor[0].CONTRACT_YEAR);
    $("#ContractCode").val(vContractInfor[0].CONTRACT_CODE);
    $("#ContractConst").val(ConstSum);
    $("#Monitor_Type").val(MonitorTypeName);

    $("#hidFreq").val(vContractInfor[0].SAMPLE_FREQ);
    $("#hidTaskId").val(vContractInfor[0].ID);

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


function GetConstractFeeCount() {
    //获取费用总额
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../Contract/MethodHander.ashx?action=GetConstractFeeCount&strContratId=" + task_id + "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vConstItems = data.Rows;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}