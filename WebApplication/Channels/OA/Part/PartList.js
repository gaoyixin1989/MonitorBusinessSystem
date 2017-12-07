// Create by 潘德军 2012.11.07  "物料信息管理"功能

var manager;
var menu;
var actionID, actionPART_CODE, actionPART_TYPE, actionPART_NAME, actionUNIT, actionMODELS, actionINVENTORY, actionMEDIUM, actionPURE, actionALARM, actionUSEING, actionREQUEST, actionNARURE;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

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

$(function () {
    $("#layout1").ligerLayout({ height: '100%' });

    var operateORinquiry = $.getUrlVar('operateORinquiry');

    //grid的菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: itemclickOfMenu, icon: 'modify' },
            { id: 'menudel', text: '删除', click: itemclickOfMenu, icon: 'delete' }
            ]
    });

    if (operateORinquiry == "inquiry") {
        //grid
        window['g'] =
    manager = $("#maingrid").ligerGrid({
        columns: [
        { display: '物料编码', name: 'PART_CODE', width: 150, align: 'left', validate: { required: true, maxlength: 50} },
        { display: '物料类别', name: 'DICT_TEXT', width: 150, align: 'left' },
        { display: '物料名称', name: 'PART_NAME', width: 200, align: 'left' },
        { display: '单位', name: 'UNIT', width: 100, align: 'left' },
        { display: '规格型号', name: 'MODELS', width: 100, align: 'left' },
        { display: '库存量', name: 'INVENTORY', width: 100, align: 'left' },
        { display: '化学式', name: 'REMARK1', width: 100, align: 'left' },
        { display: '生产厂家', name: 'REMARK2', width: 100, align: 'left' },
        { display: '生产批次', name: 'REMARK3', width: 100, align: 'left' }
        ], width: '100%', pageSizeOptions: [10, 20, 25, 30], height: '100%',
        url: 'PartList.aspx?Action=GetData&type=' + $.getUrlVar('type'),
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 20,
        title: '物料信息',
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'srh', text: '查询', click: itemclickOfToolbar, icon: 'search' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            actionID = parm.data.ID;
            actionPART_CODE = parm.data.PART_CODE;
            actionPART_TYPE = parm.data.PART_TYPE;
            actionPART_NAME = parm.data.PART_NAME;
            actionUNIT = parm.data.UNIT;
            actionMODELS = parm.data.MODELS;
            actionINVENTORY = parm.data.INVENTORY;
            actionMEDIUM = parm.data.MEDIUM;
            actionPURE = parm.data.PURE;
            actionALARM = parm.data.ALARM;
            actionUSEING = parm.data.USEING;
            actionREQUEST = parm.data.REQUEST;
            actionNARURE = parm.data.NARURE;
            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetail({
                ID: data.ID,
                PART_CODE: data.PART_CODE,
                PART_TYPE: data.PART_TYPE,
                PART_NAME: data.PART_NAME,
                UNIT: data.UNIT,
                MODELS: data.MODELS,
                INVENTORY: data.INVENTORY,
                MEDIUM: data.MEDIUM,
                PURE: data.PURE,
                ALARM: data.ALARM,
                USEING: data.USEING,
                REQUEST: data.REQUEST,
                NARURE: data.NARURE
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
        if ($.getUrlVar('type')) {
            manager.toggleCol("PART_CODE");
        }
    } else {
        //grid
        window['g'] =
    manager = $("#maingrid").ligerGrid({
        columns: [
        { display: '物料编码', name: 'PART_CODE', width: 150, align: 'left', validate: { required: true, maxlength: 50} },
        { display: '物料类别', name: 'DICT_TEXT', width: 150, align: 'left' },
        { display: '物料名称', name: 'PART_NAME', width: 200, align: 'left' },
        { display: '单位', name: 'UNIT', width: 100, align: 'left' },
        { display: '规格型号', name: 'MODELS', width: 100, align: 'left' },
        { display: '库存量', name: 'INVENTORY', width: 100, align: 'left' },
        { display: '化学式', name: 'REMARK1', width: 100, align: 'left' },
        { display: '生产厂家', name: 'REMARK2', width: 100, align: 'left' },
        { display: '生产批次', name: 'REMARK3', width: 100, align: 'left' }
        ], width: '100%', pageSizeOptions: [10, 20, 25, 30], height: '100%',
        url: 'PartList.aspx?Action=GetData&type=' + $.getUrlVar('type'),
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 20,
        title: '物料信息',
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'add', text: '增加', click: itemclickOfToolbar, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: itemclickOfToolbar, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: itemclickOfToolbar, icon: 'delete' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclickOfToolbar, icon: 'search' },
                { line: true },
                { id: 'import', text: '导入', click: function () { showDataImport() }, icon: 'database_wrench' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            actionID = parm.data.ID;
            actionPART_CODE = parm.data.PART_CODE;
            actionPART_TYPE = parm.data.PART_TYPE;
            actionPART_NAME = parm.data.PART_NAME;
            actionUNIT = parm.data.UNIT;
            actionMODELS = parm.data.MODELS;
            actionINVENTORY = parm.data.INVENTORY;
            actionMEDIUM = parm.data.MEDIUM;
            actionPURE = parm.data.PURE;
            actionALARM = parm.data.ALARM;
            actionUSEING = parm.data.USEING;
            actionREQUEST = parm.data.REQUEST;
            actionNARURE = parm.data.NARURE;
            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetail({
                ID: data.ID,
                PART_CODE: data.PART_CODE,
                PART_TYPE: data.PART_TYPE,
                PART_NAME: data.PART_NAME,
                UNIT: data.UNIT,
                MODELS: data.MODELS,
                INVENTORY: data.INVENTORY,
                MEDIUM: data.MEDIUM,
                PURE: data.PURE,
                ALARM: data.ALARM,
                USEING: data.USEING,
                REQUEST: data.REQUEST,
                NARURE: data.NARURE
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
        if ($.getUrlVar('type')) {
            manager.toggleCol("PART_CODE");
        }
    }
});

//grid 的toolbar click事件
function itemclickOfToolbar(item) {
    switch (item.id) {
        case 'add':
            showDetail(null, true);
            break;
        case 'modify':
            var data = manager.getSelectedRow();
            if (!data) { $.ligerDialog.warn('请先选择要编辑的记录！'); ; return }

            showDetail({
                ID: data.ID,
                PART_CODE: data.PART_CODE,
                PART_TYPE: data.PART_TYPE,
                PART_NAME: data.PART_NAME,
                UNIT: data.UNIT,
                MODELS: data.MODELS,
                INVENTORY: data.INVENTORY,
                MEDIUM: data.MEDIUM,
                PURE: data.PURE,
                ALARM: data.ALARM,
                USEING: data.USEING,
                REQUEST: data.REQUEST,
                NARURE: data.NARURE
            }, false);

            break;
        case 'del':
            var rows = manager.getCheckedRows();
            var strDelID = "";
            $(rows).each(function () {
                strDelID += (strDelID.length > 0 ? "," : "") + this.ID;
            });

            if (strDelID.length == 0) {
                $.ligerDialog.warn('请先选择要删除的记录！');
            }
            else {
                jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                    if (confirm)
                        delDate(strDelID);
                });
            }

            break;
        case 'srh':
            showDetailSrh();
            break;
        default:
            break;
    }
}

//监测类别grid 右键菜单
function itemclickOfMenu(item) {
    switch (item.id) {
        case 'menumodify':
            showDetail({
                ID: actionID,
                PART_CODE: actionPART_CODE,
                PART_TYPE: actionPART_TYPE,
                PART_NAME: actionPART_NAME,
                UNIT: actionUNIT,
                MODELS: actionMODELS,
                INVENTORY: actionINVENTORY,
                MEDIUM: actionMEDIUM,
                PURE: actionPURE,
                ALARM: actionALARM,
                USEING: actionUSEING,
                REQUEST: actionREQUEST,
                NARURE: actionNARURE
            }, false);

            break;
        case 'menudel':
            var strDelID = actionID;

            jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                if (confirm)
                    delDate(strDelID);
            });

            break;
        default:

    }
}

//监测类别grid 删除函数
function delDate(ids) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "PartList.aspx/deleteData",
        data: "{'strDelIDs':'" + ids + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                manager.loadData();
            }
            else {
                $.ligerDialog.warn('删除物料信息失败！');
            }
        }
    });
}

