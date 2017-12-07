var vIndustryItem = null, vAreaItem = null, vCompanyInfors = null;
var strCompanyName = "", strCompanyId = "", strCompanyIdFrim = "";
$(document).ready(function () {

    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="企业信息"/><span>企业信息</span>';
    $(strdivImg).appendTo(divImgCom);
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../Contract/MethodHander.ashx?action=GetIndustryInfo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vIndustryItem = data.Rows;
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
        url: "../../Contract/MethodHander.ashx?action=GetDict&type=administrative_area",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vAreaItem = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    //vContractInfor为父页面参数
    function GetCompanyInfor(comPanyId) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../../Contract/ProgrammingHandler.ashx?action=GetCompanyInfor&strCompanyId=" + comPanyId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vCompanyInfors = data.Rows;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    $("#Industry_Name").ligerComboBox({ data: vIndustryItem, width: 200, valueFieldID: 'INDUSTRY_OP', valueField: 'ID', textField: 'INDUSTRY_NAME', isMultiSelect: false });
    $("#Area_Name").ligerComboBox({ data: vAreaItem, width: 200, valueFieldID: 'AREA_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false });

    $("#Industry_NameFrim").ligerComboBox({ data: vIndustryItem, width: 200, valueFieldID: 'INDUSTRYFrim_OP', valueField: 'ID', textField: 'INDUSTRY_NAME', isMultiSelect: false });
    $("#Area_NameFrim").ligerComboBox({ data: vAreaItem, width: 200, valueFieldID: 'AREAFrim_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false });

    //加载数据
    SetInputValue();
    //设置只读属性
    setDisabled();
    function SetInputValue() {
        if (vContractInfor != null) {
            //填充委托企业信息
            strCompanyId = vContractInfor[0].CLIENT_COMPANY_ID;
            GetCompanyInfor(strCompanyId);
            $("#Company_Name").val(vCompanyInfors[0].COMPANY_NAME);
            //            $("#Company_Name").ligerTextBox({ disabled: true });
            $("#Contacts_Name").val(vCompanyInfors[0].CONTACT_NAME);
            $("#Tel_Phone").val(vCompanyInfors[0].PHONE);
            $("#Address").val(vCompanyInfors[0].CONTACT_ADDRESS);
//            $("#Industry_Name").ligerGetComboBoxManager().setValue(vCompanyInfors[0].INDUSTRY);
            $("#Area_Name").ligerGetComboBoxManager().setValue(vCompanyInfors[0].AREA);
            //填充受检企业信息
            strCompanyIdFrim = vContractInfor[0].TESTED_COMPANY_ID;
            GetCompanyInfor(strCompanyIdFrim);
            $("#Company_NameFrim").val(vCompanyInfors[0].COMPANY_NAME);
            //            $("#Company_NameFrim").ligerTextBox({ disabled: true });
            $("#Contacts_NameFrim").val(vCompanyInfors[0].CONTACT_NAME);
            $("#Tel_PhoneFrim").val(vCompanyInfors[0].PHONE);
            $("#AddressFrim").val(vCompanyInfors[0].MONITOR_ADDRESS);
//            $("#Industry_NameFrim").ligerGetComboBoxManager().setValue(vCompanyInfors[0].INDUSTRY);
            $("#Area_NameFrim").ligerGetComboBoxManager().setValue(vCompanyInfors[0].AREA);
        }
    }

    function setDisabled() {
        $("#Company_Name").ligerTextBox({ disabled: true });
        $("#Contacts_Name").ligerTextBox({ disabled: true });
        $("#Tel_Phone").ligerTextBox({ disabled: true });
        $("#Address").ligerTextBox({ disabled: true });
//        $("#Industry_Name").ligerTextBox({ disabled: true });
        $("#Area_Name").ligerTextBox({ disabled: true });

        $("#Company_NameFrim").ligerTextBox({ disabled: true });
        $("#Contacts_NameFrim").ligerTextBox({ disabled: true });
        $("#Tel_PhoneFrim").ligerTextBox({ disabled: true });
        $("#AddressFrim").ligerTextBox({ disabled: true });
//        $("#Industry_NameFrim").ligerTextBox({ disabled: true });
        $("#Area_NameFrim").ligerTextBox({ disabled: true });
    }

})