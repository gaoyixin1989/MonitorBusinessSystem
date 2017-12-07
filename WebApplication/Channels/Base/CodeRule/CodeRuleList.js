// Create by 潘德军 2012.11.02  "监测类别管理"功能

var manager;
var menu;
var actionID, actionSERIALNAME, actionSERIAL_RULE,
actionSERIAL_TYPE, actionSERIAL_NUMBER_BIT, actionSERIAL_TYPE_ID, actionSAMPLE_SOURCE, actionSERIAL_START_NUM,
actionSERIAL_MAX_NUM, actionSTATUS, actionIS_UNION, actionUNION_SERIAL_ID, actionUNIONDEFAULT, actionDAY_STATUS = "";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var vMonitorType = null, vSampleItems = null, vCodeType = null, vContractItems = null, vIs_Use = null, vUnionCodeItems = null;
var selectValue = "";
$(function () {
    $("#layout1").ligerLayout({ height: '100%' });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../Mis/Contract/MethodHander.ashx?action=GetMonitorType",
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
        url: "CodeRuleList.aspx?Action=GetUnionCode",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vUnionCodeItems = data.Rows;
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
        url: "../../Mis/Contract/MethodHander.ashx?action=GetDict&type=SAMPLE_SOURCE",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vSampleItems = data;
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
        url: "../../Mis/Contract/MethodHander.ashx?action=GetDict&type=Contract_Type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vContractItems = data;
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
        url: "../../Mis/Contract/MethodHander.ashx?action=GetDict&type=IS_USE",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vIs_Use = data;
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
        url: "../../Mis/Contract/MethodHander.ashx?action=GetDict&type=Code_Type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vCodeType = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    //监测类别grid的菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: itemclickOfMenu, icon: 'modify' },

            { id: 'menudel', text: '删除', click: itemclickOfMenu, icon: 'delete' }
            ]
    });

    //监测类别grid
    window['g'] =
    manager = $("#maingrid").ligerGrid({
        columns: [
        { display: '规则名称', name: 'SERIAL_NAME', width: 250, align: 'left', validate: { required: true, maxlength: 50} },
        { display: '编号规则', name: 'SERIAL_RULE', width: 200, align: 'left' },
        { display: '编号位数', name: 'SERIAL_NUMBER_BIT', width: 80, align: 'center' },
        { display: '初始编号', name: 'SERIAL_START_NUM', width: 80, align: 'center' },
        { display: '最大编号', name: 'SERIAL_MAX_NUM', width: 80, align: 'center' },
        { display: '跨年重新编号', name: 'STATUS', width: 80, align: 'center', render: function (items) {
            for (var i = 0; i < vIs_Use.length; i++) {
                if (vIs_Use[i].DICT_CODE == items.STATUS) {
                    return vIs_Use[i].DICT_TEXT;
                }
            }
            return items.STATUS;
        }
        },
//                { display: '跨天重新编号', name: 'DAY_STATUS', width: 80, align: 'center', render: function (items) {
//                    for (var i = 0; i < vIs_Use.length; i++) {
//                        if (vIs_Use[i].DICT_CODE == items.STATUS) {
//                            return vIs_Use[i].DICT_TEXT;
//                        }
//                    }
//                    return items.STATUS;
//                }
//                },
        { display: '编号类型', name: 'SERIAL_TYPE', width: 80, align: 'center', render: function (items) {
            if (vCodeType.length > 0) {
                for (var i = 0; i < vCodeType.length; i++) {
                    if (vCodeType[i].DICT_CODE == items.SERIAL_TYPE) {
                        return vCodeType[i].DICT_TEXT;
                    }
                }
            }
            return items.SERIAL_TYPE;
        }
        },
        { display: '所属类别', name: 'SERIAL_TYPE_ID', width: 400, align: 'left', render: function (items) {
            var MonitorItems = items.SERIAL_TYPE_ID.split(';');
            if (MonitorItems.length > 0) {
                var TypeName = "";
                if (items.SERIAL_TYPE == "2") {
                    TypeName = "";
                    for (var i = 0; i < vMonitorType.length; i++) {
                        for (var n = 0; n < MonitorItems.length; n++) {
                            if (vMonitorType[i].ID == MonitorItems[n]) {
                                TypeName += vMonitorType[i].MONITOR_TYPE_NAME + ";";
                            }
                        }
                    }
                    //                    return MonitorName.substring(0, MonitorName.length - 1);
                }
                else {
                    TypeName = "";
                    for (var i = 0; i < vContractItems.length; i++) {
                        for (var n = 0; n < MonitorItems.length; n++) {
                            if (vContractItems[i].DICT_CODE == MonitorItems[n]) {
                                TypeName += vContractItems[i].DICT_TEXT + ";";
                            }
                        }
                    }
                }
                return TypeName.substring(0, TypeName.length - 1);
            }
            return items.MONITOR_TYPE_ID;
        }
        },
        { display: '样品来源', name: 'SAMPLE_SOURCE', width: 80, align: 'center' }
        ], width: '100%', pageSizeOptions: [5, 10, 15, 20], height: '100%',
        url: 'CodeRuleList.aspx?Action=GetData',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'add', text: '增加', click: itemclickOfToolbar, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: itemclickOfToolbar, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: itemclickOfToolbar, icon: 'delete' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            actionID = parm.data.ID;
            actionSERIALNAME = parm.data.SERIAL_NAME;
            actionSERIAL_RULE = parm.data.SERIAL_RULE;
            actionSERIAL_TYPE = parm.data.SERIAL_TYPE;
            actionSERIAL_NUMBER_BIT = parm.data.SERIAL_NUMBER_BIT;
            actionSERIAL_TYPE_ID = parm.data.SERIAL_TYPE_ID;
            actionSAMPLE_SOURCE = parm.data.SAMPLE_SOURCE;
            actionSERIAL_START_NUM = parm.data.SERIAL_START_NUM;
            actionSERIAL_MAX_NUM = parm.data.SERIAL_MAX_NUM;
            actionSTATUS = parm.data.STATUS;
            actionIS_UNION = param.data.IS_UNION;
            actionUNION_SERIAL_ID = param.data.UNION_SEARIAL_ID;
            actionUNIONDEFAULT = param.data.UNION_DEFAULT;
//            actionDAY_STATUS = param.data.DAY_STATUS;
            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetail({
                SERIAL_NAME: data.SERIAL_NAME,
                SERIAL_RULE: data.SERIAL_RULE,
                SERIAL_TYPE: data.SERIAL_TYPE,
                SERIAL_NUMBER_BIT: data.SERIAL_NUMBER_BIT,
                SERIAL_TYPE_ID: data.SERIAL_TYPE_ID,
                SAMPLE_SOURCE: data.SAMPLE_SOURCE,
                SERIAL_START_NUM: data.SERIAL_START_NUM,
                SERIAL_MAX_NUM: data.SERIAL_MAX_NUM,
                STATUS: data.STATUS,
                IS_UNION: data.IS_UNION,
                UNION_SEARIAL_ID: data.UNION_SEARIAL_ID,
                UNION_DEFAULT: data.UNION_DEFAULT,
//                DAY_STATUS:data.DAY_STATUS,
                ID: data.ID
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

//监测类别grid 的toolbar click事件
function itemclickOfToolbar(item) {
    switch (item.id) {
        case 'add':
            showDetail(null, true);
            break;
        case 'modify':
            var selected = manager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要编辑的记录！'); ; return }

            showDetail({
                SERIAL_NAME: selected.SERIAL_NAME,
                SERIAL_RULE: selected.SERIAL_RULE,
                SERIAL_TYPE: selected.SERIAL_TYPE,
                SERIAL_NUMBER_BIT: selected.SERIAL_NUMBER_BIT,
                SERIAL_TYPE_ID: selected.SERIAL_TYPE_ID,
                SAMPLE_SOURCE: selected.SAMPLE_SOURCE,
                SERIAL_START_NUM: selected.SERIAL_START_NUM,
                SERIAL_MAX_NUM: selected.SERIAL_MAX_NUM,
                STATUS: selected.STATUS,
                IS_UNION: selected.IS_UNION,
                UNION_SEARIAL_ID: selected.UNION_SEARIAL_ID,
                UNION_DEFAULT: selected.UNION_DEFAULT,
//                DAY_STATUS: selected.DAY_STATUS,
                ID: selected.ID
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
                        delMonitorType(strDelID);
                });
            }

            break;
        default:

    }
}

//监测类别grid 右键菜单
function itemclickOfMenu(item) {
    switch (item.id) {
        case 'menumodify':
            showDetail({
                SERIAL_NAME: actionSERIALNAME,
                SERIAL_RULE: actionSERIAL_RULE,
                SERIAL_TYPE: actionSERIAL_TYPE,
                SERIAL_NUMBER_BIT: actionSERIAL_NUMBER_BIT,
                SERIAL_TYPE_ID: actionSERIAL_TYPE_ID,
                SAMPLE_SOURCE: actionSAMPLE_SOURCE,
                SERIAL_START_NUM: actionSERIAL_START_NUM,
                SERIAL_MAX_NUM: actionSERIAL_MAX_NUM,
                STATUS: actionSTATUS,
                IS_UNION: actionIS_UNION,
                UNION_SEARIAL_ID: actionUNION_SERIAL_ID,
                UNION_DEFAULT: actionUNIONDEFAULT,
//                DAY_STATUS:actionDAY_STATUS,
                ID: actionID
            }, false);

            break;
        case 'menudel':
            var strDelID = actionID;

            jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                if (confirm)
                    delMonitorType(strDelID);
            });

            break;
        default:

    }
}

//监测类别grid 删除函数
function delMonitorType(ids) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "CodeRuleList.aspx/deleteData",
        data: "{'strDelIDs':'" + ids + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                manager.loadData();
            }
            else {
                $.ligerDialog.warn('删除监测类型数据失败！');
            }
        }
    });
}

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
        var mainform = $("#editMonitorTypeform");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 120, space: 40, labelAlign: 'right',
            fields: [
                      { name: "ID", type: "hidden" },
                      { display: "规则名称", name: "SERIAL_NAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                      { display: "编号规则", name: "SERIAL_RULE", newline: false, type: "text" },
                      { display: "编号位数", name: "SERIAL_NUMBER_BIT", newline: true, type: "text" },
                      { display: "初始编号", name: "SERIAL_START_NUM", newline: false, type: "text" },
                      { display: "最大编号", name: "SERIAL_MAX_NUM", newline: true, type: "text" },
                      { display: "跨年重新编号", name: "STATUS", newline: false, type: "select", comboboxName: "STATUS_BOX", options: { valueFieldID: "STATUS_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|IS_USE"} },
                      //  { display: "跨天重新编号", name: "DAYSTATUS", newline: true, type: "select", comboboxName: "DAYSTATUS_BOX", options: { valueFieldID: "DAYSTATUS_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|IS_USE"} },
                      { display: "编号类型", name: "SERIAL_TYPE", newline: true, type: "select", comboboxName: "SERIAL_TYPE_BOX", options: { valueFieldID: "SERIAL_TYPE_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|Code_Type"} },
                      { display: "所属类型", name: "SERIAL_TYPE_ID", newline: false, type: "select", comboboxName: "SERIAL_TYPES_BOX", options: { valueFieldID: "SERIAL_TYPES_OP", data: vMonitorType, valueField: 'ID', textField: 'MONITOR_TYPE_NAME', isShowCheckBox: true, isMultiSelect: true} },
                      { display: "样品来源", name: "SAMPLE_SOURCE", newline: true, type: "select", comboboxName: "SAMPLE_SOURCE_BOX", options: { valueFieldID: "SAMPLE_SOURCE_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|SAMPLE_SOURCE"} },
                      { display: "是否联合编号", name: "YESORNO", newline: false, type: "select", comboboxName: "YESORNO_BOX", options: { valueFieldID: "YESORNO_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|company_yesno"} },
                      { display: "辅助规则", name: "UNIONSERIAL_ID", newline: true, type: "select", comboboxName: "UNIONSERIAL_ID_BOX", options: { valueFieldID: "UNIONSERIAL_ID_OP", data: vUnionCodeItems, valueField: 'ID', textField: 'SERIAL_NAME'} },
                      { display: "辅助规则缺省值", name: "UNIONDEFAULT", newline: false, type: "text" }
                    ]
        });

        $("#SERIAL_START_NUM").val("1");

        $("#SERIAL_NAME").attr("validate", "[{required:true, msg:'请填写规则名称!'},{maxlength:256,msg:'规则最大长度为256'}]");
        $("#SERIAL_RULE").attr("validate", "[{required:true,msg:'请填写规则!'}]");
        $("#SERIAL_NUMBER_BIT").attr("validate", "[{required:true,msg:'请填写编号位数!'}]");
        $("#SAMPLE_TYPE").attr("validate", "[{required:true,msg:'请填写编号类型!'}]");
        $("#SERIAL_TYPE_BOX").attr("validate", "[{required:true,msg:'请选择所属类别!'}]");

        $("#SERIAL_START_NUM").attr("validate", "[{required:true,msg:'请填写初始编号!'}]");
        $("#SERIAL_MAX_NUM").attr("validate", "[{required:true,msg:'请填写最大编号!'}]");
        $("#STATUS_BOX").attr("validate", "[{required:true,msg:'请选择是否启用跨年重新编号!'}]");
