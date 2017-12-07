// Create by 潘德军 2012.11.05  "项目管理"功能

var manager, managerMethod;
var menu, menuMethod;
var actionMONITOR_ID, actionMONITOR_NAME, actionItemID, actionITEM_NAME, actionORDER_NUM, actionLAB_CERTIFICATE, actionMEASURE_CERTIFICATE, actionIS_SAMPLEDEPT;
var actionMethod_ID, actionMethod_ItemID, actionMethod_ItemMethodID, actionMethod_ItemANALYSISID, actionMethod_ItemINSTRUMENTID, actionMethod_PRECISION, actionMethod_UPPER_LIMIT, actionMethod_LOWER_LIMIT;
var actionMethod_LOWER_CHECKOUT, actionMethod_UnitCode, actionMethod_IS_DEFAULT, actionMethod_ItemMethod, actionMethod_ItemANALYSIS, actionMethod_ItemINSTRUMENT, actionIS_ANYSCENE_ITEM, actionORI_CATALOG_TABLEID;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var samplingInstrument = null;
var strUrl = "ItemList.aspx";

var topHeight = $(window).height() / 2;
var gridHeight = $(window).height() / 2;

$(document).ready(function () {

    $("#layout1").ligerLayout({ width: '98%', leftWidth: '99.9%', rightWidth: "0.1%", allowLeftCollapse: false, allowRightCollapse: false, height: '100%', topHeight: topHeight });

    //监测项目grid的菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: itemclick_OfMenu_UnderItem, icon: 'modify' },
            { id: 'menudel', text: '删除', click: itemclick_OfMenu_UnderItem, icon: 'delete' }
            ]
    });

    //分析方法grid的菜单
    menuMethod = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: itemclick_OfMenu_UnderItemMethod, icon: 'modify' },
            { id: 'menudel', text: '删除', click: itemclick_OfMenu_UnderItemMethod, icon: 'delete' },
            { line: true },
            { id: 'menudef', text: '设为常用', click: itemclick_OfMenu_UnderItemMethod }
            ]
    });

    //监测项目grid
    window['g'] =
    manager = $("#maingrid").ligerGrid({
        columns: [
        { display: '监测类型', name: 'MONITOR_NAME', width: 250, align: 'left', isSort: false },
        { display: '监测项目', name: 'ITEM_NAME', width: 250, align: 'left', isSort: false },
        { display: '实验室认可', name: 'LAB_CERTIFICATE', width: 100, align: 'left', isSort: false },
        { display: '计量认可', name: 'MEASURE_CERTIFICATE', width: 100, align: 'left', isSort: false },
        { display: '现场监测项目', name: 'IS_SAMPLEDEPT', width: 100, align: 'left', isSort: false },
        //         { display: '现场监测项目', name: 'IS_SAMPLEDEPT', width: 100, align: 'left', isSort: false, render: function (items) {
        //             if (items.IS_SAMPLEDEPT == "1") {
        //                 return "是";
        //             } else {
        //                 return "否";
        //             }
        //         }
        //         },
        {display: '序号', name: 'ORDER_NUM', width: 100, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 8, 10], height: gridHeight,
        url: 'ItemList.aspx?Action=GetItems',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        title: '监测项目',
        toolbar: { items: [
                { id: 'add', text: '增加', click: itemclick_OfToolbar_UnderItem, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: itemclick_OfToolbar_UnderItem, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: itemclick_OfToolbar_UnderItem, icon: 'delete' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclick_OfToolbar_UnderItem, icon: 'search' },
                { line: true },
            //                { id: 'toBag', text: '项目包设置', click: itemclick_OfToolbar_UnderItem, icon: 'settings' },
            //                { line: true },
                {id: 'toQC', text: '质控标准设置', click: itemclick_OfToolbar_UnderItem, icon: 'settings'}//,
            //                { line: true },
            //                { id: 'toFee', text: '监测单价设置', click: itemclick_OfToolbar_UnderItem, icon: 'settings' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            actionItemID = parm.data.ID;
            actionMONITOR_ID = parm.data.MONITOR_ID;
            actionITEM_NAME = parm.data.ITEM_NAME;
            actionORDER_NUM = parm.data.ORDER_NUM;
            actionLAB_CERTIFICATE = parm.data.LAB_CERTIFICATE;
            actionMEASURE_CERTIFICATE = parm.data.MEASURE_CERTIFICATE;
            actionIS_SAMPLEDEPT = parm.data.IS_SAMPLEDEPT;
            actionIS_ANYSCENE_ITEM = param.data.IS_ANYSCENE_ITEM;
            actionORI_CATALOG_TABLEID = param.ORI_CATALOG_TABLEID;
            menu.show({ top: e.pageY, left: e.pageX });

            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetail({
                MONITOR_ID: data.MONITOR_ID,
                ITEM_NAME: data.ITEM_NAME,
                ORDER_NUM: data.ORDER_NUM,
                LAB_CERTIFICATE: data.LAB_CERTIFICATE,
                MEASURE_CERTIFICATE: data.MEASURE_CERTIFICATE,
                IS_SAMPLEDEPT: data.IS_SAMPLEDEPT,
                IS_ANYSCENE_ITEM: data.IS_ANYSCENE_ITEM,
                ORI_CATALOG_TABLEID: data.ORI_CATALOG_TABLEID,
                ItemID: data.ID,
                ITEM_NUM: data.ITEM_NUM
            }, false);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            var selectedItem = manager.getSelectedRow();
            managerMethod.set('url', "ItemList.aspx?Action=GetMethods&selItemID=" + selectedItem.ID);
            //点击的时候加载采样仪器的信息
            samplingInstrument.set('url', "ItemList.aspx?Action=GetItemSamplingInstrumentInfo&Item_ID=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //分析方法grid
    managerMethod = $("#maingridMethod").ligerGrid({
        columns: [
        { display: '监测项目', name: 'ITEM_NAME', width: 130, align: 'left', isSort: false },
        { display: '方法依据', name: 'METHOD', width: 150, align: 'left', isSort: false },
        { display: '分析方法', name: 'ANALYSIS_METHOD', width: 300, align: 'left', isSort: false },
        { display: '仪器编号', name: 'APPARATUS_CODE', width: 100, align: 'left', isSort: false },
        { display: '仪器名称', name: 'INSTRUMENT', width: 200, align: 'left', isSort: false },
        { display: '规格型号', name: 'MODEL', width: 100, align: 'left', isSort: false },
        { display: '最低检出限', name: 'LOWER_CHECKOUT', width: 80, align: 'left', isSort: false },
        { display: '单位', name: 'UNIT', width: 80, align: 'left', isSort: false },
        { display: '常用分析方法', name: 'IS_DEFAULT', width: 80, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 8, 10], height: gridHeight, heightDiff: -30,
        url: 'ItemList.aspx?Action=GetMethods',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'add', text: '增加', click: itemclick_OfToolbar_UnderItemMethod, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: itemclick_OfToolbar_UnderItemMethod, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: itemclick_OfToolbar_UnderItemMethod, icon: 'delete' },
                { line: true },
                { id: 'def', text: '设为常用', click: itemclick_OfToolbar_UnderItemMethod, icon: 'right' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            actionMethod_ID = parm.data.ID;
            actionMethod_ItemID = parm.data.ITEM_ID;
            actionMethod_ItemMethodID = parm.data.METHOD_ID;
            actionMethod_ItemANALYSISID = parm.data.ANALYSIS_METHOD_ID;
            actionMethod_ItemINSTRUMENTID = parm.data.INSTRUMENT_ID;
            actionMethod_ItemMethod = parm.data.METHOD;
            actionMethod_ItemANALYSIS = parm.data.ANALYSIS_METHOD;
            actionMethod_ItemINSTRUMENT = parm.data.INSTRUMENT;
            actionMethod_UnitCode = parm.data.Unitcode;
            actionMethod_PRECISION = parm.data.PRECISION;
            actionMethod_UPPER_LIMIT = parm.data.UPPER_LIMIT;
            actionMethod_LOWER_LIMIT = parm.data.LOWER_LIMIT;
            actionMethod_LOWER_CHECKOUT = parm.data.LOWER_CHECKOUT;
            actionMethod_IS_DEFAULT = parm.data.IS_DEFAULT;
            menuMethod.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetailMthod({
                Method_ID: data.ID,
                Method_ItemID: data.ITEM_ID,
                Method_ItemMethodID: data.METHOD_ID,
                Method_ItemANALYSISID: data.ANALYSIS_METHOD_ID,
                Method_ItemINSTRUMENTID: data.INSTRUMENT_ID,
                Method_ItemMETHOD: data.METHOD,
                Method_ItemANALYSIS_METHOD: data.ANALYSIS_METHOD,
                Method_ItemINSTRUMENT: data.INSTRUMENT,
                Method_UnitCode: data.Unitcode,
                Method_PRECISION: data.PRECISION,
                Method_UPPER_LIMIT: data.UPPER_LIMIT,
                Method_LOWER_LIMIT: data.LOWER_LIMIT,
                Method_LOWER_CHECKOUT: data.LOWER_CHECKOUT,
                Method_IS_DEFAULT: data.IS_DEFAULT
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
//现场采样仪器
$(document).ready(function () {
    samplingInstrument = $("#samplingInstrument").ligerGrid({
        dataAction: 'server',
        usePager: false,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        dataAction: 'server', //服务器排序
        usePager: false, //服务器分页
        pageSize: 5,
        pageSizeOptions: [5, 8, 10],
        height: gridHeight,
        heightDiff: -39,
        toolbar: { items: [
                { id: 'add', text: '增加', click: createData, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: deleteData, icon: 'delete' },
                { line: true },
                { id: 'def', text: '设为常用', click: setDefault, icon: 'right' }
                ]
        },
        columns: [
                  { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 150, minWidth: 60, frozen: true, render: function (record) {
                      var strItemName = getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                      return strItemName;
                  }
                  },
                 { display: '采样仪器', name: 'INSTRUMENT_NAME', align: 'left', width: 300, minWidth: 60 },
                 { display: '常用仪器', name: 'IS_DEFAULT', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     if (record.IS_DEFAULT == "0")
                         return "否";
                     else
                         return "是";
                 }
                 }
                ],
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //增加数据
    function createData() {
        if (!manager.getSelectedRow()) {
            $.ligerDialog.warn('请先选择监测项目！');
            return;
        }
        $.ligerDialog.open({ title: '现场采样仪器增加', width: 350, height: 200, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                $.ligerDialog.warn('数据保存失败');
            }
            samplingInstrument.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'Item_Sample_Instrument.aspx?ITEM_ID=' + manager.getSelectedRow().ID
        });
    }
    //修改数据
    function updateData() {
        if (!samplingInstrument.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: '现场采样仪器编辑', width: 350, height: 200, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据更新成功')
            }
            else {
                $.ligerDialog.warn('数据更新失败');
            }
            samplingInstrument.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'Item_Sample_Instrument.aspx?id=' + samplingInstrument.getSelectedRow().ID
        });
    }
    //删除数据
    function deleteData() {
        if (samplingInstrument.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除现场采样仪器信息 ' + samplingInstrument.getSelectedRow().INSTRUMENT_NAME + " 吗？", function (yes) {
            if (yes == true) {
                var strValue = samplingInstrument.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "ItemList.aspx/deleteSamplingInstrumentInfo",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            samplingInstrument.loadData();
                            $.ligerDialog.success('删除数据成功')
                        }
                        else {
                            $.ligerDialog.warn('删除数据失败');
                        }
                    }
                });
            }
        });
    }
    //设为常用
    function setDefault() {
        if (samplingInstrument.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行设置');
            return;
        }
        $.ligerDialog.confirm('确认设置采样仪器 ' + samplingInstrument.getSelectedRow().INSTRUMENT_NAME + " 为默认吗？", function (yes) {
            if (yes == true) {
                var ITEM_ID = samplingInstrument.getSelectedRow().ITEM_ID;
                var strValue = samplingInstrument.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "ItemList.aspx/setDefaultSamplingInstrumentInfo",
                    data: "{'ITEM_ID':'" + ITEM_ID + "','strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            samplingInstrument.loadData();
                            $.ligerDialog.success('设置默认采样仪器成功')
                        }
                        else {
                            $.ligerDialog.warn('设置默认采样仪器失败');
                        }
                    }
                });
            }
        });
    }
});