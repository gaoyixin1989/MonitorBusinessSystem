// Create by 熊卫华 2012.12.15 "采样任务分配表"查询导出功能
var objOneGrid = null;

var strUrl = "SamplingAssignmentSheet.aspx";
var strOneGridTitle = "采样任务分配表信息";

//监测任务管理
$(document).ready(function () {
    objOneGrid = $("#oneGrid").ligerGrid({
        title: strOneGridTitle,
        dataAction: 'server',
        usePager: true,
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [10, 15, 20, 50],
        height: '100%',
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '监测单位', name: 'COMPANY_NAME', align: 'left', width: 200, minWidth: 60 },
                { display: '任务下达日期', name: 'SAMPLE_ASK_DATE', width: 100, minWidth: 60 },
                { display: '监测类别', name: 'CONTRACT_TYPE_NAME', width: 100, minWidth: 60 },
                { display: '监测内容', name: 'MONITOR_TYPE_NAME', width: 150, minWidth: 60 },
                { display: '采样人', name: 'SAMPLING_MANAGER_NAME', width: 100, minWidth: 60 },
                { display: '采样协同人', name: 'SAMPLING_MAN', width: 250, minWidth: 60 }
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
            $.ligerDialog.warn('导出之前请先选择采样任务');
            return;
        }
        $.ligerDialog.confirm('您确定要导出采样任务分配表吗？', function (yes) {
            if (yes == true) {
                var strSubTaskId = "";
                var spit = "";
                for (var i = 0; i < objOneGrid.getSelecteds().length; i++) {
                    strSubTaskId += spit + objOneGrid.getSelecteds()[i].ID;
                    spit = "','";
                }
                $("#cphData_strSubTaskId").val(strSubTaskId);
                $("#cphData_btnImport").click();
                objOneGrid.loadData();
            }
        });
    }
});