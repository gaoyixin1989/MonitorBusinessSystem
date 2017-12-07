
/// 环境质量监测计划新增
/// 创建时间：2012-12-22
/// 创建人：胡方扬
var objGrid = null, vMonitorType = null, vPointItem = null, vMonth = null, vContractInfor = null, vDutyInitUser = null;
var task_id = "", struser = "", strmonitorID = "", monitorTypeName = "", CONTRACT_TYPE = "";
var tr = "", MonitorArr = [];
var saveflag = "";
var vDutyList = null;
var strPlanId = "", strIfPlan = "", strDate = "";
var strYear = "", strProjectName = "", strContratTypeId = "", strContactType = "";
var strCompanyId = "", strCompanyName = "", strIndustryId = "", strAreaId = "", strContactName = "", strTelPhone = "", strAddress = "";
var strCompanyIdFrim = "", strCompanyNameFrim = "", strIndustryIdFrim = "", strAreaIdFrim = "", strContactNameFrim = "", strTelPhoneFrim = "", strAddressFrim = "";
var GetSampleFreqItems = null;
var strFreqId = "", strPointId = "", strInitValue = "", ShowStatus = "0", strAskingDate = "";
var isEdit = false;
var checkedCustomer = [], checkedMonitorArr = [], checkedPoint = [], moveCheckPoint = [], moveCheckCustomer = [];
var vContratTypeItem = null, vAutoYearItem = null, vMonitorType = null, vEnvTypesItems = null, vContractList = null;
var strEvnTypeId = "", strEvnTypeName = "", strEvnPointId = "", strMonth = "", strKeyColumns = "", strTableName = "", FatherKeyColumn = "", FatherKeyValue = "", SubKeyColumn = "";
var objColumnItem = [], vTypeItem = null, vTypeNameItem = null, vTypeArry = null;
var isus = true;
var CCFLOW_WORKID = "";
var vIframItem = [], vIframMonitorType = [], vIframMonitorTypeID = [];
var EnvTypes = "";
var strUrl = "EnvPlanAdd.aspx";

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

