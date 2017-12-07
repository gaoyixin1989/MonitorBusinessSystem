
/// 环境质量质控计划新增
/// 创建时间：2012-12-22
/// 创建人：胡方扬
var objGrid = null, vMonitorType = null, vPointItem = null, vMonth = null, vContractInfor = null, vDutyInitUser = null;
var task_id = "", struser = "", strmonitorID = "", monitorTypeName = "";
var tr = "", MonitorArr = [];
var saveflag = "";
var vDutyList = null;
var strPlanId = "", strIfPlan = "", strDate = "";
var strYear = "", strProjectName = "", strContratTypeId = "", strContactType = "";
var strCompanyId = "", strCompanyName = "", strIndustryId = "", strAreaId = "", strContactName = "", strTelPhone = "", strAddress = "";
var strCompanyIdFrim = "", strCompanyNameFrim = "", strIndustryIdFrim = "", strAreaIdFrim = "", strContactNameFrim = "", strTelPhoneFrim = "", strAddressFrim = "";
var GetSampleFreqItems = null;
var strFreqId = "", strPointId = "", strInitValue = "", ShowStatus = "0";
var isEdit = false;
var checkedCustomer = [], checkedMonitorArr = [], checkedPoint = [], moveCheckPoint = [], moveCheckCustomer = [];
var vContratTypeItem = null, vAutoYearItem = null, vMonitorType = null, vEnvTypesItems = null;
var strEvnTypeId = "", strEvnTypeName = "", strEvnPointId = "", strMonth = "",  strKeyColumns = "", strTableName = "", FatherKeyColumn = "", FatherKeyValue = "", SubKeyColumn = "";
var objColumnItem = [], vTypeItem = null, vTypeNameItem = null, vTypeArry = null;
var isus = true;
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
    $("#layout1").ligerLayout({ topHeight: 60, leftWidth: "99%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });
    strPlanId = $.getUrlVar('strPlanId');
    strIfPlan = $.getUrlVar('strIfPlan');
    strDate = $.getUrlVar('strDate');

    $("#createDiv").html("<a style='color:Red;font-weight:bold'>请选择环境质量监测类别</a>");
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

    $("#EnvTypes").ligerComboBox({ data: vEnvTypesItems, width: 500, valueFieldID: 'EnvTypes_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isShowCheckBox: true, isMultiSelect: true, selectBoxHeight: 200, onSelected: function (value, text) {
        strEvnTypeId = value;
        strEvnTypeName = text;
    }
    });
    $("#txtProjectName").attr("style", "width:500px");

    strDate = TogetDate(new Date());
    $("#txtDate").ligerDateEditor({ initValue: strDate, width: 200, onChangeDate: function (value) {
        strDate = $("#txtDate").val();
    }
    });
    $("#btnGet").bind("click", function () {
        if (strEvnTypeId != "") {
            $("#createDiv").html("");
            strYear = new Date().getFullYear();
            strMonth = new Date().getMonth() + 1;
            strProjectName = strYear + "年度" + strMonth + "月" + "环境质量质控计划" + "(" + strEvnTypeName + ")";
            $("#txtProjectName").val(strProjectName);
            CreatTable();
            $("#btnGet").attr("disabled",true);
        }
        else {
            $.ligerDialog.warn("请选择环境质量类别！");
        }
    })
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
            //根据当前委托书的监测类别 动态生成监测类别
            var newDiv = '<div id="navtab1" position="center"  style = " width: 760px;height:600px; overflow:hidden; border:1px solid #A3C0E8; ">';
            for (var i = 0; i < vTypeArry.length; i++) {

                if (i == 0) {
                    strTempTypeId = vTypeArry[i].ID;
                    strTempTypeName = vTypeArry[i].TYPE_NAME;
                    newDiv += '<div id="div' + strTempTypeId + '" title="' + vTypeArry[i].TYPE_NAME + '" tabid="home" lselected="true" style="height:400px" >';
                    newDiv += '<iframe frameborder="0" name="showmessage' + strTempTypeId + '" src= "PointSelectedQCSettingList.aspx?strEvnTypeId=' + strTempTypeId + '&strEvnTypeName=' + encodeURI(strTempTypeName) + '&strEvnYear=' + strYear + '&strProjectName=' + strProjectName + '"></iframe>';
                    newDiv += '</div>';
                }

                else {
                    strTempTypeId = vTypeArry[i].ID;
                    strTempTypeName = vTypeArry[i].TYPE_NAME;
                    newDiv += '<div id="div' + strTempTypeId + '" title="' + vTypeArry[i].TYPE_NAME + '" style="height:400px" >';
                    newDiv += '<iframe frameborder="0" name="showmessage' + strTempTypeId + '" src= "PointSelectedQCSettingList.aspx?strEvnTypeId=' + strTempTypeId + '&strEvnTypeName=' + encodeURI(strTempTypeName) + '&strEvnYear=' + strYear + '&strProjectName=' + strProjectName + '"></iframe>';
                    newDiv += '</div>';
                }
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
                    navtab.reload(tabid);
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
    return strResult;
}

function GetPointId() {
    return strEvnPointId;
}
function GetPlanId() {
    return strPlanId;
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