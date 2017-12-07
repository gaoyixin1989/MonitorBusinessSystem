var strContractTypeId = "", strCompanyId = "", strContractId = "", strPointId = "", strPlanId = "";
var maingrid = null, vMonitorType = null;
var checkedCustomer = [];
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
    strContractId = $.getUrlVar('strContractId');
    strPointId = $.getUrlVar('strPointID');
    strPlanId = $.getUrlVar('strPlanId');
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

    maingrid = $("#maingrid").ligerGrid({
        columns: [
                    { display: '点位', name: 'POINT_NAME', align: 'left', width: 200 },
                    { display: '监测类别', name: 'MONITOR_ID', width: 80, minWidth: 60, data: vMonitorType, render: function (items) {
                        for (var i = 0; i < vMonitorType.length; i++) {
                            if (vMonitorType[i].ID == items.MONITOR_ID) {
                                return vMonitorType[i].MONITOR_TYPE_NAME;
                            }
                        }
                        return items.MONITOR_ID;
                    }
                    }
                ],
        width: '100%',
        height: '96%',
        //url: 'MonitoringPlan.ashx?action=GetContractPointList&task_id=' + strContractId + '&strPointId=' + strPointId,
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 5,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: true,
        isChecked: f_isChecked,
        onCheckRow: f_onCheckRow,
        onCheckAllRow: f_onCheckAllRow
    });
    $("#pageloading").hide();

    if (strContractId && strContractId != "") {
        maingrid.set('url','MonitoringPlan.ashx?action=GetContractPointList&task_id=' + strContractId + '&strPointId=' + strPointId);
    }
    if(strPlanId&&strPlanId!=""){
        maingrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointList&strPlanId=' + strPlanId + '&strPointId=' + strPointId);
    }

    function f_onCheckAllRow(checked) {
        for (var rowid in this.records) {
            if (checked)
                addCheckedCustomer(this.records[rowid]['ID']);
            else
                removeCheckedCustomer(this.records[rowid]['ID']);
        }
    }

    /*
    该例子实现 表单分页多选
    即利用onCheckRow将选中的行记忆下来，并利用isChecked将记忆下来的行初始化选中
    */
    function findCheckedCustomer(CustomerID) {
        for (var i = 0; i < checkedCustomer.length; i++) {
            if (checkedCustomer[i] == CustomerID) return i;
        }
        return -1;
    }
    function addCheckedCustomer(CustomerID) {
        if (findCheckedCustomer(CustomerID) == -1)
            checkedCustomer.push(CustomerID);
    }
    function removeCheckedCustomer(CustomerID) {
        var i = findCheckedCustomer(CustomerID);
        if (i == -1) return;
        checkedCustomer.splice(i, 1);
    }
    function f_isChecked(rowdata) {
        if (findCheckedCustomer(rowdata.ID) == -1)
            return false;
        return true;
    }
    function f_onCheckRow(checked, data) {
        if (checked) addCheckedCustomer(data.ID);
        else removeCheckedCustomer(data.ID);
    }
    function f_getChecked() {
        alert(checkedCustomer.join(','));
    }
})

function GetSelectPoint() {
    var strVal = "";
    if (checkedCustomer.length> 0) {
        for (var i = 0; i < checkedCustomer.length; i++) {
            strVal += checkedCustomer[i] + ";";
        }
        strVal = strVal.substring(0, strVal.length - 1);
    }

    return strVal;
}