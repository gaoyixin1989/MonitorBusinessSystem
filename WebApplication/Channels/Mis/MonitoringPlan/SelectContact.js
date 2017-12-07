var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var g = null;
var vContractData = null, vMonitorType = null, vAreaItem = null, vContratTypeItem = null;
var strContractTypeId = "";
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
$(function () {

    strContractTypeId = $.getUrlVar('strContractTypeId');
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetDict&type=administrative_area",
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
            if (data!=null) {
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

    g = $("#maingrid4").ligerGrid({
        columns: [
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 300 },
                { display: '合同号', name: 'CONTRACT_CODE', width: 160, align: 'left' },
                { display: '委托类别', name: 'CONTRACT_TYPE', width: 120, minWidth: 60, data: vContratTypeItem, render: function (items) {
                    for (var i = 0; i < vContratTypeItem.length; i++) {
                        if (vContratTypeItem[i].DICT_CODE == items.CONTRACT_TYPE) {
                            return vContratTypeItem[i].DICT_TEXT;
                        }
                    }
                    return items.CONTRACT_TYPE;
                }
                },
                { display: '所在区域', name: 'AREA', width: 80, minWidth: 60, data: vAreaItem, render: function (items) {
                    for (var i = 0; i < vAreaItem.length; i++) {
                        if (vAreaItem[i].DICT_CODE == items.AREA) {
                            return vAreaItem[i].DICT_TEXT;
                        }
                    }
                    return items.AREA;
                }
                },
                 { display: '监测类别', name: 'TEST_TYPES', width: 160, minWidth: 60, align: 'left' ,data: vMonitorType, render: function (items) {
                     var arrMonitorId = items.TEST_TYPES.split(';');
                     var strMonitor = "";
                     if (arrMonitorId != null) {
                         for (var i = 0; i < arrMonitorId.length; i++) {
                             for (var n = 0; n < vMonitorType.length; n++) {
                                 if (vMonitorType[n].ID == arrMonitorId[i]) {
                                     strMonitor += vMonitorType[n].MONITOR_TYPE_NAME + ";";
                                 }
                             }
                         }
                         return strMonitor.substring(0, strMonitor.length - 1);
                     }
                     return items.TEST_TYPES;
                 } 
                 }
                ],
        width: '100%', 
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        url: 'MonitoringPlan.ashx?action=GetContractPlanPointInfor&strIfPlan=0&strContractType='+strContractTypeId,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        toolbar: { items: [
                { id: 'search', text: '查询', click: showDetailSrh, icon: 'search' }
                ]
        },
        rownumbers: true
    });
    $("#pageloading").hide();

    //设置grid 的弹出查询对话框
    var detailWinSrh = null;
    function showDetailSrh() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构

            var mainform = $("#SrhForm");
            mainform.ligerForm({
                inputWidth: 170, labelWidth: 90, space: 40,
                fields: [
                     { display: "受检企业", name: "SEA_COMPANYNAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "所在区域", name: "SEA_AREA", newline: false, type: "select", comboboxName: "SEA_AREA_BOX", options: { valueFieldID: "SEA_AREA_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vAreaItem, render: function (item) {
                         for (var i = 0; i < vAreaItem.length; i++) {
                             if (vAreaItem[i]['DICT_CODE'] == item.AREA)
                                 return vAreaItem[i]['DICT_TEXT'];
                         }
                         return item.AREA;
                     }
                     }},
                     { display: "合同号", name: "SEA_CONTRACT_CODE", newline: true, type: "text" },
                    { display: "监测类型", name: "SEA_MONITOR_TYPE", newline: false, type: "select", comboboxName: "SEA_MONITOR_TYPE_BOX", options: { valueFieldID: "SEA_MONITOR_TYPE_OP", valueField: "ID", textField: "MONITOR_TYPE_NAME", data: vMonitorType }, render: function (item) {
                        for (var i = 0; i < vMonitorType.length; i++) {
                            if (vMonitorType[i]['ID'] == item.MONITOR_ID)
                                return vMonitorType[i]['MONITOR_TYPE_NAME'];
                        }
                        return item.MONITOR_ID;
                    }
                    }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 660, height: 170, top: 90, title: "委托书查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SEA_COMPANYNAME =encodeURI( $("#SEA_COMPANYNAME").val());
            var SEA_AREA_OP = encodeURI($("#SEA_AREA_OP").val());
            var SEA_CONTRACT_CODE = $("#SEA_CONTRACT_CODE").val();
            var SEA_MONITOR_TYPE_OP = $("#SEA_MONITOR_TYPE_OP").val();

            g.set('url', "MonitoringPlan.ashx?action=GetContractPlanPointInfor&strIfPlan=0&strContractType="+strContractTypeId+"&strCompanyNameFrim=" + SEA_COMPANYNAME + "&strAreaIdFrim=" + SEA_AREA_OP + "&strMonitorId=" + SEA_MONITOR_TYPE_OP + "&strContractCode=" + SEA_CONTRACT_CODE);
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_COMPANYNAME").val("");
        $("#SEA_CONTRACT_CODE").val("");
        $("#SEA_AREA_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_MONITOR_TYPE_BOX").ligerGetComboBoxManager().setValue("");
    }


});
function f_select() {
    return g.getSelectedRow();
}