var monthJSON = [
    { "VALUE": "1", "MONTH": "1" },
    { "VALUE": "2", "MONTH": "2" },
    { "VALUE": "3", "MONTH": "3" },
    { "VALUE": "4", "MONTH": "4" },
    { "VALUE": "5", "MONTH": "5" },
    { "VALUE": "6", "MONTH": "6" },
    { "VALUE": "7", "MONTH": "7" },
    { "VALUE": "8", "MONTH": "8" },
    { "VALUE": "9", "MONTH": "9" },
    { "VALUE": "10", "MONTH": "10" },
    { "VALUE": "11", "MONTH": "11" },
    { "VALUE": "12", "MONTH": "12" }
];
var RegionCode = [
    { "VALUE": "1", "MONTH": "承德市" },
    { "VALUE": "2", "MONTH": "市辖区" },
    { "VALUE": "3", "MONTH": "双桥区" },
    { "VALUE": "4", "MONTH": "双滦区" },
    { "VALUE": "5", "MONTH": "鹰手营子矿区" },
    { "VALUE": "6", "MONTH": "承德县" },
    { "VALUE": "7", "MONTH": "兴隆县" },
    { "VALUE": "8", "MONTH": "平泉县" },
    { "VALUE": "9", "MONTH": "滦平县" },
    { "VALUE": "10", "MONTH": "隆化县" },
    { "VALUE": "11", "MONTH": "丰宁满族自治县" },
    { "VALUE": "12", "MONTH": "宽城满族自治县" },
    { "VALUE": "13", "MONTH": "围场满族蒙古族自治县" }
];
var Functional = [{ "VALUE": "1", "MONTH": "0类功能区" },
    { "VALUE": "2", "MONTH": "1类功能区" },
    { "VALUE": "3", "MONTH": "2类功能区" },
    { "VALUE": "4", "MONTH": "3类功能区" },
    { "VALUE": "5", "MONTH": "4a类功能区" },
    { "VALUE": "6", "MONTH": "4b类功能区" }];

    $(document).ready(function () {

        $("#layout1").ligerLayout({ topHeight: 210, leftWidth: "99%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" }); //huangjinjun update 20150527

        CCFLOW_WORKID = $.getUrlVar('WorkID');   //获取CCLow主流程ID

        $("#chkBC").ligerCheckBox();

        $("#createDiv").html("<a style='color:Red;font-weight:bold'>请选择环境质量监测类别并生成计划</a>");

        //黄进军 add 20150918 获取任务表数据（抄送功能）
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../../Mis/Contract/MethodHander.ashx?action=GetEnvInfor&strCCFLOW_WORKID=" + CCFLOW_WORKID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total > 0) {
                    var monitor_type_name = "";
                    strEvnTypeId = data.Rows[0]["PLAN_TYPE"].toString();
                    if (strEvnTypeId.indexOf(";")) {
                        var ch = new Array;
                        ch = strEvnTypeId.split(";");
                        for (i = 0; i < ch.length; i++) {
                            if (0 == i) {
                                strEvnTypeName = GetMonitorType(ch[i]);
                            }
                            else {
                                strEvnTypeName = strEvnTypeName + ";" + GetMonitorType(ch[i]);
                            }
                        }
                    } else {
                        strEvnTypeName = GetMonitorType(strEvnTypeId);
                    }
                    $("#EnvTypes").val(strEvnTypeName);
                    $("#txtProjectName").val(data.Rows[0]["PROJECT_NAME"].toString());
                    $("#txtTASK_CODE").val(data.Rows[0]["TICKET_NUM"].toString());
                    $("#txtRemarks").val(data.Rows[0]["REMARK2"].toString());
                    strPlanId = data.Rows[0]["PLAN_ID"].toString();
                    $("#createDiv").html("");
                    strYear = data.Rows[0]["PLAN_YEAR"].toString();
                    var strm = data.Rows[0]["PLAN_MONTH"].toString();
                    strMonth = strm.substring(strm.indexOf('0') + 1);
                    CreatTable();
                }
                else {
                    //$.ligerDialog.warn('数据加载错误！');
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
            url: "../../Mis/Contract/MethodHander.ashx?action=GetWebConfigValue&strKey=ShowEnvTypeName",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    ShowStatus = data;
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
            async: false,
            type: "POST",
            url: "../../Mis/Contract/MethodHander.ashx?action=GetDict&type=POINT_SAMPLEFREQ",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    GetSampleFreqItems = data;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求数据失败！');
            }
        });

        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../../Mis/Contract/MethodHander.ashx?action=GetDict&type=Contract_Type",
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
            url: "../../Mis/Contract/MethodHander.ashx?action=GetDict&type=EnvTypes",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    vEnvTypesItems = data;
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
            url: "../../Mis/Contract/MethodHander.ashx?action=GetContratYearHistory",
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

        $("#EnvTypes").ligerComboBox({ data: vEnvTypesItems, width: 200, valueFieldID: 'EnvTypes_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isShowCheckBox: true, isMultiSelect: true, selectBoxHeight: 200, onSelected: function (value, text) {
            strEvnTypeId = value;
            strEvnTypeName = text;
            //alert(strEvnTypeName);
//            if (strEvnTypeName == "区域噪声环境" || strEvnTypeName == "道路交通噪声") {
//                document.getElementById('iDBody1').style.display = "";
//                //document.getElementById('iDBody2').style.display = "none";
//                $("#RegionCode").ligerComboBox({ data: RegionCode, width: 200, valueFieldID: 'RegionCode', valueField: "VALUE", textField: "MONTH" });
//            } else if (strEvnTypeName == "功能区噪声") {
//                document.getElementById('iDBody1').style.display = "";
//                document.getElementById('iDBody2').style.display = "";
//                $("#RegionCode").ligerComboBox({ data: RegionCode, width: 200, valueFieldID: 'RegionCode', valueField: "VALUE", textField: "MONTH" });
//                $("#Functional").ligerComboBox({ data: Functional, width: 200, valueFieldID: 'Functional', valueField: "VALUE", textField: "MONTH" });
//            }
        }
        });

        $("#EnvMonth").ligerComboBox({ data: monthJSON, width: 200, valueFieldID: 'EnvMonth_OP', valueField: "VALUE", textField: "MONTH", initValue: new Date().getMonth() + 1 });
        $("#EnvSY").ligerComboBox({ data: [{ "VALUE": "0", "TEXT": "否" }, { "VALUE": "1", "TEXT": "是"}], width: 200, valueFieldID: 'EnvSY_OP', valueField: "VALUE", textField: "TEXT", initValue: "0" });


        $("#txtProjectName").attr("style", "width:500px");

        strDate = TogetDate(new Date());
        strAskingDate = TogetDateAfter7(new Date());
        $("#txtDate").ligerDateEditor({ initValue: strDate, width: 200, onChangeDate: function (value) {
            strDate = $("#txtDate").val();
        }
        });

        $("#txtAskingDate").ligerDateEditor({ initValue: strAskingDate, width: 200, onChangeDate: function (value) {
            strAskingDate = $("#txtAskingDate").val();
        }
        });


        //任务单号查询
        $("#BtnSearch").click(function () {
            $.ligerDialog.open({ title: '任务单号查询', name: 'winaddtor', width: 320, height: 280, top: 90, url: '../../Mis/MonitoringPlan/PendingDoTask_Search.aspx?CONTRACT_TYPE=' + CONTRACT_TYPE, buttons: [
                { text: '关闭', onclick: f_Cancel }
            ]
            });
        });

        //生成
        $("#btnGet").bind("click", function () {
            var EnvTypes = $("#EnvTypes").val();
//            var RegionCode = $("#RegionCode").val();
//            var Functional = $("#Functional").val();
            //alert(EnvTypes);
//            if (EnvTypes == "区域噪声环境" || EnvTypes == "道路交通噪声") {
//                if (RegionCode == "") {
//                    $.ligerDialog.warn('行政区不能为空，请选择');
//                    return;
//                }
//            }
//            if (EnvTypes == "功能区噪声") {
//                if (RegionCode == "") {
//                    $.ligerDialog.warn('行政区不能为空，请选择');
//                    return;
//                }
//                if (Functional == "") {
//                    $.ligerDialog.warn('功能区不能为空，请选择');
//                    return;
//                }
//            }

            if (strEvnTypeId != "") {
                $.ajax({
                    cache: false,
                    async: false, //设置是否为异步加载,此处必须
                    type: "POST",
                    url: "../../Mis/MonitoringPlan/MonitoringPlan.ashx?action=InsertEnvPlanByCreate&strDate=" + strDate + "&strAskingDate=" + strAskingDate + "&strEnvTypeId=" + strEvnTypeId + "&strCCFLOW_WORKID=" + CCFLOW_WORKID + "&strSY=" + $("#EnvSY_OP").val() + "&strEnvTypes=" + encodeURI(EnvTypes) ,
                    contentType: "application/text; charset=utf-8",
                    dataType: "text",
                    success: function (data) {
                        if (data != "") {
                            var pid = data.substring(0, data.indexOf(','));
                            var txtTASK_CODE = data.substring(data.indexOf(',') + 1);
                            strPlanId = pid;
                            $("#createDiv").html("");
                            strYear = new Date().getFullYear();
                            strMonth = $("#EnvMonth_OP").val();
                            $("#txtTASK_CODE").val(txtTASK_CODE); //任务单号
                            strProjectName = strYear + "年度" + strMonth + "月" + "环境质量监测" + "(" + strEvnTypeName + ")"; //项目名称
                            $("#txtProjectName").val(strProjectName);
                            CreatTable();
                            $("#btnGet").attr("disabled", true);
                            $("#EnvMonth").attr("disabled", true);

                        }
                    },
                    error: function (msg) {
                        $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                    }
                });
            }
            else {
                $.ligerDialog.warn("请选择环境质量类别！");
            }

        })
        function f_Cancel(item, dialog) {
            dialog.close();
        }
        function CreatTable() {
            if (strEvnTypeId != "") {
                vTypeItem = null;
                vTypeNameItem = null;
                vTypeNameItem = strEvnTypeName.split(";");
                vTypeItem = strEvnTypeId.split(";");
                var tempArr = new Array();
                tempArr.length = vTypeItem.length;
                for (var i = 0; i < tempArr.length; i++) {
                    var arr = new Array(); //声明数组，用来存储信息
                    arr.ID = vTypeItem[i];
                    arr.TYPE_NAME = vTypeNameItem[i];
                    tempArr[i] = arr;
                }
                vTypeArry = null;
                vTypeArry = tempArr;
            }

            if (vTypeArry.length > 0) {
                //根据选择的监测类别 动态生成监测类别Tab页
                var newDiv = '<div id="navtab1" position="center"  style = " width: 760px;height:400px; overflow:hidden; border:1px solid #A3C0E8; ">';

                newDiv += '<div id="divSXGL" title="时限管理" tabid="tabSXGL" lselected="true" style="height:380px" >';
                newDiv += '<iframe frameborder="0" name="showmessagefgfgf" src= "../ProcessMgm/ProcessMgm.aspx?strPlanId=' + strPlanId + '"></iframe>';
                newDiv += '</div>';

                for (var i = 0; i < vTypeArry.length; i++) {

                    if (i == 0) {
                        strTempTypeId = vTypeArry[i].ID;
                        strTempTypeName = vTypeArry[i].TYPE_NAME;
                        newDiv += '<div id="div' + strTempTypeId + '" title="' + vTypeArry[i].TYPE_NAME + '" tabid="home" lselected="false" style="height:380px" >';
                        newDiv += '<iframe frameborder="0" name="showmessage' + strTempTypeId + '" src= "../../Mis/MonitoringPlan/PointSelectedList.aspx?strPlanId=' + strPlanId + '&strEvnTypeId=' + strTempTypeId + '&strEvnTypeName=' + encodeURI(strTempTypeName) + '&strEvnYear=' + strYear + '&strEnvMonth=' + strMonth + '"></iframe>';
                        newDiv += '</div>';
                    }
                    else {
                        strTempTypeId = vTypeArry[i].ID;
                        strTempTypeName = vTypeArry[i].TYPE_NAME;
                        newDiv += '<div id="div' + strTempTypeId + '" title="' + vTypeArry[i].TYPE_NAME + '" style="height:380px" >';
                        newDiv += '<iframe frameborder="0" name="showmessage' + strTempTypeId + '" src= "../../Mis/MonitoringPlan/PointSelectedList.aspx?strPlanId=' + strPlanId + '&strEvnTypeId=' + strTempTypeId + '&strEvnTypeName=' + encodeURI(strTempTypeName) + '&strEvnYear=' + strYear + '&strEnvMonth=' + strMonth + '"></iframe>';
                        newDiv += '</div>';
                    }

                    vIframItem.push('showmessage' + strTempTypeId);
                    vIframMonitorType.push(vTypeArry[i].TYPE_NAME);
                    vIframMonitorTypeID.push(strTempTypeId);
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
                        navtab = $("#navtab1").ligerGetTabManager();
                        //navtab.reload(navtab.getSelectedTabItemID());
                        if (tabid == "home") {
                            gridName = "0";
                        }
                        if (tabid == "tabitem1") {
                            gridName = "1";
                        }
                        // navtab.reload(tabid);
                    }
                });

            }

        }
        function SetRYearboxInitValue() {
            var strValue = "";
            strValue = vAutoYearItem[0].ID;
            return strValue;
        }

        function f_selectList() {

            if (strEvnTypeId == "") {
                objGrid.loadData();
                $.ligerDialog.warn('请选择环境质量类别!'); return;
            } else {
                $.ligerDialog.open({ title: '点位选择', name: 'winselector', width: 700, height: 500, top: 10, url: '../../Mis/MonitoringPlan/SelectList.aspx?strEvnTypeId=' + strEvnTypeId + '&strEnvYear=' + strYear + '&strEnvTypeName=' + encodeURI(strEvnTypeName), buttons: [
                { text: '确定', onclick: f_selectListOK },
                { text: '返回', onclick: f_selectListCancel }
            ]
                });
                return false;
            }
        }

        function f_selectListOK(item, dialog) {
            //alert("bbbbb");
            var fn = dialog.frame.f_select || dialog.frame.window.f_select;
            var data = fn();

            if (!data) {
                $.ligerDialog.warn('请选择行!');
                return;
            }

            var strPointNames = "";
            strEvnPointId = "";
            for (var i = 0; i < data.length; i++) {
                strPointNames += data[i].POINT_NAME + ",";
                strEvnPointId += data[i].ID + ",";
            }
            //alert(strPointNames);
            //alert(strEvnPointId + "888");
            $("#DmName").val(strPointNames.substring(0, strPointNames.length - 1));
            $("#hidDmId").val(strEvnPointId.substring(0, strEvnPointId.length - 1));
            if (strEvnPointId != "" && strPointNames != "") {
                strYear = new Date().getFullYear();
                LoadEvnGridData();
            }
            dialog.close();
        }

        function f_selectListCancel(item, dialog) {
            dialog.close();
        }
        function getPlanId() {
            return strPlanId;
        }

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

        //返回当前日期的后7天日期
        function TogetDateAfter7(date) {
            /*var strD = "";
            var thisYear = date.getYear();
            thisYear = (thisYear < 1900) ? (1900 + thisYear) : thisYear;
            var thisMonth = date.getMonth() + 1;
            //如果月份长度是一位则前面补0    
            if (thisMonth < 10) thisMonth = "0" + thisMonth;
            var thisDay = date.getDate() + 7;
            //如果天的长度是一位则前面补0    
            if (thisDay < 10) thisDay = "0" + thisDay;
            {

            strD = thisYear + "-" + thisMonth + "-" + thisDay;
            }
            return strD;
            */
            var newDate = new Date(date);
            newDate = newDate.valueOf();
            newDate = newDate + 7 * 24 * 60 * 60 * 1000;
            newDate = new Date(newDate);

            var y = newDate.getFullYear();
            var m = newDate.getMonth() + 1;
            var d = newDate.getDate();
            if (m <= 9) m = "0" + m;
            if (d <= 9) d = "0" + d;
            var strD = y + "-" + m + "-" + d;
            return strD;
        }

        function DelPlan() {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: "../../Mis/MonitoringPlan/MonitoringPlan.ashx?action=DelContractPlan&strPlanId=" + strPlanId,
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
    })

