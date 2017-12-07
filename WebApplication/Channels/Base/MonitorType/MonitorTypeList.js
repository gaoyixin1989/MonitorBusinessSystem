// Create by 潘德军 2012.11.02  "监测类别管理"功能

var manager;
var menu;
var actionMONITOR_TYPE_ID, actionMONITOR_TYPE_NAME, actionDESCRIPTION, actionIDENTIFY_CODE, actionSORT_NUM;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(function () {
    $("#layout1").ligerLayout({ height: '100%' });

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
        { display: '监测类型', name: 'MONITOR_TYPE_NAME', width: 250, align: 'left', validate: { required: true, maxlength: 50} },
        //        { display: '编码规则', name: 'IDENTIFY_CODE', width: 120, align: 'center' },
        {display: '描述', name: 'DESCRIPTION', width: 600, align: 'left' },
                { display: '显示顺序', name: 'SORT_NUM', width: 100, align: 'center' }
        ], width: '100%', pageSizeOptions: [5, 10, 15, 20], height: '100%',
        url: 'MonitorTypeList.aspx?Action=GetData',
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

            actionMONITOR_TYPE_ID = parm.data.ID;
            actionMONITOR_TYPE_NAME = parm.data.MONITOR_TYPE_NAME;
            actionIDENTIFY_CODE = param.data.IDENTIFY_CODE;
            actionDESCRIPTION = parm.data.DESCRIPTION;
            actionSORT_NUM = param.data.SORT_NUM;
            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetail({
                MONITOR_TYPE_NAME: data.MONITOR_TYPE_NAME,
                IDENTIFY_CODE: data.IDENTIFY_CODE,
                DESCRIPTION: data.DESCRIPTION,
                SORT_NUM:data.SORT_NUM,
                MONITOR_TYPE_ID: data.ID
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
                MONITOR_TYPE_NAME: selected.MONITOR_TYPE_NAME,
                IDENTIFY_CODE:selected.IDENTIFY_CODE,
                DESCRIPTION: selected.DESCRIPTION,
                SORT_NUM: selected.SORT_NUM,
                MONITOR_TYPE_ID: selected.ID
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
                MONITOR_TYPE_NAME: actionMONITOR_TYPE_NAME,
                DESCRIPTION: actionDESCRIPTION,
                SORT_NUM:actionSORT_NUM,
                MONITOR_TYPE_ID: actionMONITOR_TYPE_ID
            }, false);

            break;
        case 'menudel':
            var strDelID = actionMONITOR_TYPE_ID;

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
        url: "MonitorTypeList.aspx/deleteData",
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
                      { name: "MONITOR_TYPE_ID", type: "hidden" },
                      { display: "监测类型", name: "MONITOR_TYPE_NAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
//                      { display: "编码规则", name: "IDENTIFY_CODE", newline: true, type: "text" },
                      {display: "类型描述", name: "DESCRIPTION", newline: true, type: "text" },
                      { display: "显示顺序", name: "SORT_NUM", newline: true, type: "text" }
                    ]
        });
        $("#SORT_NUM").val("1");
        //add validate by ssz
        $("#MONITOR_TYPE_NAME").attr("validate", "[{required:true, msg:'请填写监测类型'},{maxlength:256,msg:'监测类型最大长度为256'}]");
//        $("#IDENTIFY_CODE").attr("validate", "[{maxlength:10,msg:'10'}]");
        $("#DESCRIPTION").attr("validate", "[{maxlength:512,msg:'类型描述最大长度为512'}]");
        $("#SORT_NUM").attr("validate", "[{required:true, msg:'请填写显示顺序'}]");
        detailWin = $.ligerDialog.open({
            target: $("#detail"),
            width: 350, height: 200, top: 90, title: "监测类别信息",
            buttons: [
                  { text: '确定', onclick: function () { save(); } },
                  { text: '取消', onclick: function () { clearDialogValue(); detailWin.hide(); } }
                  ]
        });
    }
    if (curentData) {
        $("#MONITOR_TYPE_NAME").val(curentData.MONITOR_TYPE_NAME);
//        $("#IDENTIFY_CODE").val(curentData.IDENTIFY_CODE);
        $("#DESCRIPTION").val(curentData.DESCRIPTION);
        $("#MONITOR_TYPE_ID").val(curentData.MONITOR_TYPE_ID);
        $("#SORT_NUM").val(curentData.SORT_NUM);
    }

    function save() {
        //表单验证
        if (!$("#editMonitorTypeform").validate())
            return false;
        curentData = curentData || {};
        curentData.MONITOR_TYPE_NAME = $("#MONITOR_TYPE_NAME").val();
//        curentData.IDENTIFY_CODE=  $("#IDENTIFY_CODE").val();
        curentData.DESCRIPTION = $("#DESCRIPTION").val();
        curentData.MONITOR_TYPE_ID = $("#MONITOR_TYPE_ID").val();
        curentData.SORT_NUM = $("#SORT_NUM").val();
        var strData = "{" + (currentIsAddNew ? "" : "'strID':'" + $("#MONITOR_TYPE_ID").val() + "',") + "'strMONITOR_TYPE_NAME':'" + $("#MONITOR_TYPE_NAME").val() + "','strDESCRIPTION':'" + $("#DESCRIPTION").val() + "','strSORT_NUM':'" + $("#SORT_NUM").val() + "'}";

        $.ajax({
            cache: false,
            type: "POST",
            url: "MonitorTypeList.aspx/" + (currentIsAddNew ? "AddData" : "EditData"),
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
                    $.ligerDialog.warn('保存监测类型数据失败！');
                }
            }
        });
    }
}

//监测类别grid 弹出编辑框 清空
function clearDialogValue() {
    $("#MONITOR_TYPE_NAME").val("");
//    $("#IDENTIFY_CODE").val("");
    $("#DESCRIPTION").val("");
    $("#MONITOR_TYPE_ID").val("");
}