/// 站务管理--物料申请列表(秦皇岛)
/// 创建时间：2014-06-12
/// 创建人：魏林

var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var maingrid = null, maingrid1 = null, maingrid2 = null, vTypeItems = null, vEmployeDept = null;
var vUserList = null;
var isFisrt = "", gridName = "0";
var strExamType = "", strEmployeNames = "";
var keshi = "";
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
    keshi = $.getUrlVar('keshi');
    if (keshi == "1") {
        var menu = { width: 120, items:
            [
             { id: 'PARTPLAN', text: '物料采购申请单', click: Add_PARTPLAN, icon: 'bookadd' },
//             { line: true },
//             { id: 'PARTPLAN_EXPER', text: '实验用品申请', click: Add_PARTPLAN_EXPER, icon: 'bookadd' },
             { line: true },
             { id: 'PARTPLAN_OFFICE', text: '办公用品申请', click: Add_PARTPLAN_OFFICE, icon: 'book_tabs' }
            ]
        };
    } else if (keshi == "2") {
        var menu = { width: 120, items:
            [
             { id: 'PARTPLAN', text: '物料采购申请单', click: Add_PARTPLAN, icon: 'bookadd' },
             { line: true },
             { id: 'PARTPLAN_OFFICE', text: '办公用品申请', click: Add_PARTPLAN_OFFICE, icon: 'book_tabs' }
            ]
        };
    } else {
        var menu = { width: 120, items:
            [
             { id: 'PARTPLAN_EXPER', text: '实验用品申请', click: Add_PARTPLAN_EXPER, icon: 'bookadd' },
             { line: true },
             { id: 'PARTPLAN_OFFICE', text: '办公用品申请', click: Add_PARTPLAN_OFFICE, icon: 'book_tabs' }
            ]
        };
    }


    $("#topmenu").ligerMenuBar({ items: [
                { id: 'add', text: '增加', menu: menu, icon: 'add' },
                { id: 'modify', text: '修改', click: ModifData, icon: 'modify' },
                { id: 'del', text: '删除', click: DeleteData, icon: 'delete' }
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
        url: "../../Employe/EmployeHander.ashx?action=GetDict&type=dept",
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
        url: "../PartHandler.ashx?action=GetUserInfor",
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
                { display: '申请主题', name: 'APPLY_TITLE', align: 'left', width: 220 },
                { display: '申请类别', name: 'APPLY_TYPE', width: 160, minWidth: 60, render: function (item) {
                    if (item.APPLY_TYPE == "01")
                    { return "物料采购"; }
                    if (item.APPLY_TYPE == "02")
                    { return "实验用品"; }
                    if (item.APPLY_TYPE == "03")
                    { return "办公用品"; }
                }
                },
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
                { display: '申请时间', name: 'APPLY_DATE', width: 120, minWidth: 60 },
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
        url: '../PartHandler.ashx?action=GetPartPushViewList&strStatus=0&keshi='+keshi,
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
                { display: '申请主题', name: 'APPLY_TITLE', align: 'left', width: 220 },
                { display: '申请类别', name: 'APPLY_TYPE', width: 160, minWidth: 60, render: function (item) {
                    if (item.APPLY_TYPE == "01")
                    { return "物料采购"; }
                    if (item.APPLY_TYPE == "02")
                    { return "实验用品"; }
                    if (item.APPLY_TYPE == "03")
                    { return "办公用品"; }
                }
                },
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
                { display: '申请时间', name: 'APPLY_DATE', width: 120, minWidth: 60 },
                { display: '流转情况', name: 'STATUS', width: 80, minWidth: 60, render: function (item) {
                    return "<a style='color:Red'>流转中</a>";
                }
                }
                ],
            width: '100%',
            height: '100%',
            pageSizeOptions: [5, 10, 15, 20],
            pageSize: 10,
            url: '../PartHandler.ashx?action=GetPartPushViewList&strStatus=1,2,3,4,5,6,7,8&keshi=' + keshi,
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            toolbar: { items: [
                { id: 'view', text: '查看', click: ViewData, icon: 'archives' }
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


    function GetFinishedDate() {
        maingrid2 = $("#maingrid2").ligerGrid({
            columns: [
                { display: '申请主题', name: 'APPLY_TITLE', align: 'left', width: 220 },
                { display: '申请类别', name: 'APPLY_TYPE', width: 160, minWidth: 60, render: function (item) {
                    if (item.APPLY_TYPE == "01")
                    { return "物料采购"; }
                    if (item.APPLY_TYPE == "02")
                    { return "实验用品"; }
                    if (item.APPLY_TYPE == "03")
                    { return "办公用品"; }
                }
                },
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
                { display: '申请时间', name: 'APPLY_DATE', width: 80, minWidth: 60 },
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
            url: '../PartHandler.ashx?action=GetPartPushViewList&strStatus=9&keshi=' + keshi,
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            toolbar: { items: [
                { id: 'view', text: '查看', click: ViewData, icon: 'archives' }
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

    //填报物料采购申请单
    function Add_PARTPLAN() {
        var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true|keshi='+keshi+'&WF_ID=PARTPLAN';
        top.f_overTab('新增填报物料采购申请单', surl);
    }
    //填报实验用品申请
    function Add_PARTPLAN_EXPER() {
        var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true|keshi=3&WF_ID=PARTPLAN_EXPER';
        top.f_overTab('新增填报实验用品申请单', surl);
    }
    //新增办公用品申请
    function Add_PARTPLAN_OFFICE() {
        var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true|keshi=2&WF_ID=PARTPLAN_OFFICE';
        top.f_overTab('新增填报办公用品申请单', surl);
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
            if (rowSelected.APPLY_TYPE != '01' || rowSelected.APPLY_TYPE == '01') {   //物料采购  何海亮修改
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|strtaskID=' + rowSelected.ID + '&WF_ID=PARTPLAN';
                top.f_overTab('修改物料采购申请单', surl);
            }
            if (rowSelected.APPLY_TYPE == '02') { //实验用品
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|strtaskID=' + rowSelected.ID + '&WF_ID=PARTPLAN_EXPER';
                top.f_overTab('修改实验用品申请单', surl);
            }
            if (rowSelected.APPLY_TYPE == '03') { //办公用品
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|strtaskID=' + rowSelected.ID + '&WF_ID=PARTPLAN_OFFICE';
                top.f_overTab('修改办公用品申请单', surl);
            }
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
            if (rowSelected.APPLY_TYPE != '01' || rowSelected.APPLY_TYPE == '01') {   //物料采购   何海亮修改
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|strtaskID=' + rowSelected.ID + '&WF_ID=PARTPLAN';
                top.f_overTab('查看物料采购计划单', surl);
            }
            if (rowSelected.APPLY_TYPE == '02') { //实验用品
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|strtaskID=' + rowSelected.ID + '&WF_ID=PARTPLAN_EXPER';
                top.f_overTab('查看实验用品计划单', surl);
            }
            if (rowSelected.APPLY_TYPE == '03') { //办公用品
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|strtaskID=' + rowSelected.ID + '&WF_ID=PARTPLAN_OFFICE';
                top.f_overTab('查看办公用品计划单', surl);
            }
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
                    url: "../PartHandler.ashx?action=DeletePartBuyRequstInfor&strtaskID=" + rowSelected.ID,
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

})