function GetParmStr() {
    var strResult = "";
    var rowSelected = objGrid.getSelectedRows();
    if (rowSelected.length < 1) {
        $.ligerDialog.warn('请选择行');
        return;
    }
    strResult += "&strEvnTypeId=" + strEvnTypeId;
    strResult += "&strProjectName=" + strProjectName;
    strResult += "&strPointId=" + strEvnPointId;
    strResult += "&strDate=" + strDate;
    strResult += "&strAskingDate=" + strAskingDate;
    return strResult;
}

function GetPointId() {
    return strEvnPointId;
}
function GetPlanId() {
    return strPlanId;
}

function GetAskingDate() {
    return strAskingDate;
}
function GetPointItems() {
    var rowDate = [];
    var rowSelected = objGrid.getSelectedRows();
    if (rowSelected.length < 1) {
        $.ligerDialog.warn('请选择行');
        return;
    }
    for (var i = 0; i < rowSelected.length; i++) {
        rowDate.push(rowSelected[i].ID);
    }
    return rowDate;
}

function GetKeyColumn() {

    return strKeyColumns;
}

function GetTableName() {
    return strTableName;
}

function GetProjectName() {
    return strProjectName;
}

function GetEnvType() {
    return strEvnTypeId;
}
function GetPointItemsName() {
    var rowDate = [];
    var rowSelected = objGrid.getSelectedRows();
    if (rowSelected.length < 1) {
        $.ligerDialog.warn('请选择行');
        return;
    }
    for (var i = 0; i < rowSelected.length; i++) {
        if (ShowStatus == "1") {
            rowDate.push(strEvnTypeName + '-' + rowSelected[i].POINT_NAME);
        } else {
            rowDate.push(rowSelected[i].POINT_NAME);
        }
    }
    return rowDate;
}


