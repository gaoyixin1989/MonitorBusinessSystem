//Create By 胡方扬 年度培训计划列表
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
//工作流
var strWf = "", wfArr = null;
var maingrid = null, maingrid1 = null, maingrid2 = null, vTypeItems = null, vExamTypeItems = null, vEStatusItems = null, vEmployeList = null, vEmployeDept = null;
var vEmployeList = null, vSubEmploye = null, strEmployePostName = "";
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

    $("#topmenu").ligerMenuBar({ items: [
                 { id: 'add', text: '增加', click: AddData, icon: 'add' },
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


    //获取培训分类
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=TrainType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vTypeItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    //获取考核办法
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetDict&type=TrainExamType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vExamTypeItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });


    //获取员工档案列表
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Employe/EmployeHander.ashx?action=GetEmployeInfor",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vEmployeList = data.Rows;
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
                { display: '培训主题', name: 'TRAIN_BT', align: 'left', width: 200 },
                { display: '计划年度', name: 'PLAN_YEAR', width: 80 },
                { display: '培训分类', name: 'TRAIN_TYPE', width: 80, minWidth: 60, render: function (item) {
                    if (vTypeItems != null) {
                        for (var i = 0; i < vTypeItems.length; i++) {
                            if (vTypeItems[i].DICT_CODE == item.TRAIN_TYPE) {
                                return vTypeItems[i].DICT_TEXT;
                            }
                        }
                    }
                    return item.TRAIN_TYPE;
                }
                },
                { display: '培训对象', name: 'TRAIN_TO', align: 'left', width: 120, minWidth: 60, render: function (item) {
                    var strUserIdArr = item.TRAIN_TO.split(";");
                    strEmployeNames = "";
                    if (vEmployeList != null && strUserIdArr.length > 0) {
                        for (var n = 0; n < strUserIdArr.length; n++) {
                            for (var i = 0; i < vEmployeList.length; i++) {
                                if (vEmployeList[i].ID == strUserIdArr[n]) {
                                    strEmployeNames += vEmployeList[i].EMPLOYE_NAME + ";";
                                }
                            }
                        }
                        return strEmployeNames;
                    }
                    return item.TRAIN_TO;
                }
                },
                { display: '培训内容', name: 'TRAIN_INFO', align: 'left', width: 200, minWidth: 60, render: function (item) {
                    var strInfor = item.TRAIN_INFO;
                    if (strInfor.length > 15) {
                        strInfor = item.TRAIN_INFO.substring(0, 15) + "...";
                    }
                    return "<a title='详细:" + item.TRAIN_INFO + "'>" + strInfor + "<a/>"
                }
                },
                { display: '培训目标', name: 'TRAIN_TARGET', align: 'left', width: 200, minWidth: 60,render: function (item) {
                    var strInfor = item.TRAIN_TARGET;
                    if (strInfor.length > 15) {
                        strInfor = item.TRAIN_TARGET.substring(0, 15) + "...";
                    }
                    return "<a title='详细:" + item.TRAIN_TARGET + "'>" + strInfor + "<a/>"
                } 
                },
                { display: '负责部门', name: 'DEPT_ID', width: 120, minWidth: 60, render: function (item) {
                    if (vEmployeDept != null) {
                        for (var i = 0; i < vEmployeDept.length; i++) {
                            if (vEmployeDept[i].DICT_CODE == item.DEPT_ID) {
                                return vEmployeDept[i].DICT_TEXT;
                            }
                        }
                    }
                    return item.TRAIN_TYPE;
                }
                },
                { display: '考核办法', name: 'EXAMINE_METHOD', width: 80, minWidth: 60, render: function (item) {
                    if (vExamTypeItems != null) {
                        for (var i = 0; i < vExamTypeItems.length; i++) {
                            if (vExamTypeItems[i].DICT_CODE == item.EXAMINE_METHOD) {
                                return vExamTypeItems[i].DICT_TEXT;
                            }
                        }
                    }
                    return item.TRAIN_TYPE;
                }
                },
                { display: '流转情况', name: 'FLOW_STATUS', width: 80, minWidth: 60, render: function (item) {
                    if (item.FLOW_STATUS == "0") {
                        return "<a style='color:Red'>未流转</a>";
                    }
                    return item.FLOW_STATUS;
                }
                },
                { display: '审批情况', name: 'APP_FLOW', width: 80, minWidth: 60 }
                ],
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        url: 'TrainHander.ashx?action=GetTrainViewList&strFlowStatus=0&strTypes=1',
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
                { display: '培训主题', name: 'TRAIN_BT', align: 'left', width: 200 },
                { display: '计划年度', name: 'PLAN_YEAR', width: 80 },
                { display: '培训分类', name: 'TRAIN_TYPE', width: 80, minWidth: 60, render: function (item) {
                    if (vTypeItems != null) {
                        for (var i = 0; i < vTypeItems.length; i++) {
                            if (vTypeItems[i].DICT_CODE == item.TRAIN_TYPE) {
                                return vTypeItems[i].DICT_TEXT;
                            }
                        }
                    }
                    return item.TRAIN_TYPE;
                }
                },
                { display: '培训对象', name: 'TRAIN_TO', align: 'left', width: 120, minWidth: 60, render: function (item) {
                    var strUserIdArr = item.TRAIN_TO.split(";");
                    strEmployeNames = "";
                    if (vEmployeList != null && strUserIdArr.length > 0) {
                        for (var n = 0; n < strUserIdArr.length; n++) {
                            for (var i = 0; i < vEmployeList.length; i++) {
                                if (vEmployeList[i].ID == strUserIdArr[n]) {
                                    strEmployeNames += vEmployeList[i].EMPLOYE_NAME + ";";
                                }
                            }
                        }
                        return strEmployeNames;
                    }
                    return item.TRAIN_TO;
                }
                },
                { display: '培训内容', name: 'TRAIN_INFO', align: 'left', width: 200, minWidth: 60, render: function (item) {
                    var strInfor = item.TRAIN_INFO;
                    if (strInfor.length > 15) {
                        strInfor = item.TRAIN_INFO.substring(0, 15) + "...";
                    }
                    return "<a title='详细:" + item.TRAIN_INFO + "'>" + strInfor + "<a/>"
                }
                },
                { display: '培训目标', name: 'TRAIN_TARGET', align: 'left', width: 200, minWidth: 60, render: function (item) {
                    var strInfor = item.TRAIN_TARGET;
                    if (strInfor.length > 15) {
                        strInfor = item.TRAIN_TARGET.substring(0, 15) + "...";
                    }
                    return "<a title='详细:" + item.TRAIN_TARGET + "'>" + strInfor + "<a/>"
                }
                },
                { display: '负责部门', name: 'DEPT_ID', width: 120, minWidth: 60, render: function (item) {
                    if (vEmployeDept != null) {
                        for (var i = 0; i < vEmployeDept.length; i++) {
                            if (vEmployeDept[i].DICT_CODE == item.DEPT_ID) {
                                return vEmployeDept[i].DICT_TEXT;
                            }
                        }
                    }
                    return item.TRAIN_TYPE;
                }
                },
                { display: '考核办法', name: 'EXAMINE_METHOD', width: 80, minWidth: 60, render: function (item) {
                    if (vExamTypeItems != null) {
                        for (var i = 0; i < vExamTypeItems.length; i++) {
                            if (vExamTypeItems[i].DICT_CODE == item.EXAMINE_METHOD) {
                                return vExamTypeItems[i].DICT_TEXT;
                            }
                        }
                    }
                    return item.TRAIN_TYPE;
                }
                },
                { display: '流转情况', name: 'FLOW_STATUS', width: 80, minWidth: 60, render: function (item) {
                    if (item.FLOW_STATUS == "1") {
                        return "<a style='color:Red'>流转中</a>";
                    }
                    return item.FLOW_STATUS;
                }
                },
                { display: '审批情况', name: 'APP_FLOW', width: 80, minWidth: 60 }
                ],
            width: '100%',
            height: '100%',
            pageSizeOptions: [5, 10, 15, 20],
            pageSize: 10,
            url: 'TrainHander.ashx?action=GetTrainViewList&strFlowStatus=1&strTypes=1',
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
                { display: '培训主题', name: 'TRAIN_BT', align: 'left', width: 200 },
                { display: '计划年度', name: 'PLAN_YEAR', width: 80 },
                { display: '培训分类', name: 'TRAIN_TYPE', width: 80, minWidth: 60, render: function (item) {
                    if (vTypeItems != null) {
                        for (var i = 0; i < vTypeItems.length; i++) {
                            if (vTypeItems[i].DICT_CODE == item.TRAIN_TYPE) {
                                return vTypeItems[i].DICT_TEXT;
                            }
                        }
                    }
                    return item.TRAIN_TYPE;
                }
                },
                { display: '培训对象', name: 'TRAIN_TO', align: 'left', width: 120, minWidth: 60, render: function (item) {
                    var strUserIdArr = item.TRAIN_TO.split(";");
                    strEmployeNames = "";
                    if (vEmployeList != null && strUserIdArr.length > 0) {
                        for (var n = 0; n < strUserIdArr.length; n++) {
                            for (var i = 0; i < vEmployeList.length; i++) {
                                if (vEmployeList[i].ID == strUserIdArr[n]) {
                                    strEmployeNames += vEmployeList[i].EMPLOYE_NAME + ";";
                                }
                            }
                        }
                        return strEmployeNames;
                    }
                    return item.TRAIN_TO;
                }
                },
                { display: '培训内容', name: 'TRAIN_INFO', align: 'left', width: 200, minWidth: 60, render: function (item) {
                    var strInfor = item.TRAIN_INFO;
                    if (strInfor.length > 15) {
                        strInfor = item.TRAIN_INFO.substring(0, 15) + "...";
                    }
                    return "<a title='详细:" + item.TRAIN_INFO + "'>" + strInfor + "<a/>"
                }
                },
                { display: '培训目标', name: 'TRAIN_TARGET', align: 'left', width: 200, minWidth: 60, render: function (item) {
                    var strInfor = item.TRAIN_TARGET;
                    if (strInfor.length > 15) {
                        strInfor = item.TRAIN_TARGET.substring(0, 15) + "...";
                    }
                    return "<a title='详细:" + item.TRAIN_TARGET + "'>" + strInfor + "<a/>"
                }
                },
                { display: '负责部门', name: 'DEPT_ID', width: 120, minWidth: 60, render: function (item) {
                    if (vEmployeDept != null) {
                        for (var i = 0; i < vEmployeDept.length; i++) {
                            if (vEmployeDept[i].DICT_CODE == item.DEPT_ID) {
                                return vEmployeDept[i].DICT_TEXT;
                            }
                        }
                    }
                    return item.TRAIN_TYPE;
                }
                },
                { display: '考核办法', name: 'EXAMINE_METHOD', width: 80, minWidth: 60, render: function (item) {
                    if (vExamTypeItems != null) {
                        for (var i = 0; i < vExamTypeItems.length; i++) {
                            if (vExamTypeItems[i].DICT_CODE == item.EXAMINE_METHOD) {
                                return vExamTypeItems[i].DICT_TEXT;
                            }
                        }
                    }
                    return item.TRAIN_TYPE;
                }
                },
                { display: '流转情况', name: 'FLOW_STATUS', width: 80, minWidth: 60, render: function (item) {
                    if (item.FLOW_STATUS == "9") {
                        return "<a style='color:Red'>已办结(归档)</a>";
                    }
                    return item.FLOW_STATUS;
                }
                },
                { display: '审批情况', name: 'APP_FLOW', width: 80, minWidth: 60 }
                ],
            width: '100%',
            height: '100%',
            pageSizeOptions: [5, 10, 15, 20],
            pageSize: 10,
            url: 'TrainHander.ashx?action=GetTrainViewList&strFlowStatus=9&strTypes=1',
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


    function AddData() {

        var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true&WF_ID=' + wfArr[0];
        top.f_overTab('新增年度培训计划登记单', surl);
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
            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|strtaskID=' + rowSelected.ID + '&WF_ID=' + wfArr[0];
            top.f_overTab('修改年度培训计划登记单', surl);

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

            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|strtaskID=' + rowSelected.ID + '&WF_ID=' + wfArr[0];
            top.f_overTab('查看年度培训计划登记单', surl);

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
                    url: "TrainHander.ashx?action=DeleteTrainInfor&strtaskID=" + rowSelected.ID,
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