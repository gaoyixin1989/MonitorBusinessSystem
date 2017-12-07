
/// 日历控件--监测计划新增
/// 创建时间：2012-12-22
/// 创建人：胡方扬
var objGrid = null, objItemGrid = null, vMonitorType = null, vPointItem = null, vMonth = null, vContractInfor = null, vDutyInitUser = null, GetMonitorItemData = null;
var task_id = "", struser = "", strmonitorID = "", monitorTypeName = "";
var tr = "", MonitorArr = [], Copy = [];
var saveflag = "";
var vDutyList = null;
var strPlanId = "", strIfPlan = "", strDate = "";
var strYear = "", strProjectName = "", strContratTypeId = "", strContactType = "";
var strCompanyId = "", strCompanyName = "", strIndustryId = "", strAreaId = "", strContactName = "", strTelPhone = "", strAddress = "";
var strCompanyIdFrim = "", strCompanyNameFrim = "", strIndustryIdFrim = "", strAreaIdFrim = "", strContactNameFrim = "", strTelPhoneFrim = "", strAddressFrim = "";

var strFreqId = "", strPointId = "", strCopyPointId = "", strInitValue = "", strMonitorItem_ID = "", strNew_id = "";
var isEdit = false;
var checkedCustomer = [], checkedMonitorArr = [], checkedPoint = [], moveCheckPoint = [], moveCheckCustomer = [];
var vContratTypeItem = null, vAutoYearItem = null, vMonitorType = null;
var strMonitorListNew = "", strDutyUserListNew = "", MonitorTypeId = "";
var vIframItem = [],vIfMonitorType=[];
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
    $("#layout1").ligerLayout({ topHeight: 0, leftWidth: "99%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });
    //$("#layout1").ligerLayout({ topHeight: 100, leftWidth: "60%",rightWidth: "39%",allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });
    strPlanId = $.getUrlVar('strPlanId');
    strIfPlan = $.getUrlVar('strIfPlan');
    strDate = $.getUrlVar('strDate');
    
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetMonitorType",
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
        url: "../Contract/MethodHander.ashx?action=GetDict&type=Contract_Type",
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
        url: "../Contract/MethodHander.ashx?action=GetContratYearHistory",
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
    $("#txtType").ligerComboBox({ data: vMonitorType, width: 200, valueFieldID: 'txtType_OP', valueField: 'ID', textField: 'MONITOR_TYPE_NAME', isMultiSelect: true, isShowCheckBox: true });

    $("#Contract_Type").ligerComboBox({ data: vContratTypeItem, width: 200, valueFieldID: 'Contract_Type_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false, initValue: '04', disabled: true });
    strContratTypeId = $("#Contract_Type_OP").val();
    $("#Contrat_Year").ligerComboBox({ data: vAutoYearItem, width: 200, valueFieldID: 'Contrat_Year_OP', valueField: 'ID', textField: 'YEAR', isMultiSelect: false, initValue: SetRYearboxInitValue(), onSelected: function (value, text) {
        strYear = text;
        strContactType = $("#Contract_Type").val();
        if (strCompanyNameFrim != "" && strContactType != "") {
            $("#txtProjectName").val(strCompanyNameFrim + strYear + "年度" + strContactType);
            strProjectName = $("#txtProjectName").val();
            $("#btnOk").attr("disabled", false);

            $("#btnOk").bind("click", function () {
                CreateSamplePlan();
                if (strPlanId != "") {
                    CreateDiv();

                }
            })
        }
    }
    });
    $("#txtDate").ligerDateEditor({ width: 200, initValue: strDate });
    //    $("#txtDate").ligerGetDateEditorManager().setDisabled();

    $("#btnOk").bind("click", function () {
        if ($("#Company_Name").val() != "" && $("#Company_NameFrim").val() != "") {
            if (!checkIsExistValue($("#Company_Name").val())) {
                $.ligerDialog.confirm('当前【<a style="color:Red; font-size:12px; font-weight:bold;">委托企业</a>】不存在，是否新增？\r\n', function (result) {
                    if (result) {
                        $.ajax({
                            cache: false,
                            async: false, //设置是否为异步加载,此处必须
                            type: "POST",
                            url: encodeURI("../Contract/MethodHander.ashx?action=InsertBaseCompany&strCompanyName=" + $("#Company_Name").val()),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                if (data != "") {
                                    strCompanyId = data.Rows[0].ID;
                                    strCompanyName = data.Rows[0].COMPANY_NAME;
                                    strAreaId = data.Rows[0].AREA;
                                    strContactName = data.Rows[0].CONTACT_NAME;
                                    strIndustryId = data.Rows[0].INDUSTRY;
                                    strTelPhone = data.Rows[0].PHONE;
                                    strAddress = data.Rows[0].CONTACT_ADDRESS;

                                    strContactType = $("#Contract_Type").val();
                                    strYear = $("#Contrat_Year").val();

                                    //if (!checkIsExistValue($("#Company_NameFrim").val())) {
                                        $.ajax({
                                            cache: false,
                                            async: false, //设置是否为异步加载,此处必须
                                            type: "POST",
                                            url: encodeURI("../Contract/MethodHander.ashx?action=InsertBaseCompany&strCompanyName=" + $("#Company_NameFrim").val()),
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            success: function (data) {
                                                if (data != "") {
                                                    strCompanyIdFrim = data.Rows[0].ID;
                                                    strCompanyNameFrim = data.Rows[0].COMPANY_NAME;
                                                    strAreaIdFrim = data.Rows[0].AREA;
                                                    strContactNameFrim = data.Rows[0].CONTACT_NAME;
                                                    strIndustryIdFrim = data.Rows[0].INDUSTRY;
                                                    strTelPhoneFrim = data.Rows[0].PHONE;
                                                    strAddressFrim = data.Rows[0].CONTACT_ADDRESS;

                                                    $("#txtProjectName").val(strCompanyNameFrim + strYear + "年度" + strContactType);
                                                    strProjectName = $("#txtProjectName").val();

                                                    CreateSamplePlan();
                                                    if (strPlanId != "") {
                                                        CreateDiv();
                                                    }
                                                }
                                            },
                                            error: function (msg) {
                                                $.ligerDialog.warn('AJAX数据请求失败！');
                                                return;
                                            }
                                        });
                                    //}
                                    //else {
                                       // $("#txtProjectName").val(strCompanyNameFrim + strYear + "年度" + strContactType);
                                       // strProjectName = $("#txtProjectName").val();
                                      //  CreateSamplePlan();
                                       // if (strPlanId != "") {
                                           // CreateDiv();
                                       // }
                                   // }
                                }
                            },
                            error: function (msg) {
                                $.ligerDialog.warn('AJAX数据请求失败！');
                                return;
                            }
                        });
                    }
                    else {
                        ClearValue();
                    }
                });
            }
            else {
                if (!checkIsExistValue($("#Company_NameFrim").val())) {
                    $.ajax({
                        cache: false,
                        async: false, //设置是否为异步加载,此处必须
                        type: "POST",
                        url: encodeURI("../Contract/MethodHander.ashx?action=InsertBaseCompany&strCompanyName=" + $("#Company_NameFrim").val()),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data != "") {
                                strCompanyIdFrim = data.Rows[0].ID;
                                strCompanyNameFrim = data.Rows[0].COMPANY_NAME;
                                strAreaIdFrim = data.Rows[0].AREA;
                                strContactNameFrim = data.Rows[0].CONTACT_NAME;
                                strIndustryIdFrim = data.Rows[0].INDUSTRY;
                                strTelPhoneFrim = data.Rows[0].PHONE;
                                strAddressFrim = data.Rows[0].CONTACT_ADDRESS;

                                $("#txtProjectName").val(strCompanyNameFrim + strYear + "年度" + strContactType);
                                strProjectName = $("#txtProjectName").val();

                                CreateSamplePlan();
                                if (strPlanId != "") {
                                    CreateDiv();
                                }
                            }
                        },
                        error: function (msg) {
                            $.ligerDialog.warn('AJAX数据请求失败！');
                            return;
                        }
                    });
                }
                else {
                    $("#txtProjectName").val(strCompanyNameFrim + strYear + "年度" + strContactType);
                    strProjectName = $("#txtProjectName").val();
                    CreateSamplePlan();
                    if (strPlanId != "") {
                        CreateDiv();
                    } 
                }
            }
        }

    });

    //生成自送样监测计划
    function CreateSamplePlan() {
        //项目名称和受检企业
        //        if ($("#Company_Name").val() != $("#Company_NameFrim").val()) {
        //            $("#Company_NameFrim").val($("#Company_Name").val());
        //            $("#txtProjectName").val($("#Company_Name").val() + strYear + "年度" + strContactType);
        //        }
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "SamplePlanHandler.ashx?action=CreateSamplePlan&Company_Name=" + encodeURI($("#Company_Name").val()),
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    strPlanId = data;
                    //  strNew_id = data.split(',')[1];
                }
                else {
                    $.ligerDialog.warn('监测计划生成失败！');
                    return;
                }

            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    // 获取监测类别
    function CreateDiv() {
        strMonitorListNew = $("#txtType_OP").val();
        var vContractMonitorItems = [];
        var strMonitorNameList = $("#txtType").val().split(';');
        var strMonitorIDList = $("#txtType_OP").val().split(';');

        if (strMonitorNameList != "" && strMonitorIDList != "") {
            var arrMonitor = new Array();
            if (strMonitorIDList.length == strMonitorNameList.length) {
                arrMonitor.length = strMonitorIDList.length;
                for (var i = 0; i < arrMonitor.length; i++) {
                    var arr = new Array();

                    arr.ID = strMonitorIDList[i];
                    arr.MONITOR_TYPE_NAME = strMonitorNameList[i];
                    arrMonitor[i] = arr;
                }
                vContractMonitorItems = arrMonitor;
            }

            if (vContractMonitorItems.length > 0) {
                //根据当前委托书的监测类别 动态生成监测类别
                var newDiv = '<div id="navtab1" position="center"  style = " width:780px;height:300px; overflow:hidden; border:1px solid #A3C0E8; ">';
                for (var i = 0; i < vContractMonitorItems.length; i++) {

                    if (i == 0) {
                        MonitorTypeId = vContractMonitorItems[i].ID;
                        newDiv += '<div id="div' + MonitorTypeId + '" title="' + vContractMonitorItems[i].MONITOR_TYPE_NAME + '" tabid="home" lselected="true" style="height:300px" >';
                        newDiv += '<iframe frameborder="0" name="showmessage' + MonitorTypeId + '" src= "ContractCheckTab_Since.aspx?strContratId=&strMonitorType=' + MonitorTypeId + '&strMonitorName=' + vContractMonitorItems[i].MONITOR_TYPE_NAME + '&strCompanyIdFrim=' + strCompanyIdFrim + '&strProject=' + strProjectName + '&strContractTypeId=' + strContratTypeId + '&isView=false&strContractCode=&strPlanId=' + strPlanId + '"></iframe>';
                        newDiv += '</div>';

                    }

                    else {
                        MonitorTypeId = vContractMonitorItems[i].ID;
                        newDiv += '<div id="div' + MonitorTypeId + '" title="' + vContractMonitorItems[i].MONITOR_TYPE_NAME + '" style="height:300px" >';
                        newDiv += '<iframe frameborder="0" name="showmessage' + MonitorTypeId + '" src= "ContractCheckTab_Since.aspx?strContratId=&strMonitorType=' + MonitorTypeId + '&strMonitorName=' + vContractMonitorItems[i].MONITOR_TYPE_NAME + '&strCompanyIdFrim=' + strCompanyIdFrim + '&strProject=' + strProjectName + '&strContractTypeId=' + strContratTypeId + '&isView=false&strContractCode=&strPlanId=' + strPlanId + '"></iframe>';
                        newDiv += '</div>';
                    }
                    vIframItem.push('showmessage' + MonitorTypeId);
                    vIfMonitorType.push(vContractMonitorItems[i].MONITOR_TYPE_NAME);
                }
                newDiv += '</div>';
                $(newDiv).appendTo(createDiv);
                $("#navtab1").ligerTab();


                $("#navtab1").ligerTab({
                    //在点击选项卡之前触发
                    onBeforeSelectTabItem: function (tabid) {
                    },
                    //在点击选项卡之后触发   点击其他的选项卡后，刷新该选项卡，防止CSS样式被串
                    onAfterSelectTabItem: function (tabid) {
                        //                if (tabFirst) {
                        navtab = $("#navtab1").ligerGetTabManager();
                        navtab.reload(navtab.getSelectedTabItemID());
                        //                    tabFirst = false;
                        //                }
                    }
                });
            }
            $("#btnOk").attr("disabled", true);
        }
        else {
            $.ligerDialog.warn('请选择监测类别！');
        }

    }



    function SetRYearboxInitValue() {
        var strValue = "";
        strValue = vAutoYearItem[0].ID;
        return strValue;
    }


    $("#Company_Name").unautocomplete();

    $("#Company_Name").autocomplete("../Contract/MethodHander.ashx?action=GetCompanyInfo",
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

        //        if (strYear != "" && strContactType != "") {
        // $("#txtProjectName").val(strCompanyNameFrim + strYear + "年度" + strContactType);
        strProjectName = $("#txtProjectName").val();
        //            $("#btnOk").attr("disabled", true);
        strAreaIdFrim = strAreaId;
        strContactNameFrim = strContactName;
        strIndustryIdFrim = strIndustryId;
        strTelPhoneFrim = strTelPhone;
        strAddressFrim = strAddress;
        strContactType = $("#Contract_Type").val();
        strYear = $("#Contrat_Year").val();
        //        $("#btnOk").bind("click", function () {
        //            CreateSamplePlan();
        //            if (strPlanId != "") {
        //                CreateDiv();
        //            }
        //        })
        //        }

        //        $.ligerDialog.confirm("是否复制委托单位为受检单位\r\n?", function (result) {
        //            if (result == true) {
        //                $("#Company_NameFrim").val(strCompanyName);

        //                strCompanyIdFrim = strCompanyId;
        //                strCompanyNameFrim = strCompanyName;
        //                strAreaIdFrim = strAreaId;
        //                strContactNameFrim = strContactName;
        //                strIndustryIdFrim = strIndustryId;
        //                strTelPhoneFrim = strTelPhone;
        //                strAddressFrim = strAddress;
        //                strContactType = $("#Contract_Type").val();
        //                strYear = $("#Contrat_Year").val();
        //                if (strYear != "" && strContactType != "") {
        //                    $("#txtProjectName").val(strCompanyNameFrim + strYear + "年度" + strContactType);
        //                    strProjectName = $("#txtProjectName").val();
        //                    $("#btnOk").attr("disabled", false);

        //                    $("#btnOk").bind("click", function () {
        //                        CreateSamplePlan();
        //                        if (strPlanId != "") {
        //                            CreateDiv();
        //                        }
        //                    })
        //                }
        //            }
        //        });

    }
    $("#Company_Name").bind("change", function () {
        if ($("#Company_Name").val() == "") {
            ClearValue();
        }
    })

    function ClearValue() {
        strCompanyId = "";
        strCompanyName = "";
        strContactName = "";
        strAreaId = "";
        strIndustryId = "";
        strTelPhone = "";
        strAddress = "";
    }
    $("#Company_NameFrim").unautocomplete();
    $("#Company_NameFrim").autocomplete("../Contract/MethodHander.ashx?action=GetCompanyInfo",
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
    $("#Company_NameFrim").result(findValueCallbackFrim); //加这个主要是联动显示id

    function findValueCallbackFrim(event, data, formatted) {
        strCompanyIdFrim = data["ID"]; //获取选择的ID
        strCompanyNameFrim = data["COMPANY_NAME"];
        strAreaIdFrim = data["AREA"];
        strContactNameFrim = data["CONTACT_NAME"];
        strIndustryIdFrim = data["INDUSTRY"];
        strTelPhoneFrim = data["PHONE"];
        strAddressFrim = data["CONTACT_ADDRESS"];

        //        if (strYear != "" && strContactType != "") {
        $("#txtProjectName").val(strCompanyNameFrim + strYear + "年度" + strContactType);
        strProjectName = $("#txtProjectName").val();
        //            $("#btnOk").attr("disabled", true);

        //            $("#btnOk").bind("click", function () {
        //                SaveData();
        //            })
    }
    //    }

    $("#Company_NameFrim").bind("change", function () {
        if ($("#Company_NameFrim").val() == "") {
            ClearFrimValue();
        }
    })

    function ClearFrimValue() {
        strCompanyIdFrim = "";
        strCompanyNameFrim = "";
        strContactNameFrim = "";
        strAreaIdFrim = "";
        strIndustryIdFrim = "";
        strTelPhoneFrim = "";
        strAddressFrim = "";
    }

    function SavePlan() {
        //var rowdate = objGrid.data.Rows;
        var rowdate = objGrid.getSelectedRows();
        strDate = $("#txtDate").val();
        if (rowdate.length > 0) {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: "MonitoringPlan.ashx?action=SavePlan&task_id=" + task_id + "&strCompanyId=" + strCompanyIdFrim + "&strDate=" + strDate,
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    if (data != "") {
                        strPlanId = data;
                        SavePlanPoint();
                    }
                },
                error: function (msg) {
                    $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                }
            });
        }
        else {
            return;
        }
    }

    function getPlanId() {
        return strPlanId;
    }

    /**
    *功能描述:返回父页面请求的URL参数
    */


    function TogetDate(date) {
        var strD = "";
        var thisYear = date.getYear();
        thisYear = (thisYear < 1900) ? (1900 + thisYear) : thisYear;
        var thisMonth = date.getMonth() + 1;
        //如果月份长度是一位则前面补0    
        if (thisMonth < 10) thisMonth = "0" + thisMonth;
        var thisDay = date.getDate();
        //如果天的长度是一位则前面补0    
        if (thisDay < 10) thisDay = "0" + thisDay;
        {

            strD = thisYear + "-" + thisMonth + "-" + thisDay;
        }
        return strD;
    }

    function DelPlan() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=DelContractPlan&strPlanId=" + strPlanId,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    SavePlan();
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    //数组删除重复值
    function delArr(Dataarr) {
        var a = {}, c = [], l = Dataarr.length;
        for (var i = 0; i < l; i++) {
            var b = Dataarr[i];
            var d = (typeof b) + b;
            if (a[d] === undefined) {
                c.push(b);
                a[d] = 1;
            }
        }
        return c;
    }
});

