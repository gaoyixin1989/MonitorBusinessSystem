
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
var vContratTypeItem = null, vAutoYearItem = null, vMonitorType = null, vEnvTypesItems = null;
var strEvnTypeId = "", strEvnTypeName = "", strEvnPointId = "", strMonth = "",  strKeyColumns = "", strTableName = "", FatherKeyColumn = "", FatherKeyValue = "", SubKeyColumn = "";
var objColumnItem = [], vTypeItem = null, vTypeNameItem = null, vTypeArry = null;
var isus = true;
var vIframItem = [], vIframMonitorType = [], vIframMonitorTypeID = [];
var strMangerAuditSetting = "";
var DeptItems = [
    { "VALUE": "01", "TEXT": "现场室" },
    { "VALUE": "02", "TEXT": "分析室" }
];
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

    $(document).ready(function () {
        $("#layout1").ligerLayout({ topHeight: 138, leftWidth: "99%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });
        strPlanId = $.getUrlVar('strPlanId');
        strIfPlan = $.getUrlVar('strIfPlan');
        strDate = $.getUrlVar('strDate');
        strMangerAuditSetting = $.getUrlVar('strMangerAuditSetting');

        $("#chkBC").ligerCheckBox();

        $("#createDiv").html("<a style='color:Red;font-weight:bold'>请选择环境质量监测类别并生成计划</a>");
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../Contract/MethodHander.ashx?action=GetWebConfigValue&strKey=ShowEnvTypeName",
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
            url: "../Contract/MethodHander.ashx?action=GetDict&type=POINT_SAMPLEFREQ",
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
            url: "../Contract/MethodHander.ashx?action=GetDict&type=EnvTypes",
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

        $("#EnvTypes").ligerComboBox({ data: vEnvTypesItems, width: 200, valueFieldID: 'EnvTypes_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isShowCheckBox: true, isMultiSelect: true, selectBoxHeight: 200, onSelected: function (value, text) {
            strEvnTypeId = value;
            strEvnTypeName = text;
        }
        });

        $("#EnvMonth").ligerComboBox({ data: monthJSON, width: 200, valueFieldID: 'EnvMonth_OP', valueField: "VALUE", textField: "MONTH", initValue: new Date().getMonth() + 1 });

        $("#txtSendDept").ligerComboBox({ data: DeptItems, width: 200, valueFieldID: 'txtSendDept_OP', valueField: 'VALUE', textField: 'TEXT', initValue: '01' });
        if (strMangerAuditSetting == "1") {
            $("#trSendDept")[0].style.display = "";
        }

        $("#txtProjectName").attr("style", "width:500px");
        //    $("#DmName").ligerComboBox({ onBeforeOpen: f_selectList, valueFieldID: 'hidDmId', width: 500 });
        //    if (!strDate) {
        //        strDate = TogetDate(new Date());
        //    }

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
            $.ligerDialog.open({ title: '任务单号查询', name: 'winaddtor', width: 320, height: 280, top: 90, url: '../MonitoringPlan/PendingDoTask_Search.aspx?CONTRACT_TYPE=' + CONTRACT_TYPE, buttons: [
                { text: '关闭', onclick: f_Cancel }
            ]
            });
        });

        $("#btnGet").bind("click", function () {
            if (strEvnTypeId != "") {
                $.ajax({
                    cache: false,
                    async: false, //设置是否为异步加载,此处必须
                    type: "POST",
                    url: "MonitoringPlan.ashx?action=InsertEnvPlanByCreate&strDate=" + strDate + "&strAskingDate=" + strAskingDate + "&strEnvTypeId=" + strEvnTypeId,
                    contentType: "application/text; charset=utf-8",
                    dataType: "text",
                    success: function (data) {
                        if (data != "") {
                            strPlanId = data;
                            $("#createDiv").html("");
                            strYear = new Date().getFullYear();
                            strMonth = $("#EnvMonth_OP").val();
                            strProjectName = strYear + "年度" + strMonth + "月" + "环境质量监测" + "(" + strEvnTypeName + ")";
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
                for (var i = 0; i < vTypeArry.length; i++) {

                    if (i == 0) {
                        strTempTypeId = vTypeArry[i].ID;
                        strTempTypeName = vTypeArry[i].TYPE_NAME;
                        newDiv += '<div id="div' + strTempTypeId + '" title="' + vTypeArry[i].TYPE_NAME + '" tabid="home" lselected="true" style="height:380px" >';
                        newDiv += '<iframe frameborder="0" name="showmessage' + strTempTypeId + '" src= "PointSelectedList.aspx?strPlanId=' + strPlanId + '&strEvnTypeId=' + strTempTypeId + '&strEvnTypeName=' + encodeURI(strTempTypeName) + '&strEvnYear=' + strYear + '&strEnvMonth=' + strMonth + '"></iframe>';
                        newDiv += '</div>';
                    }

                    else {
                        strTempTypeId = vTypeArry[i].ID;
                        strTempTypeName = vTypeArry[i].TYPE_NAME;
                        newDiv += '<div id="div' + strTempTypeId + '" title="' + vTypeArry[i].TYPE_NAME + '" style="height:380px" >';
                        newDiv += '<iframe frameborder="0" name="showmessage' + strTempTypeId + '" src= "PointSelectedList.aspx?strPlanId=' + strPlanId + '&strEvnTypeId=' + strTempTypeId + '&strEvnTypeName=' + encodeURI(strTempTypeName) + '&strEvnYear=' + strYear + '&strEnvMonth=' + strMonth + '"></iframe>';
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
                        navtab.reload(navtab.getSelectedTabItemID());
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
                $.ligerDialog.open({ title: '点位选择', name: 'winselector', width: 700, height: 500, top: 10, url: 'SelectList.aspx?strEvnTypeId=' + strEvnTypeId + '&strEnvYear=' + strYear + '&strEnvTypeName=' + encodeURI(strEvnTypeName), buttons: [
                { text: '确定', onclick: f_selectListOK },
                { text: '返回', onclick: f_selectListCancel }
            ]
                });
                return false;
            }
        }

        function f_selectListOK(item, dialog) {
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
    var rowDate =[];
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

function GetDataRow() {
    var strParme = "";
    if (vIframItem.length > 0) {
        for (var i = 0; i < vIframItem.length; i++) {
            var fn_1 = window.frames[vIframItem[i]].GetDataRow;
            var fn_value = fn_1();
            if (fn_value == "") {
                strParme +=vIframMonitorType[i]+ ";";
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
                    strParme = SavePointItems(strPlanId, vIframMonitorTypeID[i], pointId, pointName,strProjectName, data_table, data_key);
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
            str += Arr[i].ID+";";
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
            str += Arr[i].POINT_NAME + ";" ;
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
        data: "{'strPlanId':'"+strplanId+"','strEvnTypeId':'"+strevntype+"','strPointItem':'"+strpointid+"','strPointItemName':'"+strpointname+"','strProjectName':'"+strproject+"','strTableName':'"+strtable+"','strKeyColumns':'"+strkeycolumns+"'}",
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
