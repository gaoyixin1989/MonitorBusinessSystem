//Create By  胡方扬  监测费用明细列表
var ItemList = null, AttItemList = null, vSumList = null;
var objTool = null, isEdit = false;
var view = false;
var strAtt_item_Id = "", strAttFeeId = "", strAttFee = "0", strAttPointNum = "1";
var curPane = "tip1";
var pdata = [
{ id: '是' },
{ id: '否' }
];
var gdata = [
{ id: '01', name: '地面水/表层（距水面≤0.5米）' },
{ id: '02', name: '地面水/中下层（距水面＞0.5米）' },
{ id: '03', name: '样品03' },
{ id: '04', name: '样品04' },
{ id: '05', name: '样品05' },
{ id: '06', name: '样品06' },
{ id: '07', name: '样品07' }
]; 
function show(switchSysBar) {
    if (switchSysBar == curPane) { return; }
    document.getElementById(curPane).style.display = "none";
    document.getElementById(switchSysBar).style.display = "block";
    curPane = switchSysBar;
}
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
$(document).ready(function () {
    $("#layout1").ligerLayout({ topHeight: 220, leftWidth: "65%", rightWidth: "35%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });
    //获取URL 参数
    $("#Income").bind("blur", function () {
        $('#Income').formatCurrency();
    })
    var strContratId = $.getUrlVar('strContractId'); //$.query.get('standartId');
    var strurlMonitorTypeId = $.getUrlVar('strMonitorType');
    var strIsView = $.getUrlVar('isView');
    var strContractCode = $.getUrlVar('strContractCode');

    if (strIsView == 'true') {
        view = true;
        setDisabled();
    }

    if (view == false) {
        //设置显示操作按钮
        objTool = { items: [
                { id: 'setting', text: '选择附加项目', click: toolBarClick, icon: 'database_wrench' },
                { line: true },
                { id: 'add', text: '增加', click: toolBarClick, icon: 'add' },
                { line: true },
                { id: 'del', text: '删除', click: toolBarClick, icon: 'delete' }
                ]
        };
        //设置可修改
        isEdit = true;
    }
    // 获取监测项目列表
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        //url: "../MethodHander.ashx?action=GetItemList&strMonitroType=" + strurlMonitorTypeId + "",
        url: "../MethodHander.ashx?action=GetItemList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Rows != null) {
                ItemList = data.Rows;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    function GetAttItemList() {
        // 获取附加费用列表
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../MethodHander.ashx?action=GetAttItemList",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Rows != null) {
                    AttItemList = data.Rows;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    GetAttItemList();
    GetContractConstFeeDetail();
    GetAttFeeDetail();
    //填充布局
    //gridDraggable(g1, g2);
    SetTextValueConst();
    function GetContractConstFeeDetail() {
        window['g1'] =
    manager1 = $("#divstestfree").ligerGrid({

        columns: [
//                { display: '序号', name: 'name', align: 'left', width: 50, data: gdata, render: function (item) {
//                    for (var i = 0; i < ItemList.length; i++) {
//                        if (ItemList[i]['ID'] == item.TEST_ITEM_ID)
//                            return i;
//                    }
//                    return item.TEST_ITEM_ID; //POINT_NAME
//                }
        //                },
                {display: '序号', name: 'ID', minWidth: 50 },
                { display: '类型', name: 'MONITOR_TYPE_NAME', minWidth: 100 },
                { display: '点位', name: 'SAMPLE_NAME', minWidth: 160 },
                { display: '采样项目', name: 'PRETREATMENT_FEE', minWidth: 200, editor: { type: 'select', data: gdata, isShowCheckBox: true, valueField: 'name', textField: 'name'} },
        //      { display: '计算单位', name: 'TEST_ANSY_FEE', minWidth: 100, editor: { type: 'text'} },
                {display: '收费标准(元)', name: 'TEST_NUM', minWidth: 100, editor: { type: 'text'} },
        //                { display: '分析批数', name: 'FREQ', minWidth: 120 },
        //                { display: '采样费', name: 'TEST_PRICE', minWidth: 100 },
        //                { display: '开机费', name: 'TEST_POWER_PRICE', minWidth: 100 },
        //                { display: '测点数', name: 'TEST_POINT_NUM', minWidth: 100, editor: { type: 'text'} },
                {display: '小计', name: 'FEE_COUNT', minWidth: 40 }
                ], rownumbers: true,
        width: '98%',
        height: 220,
        pageSizeOptions: [5, 10, 15, 20],
        url: '../MethodHander.ashx?action=GetContractConstFeeDetail&strContratId=' + strContratId + '',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        enabledEdit: isEdit,
        onAfterEdit: f_onAfterEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
        function f_onAfterEdit(e) {
            if (isNaN(e.record.TEST_NUM)) {
                //  $.ligerDialog.warn("请输入有效的数字");
                return;
            }

            if (isNaN(e.record.TEST_POINT_NUM)) {
                $.ligerDialog.warn("请输入有效的数字");
                return;
            }
            if (isNaN(e.record.TEST_ANSY_FEE)) {
                $.ligerDialog.warn("请输入有效的数字");
                return;
            }
            manager1.updateCell('TEST_NUM', e.record.TEST_NUM, e.record);
            manager1.updateCell('TEST_POINT_NUM', e.record.TEST_POINT_NUM, e.record);
            manager1.updateCell('TEST_ANSY_FEE', e.record.TEST_ANSY_FEE, e.record);
            UpdateTestFree(e.record.ID, e.record.TEST_NUM, "", "", e.rowindex);
            UpdateTestFree(e.record.ID, "", e.record.TEST_POINT_NUM, "", e.rowindex);
            UpdateTestFree(e.record.ID, "", "", e.record.TEST_ANSY_FEE, e.rowindex);
        }
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    }

    function UpdateTestFree(strid, strTestNum, strTestPointNum, strTestAnsyFee, GridIndex) {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../MethodHander.ashx?action=UpdateTestFree&strFeeId=" + strid + "&strTestNum=" + strTestNum + "&strTestPointNum=" + strTestPointNum + "&strTestAnsyFee=" + strTestAnsyFee,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    manager1.cancelEdit(GridIndex);
                    manager1.loadData();
                    SetTextValueConst();
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求数据失败！');
            }
        });
    }
    function GetAttFeeDetail() {
        window['g2'] =
    manager2 = $("#divattfreelist").ligerGrid({
        columns: [
                { display: '附加项目', name: 'ATT_FEE_ITEM_ID', align: 'left', width: 120, data: AttItemList, render: function (item) {
                    for (var i = 0; i < AttItemList.length; i++) {
                        if (AttItemList[i]['ID'] == item.ATT_FEE_ITEM_ID)
                            return AttItemList[i]['ATT_FEE_ITEM'];
                    }
                    return item.TEST_ITEM_ID;
                }
                },
                { display: '费用', name: 'FEE', minWidth: 100, editor: { type: 'text'} }
        //{ display: '描述', name: 'INFO', align: 'left', minWidth: 120 }
                ],
        width: '100%',
        height: '98%',
        pageSizeOptions: [5, 10, 15, 20],
        url: '../MethodHander.ashx?action=GetAttFeeDetail&strContratId=' + strContratId + '',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        toolbar: objTool,
        alternatingRow: false,
        enabledEdit: isEdit,
        checkbox: true,
        rownumbers: true,
        onAfterEdit: f_onAfterEdit,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        }
        //onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    }
    objOneGrid = $("#oneGrid").ligerGrid({

        columns: [
        //                { display: '序号', name: 'TEST_ITEM_ID', align: 'left', width: 50, data: ItemList, render: function (item) {
        //                    for (var i = 0; i < ItemList.length; i++) {
        //                        if (ItemList[i]['ID'] == item.TEST_ITEM_ID)
        //                            return i;
        //                    }
        //                    return item.TEST_ITEM_ID; //POINT_NAME
        //                }
        //                },
                {display: '序号', name: 'ID', minWidth: 50 },
                {display: '类型', name: 'MONITOR_TYPE_NAME', minWidth: 100 },
                { display: '样品名称', name: 'SAMPLE_NAME', minWidth: 160 },
                { display: '监测项目', name: 'ITEM_NAME', minWidth: 160 },
                { display: '监测方法', name: 'TEST_ANSY_FEE', minWidth: 100, editor: { type: 'text'} },
                { display: '大型仪器开机费', name: 'TEST_NUM', minWidth: 100, editor: { type: 'select', data: pdata, isShowCheckBox: true, valueField: 'id', textField: 'id'} },
                { display: '收费标准(元)', name: 'FREQ', minWidth: 120 },
        //              { display: '采样费', name: 'TEST_PRICE', minWidth: 100 },
        //              { display: '开机费', name: 'TEST_POWER_PRICE', minWidth: 100 },
        //              { display: '测点数', name: 'TEST_POINT_NUM', minWidth: 100, editor: { type: 'text'} },
                {display: '小计', name: 'FEE_COUNT', minWidth: 40 }
                ], 
        width: '98%',
        height: 220,
        pageSizeOptions: [5, 10, 15, 20],
        url: '../MethodHander.ashx?action=GetContractConstFeeDetail&strContratId=' + strContratId + '',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,

        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        enabledEdit: isEdit,
        // onAfterEdit: f_onAfterEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    function f_onAfterEdit(e) {
        manager2.updateCell('FEE', e.record.FEE, e.record);
        strAttFeeId = e.record.ID
        strAtt_item_Id = e.record.ATT_FEE_ITEM_ID;
        strAttFee = e.record.FEE;
        updateAttFeeInfor();
    }
    function updateAttFeeInfor() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../MethodHander.ashx?action=UpdateAttFeeInfor&strContratId=" + strContratId + "&strAttFeeId=" + strAttFeeId + "&strAtt_item_Id=" + strAtt_item_Id + "&strAttFee=" + strAttFee,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    manager2.loadData();
                    SetTextValueConst();
                }
                else {
                    $.ligerDialog.warn('修改失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    function toolBarClick(item) {
        switch (item.id) {
            case 'setting':
                $.ligerDialog.open({ title: '附加项目设置', top: 60, width: 440, height: 400, buttons:
            [{ text: '确定', onclick: f_SaveDivItemData },
             { text: '返回', onclick: function (item, dialog) { dialog.close(); }
             }], url: 'ContractAttFeeItems.aspx?strContratId=' + strContratId
                });
                break;
            case 'add':
                showDetailSrh(false);
                break;
            case 'del':
                var rowselected = manager2.getSelectedRow();
                if (rowselected == null) {
                    $.ligerDialog.warn('请选择一条记录'); return;
                }
                strAttFeeId = rowselected.ID;
                f_DelAttFeeItems();
                break;
            default:
                break;
        }
    }

    //通过获取弹出窗口页面中 获取附加项目右侧ListBox集合以及移除的附加项目
    function f_SaveDivItemData(item, dialog) {
        var fn = dialog.frame.GetMoveItems || dialog.frame.window.GetMoveItems;
        var strMovedata = fn();

        var fn1 = dialog.frame.GetSelectItems || dialog.frame.window.GetSelectItems;
        var strSelectData = fn1();

        if (strSelectData == "" & strMovedata == "") {
            return;
        }
        else {
            SaveDivItemData(strSelectData, strMovedata)
            dialog.close();
        }
    }

    //保存监测项目
    function SaveDivItemData(strSelectData, strMovedata) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../MethodHander.ashx?action=SaveDivAttItemData&strContratId=" + strContratId + "&strAttAddItemsId=" + strSelectData + "&strAttMoveItemsId=" + strMovedata,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    manager2.loadData();
                    SetTextValueConst();
                    parent.$.ligerDialog.success('数据保存成功！');
                }
                else {
                    parent.$.ligerDialog.warn('数据操作失败！');
                }
            },
            error: function () {
                parent.$.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }

    //计算合计费用
    function SetTextValueConst() {

        // 获取监测费用总计
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../MethodHander.ashx?action=GetConstractFeeCount&strContratId=" + strContratId + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vSumList = data.Rows;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
        $("#Test_Fee").attr("style", "background-image:url(../../../../Images/Icons/money_yen.png);background-position:left bottom;background-repeat: no-repeat; padding-left:15px;background-color:#E0E0E0;");
        $("#AttFee").attr("style", "background-image:url(../../../../Images/Icons/money_yen.png);background-position:left bottom;background-repeat: no-repeat; padding-left:15px;background-color:#E0E0E0;");
        $("#Budget").attr("style", "background-image:url(../../../../Images/Icons/money_yen.png);background-position:left bottom;background-repeat: no-repeat; padding-left:15px;background-color:#E0E0E0;");
        $("#Income").attr("style", "background-image:url(../../../../Images/Icons/money_yen.png);background-position:left bottom;background-repeat: no-repeat; padding-left:15px");
        //设置部分只读
        $("#Test_Fee").attr("disabled", true);
        $("#AttFee").attr("disabled", true);
        $("#Budget").attr("disabled", true);
        //用ligerUI 文本框前加图片后 样式有问题
        //        $("#Test_Fee").ligerTextBox({disabled:true});
        //        $("#AttFee").ligerTextBox({ disabled: true });
        //        $("#Income").ligerTextBox({ number: true });
        if (vSumList != null) {
            if (vSumList[0].TEST_FEE != "") {
                $("#Test_Fee").val(vSumList[0].TEST_FEE);
            }
            else {
                $("#Test_Fee").val("0");
            }
            if (vSumList[0].ATT_FEE != "") {
                $("#AttFee").val(vSumList[0].ATT_FEE);
            }
            else {
                $("#AttFee").val("0");
            }
            if (vSumList[0].BUDGET != "") {
                $("#Budget").val(vSumList[0].BUDGET);
            }
            else {
                $("#Budget").val("0");
            }
            if (vSumList[0].INCOME != "") {
                $("#Income").val(vSumList[0].INCOME);
            }
            else {
                $("#Income").val("0");
            }
        }
        //如果为空 全部默认填写0
        else {
            $("#Test_Fee").val('0');
            $("#AttFee").val('0');
            $("#Budget").val('0');
            $("#Income").val('0');
        }

        $('#Test_Fee').formatCurrency();
        $('#AttFee').formatCurrency();
        $('#Budget').formatCurrency();
        $('#Income').formatCurrency();

    }


    //设置grid 的弹出查询对话框
    var detailWinSrh = null;
    function showDetailSrh() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构
            var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
            var mainform = $("#SrhForm");
            mainform.ligerForm({
                inputWidth: 170, labelWidth: 90, space: 40,
                fields: [
                     { display: "附加项目名称", name: "SEA_ATT_FEE_ITEM", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "费用", name: "SEA_PRICE", newline: true, type: "text" },
                     { display: "项目描述", name: "SEA_INFO", newline: true, type: "text" }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 400, height: 220, top: 90, title: "新增附加项目",
                buttons: [
                  { text: '确定', onclick: function () { f_SaveDivAttItems(); clearDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }
    }

    function f_DelAttFeeItems() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../MethodHander.ashx?action=DelAttFeeItems&strAttFeeId=" + strAttFeeId,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据删除成功！');
                    manager2.loadData();
                    SetTextValueConst();
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    function f_SaveDivAttItems() {
        VailValue();
        // 获取监测费用总计
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../MethodHander.ashx?action=SaveDivAttItems&strAttItemName=" + encodeURI($("#SEA_ATT_FEE_ITEM").val()) + "&strAttFee=" + encodeURI($("#SEA_PRICE").val()) + "&strAttItemInfor=" + encodeURI($("#SEA_INFO").val()) + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    GetAttItemList();
                    manager2.loadData();
                    $.ligerDialog.success('数据保存成功！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    function clearDialogValue() {
        $("#SEA_ATT_FEE_ITEM").val("");
        $("#SEA_PRICE").val("");
        $("#SEA_INFO").val("");
    }

    function VailValue() {
        if ($("#SEA_ATT_FEE_ITEM").val() == "") {
            $.ligerDialog.warn('项目名称不能为空！');
            return;
        }
        if ($("#SEA_PRICE").val() == "") {
            $.ligerDialog.warn('项目名称不能为空！');
            return;
        }
    }

    function setDisabled() {
        //        $("#Test_Fee").ligerTextBox({ disabled: true });
        //        $("#AttFee").ligerTextBox({ disabled: true });
        //        $("#Budget").ligerTextBox({ disabled: true });
        //        $("#Income").ligerTextBox({ disabled: true });
        $("#Test_Fee").attr("disabled", true);
        $("#AttFee").attr("disabled", true);
        $("#Budget").attr("disabled", true);
        $("#Income").attr("disabled", true);
    }
})

function GetChildInputValue() {
    var strRequest = "";
    strRequest += "&strFeeTest_FeeSum=" + $("#Test_Fee").val().replace(",", "");
    strRequest += "&strFeeAtt_FeeSum=" + $("#AttFee").val().replace(",", "");
    strRequest += "&strBudGet=" + $("#Budget").val().replace(",", "");
    strRequest += "&strIncome=" + $("#Income").val().replace(",", "");
    return strRequest;
}

// Create by 熊卫华 2013.03.18  "分析室主任审核"功能
var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;
var objFourGrid = null;
var objFiveGrid = null;

var objOneGrid2 = null;
var objTwoGrid2 = null;
var objThreeGrid2 = null;
var objFourGrid2 = null;
var objFiveGrid2 = null;

var strUrl = "ContractConstDetail_QY.aspx";
var strOneGridTitle = "任务信息";
var strSubTaskID = "";

var selectTabId = "0";

//监测任务管理
$(document).ready(function () {
    $("#navtab1").ligerTab({ onAfterSelectTabItem: function (tabid) {
        tab = $("#navtab1").ligerGetTabManager();
        selecttabindex = tab.getSelectedTabItemID();
        changeTab(selecttabindex);
    }
    });
    //Tab标签切换事件
    function changeTab(tabid) {
        switch (tabid) {
            // 未确认                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
            case "tabitem1":
                selectTabId = "0";
               // manager1.set('url', strUrl + "?type=getOneGridInfo&IsDo=1");
                break;
            case "tabitem2":
                selectTabId = "1";
                //objOneGrid.set('url', strUrl + "?type=getOneGridInfo&IsDo=1");

                break;
        }
    }
    //待办
   
    //已办
    objOneGrid2 = $("#oneGrid2").ligerGrid({

        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        //checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '220px',
        //url: strUrl + '?type=getOneGridInfo&IsDo=1',
        columns: [
                 { display: '序号', name: 'TICKET_NUM', align: 'left', width: 50, minWidth: 60 },
                { display: '类型', name: 'CONTRACT_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '项目', name: 'CONTRACT_TYPE', width: 100, minWidth: 60, render: function (record) {
                    return getDictName(record.CONTRACT_TYPE, "Contract_Type");
                }
                },
                { display: '计算单位', name: 'PROJECT_NAME', width: 150, minWidth: 60 },
                { display: '收费标准（元）', name: 'TESTED_COMPANY_ID', width: 150, minWidth: 60, render: function (record) {
                    return getCompanyName(record.ID, record.TESTED_COMPANY_ID);
                }
                },
                { display: '数量', name: 'ASKING_DATE', width: 100, minWidth: 60 },
                { display: '小计', name: 'REMARK1', width: 180, minWidth: 60,
                    render: function (record, rowindex, value) {
                        return "<a href=\"javascript:showSuggestion('" + value + "')\">" + (value.length > 10 ? value.substring(0, 10) + "......" : value) + "</a> ";
                    }
                }
                ],
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载样品信息
            objTwoGrid2.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID + "&IsDo=1");
            objThreeGrid2.set("data", emptyArray);
            objFourGrid2.set("data", emptyArray);
            objFiveGrid2.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    //退回监测监测项目 
    function GoToBack() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('退回之前请先选择一条任务信息');
            return;
        }

        $.ligerDialog.prompt('退回意见', '', true, function (yes, value) {

            if (yes) {
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=GoToBack&strTaskId=" + objOneGrid.getSelectedRow().ID + "&strSuggestion=" + encodeURI(value),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data == "1") {
                            objOneGrid.loadData();
                            objTwoGrid.set("data", emptyArray);
                            objThreeGrid.set("data", emptyArray);
                            objFourGrid.set("data", emptyArray);
                            objFiveGrid.set("data", emptyArray);
                            $.ligerDialog.success('监测任务退回成功')
                        }
                        else {
                            $.ligerDialog.warn('监测任务退回失败');
                        }
                    }
                });
            }
        });
    }
    //发送到下一环节
    function SendToNext() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('发送之前请选择一条任务信息');
            return;
        }
        $.ligerDialog.confirm('您确认发送该任务至下一环节吗？', function (yes) {
            if (yes == true) {
                if (IsCanSendTaskQcToNextFlow(objOneGrid.getSelectedRow().ID) == "0") {
                    $.ligerDialog.warn('该任务项目正在执行中，不允许发送');
                    return;
                }
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=SendToNext&strTaskId=" + objOneGrid.getSelectedRow().ID,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.result == "1") {
                            objOneGrid.loadData();
                            objTwoGrid.set("data", emptyArray);
                            objThreeGrid.set("data", emptyArray);
                            objFourGrid.set("data", emptyArray);
                            objFiveGrid.set("data", emptyArray);
                            if (data.msg != "")
                                $.ligerDialog.success('任务发送成功，发送至【<a style="color:Red;font-weight:bold">' + data.msg + '</a>】环节');
                            else
                                $.ligerDialog.success('任务发送成功');
                        }
                        else {
                            $.ligerDialog.warn('任务发送失败');
                        }
                    }
                });
            }
        });
    }


});
//样品信息
$(document).ready(function () {
    //待办
    objTwoGrid = $("#twoGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: $(window).height() - 380,
        columns: [
                { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 150, minWidth: 60 },
                 { display: '特殊样说明', name: 'RemarkView', align: 'center', width: 80, render: function (items) {
                     if (items.SPECIALREMARK != "") {
                         return "<a href='javascript:showDetailRemarkSrh(\"" + items.ID + "\",\"" + items.SPECIALREMARK + "\",false)'>查看</a>";
                     }
                 }
                 },
                { display: '子样', name: 'SUBSAMPLE', align: 'center', width: 80, render: function (items) {
                    if (getSubSample(items.ID) != null) {
                        return "<a href='javascript:ShowSubSample(\"" + items.ID + "\")'>明细</a>";
                    }
                }
                }
                ],
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载监测项目信息
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID);
            strSubTaskID = rowdata.SUBTASK_ID;
            objFourGrid.set("data", emptyArray);
            objFiveGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    //已办
    objTwoGrid2 = $("#twoGrid2").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: $(window).height() - 380,
        columns: [
                { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 150, minWidth: 60 },
                 { display: '特殊样说明', name: 'RemarkView', align: 'center', width: 80, render: function (items) {
                     if (items.SPECIALREMARK != "") {
                         return "<a href='javascript:showDetailRemarkSrh(\"" + items.ID + "\",\"" + items.SPECIALREMARK + "\",false)'>查看</a>";
                     }
                 }
                 },
                { display: '子样', name: 'SUBSAMPLE', align: 'center', width: 80, render: function (items) {
                    if (getSubSample(items.ID) != null) {
                        return "<a href='javascript:ShowSubSample(\"" + items.ID + "\")'>明细</a>";
                    }
                }
                }
                ],
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载监测项目信息
            objThreeGrid2.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID);
            strSubTaskID = rowdata.SUBTASK_ID;
            objFourGrid2.set("data", emptyArray);
            objFiveGrid2.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
//监测项目信息
$(document).ready(function () {
    //待办
    objThreeGrid = $("#threeGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: 180,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                     return strItemName;
                 }
                 },
                 { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60, render: function (items) {
                     //var isShow = getItemInfoName(items.ITEM_ID, "IS_ANYSCENE_ITEM");
                     //if (isShow == "1") {
                     //    var strSubTaskId = objTwoGrid.getSelectedRow();

                     //    return "<a href='javascript:SetTable(\"" + strSubTaskId.SUBTASK_ID + "\" ,\"" + items.ITEM_ID + "\")'>原始样结果</a>";
                     //}
                     return items.ITEM_RESULT;
                 }
                 },
                 { display: '质控手段', name: 'QC', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     return getQcName(record.QC);
                 }
                 },
                  { display: '方法依据', name: 'METHOD_CODE', align: 'left', width: 200, minWidth: 60 },
                 { display: '分析方法', name: 'ANALYSIS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '检出限', name: 'RESULT_CHECKOUT', align: 'left', width: 100, minWidth: 60 },
                 { display: '单位', name: 'UNIT', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器编号', name: 'APPARATUS_CODE', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器', name: 'APPARATUS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '前处理说明', name: 'REMARK_2', align: 'center', width: 80, render: function (items) {
                     if (items.REMARK_2 != "") {
                         return "<a href='javascript:showSampleRemarkSrh(\"" + items.ID + "\",\"" + items.REMARK_2 + "\",false)'>查看</a>";
                     }
                     return items.REMARK_2;
                 }
                 },
                 { display: '分析负责人', name: 'HEAD_USERID', align: 'left', width: 100, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUserName(record.HEAD_USERID);
                 }
                 },
                 { display: '分析协同人', name: 'ASSISTANT_USERID', align: 'left', width: 200, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUseExName(record.ASSISTANT_USERID);
                 }
                 }
                ],
        toolbar: { items: [
                { text: '原始记录信息', click: SetTable, icon: 'attibutes' }, { text: '质控信息', click: QcInfo, icon: 'attibutes' }, { text: '退回', click: ResultGoToBack, icon: 'add'}]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载质控信息
            objFourGrid.set('url', strUrl + "?type=getFourGridInfo&threeGridId=" + rowdata.ID + "&strSubTaskID=" + strSubTaskID + "&strItemID=" + rowdata.ITEM_ID);
            //点击的时候加载实验室质控信息
            objFiveGrid.set('url', strUrl + "?type=getFiveGridInfo&threeGridId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    //已办
    objThreeGrid2 = $("#threeGrid2").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: 180,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                     return strItemName;
                 }
                 },
                 { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60, render: function (items) {
                     return items.ITEM_RESULT;
                 }
                 },
                 { display: '质控手段', name: 'QC', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     return getQcName(record.QC);
                 }
                 },
                  { display: '方法依据', name: 'METHOD_CODE', align: 'left', width: 200, minWidth: 60 },
                 { display: '分析方法', name: 'ANALYSIS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '检出限', name: 'RESULT_CHECKOUT', align: 'left', width: 100, minWidth: 60 },
                 { display: '单位', name: 'UNIT', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器编号', name: 'APPARATUS_CODE', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器', name: 'APPARATUS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '前处理说明', name: 'REMARK_2', align: 'center', width: 80, render: function (items) {
                     if (items.REMARK_2 != "") {
                         return "<a href='javascript:showSampleRemarkSrh(\"" + items.ID + "\",\"" + items.REMARK_2 + "\",false)'>查看</a>";
                     }
                     return items.REMARK_2;
                 }
                 },
                 { display: '分析负责人', name: 'HEAD_USERID', align: 'left', width: 100, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUserName(record.HEAD_USERID);
                 }
                 },
                 { display: '分析协同人', name: 'ASSISTANT_USERID', align: 'left', width: 200, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUseExName(record.ASSISTANT_USERID);
                 }
                 }
                ],
        toolbar: { items: [
                { text: '原始记录信息', click: SetTable, icon: 'attibutes' }, { text: '质控信息', click: QcInfo, icon: 'attibutes'}]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载质控信息
            objFourGrid2.set('url', strUrl + "?type=getFourGridInfo&threeGridId=" + rowdata.ID + "&strSubTaskID=" + strSubTaskID + "&strItemID=" + rowdata.ITEM_ID);
            //点击的时候加载实验室质控信息
            objFiveGrid2.set('url', strUrl + "?type=getFiveGridInfo&threeGridId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    function ResultGoToBack() {
        if (objThreeGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('退回之前请先选择监测项目信息');
            return;
        }
        $.ligerDialog.prompt('退回意见', '', true, function (yes, value) {
            if (yes == true) {
                //退回该项目
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=ResultGoToBack&strResultId=" + objThreeGrid.getSelectedRow().ID + "&strSuggestion=" + encodeURI(value),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data == "1") {
                            objThreeGrid.loadData();
                            objFourGrid.set("data", emptyArray);
                            objFiveGrid.set("data", emptyArray);
                            $.ligerDialog.success('监测项目退回成功')
                        }
                        else {
                            $.ligerDialog.warn('监测项目退回失败');
                        }
                    }
                });
            }
        });
    }
});
//质控信息
$(document).ready(function () {
    //待办
    objFourGrid = $("#fourGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: $(window).height() - 500,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                     return strItemName;
                 }
                 },
                 { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 120, minWidth: 60 },
                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     return getQcName(record.QC_TYPE);
                 }
                 },
                 { display: '质控结果', name: 'REMARK', align: 'left', width: 320, minWidth: 60 },
                  { display: '是否合格', name: 'IS_OK', align: 'left', width: 50, minWidth: 60, render: function (record) {
                      if (record.IS_OK == "0")
                          return "<span style='color:red'>否</span>";
                      else if (record.IS_OK == "1")
                          return "是";
                      else
                          return "";
                  }
                  }
                ],
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            //点击的时候加载实验室质控信息
            objFiveGrid.set('url', strUrl + "?type=getFiveGridInfo&threeGridId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    //已办
    objFourGrid2 = $("#fourGrid2").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: $(window).height() - 500,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                     return strItemName;
                 }
                 },
                 { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 120, minWidth: 60 },
                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     return getQcName(record.QC_TYPE);
                 }
                 },
                 { display: '质控结果', name: 'REMARK', align: 'left', width: 320, minWidth: 60 },
                  { display: '是否合格', name: 'IS_OK', align: 'left', width: 50, minWidth: 60, render: function (record) {
                      if (record.IS_OK == "0")
                          return "<span style='color:red'>否</span>";
                      else if (record.IS_OK == "1")
                          return "是";
                      else
                          return "";
                  }
                  }
                ],
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            //点击的时候加载实验室质控信息
            objFiveGrid2.set('url', strUrl + "?type=getFiveGridInfo&threeGridId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
//实验室质控信息
$(document).ready(function () {
    //待办
    objFiveGrid = $("#fiveGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: $(window).height() - 500,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                     return strItemName;
                 }
                 },
                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     return getQcName(record.QC_TYPE);
                 }
                 },
                 { display: '质控结果', name: 'REMARK', align: 'left', width: 320, minWidth: 60 },
                  { display: '是否合格', name: 'IS_OK', align: 'left', width: 50, minWidth: 60, render: function (record) {
                      if (record.IS_OK == "0")
                          return "<span style='color:red'>否</span>";
                      else if (record.IS_OK == "1")
                          return "是";
                      else
                          return "";
                  }
                  }
                ],
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    //已办
    objFiveGrid2 = $("#fiveGrid2").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: $(window).height() - 500,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                     return strItemName;
                 }
                 },
                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     return getQcName(record.QC_TYPE);
                 }
                 },
                 { display: '质控结果', name: 'REMARK', align: 'left', width: 320, minWidth: 60 },
                  { display: '是否合格', name: 'IS_OK', align: 'left', width: 50, minWidth: 60, render: function (record) {
                      if (record.IS_OK == "0")
                          return "<span style='color:red'>否</span>";
                      else if (record.IS_OK == "1")
                          return "是";
                      else
                          return "";
                  }
                  }
                ],
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    
});
//获取质控手段名称
function getQcName(strQcId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getQcName",
        data: "{'strQcId':'" + strQcId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取默认负责人用户名称
function getAjaxUserName(strUserId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDefaultUserName",
        data: "{'strUserId':'" + strUserId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取默认协同人名称
function getAjaxUseExName(strUserId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDefaultUserExName",
        data: "{'strUserId':'" + strUserId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//发送之前判断是否满足发送条件
function IsCanSendTaskQcToNextFlow(strTaskId) {
    var isCanBack = false;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=IsCanSendTaskQcToNextFlow&strTaskId=" + strTaskId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            isCanBack = data;
        }
    });
    return isCanBack;
}
//发送之前判断现场项目是否已
function IsCanSendTaskQcToNextFlowWithSence(strTaskId) {
    var isCanBack = false;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=IsCanSendTaskQcToNextFlowWithSence&strTaskId=" + strTaskId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            isCanBack = data;
        }
    });
    return isCanBack;
}
//设置grid 的弹出特殊样说明录入对话框
var detailRemarkWinSrh = null;
function showDetailRemarkSrh(strSubTaskId, oldRemark, isAdd) {
    //创建表单结构

    var mainRemarkform = $("#RemarkForm");
    mainRemarkform.ligerForm({
        inputWidth: 430, labelWidth: 90, space: 40,
        fields: [
                     { display: "特殊样说明", name: "SEA_REMARK", newline: true, type: "textarea" }
                    ]
    });
    $("#SEA_REMARK").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:400px");

    $("#SEA_REMARK").val(oldRemark);
    var ObjButton = [];
    if (!isAdd) {
        $("#SEA_REMARK").attr("disabled", true);
        ObjButton = [
                  { text: '返回', onclick: function () { clearRemarkDialogValue(); detailRemarkWinSrh.hide(); } }
                  ];
    } else {
        $("#SEA_REMARK").attr("disabled", false);
        ObjButton = [
                  { text: '确定', onclick: function () { SaveRemark(strSubTaskId); } },
                  { text: '返回', onclick: function () { clearRemarkDialogValue(); detailRemarkWinSrh.hide(); } }
                  ];
    }
    detailRemarkWinSrh = $.ligerDialog.open({
        target: $("#detailRemark"),
        width: 660, height: 170, top: 90, title: isAdd ? "特殊样说明录入" : "特殊样说明查看",
        buttons: ObjButton
    });
}
function clearRemarkDialogValue() {
    $("#SEA_REMAKR").val("");
}