function GetBaseInfor() {
    var strParme = "";
    var Company_Names = $("#Company_Name").val();
    var Company_NameFrims = $("#Company_NameFrim").val();
    var Send_Man = $("#txtSendMan").val();
    strContratTypeId = $("#Contract_Type_OP").val();
    strDate = $("#txtDate").val();
        strParme += "&strSamplePlanId=" + strPlanId;
        strParme += "&strCompanyId=" + strCompanyId + "&strCompanyName=" + encodeURI(strCompanyName);
        strParme += "&strCompanyIdFrim=" + strCompanyIdFrim + "&strCompanyNameFrim=" + encodeURI(strCompanyNameFrim);
        strParme += "&strContractTypeId=" + strContratTypeId + "&strYear=" + strYear + "&strDate=" + strDate + "&strMonitorId=" + strMonitorListNew + "&strProjectName=" + encodeURI(strProjectName) + "&strMan=" + encodeURI(Send_Man);
//    strParme += "&strSamplePlanId=" + strPlanId;
//    if (strNew_id != "") {
//        if (strNew_id != strCompanyId) {
//            strParme += "&strCompanyId=" + strNew_id;
//        }
//        else {
//            strParme += "&strCompanyId=" + strCompanyId;
//        }
//    }
//    else {
//        strParme += "&strCompanyId=" + strCompanyId;
//    }

//    if (strCompanyName != Company_Names) {
//        strParme += "&strCompanyName=" + encodeURI(Company_Names);
//    }
//    else {
//        strParme += "&strCompanyName=" + encodeURI(strCompanyName);
//    }

//    if (strNew_id != "") {
//        if (strNew_id != strCompanyIdFrim) {
//            strParme += "&strCompanyIdFrim=" + strNew_id;
//        }
//        else {
//            strParme += "&strCompanyIdFrim=" + strCompanyIdFrim;
//        }
//    }
//    else {
//        strParme += "&strCompanyIdFrim=" + strCompanyIdFrim;
//    }

//    if (strCompanyNameFrim != Company_NameFrims) {
//        strParme += "&strCompanyName=" + encodeURI(Company_NameFrims);
//    }
//    else {
//        strParme += "&strCompanyName=" + encodeURI(strCompanyName);
//    }

//    strParme += "&strContractTypeId=" + strContratTypeId;
//    strParme += "&strYear=" + strYear;
//    strParme += "&strDate=" + strDate;
//    strParme += "&strMonitorId=" + strMonitorListNew;
//    var project_name = Company_Names + strYear + "年度" + strContactType;
//    if (project_name != strProjectName) {
//        strParme += "&strProjectName=" + encodeURI(project_name);
//    }
//    else {
//        strParme += "&strProjectName=" + encodeURI(strProjectName);
//    }
    return strParme;
}

