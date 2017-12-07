//Create By 胡方扬
//用户控件 委托书录入


var vContratTypeItem = null, vAutoYearItem = null, vMonitorType = null;
var strContratTypeId = "", strAutoYear = "", strMonitorTypeId = "",strYear="",strMonitorTypeName="",strProjectName="";
var strContratId = "";
var strParme = "", strReqContratId = "", strMethod = "";
var strCreated = false, ProjectNameStr = "";
var cacheSelectSave = false;
$(document).ready(function () {
    var strdivImg = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="选择委托类型"/><span>选择委托类型</span>';
    $(strdivImg).appendTo(divImgSelect);
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetDict&type=Contract_Type",
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

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../MethodHander.ashx?action=GetContratYear",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vAutoYearItem = data.Rows;
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
        url: "../MethodHander.ashx?action=GetMonitorType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
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


    $("#Contract_Type").ligerComboBox({ data: vContratTypeItem, width: 200, valueFieldID: 'Contract_Type_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false, initValue: SetRTypeboxInitValue() });
    $("#Contrat_Year").ligerComboBox({ data: vAutoYearItem, width: 200, valueFieldID: 'Contrat_Year_OP', valueField: 'ID', textField: 'YEAR', isMultiSelect: false, initValue: SetRYearboxInitValue() });
    $("#Monitor_Type").ligerComboBox({ data: vMonitorType, width: 300, valueFieldID: 'Monitor_Type_OP', valueField: 'ID', textField: 'MONITOR_TYPE_NAME', isShowCheckBox: true, isMultiSelect: true });


    if (isAdd == false || !strType) {
        SetInputDisable();
    }

    if (strIdShow == "1") {
        $("#Contract_Type").ligerGetComboBoxManager().setDisabled();
    }
    //几个特殊的委托类型字典项编码  送样 04 验收 05 环境质量 07
    function SetRTypeboxInitValue() {
        var strValue = "";
        if (strIdShow == "1") {
            for (var i = 0; i < vContratTypeItem.length; i++) {
                if (vContratTypeItem[i].DICT_CODE == '05') {
                    strValue = vContratTypeItem[i].DICT_CODE;
                }
            }
        }
        return strValue;
    }
    function SetRYearboxInitValue() {
        var strValue = "";
        strValue = vAutoYearItem[0].ID;
        return strValue;
    }
    $("#btn_OkSelect").bind("click", function () {

        //先检查委托单位信息，如果不存在根据用户需求 进行是否增加
        if (isAdd) {
            var isTrue = $("#form1").validate();
            if (isTrue) {
                InsertCompanyInfor();
            } else {
                return;
            }
        }
        else {
            GetPageSelectValue();
            SaveData();
        }
    })
    //新增受检企业信息
    function InsertCompanyFrimInfor() {

        strCompanyNameFrim = $("#Company_NameFrim").val();
        if (strCompanyNameFrim == "" || strCompanyNameFrim == "请选择受检企业") {
            //            $.ligerDialog.warn('请选择受检企业！');
            //            $("#Company_NameFrim").ligerTextBox({ nullText: '请选择受检企业' });
            return;
        }
        checkIsExistFrimValue();
        if (strCompanyIdFrim == "") {
            $.ligerDialog.confirm('当前【<a style="color:Red; font-size:12px; font-weight:bold;">受检企业</a>】不存在，是否新增？\r\n', function (result) {
                if (result) {
                    GetPageValueFrim();
                    InsertCompanyFrimValue();
                    if (strCompanyIdFrim != "") {
                        GetPageValueFrim();
                        SetFrimData();
                        GetPageSelectValue();
                        SaveData();
                    }
                }
                else {
                    return;
                }
            })
        }

        else {
            GetPageValueFrim();
            SetFrimData();
            setBluFrimValue();

            //如果添加成功，则检查委托企业
            GetPageSelectValue();
            SaveData();
        }
    }

    //新增委托企业信息
    function InsertCompanyInfor() {
        strCompanyName = $("#Company_Name").val();
        if (strCompanyName == "" || strCompanyName == "请选择委托企业") {
            $.ligerDialog.warn('委托企业不能为空！');
            //            $("#Company_Name").ligerTextBox({ nullText: '请选择委托企业' });
            return;
        }
        checkIsExistValue();
        if (strCompanyId == "") {
            $.ligerDialog.confirm('当前【<a style="color:Red; font-size:12px; font-weight:bold;">委托企业</a>】不存在，是否新增？\r\n', function (result) {
                if (result) {
                    GetPageValue();
                    InsertCompanyValue();
                    if (strCompanyId != "") {
                        GetPageValue();
                        SetData();
                        //如果受检企业不是选择的 那么其strCompanyIdFrim为空，则提示是否新增受检企业信息，如果是 则新增加
                        InsertCompanyFrimInfor();
                    }
                }
                else {
                    return;
                }
            })
        }

        else {
            GetPageValue();
            setBluValue();
            SetData();
            InsertCompanyFrimInfor();
        }
    }


    function checkValue() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: encodeURI("../MethodHander.ashx?action=" + strMethod + strParme),
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    strContratId = data;
                    $.ligerDialog.success('保存成功！');
                    return;
                }
                else {
                    $.ligerDialog.warn('保存失败！');
                    return;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('AJAX数据请求失败！');
                return;
            }
        });
    }

    function createQueryString() {
        strYear = $("#Contrat_Year").val();
        strMonitorTypeName = $("#Contract_Type").val();
        strProjectName = strCompanyNameFrim + strYear + "年度" + strMonitorTypeName;
        GetPageValue()
        GetPageValueFrim();
        //获取控件的值
        strMethod = isAdd ? "InsertInfo" : "EditInfo";
        strReqContratId = isAdd ? "" : "&strContratId=" + strContratId + ""
        strParme = strReqContratId + "&strCompanyId=" + strCompanyId + "&strCompanyName=" + strCompanyName + "&strIndustryId=" + strIndustryId + "&strAreaId=" + strAreaId + "&strContactName=" + strContactName + "&strTelPhone=" + strTelPhone + "&strAddress=" + strAddress + "";
        strParme += "&strCompanyIdFrim=" + strCompanyIdFrim + "&strCompanyNameFrim=" + strCompanyNameFrim + "&strIndustryIdFrim=" + strIndustryIdFrim + "&strAreaIdFrim=" + strAreaIdFrim + "&strContactNameFrim=" + strContactNameFrim + "&strTelPhoneFrim=" + strTelPhoneFrim + "&strAddressFrim=" + strAddressFrim + "";
        strParme += "&strContratType=" + strContratTypeId + "&strContratYear=" + strAutoYear + "&strBookType=0&strProjectName=" + strProjectName + "&strMonitroType=" + strMonitorTypeId;
        return strParme;
    }

    //后期做统一保存
    function SaveData() {

        if (!vailValue()) {
            //创建参数字符串

            createQueryString();
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: encodeURI("../MethodHander.ashx?action=" + strMethod + strParme),
                contentType: "application/json; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    if (data != "") {
                        if (isAdd == true) {
                            strContratId = data;
                            isAdd = false;
                            cacheSelectSave = true;
                            SetInputDisable();
                            ShowNextPage();
                            return;
                        }
                        else {
                            if (data == "True") {
                                $.ligerDialog.success('保存成功！');
                                SetInputDisable();
                                return;
                            }
                            else {
                                $.ligerDialog.warn('保存失败！');
                                return;
                            }
                        }
                    }
                    else {
                        $.ligerDialog.warn('保存失败！');
                        return;
                    }
                },
                error: function (msg) {
                    $.ligerDialog.warn('AJAX数据请求失败！');
                    return;
                }
            });
        }

    }


    function GetPageSelectValue() {
        strContratTypeId = $("#Contract_Type_OP").val();
        strAutoYear = $("#Contrat_Year").val();
        strMonitorTypeId = $("#Monitor_Type_OP").val();
    }

    function vailValue() {
        if (strCompanyName == "") {
            $.ligerDialog.warn('请选择委托单位！');
            return true;
        }
        if (strContratTypeId == "") {
            $.ligerDialog.warn('请选择委托类别！');
            return true;
        }
        if (strAutoYear == "") {
            $.ligerDialog.warn('请选择委托年度！');
            return true;
        }
        if (strMonitorTypeId == "") {
            $.ligerDialog.warn('请选择监测类别！');
            return true;
        }
    }


    function SetInputDisable() {
        if (isAdd == false || !strType) {
            $("#Contract_Type").ligerGetComboBoxManager().setValue(strContratTypeId);
            var d = new Date();
            var str = d.getFullYear();
            if (str.toString() == strAutoYear) {
                $("#Contrat_Year").ligerGetComboBoxManager().setValue('0');
            }
            else {
                $("#Contrat_Year").ligerGetComboBoxManager().setValue('1');
            }

            $("#Monitor_Type").ligerGetComboBoxManager().setValue(strMonitorTypeId);
            $("#Contract_Type").ligerGetComboBoxManager().setDisabled();
            $("#Contrat_Year").ligerGetComboBoxManager().setDisabled();
            $("#Monitor_Type").ligerGetComboBoxManager().setDisabled();
            ShowNextPage();
        }

        if (isView == true) {
            $("#btn_OkSelect").remove();
        }
    }


    function ShowNextPage() {
        //填充核对委托书信息 委托项目
        strYear = $("#Contrat_Year").val();
        strMonitorTypeName = $("#Contract_Type").val();
        strProjectName = strCompanyNameFrim + strYear + "年度" + strMonitorTypeName;
        $("#Project_Name").val(strProjectName);
        if (!strCreated) {
            CreateDiv();
            strCreated = true;
        }
        //移除style display:none 属性 显示核对委托书信息 DIV层
        $("#divContratCheck").attr("style", "");
        $("#divContractPlan").attr("style", "");
        $("#divContratSubmit").attr("style", "clear:both;margin-bottom:10px");
    }
})

