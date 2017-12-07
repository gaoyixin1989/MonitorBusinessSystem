/// 站务管理--物料申请列表
/// 创建时间：2013-01-31
/// 创建人：胡方扬
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
//工作流
var strWf = "", wfArr = null;
var maingrid = null, maingrid1 = null, maingrid2 = null, vTypeItems = null, vEmployeDept = null, DeptItems = null, vCheckTypeItems = null, vPartItems = null;
var vUserList = null;
var isFisrt = "", gridName = "0", strStatus = "",strRealName="";
var strExamType = "", strEmployeNames = "";
var keshi = "";//黄进军添加
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
    strWf = $.getUrlVar('WF_ID');
    keshi = $.getUrlVar('keshi'); //黄进军添加
    if (strWf != "") {
        wfArr = strWf.split('|');
        if (wfArr.length < 3) {
            for (var i = 0; i <= 3 - wfArr.length; i++) {
                wfArr.push("");
            }
        }
    }
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=dept",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                DeptItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    //    $.ajax({
    //        cache: false,
    //        async: false, //设置是否为异步加载,此处必须
    //        type: "POST",
    //        url: "../Employe/EmployeHander.ashx?action=GetDict&type=PART_TYPE",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (data) {
    //            if (data != null) {
    //                vPartItems = data;
    //            }
    //            else {
    //                $.ligerDialog.warn('获取数据失败！');
    //            }
    //        },
    //        error: function (msg) {
    //            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
    //        }
    //    });

    //    $.ajax({
    //        cache: false,
    //        async: false, //设置是否为异步加载,此处必须
    //        type: "POST",
    //        url: "../Employe/EmployeHander.ashx?action=GetDict&type=CheckResult",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (data) {
    //            if (data != null) {
    //                vCheckTypeItems = data;
    //            }
    //            else {
    //                $.ligerDialog.warn('获取数据失败！');
    //            }
    //        },
    //        error: function (msg) {
    //            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
    //        }
    //    });
    function getUserName(strUserID) {
        var strtempName = "";
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "PartHandler.ashx?action=GetUserInfor&strUserID=" + strUserID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    arrUserDetail = data.Rows;
                    strtempName = arrUserDetail[0].REAL_NAME;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        })

        return strtempName;
    }

    $("#navtab1").ligerTab({ contextmenu: false, onBeforeSelectTabItem: function (tabid) {
    },
        //在点击选项卡之后触发   点击其他的选项卡后，刷新该选项卡，防止CSS样式被串
        onAfterSelectTabItem: function (tabid) {
            navtab = $("#navtab1").ligerGetTabManager();
            if (tabid == "home") {
                gridName = "0";
            }
            if (tabid == "tabitem1") {
                gridName = "1";
            }
            navtab.reload(navtab.getSelectedTabItemID());
            if (tabid != 'home') {
                isFisrt = false;
                GetPartAcceptList();
            }
            else {
                isFisrt = true;
            }
        }
    });

    maingrid = $("#maingrid").ligerGrid({
        columns: [
                { display: '物料名称', name: 'PART_NAME', align: 'left', width: 160, minWidth: 60 },
                 { display: '物料编码', name: 'PART_CODE', align: 'left', width: 160, minWidth: 60 },
                { display: '申请单标题', name: 'APPLY_TITLE', align: 'left', width: 220, minWidth: 60, render: function (item) {
                    if (item.length >= 20) {
                        return "<a title=" + item.APPLY_TITLE + ">" + item.APPLY_TITLE.substring(0, 20) + "...</a>";
                    }
                    return item.APPLY_TITLE;
                }
                },
            { display: '申请部门', name: 'DEPT_NAME', width: 80, minWidth: 60 },
            { display: '申请人', name: 'REAL_NAME', width: 80, minWidth: 60 },
            { display: '规格型号', name: 'MODELS', width: 80, minWidth: 60 },
            { display: '需求数量', name: 'NEED_QUANTITY', width: 80, minWidth: 60 },
            { display: '要求交货期', name: 'DELIVERY_DATE', width: 80, minWidth: 60 },
            { display: '计划资金', name: 'BUDGET_MONEY', width: 80, minWidth: 60, render: function (item) {
                if (item.BUDGET_MONEY != "") {
                    return "<a style='color:Red'><b>￥</b></a>" + item.BUDGET_MONEY;
                }
                return "<a style='color:Red'><b>￥0</b></a>";
            }
            },
            { display: '技术要求', name: 'REQUEST', align: 'left', width: 200, minWidth: 60 },
            { display: '用途', name: 'USEING', align: 'left', width: 200, minWidth: 60 }
            ],
        width: '100%', height: '100%',
        pageSizeOptions: [5, 10],
        url: "PartHandler.ashx?action=GetPartPlanList&strStatus=9&strReqStatus=0&type=1", //何海亮修改
        dataAction: 'server', //服务器排序GetAcceptPartPlanList
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        rownumbers: true,
        toolbar: { items: [
                        { id: 'subtrue', text: '验收', click: AcceptData, icon: 'TRUE' },
                                                     { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' },
                                { line: true },
                { id: 'excel', text: '导出', click: exportExcel, icon: 'excel' }
                ]
        },
        whenRClickToSelect: true,
        onCheckRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            $.ligerDialog.open({ title: '采购物料验收', top: 40, width: 700, height: 420, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'PartAcceptEdit.aspx?strPartPlanId=' + data.ID + '&strPartId=' + encodeURI(data.PART_ID) + '&strPartCode=' + encodeURI(data.PART_CODE) + '&strPartName=' + encodeURI(data.PART_NAME) + '&strNeedQuanity=' + encodeURI(data.NEED_QUANTITY) + '&strUserDo=' + encodeURI(data.USEING)
            });
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }

    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll

    function GetPartAcceptList() {
        maingrid1 = $("#maingrid1").ligerGrid({
            columns: [
             { display: '类别', name: 'PARTTYPE', width: 100, minWidth: 60 },
            { display: '品名(编码)', name: 'PART_NAME', align: 'left', width: 140, minWidth: 60, render: function (item) {
                if (item.PART_CODE != "") {
                    return item.PART_NAME + "(" + item.PART_CODE + ")";
                }
                return item.PART_NAME;
            }
            },
             { display: '供应商名称', name: 'ENTERPRISE_NAME', align: 'left', width: 160, minWidth: 60 },
            { display: '采购数量', name: 'NEED_QUANTITY', width: 80, minWidth: 60 },
             { display: '单价', name: 'PRICE', width: 80, minWidth: 60, render: function (item) {
                 if (item.PRICE != "") {
                     return "<a style='color:Red'><b>￥</b></a>" + item.PRICE;
                 }
                 return "<a style='color:Red'><b>￥0</b></a>";
             }
             },
            { display: '金额', name: 'AMOUNT', width: 80, minWidth: 60, render: function (item) {
                if (item.AMOUNT != "") {
                    return "<a style='color:Red'><b>￥</b></a>" + item.AMOUNT;
                }
                return "<a style='color:Red'><b>￥0</b></a>";
            }
            },
            { display: '验收日期', name: 'CHECK_DATE', width: 80, minWidth: 60 },
            { display: '检验日期', name: 'RECIVEPART_DATE', width: 80, minWidth: 60 },
            { display: '验收情况', name: 'CHECKRESULT', width: 80, minWidth: 60 },
            { display: '验收人', name: 'REAL_NAME', align: 'left', width: 80, minWidth: 60 },
            { display: '备注', name: 'REMARK1', width: 200, minWidth: 60, render: function (item) {
                if (item.length >= 20) {
                    return "<a title=" + item.REMARK1 + ">" + item.REMARK1.substring(0, 20) + "...</a>";
                }
                return item.APPLY_TITLE;
            }
            }
            ],
            width: '100%', height: '100%',
            pageSizeOptions: [5, 10],
            url: "PartHandler.ashx?action=GetAcceptPartPlanList",
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            toolbar: { items: [
                { id: 'view', text: '查看明细', click: ViewData, icon: 'archives' },
                { line: true },
                { id: 'srh', text: '查询', click: showAcceptDetailSrh, icon: 'search' },
                                                { line: true },
                { id: 'excel', text: '导出', click: exportExcel1, icon: 'excel' },
               { id: 'export', text: '打印', click: ExportData, icon: 'add' }
                ]
            },
            whenRClickToSelect: true,
            //            onCheckRow: function (data, rowindex, rowobj) {
            //                for (var rowid in this.records)
            //                    this.unselect(rowid);
            //                this.select(rowindex);
            //            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }

        });
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
    }
    //打印
    function ExportData() {
        var rowSelected = null, grid = null;
        if (gridName == "0") {
            rowSelected = maingrid.getSelectedRow()
            grid = maingrid;
        }
        if (gridName == "1") {
            rowSelected = maingrid1.getSelectedRow()
            grid = maingrid1;
        }
        if (rowSelected == null) {
            $.ligerDialog.warn('打印之前请先选择单据');
            return;
        }
        $("#hidFwId").val(rowSelected.ID);
        $("#btnExports").click();
    }
    function ViewData() {
        var rowSelected = null, grid = null;
        if (gridName == "0") {
            rowSelected = maingrid.getSelectedRows();
            grid = maingrid;
        }
        if (gridName == "1") {
            rowSelected = maingrid1.getSelectedRows();
            grid = maingrid1;
        }
        if (rowSelected.length < 1) {
            $.ligerDialog.warn('请选择一行进行查看！'); return
        } else {
            if (rowSelected.length > 1) {
                $.ligerDialog.warn('只能同时查看一条记录的明细！'); return
            } else {
                var tabid = "tabidview"
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|keshi='+keshi+'|strtaskID=' + rowSelected[0].REQUST_ID + '&WF_ID=' + wfArr[0];
                top.f_addTab(tabid, '查看采购计划单', surl);
            }
        }

    };
    function AcceptData() {
        var rowSelected = null, grid = null;
        if (gridName == "0") {
            rowSelected = maingrid.getSelectedRows();
            grid = maingrid;
        }
        if (gridName == "1") {
            rowSelected = maingrid1.getSelectedRows();
            grid = maingrid1;
        }
        if (rowSelected.length < 1) {
            $.ligerDialog.warn('请选择一行进行操作！'); return;
        } else {
            if (rowSelected.length > 1) {
                $.ligerDialog.warn('只能同时验收一条记录的明细！'); return
            } else {
                $.ligerDialog.open({ title: '采购物料验收', top: 40, width: 700, height: 420, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'PartAcceptEdit.aspx?strPartPlanId=' + rowSelected[0].ID + '&strPartId=' + encodeURI(rowSelected[0].PART_ID) + '&strPartCode=' + encodeURI(rowSelected[0].PART_CODE) + '&strPartName=' + encodeURI(rowSelected[0].PART_NAME) + '&strNeedQuanity=' + encodeURI(rowSelected[0].NEED_QUANTITY) + '&strUserDo=' + encodeURI(rowSelected[0].USEING)
                });
            }
        }
    };

    //保存数据
    function f_SaveDate(item, dialog) {
        var fnSave = dialog.frame.GetBaseInfoStr || dialog.frame.window.GetBaseInfoStr;
        var strReques = fnSave();
        if (strReques != "") {
            SavePartAcceptedInfor(strReques);
            dialog.close();
        }
        else {
            $.ligerDialog.warn('请核对必填数据项!'); return;
        }
    }


    function SavePartAcceptedInfor(strReques) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "PartHandler.ashx?action=SavePartAcceptedInfor" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据保存成功！');
                    maingrid.loadData();
                    return;
                }
                else {
                    $.ligerDialog.warn('数据保存失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }


    //设置grid 的弹出查询对话框
    var detailWinSrh = null;
    function showDetailSrh() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构

            var mainform = $("#SrhForm");
            mainform.ligerForm({
                inputWidth: 160, labelWidth: 90, space: 40,
                fields: [
                     { display: "物料编码", name: "SEA_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "物料名称", name: "SEA_NAME", newline: false, type: "text" },
                    { display: "申请部门", name: "SEA_DEPT", newline: true, type: "select", comboboxName: "SEA_DEPT_BOX", options: { valueFieldID: "SEA_DEPT_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: DeptItems} },
                    { display: "申请人", name: "SEA_APP_USER", newline: false, type: "text" },
                    { display: "交货开始日期", name: "SEA_BEGINDATE", newline: true, type: "date" },
                    { display: "交货截止日期", name: "SEA_ENDDATE", newline: false, type: "date" }
                    ]
            });
            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 660, height: 200, top: 90, title: "待验收物料查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SEA_CODE = encodeURI($("#SEA_CODE").val());
            var SEA_NAME = encodeURI($("#SEA_NAME").val());
            var SEA_DEPT_OP = $("#SEA_DEPT_OP").val();
            var SEA_APP_USER = $("#SEA_APP_USER").val();
            var SEA_ENDDATE = encodeURI($("#SEA_ENDDATE").val());
            var SEA_BEGINDATE = encodeURI($("#SEA_BEGINDATE").val());
            if (SEA_BEGINDATE != "" && SEA_ENDDATE == "") {
                $.ligerDialog.warn('请选择截止日期！'); return;
            }
            if (SEA_ENDDATE != "" && SEA_BEGINDATE == "") {
                $.ligerDialog.warn('请选择开始日期！'); return;
            }
            maingrid.set('url', "PartHandler.ashx?action=GetPartPlanList&strStatus=9&strReqStatus=0&strPartCode=" + SEA_CODE + "&strPartName=" + SEA_NAME + "&strDept=" + SEA_DEPT_OP + "&strUserName=" + encodeURI(SEA_APP_USER) + "&strBeginDate=" + SEA_BEGINDATE + "&strEndDate=" + SEA_ENDDATE);
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_CODE").val("");
        $("#SEA_NAME").val("");
        $("#SEA_DEPT_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_APP_USER").val("");
        $("#SEA_ENDDATE").val("");
        $("#SEA_BEGINDATE").val("");
    }


    //设置grid 的弹出查询对话框
    var detailAcceptWinSrh = null;
    function showAcceptDetailSrh() {
        if (detailAcceptWinSrh) {
            detailAcceptWinSrh.show();
        }
        else {
            //创建表单结构

            var mainform1 = $("#SrhForm1");
            mainform1.ligerForm({
                inputWidth: 160, labelWidth: 90, space: 40,
                fields: [
                     { display: "物料编码", name: "SEA_CODE1", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "物料名称", name: "SEA_NAME1", newline: false, type: "text" },
                    { display: "验收开始日期", name: "SEA_BEGINDATE1", newline: true, type: "date" },
                    { display: "验收截止日期", name: "SEA_ENDDATE1", newline: false, type: "date" }
                    ]
            });
            detailAcceptWinSrh = $.ligerDialog.open({
                target: $("#detailSrh1"),
                width: 660, height: 200, top: 90, title: "已验收物料查询",
                buttons: [
                  { text: '确定', onclick: function () { Acceptsearch(); clearAcceptSearchDialogValue(); detailAcceptWinSrh.hide(); } },
                  { text: '返回', onclick: function () { clearAcceptSearchDialogValue(); detailAcceptWinSrh.hide(); } }
                  ]
            });
        }

        function Acceptsearch() {
            var SEA_CODE = encodeURI($("#SEA_CODE1").val());
            var SEA_NAME = encodeURI($("#SEA_NAME1").val());
            var SEA_ENDDATE = encodeURI($("#SEA_ENDDATE1").val());
            var SEA_BEGINDATE = encodeURI($("#SEA_BEGINDATE1").val());
            if (SEA_BEGINDATE != "" && SEA_ENDDATE == "") {
                $.ligerDialog.warn('请选择截止日期！'); return;
            }
            if (SEA_ENDDATE != "" && SEA_BEGINDATE == "") {
                $.ligerDialog.warn('请选择开始日期！'); return;
            }
            maingrid1.set('url', "PartHandler.ashx?action=GetAcceptPartPlanList&strPartCode=" + SEA_CODE + "&strPartName=" + SEA_NAME + "&strBeginDate=" + SEA_BEGINDATE + "&strEndDate=" + SEA_ENDDATE);
        }
    }

    function clearAcceptSearchDialogValue() {
        $("#SEA_CODE1").val("");
        $("#SEA_NAME1").val("");
        $("#SEA_ENDDATE1").val("");
        $("#SEA_BEGINDATE1").val("");
    }


    function exportExcel() {
        $.ligerDialog.confirm('您确定要导出采购计划表吗？', function (yes) {
            if (yes == true) {
                var strPartCollarId = "";
                var spit = "";
                var rowSelected = maingrid.getSelectedRows();
                if (rowSelected.length > 0) {
                    for (var i = 0; i < rowSelected.length; i++) {
                        strPartCollarId += spit + rowSelected[i].ID;
                        spit = ",";
                    }
                    $("#hidGrid").val('1');
                    $("#hidExportDate").val(strPartCollarId);
                    $("#btnExport").click();
                } else {
                    $("#hidGrid").val('1');
                    $("#btnExport").click();
                }
                maingrid.loadData();
            }
        });
    }


    function exportExcel1() {
        $.ligerDialog.confirm('您确定要导出采购验收表吗？', function (yes) {
            if (yes == true) {
                var strPartCollarId = "";
                var spit = "";
                var rowSelected = maingrid1.getSelectedRows();
                if (rowSelected.length > 0) {
                    for (var i = 0; i < rowSelected.length; i++) {
                        strPartCollarId += spit + rowSelected[i].ID;
                        spit = ",";
                    }
                    $("#hidGrid").val('2');
                    $("#hidExportDate").val(strPartCollarId);
                    $("#btnExport").click();
                } else {
                    $("#hidGrid").val('2');
                    $("#btnExport").click();
                }
                maingrid.loadData();
            }
        });
    }
})