var objPURE = [{ 'VALUE': '色谱纯', 'TEXT': '色谱纯' }, { 'VALUE': '基准纯', 'TEXT': '基准纯' }, { 'VALUE': '优级纯', 'TEXT': '优级纯' }, { 'VALUE': '分析纯', 'TEXT': '分析纯' }, { 'VALUE': '化学纯', 'TEXT': '化学纯'}];
//监测类别grid 弹出编辑框及save函数
var detailWin = null, curentData = null, currentIsAddNew;
function showDetail(data, isAddNew) {
    curentData = data;
    currentIsAddNew = isAddNew;
    if (detailWin) {
        detailWin.show();
    }
    else {
        //创建表单结构
        var mainform = $("#editform");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { name: "ID", type: "hidden" },
                      { display: "物料编码", name: "PART_CODE", newline: true, type: "text", validate: { required: true, minlength: 3 }, group: "基本信息", groupicon: groupicon },
                      { display: "物料名称", name: "PART_NAME", newline: false, type: "text", validate: { required: true, minlength: 3} },
                      { display: "物料类别", name: "PART_TYPE_NAME", newline: true, type: "select", comboboxName: "PART_TYPE_BOX", options: { valueFieldID: "PART_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "PartList.aspx?Action=getDict&dictType=PART_TYPE&type=" + $.getUrlVar('type')} },
                      { display: "规格型号", name: "MODELS", newline: false, type: "text" },
                      { display: "库存量", name: "INVENTORY", newline: true, type: "text" },
                      { display: "单位", name: "UNIT", newline: false, type: "text" },
                      { display: "介质/基体", name: "MEDIUM", newline: true, type: "text" },
                      { display: "纯度", name: "PURE_NAME", newline: false, type: "select", comboboxName: "PURE_BOX", options: { valueFieldID: "PURE", valueField: "VALUE", textField: "TEXT", data: objPURE} },
                      { display: "报警值", name: "ALARM", newline: true, type: "text" },
                      { display: "用途", name: "USEING", width: 470, newline: true, type: "text" },
                      { display: "技术要求", name: "REQUEST", width: 470, newline: true, type: "text" },
                      { display: "性质说明", name: "NARURE", width: 470, newline: true, type: "text" }
                    ]
        });
        if ($.getUrlVar('type')) {
            $("#PART_CODE").ligerGetTextBoxManager().setDisabled();
        }
        else {
            $("#PART_CODE").attr("validate", "[{required:true,msg:'物料编码不能为空'}]");
            $("#MODELS").attr("validate", "[{required:true,msg:'规格不能为空'}]");
        }
        $("#PART_NAME").attr("validate", "[{required:true,msg:'物料名称不能为空'}]");
        $("#PART_TYPE").attr("validate", "[{required:true,msg:'请选择物料类别'}]");
        
        detailWin = $.ligerDialog.open({
            target: $("#detail"),
            width: 650, height: 400, top: 90, title: "物料信息",
            buttons: [
                  { text: '确定', onclick: function () { save(); } },
                  { text: '取消', onclick: function () { clearDialogValue(); detailWin.hide(); } }
                  ]
        });
    }
    
    if (curentData) {
        $("#ID").val(curentData.ID);
        $("#PART_CODE").val(curentData.PART_CODE);
        actionPART_CODE = curentData.PART_CODE;
        $("#PART_NAME").val(curentData.PART_NAME);
        actionPART_NAME = curentData.PART_NAME;
        //$("#PART_TYPE").val(curentData.PART_TYPE);
        $.ligerui.get("PART_TYPE_BOX").selectValue(curentData.PART_TYPE);

        $("#UNIT").val(curentData.UNIT);
        $("#MODELS").val(curentData.MODELS);
        actionMODELS = curentData.MODELS;
        $("#INVENTORY").val(curentData.INVENTORY);
        $("#MEDIUM").val(curentData.MEDIUM);
        $.ligerui.get("PURE_BOX").selectValue(curentData.PURE);
        //$("#PURE").val(curentData.PURE);
        $("#ALARM").val(curentData.ALARM);
        $("#USEING").val(curentData.USEING);
        $("#REQUEST").val(curentData.REQUEST);
        $("#NARURE").val(curentData.NARURE);
    }
    else {
        $.ligerui.get("PART_TYPE_BOX").selectValue('');
    }

    function save() {
        if (!$("#editform").validate()) {
            return;
        }
        
        curentData = curentData || {};
        curentData.MONITOR_TYPE_NAME = $("#MONITOR_TYPE_NAME").val();
        curentData.DESCRIPTION = $("#DESCRIPTION").val();
        curentData.MONITOR_TYPE_ID = $("#MONITOR_TYPE_ID").val();

        var strData = "{";
        strData += currentIsAddNew ? "" : "'strID':'" + $("#ID").val() + "',";
        strData += "'strPART_CODE':'" + $("#PART_CODE").val() + "',";
        strData += "'strPART_NAME':'" + $("#PART_NAME").val() + "',";
        strData += "'strPART_TYPE':'" + $("#PART_TYPE").val() + "',";
        strData += "'strUNIT':'" + $("#UNIT").val() + "',";
        strData += "'strMODELS':'" + $("#MODELS").val() + "',";
        strData += "'strINVENTORY':'" + $("#INVENTORY").val() + "',";
        strData += "'strMEDIUM':'" + $("#MEDIUM").val() + "',";
        strData += "'strPURE':'" + $("#PURE").val() + "',";
        strData += "'strALARM':'" + $("#ALARM").val() + "',";
        strData += "'strUSEING':'" + $("#USEING").val() + "',";
        strData += "'strREQUEST':'" + $("#REQUEST").val() + "',";
        strData += "'strNARURE':'" + $("#NARURE").val() + "'";
        if (!currentIsAddNew) {
            if ($("#PART_CODE").val() == actionPART_CODE && $("#PART_NAME").val() == actionPART_NAME && $("#MODELS").val() == actionMODELS) {
                strData += ",'isCheck':'false'";
            }
            else {
                strData += ",'isCheck':'true'";
            }
        }
        strData += "}";

        $.ajax({
            cache: false,
            type: "POST",
            url: "PartList.aspx/" + (currentIsAddNew ? "AddData" : "EditData"),
            data: strData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    detailWin.hidden();
                    manager.loadData();
                    clearDialogValue();
                }
                else if (data.d == "0") {
                    $.ligerDialog.warn('保存物料数据失败！');
                }
                else {
                    $.ligerDialog.warn(data.d);
                }
            }
        });
    }
}