function GetIfram() {
    var str = "";
    if (vIframItem.length > 0) {
        str = "1";
    }
    return str;
}

function GetBcInfo() {
    return $("#chkBC")[0].checked;
}

function GetchkFXS() {
    return $("#chkFXS")[0].checked;
}

function GetDataRow() {
    var strParme = "";
    if (vIframItem.length > 0) {
        for (var i = 0; i < vIframItem.length; i++) {
            var fn_1 = window.frames[vIframItem[i]].GetDataRow;
            var fn_value = fn_1();
            if (fn_value == "") {
                strParme += vIframMonitorType[i] + ";";
            }
        }
    }
    if (strParme != "") {
        strParme = strParme.substring(0, strParme.length - 1);
    }
    return strParme;
}

function SaveDataRowPoint() {
    var strParme = false;
    if (vIframItem.length > 0) {
        for (var i = 0; i < vIframItem.length; i++) {
            var fn_1 = window.frames[vIframItem[i]].GetDataRowPoint;
            var fn_value = fn_1();
            if (fn_value.length > 0) {
                var pointId = ReturnIDStr(fn_value);
                var pointName = ReturnNameStr(fn_value);
                var fn_table = window.frames[vIframItem[i]].GetTableName;
                var data_table = fn_table();
                var fn_key = window.frames[vIframItem[i]].GetKeyName;
                var data_key = fn_key();
                if (pointId != "" && pointName != "" && strPlanId != "") {
                    strParme = SavePointItems(strPlanId, vIframMonitorTypeID[i], pointId, pointName, strProjectName, data_table, data_key);
                }
            }
        }
    }
    return strParme;
}


