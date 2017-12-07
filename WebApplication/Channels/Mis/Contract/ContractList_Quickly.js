var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var manager = null, manager2 = null, manager3 = null, tempGrid = null;
var gridName = "0";
var vContratTypeItem = null, vMonitorType = null, vCompanyItems = null, vAutoYearItem = null;
var isFisrt = true;
var strWf = "", wfArr = null;

$(document).ready(function () {

    //是否现在验收类委托书启用流程
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MethodHander.ashx?action=GetWebConfigValue&strKey=YSTypeShow",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                strYsShowStatus = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    var menu1 = { width: 120, items:
            [
            { id: 'addRoutine', text: '通用类委托书', click: AddData, icon: 'bookadd' },
             { line: true },
        //            { id: 'addCheck', text: '验收类', click: AddData, icon: 'book_tabs' },
        //            { line: true },
            {id: 'addSample', text: '送样类委托书', click: AddData, icon: 'book_red' }
            ]
    };

    if (strYsShowStatus == "1") {
        menu1 = { width: 120, items:
            [
            { id: 'addRoutine', text: '通用类委托书', click: AddData, icon: 'bookadd' },
             { line: true },
            { id: 'addCheck', text: '验收类委托书', click: AddData, icon: 'book_tabs' },
            { line: true },
            { id: 'addSample', text: '送样类委托书', click: AddData, icon: 'book_red' }
            ]
        };
    }

    $("#topmenu").ligerMenuBar({ items: [
                 { id: 'add', text: '增加', menu: menu1, icon: 'add' },
                { id: 'modify', text: '修改', click: ModifData, icon: 'modify' },
                { id: 'del', text: '删除', click: DeleteData, icon: 'delete' },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
            ]
    });
    var tab;
    //窗口改变时的处理函数
    function f_heightChanged(options) {
        if (tab)
            tab.addHeight(options.diff);
    }

    var topHeight = $(window).height() / 2;
    var gridHeight = $(window).height() / 2;

    $("#layout1").ligerLayout({ topHeight: topHeight, leftWidth: "100%", allowLeftCollapse: false, allowRightCollapse: false, height: "100%", onHeightChanged: f_heightChanged });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MethodHander.ashx?action=GetDict&type=Contract_Type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vContratTypeItem = data;
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
        url: "MethodHander.ashx?action=GetMonitorType",
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

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MethodHander.ashx?action=GetContratYear",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vAutoYearItem = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    function GetVCompanyItem(strContractId) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MethodHander.ashx?action=GetContractCompany&strContratId=" + strContractId + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    vCompanyItems = data;
                    // return vCompanyItems;
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
    window['g1'] =
    manager = $("#maingrid1").ligerGrid({
        columns: [
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 300, minWidth: 180 },
                { display: '合同号', name: 'CONTRACT_CODE', align: 'left', width: 160, minWidth: 100 },
                { display: '合同年度', name: 'CONTRACT_YEAR', width: 60, minWidth: 40 },
                { display: '委托类型', name: 'CONTRACT_TYPE', wdth: 100, minWidth: 80, data: vContratTypeItem, render: function (item) {
                    for (var i = 0; i < vContratTypeItem.length; i++) {
                        if (vContratTypeItem[i]['DICT_CODE'] == item.CONTRACT_TYPE)
                            return vContratTypeItem[i]['DICT_TEXT'];
                    }
                    return item.CONTRACT_TYPE;
                }
                },
                { display: '监测类型', name: 'TEST_TYPES', align: 'left', width: 140, minWidth: 100, data: vMonitorType, render: function (item) {
                    var typeName = "";
                    for (var i = 0; i < vMonitorType.length; i++) {
                        var strTestTypes = item.TEST_TYPES.split(';');
                        for (var n = 0; n < strTestTypes.length; n++) {
                            if (vMonitorType[i]['ID'] == strTestTypes[n]) {
                                typeName += vMonitorType[i]['MONITOR_TYPE_NAME'] + ";";
                            }
                        }
                    }
                    if (typeName != "") { return typeName = typeName.substring(0, typeName.length - 1); }
                    else
                    { return item.TEST_TYPES; }
                }
                },
                { display: '委托单位', name: 'CLIENT_COMPANY_ID', align: 'left', width: 120, minWidth: 180, render: function (item) {
                    GetVCompanyItem(item.ID);
                    for (var i = 0; i < vCompanyItems.length; i++) {
                        if (vCompanyItems[i].ID == item.CLIENT_COMPANY_ID)
                            return vCompanyItems[i].COMPANY_NAME;
                    }
                    return item.CLIENT_COMPANY_ID;
                }
                },
                { display: '受检单位', name: 'TESTED_COMPANY_ID', align: 'left', width: 120, minWidth: 180, render: function (item) {
                    GetVCompanyItem(item.ID);
                    for (var i = 0; i < vCompanyItems.length; i++) {
                        if (vCompanyItems[i].ID == item.TESTED_COMPANY_ID)
                            return vCompanyItems[i].COMPANY_NAME;
                    }
                    return item.TESTED_COMPANY_ID;
                }
                },
            { display: '是否快捷录入', name: 'ISQUICKLY', width: 100, minWidth: 80, render: function (item) {
                if (item.ISQUICKLY == '1') {
                    return "<a style='color:Red'>是</a>";
                } else {
                    return "否";
                }
                return item.ISQUICKLY;
            }
            },
            { display: '流程状态', name: 'CONTRACT_SATAUS', width: 100, minWidth: 80, render: function (item) {
                if (item.CONTRACT_STATUS == '9') {
                    return "<a style='color:Red'>已办结</a>";
                }
                return item.CONTRACT_STATUS;
            }
            }
                ],
        width: '100%',
        height: '50%',
        pageSizeOptions: [5, 10, 15, 20],
        url: 'MethodHander.ashx?action=GetContractListData&strStatus=9&strQuck=1',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        onDblClickRow: function (data, rowindex, rowobj) {
            ModifData();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            LoadGridData(rowdata.ID, rowdata.BOOKTYPE);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );

    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll


    function AddData(item) {
        switch (item.id) {
            case 'addRoutine':
                var surl = '../Channels/Mis/Contract/QuicklyCreate/ContractInfor_Quickly.aspx?type=true';
                top.f_overTab('新增常规类委托书', surl);
                //                top.f_addTab(tabid, '新增常规类委托书', surl);
                break;
            case 'addCheck':
                //                var tabid = "tabidContractCheck";
                var surl = '../Channels/Mis/Contract/QuicklyCreate/ContractInfor_Quickly.aspx?type=true';
                if (strYsShowStatus == "1") {
                    surl = '../Channels/Mis/Contract/QuicklyCreate/ContractInfor_Quickly.aspx?type=true&idShow=1';
                }
                top.f_overTab('新增验收类委托书', surl);
                //top.f_addTab(tabid, '新增验收类委托书', surl);
                break;
            case 'addSample':
                var tabid = "tabidContractSample";
                var surl = '../Channels/Mis/SinceSample/QuicklyCreate/ContractInfor_QuicklySince.aspx?type=true';
                top.f_overTab('新增自送样类委托书', surl);
                //                top.f_addTab(tabid, '新增自送样类委托书', surl);
                break;
            default:
                break;
        }
    }
    function ModifData() {
        var rowSelected = manager.getSelectedRow();
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！'); return;
        }
        //        var strQuestrow = jQuery.param(rowSelected);
        var strQuestrow = 'strContratId=' + rowSelected.ID;
        strQuestrow = strQuestrow.replace(/&/g, "|"); //将所有的&符号转化成|，将在后台进行解析
        if (rowSelected.BOOKTYPE == '0') {
            //            var tabid = "tabidContractModify0"
            var surl = '../Channels/Mis/Contract/QuicklyCreate/ContractInfor_Quickly.aspx?ype=false&view=false&' + strQuestrow + '';
            top.f_overTab('常规类委托书修改', surl);
            //            top.f_addTab(tabid, '常规类委托书修改', surl);
        }
        if (rowSelected.BOOKTYPE == '1') {

            //            var tabid = "tabidContractModify1"
            var surl = '../Channels/Mis/Contract/Acceptance/ContractInfor_QuicklyAcceptance.aspx?ype=false&view=false&' + strQuestrow + '';
            top.f_overTab('验收类委托书修改', surl);
            //top.f_addTab(tabid, '验收类委托书修改', surl);
        }
        if (rowSelected.BOOKTYPE == '2') {
            //            var tabid = "tabidContractModify2"
            var surl = '../Channels/Mis/SinceSample/QuicklyCreate/ContractInfor_QuicklySince.aspx?ype=false&view=false&' + strQuestrow + '';
            top.f_overTab('自送样类委托书修改', surl);
            //            top.f_addTab(tabid, '自送样类委托书修改', surl);
        }
    }


    function DeleteData() {
        var rowSelected = manager.getSelectedRow();
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！'); return;
        }
        $.ligerDialog.confirm('确定要删除该条记录？\r\n', function (result) {
            if (result == true) {
                $.ajax({
                    cache: false,
                    async: false, //设置是否为异步加载,此处必须
                    type: "POST",
                    url: "MethodHander.ashx?action=DeleteContractInfor&strContratId=" + rowSelected.ID + "",
                    contentType: "application/text; charset=utf-8",
                    dataType: "text",
                    success: function (data) {
                        if (data != "") {
                            manager.loadData();
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
                inputWidth: 170, labelWidth: 90, space: 40,
                fields: [
                     { display: "项目名称", name: "SEA_PROJECT_NAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "合同编号", name: "SEA_CONTRACT_CODE", newline: false, type: "text" },
                     { display: "合同年度", name: "SEA_CONTRACT_YEAR", newline: true, type: "select", comboboxName: "SEA_CONTRACT_YEAR_BOX", options: { valueFieldID: "SEA_CONTRACT_YEAR_OP", valueField: "ID", textField: "YEAR", data: vAutoYearItem} },
                     { display: "委托类型", name: "SEA_CONTRACT_TYPE", newline: false, type: "select", comboboxName: "SEA_CONTRACT_TYPE_BOX", options: { valueFieldID: "SEA_CONTRACT_TYPE_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vContratTypeItem} },
                     { display: "委托单位", name: "SEA_CLIENT_COMPANYNAME", newline: true, type: "text" },
                     { display: "受检单位", name: "SEA_TEST_COMPANYNAME", newline: false, type: "text" }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 660, height: 240, top: 90, title: "委托书查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SEA_PROJECT_NAME = encodeURI($("#SEA_PROJECT_NAME").val());
            var SEA_CONTRACT_CODE = encodeURI($("#SEA_CONTRACT_CODE").val());
            var SEA_CONTRACT_YEAR_OP = encodeURI($("#SEA_CONTRACT_YEAR_BOX").val());
            var SEA_CONTRACT_TYPE_OP = $("#SEA_CONTRACT_TYPE_OP").val();
            var SEA_CLIENT_COMPANYNAME = encodeURI($("#SEA_CLIENT_COMPANYNAME").val());
            var SEA_TEST_COMPANYNAME = encodeURI($("#SEA_TEST_COMPANYNAME").val());


            var url = "MethodHander.ashx?action=GetContractListData&strStatus=9&strProjectName=" + SEA_PROJECT_NAME + "&strContractCode=" + SEA_CONTRACT_CODE + "&strContratYear=" + SEA_CONTRACT_YEAR_OP + "&strContratType=" + SEA_CONTRACT_TYPE_OP + "&strCompanyName=" + SEA_CLIENT_COMPANYNAME + "&strCompanyNameFrim=" + SEA_TEST_COMPANYNAME;
            manager.set('url', url)
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_PROJECT_NAME").val("");
        $("#SEA_CONTRACT_CODE").val("");
        $("#SEA_TEST_COMPANYNAME").val("");
        $("#SEA_CLIENT_COMPANYNAME").val("");
        $("#SEA_CONTRACT_YEAR_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_CONTRACT_TYPE_BOX").ligerGetComboBoxManager().setValue("");
    }

    /* Start 以下是添加根据委托书添加任务函数接口*/
    function LoadGridData(strtask_id, strBookType) {
        //        alert('委托书ID是':strtask_id+',委托类型是:'+strBookType);   //strBookType=0表示常规类委托书，1表示验收类，2表示自送样类
    }
    /* End 添加根据委托书添加任务函数接口*/
});
