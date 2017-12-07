// Create by 潘德军 2012.11.07  "服务商管理"功能

var mainmanager, submanager;
var actionSupplierID,actionSupplierName,actionType,actionProducts,actionLinkMan,actionTel,actionAddress,actionFax,actionEmail,actionPost,actionBank,actionAccountFor;
var actionJudgeID,actionSupplierID,actionPartName,actionModel,actionPrice,actionStandard,actionPeriod,actionQuanlity,actionEnterprisecode,actionQuatitySystem,actionSincerity;
var mainmenu, submenu;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    var topHeight = $(window).height() / 2;
    var gridHeight = $(window).height() / 2 ;

    $("#layout1").ligerLayout({ height: '100%', topHeight: topHeight });

    //maingrid的菜单
    mainmenu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: itemclick_OfMenu_main, icon: 'modify' },
            { id: 'menudel', text: '删除', click: itemclick_OfMenu_main, icon: 'delete' }
            ]
    });

    //subgrid的菜单
    submenu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: itemclick_OfMenu_sub, icon: 'modify' },
            { id: 'menudel', text: '删除', click: itemclick_OfMenu_sub, icon: 'delete' }
            ]
    });

    //供应商grid
    window['g'] =
    mainmanager = $("#maingrid").ligerGrid({
        columns: [
        { display: '供应商', name: 'SUPPLIER_NAME', width: 250, align: 'left', isSort: false },
        { display: '供应物质类别', name: 'SUPPLIER_TYPE', width: 200, align: 'left', isSort: false },
        { display: '经营范围', name: 'PRODUCTS', width: 400, align: 'left', isSort: false },
        { display: '联系人', name: 'LINK_MAN', width: 100, align: 'left', isSort: false },
        { display: '电话', name: 'TEL', width: 150, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 8, 10, 20, 50], height: gridHeight,
        url: 'SupplierList.aspx?Action=GetSupplier',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        title:"服务商",
        toolbar: { items: [
                { id: 'add', text: '增加', click: itemclick_OfToolbar_Main, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: itemclick_OfToolbar_Main, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: itemclick_OfToolbar_Main, icon: 'delete' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclick_OfToolbar_Main, icon: 'search' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            actionSupplierID = parm.data.ID;
            actionSupplierName = parm.data.SUPPLIER_NAME;
            actionType = parm.data.SUPPLIER_TYPE;
            actionProducts = parm.data.PRODUCTS;
            actionLinkMan = parm.data.LINK_MAN;
            actionTel = parm.data.TEL;
            actionAddress = parm.data.ADDRESS;
            actionFax = parm.data.FAX;
            actionEmail = parm.data.EMAIL;
            actionPost = parm.data.POST_CODE;
            actionBank = parm.data.BANK;
            actionAccountFor = parm.data.ACCOUNT_FOR;

            mainmenu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetailMain({
                ID: data.ID,
                SUPPLIER_NAME: data.SUPPLIER_NAME,
                SUPPLIER_TYPE: data.SUPPLIER_TYPE,
                PRODUCTS: data.PRODUCTS,
                LINK_MAN: data.LINK_MAN,
                TEL: data.TEL,
                ADDRESS: data.ADDRESS,
                FAX: data.FAX,
                EMAIL: data.EMAIL,
                POST_CODE: data.POST_CODE,
                BANK: data.BANK,
                ACCOUNT_FOR: data.ACCOUNT_FOR
            }, false);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            var selectedItem = mainmanager.getSelectedRow();
            submanager.set('url', "SupplierList.aspx?Action=GetJudge&selSuppierID=" + selectedItem.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //评审grid
    submanager = $("#subgrid").ligerGrid({
        columns: [
        { display: '供应商', name: 'SUPPLIER_NAME', width: 200, align: 'left', isSort: false },
        { display: '产品或服务项目', name: 'PARTNAME', width: 400, align: 'left', isSort: false },
        { display: '规格型号', name: 'MODEL', width: 100, align: 'left', isSort: false },
        { display: '参考价', name: 'REFERENCEPRICE', width: 100, align: 'left', isSort: false },
        { display: '生产标准', name: 'PRODUCTSTANDARD', width: 200, align: 'left', isSort: false },
        { display: '最短供货期', name: 'DELIVERYPERIOD', width: 100, align: 'left', isSort: false },
        { display: '供货数量', name: 'QUANTITY', width: 100, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5,8,10,20,50], height: '100%',
        url: 'SupplierList.aspx?Action=GetJudge',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        title: "服务商评价",
        toolbar: { items: [
                { id: 'add', text: '增加', click: itemclick_OfToolbar_sub, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: itemclick_OfToolbar_sub, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: itemclick_OfToolbar_sub, icon: 'delete' },
                 { line: true },
                { text: '附件上传', click: upLoadFile, icon: 'add' },
                { line: true },
                { text: '附件下载', click: downLoadFile, icon: 'add' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            actionJudgeID = parm.data.ID;
            actionSupplierID = parm.data.SUPPLIER_ID;
            actionPartName = parm.data.PARTNAME;
            actionModel = parm.data.MODEL;
            actionPrice = parm.data.REFERENCEPRICE;
            actionStandard = parm.data.PRODUCTSTANDARD;
            actionPeriod = parm.data.DELIVERYPERIOD;
            actionQuanlity = parm.data.QUANTITY;
            actionEnterprisecode = parm.data.ENTERPRISECODE;
            actionQuatitySystem = parm.data.QUATITYSYSTEM;
            actionSincerity = parm.data.SINCERITY;

            submenu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetailSub({
                ID: data.ID,
                SUPPLIER_ID: data.SUPPLIER_ID,
                PARTNAME: data.PARTNAME,
                MODEL: data.MODEL,
                REFERENCEPRICE: data.REFERENCEPRICE,
                PRODUCTSTANDARD: data.PRODUCTSTANDARD,
                DELIVERYPERIOD: data.DELIVERYPERIOD,
                QUANTITY: data.QUANTITY,
                ENTERPRISECODE: data.ENTERPRISECODE,
                QUATITYSYSTEM: data.QUATITYSYSTEM,
                SINCERITY: data.SINCERITY
            }, false);
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