function ReturnIDStr(Arr) {
    var str = "";
    if (Arr.length > 0) {
        for (var i = 0; i < Arr.length; i++) {
            str += Arr[i].ID + ";";
        }
    }
    if (str != "") {
        str = str.substring(0, str.length - 1);
    }
    return str;
}


function ReturnNameStr(Arr) {
    var str = "";
    if (Arr.length > 0) {
        for (var i = 0; i < Arr.length; i++) {
            str += Arr[i].POINT_NAME + ";";
        }
    }
    if (str != "") {
        str = str.substring(0, str.length - 1);
    }
    return str;
}

function SavePointItems(strplanId, strevntype, strpointid, strpointname, strproject, strtable, strkeycolumns) {
    var blFlag = false;
    //        url: "MonitoringPlan.ashx?action=InsertEnvContractPoint&strPlanId=" + strplanId + "&strEvnTypeId=" + strevntype + "&strPointItem=" + strpointid + "&strPointItemName=" + encodeURI(strpointname) + "&strProjectName=" + encodeURI(strproject) + "&strTableName=" + strtable + "&strKeyColumns=" + strkeycolumns,
    //    data: "{'strPlanId':'" + strplanId + "','strEvnTypeId':'" + strevntype + "','strPointItem':'" + strpointid + "','strPointItemName':'" + encodeURI(strpointname) + "','strProjectName':'" + encodeURI(strproject) + "','strTableName':'" + strtable + "','strKeyColumns':'" + strkeycolumns + "'}",
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "EnvPlanAdd.aspx/InsertEnvContractPoint_Pan",
        data: "{'strPlanId':'" + strplanId + "','strEvnTypeId':'" + strevntype + "','strPointItem':'" + strpointid + "','strPointItemName':'" + strpointname + "','strProjectName':'" + strproject + "','strTableName':'" + strtable + "','strKeyColumns':'" + strkeycolumns + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == "1") {
                blFlag = true;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            return;
        }
    });

    return blFlag;
}