//设置grid 的弹出特殊样说明录入对话框
var SampleRemarkWinSrh = null;
function showSampleRemarkSrh(strSubTaskId, oldRemark, isAdd) {
    //创建表单结构

    var sampleRemarkform = $("#SampleRemarkForm");
    sampleRemarkform.ligerForm({
        inputWidth: 430, labelWidth: 90, space: 40,
        fields: [
                     { display: "前处理说明", name: "SEA_SAMPLEREMARK", newline: true, type: "textarea" }
                    ]
    });
    $("#SEA_SAMPLEREMARK").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:400px");

    $("#SEA_SAMPLEREMARK").val(oldRemark);
    var ObjButton = [];
    if (!isAdd) {
        $("#SEA_SAMPLEREMARK").attr("disabled", true);
        ObjButton = [
                  { text: '返回', onclick: function () { clearSampleRemarkDialogValue(); SampleRemarkWinSrh.hide(); } }
                  ];
    } else {
        $("#SEA_SAMPLEREMARK").attr("disabled", false);
        ObjButton = [
                  { text: '确定', onclick: function () { SaveSampleRemark(strSubTaskId); } },
                  { text: '返回', onclick: function () { clearSampleRemarkDialogValue(); SampleRemarkWinSrh.hide(); } }
                  ];
    }
    SampleRemarkWinSrh = $.ligerDialog.open({
        target: $("#detailSampleRemark"),
        width: 660, height: 170, top: 90, title: isAdd ? "前处理说明录入" : "前处理说明查看",
        buttons: ObjButton
    });
}
function clearSampleRemarkDialogValue() {
    $("#SEA_SAMPLEREMARK").val("");
}

