// Create by weilin 2014.03.27  现场结果审核列表

var objGridList = null;
var strUrl = "SampleResultQcCheckList.aspx";
var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {
    
    objGridList = $("#divList").ligerGrid({
        columns: [
                { display: '任务单号', name: 'TICKET_NUM', width: 120 },
                { display: '监测类别', name: 'MONITOR_ID', width: 120, minWidth: 60, render: function (record) {
                    return getMonitorTypeName(record.MONITOR_ID);
                }
                },
                { display: '项目名称', name: 'PROJECT_NAME', width: 300 },
                { display: '委托书编号', name: 'CONTRACT_CODE', width: 100 }
                ],
        title: "",
        width: '100%',
        height: '100%',
        pageSizeOptions: [20, 30, 40, 50],
        url: strUrl+'?action=getListInfo',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 20,
        sortName: 'ID',
        sortOrder: 'DESC',
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'done', click: click_done, text: '办理', icon: 'modify' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onDblClickRow: function (data, rowindex, rowobj) {
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

});

function click_done() {
    var selected = objGridList.getSelectedRow();
    if (!selected) { $.ligerDialog.warn('请先选择一条记录！');return; }

    var tabid = "tabid" + selected.ID + "Check";
    var surl = "";
    var isBack = selected.TASK_TYPE == '退回' ? '1' : '0';
    if (selected.MONITOR_ID == "000000004" || selected.MONITOR_ID == "FunctionNoise" || selected.MONITOR_ID == "AreaNoise" || selected.MONITOR_ID == "EnvRoadNoise")
        surl = "../Channels/Mis/Monitor/sampling/QY/Sampling_Noise.aspx?Link=QcCheck&strSubtaskID=" + selected.ID + "&strMonitor_ID=" + selected.MONITOR_ID + "&IS_BACK=" + isBack;
    else
        surl = "../Channels/Mis/Monitor/Result/QY/SampleResultQcCheck.aspx?strSubtaskID=" + selected.ID + "&IS_BACK=" + isBack;
    top.f_addTab(tabid, '现场监测结果审核', surl);
}