//        $("#DAYSTATUS_BOX").attr("validate", "[{required:true,msg:'请选择是否启用跨日重新编号!'}]");
        $("#SAMPLE_SOURCE_BOX").ligerGetComboBoxManager().setDisabled();
        $("#YESORNO_BOX").ligerGetComboBoxManager().setDisabled();
        $("#UNIONSERIAL_ID_BOX").ligerGetComboBoxManager().setDisabled();
        $("#UNIONDEFAULT").ligerGetTextBoxManager().setDisabled();

        $("#SERIAL_TYPE_BOX").ligerComboBox({ onSelected: function (value, text) {
            if (value != "2") {
                $("#SERIAL_TYPES_BOX").ligerComboBox({ valueFieldID: "SERIAL_TYPES_OP", valueField: 'DICT_CODE', textField: 'DICT_TEXT', isShowCheckBox: true, isMultiSelect: true });
                $("#SERIAL_TYPES_BOX").ligerGetComboBoxManager().setData(vContractItems);
                $("#SAMPLE_SOURCE_BOX").ligerGetComboBoxManager().setDisabled();
                $("#YESORNO_BOX").ligerGetComboBoxManager().setDisabled();
                $("#UNIONSERIAL_ID_BOX").ligerGetComboBoxManager().setDisabled();
                $("#UNIONDEFAULT").ligerGetTextBoxManager().setDisabled();
            } else {
                $("#SERIAL_TYPES_BOX").ligerComboBox({ valueFieldID: "SERIAL_TYPES_OP", valueField: 'ID', textField: 'MONITOR_TYPE_NAME', isShowCheckBox: true, isMultiSelect: true });
                $("#SERIAL_TYPES_BOX").ligerGetComboBoxManager().setData(vMonitorType);
                $("#SAMPLE_SOURCE_BOX").ligerGetComboBoxManager().setEnabled();
                $("#YESORNO_BOX").ligerGetComboBoxManager().setEnabled();
//                $("#UNIONDEFAULT").ligerGetTextBoxManager().setEnabled();
            }
        }
        });


    $("#YESORNO_BOX").ligerComboBox({ onSelected: function (value, text) {
        if (value == "0") {
            $("#UNIONSERIAL_ID_BOX").ligerGetComboBoxManager().setValue("");
            $("#UNIONSERIAL_ID_BOX").ligerGetComboBoxManager().setDisabled();
            $("#UNIONDEFAULT").ligerGetTextBoxManager().setDisabled();
        } else if(value=="1"){
            $("#UNIONSERIAL_ID_BOX").ligerGetComboBoxManager().setEnabled();
            $("#UNIONDEFAULT").ligerGetTextBoxManager().setEnabled();
        }
    }
    });

    $("#SERIAL_TYPES_BOX").ligerComboBox({ onShow: function () {
        var varSerialType = $("#SERIAL_TYPE_BOX").val();
        if (varSerialType == "") {
            return $.ligerDialog.warn('请选择编号类型！');
        }
    }
    });
        detailWin = $.ligerDialog.open({
            target: $("#detail"),
            width: 680, height: 300, top: 90, title: "编码规则信息",
            buttons: [
                  { text: '确定', onclick: function () { save(); } },
                  { text: '取消', onclick: function () { clearDialogValue(); detailWin.hide(); } }
                  ]
        });
    }
    if (curentData) {
        $("#ID").val(curentData.ID);
        $("#SERIAL_NAME").val(curentData.SERIAL_NAME);
        $("#SERIAL_RULE").val(curentData.SERIAL_RULE);
        $("#SERIAL_NUMBER_BIT").val(curentData.SERIAL_NUMBER_BIT);
        $("#SERIAL_START_NUM").val(curentData.SERIAL_START_NUM);
        $("#SERIAL_MAX_NUM").val(curentData.SERIAL_MAX_NUM);
        $("#STATUS_BOX").ligerGetComboBoxManager().setValue(curentData.STATUS);
//        $("#DAYSTATUS_BOX").ligerGetComboBoxManager().setValue(curentData.DAY_STATUS);
        $("#SERIAL_TYPE_BOX").ligerGetComboBoxManager().setValue(curentData.SERIAL_TYPE);
        $("#SERIAL_TYPES_BOX").ligerGetComboBoxManager().setValue(curentData.SERIAL_TYPE_ID);
        $("#SAMPLE_SOURCE_BOX").ligerGetComboBoxManager().setValue(curentData.SAMPLE_SOURCE);

        $("#YESORNO_BOX").ligerGetComboBoxManager().setValue(curentData.IS_UNION);
        $("#UNIONSERIAL_ID_BOX").ligerGetComboBoxManager().setValue(curentData.UNION_SEARIAL_ID);
        $("#UNIONDEFAULT").val(curentData.UNION_DEFAULT);
    }

    function save() {
        //表单验证
        if (!$("#editMonitorTypeform").validate())
            return false;
        curentData = curentData || {};
        curentData.SERIAL_NAME = $("#SERIAL_NAME").val();
        curentData.SERIAL_NUMBER_BIT=  $("#SERIAL_NUMBER_BIT").val();
        curentData.SERIAL_RULE = $("#SERIAL_RULE").val();
        curentData.SERIAL_START_NUM = $("#SERIAL_START_NUM").val();
        curentData.SERIAL_MAX_NUM = $("#SERIAL_MAX_NUM").val();
        curentData.STATUS = $("#STATUS_OP").val();
        curentData.SERIAL_TYPE = $("#SERIAL_TYPE_OP").val();
        curentData.SERIAL_TYPE_ID = $("#SERIAL_TYPES_OP").val();
        curentData.SAMPLE_SOURCE = $("#SAMPLE_SOURCE_OP").val();
        curentData.IS_UNION = $("#YESORNO_OP").val();
        curentData.UNION_SERIAL_ID = $("#UNIONSERIAL_ID_OP").val();
        curentData.UNIONDEFAULT = $("#UNIONDEFAULT").val();
//        curentData.DAY_STATUS = $("#DAYSTATUS_OP").val();
        if ($("#SERIAL_TYPE_OP").val() == "2") {
            if ($("#SAMPLE_SOURCE_OP").val() == "") {
                return $.ligerDialog.warn('请选择样品来源！');
            }
        }
        //var strData = "{" + (currentIsAddNew ? "" : "'strID':'" + $("#ID").val() + "',") + "'strSERIAL_NAME':'" + $("#SERIAL_NAME").val() + "','strSERIAL_TYPE':'" + $("#SERIAL_TYPE_OP").val() + "','strSERIAL_RULE':'" + $("#SERIAL_RULE").val() + "','strSEARIAL_NUMBER_BIT':'" + $("#SERIAL_NUMBER_BIT").val() + "','strMONITOR_TYPE_ID':'" + $("#SERIAL_TYPES_OP").val() + "','strSAMPLE_SOURCE':'" + $("#SAMPLE_SOURCE_OP").val() + "','strStartNum':'" + $("#SERIAL_START_NUM").val() + "','strMaxNum':'" + $("#SERIAL_MAX_NUM").val() + "','strStatus':'" + $("#STATUS_OP").val() + "','strIsUnion':'" + $("#YESORNO_OP").val() + "','strUnionSerialId':'" + $("#UNIONSERIAL_ID_OP").val() + "','strUnionDefault':'" + $("#UNIONDEFAULT").val() + "','strDayStatus':'" + $("#DAYSTATUS_OP").val() + "'}";
        var strData = "{" + (currentIsAddNew ? "" : "'strID':'" + $("#ID").val() + "',") + "'strSERIAL_NAME':'" + $("#SERIAL_NAME").val() + "','strSERIAL_TYPE':'" + $("#SERIAL_TYPE_OP").val() + "','strSERIAL_RULE':'" + $("#SERIAL_RULE").val() + "','strSEARIAL_NUMBER_BIT':'" + $("#SERIAL_NUMBER_BIT").val() + "','strMONITOR_TYPE_ID':'" + $("#SERIAL_TYPES_OP").val() + "','strSAMPLE_SOURCE':'" + $("#SAMPLE_SOURCE_OP").val() + "','strStartNum':'" + $("#SERIAL_START_NUM").val() + "','strMaxNum':'" + $("#SERIAL_MAX_NUM").val() + "','strStatus':'" + $("#STATUS_OP").val() + "','strIsUnion':'" + $("#YESORNO_OP").val() + "','strUnionSerialId':'" + $("#UNIONSERIAL_ID_OP").val() + "','strUnionDefault':'" + $("#UNIONDEFAULT").val() + "'}";
        $.ajax({
            cache: false,
            type: "POST",
            url: "CodeRuleList.aspx/" + (currentIsAddNew ? "AddData" : "EditData"),
            data: strData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                $('body').append("<div class='jloading'>正在保存数据中...</div>");
                $.ligerui.win.mask();
            },
            complete: function () {
                $('body > div.jloading').remove();
                $.ligerui.win.unmask({ id: new Date().getTime() });
            },
            success: function (data, textStatus) {
                if (data.d == "1") {
                    detailWin.hidden();
                    manager.loadData();
                    clearDialogValue();
                }
                else {
                    $.ligerDialog.warn('保存数据失败！');
                }
            }
        });
    }
}

