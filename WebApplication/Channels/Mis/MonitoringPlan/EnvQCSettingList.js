var objGrid = null, vEnvTypesItems = null;
var strPlanId = "", strPointId = "", strPointItems = "", strPointItemsName = "", strKeyColumns = "", strTableName = "", strProjectName = "", strEnvType = "";
var strSampleUrl = "", gridName = "0";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetWebConfigValue&strKey=SampeTaskUrl",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                strSampleUrl = data;
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

    objGrid = $("#maingrid").ligerGrid({
        columns: [
                    { display: '点位名称', name: 'POINT_NAME', align: 'left', width: 120 },
                    { display: '年度', name: 'YEAR', align: 'left', width: 120 },
                    { display: '月份', name: 'MONTH', align: 'left', width: 120 },
                    { display: '环境类别', name: 'MONITOR_ID', align: 'left', width: 120, minWidth: 80, render: function (items) {
                        for (var i = 0; i < vEnvTypesItems.length; i++) {
                            if (vEnvTypesItems[i].DICT_CODE == items.MONITOR_ID) {
                                return vEnvTypesItems[i].DICT_TEXT;
                            }
                        }
                        return items.MONITOR_ID;
                    }
                    },
                    { display: '现场空白项目', name: 'XC_ITEMS', align: 'left', width: 400, render: function (items) {
                        var strPxItems = getItemList(items.ID, '1');
                        if (strPxItems.length > 40) {
                            return "<a title='" + strPxItems + "'>" + strPxItems.substring(0, 40) + "...</a>"
                        }
                        return strPxItems;
                    }
                    },
                    { display: '现场平行项目', name: 'KB_ITEMS', align: 'left', width: 400, render: function (items) {
                        var strKbItems = getItemList(items.ID, '3');
                        if (strKbItems.length > 40) {
                            return "<a title='" + strKbItems + "'>" + strKbItems.substring(0, 40) + "...</a>"
                        }
                        return strKbItems;
                    }
                    }
                ],
        width: '100%',
        height: '99%',
        toolbar: { items: [
                { id: 'add', text: '增加', click: f_Add, icon: 'add' },
                { line: true },
                 { id: 'dele', text: '删除', click: f_delete, icon: 'delete' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
            ]
        },
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 20,
        url: 'MonitoringPlan.ashx?action=GetEnvQCSettingPointList',
        dataAction: 'server', //服务器排序
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

    function getItemList(strQcSettingId, strQcTypeId) {
        var ItemName = "";
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=GetEnvPointItemsForQcSetting&strPointQcSetting_Id=" + strQcSettingId + "&strQcStatus=" + strQcTypeId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != "0") {
                    for (var i = 0; i < data.Rows.length; i++) {
                        ItemName += data.Rows[i].ITEM_NAME + ";";
                    }
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
        if (ItemName != "") {
            ItemName = ItemName.substring(0, ItemName.length - 1);
        }
        return ItemName;
    }


    function f_delete() {
        var rowSelect = null, grid = null;
        rowSelect = objGrid.getSelectedRow()
        grid = objGrid;

        if (rowSelect != null) {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: "MonitoringPlan.ashx?action=DelPointQcSetting&strPointQcSetting_Id=" + rowSelect.ID,
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    if (data == "True") {
                        objGrid.loadData();
                        $.ligerDialog.success('数据操作成功！');
                    }
                    else {
                        $.ligerDialog.warn('数据操作失败！');
                    }
                },
                error: function (msg) {
                    $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                }
            });
        }
        else {
            $.ligerDialog.warn('请选择一行！');
            return;
        }
    }


    function f_Add() {
        $.ligerDialog.open({ title: "环境质量类质控设置", name: 'winselector', width: 800, height: 600, top: 30, url: 'EnvQCSettingAdd.aspx', buttons: [
                { text: '确定', onclick: f_ListOK },
                { text: '返回', onclick: f_ListCancel }
            ]
        });
    }



    function f_ListOK(item, dialog) {
        dialog.close();
        objGrid.loadData();
    }

    function f_ListCancel(item, dialog) {
        dialog.close();
        objGrid.loadData();
    }


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
                     { display: "点位名称", name: "SEA_POINT_NAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "环境质量类型", name: "SEA_CONTRACT_TYPE", newline: false, type: "select", comboboxName: "SEA_CONTRACT_TYPE_BOX", options: { valueFieldID: "SEA_CONTRACT_TYPE_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vEnvTypesItems} },
                     { display: "年度", name: "SEA_YEAR", newline: true, type: "select", comboboxName: "ddlYearBox", options: { valueFieldID: "hidYear_OP", valueField: "value", textField: "value", resize: false, url:"MonitoringPlan.ashx?action=GetYear"} },
                     { display: "月份", name: "SEA_MONTH", newline: false, type: "select", comboboxName: "ddlMonthBox", options: { valueFieldID: "hidMonth_OP", valueField: "value", textField: "value", resize: false, url: "MonitoringPlan.ashx?action=GetMonth"} }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 660, height: 240, top: 90, title: "项目质控查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SEA_POINT_NAME = encodeURI($("#SEA_POINT_NAME").val());
            var SEA_CONTRACT_TYPE_OP = $("#SEA_CONTRACT_TYPE_OP").val();
            var SEA_YEAR = $("#hidYear_OP").val();
            var SEA_MONTH = $("#hidMonth_OP").val();
            var url = "MonitoringPlan.ashx?action=GetEnvQCSettingPointList&strPoint_Name=" + SEA_POINT_NAME + "&strMonitorId=" + SEA_CONTRACT_TYPE_OP + "&strYear=" + SEA_YEAR + "&strMonth=" + SEA_MONTH;
            objGrid.set('url', url)
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_POINT_NAME").val("");
        $("#SEA_CONTRACT_TYPE_OP").val("");
        $("#hidYear_OP").val("");
        $("#hidMonth_OP").val("");
    }
});