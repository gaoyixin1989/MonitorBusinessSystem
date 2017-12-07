//Create By 胡方扬 人员考核列表
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
//工作流
var strWf = "", wfArr = null;
var maingrid = null, maingrid1 = null, maingrid2 = null, vETypeItems = null, vEStatusItems = null, vEmployeList = null;
var vEmployeList = null, vSubEmploye = null, strEmployePostName = "";
var isFisrt = "", gridName ="0";
var strExamType = "";
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

    var menu1 = { width: 120, items:
            [
            { id: 'addRoutine', text: '事业人员考核', click: AddData, icon: 'bookadd' },
             { line: true },
            { id: 'addCheck', text: '专业人员考核', click: AddData, icon: 'book_tabs' }
            ]
    };

    $("#topmenu").ligerMenuBar({ items: [
                 { id: 'add', text: '增加', menu: menu1, icon: 'add' },
                { id: 'modify', text: '修改', click: ModifData, icon: 'modify' },
                { id: 'del', text: '删除', click: DeleteData, icon: 'delete' }
        //                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
            ]
    });
    strWf = $.getUrlVar('WF_ID');
    if (strWf != "") {
        wfArr = strWf.split('|');
        if (wfArr.length < 3) {
            for (var i = 0; i <= 3 - wfArr.length; i++) {
                wfArr.push("");
            }
        }
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
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=ExamType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vETypeItems = data;
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
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=ExamStatus",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vEStatusItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });


    function GetSubEmployeInfor(strUserID) {

        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "ExamHander.ashx?action=GetSubUserEmployInfor&strUserID=" + strUserID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vSubEmploye = data.Rows;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    maingrid = $("#maingrid").ligerGrid({
        columns: [
                        { display: '主题', name: 'Bt', align: 'left', width: 300, render: function (items) {
                            for (var i = 0; i < vETypeItems.length; i++) {
                                if (vETypeItems[i].DICT_CODE == items.EXAMINE_TYPE) {
                                    GetSubEmployeInfor(items.USERID);
                                    return vETypeItems[i].DICT_TEXT + '(<a style="color:Red">' + vSubEmploye[0].EMPLOYE_NAME + '</a>)' + items.EXAMINE_YEAR + '年度考核';
                                }
                            }
                            return "";
                        }
                        },
                { display: '员工姓名', name: 'EMPLOYENAME', align: 'left', width: 140, render: function (items) {
                    GetSubEmployeInfor(items.USERID);
                    return vSubEmploye[0].EMPLOYE_NAME;
                }
                },
                { display: '考核类别', name: 'EXAMINE_TYPE', width: 200, align: 'left', data: vETypeItems, render: function (items) {
                    for (var i = 0; i < vETypeItems.length; i++) {
                        if (vETypeItems[i].DICT_CODE == items.EXAMINE_TYPE) {
                            return vETypeItems[i].DICT_TEXT;
                        }
                    }
                    return items.EXAMINE_TYPE;
                }
                },
                { display: '考核年度', name: 'EXAMINE_YEAR', width: 120, minWidth: 60 },
                { display: '考核状态', name: 'EXAMINE_STATUS', width: 80, minWidth: 60, render: function (item) {
                    if (item.EXAMINE_STATUS == '0') {
                        return "<a style='color:Red'>未提交</a>";
                    }
                    return item.EXAMINE_STATUS;
                }
                }
                ],
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        url: 'ExamHander.ashx?action=GetExamViewList&strExamStatus=0',
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
                                    { display: '主题', name: 'Bt', align: 'left', width: 300, render: function (items) {
                                        for (var i = 0; i < vETypeItems.length; i++) {
                                            if (vETypeItems[i].DICT_CODE == items.EXAMINE_TYPE) {
                                                GetSubEmployeInfor(items.USERID);
                                                return vETypeItems[i].DICT_TEXT + '(<a style="color:Red">' + vSubEmploye[0].EMPLOYE_NAME + '</a>)' + items.EXAMINE_YEAR + '年度考核';
                                            }
                                        }
                                        return "";
                                    }
                                    },
                { display: '员工姓名', name: 'EMPLOYENAME', width: 140, render: function (items) {
                    GetSubEmployeInfor(items.USERID);
                    return vSubEmploye[0].EMPLOYE_NAME;
                }
                },
                { display: '考核类别', name: 'EXAMINE_TYPE', width: 200, data: vETypeItems, render: function (items) {
                    for (var i = 0; i < vETypeItems.length; i++) {
                        if (vETypeItems[i].DICT_CODE == items.EXAMINE_TYPE) {
                            return vETypeItems[i].DICT_TEXT;
                        }
                    }
                    return items.EXAMINE_TYPE;
                }
                },
                { display: '考核年度', name: 'EXAMINE_YEAR', width: 120, minWidth: 60 },
                { display: '考核状态', name: 'EXAMINE_STATUS', width: 80, minWidth: 60, render: function (item) {
                    if (item.EXAMINE_STATUS == '1') {
                        return "<a style='color:Red'>流转中</a>";
                    }
                    return item.EXAMINE_STATUS;
                }
                }
                ],
            width: '100%',
            height: '100%',
            pageSizeOptions: [5, 10, 15, 20],
            pageSize: 10,
            url: 'ExamHander.ashx?action=GetExamViewList&strExamStatus=1',
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
                                    { display: '主题', name: 'Bt', align: 'left', width: 300, render: function (items) {
                                        for (var i = 0; i < vETypeItems.length; i++) {
                                            if (vETypeItems[i].DICT_CODE == items.EXAMINE_TYPE) {
                                                GetSubEmployeInfor(items.USERID);
                                                return vETypeItems[i].DICT_TEXT + '(<a style="color:Red">' + vSubEmploye[0].EMPLOYE_NAME + '</a>)' + items.EXAMINE_YEAR + '年度考核';
                                            }
                                        }
                                        return "";
                                    }
                                    },
                { display: '员工姓名', name: 'EMPLOYENAME', width: 140, render: function (items) {
                    GetSubEmployeInfor(items.USERID);
                    return vSubEmploye[0].EMPLOYE_NAME;
                }
                },
                { display: '考核类别', name: 'EXAMINE_TYPE', width: 200, data: vETypeItems, render: function (items) {
                    for (var i = 0; i < vETypeItems.length; i++) {
                        if (vETypeItems[i].DICT_CODE == items.EXAMINE_TYPE) {
                            return vETypeItems[i].DICT_TEXT;
                        }
                    }
                    return items.EXAMINE_TYPE;
                }
                },
                { display: '考核年度', name: 'EXAMINE_YEAR', width: 120, minWidth: 60 },
                { display: '考核状态', name: 'EXAMINE_STATUS', width: 80, minWidth: 60, render: function (item) {
                    if (item.EXAMINE_STATUS == '9') {
                        return "<a style='color:Red'>已办结(归档)</a>";
                    }
                    return item.EXAMINE_STATUS;
                }
                }
                ],
            width: '100%',
            height: '100%',
            pageSizeOptions: [5, 10, 15, 20],
            pageSize: 10,
            url: 'ExamHander.ashx?action=GetExamViewList&strExamStatus=9',
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

    function IsExistUser() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "ExamHander.ashx?action=GetUserEmployInfor",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vEmployeList = data.Rows;
                }
                else {
                    $.ligerDialog.warn('数据加载错误！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    function AddData(item) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "ExamHander.ashx?action=GetUserEmployInfor",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vEmployeList = data.Rows;
                    switch (item.id) {
                        case 'addRoutine':
                            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true|strEmployeID=' + vEmployeList[0].ID + '|&WF_ID=' + wfArr[0];
                            top.f_overTab('新增行政人员考核单', surl);
                            break;
                        case 'addCheck':
                            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true|strEmployeID=' + vEmployeList[0].ID + '|&WF_ID=' + wfArr[1];
                            top.f_overTab('新增专业人员考核单', surl);
                            break;
                        default:
                            break;
                    }

                }
                else {
                    $.ligerDialog.warn('档案数据中不存在该用户\r\n，请联系管理员！'); return;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
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
            strExamType = rowSelected.EXAMINE_TYPE;
            if (strExamType == "1") {
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|strtaskID=' + rowSelected.ID + '|strUserID=' + rowSelected.USERID + '|&WF_ID=' + wfArr[0];
                top.f_overTab('修改行政人员考核单', surl);
            }
            else {
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|strtaskID=' + rowSelected.ID + '|strUserID=' + rowSelected.USERID + '|&WF_ID=' + wfArr[1];
                top.f_overTab('修改专业技术人员考核单', surl);
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
            strExamType = rowSelected.EXAMINE_TYPE;
            if (strExamType == "1") {
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|strtaskID=' + rowSelected.ID + '|strUserID=' + rowSelected.USERID + '|&WF_ID=' + wfArr[0];
                top.f_overTab('查看行政人员考核单', surl);
            }
            else {
                var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|strtaskID=' + rowSelected.ID + '|strUserID=' + rowSelected.USERID + '|&WF_ID=' + wfArr[1];
                top.f_overTab('查看专业技术人员考核单', surl);
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
                    url: "ExamHander.ashx?action=DeleteExamInfor&strtaskID=" + rowSelected.ID,
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