//监测类别grid 弹出编辑框 清空
function clearDialogValue() {
    $("#SERIAL_NAME").val("");
    $("#SERIAL_RULE").val("");
    $("#SERIAL_TYPE_BOX").ligerGetComboBoxManager().setValue("");
    $("#SERIAL_NUMBER_BIT").val("");
    $("#SERIAL_START_NUM").val("1");
    $("#SERIAL_MAX_NUM").val("");

    $("#STATUS_BOX").ligerGetComboBoxManager().setValue("");
//    $("#DAYSTATUS_BOX").ligerGetComboBoxManager().setValue("");
    $("#SERIAL_TYPES_BOX").ligerGetComboBoxManager().setValue("");
    $("#SAMPLE_SOURCE_BOX").ligerGetComboBoxManager().setValue("");

    $("#YESORNO_BOX").ligerGetComboBoxManager().setValue("");
    $("#UNIONSERIAL_ID_BOX").ligerGetComboBoxManager().setValue("");
    $("#UNIONDEFAULT").val("");

    $("#SAMPLE_SOURCE_BOX").ligerGetComboBoxManager().setDisabled();

    $("#YESORNO_BOX").ligerGetComboBoxManager().setDisabled();
    $("#UNIONSERIAL_ID_BOX").ligerGetComboBoxManager().setDisabled();
    $("#UNIONDEFAULT").ligerTextBox({ disabled: true });
    manager.loadData();
}