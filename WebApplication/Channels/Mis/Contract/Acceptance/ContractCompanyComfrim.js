//Create By 胡方扬
//用户控件 委托书录入


var vIndustryItem = null, vAreaItem = null, vAutoItem = null;
var strCompanyIdFrim = "", strCompanyNameFrim = "", strIndustryIdFrim = "", strAreaIdFrim = "", strContactNameFrim = "", strTelPhoneFrim = "", strAddressFrim = "";
var cacheSaveFrim = false, isExistCompanyFrm = false;


$(document).ready(function () {
    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="添加受检单位"/><span>添加受检单位</span>  ';
    $(strdivImg).appendTo(divImgComFrim);
    //获取行业类别
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetIndustryInfo",
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
        url: "../MethodHander.ashx?action=GetDict&type=administrative_area",
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

    //自动检索加载
    if (isAdd == true & cacheSaveFrim == false) {
        $("#Company_NameFrim").unautocomplete();

        $("#Company_NameFrim").autocomplete("../MethodHander.ashx?action=GetCompanyInfo",
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
        $("#Company_NameFrim").result(findValueCallback); //加这个主要是联动显示id


        function findValueCallback(event, data, formatted) {
            strCompanyIdFrim = data["ID"]; //获取选择的ID
            strCompanyNameFrim = data["COMPANY_NAME"];
            strAreaIdFrim = data["AREA"];
            strContactNameFrim = data["CONTACT_NAME"];
            strIndustryIdFrim = data["INDUSTRY"];
            strTelPhoneFrim = data["PHONE"];
            strAddressFrim = data["CONTACT_ADDRESS"];
            SetInputValue();
        }
    }
    $("#Area_NameFrim").ligerComboBox({ data: vAreaItem, width: 200, valueFieldID: 'AREA_Frim_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false });
    if (isAdd == false) {
        SetInputValue();
    }

    $("#Company_NameFrim").bind("change", function () {
        if ($("#Company_NameFrim").val() == "") {
            ClearValue();
            SetInputValue();
        }
    })

    $("#btn_Copy").bind("click", function () {
        if (strCompanyName == "") {
            $.ligerDialog.warn('【<a style="color:Red; font-size:12px; font-weight:bold;">委托单位</a>】为空,请选择后再复制！');
            return;
        }
        else {
            $("#Company_NameFrim").val(strCompanyName);
            $("#Contacts_NameFrim").val(strContactName);
            $("#Tel_PhoneFrim").val(strTelPhone);
            $("#AddressFrim").val(strAddress);
            $("#Area_NameFrim").ligerGetComboBoxManager().setValue(strAreaId);
        }
    })


    $("#btn_OkFrim").bind("click", function () {
        strCompanyNameFrim = $("#Company_NameFrim").val();
        if (strCompanyNameFrim == "") {
            $.ligerDialog.warn('受检企业不能为空！');
            return;
        }
        checkIsExistValue();
        if (strCompanyIdFrim == "") {
            $.ligerDialog.confirm('当前委托企业不存在，是否新增？\r\n', function (result) {
                if (result) {
                    GetPageValueFrim();
                    InsertCompanyValue();
                    if (strCompanyIdFrim != "") {
                        GetPageValueFrim();
                        SetData();
                    }
                }
                else {
                    return;
                }
            })
        }

        else {
            GetPageValueFrim();
            SetData();
            setBluValue();
        }

    })


    function setBluValue() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../MethodHander.ashx?action=GetCompanyInfo",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    for (var i = 0; i < data.length; i++) {
                        if (strCompanyId == data[i].ID) //获取选择的ID
                        {
                            strCompanyNameFrim = data[i].COMPANY_NAME;
                            strAreaIdFrim = data[i].AREA;
                            strContactNameFrim = data[i].CONTACT_NAME;
                            strIndustryIdFrim = data[i].INDUSTRY;
                            strTelPhoneFrim = data[i].PHONE;
                            strAddressFrim = data[i].CONTACT_ADDRESS;
                            SetInputValue();
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

    function ClearValue() {
        strCompanyIdFrim = "";
        strCompanyNameFrim = "";
        strContactNameFrim = "";
        strAreaIdFrim = "";
        strIndustryIdFrim = "";
        strTelPhoneFrim = "";
        strAddressFrim = "";
    }

    function SetInputValue() {
        $("#Company_NameFrim").val(strCompanyName);
        if (isAdd == false) {
            $("#Company_NameFrim").unautocomplete();
            $("#Company_NameFrim").ligerTextBox({ disabled: true });
            $("#btn_OkFrim").remove();
            $("#btn_Copy").remove();
        }
        if (isView == true) {
            $("#Company_NameFrim").ligerTextBox({ disabled: true });
            $("#Contacts_NameFrim").ligerTextBox({ disabled: true });
            $("#Tel_PhoneFrim").ligerTextBox({ disabled: true });
            $("#AddressFrim").ligerTextBox({ disabled: true });
            $("#Area_NameFrim").ligerGetComboBoxManager().setDisabled();
            $("#btn_OkFrim").remove();
            $("#btn_Copy").remove();
        }
        $("#Contacts_NameFrim").val(strContactNameFrim);
        $("#Tel_PhoneFrim").val(strTelPhoneFrim);
        $("#AddressFrim").val(strAddressFrim);
        $("#Area_NameFrim").ligerGetComboBoxManager().setValue(strAreaIdFrim);
    }

    function checkIsExistValue() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: encodeURI("../MethodHander.ashx?action=checkCompany&strCompanyName=" + strCompanyNameFrim + ""),
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    isExistCompanyFrm = true;
                    strCompanyIdFrim = data;
                    return strCompanyIdFrim;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('AJAX数据请求失败！');
                return;
            }
        });
    }
    //设置委托单位为不可编辑
    function SetData() {
        $("#Company_NameFrim").unautocomplete();
        $("#Company_NameFrim").ligerTextBox({ disabled: true });
        cacheSaveFrim = true;

    }

})

function GetPageValueFrim() {
    strCompanyIdFrim = strCompanyId.toString();
    strCompanyNameFrim = $("#Company_NameFrim").val();
    strContactNameFrim = $("#Contacts_NameFrim").val();
    strTelPhoneFrim = $("#Tel_PhoneFrim").val();
    strAddressFrim = $("#AddressFrim").val();
    strIndustryIdFrim = $("#INDUSTRY_Frim_OP").val();
    strAreaIdFrim = $("#AREA_Frim_OP").val();
}
