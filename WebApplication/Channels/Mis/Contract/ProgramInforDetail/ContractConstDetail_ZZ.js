//Create By  胡方扬  监测费用明细列表
var ItemList = null, AttItemList = null, vSumList = null;
var objTool = null, isEdit = false;
var view = false;
var strAtt_item_Id = "", strAttFeeId = "", strAttFee = "";
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
    $("#layout1").ligerLayout({ topHeight: 120, leftWidth: "98%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });
    $("#pageloading").hide();
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
    $("#divFileExcelUp").html("<a href='javascript:'>费用上传</a>");
    $("#divFileExcelDown").html("<a href='javascript:'>费用下载</a>");
    $("#divFileExcelUp").bind("click", function () {
        upLoadFile();
    })

    $("#divFileExcelDown").bind("click", function () {
        downLoadFile();
    })
    GetFile();

    ///附件上传
    function upLoadFile() {
        if (strContratId == "") {
            $.ligerDialog.warn('业务ID参数错误');
            return;
        }
        $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
            buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        dialog.frame.upLoadFile();
                        GetFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { GetFileSataus(item, dialog), dialog.close(); }
            }], url: '../../../OA/ATT/AttFileUpload.aspx?filetype=ContractCountFile&id=' + strContratId
        });
    }
    function GetFileSataus(item, dialog) {
        var fn = dialog.frame.getUpLoadStatus();
        if (fn == "1") {
            $("#divFileExcelDown").html("<a href='javascript:'>费用下载</a>")
            $("#divFileExcelDown").bind("click", function () {
                downLoadFile();
            })
            dialog.close();
        }

        if (fn == "2") {
            $("#divFileExcelDown").html("")
            $("#divFileExcelDown").bind("click");
        }
    }
    ///附件下载
    function downLoadFile() {
        if (strContratId == "") {
            $.ligerDialog.warn('业务ID参数错误');
            return;
        }
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?filetype=ContractCountFile&id=' + strContratId
        });
    }
    function GetFile() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../ProgrammingHandler.ashx?action=GetFile&task_id=" + strContratId + "&strFileType=ContractCountFile",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {

                }
                else {
                    $("#divFileExcelDown").unbind("click");
                    $("#divFileExcelDown").html("");
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    if (view == false) {
        //设置显示操作按钮
        //        objTool = { items: [
        //                { id: 'setting', text: '选择附加项目', click: toolBarClick, icon: 'database_wrench' },
        //                { line: true },
        //                { id: 'add', text: '增加', click: toolBarClick, icon: 'add' },
        //                { line: true },
        //                { id: 'del', text: '删除', click: toolBarClick, icon: 'delete' }
        //                ]
        //        };
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
    //GetContractConstFeeDetail();
    // GetAttFeeDetail();
    //填充布局
    //gridDraggable(g1, g2);
    SetTextValueConst();
    //    function GetContractConstFeeDetail() {
    //        window['g1'] =
    //    manager1 = $("#divstestfree").ligerGrid({
    //        columns: [
    //                { display: '监测项目', name: 'TEST_ITEM_ID', align: 'left', width: 120, data: ItemList, render: function (item) {
    //                    for (var i = 0; i < ItemList.length; i++) {
    //                        if (ItemList[i]['ID'] == item.TEST_ITEM_ID)
    //                            return ItemList[i]['ITEM_NAME'];
    //                    }
    //                    return item.TEST_ITEM_ID;
    //                }
    //                },
    //                { display: '监测单价', name: 'TEST_PRICE', minWidth: 100 },
    //                { display: '样品数', name: 'TEST_NUM', minWidth: 40 },
    //                { display: '监测费用', name: 'TEST_FEE', minWidth: 120 },
    //                { display: '开机费', name: 'TEST_POWER_FEE', minWidth: 100 },
    //                 { display: '小计', name: 'FEE_COUNT', minWidth: 40 }
    //                ],
    //        width: '98%',
    //        height: 220,
    //        pageSizeOptions: [5, 10, 15, 20],
    //        url: '../MethodHander.ashx?action=GetContractConstFeeDetail&strContratId=' + strContratId + '',
    //        dataAction: 'server', //服务器排序
    //        usePager: true,       //服务器分页
    //        pageSize: 5,
    //        alternatingRow: false,
    //        whenRClickToSelect: true,
    //        onCheckRow: function (checked, rowdata, rowindex) {
    //            for (var rowid in this.records)
    //                this.unselect(rowid);
    //            this.select(rowindex);
    //        },
    //        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    //    }
    //    );

    //        $("#pageloading").hide();
    //        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    //    }


    //    function GetAttFeeDetail() {
    //        window['g2'] =
    //    manager2 = $("#divattfreelist").ligerGrid({
    //        columns: [
    //                { display: '附加项目', name: 'ATT_FEE_ITEM_ID', align: 'left', width: 120, data: AttItemList, render: function (item) {
    //                    for (var i = 0; i < AttItemList.length; i++) {
    //                        if (AttItemList[i]['ID'] == item.ATT_FEE_ITEM_ID)
    //                            return AttItemList[i]['ATT_FEE_ITEM'];
    //                    }
    //                    return item.TEST_ITEM_ID;
    //                }
    //                },
    //                { display: '费用', name: 'FEE', minWidth: 100, editor: { type: 'text'} },
    //                { display: '描述', name: 'INFO', align: 'left', minWidth: 120 }
    //                ],
    //        width: '100%',
    //        height: '98%',
    //        pageSizeOptions: [5, 10, 15, 20],
    //        url: '../MethodHander.ashx?action=GetAttFeeDetail&strContratId=' + strContratId + '',
    //        dataAction: 'server', //服务器排序
    //        usePager: true,       //服务器分页
    //        pageSize: 5,
    //        toolbar: objTool,
    //        alternatingRow: false,
    //        enabledEdit: isEdit,
    //        checkbox: true,
    //        rownumbers: true,
    //        onAfterEdit: f_onAfterEdit,
    //        onCheckRow: function (checked, rowdata, rowindex) {
    //            for (var rowid in this.records)
    //                this.unselect(rowid);
    //            this.select(rowindex);
    //        }
    //        //onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    //    }
    //    );
    //        $("#pageloading").hide();
    //        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    //    }

    //    function f_onAfterEdit(e) {
    //        manager2.updateCell('FEE', e.record.FEE, e.record);
    //        strAttFeeId = e.record.ID
    //        strAtt_item_Id = e.record.ATT_FEE_ITEM_ID;
    //        strAttFee = e.record.FEE;
    //        updateAttFeeInfor();
    //    }
    //    function updateAttFeeInfor() {
    //        $.ajax({
    //            cache: false,
    //            async: false, //设置是否为异步加载,此处必须
    //            type: "POST",
    //            url: "../MethodHander.ashx?action=UpdateAttFeeInfor&strContratId=" + strContratId + "&strAttFeeId=" + strAttFeeId + "&strAtt_item_Id=" + strAtt_item_Id + "&strAttFee=" + strAttFee + "",
    //            contentType: "application/text; charset=utf-8",
    //            dataType: "text",
    //            success: function (data) {
    //                if (data != "") {
    //                    manager2.loadData();
    //                    SetTextValueConst();
    //                }
    //                else {
    //                    $.ligerDialog.warn('修改失败！');
    //                }
    //            },
    //            error: function (msg) {
    //                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
    //            }
    //        });
    //    }
    //    function toolBarClick(item) {
    //        switch (item.id) {
    //            case 'setting':
    //                $.ligerDialog.open({ title: '附加项目设置', top: 60, width: 440, height: 400, buttons:
    //            [{ text: '确定', onclick: f_SaveDivItemData },
    //             { text: '返回', onclick: function (item, dialog) { dialog.close(); }
    //             }], url: 'ContractAttFeeItems.aspx?strContratId=' + strContratId
    //                });
    //                break;
    //            case 'add':
    //                showDetailSrh(false);
    //                break;
    //            case 'del':
    //                var rowselected = manager2.getSelectedRow();
    //                if (rowselected == null) {
    //                    $.ligerDialog.warn('请选择一条记录'); return;
    //                }
    //                strAttFeeId = rowselected.ID;
    //                f_DelAttFeeItems();
    //                break;
    //            default:
    //                break;
    //        }
    //    }

    //通过获取弹出窗口页面中 获取附加项目右侧ListBox集合以及移除的附加项目
    //    function f_SaveDivItemData(item, dialog) {
    //        var fn = dialog.frame.GetMoveItems || dialog.frame.window.GetMoveItems;
    //        var strMovedata = fn();

    //        var fn1 = dialog.frame.GetSelectItems || dialog.frame.window.GetSelectItems;
    //        var strSelectData = fn1();

    //        if (strSelectData == "" & strMovedata == "") {
    //            return;
    //        }
    //        else {
    //            SaveDivItemData(strSelectData, strMovedata)
    //        }
    //    }

    //    //保存监测项目
    //    function SaveDivItemData(strSelectData, strMovedata) {
    //        $.ajax({
    //            cache: false,
    //            async: false, //设置是否为异步加载,此处必须
    //            type: "POST",
    //            url: "../MethodHander.ashx?action=SaveDivAttItemData&strContratId=" + strContratId + "&strAttAddItemsId=" + strSelectData + "&strAttMoveItemsId=" + strMovedata,
    //            contentType: "application/text; charset=utf-8",
    //            dataType: "text",
    //            success: function (data) {
    //                if (data != "") {
    //                    manager2.loadData();
    //                    SetTextValueConst();
    //                    parent.$.ligerDialog.success('数据保存成功！');
    //                }
    //                else {
    //                    parent.$.ligerDialog.warn('数据操作失败！');
    //                }
    //            },
    //            error: function () {
    //                parent.$.ligerDialog.warn('Ajax加载数据失败！');
    //            }
    //        });
    //    }

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

function OpExcel() {
    $("#btnExport").click();
}
