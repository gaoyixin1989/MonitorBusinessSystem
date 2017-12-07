// Create by 熊卫华 2012.11.06  "属性信息"功能

var objAttributeInfoGrid = null;
var strAttributeInfoId = "";
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

    objAttributeInfoGrid = $("#attributeInfoGrid").ligerGrid({
        title: '属性信息',
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        whenRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '100%',
        url: 'AttributeInfo.aspx?type=getAttributeInfo',
        columns: [

                { display: '属性名称', name: 'ATTRIBUTE_NAME', minWidth: 140 },
                { display: '控件类型', name: 'CONTROL_NAME', minWidth: 140, render: function (record) {
                    return getDictName(record.CONTROL_NAME, 'AttributeControlType');
                }
                },
                { display: '控件宽度', name: 'WIDTH', minWidth: 140 },
                { display: '是否可编辑', name: 'ENABLE', minWidth: 140, render: function (record) {
                    return getDictName(record.ENABLE, 'AttributeYesNo');
                }
                },
                { display: '可否为空', name: 'IS_NULL', minWidth: 140, render: function (record) {
                    return getDictName(record.IS_NULL, 'AttributeYesNo');
                }
                },
                { display: '最大长度', name: 'MAX_LENGTH', minWidth: 140 }
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

            strAttributeInfoId = rowdata.ID;
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //增加数据
    function createData() {
        $.ligerDialog.open({ title: '属性信息增加', width: 400, height: 500, buttons:
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
            objAttributeInfoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'AttributeInfoEdit.aspx'
        });
    }
    //修改数据
    function updateData() {
        if (!objAttributeInfoGrid.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: '属性信息编辑', width: 400, height: 500, buttons:
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
            objAttributeInfoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'AttributeInfoEdit.aspx?id=' + objAttributeInfoGrid.getSelectedRow().ID
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
            var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
            var divmainform = $("#Seachdiv");

            divmainform.ligerForm({
                inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
                fields: [
                                { display: "属性名称", name: "SEA_ATTRIBUTE_NAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "控件类型", name: "SEA_CONTROL_NAME_NAME", newline: true, type: "select", comboboxName: "CONTROL_NAME_NAME", options: { valueFieldID: "CONTROL_NAME_BOX", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "AttributeInfoEdit.aspx?type=getDict&dictType=AttributeControlType"} }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#Seachdetail"),
                width: 350, height: 200, top: 90, title: "属性信息查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }
    }

    function search() {
        var SEA_NAME = encodeURI($("#SEA_ATTRIBUTE_NAME").val());

        var SEA_CONTROL_ID = encodeURI($("#CONTROL_NAME_BOX").val());

        objAttributeInfoGrid.set('url', "AttributeInfo.aspx?type=getAttributeInfo&srhName=" + SEA_NAME + "&srhControlId=" + SEA_CONTROL_ID);
    }

    function clearSearchDialogValue() {
        $("#SEA_ATTRIBUTE_NAME").val("");

        //$("#CONTROL_NAME_BOX").val("");
        $("#CONTROL_NAME_NAME").ligerGetComboBoxManager().setValue("");
    }
    //删除数据
    function deleteData() {
        if (objAttributeInfoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除属性信息 ' + objAttributeInfoGrid.getSelectedRow().ATTRIBUTE_NAME + " 吗？", function (yes) {
            if (yes == true) {
                var strValue = objAttributeInfoGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "AttributeInfo.aspx/deleteAttributeInfo",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objAttributeInfoGrid.loadData();
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
    //获取字典项信息
    function getDictName(strDictCode, strDictType) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "AttributeInfo.aspx/getDictName",
            data: "{'strDictCode':'" + strDictCode + "','strDictType':'" + strDictType + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
});