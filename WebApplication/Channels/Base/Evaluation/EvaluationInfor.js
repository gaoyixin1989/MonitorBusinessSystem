//Create By 胡方扬 2012-11-01
//评价标准管理
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var MonitorTypeList = "", StandardList = "";
var manager;
var menu;
var actionSTANDARD_ID, actionSTANDARD_CODE, actionSTANDARD_NAME, actionSTANDARD_TYPE, actionMONITOR_ID;
var GetMonitor = null, GetStandard = null;
$(document).ready(function () {

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "EvaluationInfor.aspx/GetMonitor",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                GetMonitor = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载监测类别数据失败！');
        }
    });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "EvaluationInfor.aspx/GetStandard",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                GetStandard = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载监测类别数据失败！');
        }
    });

})

$(function () {
    $("#layout1").ligerLayout({ width: '100%', height: '100%' });

    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: itemclickOfMenu, icon: 'modify' },

            { id: 'menudel', text: '删除', click: itemclickOfMenu, icon: 'delete' },
            { id: 'set', text: '阀值设定', click: itemclickOfMenu, icon: 'database_wrench' }
            ]
    });

    ///附件上传
    function upLoadFile() {
        if (manager.getSelectedRow() == null) {
            $.ligerDialog.warn('上传附件之前请先选择一条记录');
            return;
        }
        $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
            buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        $("iframe")[0].contentWindow.upLoadFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=Evaluation&id=' + manager.getSelectedRow().ID
        });
    }
    ///附件下载
    function downLoadFile() {
        if (manager.getSelectedRow() == null) {
            $.ligerDialog.warn('下载附件之前请先选择一条记录');
            return;
        }
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=Evaluation&id=' + manager.getSelectedRow().ID
        });
    }


    window['g'] =
    manager = $("#maingrid").ligerGrid({
        columns: [
                { display: 'ID', name: 'ID', align: 'left', width: 100, minWidth: 60, hide: 'true' },
                { display: '标准代码', name: 'STANDARD_CODE', align: 'left', width: 100, minWidth: 60 },
                { display: '标准名称', name: 'STANDARD_NAME', minWidth: 120 },
                { display: '标准类型', name: 'STANDARD_TYPE', minWidth: 140, data: GetStandard.d, render: function (item) {
                    for (var i = 0; i < GetStandard.d.length; i++) {
                        if (GetStandard.d[i]['DICT_CODE'] == item.STANDARD_TYPE)
                            return GetStandard.d[i]['DICT_TEXT'];
                    }
                    return item.STANDARD_TYPE;
                }
                },
                { display: '监测类型', name: 'MONITOR_ID', minWidth: 140, data: GetMonitor.d, render: function (item) {
                    for (var i = 0; i < GetMonitor.d.length; i++) {
                        if (GetMonitor.d[i]['ID'] == item.MONITOR_ID)
                            return GetMonitor.d[i]['MONITOR_TYPE_NAME'];
                    }
                    return item.MONITOR_ID;
                }
                }
                ],
        title: '评价标准列表',
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        url: 'EvaluationInfor.aspx?action=GetEvaluData',
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
                { id: 'del', text: '删除', click: itemclickOfToolbar, icon: 'delete' },
                { id: 'set', text: '阀值设定', click: itemclickOfToolbar, icon: 'database_wrench' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclickOfToolbar, icon: 'search' },
                 { line: true },
                { text: '附件上传', click: upLoadFile, icon: 'add' },
                { line: true },
                { text: '附件下载', click: downLoadFile, icon: 'add' }
                ]
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetail({
                STANDARD_ID: data.ID,
                STANDARD_CODE: data.STANDARD_CODE,
                STANDARD_NAME: data.STANDARD_NAME,
                STANDARD_TYPE: data.STANDARD_TYPE,
                MONITOR_ID: data.MONITOR_ID
            }, false);
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            actionSTANDARD_ID = parm.data.ID;
            actionSTANDARD_CODE = parm.data.STANDARD_CODE;
            actionSTANDARD_NAME = parm.data.STANDARD_NAME;
            actionSTANDARD_TYPE = parm.data.STANDARD_TYPE;
            actionMONITOR_ID = parm.data.MONITOR_ID;
            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );

    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});

