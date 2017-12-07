// Create by 熊卫华 2012.11.07  "属性配置"功能

var objAttributeConfigGrid = null;
var strAttributeConfigInfoId = "";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

//属性配置管理
$(document).ready(function () {
    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: updateData, icon: 'modify' },

            { id: 'menudel', text: '删除', click: deleteData, icon: 'delete' }
            ]
    });

    objAttributeConfigGrid = $("#attributeConfigGrid").ligerGrid({
        title: '属性配置信息',
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
        url: 'AttributeConfig.aspx?type=getAttributeConfigInfo',
        columns: [

                { display: '监测类别', name: 'ITEM_TYPE', minWidth: 140, render: function (record) {
                    return getMonitorType(record.ITEM_TYPE);
                }
                },
                { display: '属性类别', name: 'ATTRIBUTE_TYPE_ID', minWidth: 140, render: function (record) {
                    return getAttributeType(record.ATTRIBUTE_TYPE_ID);
                }
                },
                { display: '属性名称', name: 'ATTRIBUTE_ID', minWidth: 140, render: function (record) {
                    return getAttributeName(record.ATTRIBUTE_ID);
                }
                }
                ],
        toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' },
                { line: true },
                { text: '查询', click: showDetailSrh, icon: 'search' },
                { line: true },
                { text: '属性类别设置', click: toAttributeType, icon: 'settings' },
                { line: true },
                { text: '属性信息设置', click: toAttributeInfo, icon: 'settings' }
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

            strAttributeConfigInfoId = rowdata.ID;
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //增加数据
    function createData() {
        $.ligerDialog.open({ title: '属性配置信息增加', width: 400, height: 250, buttons:
        [{ text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                //dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }
            objAttributeConfigGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'AttributeConfigEdit.aspx'
        });
    }
    //修改数据
    function updateData() {
        if (!objAttributeConfigGrid.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: '属性配置信息编辑', width: 400, height: 250, buttons:
        [{ text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据更新成功')
            }
            else {
                //dialog.close();
                $.ligerDialog.warn('数据更新失败');
            }
            objAttributeConfigGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'AttributeConfigEdit.aspx?id=' + objAttributeConfigGrid.getSelectedRow().ID
        });
    }

    //属性类别设置
    function toAttributeType() {
        var surl = '../Channels/Base/DynamicAttribute/AttributeTypeInfo.aspx?MenuNo=toAttributeType';
        top.f_addTab('toAttributeType', '属性类别设置', surl);
    }

    //属性信息设置
    function toAttributeInfo() {
        var surl = '../Channels/Base/DynamicAttribute/AttributeInfo.aspx?MenuNo=toAttributeInfo';
        top.f_addTab('toAttributeInfo', '属性信息设置', surl);
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
                { display: "监测类别", name: "SEA_ITEM_TYPE_NAME", newline: true, type: "select", group: "基本信息", groupicon: groupicon, comboboxName: "ITEM_TYPE_NAME_Com", options: { valueFieldID: "ITEM_TYPE_BOX", valueField: "ID", textField: "MONITOR_TYPE_NAME", url: "AttributeConfigEdit.aspx?type=getMonitorTypeInfo"} },
                { display: "属性类别", name: "SEA_ATTRIBUTE_TYPE_ID_NAME", newline: true, type: "select", comboboxName: "ATTRIBUTE_TYPE_ID_NAME", options: { valueFieldID: "ATTRIBUTE_TYPE_ID_BOX", valueField: "ID", textField: "SORT_NAME", url: "AttributeConfigEdit.aspx?type=getAttributeTypeInfo"} },
                { display: "属性", name: "SEA_ATTRIBUTE_ID_NAME", newline: true, type: "select", comboboxName: "ATTRIBUTE_ID_NAME", options: { valueFieldID: "ATTRIBUTE_ID_BOX", valueField: "ID", textField: "ATTRIBUTE_NAME", url: "AttributeConfigEdit.aspx?type=getAttributeInfo"} }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#Seachdetail"),
                width: 400, height: 200, top: 90, title: "属性配置查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }
    }



    function search() {
        var SEA_ITEM_TYPE_NAME = encodeURI($("#ITEM_TYPE_BOX").val());

        var SEA_ATTRIBUTE_TYPE_ID_NAME = encodeURI($("#ATTRIBUTE_TYPE_ID_BOX").val());
        var SEA_ATTRIBUTE_ID_NAME = encodeURI($("#ATTRIBUTE_ID_BOX").val());

        objAttributeConfigGrid.set('url', "AttributeConfig.aspx?type=getAttributeConfigInfo&srh_ItemId=" + SEA_ITEM_TYPE_NAME + "&srh_AttTypeId=" + SEA_ATTRIBUTE_TYPE_ID_NAME + "&srh_AttId=" + SEA_ATTRIBUTE_ID_NAME);


    }

    function clearSearchDialogValue() {
        $("#ITEM_TYPE_NAME_Com").ligerGetComboBoxManager().setValue("");

        $("#ATTRIBUTE_TYPE_ID_NAME").ligerGetComboBoxManager().setValue("");
        $("#ATTRIBUTE_ID_NAME").ligerGetComboBoxManager().setValue("");
    }
    //删除数据
    function deleteData() {
        if (objAttributeConfigGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除属性配置信息吗？', function (yes) {
            if (yes == true) {
                var strValue = objAttributeConfigGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "AttributeConfig.aspx/deleteAttributeConfigInfo",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objAttributeConfigGrid.loadData();
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
            url: "AttributeConfig.aspx/getMonitorType",
            data: "{'strValue':'" + strMonitorTypeCode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
    //获取属性类别
    function getAttributeType(strAttributeTypeCode) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "AttributeConfig.aspx/getAttributeType",
            data: "{'strValue':'" + strAttributeTypeCode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
    //获取属性名称
    function getAttributeName(strAttributeCode) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "AttributeConfig.aspx/getAttributeName",
            data: "{'strValue':'" + strAttributeCode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
});