function GetIfram() {
    var str = "";
    if (vIframItem.length > 0) {
        str = "1";
    }
    return str;
}

function GetSampleRow() {
    var strParme = "";
    if (vIframItem.length > 0) {
        for (var i = 0; i < vIframItem.length; i++) {
            var fn_1 = window.frames[vIframItem[i]].GetSampleRow;
            var fn_value = fn_1();
            if (fn_value == "") {
                strParme +=vIfMonitorType[i]+ ";";
            }
        }
    }
    if (strParme != "") {
        strParme = strParme.substring(0, strParme.length - 1);
    }
    return strParme;
}

function GetSampleItems() {
    var strParme = "";
    if (vIframItem.length > 0) {
        for (var i = 0; i < vIframItem.length; i++) {
            var fn = window.frames[vIframItem[i]].GetSampleItems;
            var fn_value = fn();
            if (fn_value != "") {
                strParme +=vIfMonitorType[i]+"类:" + fn_value + ";";
            }
        }
    }
    if (strParme != "") {
        strParme = strParme.substring(0, strParme.length - 1);
    }
    return strParme;
}

function checkIsExistValue(strName) {
    var IsExist = false;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: encodeURI("../Contract/MethodHander.ashx?action=checkCompany&strCompanyName=" + strName + ""),
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                IsExist = true;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('AJAX数据请求失败！');
        }
    });
    return IsExist;
}