//潘德军 2013-12-23 任务单号可改，且初始不生成
//任务单号可改，且初始不生成
//得到任务单号
function getTASK_CODE() {

    var strData1 = $("#txtTASK_CODE").val();

    return strData1;
}

function getSendDept() {
    return $("#txtSendDept_OP").val();
}

//CCFLOW工作流发送前事件
function Save() {
    var data_ifram = GetIfram();
    if (data_ifram == "") {
        $.ligerDialog.warn('请选择监测类别，并生成初始化数据！');
        return 0;
    }
    var data_row = GetDataRow();
    if (data_row != "") {
        $.ligerDialog.warn('请选择【<a style="color:Red;font-weight:bold">' + data_row + '</a>】监测类别的点位!');
        return 0;
    }
    if (strProjectName == "") {
        $.ligerDialog.warn('请填写项目名称！');
        return 0;
    }

    var strEnvType = "";
    strEnvType = GetEnvType();
    if (strEnvType == "") {
        return 0;
    }

    var strPlanId = "";
    var data_PlanId = GetPlanId();
    if (data_PlanId == "") {
        return 0;
    }

    strPlanId = data_PlanId;
    var strAskingDate = "";
    var data_AskingDate = GetAskingDate();
    if (data_AskingDate == "") {
        return 0;
    }

    strAskingDate = data_AskingDate;
    //潘德军 2013-12-23 任务单号可改，且自动生成
    var data_TaskCode = getTASK_CODE();
    if (data_TaskCode == "") {
        $.ligerDialog.warn('请先录入任务单号！');
        return 0;
    }
    //黄进军 add 20150917 备注
    var strRemarks = "";
    strRemarks = $("#txtRemarks").val();

    var data_save = SaveDataRowPoint();
    var iBC = GetBcInfo();
    var chkFXS = GetchkFXS();
    if (data_save) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须strTaskNum
            type: "POST",
            url: "../../Mis/MonitoringPlan/MonitoringPlan.ashx?action=doPlanTask&strTaskNum=" + encodeURI(data_TaskCode) + "&strEnvTypeId=" + strEnvType + "&strProjectName=" + encodeURI(strProjectName) + "&strAskingDate=" + strAskingDate + "&strPlanId=" + strPlanId + "&strState=" + iBC + "&strFXS=" + chkFXS + "&strRemarks=" + encodeURI(strRemarks),
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data == "True") {
                    return 1;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                return 0;
            }
        });
    }
    //return 1;
}

//获取任务类型名称信息
function GetMonitorType(strType) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getMonitorName",
        data: "{'strMonitorName':'" + strType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
            //alert(strValue);
        }
    });
    return strValue;
}

