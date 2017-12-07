// Create by 潘德军 2013.5.3  "纸质数据审核记录"功能

var objTaskHasAppGrid = null;
var strWF_ID = null;
var strWF_TASK_ID = null;
var strWF_TASK_Name = null;
var strTASK_ID = null;

//"纸质数据审核记录"功能
$(document).ready(function () {
    $("#layout1").ligerLayout({ height: '100%' });

    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '已签', click: appData, icon: 'modify' }
            ]
    });

    objTaskHasAppGrid = $("#companyInfoInfoGrid").ligerGrid({
        title: '纸质数据审核记录',
        dataAction: 'server',
        usePager: false,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        whenRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        height: '99%',
        url: 'WfSettingTaskHasapp.aspx?type=getAppInfo',
        columns: [
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 200, minWidth: 60 },
                { display: '合同号', name: 'CONTRACT_CODE' },
                { display: '任务单号', name: 'TICKET_NUM' },
                { display: '报告号', name: 'REPORT_CODE' },
                { display: '委托类别', name: 'CONTRACT_TYPE' },
                { display: '审批环节', name: 'INST_NOTE' },
                { display: '审批时间', name: 'INST_TASK_ENDTIME' }
                ],
        toolbar: { items: [
                { text: '已签', click: appData, icon: 'modify' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            appData();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            strWF_ID = rowdata.WF_ID;
            strWF_TASK_ID = rowdata.WF_TASK_ID;
            strWF_TASK_Name = rowdata.INST_NOTE;
            strTASK_ID = rowdata.SERVICE_KEY_VALUE;
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    function appData() {
        if (objTaskHasAppGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行已签设置');
            return;
        }
        $.ligerDialog.confirm('确认对 ' + objTaskHasAppGrid.getSelectedRow().PROJECT_NAME + " 设置已签吗？", function (yes) {
            if (yes == true) {
                strWF_ID = objTaskHasAppGrid.getSelectedRow().WF_ID;
                strWF_TASK_ID = objTaskHasAppGrid.getSelectedRow().WF_TASK_ID;
                strWF_TASK_Name = objTaskHasAppGrid.getSelectedRow().INST_NOTE;
                strTASK_ID = objTaskHasAppGrid.getSelectedRow().SERVICE_KEY_VALUE;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "WfSettingTaskHasapp.aspx/appData",
                    data: "{'strWF_ID':'" + strWF_ID + "','strWF_TASK_ID':'" + strWF_TASK_ID + "','strWF_TASK_Name':'" + strWF_TASK_Name + "','strTASK_ID':'" + strTASK_ID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objTaskHasAppGrid.loadData();
                            $.ligerDialog.success('设置已签成功')
                        }
                        else {
                            $.ligerDialog.warn('设置已签失败');
                        }
                    }
                });
            }
        });
    }

});