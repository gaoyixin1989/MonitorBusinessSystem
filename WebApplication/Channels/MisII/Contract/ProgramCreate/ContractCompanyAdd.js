//Create By 胡方扬
//用户控件 委托书录入


var vIndustryItem = null, vAreaItem = null, vAutoItem = null;
var strCompanyId = "", strCompanyName = "", strIndustryId = "", strAreaId = "", strContactName = "", strTelPhone = "", strAddress = "";
var cacheSave = false;
var isExistCompany = false, isSelect = false;

var ModifyCompanyInfor = null;
//获取行业类别

$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});


$(document).ready(function () {
    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="添加委托单位"/><span>添加委托单位</span>';
    $(strdivImg).appendTo(divImgCom);

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../../Mis/Contract/MethodHander.ashx?action=GetIndustryInfo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
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
        url: "../../../Mis/Contract/MethodHander.ashx?action=GetDict&type=administrative_area",
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
    //    $("#Industry_Name").ligerComboBox({ data: vIndustryItem, width: 200, valueFieldID: 'INDUSTRY_OP', valueField: 'ID', textField: 'INDUSTRY_NAME', isMultiSelect: false });
    $("#Area_Name").ligerComboBox({ data: vAreaItem, width: 200, valueFieldID: 'AREA_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false });

    if (isAdd == false||!strType) {
        SetComInputValue();
    }

    //自动检索加载
    if (isAdd == true & cacheSave == false) {
        $("#Company_Name").unautocomplete();

        $("#Company_Name").autocomplete("../../../Mis/Contract/MethodHander.ashx?action=GetCompanyInfo",
    {
        max: 12,     // 列表里的条目数 
        minChars: 0,     // 自动完成激活之前填入的最小字符 
        matchContains: true,     // 包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示 
        autoFill: false,     // 自动填充 
        width: 250,
        max: 20,
        dataType: "json",
        scrollHeight: 150,
        parse: function (data) {

            return $.map(eval(data), function (row) {
                return {
                    data: row,

                    value: row.ID + " <" + row.COMPANY_NAME + ">",
                    result: row.COMPANY_NAME

                }
            });
        },
        formatItem: function (item) {
            //return "<font color=green>" + item.ID + "</font>&nbsp;(" + item.COMPANY_NAME + ")";
            return item.COMPANY_NAME;
        }
    }
    );
        $("#Company_Name").result(findValueCallback); //加这个主要是联动显示id
        function findValueCallback(event, data, formatted) {
            strCompanyId = data["ID"]; //获取选择的ID
            strCompanyName = data["COMPANY_NAME"];
            strAreaId = data["AREA"];
            strContactName = data["CONTACT_NAME"];
            strIndustryId = data["INDUSTRY"];
            strTelPhone = data["PHONE"];
            strAddress = data["CONTACT_ADDRESS"];
            SetComInputValue();


        }




        $("#wei").unautocomplete();

        $("#wei").autocomplete("../../../Mis/Contract/MethodHander.ashx?action=GetCompanyInfo",
    {
        max: 12,     // 列表里的条目数 
        minChars: 0,     // 自动完成激活之前填入的最小字符 
        matchContains: true,     // 包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示 
        autoFill: false,     // 自动填充 
        width: 250,
        max: 20,
        dataType: "json",
        scrollHeight: 150,
        parse: function (data) {

            return $.map(eval(data), function (row) {
                return {
                    data: row,

                    value: row.ID + " <" + row.COMPANY_NAME + ">",
                    result: row.COMPANY_NAME

                }
            });
        },
        formatItem: function (item) {
            //return "<font color=green>" + item.ID + "</font>&nbsp;(" + item.COMPANY_NAME + ")";
            return item.COMPANY_NAME;
        }
    }
    );


    }
})
    $("#Company_Name").bind("change", function () {
        if ($("#Company_Name").val() == "") {
            ClearValue();
            SetComInputValue();
        }
    })

    function setBluValue() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../../../Mis/Contract/MethodHander.ashx?action=GetCompanyInfo",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    for (var i = 0; i < data.length; i++) {
                        if (strCompanyId == data[i].ID) //获取选择的ID
                        {
                            strCompanyName = data[i].COMPANY_NAME;
                            strAreaId = data[i].AREA;
                            strContactName = data[i].CONTACT_NAME;
                            strIndustryId = data[i].INDUSTRY;
                            strTelPhone = data[i].PHONE;
                            strAddress = data[i].CONTACT_ADDRESS;
                            SetComInputValue();
                        }
                    }
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
    //设置委托单位为不可编辑
    function SetData() {
        $("#Company_Name").unautocomplete();
        $("#Company_Name").ligerTextBox({ disabled: true })
        GetPageValue();
        cacheSave = true;
    }

    function ClearValue() {
        strCompanyId = "";
        strCompanyName = "";
        strContactName = "";
        strAreaId = "";
        strIndustryId = "";
        strTelPhone = "";
        strAddress = "";
    }


    function SetComInputValue() {
        $("#Company_Name").val(strCompanyName);
        if (isAdd == false) {
            $("#Company_Name").unautocomplete();
            $("#Company_Name").ligerTextBox({ disabled: true });
            $("#btn_Ok").remove();
        }

        if (isView == true) {
            $("#Company_Name").unautocomplete();
            $("#Company_Name").ligerTextBox({ disabled: true });
            $("#Contacts_Name").ligerTextBox({ disabled: true });
            $("#Tel_Phone").ligerTextBox({ disabled: true });
            $("#Address").ligerTextBox({ disabled: true });
//            $("#Industry_Name").ligerGetComboBoxManager().setDisabled();
            $("#Area_Name").ligerGetComboBoxManager().setDisabled();
            $("#btn_Ok").remove();
        }
        $("#Contacts_Name").val(strContactName);
        $("#Tel_Phone").val(strTelPhone);
        $("#Address").val(strAddress);
//        $("#Industry_Name").ligerGetComboBoxManager().setValue(strIndustryId);
        $("#Area_Name").ligerGetComboBoxManager().setValue(strAreaId);
    }

    function checkIsExistValue() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: encodeURI("../../../Mis/Contract/MethodHander.ashx?action=checkCompany&strCompanyName=" + strCompanyName + ""),
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    isExistCompany = true;
                    strCompanyId = data;
                    return strCompanyId;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('AJAX数据请求失败！');
                return;
            }
        });
    }


    function InsertCompanyValue() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: encodeURI("../../../Mis/Contract/MethodHander.ashx?action=InsertCompany&strCompanyName=" + strCompanyName + "&strIndustryId=" + strIndustryId + "&strAreaId=" + strAreaId + "&strContactName=" + strContactName + "&strTelPhone=" + strTelPhone + "&strAddress=" + strAddress + ""),
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    strCompanyId = data;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('AJAX数据请求失败！');
                return;
            }
        });
    }


function GetPageValue() {
    
    $("#Monitor_Purpose").val($("#Contract_Type").val());
    strCompanyId = strCompanyId.toString();
    strCompanyName = $("#Company_Name").val();
    strContactName = $("#Contacts_Name").val();
    strTelPhone = $("#Tel_Phone").val();
    strAddress = $("#Address").val();
//    strIndustryId = $("#INDUSTRY_OP").val();
    strAreaId = $("#AREA_OP").val();
}