function itemclickOfToolbar(item) {
    switch (item.id) {
        case 'add':
            showDetail(null, true);
            break;
        case 'modify':
            var selected = manager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要编辑的记录！'); return; }
            var rownu = manager.getCheckedRows().length
            if (rownu > 1) { $.ligerDialog.warn('【编辑】操作只能选择一条记录'); return; }

            showDetail({
                STANDARD_ID: selected.ID,
                STANDARD_CODE: selected.STANDARD_CODE,
                STANDARD_NAME: selected.STANDARD_NAME,
                STANDARD_TYPE: selected.STANDARD_TYPE,
                MONITOR_ID: selected.MONITOR_ID
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
        case 'set':
            var selected = manager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要进行配置的记录！'); return; }
            var rownu = manager.getCheckedRows().length
            if (rownu > 1) { $.ligerDialog.warn('【阀值设定】操作只能选择一条记录'); return; }
            var strstandartId = selected.ID;
            var strMonitorName = null, strStandTypeName = null;

            for (var i = 0; i < GetMonitor.d.length; i++) {
                if (selected.MONITOR_ID == GetMonitor.d[i]["ID"]) {
                    strMonitorName = GetMonitor.d[i]["MONITOR_TYPE_NAME"];
                }
            }

            for (var n = 0; n < GetStandard.d.length; n++) {
                if (selected.STANDARD_TYPE == GetStandard.d[n]["DICT_CODE"]) {
                    strStandTypeName = GetStandard.d[n]["DICT_TEXT"];
                }
            }
            $.ligerDialog.open({ url: 'EvaluationTapSetting.aspx?standartId=' + strstandartId + '&strevcode=' + selected.STANDARD_CODE + '&strevname=' + selected.STANDARD_NAME + '&strevtype=' + strStandTypeName + '&strevmonitor=' + strMonitorName + '&strevmonitorid=' + selected.MONITOR_ID + '', height: 580, width: 700, top: 0, title: '阀值设置' });
            break;
        case 'srh':
            showDetailSrh();
            break;
        default:
            break;
    }
}

function itemclickOfMenu(item) {
    switch (item.id) {
        case 'menumodify':
            showDetail({

                STANDARD_ID: actionSTANDARD_ID,
                STANDARD_CODE: actionSTANDARD_CODE,
                STANDARD_NAME: actionSTANDARD_NAME,
                STANDARD_TYPE: actionSTANDARD_TYPE,
                MONITOR_ID: actionMONITOR_ID

            }, false);

            break;
        case 'menudel':
            var strDelID = actionSTANDARD_ID;

            jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                if (confirm)
                    delMonitorType(strDelID);
            });

            break;
        default:

    }
}

function delMonitorType(ids) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "EvaluationInfor.aspx/DelEvaluData",
        data: "{'strID':'" + ids + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == "true") {
                manager.loadData();
                $.ligerDialog.success('数据操作成功！');
            }
            else {
                $.ligerDialog.warn('删除数据操作失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax请求数据失败！');
        }
    });
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
                     { display: "标准编码", name: "SEA_STANDARD_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "标准名称", name: "SEA_STANDARD_NAME", newline: false, type: "text" },
                     { display: "标准类型", name: "SEA_STANDARD_TYPE", newline: true, type: "select", comboboxName: "SEA_STANDARD_TYPE_ID", options: { valueFieldID: "SEA_STANDARD_TYPE_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: GetStandard.d, render: function (item) {
                         for (var i = 0; i < GetStandard.d.length; i++) {
                             if (GetStandard.d[i]['DICT_CODE'] == item.STANDARD_TYPE)
                                 return GetStandard.d[i]['DICT_TEXT'];
                         }
                         return item.STANDARD_TYPE;
                     }
                     }
                     },
                    { display: "监测类型", name: "SEA_MONITOR_ID", newline: false, type: "select", comboboxName: "SEA_MONITOR_Type_ID", options: { valueFieldID: "SEA_MONITOR_ID_OP", valueField: "ID", textField: "MONITOR_TYPE_NAME", data: GetMonitor.d }, render: function (item) {
                        for (var i = 0; i < GetMonitor.d.length; i++) {
                            if (GetMonitor.d[i]['ID'] == item.MONITOR_ID)
                                return GetMonitor.d[i]['MONITOR_TYPE_NAME'];
                        }
                        return item.MONITOR_ID;
                    }
                    }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 660, height: 170, top: 90, title: "监评价标准查询",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SEA_STANDARD_CODE = encodeURI($("#SEA_STANDARD_CODE").val());
        var SEA_STANDARD_NAME = encodeURI($("#SEA_STANDARD_NAME").val());
        var SEA_STANDARD_TYPE_OP = $("#SEA_STANDARD_TYPE_OP").val();
        var SEA_MONITOR_ID_OP = $("#SEA_MONITOR_ID_OP").val();

        manager.set('url', "EvaluationInfor.aspx?action=GetEvaluData&srhStandard_Code=" + SEA_STANDARD_CODE + "&srhStandard_Name=" + SEA_STANDARD_NAME + "&srhStandard_Type=" + SEA_STANDARD_TYPE_OP + "&srhMonitor_Id=" + SEA_MONITOR_ID_OP);


    }
}