function SaveSampleRemark(strSubTaskId) {
    var strRemark = $("#SEA_SAMPLEREMARK").val();
    $.ajax({
        cache: false,
        type: "POST",
        url: "AnalysisResult.aspx/SaveRemark",
        data: "{'strValue':'" + strSubTaskId + "','strSampleRemark':'" + strRemark + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "true") {
                objTwoGrid.loadData();
                clearSampleRemarkDialogValue();
                SampleRemarkWinSrh.hide();
                $.ligerDialog.success('数据操作成功')
            }
            else {
                $.ligerDialog.warn('数据操作失败');
            }
        }
    });
}

function getSubSample(SampleId) {
    var objItems = null;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../../sampling/QY/SubSample.aspx?action=GetSubSampleList&strSampleId=" + SampleId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != "0") {
                objItems = data.Rows;
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax请求数据失败！');
        }
    });
    return objItems;
}

function ShowSubSample(strId) {
    $.ligerDialog.open({ title: '子样明细', name: 'winaddtor', width: 700, height: 400, top: 90, url: '../../sampling/QY/SubSample.aspx?strView=true&strSampleId=' + strId, buttons: [
                { text: '返回', onclick: function (item, dialog) { dialog.close() } }
            ]
    });
}

//获取监测项目信息
function getItemInfor(strItemID) {
    var strValue = null;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../../sampling/QY/SamplePoint.aspx?type=getBaseItemInfor&strItemId=" + strItemID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.Total != '0') {
                strValue = data.Rows;
            }
        }
    });
    return strValue;
}

