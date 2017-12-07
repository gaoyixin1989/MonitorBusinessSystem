/// 站务管理--物料申请列表
/// 创建时间：2013-01-31
/// 创建人：胡方扬
/// 修改人：魏林 2013-09-10
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
//黄进军添加20141024
var webServiceUrl = "../../Base/Search/TotalSearch.aspx";
var wfStepUrl = "../../../Sys/WF/WFShowStepDetail.aspx";
//工作流ID
var strWF_ID = "";
var maingrid = null, maingrid1 = null, maingrid2 = null, vTypeItems = null, vEmployeDept = null, vEmployePartType = null;
var vUserList = null;
var isFisrt = "", gridName = "0";
var strExamType = "", strEmployeNames = "";
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

    var menu = { width: 120, items:
            [
            //{ id: 'FillMasterial_OW', text: '填报办公领料单', click: AddData_OW, icon: 'bookadd' },
             //{ line: true },
             //{ id: 'FillMasterial_EP', text: '填报实验领料单', click: AddData_EP, icon: 'bookadd' },
             //{ line: true },
            //{ id: 'WorkSubmit', text: '工作呈报单', click: AddWorkSubmitData, icon: 'book_tabs' }
            ]
    };
    $("#topmenu").ligerMenuBar({ items: [
                 //{ id: 'add', text: '增加', menu: menu, icon: 'add' },
          { id: 'add', text: '增加', click: AddData, icon: 'add' },
                {id: 'modify', text: '修改', click: ModifData, icon: 'modify' },
                { id: 'del', text: '删除', click: DeleteData, icon: 'delete' },
               { id: 'export', text: '打印', click: ExportData, icon: 'add' }
            ]
    });

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
            if (tabid == "tabitem2") {
                gridName = "2";
            }
            navtab.reload(navtab.getSelectedTabItemID());
            if (tabid != 'home') {
                isFisrt = false;
                GetIngDate();
                GetFinishedDate();
            }
            else {
                isFisrt = true;
            }
        }
    });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=dept",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vEmployeDept = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=PART_TYPE",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vEmployePartType = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });


    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "PartHandler.ashx?action=GetUserInfor",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vUserList = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    maingrid = $("#maingrid").ligerGrid({
        columns: [
                { display: '采购主题', name: 'APPLY_TITLE', align: 'left', width: 220 },
                //{ display: '物料类别', name: 'APPLY_TYPE', width: 160, minWidth: 60, render: function (item) {
                //    if (vEmployePartType != null) {
                //        for (var i = 0; i < vEmployePartType.length; i++) {
                //            if (vEmployePartType[i].DICT_CODE == item.APPLY_TYPE) {
                //                return vEmployePartType[i].DICT_TEXT;
                //            }
                //        }
                //    }
                //    return item.APPLY_TYPE;
                //}
                //},
                { display: '申请人', name: 'APPLY_USER_ID', width: 160, minWidth: 60, render: function (item) {
                    if (vUserList != null) {
                        for (var i = 0; i < vUserList.length; i++) {
                            if (vUserList[i].ID == item.APPLY_USER_ID) {
                                return vUserList[i].REAL_NAME;
                            }
                        }
                    }
                    return item.APPLY_USER_ID;
                }
                },
                { display: '申请部门', name: 'APPLY_DEPT_ID', width: 160, minWidth: 60, render: function (item) {
                    if (vEmployeDept != null) {
                        for (var i = 0; i < vEmployeDept.length; i++) {
                            if (vEmployeDept[i].DICT_CODE == item.APPLY_DEPT_ID) {
                                return vEmployeDept[i].DICT_TEXT;
                            }
                        }
                    }
                    return item.APPLY_DEPT_ID;
                }
                },
           { display: '拟验收时间', name: 'APPLY_DATE', width: 120, minWidth: 60 },
                { display: '流转情况', name: 'STATUS', width: 80, minWidth: 60, render: function (item) {
                    if (item.STATUS == "0") {
                        return "<a style='color:Red'>未流转</a>";
                    }
                    return item.STATUS;
                }
                }
                ],
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        url: 'PartHandler.ashx?action=GetPartPushViewList&strStatus=0',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: true,
        whenRClickToSelect: true,
        onDblClickRow: function (data, rowindex, rowobj) {
            ModifData();
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

    function GetIngDate() {
        maingrid1 = $("#maingrid1").ligerGrid({
            columns: [
                { display: '采购主题', name: 'APPLY_TITLE', align: 'left', width: 220 },
//                { display: '物料类别', name: 'APPLY_TYPE', width: 160, minWidth: 60, render: function (item) {
//                    if (vEmployePartType != null) {
//                        for (var i = 0; i < vEmployePartType.length; i++) {
//                            if (vEmployePartType[i].DICT_CODE == item.APPLY_TYPE) {
//                                return vEmployePartType[i].DICT_TEXT;
//                            }
//                        }
//                    }
//                    return item.APPLY_TYPE;
//                }
//                },
                { display: '申请人', name: 'APPLY_USER_ID', width: 160, minWidth: 60, render: function (item) {
                    if (vUserList != null) {
                        for (var i = 0; i < vUserList.length; i++) {
                            if (vUserList[i].ID == item.APPLY_USER_ID) {
                                return vUserList[i].REAL_NAME;
                            }
                        }
                    }
                    return item.APPLY_USER_ID;
                }
                },
                { display: '申请部门', name: 'APPLY_DEPT_ID', width: 160, minWidth: 60, render: function (item) {
                    if (vEmployeDept != null) {
                        for (var i = 0; i < vEmployeDept.length; i++) {
                            if (vEmployeDept[i].DICT_CODE == item.APPLY_DEPT_ID) {
                                return vEmployeDept[i].DICT_TEXT;
                            }
                        }
                    }
                    return item.APPLY_DEPT_ID;
                }
                },
           { display: '拟验收时间', name: 'APPLY_DATE', width: 120, minWidth: 60 },
                { display: '流转情况', name: 'STATUS', width: 80, minWidth: 60, render: function (item) {
                    //if (item.STATUS == "1") {
                        return "<a style='color:Red'>流转中</a>";
                    //}
                    //return item.STATUS;
                }
                }
                ],
            width: '100%',
            height: '100%',
            pageSizeOptions: [5, 10, 15, 20],
            pageSize: 10,
            url: 'PartHandler.ashx?action=GetPartPushViewList&strStatus=1,2,3,4,5,6,7,8',
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            toolbar: { items: [
                { id: 'view', text: '查看', click: ViewData, icon: 'archives' },
                { id: 'task', text: '执行情况', click: showPurchaseStep, icon: 'role'}//黄进军添加20141024
                ]
            },
            rownumbers: true,
            checkbox: true,
            whenRClickToSelect: true,
            onDblClickRow: function (data, rowindex, rowobj) {
                ViewData();
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
    }

    //物料采购流程追踪，黄进军添加20141024
    function showPurchaseStep() {
        var selectedConItem;
        if (gridName == "1") {
            selectedConItem = maingrid1.getSelectedRow();
        }
        else if (gridName == "2") {
            selectedConItem = maingrid2.getSelectedRow();
        }
        if (selectedConItem == null) {
            $.ligerDialog.warn('请先选择一条数据！');
            return;
        }
        var flow_id;
        var winHeight;
        $.ajax({
            type: "POST",
            async: false,
            url: webServiceUrl + "?type=getPurchaseInfo&business_id=" + selectedConItem.ID,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != null) {
                    var strList = data.split('|');
                    if (strList.length > 1) {
                        flow_id = strList[0]; //流程ID
                        winHeight = strList[1]; //窗体高度
                    }
                }
            }
        });
        var strUrl = (wfStepUrl + "?ID=" + flow_id);
        var strHeight = $(window).height();
        $(document).ready(function () { $.ligerDialog.open({ title: '采购执行情况', url: strUrl, width: 400, height: strHeight, modal: false }); });
    }

    function GetFinishedDate() {
        maingrid2 = $("#maingrid2").ligerGrid({
            columns: [
                { display: '采购主题', name: 'APPLY_TITLE', align: 'left', width: 220 },
//                { display: '物料类别', name: 'APPLY_TYPE', width: 160, minWidth: 60, render: function (item) {
//                    if (vEmployePartType != null) {
//                        for (var i = 0; i < vEmployePartType.length; i++) {
//                            if (vEmployePartType[i].DICT_CODE == item.APPLY_TYPE) {
//                                return vEmployePartType[i].DICT_TEXT;
//                            }
//                        }
//                    }
//                    return item.APPLY_TYPE;
//                }
//                },
                { display: '申请人', name: 'APPLY_USER_ID', width: 160, minWidth: 60, render: function (item) {
                    if (vUserList != null) {
                        for (var i = 0; i < vUserList.length; i++) {
                            if (vUserList[i].ID == item.APPLY_USER_ID) {
                                return vUserList[i].REAL_NAME;
                            }
                        }
                    }
                    return item.APPLY_USER_ID;
                }
                },
                { display: '申请部门', name: 'APPLY_DEPT_ID', width: 160, minWidth: 60, render: function (item) {
                    if (vEmployeDept != null) {
                        for (var i = 0; i < vEmployeDept.length; i++) {
                            if (vEmployeDept[i].DICT_CODE == item.APPLY_DEPT_ID) {
                                return vEmployeDept[i].DICT_TEXT;
                            }
                        }
                    }
                    return item.APPLY_DEPT_ID;
                }
                },
           { display: '拟验收时间', name: 'APPLY_DATE', width: 80, minWidth: 60 },
                { display: '流转情况', name: 'STATUS', width: 80, minWidth: 60, render: function (item) {
                    if (item.STATUS == "9") {
                        return "<a style='color:Red'>已办结(归档)</a>";
                    }
                    return item.STATUS;
                }
                }
                ],
            width: '100%',
            height: '100%',
            pageSizeOptions: [5, 10, 15, 20],
            pageSize: 10,
            url: 'PartHandler.ashx?action=GetPartPushViewList&strStatus=9',
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            toolbar: { items: [
                { id: 'view', text: '查看', click: ViewData, icon: 'archives' },
                { id: 'export', text: '打印', click: ExportData, icon: 'add' },
                { id: 'task', text: '执行情况', click: showPurchaseStep, icon: 'role'}//黄进军添加20141024
                ]
            },
            rownumbers: true,
            checkbox: true,
            whenRClickToSelect: true,
            onDblClickRow: function (data, rowindex, rowobj) {
                ViewData();
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
    }
    //填报采购申请单
    function AddData() {
        var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true&WF_ID=PARTPLAN';
        top.f_overTab('新增填报采购申请单', surl);
    }
    //填报办公物料申请单
    function AddData_OW() {
        var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true&WF_ID=OA_PARTPLAN';
        top.f_overTab('新增填报办公物料申请单', surl);
    }
    //填报实验物料申请单
    function AddData_EP() {
        var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true&WF_ID=EP_PARTPLAN';
        top.f_overTab('新增填报实验物料申请单', surl);
    }
    //新增工作呈报单
    function AddWorkSubmitData() {
        var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true&WF_ID=WorkSubmit';
        top.f_overTab('工作呈报申请单', surl);
    }
    function ModifData() {

        var rowSelected = null, grid = null;
        if (gridName == "0") {
            rowSelected = maingrid.getSelectedRow()
            grid = maingrid;
        }
        if (gridName == "1") {
            rowSelected = maingrid1.getSelectedRow()
            grid = maingrid1;
        }
        if (gridName == "2") {
            rowSelected = maingrid2.getSelectedRow()
            grid = maingrid2;
        }
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行进行修改！');
        } else {
            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|strtaskID=' + rowSelected.ID + '&WF_ID=PARTPLAN';
            top.f_overTab('修改采购申请单', surl);
//            if (rowSelected.REMARK1 == '0') {   //物料采购
//                if (rowSelected.APPLY_TYPE == '01')  //办公物料
//                {
//                    var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|strtaskID=' + rowSelected.ID + '&WF_ID=OW_PARTPLAN';
//                    top.f_overTab('修改采购计划单', surl);
//                }
//                else if (rowSelected.APPLY_TYPE == '02') {  //实验物料
//                    var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|strtaskID=' + rowSelected.ID + '&WF_ID=EP_PARTPLAN';
//                    top.f_overTab('修改采购计划单', surl);
//                }
//            }
//            else if (rowSelected.REMARK1 == '1') { //呈报
//                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|strtaskID=' + rowSelected.ID + '&WF_ID=WorkSubmit';
//                top.f_overTab('修改呈报单', surl);
//            }
        }

    };
    function ViewData() {
        var rowSelected = null, grid = null;
        if (gridName == "0") {
            rowSelected = maingrid.getSelectedRow()
            grid = maingrid;
        }
        if (gridName == "1") {
            rowSelected = maingrid1.getSelectedRow()
            grid = maingrid1;
        }
        if (gridName == "2") {
            rowSelected = maingrid2.getSelectedRow()
            grid = maingrid2;
        }
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行进行查看！');
        } else {
            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|strtaskID=' + rowSelected.ID + '&WF_ID=PARTPLAN';
            top.f_overTab('查看采购申请单', surl);
//            if (rowSelected.REMARK1 == '0') {   //物料采购
//                if (rowSelected.APPLY_TYPE == '01')  //办公物料
//                {
//                    var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|strtaskID=' + rowSelected.ID + '&WF_ID=OW_PARTPLAN';
//                    top.f_overTab('查看采购计划单', surl);
//                }
//                else if (rowSelected.APPLY_TYPE == '02') {  //实验物料
//                    var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|strtaskID=' + rowSelected.ID + '&WF_ID=EP_PARTPLAN';
//                    top.f_overTab('查看采购计划单', surl);
//                }
//            }
//            else if (rowSelected.REMARK1 == '1') { //呈报
//                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|strtaskID=' + rowSelected.ID + '&WF_ID=WorkSubmit';
//                top.f_overTab('查看呈报单', surl);
//            }
        }

    };
    function DeleteData() {
        var rowSelected = null, grid = null;
        if (gridName == "0") {
            rowSelected = maingrid.getSelectedRow()
            grid = maingrid;
        }
        if (gridName == "1") {
            rowSelected = maingrid1.getSelectedRow()
            grid = maingrid1;
        }
        if (gridName == "2") {
            rowSelected = maingrid2.getSelectedRow()
            grid = maingrid2;
        }
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！'); return;
        }
        $.ligerDialog.confirm('确定要删除该条记录？\r\n', function (result) {
            if (result == true) {
                $.ajax({
                    cache: false,
                    async: false, //设置是否为异步加载,此处必须
                    type: "POST",
                    url: "PartHandler.ashx?action=DeletePartBuyRequstInfor&strtaskID=" + rowSelected.ID,
                    contentType: "application/text; charset=utf-8",
                    dataType: "text",
                    success: function (data) {
                        if (data != "") {
                            grid.loadData();
                        }
                        else {
                            $.ligerDialog.warn('删除失败！');
                        }
                    },
                    error: function (msg) {
                        $.ligerDialog.warn('Ajax访问失败！' + msg);
                    }
                });
            }
            else {
                return;
            }
        })
    };
    function showDetailSrh() { };
})
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
        if (gridName == "2") {
            rowSelected = maingrid2.getSelectedRow()
            grid = maingrid2;
        }

        if (rowSelected == null) {
            $.ligerDialog.warn('打印之前请先选择发文');
            return;
        }
        $("#hidFwId").val(rowSelected.ID);
        $("#btnExport").click();
    }
