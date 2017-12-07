// Create by 熊卫华 2013.07.02  样品分析通知单打印功能

var objOneGrid = null;

var strUrl = "SamplesOrder.aspx";
var strOneGridTitle = "样品分析通知单";

//监测任务管理
$(document).ready(function () {

    objOneGrid = $("#oneGrid").ligerGrid({
        title: strOneGridTitle,
        dataAction: 'server',
        usePager: false,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        enabledSort: false,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '100%',
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '时间', name: 'TASK_DATE', align: 'left', width: 100, minWidth: 60 },
                { display: '编号', name: 'SAMPLE_CODE', width: 150, minWidth: 60 },
                { display: '采样地点/采样单位', name: 'COMPANY_NAME', width: 200, minWidth: 60, render: function (record) {
                    return record.COMPANY_NAME + record.SAMPLE_NAME;
                }
                },
                { display: '样品种类', name: 'SAMPLE_TYPE', width: 100, minWidth: 60 },
                { display: '监测项目', name: 'ITEM_NAME', width: 300, minWidth: 60 },
                { display: '完成日期', name: 'ASKING_DATE', width: 100, minWidth: 60 },
                { display: '备注', name: 'PROJECT_NAME', width: 100, minWidth: 60 },
                { display: '打印状态', name: 'SAMPLES_ORDER_ISPRINTED', width: 100, minWidth: 60, render: function (record) {
                    if (record.SAMPLES_ORDER_ISPRINTED == "1")
                        return "<font color='red'>已打印</font>";
                    else
                        return "未打印";
                }
                }
                ],
        toolbar: { items: [
                { text: '导出（室内）', click: ExportSamplesOrderInner, icon: 'add' }, { text: '导出（室外）', click: ExportSamplesOrderOuter, icon: 'add'}]
        },
        onCheckRow: function (checked, rowdata, rowindex) {

        }
    });
    //导出数据
    function ExportSamplesOrderInner() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('导出之前请先选择样品');
            return;
        }
        $.ligerDialog.confirm('您确定要导出样品分析通知单吗？', function (yes) {
            if (yes == true) {
                $("#cphData_hidden").val("0");
                var strSampleId = "";
                var spit = "";
                for (var i = 0; i < objOneGrid.getSelecteds().length; i++) {
                    strSampleId += spit + objOneGrid.getSelecteds()[i].ID;
                    spit = ",";
                }
                $("#cphData_strSampleId").val(strSampleId);
                $("#cphData_btnImport").click();
                objOneGrid.loadData();
            }
        });
    }
    //导出数据
    function ExportSamplesOrderOuter() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('导出之前请先选择样品');
            return;
        }
        $.ligerDialog.confirm('您确定要导出样品分析通知单吗？', function (yes) {
            if (yes == true) {
                $("#cphData_hidden").val("1");
                var strSampleId = "";
                var spit = "";
                for (var i = 0; i < objOneGrid.getSelecteds().length; i++) {
                    strSampleId += spit + objOneGrid.getSelecteds()[i].ID;
                    spit = ",";
                }
                $("#cphData_strSampleId").val(strSampleId);
                $("#cphData_btnImport").click();
                objOneGrid.loadData();
            }
        });
    }
});