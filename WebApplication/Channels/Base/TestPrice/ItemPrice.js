// Create by 潘德军 2012.11.05  "监测项目单价设置"功能

var manager;
var menu;
var actionID, actionItemName, actionCharge, actionPowerFee, actionPretreatmentFree, actionTestAnsyFree, actionTestPointNum, actionAnsyNum;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    $("#layout1").ligerLayout({ height: '100%' });

    //质控设置菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: click_OfMenu, icon: 'modify' }
            ]
    });

    //grid
    window['g'] =
    manager = $("#maingridItem").ligerGrid({
        columns: [
        { display: '监测类型', name: 'MONITOR_NAME', width: 200, align: 'left', isSort: false },
        { display: '监测项目', name: 'ITEM_NAME', width: 300, align: 'left', isSort: false },
        { display: '前处理费', name: 'PRETREATMENT_FEE', width: 150, align: 'left', isSort: false },
        { display: '测试分析费', name: 'TEST_ANSY_FEE', width: 150, align: 'left', isSort: false },
        { display: '测点数', name: 'TEST_POINT_NUM', width: 150, align: 'left', isSort: false },
        { display: '分析批数', name: 'ANALYSE_NUM', width: 150, align: 'left', isSort: false },
        { display: '监测(采样)单价', name: 'CHARGE', width: 150, align: 'left', isSort: false },
        { display: '开机费用', name: 'TEST_POWER_FEE', width: 150, align: 'left', isSort: false }
        ], width: '100%', height: '100%', pageSizeOptions: [10, 15, 20, 50],
        url: 'ItemPrice.aspx?Action=GetItem',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'modify', text: '修改', click: click_OfToolbar, icon: 'modify' },
                { line: true },
                { id: 'srh', text: '查询', click: click_OfToolbar, icon: 'search' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            actionID = parm.data.ID;
            actionItemName = parm.data.ITEM_NAME;
            actionCharge = parm.data.CHARGE;
            actionPowerFee = parm.data.TEST_POWER_FEE;
            actionPretreatmentFree = param.data.PRETREATMENT_FEE;
            actionTestAnsyFree = param.data.TEST_ANSY_FEE;
            actionTestPointNum = param.data.TEST_POINT_NUM;
            actionAnsyNum = param.data.ANALYSE_NUM;
            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetail({
                ItemID: data.ID,
                ITEM_NAME: data.ITEM_NAME,
                CHARGE: data.CHARGE,
                TEST_POWER_FEE: data.TEST_POWER_FEE,
                PRETREATMENT_FEE: data.PRETREATMENT_FEE,
                TEST_ANSY_FEE: data.TEST_ANSY_FEE,
                TEST_POINT_NUM: data.TEST_POINT_NUM,
                ANALYSE_NUM: data.ANALYSE_NUM
            });
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});

//toolbar click
function click_OfToolbar(item) {
    switch (item.id) {
        case 'modify':
            var selected = manager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要编辑的记录！'); ; return }

            showDetail({
                ItemID: selected.ID,
                ITEM_NAME: selected.ITEM_NAME,
                CHARGE: selected.CHARGE,
                TEST_POWER_FEE:selected.TEST_POWER_FEE,
                PRETREATMENT_FEE: selected.PRETREATMENT_FEE,
                TEST_ANSY_FEE: selected.TEST_ANSY_FEE,
                TEST_POINT_NUM: selected.TEST_POINT_NUM,
                ANALYSE_NUM: selected.ANALYSE_NUM
            });

            break;
        case 'srh':
            showDetailSrh();
            break;
        default:
            break;
    }
}

//右键菜单
function click_OfMenu(item) {
    switch (item.id) {
        case 'menumodify':
            showDetail({
                ItemID: actionID,
                ITEM_NAME: actionItemName,
                CHARGE: actionCharge,
                TEST_POWER_FEE: actionPowerFee,
                PRETREATMENT_FEE: actionPretreatmentFree,
                TEST_ANSY_FEE: actionTestAnsyFree,
                TEST_POINT_NUM: actionTestPointNum,
                ANALYSE_NUM: actionAnsyNum
            }, false);

            break;
        default:
            break;
    }
}

//弹出编辑对话框
var detailWin = null, curentData = null;
function showDetail(data) {
    curentData = data;
    if (detailWin) {
        detailWin.show();
    }
    else {
        //创建表单结构
        var mainform = $("#editForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "监测项目", name: "ItemID", newline: true, type: "select", comboboxName: "ItemID1", group: "基本信息", groupicon: groupicon, options: { valueFieldID: "ItemID", url: "../MonitorType/Select.ashx?view=T_BASE_ITEM_INFO&idfield=ID&textfield=ITEM_NAME&where=is_del|0"} },
                      { display: "前处理费", name: "PRETREATMENT_FEE", newline: false, type: "text" },
                      { display: "测试分析费", name: "TEST_ANSY_FEE", newline: true, type: "text" },
                      { display: "测点数", name: "TEST_POINT_NUM", newline: false, type: "text" },
                      { display: "分析批数", name: "ANALYSE_NUM", newline: true, type: "text" },
                      { display: "监测单价", name: "CHARGE", newline: false, type: "text" },
                      { display: "开机费用", name: "TEST_POWER_FEE", newline: true, type: "text" }
                    ]
        });
        $("#PRETREATMENT_FEE").val("0");
        $("#TEST_ANSY_FEE").val("0");
        $("#TEST_POINT_NUM").val("1");
        $("#ANALYSE_NUM").val("1");
        $("#CHARGE").val("0");
        $("#TEST_POWER_FEE").val("0");

