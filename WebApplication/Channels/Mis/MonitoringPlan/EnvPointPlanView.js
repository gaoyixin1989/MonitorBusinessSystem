
/// 环境质量监测计划新增
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
var strFreqId = "", strPointId = "", strInitValue = "";
var isEdit = false;
var checkedCustomer = [], checkedMonitorArr = [], checkedPoint = [], moveCheckPoint = [], moveCheckCustomer = [];
var vContratTypeItem = null, vAutoYearItem = null, vMonitorType = null, vEnvTypesItems = null;
var strEvnTypeId = "", strEvnTypeName = "", strEvnPointId = "", strKeyColumns = "", strTableName = "", FatherKeyColumn = "", FatherKeyValue = "", SubKeyColumn = "";
var objColumnItem = [];
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
    $("#layout1").ligerLayout({ topHeight: 0, leftWidth: "99%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });
    strPlanId = $.getUrlVar('strPlanId');
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

    objGrid = $("#gridItems").ligerGrid({
        columns: [
                    { display: '监测点位', name: 'POINT_NAME', align: 'left', width: 200 },
                     { display: '环境类别', name: 'MONITOR_ID', width: 80, minWidth: 60, render: function (items) {
                         for (var i = 0; i < vEnvTypesItems.length; i++) {
                             if (vEnvTypesItems[i].DICT_CODE == items.MONITOR_ID) {
                                 return vEnvTypesItems[i].DICT_TEXT;
                             }
                         }
                         return items.MONITOR_ID;
                     }
                     }
                ],
        width: '100%',
        height: '96%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        dataAction: 'server', //服务器排序
        url: 'MonitoringPlan.ashx?action=GetEnvPlanTaskListDetail&strPlanId=' + strPlanId,
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: true,
        whenRClickToSelect: true,
        onDblClickRow: function (data, rowindex, rowobj) {
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }

    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});