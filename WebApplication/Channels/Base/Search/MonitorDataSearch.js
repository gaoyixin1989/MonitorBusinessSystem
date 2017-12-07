//监测数据查询
//创建人：魏林 2014-02-18
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var vMonitorType = null;
var maingrid = null;
var detailExcelWin = null;
$(document).ready(function () {
    $("#btnSearch").ligerButton(
    {
        text: "查询",
        width: "40",
        click: function () {
            if ($("#hidType").val() == "environment")
                showEnvSrh();
            else
                showPollSrh();
        }
    }
    );
    $("#btnExport").ligerButton(
    {
        text: "导出",
        width: "40",
        click: function () {
            if (maingrid.rows.length == 0) {
                $.ligerDialog.warn('无数据导出！');
                return;
            }
            var contracttype = "";
            var companyname = "";
            var monitortype = "";
            var pointname = "";
            var datestart = "";
            var dateend = "";
            var itemid = "";
            if ($("#hidType").val() == "environment") {
                monitortype = $("#hidENV_MONITOR_TYPE").val();
                pointname = $("#ENV_POINT").val();
                datestart = $("#ENV_DATE_S").val();
                dateend = $("#ENV_DATE_E").val();
                itemid = $("#hidENV_ITEM").val();
                detailExcelWin = $.ligerDialog.open({ url: "SearchFunction/Print_MonitorData.aspx?type=Env&monitortype=" + monitortype + "&pointname=" + encodeURI(pointname) + "&datestart=" + datestart + "&dateend=" + dateend + "&itemid=" + itemid });
            }
            else {
                contracttype = $("#hidPOLL_CONTRACT_TYPE").val();
                companyname = $("#hidPOLL_COMPANY_NAME").val();
                monitortype = $("#hidPOLL_MONITOR_TYPE").val();
                pointname = $("#POLL_POINT").val();
                datestart = $("#POLL_DATE_S").val();
                dateend = $("#POLL_DATE_E").val();
                itemid = $("#hidPOLL_ITEM").val();
                detailExcelWin = $.ligerDialog.open({ url: "SearchFunction/Print_MonitorData.aspx?type=Poll&contracttype=" + contracttype + "&companyname=" + encodeURI(companyname) + "&monitortype=" + monitortype + "&pointname=" + encodeURI(pointname) + "&datestart=" + datestart + "&dateend=" + dateend + "&itemid=" + itemid });
            }
        }
    }
    );

    $("#ddlType").ligerComboBox({
        width: "130",
        data: [
                    { text: '环境质量', id: 'environment' },
                    { text: '污染源', id: 'pollution' }
                ],
        valueFieldID: 'hidType',
        initValue: 'environment'
    });
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../Mis/Contract/MethodHander.ashx?action=GetMonitorType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
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

    //构建环境质量查询表单
    $("#EnvSrhForm").ligerForm({
        inputWidth: 170, labelWidth: 60, space: 40, labelAlign: 'right',
        fields: [
                     { display: "监测类型", name: "ENV_MONITOR_TYPE", newline: true, type: "select", comboboxName: "ddlENV_MONITOR_TYPE", options: { valueFieldID: "hidENV_MONITOR_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", resize: false, url: "../../Mis/Contract/MethodHander.ashx?action=GetDict&type=EnvTypes", initValue: "EnvRiver" }, group: "基本信息", groupicon: groupicon },
                     { display: "点位", name: "ENV_POINT", width: 420, newline: true, type: "select" },
                     { display: "日期范围", name: "ENV_DATE_S", newline: true, type: "date" },
                     { display: "至", labelWidth: 40, name: "ENV_DATE_E", newline: false, type: "date" },
                     { display: "监测项目", name: "ENV_ITEM", width: 420, newline: true, type: "select" },
                     { name: "hidENV_ITEM", type: "hidden" }
                ]
    });
    //下拉框联动
    $.ligerui.get("ddlENV_MONITOR_TYPE").bind("selected", function () {
        $("#ENV_POINT").val('');
        $("#ENV_ITEM").val('');
        $("#hidENV_ITEM").val('');
    });
    $("#ENV_POINT").ligerComboBox({
        onBeforeOpen: EnvPoint_select, valueFieldID: 'hidENV_POINT'
    });
    $("#ENV_ITEM").ligerComboBox({
        onBeforeOpen: EnvItem_select, valueFieldID: 'hidENV_ITEM'
    });

    //构建污染源查询表单
    $("#PollSrhForm").ligerForm({
        inputWidth: 170, labelWidth: 60, space: 40, labelAlign: 'right',
        fields: [
                     { display: "委托类型", name: "POLL_CONTRACT_TYPE", newline: true, type: "select", comboboxName: "ddlPOLL_CONTRACT_TYPE", options: { valueFieldID: "hidPOLL_CONTRACT_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", resize: false, url: "../../Mis/Contract/MethodHander.ashx?action=GetDict&type=Contract_type", initValue: "01" }, group: "基本信息", groupicon: groupicon },
                     { display: "企业名称", name: "POLL_COMPANY_NAME", width: 200, newline: false, type: "text" },
                     { name: "hidPOLL_COMPANY_NAME", type: "hidden" },
                     { display: "监测类型", name: "POLL_MONITOR_TYPE", newline: true, type: "select", comboboxName: "ddlPOLL_MONITOR_TYPE", options: { valueFieldID: "hidPOLL_MONITOR_TYPE", valueField: "ID", textField: "MONITOR_TYPE_NAME", resize: false, data: vMonitorType, initValue: "000000001"} },
                     { display: "点位", name: "POLL_POINT", width: 470, newline: true, type: "select" },
                     { display: "日期范围", name: "POLL_DATE_S", newline: true, type: "date" },
                     { display: "至", labelWidth: 40, name: "POLL_DATE_E", newline: false, type: "date" },
                     { display: "监测项目", name: "POLL_ITEM", width: 470, newline: true, type: "select" },
                     { name: "hidPOLL_ITEM", type: "hidden" }
                ]
    });
    //下拉框联动
    $.ligerui.get("ddlPOLL_MONITOR_TYPE").bind("selected", function () {
        $("#POLL_ITEM").val('');
        $("#hidPOLL_ITEM").val('');
    });
    $("#POLL_COMPANY_NAME").unautocomplete();
    $("#POLL_COMPANY_NAME").autocomplete("../../Mis/Contract/MethodHander.ashx?action=GetCompanyInfo",
    {
        max: 20,     // 列表里的条目数 
        minChars: 0,     // 自动完成激活之前填入的最小字符 
        matchContains: true,     // 包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示 
        autoFill: false,     // 自动填充 
        width: 250,
        dataType: "json",
        scrollHeight: 150,
        parse: function (data) {

            return $.map(eval(data), function (row) {
                return {
                    data: row,
                    value: row.ID + " <" + row.COMPANY_NAME + ">",
                    result: row.COMPANY_NAME
                }
            });
        },
        formatItem: function (item) {
            //return "<font color=green>" + item.ID + "</font>&nbsp;(" + item.COMPANY_NAME + ")";
            //下拉显示的信息
            return item.COMPANY_NAME;
        }
    }
    );
    $("#POLL_COMPANY_NAME").result(findValueCallback); //加这个主要是联动显示id
    function findValueCallback(event, data, formatted) {
        $("#hidPOLL_COMPANY_NAME").val(data["ID"]); //获取选择的ID
        $("#POLL_POINT").val("");
    }

    $("#POLL_POINT").ligerComboBox({
        onBeforeOpen: PollPoint_select, valueFieldID: 'hidPOLL_POINT'
    });
    $("#POLL_ITEM").ligerComboBox({
        onBeforeOpen: PollItem_select, valueFieldID: 'hidPOLL_ITEM'
    });

    maingrid = $("#firstgrid").ligerGrid({
        columns: [],
        title: '',
        width: '99%',
        height: '99%',
        pageSizeOptions: [10, 15, 20, 30],
        pageSize: 20,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: false,
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

//设置grid 的弹出环境质量查询对话框
var detailEnvSrh = null;
function showEnvSrh() {
    if (detailEnvSrh) {
        detailEnvSrh.show();
    }
    else {
        detailWinSrh = $.ligerDialog.open({
            target: $("#divEnvSrh"),
            width: 550, height: 220, top: 90, title: "环境质量监测数据查询",
            buttons: [
                  { text: '确定', onclick: function () {
                      searchEnvData();
                      detailWinSrh.hide();
                  }
                  },
                  { text: '取消', onclick: function () { detailWinSrh.hide(); } }
                  ]
        });
    }
}
//设置grid 的弹出污染源查询对话框
var detailPollSrh = null;
function showPollSrh() {
    if (detailPollSrh) {
        detailPollSrh.show();
    }
    else {
        detailPollSrh = $.ligerDialog.open({
            target: $("#divPollSrh"),
            width: 600, height: 250, top: 90, title: "污染源监测数据查询",
            buttons: [
                  { text: '确定', onclick: function () {
                      if ($("#POLL_COMPANY_NAME").val() == "" || $("#hidPOLL_COMPANY_NAME").val() == "") {
                          $.ligerDialog.warn('请先选择企业信息！');
                          return;
                      }
                      searchPollData();
                      detailPollSrh.hide();
                  } 
                  },
                  { text: '取消', onclick: function () { detailPollSrh.hide(); } }
                  ]
        });
    }
}

function search() {
    var SEA_PROJECT_NAME = encodeURI($("#SEA_PROJECT_NAME1").val());
    var SEA_CONTRACT_CODE = encodeURI($("#SEA_CONTRACT_CODE1").val());
    var SEA_CONTRACT_YEAR_OP = encodeURI($("#SEA_CONTRACT_YEAR_BOX1").val());
    var SEA_CONTRACT_TYPE_OP = $("#SEA_CONTRACT_TYPE_OP1").val();
    var SEA_TEST_COMPANYNAME = encodeURI($("#SEA_TEST_COMPANYNAME1").val());
    var SEA_AREA = $("#SEA_AREA_OP1").val();
    var SEA_PLANDATE = $("#SEA_PLANDATE1").val();
    var SEA_TASK_CODE = encodeURI($("#SEA_TASK_CODE").val());
    var url = "PlanTaskSearch.aspx?action=GetPendingPlanList&strType=true&strProjectName=" + SEA_PROJECT_NAME + "&strContractCode=" + SEA_CONTRACT_CODE + "&strContratYear=" + SEA_CONTRACT_YEAR_OP + "&strContractType=" + SEA_CONTRACT_TYPE_OP + "&strCompanyNameFrim=" + SEA_TEST_COMPANYNAME + "&strAreaIdFrim=" + SEA_AREA + "&strDate=" + SEA_PLANDATE + "&strTaskCode=" + SEA_TASK_CODE;
    maingrid.set('url', url)
}

function clearSearchDialogValue() {
    $("#SEA_PROJECT_NAME1").val("");
    $("#SEA_CONTRACT_CODE1").val("");
    $("#SEA_TEST_COMPANYNAME1").val("");
    $("#SEA_PLANDATE1").val("");
    $("#SEA_CONTRACT_YEAR_BOX1").ligerGetComboBoxManager().setValue("");
    $("#SEA_CONTRACT_TYPE_BOX1").ligerGetComboBoxManager().setValue("");
    $("#SEA_AREA_BOX1").ligerGetComboBoxManager().setValue("");
}

//弹出环境质量点位grid
function EnvPoint_select() {
    $.ligerDialog.open({ title: '选择点位', name: 'winselector', width: 700, height: 370, url: 'SelectPoint.aspx?MONITOR_TYPE=' + $("#hidENV_MONITOR_TYPE").val(), buttons: [
                { text: '确定', onclick: EnvPoint_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
    });
    return false;
}
//环境质量点位弹出grid ok按钮
function EnvPoint_selectOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();

    $("#ENV_POINT").val(data);
    dialog.close();
}

//弹出环境质量监测项目grid
function EnvItem_select() {
    $.ligerDialog.open({ title: '选择监测项目', top: 260, width: 440, height: 380, buttons:
        [{ text: '确定', onclick: EnvItem_selectOK },
         { text: '返回', onclick: selectCancel
         }], url: '../../Mis/Contract/ProgramInforDetail/ContractPointItems.aspx?MonitorTypeId=' + $("#hidENV_MONITOR_TYPE").val()
    });
    return false;
}
//通过获取弹出窗口页面中 获取监测项目右侧ListBox集合以及移除的监测项目
function EnvItem_selectOK(item, dialog) {
    var fn = dialog.frame.GetSelectItemNames || dialog.frame.window.GetSelectItemNames;
    var strItemNames = fn();

    var fn1 = dialog.frame.GetSelectItems || dialog.frame.window.GetSelectItems;
    var strItemIds = fn1();

    $("#hidENV_ITEM").val(strItemIds);
    $("#ENV_ITEM").val(strItemNames);
    dialog.close();
}

//弹出污染源企业点位grid
function PollPoint_select() {
    if ($("#POLL_COMPANY_NAME").val() == "" || $("#hidPOLL_COMPANY_NAME").val() == "") {
        $.ligerDialog.warn('请先选择企业信息！');
        return false;
    }
    $.ligerDialog.open({ title: '选择点位', name: 'winselector', width: 700, height: 370, url: 'SelectCompanyPoint.aspx?COMPANY_ID=' + $("#hidPOLL_COMPANY_NAME").val(), buttons: [
                { text: '确定', onclick: PollPoint_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
    });
    return false;
}
//污染源企业点位弹出grid ok按钮
function PollPoint_selectOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();

    $("#POLL_POINT").val(data);
    dialog.close();
}

//弹出污染源企业监测项目grid
function PollItem_select() {
    $.ligerDialog.open({ title: '选择监测项目', top: 260, width: 440, height: 380, buttons:
        [{ text: '确定', onclick: PollItem_selectOK },
         { text: '返回', onclick: selectCancel
         }], url: '../../Mis/Contract/ProgramInforDetail/ContractPointItems.aspx?MonitorTypeId=' + $("#hidPOLL_MONITOR_TYPE").val()
    });
    return false;
}
//通过获取弹出窗口页面中 获取监测项目右侧ListBox集合以及移除的监测项目
function PollItem_selectOK(item, dialog) {
    var fn = dialog.frame.GetSelectItemNames || dialog.frame.window.GetSelectItemNames;
    var strItemNames = fn();

    var fn1 = dialog.frame.GetSelectItems || dialog.frame.window.GetSelectItems;
    var strItemIds = fn1();

    $("#hidPOLL_ITEM").val(strItemIds);
    $("#POLL_ITEM").val(strItemNames);
    dialog.close();
}

function selectCancel(item, dialog) {
    dialog.close();
}

//查询获取环境质量监测数据
function searchEnvData() {
    var monitortype = $("#hidENV_MONITOR_TYPE").val();
    var pointname = $("#ENV_POINT").val();
    var datestart = $("#ENV_DATE_S").val();
    var dateend = $("#ENV_DATE_E").val();
    var itemid = $("#hidENV_ITEM").val();
    
    $.ajax({
        url: "MonitorDataSearch.aspx",
        data: "type=GetEnvData&monitortype=" + monitortype + "&pointname=" + pointname + "&datestart=" + datestart + "&dateend=" + dateend + "&itemid=" + itemid,
        type: "post",
        dataType: "json",
        async: true,
        cache: false,
        beforeSend: function () {
            $.ligerDialog.waitting('数据加载中,请稍候...');
        },
        complete: function () {
            $.ligerDialog.closeWaitting();
        },
        success: function (json) {
            if (parseInt(json.Total) > 0) {
                gridJSON = json;

                //构建表格列
                //固定的列
                var columnsArr = [];

                //添加所有动态的列
                $.each(json.UnSureColumns, function (i, n) {
                    columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth), minWidth: 60, align: "center" });
                });

                maingrid.set("columns", columnsArr);
                maingrid.set("data", json);

                //隐藏不需要显示的列
                maingrid.toggleCol("ID");

                //$(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
            }
            else {
                maingrid.set("data", json);
            }
        }
    });
}

//查询获取污染源企业监测数据
function searchPollData() {
    
    var contracttype = $("#hidPOLL_CONTRACT_TYPE").val();
    var companyname=$("#hidPOLL_COMPANY_NAME").val();
    var monitortype = $("#hidPOLL_MONITOR_TYPE").val();
    var pointname = $("#POLL_POINT").val();
    var datestart = $("#POLL_DATE_S").val();
    var dateend = $("#POLL_DATE_E").val();
    var itemid = $("#hidPOLL_ITEM").val();

    $.ajax({
        url: "MonitorDataSearch.aspx",
        data: "type=GetPollData&contracttype=" + contracttype + "&companyname=" + companyname + "&monitortype=" + monitortype + "&pointname=" + pointname + "&datestart=" + datestart + "&dateend=" + dateend + "&itemid=" + itemid,
        type: "post",
        dataType: "json",
        async: true,
        cache: false,
        beforeSend: function () {
            $.ligerDialog.waitting('数据加载中,请稍候...');
        },
        complete: function () {
            $.ligerDialog.closeWaitting();
        },
        success: function (json) {
            if (parseInt(json.Total) > 0) {
                gridJSON = json;

                //构建表格列
                //固定的列
                var columnsArr = [];

                //添加所有动态的列
                $.each(json.UnSureColumns, function (i, n) {
                    columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth)+100, minWidth: 60, align: "center" });
                });

                maingrid.set("columns", columnsArr);
                maingrid.set("data", json);

                //隐藏不需要显示的列
                maingrid.toggleCol("ID");

                //$(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
            }
            else {
                maingrid.set("data", json);
            }
        }
    });
}