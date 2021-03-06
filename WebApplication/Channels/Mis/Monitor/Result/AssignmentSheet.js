﻿// Create by 熊卫华 2012.12.13  "分析任务分配单查询与导出"功能
var objOneGrid = null;

var strUrl = "AssignmentSheet.aspx";
var strOneGridTitle = "样品分析任务信息";

//监测任务管理
$(document).ready(function () {

    objOneGrid = $("#oneGrid").ligerGrid({
        title: strOneGridTitle,
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '100%',
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '样品编号', name: 'SAMPLE_CODE', align: 'left', width: 100, minWidth: 60 },
                { display: '样品类型', name: 'SAMPLE_TYPE', width: 100, minWidth: 60 },
                { display: '分配日期', name: 'ANALYSE_ASSIGN_DATE', width: 100, minWidth: 60 },
                { display: '分配项目', name: 'ITEM_NAME', width: 300, minWidth: 60 },
                { display: '要求上报时间', name: 'FINISH_DATE', width: 100, minWidth: 60 }
                ],
        toolbar: { items: [
                { text: '查询', click: Search, icon: 'add' },
                { text: '导出', click: Export, icon: 'add' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {

        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    //查询数据
    var objLigerForm = null;
    function Search() {
        var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
        //创建表单结构 
        if (objLigerForm) {
            objLigerForm.show();
        }
        else {
            $("#searchForm").ligerForm({
                inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
                fields: [
                { display: "委托类型", name: "Contract_Type_Name", newline: true, group: "基本信息", groupicon: groupicon, type: "select", comboboxName: "Drop_Contract_Type_Name", options: { valueFieldID: "Contract_Type_Name_Id", valueField: "DICT_CODE", textField: "DICT_TEXT", url: strUrl + "?type=getDict&dictType=Contract_Type"} },
                { display: "分配日期", name: "txtDateTime", newline: true, type: "date" },
                { display: "监测类别", name: "Monitor_Id_Name", newline: true, type: "select", comboboxName: "Drop_Monitor_Id_Name", options: { split: ',', isMultiSelect: true, isShowCheckBox: true, valueFieldID: "Monitor_Id", valueField: "ID", textField: "MONITOR_TYPE_NAME", url: strUrl + "?type=getMonitorType"} }
                ]
            });
            objLigerForm = $.ligerDialog.open({
                target: $("#searchForm"),
                width: 400, height: 230, title: "查询",
                buttons: [
                  { text: '确定', onclick: function (item, dialog) {
                      objOneGrid.set('url', strUrl + "?type=getOneGridInfo&strContractType=" + $("#Contract_Type_Name_Id").val() + "&strMonitorType=" + $("#Monitor_Id").val() + "&strAnalyseAssignDate=" + $("#txtDateTime").val());
                  }
                  },
                  { text: '取消', onclick: function (item, dialog) { clearSearch(); objLigerForm.hide(); } }
                  ]
            });
        }
    }
    function clearSearch() {
        $("#Drop_Contract_Type_Name").ligerGetComboBoxManager().setValue("");
        $("#txtDateTime").val('');
        $("#Drop_Monitor_Id_Name").ligerGetComboBoxManager().setValue("");
    }
    //导出数据
    function Export() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('导出之前请先选择样品');
            return;
        }
        $.ligerDialog.confirm('您确定要导出样品分析任务表吗？', function (yes) {
            if (yes == true) {
                var strSampleId = "";
                var spit = "";
                for (var i = 0; i < objOneGrid.getSelecteds().length; i++) {
                    strSampleId += spit + objOneGrid.getSelecteds()[i].ID;
                    spit = "','";
                }
                $("#cphData_strSampleId").val(strSampleId);
                $("#cphData_btnImport").click();
                objOneGrid.loadData();
            }
        });
    }
});