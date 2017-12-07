// Create by 潘德军 2013.01.07  "任务追踪"功能

var objGrid = null;
var url = "WFStepFollow.aspx";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

//任务追踪功能
$(document).ready(function () {

    //菜单
    var objmenu = $.ligerMenu({ width: 120, items:
            [
            { text: '追踪', click: dealData, icon: 'modify' }
            ]
    });

    objGrid = $("#objGrid").ligerGrid({
        title: '任务列表',
        dataAction: 'server',
        usePager: true,
        pageSize: 15,
        width: '100%',
        height: '100%',
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        pageSizeOptions: [10, 15, 20, 50],
        url: url + '?type=getData',
        columns: [
                 { display: '项目名称', name: 'PROJECT_NAME', align: 'left', isSort: false, width: 350 },
                 { display: '任务单号', name: 'TICKET_NUM', width: 150 },
                 { display: '委托书编号', name: 'CONTRACT_CODE', minWidth: 250 },
                 { display: '业务流程', name: 'WF_STEP', align: 'left', isSort: false, width: 150
                 },
                 { display: '当前环节', name: 'WF_TASK_NAME', align: 'left', isSort: false, width: 150
                 },
                 { display: '当前执行人', name: 'OBJECT_USER_NAME', align: 'left', isSort: false, width: 100
                 }
                ],
        toolbar: { items: [
             { text: '所有记录', click: searchAll, icon: 'search' },
             { line: true },
             { text: '查询', click: searchData, icon: 'search' },
             { line: true },
             { text: '追踪', click: dealData, icon: 'modify' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            objmenu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            dealData();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //追踪
    function dealData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行追踪！');
            return;
        }

        var strId = objGrid.getSelectedRow().ID;
        var strHeight = $(window).height();

        var strUrl = ("WFShowStepImg.aspx?task_id=" + strId);
        $(document).ready(function () { $.ligerDialog.open({ title: '任务追踪', url: strUrl, width: 400, height: strHeight, modal: false }); });
    }
});

function searchAll() {
    objGrid.set('url', url + "?type=getData");
}

//grid 的查询对话框
var detailWinSrh = null;
function searchData() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#searchFirstForm");
        mainform.ligerForm({
            inputWidth: 150, labelWidth: 90, space: 0, labelAlign: 'right',
            fields: [
                     { display: "任务单号", name: "SRH_TASK_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "委托书编号", name: "SRH_CONTRACT_CODE", newline: false, type: "text" },
                     { display: "项目名称", name: "SRH_PROJECT_NAME", newline: true, type: "text" },
                     { display: "委托类型", name: "SRH_CONTRACT_TYPE", newline: false, type: "select", comboboxName: "DROP_CONTRAC_TYPE", options: { valueFieldID: "CONTRAC_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: url + "?type=getContractType"} }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 520, height: 170, top: 90, title: "查询任务信息",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }
    function search() {
        var SRH_TASK_CODE = $("#SRH_TASK_CODE").val();
        var SRH_CONTRACT_CODE = $("#SRH_CONTRACT_CODE").val();
        var SRH_PROJECT_NAME = $("#SRH_PROJECT_NAME").val();
        var SRH_CONTRACT_TYPE = $("#CONTRAC_TYPE").val();

        objGrid.set('url', url + "?type=getData&srhContractType=" + SRH_CONTRACT_TYPE + "&srhContractCode=" + SRH_CONTRACT_CODE + "&srhProjectName=" + encodeURI(SRH_PROJECT_NAME) + "&srhTaskCode=" + encodeURI(SRH_TASK_CODE));
    }
}

//grid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#SRH_CONTRACT_CODE").val("");
    $("#SRH_COMPANY_ID").val("");
    $("#CONTRAC_TYPE").val();
    $("#SRH_TASK_CODE").val();
}

//获取用户信息
function getUserName(strUserID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: url + "/getUserName",
        data: "{'strValue':'" + strUserID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取字典名称
function GetContractType(code, type) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: url + "/GetDataDictName",
        data: "{'strValue':'" + code + "','strType':'" + type + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载监测类别数据失败！');
        }
    });
    return strReturn;
}