function SetTable() {
    var selectedSample = null;
    var selectedItem = null;
    if (selectTabId == "0") {
        selectedSample = objTwoGrid.getSelectedRow();
        selectedItem = objThreeGrid.getSelectedRow();
    } else {
        selectedSample = objTwoGrid2.getSelectedRow();
        selectedItem = objThreeGrid2.getSelectedRow();
    }

    if (!selectedSample) {
        $.ligerDialog.warn('请先选择样品！');
        return;
    }
    if (!selectedItem) {
        $.ligerDialog.warn('请先选择监测项目！');
        return;
    }
    //结果类型，Poll：污染源 Air：大气
    var strPA = selectedItem.REMARK_5;
    var ItemInfor = getItemInfor(selectedItem.ITEM_ID);
    var strCataLogName = ItemInfor[0].ORI_CATALOG_TABLEID;
    //获取监测项目的监测类型 废气：000000002
    var strMONITORID = ItemInfor[0].MONITOR_ID;
    var strTitle = "原始记录表", strPageUrl = "", strKeyTableName = "", strBaseTableName = "";
    if (strCataLogName != "" || strMONITORID == "000000002") {
        if (strPA != "Air") {
            //固定污染源原始记录表
            switch (strCataLogName) {
                //烟尘类的 使用该表作为原始记录主表               
                case "T_MIS_MONITOR_DUSTATTRIBUTE":
                    if (ItemInfor[0].ITEM_NAME.indexOf("油烟") != -1) {
                        strTitle = "饮食业油烟分析原始记录表";
                        strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_YY.aspx";
                    }
                    else {
                        strTitle = "固定污染源排气中颗粒物采样分析原始记录表";
                        strPageUrl = "../../sampling/QY/OriginalTable/DustyTable.aspx";
                    }
                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE";
                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                    break;
                //除了标明原始记录表名的监测项目外其他废气的监测项目也使用该表作为原始记录主表   
                case "":
                    strTitle = "污染源采样原始记录表";
                    strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_PM.aspx";
                    strKeyTableName = "";
                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                    break;
                //PM的和总悬浮物项目类的 使用该表作为原始记录主表               
                case "T_MIS_MONITOR_DUSTATTRIBUTE_PM":
                    strTitle = "污染源采样原始记录表";
                    strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_PM.aspx";
                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE_PM";
                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                    break;
                //SO2和NOX类的 使用该表作为原始记录主表               
                case "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX":
                    strTitle = "固定污染源排气中气态污染物采样分析原始记录表";
                    strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_So2OrNox.aspx";
                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX";
                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                    break;
                default:
                    break;
            }
        }
        else {
            strTitle = "大气采样原始记录表";
            strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_Air.aspx";
            strKeyTableName = strCataLogName;
            strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
            OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
        }
    } else {
        return;
    }

    //    $.ligerDialog.open({ title: getItemInfoName(strItemId, "ITEM_NAME") + '样品原始记录表', top: 0, width: 1100, height: 680, buttons:
    //         [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); }
    //         }], url: '../../sampling/QY/OriginalTable/DustyTable.aspx?strSubTask_Id=' + strSubId + '&strIsView=true&strItem_Id=' + strItemId
    //    });
}
//strLinkCode环节编号，01：采样环节；02：监测分析环节；03：分析结果复核环节；04：质控审核环节；05：现场项目结果核录环节；06：分析主任审核环节；07：现场结果复核环节；08：现场室主任审核环节
function OpenDialog(Title, PageUrl, KeyTable, BaseTable, ItemID, SubTaskID) {
    $.ligerDialog.open({ Title: Title, top: 0, width: 1100, height: 680, buttons:
         [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: PageUrl + '?strSubTask_Id=' + SubTaskID + '&strIsView=true&strItem_Id=' + ItemID + '&strKeyTableName=' + KeyTable + '&strBaseTableName=' + BaseTable + '&strLinkCode=06'
    });
}

//function SetTable(strSubId, strItemId) {
//    $.ligerDialog.open({ title: getItemInfoName(strItemId, "ITEM_NAME") + '样品原始记录表', top: 0, width: 1100, height: 680, buttons:
//         [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); }
//         }], url: '../../sampling/QY/OriginalTable/DustyTable.aspx?strSubTask_Id=' + strSubId + '&strIsView=true&strItem_Id=' + strItemId
//    });
//}

//查看质控信息
function QcInfo() {
    if (objThreeGrid.getSelectedRow() == null) {
        $.ligerDialog.warn('请选择一个项目进行查看');
        return;
    }
    var strResultID = objThreeGrid.getSelectedRow().ID;
    var strItemID = objThreeGrid.getSelectedRow().ITEM_ID;
    $.ligerDialog.open({ title: "质控信息查看", width: 780, height: 530, isHidden: false, buttons:
        [
        { text:
        '返回', onclick: function (item, dialog) { dialog.close(); }
        }], url: "QcInfo.aspx?strSubTaskID=" + strSubTaskID + "&strResultID=" + strResultID + "&strItemID=" + strItemID + "&strMark=0"
    });
}

//弹出退回意见框
var SuggestionDialog = null;
function showSuggestion(value) {
    //创建表单结构
    $("#SuggForm").ligerForm({
        inputWidth: 430, labelWidth: 90, space: 40,
        fields: [
                     { display: "退回意见", name: "Suggestion", newline: true, type: "textarea" }
                    ]
    });
    $("#Suggestion").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:320px");

    $("#Suggestion").val(value);
    var ObjButton = [];

    $("#Suggestion").attr("disabled", true);
    ObjButton = [
                  { text: '返回', onclick: function () { SuggestionDialog.hide(); } }
                  ];
    SuggestionDialog = $.ligerDialog.open({
        target: $("#divSugg"),
        width: 560, height: 170, top: 90, title: "退回意见查看",
        buttons: ObjButton
    });
}