//        $("#PRETREATMENT_FEE").attr("style", "background-image:url(../../../Images/Icons/money_yen.png);background-position:left bottom;background-repeat: no-repeat; padding-left:15px;");
//        $("#CHARGE").attr("style", "background-image:url(../../../Images/Icons/money_yen.png);background-position:left bottom;background-repeat: no-repeat; padding-left:15px;");
//        $("#TEST_POWER_FEE").attr("style", "background-image:url(../../../Images/Icons/money_yen.png);background-position:left bottom;background-repeat: no-repeat; padding-left:15px;");
//        $("#TEST_ANSY_FEE").attr("style", "background-image:url(../../../Images/Icons/money_yen.png);background-position:left bottom;background-repeat: no-repeat; padding-left:15px");
//        //add validate by ssz
        $("#CHARGE").attr("validate", "[{required:true,msg:'请填写监测单价'},{maxlength:64,msg:'监测单价最大长度为64'}]");
        $("#TEST_POWER_FEE").attr("validate", "[{maxlength:8,msg:'开机费用最大长度为8'}]");

        detailWin = $.ligerDialog.open({
            target: $("#detailEdit"),
            width: 600, height: 260, top: 90, title: "项目费用信息",
            buttons: [
                  { text: '确定', onclick: function () { save(); } },
                  { text: '取消', onclick: function () { clearDialogValue(); detailWin.hide(); } }
                  ]
        });
    }
    if (curentData) {
        $("#ItemID").val(curentData.ItemID);
        $("#ItemID1").ligerGetComboBoxManager().setValue(curentData.ItemID);
        $("#ItemID1").ligerGetComboBoxManager().setDisabled();

        if (curentData.CHARGE != "") {
            $("#CHARGE").val(curentData.CHARGE);
        }
        if (curentData.TEST_POWER_FEE!="") {
            $("#TEST_POWER_FEE").val(curentData.TEST_POWER_FEE);
        }
        if (curentData.PRETREATMENT_FEE != "") {
            $("#PRETREATMENT_FEE").val(curentData.PRETREATMENT_FEE);
        }
        if (curentData.TEST_ANSY_FEE) {
            $("#TEST_ANSY_FEE").val(curentData.TEST_ANSY_FEE);
        }
        if (curentData.TEST_POINT_NUM) {
            $("#TEST_POINT_NUM").val(curentData.TEST_POINT_NUM);
        }
        if (curentData.ANALYSE_NUM != "") {
            $("#ANALYSE_NUM").val(curentData.ANALYSE_NUM);
        }
    }
//    $('#CHARGE').formatCurrency();
//    $('#TEST_POWER_FEE').formatCurrency();
//    $('#PRETREATMENT_FEE').formatCurrency();
//    $('#TEST_ANSY_FEE').formatCurrency();

    function save() {
        //验证表单
        if (!$("#editForm").validate())
            return false;
        var strData = "{";
        strData += "'strItemID':'" + $("#ItemID").val() + "',";
        strData += "'strCharge':'" + $("#CHARGE").val() + "',";
        strData += "'strPowerFee':'" + $("#TEST_POWER_FEE").val() + "',";
        strData += "'strPreFree':'" + $("#PRETREATMENT_FEE").val() + "',";
        strData += "'strTestAnsyFree':'" + $("#TEST_ANSY_FEE").val() + "',";
        strData += "'strTestPointNum':'" + $("#TEST_POINT_NUM").val() + "',";
        strData += "'strAnsyNum':'" + $("#ANALYSE_NUM").val() + "'";
        strData += "}";

        $.ajax({
            cache: false,
            type: "POST",
            url: "ItemPrice.aspx/EditItem",
            data: strData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    detailWin.hidden();
                    manager.loadData();
                    clearDialogValue();
                }
                else {
                    $.ligerDialog.warn('保存项目数据失败！');
                }
            }
        });
    }
}

//弹出编辑对话框 清空
function clearDialogValue() {
    $("#ItemID").val("");
    $("#ItemID1").val("");

    $("#PRETREATMENT_FEE").val("0");
    $("#TEST_ANSY_FEE").val("0");
    $("#TEST_POINT_NUM").val("5");
    $("#ANALYSE_NUM").val("1");

    $("#CHARGE").val("0");
    $("#TEST_POWER_FEE").val("0");
}

//弹出查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#SrhForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "监测类型", name: "SrhMONITOR_ID", newline: true, type: "select", comboboxName: "SrhMONITOR_TYPE_ID", group: "查询信息", groupicon: groupicon, options: { valueFieldID: "SrhMONITOR_ID", url: "../MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} },
                      { display: "监测项目", name: "SrhITEM_NAME", newline: true, type: "text", validate: { required: true, minlength: 3} }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 350, height: 200, top: 90, title: "查询监测项目",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhMONITOR_ID = $("#SrhMONITOR_ID").val();
        var SrhITEM_NAME = escape($("#SrhITEM_NAME").val());

        manager.set('url', "ItemPrice.aspx?Action=GetItem&SrhMONITOR_ID=" + SrhMONITOR_ID + "&SrhITEM_NAME=" + SrhITEM_NAME);
    }
}

//质控设置grid 的弹出查询对话框 清空
function clearSearchDialogValue() {
    $("#SrhMONITOR_ID").val("");
    $("#SrhMONITOR_TYPE_ID").val("");
    $("#SrhITEM_NAME").val("");
}