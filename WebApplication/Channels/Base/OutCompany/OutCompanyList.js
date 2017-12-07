// Create by 潘德军 2012.11.06  "分包单位管理"功能

var mainmanager, submanager;
var actionOutID, actionCompanyName, actionCompanyCode, actionLinkMan, actionPhone, actionPost, actionAddress, actionAptitude;
var actionAllowID, actionOutCompanyID,actionOutCompanyName, actionQualification, actionProject, actionQC, actionIsOk, actionCheckUserID,actionCheckDate,actionComplete,actionAppInfo,actionAppUserID,actionAppDate;
var mainmenu, submenu;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    var topHeight = $(window).height() / 2;
    var gridHeight = $(window).height() / 2;

    $("#layout1").ligerLayout({ height: '100%', topHeight: topHeight });

    //maingrid的菜单
    mainmenu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: itemclick_OfMenu_main, icon: 'modify' },
            { line: true },
            { id: 'menudel', text: '删除', click: itemclick_OfMenu_main, icon: 'delete' }
            ]
    });

    //subgrid的菜单
    submenu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: itemclick_OfMenu_sub, icon: 'modify' },
            { line: true },
            { id: 'menudel', text: '删除', click: itemclick_OfMenu_sub, icon: 'delete' }
            ]
    });

    //分包单位grid
    window['g'] =
    mainmanager = $("#maingrid").ligerGrid({
        columns: [
        { display: '分包单位', name: 'COMPANY_NAME', width: 250, align: 'left', isSort: false },
        { display: '法人代码', name: 'COMPANY_CODE', width: 100, align: 'left', isSort: false },
        { display: '联系人', name: 'LINK_MAN', width: 100, align: 'left', isSort: false },
        { display: '联系电话', name: 'PHONE', width: 100, align: 'left', isSort: false },
        { display: '邮编', name: 'POST', width: 100, align: 'left', isSort: false },
        { display: '地址', name: 'ADDRESS', width: 100, align: 'left', isSort: false },
        { display: '资质', name: 'APTITUDE', width: 450, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 8, 10, 20], height: gridHeight,
        url: 'OutCompanyList.aspx?Action=GetOutCompany',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        title: '分包单位',
        toolbar: { items: [
                { id: 'add', text: '增加', click: itemclick_OfToolbar_Main, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: itemclick_OfToolbar_Main, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: itemclick_OfToolbar_Main, icon: 'delete' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            actionOutID = parm.data.ID;
            actionCompanyName = parm.data.COMPANY_NAME;
            actionCompanyCode = parm.data.COMPANY_CODE;
            actionLinkMan = parm.data.LINK_MAN;
            actionPhone = parm.data.PHONE;
            actionPost = parm.data.POST;
            actionAddress = parm.data.ADDRESS;
            actionAptitude = parm.data.APTITUDE;

            mainmenu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetailMain({
                ID: data.ID,
                COMPANY_NAME: data.COMPANY_NAME,
                COMPANY_CODE: data.COMPANY_CODE,
                LINK_MAN: data.LINK_MAN,
                PHONE: data.PHONE,
                POST: data.POST,
                ADDRESS: data.ADDRESS,
                APTITUDE: data.APTITUDE
            }, false);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            var selectedItem = mainmanager.getSelectedRow();
            submanager.set('url', "OutCompanyList.aspx?Action=GetAllow&selCompanyID=" + selectedItem.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //资质grid
    submanager = $("#subgrid").ligerGrid({
        columns: [
        { display: '分包单位', name: 'COMPANY_NAME', width: 200, align: 'left', isSort: false },
        { display: '资质变化情况', name: 'QUALIFICATIONS_INFO', width: 300, align: 'left', isSort: false },
        { display: '主要项目情况', name: 'PROJECT_INFO', width: 300, align: 'left', isSort: false },
        { display: '质保体系情况', name: 'QC_INFO', width: 300, align: 'left', isSort: false },
        { display: '是否通过评审', name: 'IS_OK', width: 100, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 8, 10, 20], height: gridHeight, heightDiff: -10,
        url: 'OutCompanyList.aspx?Action=GetAllow',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        title: '资质查新',
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

            actionAllowID = parm.data.ID;
            actionOutCompanyID = parm.data.OutCompany_ID;
            actionOutCompanyName = parm.data.COMPANY_NAME;
            actionQualification = parm.data.QUALIFICATIONS_INFO;
            actionProject = parm.data.PROJECT_INFO;
            actionQC = parm.data.QC_INFO;
            actionIsOk = parm.data.IS_OK;
            actionCheckUserID = parm.data.CHECK_USER_ID;
            actionCheckDate = parm.data.CHECK_DATE;
            actionComplete = parm.data.COMPLETE_INFO;
            actionAppInfo = parm.data.APP_INFO;
            actionAppUserID = parm.data.APP_USER_ID;
            actionAppDate = parm.data.APP_DATE;

            submenu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetailSub({
                ID: data.ID,
                OutCompany_ID: data.OutCompany_ID,
                COMPANY_NAME: data.COMPANY_NAME,
                QUALIFICATIONS_INFO: data.QUALIFICATIONS_INFO,
                PROJECT_INFO: data.PROJECT_INFO,
                QC_INFO: data.QC_INFO,
                IS_OK: data.IS_OK,
                CHECK_USER_ID: data.CHECK_USER_ID,
                CHECK_DATE: data.CHECK_DATE,
                COMPLETE_INFO: data.COMPLETE_INFO,
                APP_INFO: data.APP_INFO,
                APP_USER_ID: data.APP_USER_ID,
                APP_DATE: data.APP_DATE
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