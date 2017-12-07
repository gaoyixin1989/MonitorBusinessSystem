//书签管理
var firstManager;

$(document).ready(function () {
    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改模板', click: updateData, icon: 'modify' },
            { id: 'menudel', text: '删除模板', click: deleteData, icon: 'delete' }
            ]
    });

    firstManager = $("#firstgrid").ligerGrid({
        title: '模板信息',
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
        url: 'TemplateList.aspx?Action=getTemplateInfo',
        columns: [
                { display: '模板名称', name: 'FILE_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '文件类型', name: 'FILE_TYPE', align: 'left', width: 200, minWidth: 60 },
                  { display: '监测类别', name: 'FILE_DESC', align: 'left', width: 200, minWidth: 60, render: function (record) {
                      return getItemType(record.FILE_DESC);
                  }
                  }
                ],
        toolbar: { items: [
                { text: '增加模板', click: createData, icon: 'add' },
                { line: true },
                { text: '修改模板', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除模板', click: deleteData, icon: 'delete' },
                { line: true },
                { text: '模板配置', click: modelEdit, icon: 'modify' },
                { line: true },
                { text: '书签设置', click: toMark, icon: 'settings' },
                { line: true },
                { text: '签名设置', click: toSign, icon: 'settings' }
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
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //增加数据
    function createData() {
        $.ligerDialog.open({ title: '模板增加', top: 0, width: 400, height: 200, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                // dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }
            firstManager.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'TemplateEdit.aspx'
        });
    }
    //修改数据
    function updateData() {
        if (!firstManager.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: '模板编辑', top: 0, width: 400, height: 200, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据更新成功')
            }
            else {
                // dialog.close();
                $.ligerDialog.warn('数据更新失败');
            }
            firstManager.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'TemplateEdit.aspx?id=' + firstManager.getSelectedRow().ID
        });
    }

    //删除数据
    function deleteData() {
        if (firstManager.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除模板信息 ' + firstManager.getSelectedRow().FILE_NAME + " 吗？", function (yes) {
            if (yes == true) {
                var strValue = firstManager.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "TemplateList.aspx/deleteTemplate",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            firstManager.loadData();
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

    //上传模板文件
    function addloadFile() {
        $.ligerDialog.open({ title: '模板文件上传', width: 450, height: 200,
            buttons: [
            {
                text:
                    '添加', onclick: function (item, dialog) {
                        $("iframe")[0].contentWindow.upLoadFile();
                        firstManager.loadData();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: 'TemplateEdit.aspx?id='
        });
    }

    //修改上传模板文件
    function updateloadFile() {
        if (firstManager.getSelectedRow() == null) {
            $.ligerDialog.warn('上传文件之前请先选择一条记录');
            return;
        }
        var selectId;
        try {
            selectId = firstManager.getSelectedRow().ID;
        }
        catch (e) {
            selectId = "";
        }
        $.ligerDialog.open({ title: '模板文件上传', width: 450, height: 200,
            buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        $("iframe")[0].contentWindow.upLoadFile();
                        firstManager.loadData();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: 'TemplateEdit.aspx?id=' + selectId
        });
    }

    //模板编辑
    function modelEdit() {
        var selectId;
        try {
            selectId = firstManager.getSelectedRow().ID;
        }
        catch (e) {
            selectId = "";
        }
        if (selectId.length <= 0) {
            $.ligerDialog.warn("请选择一个已有模板");
            return false;
        }
        var url = "TemplateModel.aspx?TEMPLATE_ID=" + selectId + "&EDIT_TYPE=1&FILE_TYPE=.doc";
        window.showModalDialog(url, window, 'dialogWidth:900px;dialogHeight:800px;status:no;scroll:no;help:no;resizable:no;center:yes;');

    }

    //书签设置
    function toMark() {
        var surl = '../Channels/Rpt/Mark/MarkList.aspx?MenuNo=toMark';
        top.f_addTab('toMark', '书签设置', surl);
    }

    //签名设置
    function toSign() {
        var surl = '../Channels/Rpt/Sign/SignatureList.aspx?MenuNo=toSign';
        top.f_addTab('toSign', '签名设置', surl);
    }
});

//获得监测类别
function getItemType(value) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "TemplateList.aspx/getItemType",
        data: "{'strValue':'" + value + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        }
    });
    return strReturn;
}
   