//监测类别grid 弹出编辑框 清空
function clearDialogValue() {
    $("#ID").val("");
    $("#PART_CODE").val("");
    $("#PART_NAME").val("");
    $("#PART_TYPE").val("");
    $("#PART_TYPE_BOX").val("");
    $("#UNIT").val("");
    $("#MODELS").val("");
    $("#INVENTORY").val("");
    $("#MEDIUM").val("");
    //$("#PURE").val("");
    $("#ALARM").val("");
    $("#USEING").val("");
    $("#REQUEST").val("");
    $("#NARURE").val("");
}

var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#searchForm");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "物料编码", name: "SrhPART_CODE", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                      { display: "物料名称", name: "SrhPART_NAME", newline: true, type: "text" },
                      //{ display: "物料类别", name: "SrhPART_TYPE", newline: true, type: "select", comboboxName: "SrhPART_TYPE1", options: { valueFieldID: "SrhPART_TYPE", url: "../../base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|PART_TYPE"} }
                      { display: "物料类别", name: "SrhPART_TYPE", newline: true, type: "select", comboboxName: "SrhPART_TYPE1", options: { valueFieldID: "SrhPART_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "PartList.aspx?Action=getDict&dictType=PART_TYPE&type=" + $.getUrlVar('type')} }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 350, height: 200, top: 90, title: "查询物料",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhPART_CODE = $("#SrhPART_CODE").val();
        var SrhPART_NAME = escape($("#SrhPART_NAME").val());
        var SrhPART_TYPE = escape($("#SrhPART_TYPE").val());

        manager.set('url', "PartList.aspx?Action=GetData&SrhPART_CODE=" + SrhPART_CODE + "&SrhPART_NAME=" + SrhPART_NAME + "&SrhPART_TYPE=" + SrhPART_TYPE + "&type=" + $.getUrlVar('type'));
    }
}

//maingrid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#SrhPART_CODE").val("");
    $("#SrhPART_NAME").val("");
    $("#SrhPART_TYPE").val("");
    $("#SrhPART_TYPE1").val("");
}
//Excel导入
function showDataImport() {
    $.ligerDialog.open({ title: "Excel导入界面", width: 500, height: 200, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            value = $("iframe")[0].contentWindow.Import();
            manager.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); manager.loadData(); }
        }], url: "PartImportDemo.aspx"
    });
}