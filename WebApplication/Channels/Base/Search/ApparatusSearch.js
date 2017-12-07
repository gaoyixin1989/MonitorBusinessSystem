// Create by 邵世卓 2012.11.28  "仪器查询"功能
var firstManager;
var secondManager;
var thirdManager;

var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {
    var gridHeight = $(window).height() / 2;
    var topHeight = $(window).height() / 2;

    $("#layout1").ligerLayout({ topHeight: topHeight, leftWidth: "50%", rightWidth: "50%", allowLeftCollapse: false, allowRightCollapse: false, height: "100%" });

    //仪器信息grid
    window['g'] =
    firstManager = $("#firstgrid").ligerGrid({
        columns: [
                { display: '仪器编号', name: 'APPARATUS_CODE', align: 'left', width: 100, minWidth: 60 },
                { display: '仪器名称', name: 'NAME', minWidth: 120 },
                { display: '规格型号', name: 'MODEL', minWidth: 140 },
                { display: '仪器供应商', name: 'FITTINGS_PROVIDER' }
                ],
        title:'仪器信息',
        width: '100%',
        height: gridHeight,
        pageSizeOptions: [5, 10, 15, 20],
        url: 'ApparatusSearch.aspx?type=getApparatusInfo',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'srhAll', text: '所有记录', click: itemclick_OfToolbar_UnderItem, icon: 'refresh' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclick_OfToolbar_UnderItem, icon: 'search' },
                { line: true },
                { id: 'view', text: '详细', click: itemclick_OfToolbar_UnderItem, icon: 'archives' }
                ]
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetail({
                APPARATUS_CODE: data.APPARATUS_CODE,
                NAME: data.NAME,
                MODEL: data.MODEL,
                SERIAL_NO: data.SERIAL_NO,
                BELONG_TO: data.BELONG_TO,
                BUY_TIME: data.BUY_TIME,
                SCRAP_TIME: data.SCRAP_TIME,
                CERTIFICATE_TYPE_NAME: getDictText('certificate_Type',data.CERTIFICATE_TYPE),
                TRACE_RESULT_NAME: getDictText('TRACE_RESULT',data.TRACE_RESULT),
                TEST_MODE_NAME: getDictText('test_mode',data.TEST_MODE),
                VERIFY_CYCLE: data.VERIFY_CYCLE,
                DEPT_NAME: getDictText('dept',data.DEPT),
                KEEPER: data.KEEPER,
                POSITION: data.POSITION,
                ARCHIVES_TYPE: data.ARCHIVES_TYPE,
                SORT1_NAME: getDictText('sort1',data.SORT1),
                SORT2: data.SORT2,
                APPARATUS_PROVIDER: data.APPARATUS_PROVIDER,
                FITTINGS_PROVIDER: data.FITTINGS_PROVIDER,
                WEB_SITE: data.WEB_SITE,
                LINK_MAN: data.LINK_MAN,
                LINK_PHONE: data.LINK_PHONE,
                POST: data.POST,
                ADDRESS: data.ADDRESS,
                BEGIN_TIME: data.BEGIN_TIME,
                END_TIME: data.END_TIME,
                VERIFICATION_WAY_NAME: getDictText('VERIFICATION_WAY',data.VERIFICATION_WAY),
                VERIFICATION_RESULT_NAME: getDictText('VERIFICATION_RESULT',data.VERIFICATION_RESULT),
                VERIFICATION_BEGIN_TIME: data.VERIFICATION_BEGIN_TIME,
                VERIFICATION_END_TIME: data.VERIFICATION_END_TIME,
                LINK_MAN: data.LINK_MAN,
                EXPANDED_UNCETAINTY: data.EXPANDED_UNCETAINTY,
                MEASURING_RANGE: data.MEASURING_RANGE,
                EXAMINE_DEPARTMENT: data.EXAMINE_DEPARTMENT,
                DEPARTMENT_PHONE: data.DEPARTMENT_PHONE,
                DEPARTMENT_LINKMAN: data.DEPARTMENT_LINKMAN
            }, false);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            strApparatusId = rowdata.ID;
            //点击的时候加载仪器资料数据
            secondManager.set('url', "ApparatusSearch.aspx?type=getApparatusDocumentInfo&appId=" + rowdata.ID);
            //点击的时候加载仪器检定证书数据
            thirdManager.set('url', "ApparatusSearch.aspx?type=getApparatusCertificInfo&appId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //仪器资料信息
    secondManager = $("#secondgrid").ligerGrid({
        columns: [
               { display: '仪器编号', name: 'APPARATUS_ID', align: 'left', width: 50, minWidth: 60 },
                { display: '仪器名称', name: 'APPARATUS_NAME', minWidth: 120 },
                { display: '资料名称', name: 'APPARATUS_ATT_NAME', minWidth: 140 }
                ],
        title:'',
        width: '100%',
        height: gridHeight,heightDiff: -30,
        pageSizeOptions: [5, 10, 15, 20],
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
         toolbar: { items: [
                { id: 'load1', text: '附件下载', click: itemclick_OfToolbar_UnderItem, icon: 'bookpen' }
                ]
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

    //仪器检定证书
    thirdManager = $("#thirdgrid").ligerGrid({
        columns: [
                { display: '仪器编号', name: 'APPARATUS_CODE', align: 'left', width: 100, minWidth: 60 },
                { display: '仪器名称', name: 'APPARATUS_NAME', minWidth: 120 },
                { display: '检定证书名称', name: 'APPRAISAL_NAME', minWidth: 140 },
                { display: '检定时间', name: 'APPRAISAL_DATE' }
                ],
        title:'',
        width: '100%',
        height: gridHeight,heightDiff: -30,
        pageSizeOptions: [5, 10, 15, 20],
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'load2', text: '附件下载', click: itemclick_OfToolbar_UnderItem, icon: 'bookpen' }
                ]
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


// Create by 邵世卓 2012.11.28  "仪器信息查询、查看"功能

//仪器信息grid 的Toolbar click事件
function itemclick_OfToolbar_UnderItem(item) {
    switch (item.id) {
        case 'view':
            var selected = firstManager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要查看的记录！'); ; return }

            showDetail({
                APPARATUS_CODE: selected.APPARATUS_CODE,
                NAME: selected.NAME,
                MODEL: selected.MODEL,
                SERIAL_NO: selected.SERIAL_NO,
                BELONG_TO: selected.BELONG_TO,
                BUY_TIME: selected.BUY_TIME,
                SCRAP_TIME: selected.SCRAP_TIME,
                CERTIFICATE_TYPE_NAME: getDictText('certificate_Type',selected.CERTIFICATE_TYPE),
                TRACE_RESULT_NAME: getDictText('TRACE_RESULT',selected.TRACE_RESULT),
                TEST_MODE_NAME: getDictText('test_mode',selected.TEST_MODE),
                VERIFY_CYCLE: selected.VERIFY_CYCLE,
                DEPT_NAME: getDictText('dept',selected.DEPT),
                KEEPER: selected.KEEPER,
                POSITION: selected.POSITION,
                ARCHIVES_TYPE: selected.ARCHIVES_TYPE,
                SORT1_NAME: getDictText('sort1',selected.SORT1),
                SORT2: selected.SORT2,
                APPARATUS_PROVIDER: selected.APPARATUS_PROVIDER,
                FITTINGS_PROVIDER: selected.FITTINGS_PROVIDER,
                WEB_SITE: selected.WEB_SITE,
                LINK_MAN: selected.LINK_MAN,
                LINK_PHONE: selected.LINK_PHONE,
                POST: selected.POST,
                ADDRESS: selected.ADDRESS,
                BEGIN_TIME: selected.BEGIN_TIME,
                END_TIME: selected.END_TIME,
                VERIFICATION_WAY_NAME: getDictText('VERIFICATION_WAY',selected.VERIFICATION_WAY),
                VERIFICATION_RESULT_NAME: getDictText('VERIFICATION_RESULT',selected.VERIFICATION_RESULT),
                VERIFICATION_BEGIN_TIME: selected.VERIFICATION_BEGIN_TIME,
                VERIFICATION_END_TIME: selected.VERIFICATION_END_TIME,
                LINK_MAN: selected.LINK_MAN,
                EXPANDED_UNCETAINTY: selected.EXPANDED_UNCETAINTY,
                MEASURING_RANGE: selected.MEASURING_RANGE,
                EXAMINE_DEPARTMENT: selected.EXAMINE_DEPARTMENT,
                DEPARTMENT_PHONE: selected.DEPARTMENT_PHONE,
                DEPARTMENT_LINKMAN: selected.DEPARTMENT_LINKMAN
            }, false);
            break;
        case 'srh':
            showDetailSrh();
            break;
        case 'srhAll':
            firstManager.set('url', "ApparatusSearch.aspx?type=getApparatusInfo");
            break;
        case 'load1':
            downLoadDocumentFile();
            break;
            case'load2':
            downLoadCertificFile();
            break;
        default:
            break;
    }
}

//仪器信息grid 的编辑对话框及save函数
var detailWin = null, curentData = null, currentIsAddNew;
function showDetail(data, isAddNew) {
    curentData = data;
    currentIsAddNew = isAddNew;
    if (detailWin) {
        detailWin.show();
    }
    else {
        //创建表单结构
        var mainform = $("#editFirstForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 120, space: 90, labelAlign: 'right',
            fields: [
                { display: "仪器编号", name: "APPARATUS_CODE1", newline: true, type: "label", group: "基本信息", groupicon: groupicon },
                { display: "仪器名称", name: "NAME1", newline: true, type: "label", width: 530 },
                { display: "规格型号", name: "MODEL1", newline: true, type: "label" },
                { display: "出厂编号", name: "SERIAL_NO1", newline: false, type: "label" },
                { display: "所属仪器或项目", name: "BELONG_TO1", newline: true, type: "label" },
                { display: "购买时间", name: "BUY_TIME1", newline: false, type: "label" },
                { display: "报废时间", name: "SCRAP_TIME1", newline: true, type: "label" },
                { display: "溯源方式", name: "CERTIFICATE_TYPE_NAME1", newline: false, type: "label" },
                { display: "溯源结果", name: "TRACE_RESULT_NAME1", newline: true, type: "label"},
                { display: "检定方式", name: "TEST_MODE_NAME1", newline: false, type: "label" },
                { display: "校正周期", name: "VERIFY_CYCLE1", newline: true, type: "label" },
                { display: "使用科室", name: "DEPT_NAME1", newline: false, type: "label"},
                { display: "保管人", name: "KEEPER1", newline: true, type: "label" },
                { display: "放置地点", name: "POSITION1", newline: false, type: "label" },
                { display: "档案类别", name: "ARCHIVES_TYPE1", newline: true, type: "label", group: "类别信息", groupicon: groupicon },
                { display: "类别1", name: "SORT1_NAME1", newline: false, type: "label"},
                { display: "类别2", name: "SORT2_NAME1", newline: true, type: "label" },
                { display: "仪器供应商", name: "APPARATUS_PROVIDER1", newline: true, type: "label", group: "供应商信息", groupicon: groupicon },
                { display: "配件供应商", name: "FITTINGS_PROVIDER1", newline: false, type: "label" },
                { display: "仪器供应商网址", name: "WEB_SITE1", newline: true, type: "label", width: 530 },
                { display: "联系人", name: "LINK_MAN1", newline: true, type: "label", group: "联系人信息", groupicon: groupicon },
                { display: "联系电话", name: "LINK_PHONE1", newline: false, type: "label" },
                { display: "邮编", name: "POST1", newline: true, type: "label" },
                { display: "联系地址", name: "ADDRESS1", newline: false, type: "label" },
                { display: "最近检定/校准时间", name: "BEGIN_TIME1", newline: true, type: "label", group: "检定信息", groupicon: groupicon },
                { display: "到期检定/校准时间", name: "END_TIME1", newline: false, type: "label" },
                { display: "期间核查方式", name: "VERIFICATION_WAY_NAME1", newline: true, type: "label"},
                { display: "期间核查结果", name: "VERIFICATION_RESULT_NAME1", newline: false, type: "label"},
                { display: "最近期间核查时间", name: "VERIFICATION_BEGIN_TIME1", newline: true, type: "label" },
                { display: "到期期间核查时间", name: "VERIFICATION_END_TIME1", newline: false, type: "label" },
                { display: "扩展不确定度", name: "EXPANDED_UNCETAINTY1", newline: true, type: "label" },
                { display: "测量范围", name: "MEASURING_RANGE1", newline: false, type: "label" },
                { display: "检定单位", name: "EXAMINE_DEPARTMENT1", newline: true, type: "label" },
                { display: "检定单位电话", name: "DEPARTMENT_PHONE1", newline: false, type: "label" },
                { display: "检定单位联系人", name: "DEPARTMENT_LINKMAN1", newline: true, type: "label" }
                ]
        });

        detailWin = $.ligerDialog.open({
            target: $("#detail"),
            width: 700, height: 450, top: 5, title: "仪器信息信息",
            buttons: [
                  { text: '取消', onclick: function () { clearDialogValue(); detailWin.hide(); } }
                  ]
        });
    }
    if (curentData) {
        $("#APPARATUS_CODE1").val(curentData.APPARATUS_CODE);
        $("#NAME1").val(curentData.NAME);
        $("#MODEL1").val(curentData.MODEL);
        $("#SERIAL_NO1").val(curentData.SERIAL_NO);
        $("#BELONG_TO1").val(curentData.BELONG_TO);
        $("#BUY_TIME1").val(curentData.BUY_TIME);
        $("#SCRAP_TIME1").val(curentData.SCRAP_TIME);
        $("#CERTIFICATE_TYPE_NAME1").val(curentData.CERTIFICATE_TYPE_NAME);
        $("#TRACE_RESULT_NAME1").val(curentData.TRACE_RESULT_NAME);
        $("#TEST_MODE_NAME1").val(curentData.TEST_MODE_NAME);
        $("#VERIFY_CYCLE1").val(curentData.VERIFY_CYCLE);
        $("#DEPT_NAME1").val(curentData.DEPT_NAME);
        $("#KEEPER1").val(curentData.KEEPER);
        $("#POSITION1").val(curentData.POSITION);
        $("#ARCHIVES_TYPE1").val(curentData.ARCHIVES_TYPE);
        $("#SORT1_NAME1").val(curentData.SORT1_NAME);
        $("#SORT2_NAME1").val(curentData.SORT2);
        $("#APPARATUS_PROVIDER1").val(curentData.APPARATUS_PROVIDER);
        $("#FITTINGS_PROVIDER1").val(curentData.FITTINGS_PROVIDER);
        $("#WEB_SITE1").val(curentData.WEB_SITE);
        $("#LINK_MAN1").val(curentData.LINK_MAN);
        $("#LINK_PHONE1").val(curentData.LINK_PHONE);
        $("#POST1").val(curentData.POST);
        $("#ADDRESS1").val(curentData.ADDRESS);
        $("#BEGIN_TIME1").val(curentData.BEGIN_TIME);
        $("#END_TIME1").val(curentData.END_TIME);
        $("#VERIFICATION_WAY_NAME1").val(curentData.VERIFICATION_WAY_NAME);
        $("#VERIFICATION_RESULT_NAME1").val(curentData.VERIFICATION_RESULT_NAME);
        $("#VERIFICATION_BEGIN_TIME1").val(curentData.VERIFICATION_BEGIN_TIME);
        $("#VERIFICATION_END_TIME1").val(curentData.VERIFICATION_END_TIME);
        $("#EXPANDED_UNCETAINTY1").val(curentData.EXPANDED_UNCETAINTY);
        $("#MEASURING_RANGE1").val(curentData.MEASURING_RANGE);
        $("#EXAMINE_DEPARTMENT1").val(curentData.EXAMINE_DEPARTMENT);
        $("#DEPARTMENT_PHONE1").val(curentData.DEPARTMENT_PHONE);
        $("#DEPARTMENT_LINKMAN1").val(curentData.DEPARTMENT_LINKMAN);
    }
    else {
        $("#APPARATUS_CODE1").val("");
        $("#NAME1").val("");
        $("#MODEL1").val("");
        $("#SERIAL_NO1").val("");
        $("#BELONG_TO1").val("");
        $("#BUY_TIME1").val("");
        $("#SCRAP_TIME1").val("");
        $("#CERTIFICATE_TYPE_NAME1").val("");
        $("#TRACE_RESULT_NAME1").val("");
        $("#TEST_MODE_NAME1").val("");
        $("#VERIFY_CYCLE1").val("");
        $("#DEPT_NAME1").val("");
        $("#KEEPER1").val("");
        $("#POSITION1").val("");
        $("#ARCHIVES_TYPE1").val("");
        $("#SORT1_NAME1").val("");
        $("#SORT2_NAME1").val("");
        $("#APPARATUS_PROVIDER1").val("");
        $("#FITTINGS_PROVIDER1").val("");
        $("#WEB_SITE1").val("");
        $("#LINK_MAN1").val("");
        $("#LINK_PHONE1").val("");
        $("#POST1").val("");
        $("#ADDRESS1").val("");
        $("#BEGIN_TIME1").val("");
        $("#END_TIME1").val("");
        $("#VERIFICATION_WAY_NAME1").val("");
        $("#VERIFICATION_RESULT_NAME1").val("");
        $("#VERIFICATION_BEGIN_TIME1").val("");
        $("#VERIFICATION_END_TIME1").val("");
        $("#EXPANDED_UNCETAINTY1").val("");
        $("#MEASURING_RANGE1").val("");
        $("#EXAMINE_DEPARTMENT1").val("");
        $("#DEPARTMENT_PHONE1").val("");
        $("#DEPARTMENT_LINKMAN1").val("");
    }
}

//仪器信息grid 的编辑对话框元素的值 清除
function clearDialogValue() {
        $("#APPARATUS_CODE1").val("");
        $("#NAME1").val("");
        $("#MODEL1").val("");
        $("#SERIAL_NO1").val("");
        $("#BELONG_TO1").val("");
        $("#BUY_TIME1").val("");
        $("#SCRAP_TIME1").val("");
        $("#CERTIFICATE_TYPE_NAME1").val("");
        $("#TRACE_RESULT_NAME1").val("");
        $("#TEST_MODE_NAME1").val("");
        $("#VERIFY_CYCLE1").val("");
        $("#DEPT_NAME1").val("");
        $("#KEEPER1").val("");
        $("#POSITION1").val("");
        $("#ARCHIVES_TYPE1").val("");
        $("#SORT1_NAME1").val("");
        $("#SORT2_NAME1").val("");
        $("#APPARATUS_PROVIDER1").val("");
        $("#FITTINGS_PROVIDER1").val("");
        $("#WEB_SITE1").val("");
        $("#LINK_MAN1").val("");
        $("#LINK_PHONE1").val("");
        $("#POST1").val("");
        $("#ADDRESS1").val("");
        $("#BEGIN_TIME1").val("");
        $("#END_TIME1").val("");
        $("#VERIFICATION_WAY_NAME1").val("");
        $("#VERIFICATION_RESULT_NAME1").val("");
        $("#VERIFICATION_BEGIN_TIME1").val("");
        $("#VERIFICATION_END_TIME1").val("");
        $("#EXPANDED_UNCETAINTY1").val("");
        $("#MEASURING_RANGE1").val("");
        $("#EXAMINE_DEPARTMENT1").val("");
        $("#DEPARTMENT_PHONE1").val("");
        $("#DEPARTMENT_LINKMAN1").val("");
}

//仪器信息grid 的查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var searchform = $("#searchFirstForm");
        searchform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                     { display: "仪器编号", name: "APPARATUS_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "仪器名称", name: "NAME", newline: false, type: "text" },
                     { display: "仪器型号", name: "MODEL", newline: true, type: "text",  },
                     { display: "供应商名称", name: "APPARATUS_PROVIDER", newline: false, type: "text"}
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 660, height: 170, top: 90, title: "查询仪器信息",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var APPARATUS_CODE = encodeURI($("#APPARATUS_CODE").val());
        var NAME = encodeURI($("#NAME").val());
        var MODEL = encodeURI($("#MODEL").val());
        var APPARATUS_PROVIDER = encodeURI($("#APPARATUS_PROVIDER").val());

        firstManager.set('url', "ApparatusSearch.aspx?type=getApparatusInfo&srhApparatus_Code=" + APPARATUS_CODE + "&srh_Name=" + NAME + "&srh_Model=" + MODEL + "&srhProvider=" + APPARATUS_PROVIDER);
    }
}

//获取DictText
function getDictText(type,code) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "ApparatusSearch.aspx/GetDict",
        data: "{'strType':'" + type + "','strCode':'"+code+"'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载监测类别数据失败！');
        }
    });
    return strReturn;
}

//仪器信息grid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#APPARATUS_CODE").val("");
    $("#NAME").val("");
    $("#MODEL").val("");
    $("#APPARATUS_PROVIDER").val("");
}

///仪器资料附件下载
function downLoadDocumentFile() {
    if (secondManager.getSelectedRow() == null) {
        $.ligerDialog.warn('下载附件之前请先选择一条记录');
        return;
    }
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=ApparatusDocument&id=' + secondManager.getSelectedRow().ID
    });
}

    ///检定证书附件下载
    function downLoadCertificFile() {
        if (thirdManager.getSelectedRow() == null) {
            $.ligerDialog.warn('下载附件之前请先选择一条记录');
            return;
        }
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=ApparatusCertific&id=' + thirdManager.getSelectedRow().ID
        });
    }


