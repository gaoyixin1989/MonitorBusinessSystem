// Create by 熊卫华 2012.11.06  "属性类别"功能

var objAttributeTypeInfoGrid = null;
var strAttributeTypeInfoId = "";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

//属性类别管理
$(document).ready(function () {
    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: updateData, icon: 'modify' },

            { id: 'menudel', text: '删除', click: deleteData, icon: 'delete' }
            ]
    });

    objAttributeTypeInfoGrid = $("#attributeTypeInfoGrid").ligerGrid({
        title: '属性类别',
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '100%',
        url: 'AttributeTypeInfo.aspx?type=getAttributeTypeInfo',
        columns: [
                { display: '监测类别', name: 'MONITOR_ID', minWidth: 150, render: function (record) {
                    return getMonitorType(record.MONITOR_ID);
                }
                },
                { display: '属性类别名称', name: 'SORT_NAME', minWidth: 140 }
                ],
        toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' },
                { line: true },
                { text: '查询', click: showDetailSrh, icon: 'search' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            updateData();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            strAttributeTypeInfoId = rowdata.ID;
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //增加数据
    function createData() {
        $.ligerDialog.open({ title: '属性类别增加', width: 350, height: 200, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                //dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }
            objAttributeTypeInfoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'AttributeTypeInfoEdit.aspx'
        });
    }
    //修改数据
    function updateData() {
        if (!objAttributeTypeInfoGrid.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: '属性类别编辑', width: 350, height: 200, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据更新成功')
            }
            else {
                //dialog.close();
                $.ligerDialog.warn('数据更新失败');
            }
            objAttributeTypeInfoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'AttributeTypeInfoEdit.aspx?id=' + objAttributeTypeInfoGrid.getSelectedRow().ID
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
            var divmainform = $("#Seachdiv");

            divmainform.ligerForm({
                inputWidth: 160, labelWidth: 120, space: 40, labelAlign: 'right',
                fields: [
                 { display: "属性类别名称", name: "SORT_NAME", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                { display: "监测类别", name: "MONITOR_ID_NAME", newline: true, type: "select", comboboxName: "MONITOR_ID_NAME_Com", options: { valueFieldID: "MONITOR_ID_BOX", valueField: "ID", textField: "MONITOR_TYPE_NAME", url: "AttributeTypeInfoEdit.aspx?type=getMonitorTypeInfo"} }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#Seachdetail"),
                width: 350, height: 200, top: 90, title: "属性类别查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }
    }



    function search() {
        var SEA_NAME = encodeURI($("#SORT_NAME").val());

        var SEA_MONITOR_ID = encodeURI($("#MONITOR_ID_BOX").val());

        objAttributeTypeInfoGrid.set('url', "AttributeTypeInfo.aspx?type=getAttributeTypeInfo&srhSortName=" + SEA_NAME + "&srh_MonitorId=" + SEA_MONITOR_ID);


    }

    function clearSearchDialogValue() {
        $("#SORT_NAME").val("");

        $("#MONITOR_ID_NAME_Com").ligerGetComboBoxManager().setValue("");
    }
    //删除数据
    function deleteData() {
        if (objAttributeTypeInfoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除企业信息 ' + objAttributeTypeInfoGrid.getSelectedRow().SORT_NAME + " 吗？", function (yes) {
            if (yes == true) {
                var strValue = objAttributeTypeInfoGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "AttributeTypeInfo.aspx/deleteAttributeTypeInfo",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objAttributeTypeInfoGrid.loadData();
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
    //获取监测类别
    function getMonitorType(strMonitorTypeCode) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "AttributeTypeInfo.aspx/getMonitorType",
            data: "{'strValue':'" + strMonitorTypeCode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
});