function clearSearchDialogValue() {
    $("#SEA_STANDARD_CODE").val("");
    $("#SEA_STANDARD_NAME").val("");
    $("#SEA_STANDARD_TYPE_ID").ligerGetComboBoxManager().setValue("");
    $("#SEA_MONITOR_Type_ID").ligerGetComboBoxManager().setValue("");
}

var detailWin = null, curentData = null, currentIsAddNew;
function showDetail(data, isAddNew) {
    //传值，当为新增时,data为null，isAddNew为true
    curentData = data;
    currentIsAddNew = isAddNew;
    if (detailWin) {
        detailWin.show();
    }
    else {
        //创建表单结构
        var mainform = $("#editMonitorTypeform");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40,
            fields: [
                      { name: "STANDARD_ID", type: "hidden" },
                      { display: "标准编码", name: "STANDARD_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                      { display: "标准名称", name: "STANDARD_NAME", newline: false, type: "text" },

                      { display: "标准类型", name: "STANDARD_TYPE", newline: true, type: "select", comboboxName: "STANDARD_TYPE_ID", options: { valueFieldID: "STANDARD_TYPE_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: GetStandard.d, render: function (item) {
                          for (var i = 0; i < GetStandard.d.length; i++) {
                              if (GetStandard.d[i]['DICT_CODE'] == item.STANDARD_TYPE)
                                  return GetStandard.d[i]['DICT_TEXT'];
                          }
                          return item.STANDARD_TYPE;
                      }
                      }
                      },
            // 写法2                     { display: "标准类型", name: "STANDARD_TYPE", newline: true, type: "select", comboboxName: "STANDARD_TYPE_ID", options: { valueFieldID: "STANDARD_TYPE_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "EvaluationInfor.aspx?action=GetStandardType"} },
                       {display: "监测类型", name: "MONITOR_ID", newline: false, type: "select", comboboxName: "MONITOR_Type_ID", options: { valueFieldID: "MONITOR_ID_OP", valueField: "ID", textField: "MONITOR_TYPE_NAME", data: GetMonitor.d }, render: function (item) {
                           for (var i = 0; i < GetMonitor.d.length; i++) {
                               if (GetMonitor.d[i]['ID'] == item.MONITOR_ID)
                                   return GetMonitor.d[i]['MONITOR_TYPE_NAME'];
                           }
                           return item.MONITOR_ID;
                       }
                   }
            //写法2                      { display: "监测类型", name: "MONITOR_ID", newline: false, type: "select", comboboxName: "MONITOR_Type_ID", options: { valueFieldID: "MONITOR_ID_OP", valueField: "ID", textField: "MONITOR_TYPE_NAME", url: "EvaluationInfor.aspx?action=GetMonitorType"} }
                    ]
        });

        //add validate by ssz
        $("#STANDARD_CODE").attr("validate", "[{required:true, msg:'请填写标准编码'},{maxlength:64,msg:'标准编码最大长度为64'}]");
        $("#STANDARD_NAME").attr("validate", "[{required:true, msg:'请填写标准名称'},{maxlength:256,msg:'标准名称最大长度为256'}]");

        //打开窗口事件
        detailWin = $.ligerDialog.open({
            //要弹出的DIV
            target: $("#detail"),
            title: currentIsAddNew ? '新增数据' : '编辑数据',
            width: 660, height: 180, top: 90,
            //按钮设置
            buttons: [
                  { text: '确定', onclick: function () { save(); } },
                  { text: '取消', onclick: function () { clearDialogValue(); detailWin.hide(); } }
                  ]
        });
    }

    //DIV窗口填充数据
    if (curentData) {
        $("#STANDARD_CODE").ligerTextBox({ nullText: null });
        $("#STANDARD_NAME").ligerTextBox({ nullText: null });
        $("#STANDARD_CODE").attr("class", "l-text-field");
        $("#STANDARD_NAME").attr("class", "l-text-field");
        $("#STANDARD_ID").val(curentData.STANDARD_ID);
        $("#STANDARD_CODE").val(curentData.STANDARD_CODE);
        $("#STANDARD_NAME").val(curentData.STANDARD_NAME);
        $("#STANDARD_TYPE_ID").ligerGetComboBoxManager().setValue(curentData.STANDARD_TYPE);
        $("#MONITOR_Type_ID").ligerGetComboBoxManager().setValue(curentData.MONITOR_ID);

        //        $("#STANDARD_TYPE_OP").val(curentData.STANDARD_TYPE);
        //        $("#MONITOR_ID_OP").val(curentData.MONITOR_ID);
    }
    //    else {
    //        $("#STANDARD_CODE").ligerTextBox({ nullText: '标准编码不能为空' });
    //        $("#STANDARD_NAME").ligerTextBox({ nullText: '标准名称不能为空' });
    //    }
    //保存事件
    function save() {
        //表单验证
        if (!$("#editMonitorTypeform").validate())
            return false;
        curentData = curentData || {};
        //        if ($("#STANDARD_CODE").val() == "" || $("#STANDARD_CODE").val() == "标准编码不能为空") {
        //        return;
        //    }
        //    if ($("#STANDARD_NAME").val() == "" || $("#STANDARD_NAME").val() == "标准名称不能为空") {
        //        return;
        //    }
        curentData.STANDARD_ID = $("#STANDARD_ID").val();
        curentData.STANDARD_CODE = $("#STANDARD_CODE").val();
        curentData.STANDARD_NAME = $("#STANDARD_NAME").val();
        curentData.STANDARD_TYPE = $("#STANDARD_TYPE_OP").val();
        curentData.MONITOR_ID = $("#MONITOR_ID_OP").val();
        var strData = "{" + (currentIsAddNew ? "" : "'strID':'" + $("#STANDARD_ID").val() + "',") + "'strSTANDARD_CODE':'" + $("#STANDARD_CODE").val() + "','strSTANDARD_NAME':'" + $("#STANDARD_NAME").val() + "','strSTANDARD_TYPE':'" + $("#STANDARD_TYPE_OP").val() + "','strMONITOR_ID':'" + $("#MONITOR_ID_OP").val() + "'}";
        var Methold = currentIsAddNew ? "/AddEvaluData" : "/EditEvaluData";
        $.ajax({
            cache: false,
            type: "POST",
            url: "EvaluationInfor.aspx" + Methold + "",
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
            success: function (data) {
                if (data.d == "true") {
                    $.ligerDialog.success("数据操作成功！");
                    detailWin.hidden();
                    manager.loadData();
                    clearDialogValue();
                }
                else {
                    $.ligerDialog.warn('数据操作失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求数据失败！');
            }
        });
    }
}

function clearDialogValue() {
    $("#STANDARD_ID").val("");
    $("#STANDARD_CODE").val("");
    $("#STANDARD_NAME").val("");
    $("#STANDARD_TYPE_ID").ligerGetComboBoxManager().setValue("");
    $("#MONITOR_Type_ID").ligerGetComboBoxManager().setValue("");
}