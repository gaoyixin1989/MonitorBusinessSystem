// Create by 熊卫华 2012.11.05  "方法依据"功能

var objMethodInfoGrid = null;
var objAnalysisInfoGrid = null;
var strMethodId = "";

var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
//方法依据管理功能
$(document).ready(function () {
    var gridHeight = $(window).height() / 2;

    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: updateData, icon: 'modify' },

            { id: 'menudel', text: '删除', click: deleteData, icon: 'delete' }
            ]
    });

    objMethodInfoGrid = $("#methodInfoGrid").ligerGrid({
        title: '方法依据',
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "METHOD_CODE",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        whenRClickToSelect: true,
        height: gridHeight,
        url: 'MethodInfo.aspx?type=getMethodInfo',
        columns: [
                { display: '方法依据代码', name: 'METHOD_CODE', align: 'left', width: 200, minWidth: 60 },
                { display: '方法依据名称', name: 'METHOD_NAME', minWidth: 250 },
                { display: '监测类别', name: 'MONITOR_ID', minWidth: 140, render: function (record) {
                    return getMonitorTypeName(record.MONITOR_ID);
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
                { text: '附件上传', click: upLoadFile, icon: 'add' },
                { line: true },
                { text: '附件下载', click: downLoadFile, icon: 'add' }
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

            strMethodId = rowdata.ID;
            //点击的时候加载分析方法数据
            objAnalysisInfoGrid.set('url', "MethodInfo.aspx?type=getAnalysisInfo&appId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //增加数据
    function createData() {
        $.ligerDialog.open({ title: '方法依据增加', width: 400, height: 250, buttons:
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
            objMethodInfoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'MethodInfoEdit.aspx'
        });
    }
    //修改数据
    function updateData() {
        if (!objMethodInfoGrid.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: '方法依据编辑', width: 400, height: 250, buttons:
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
            objMethodInfoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'MethodInfoEdit.aspx?id=' + objMethodInfoGrid.getSelectedRow().ID
        });
    }
    //删除数据
    function deleteData() {
        if (objMethodInfoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除方法依据信息 ' + objMethodInfoGrid.getSelectedRow().METHOD_NAME + " 吗？", function (yes) {
            if (yes == true) {
                var strValue = objMethodInfoGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "MethodInfo.aspx/deleteMethodInfo",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objMethodInfoGrid.loadData();
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
    ///附件上传
    function upLoadFile() {
        if (objMethodInfoGrid.getSelectedRow() == null) {
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
            }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=Method&id=' + objMethodInfoGrid.getSelectedRow().ID
        });
    }
    ///附件下载
    function downLoadFile() {
        if (objMethodInfoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('下载附件之前请先选择一条记录');
            return;
        }
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=Method&id=' + objMethodInfoGrid.getSelectedRow().ID
        });
    }
    //获取监测类别名称
    function getMonitorTypeName(strMonitorTypeName) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "MethodInfo.aspx/getMonitorTypeName",
            data: "{'strValue':'" + strMonitorTypeName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
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
                inputWidth: 170, labelWidth: 90, space: 40,
                fields: [
                        { display: "方法依据代码", name: "SEA_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                        { display: "方法依据名称", name: "SEA_NAME", newline: true, type: "text" },
                        { display: "监测类别", name: "MONITOR_ID_NAME", newline: true, type: "select", comboboxName: "MONITOR_ID_NAME_BOX", options: { valueFieldID: "MONITOR_ID_OP", valueField: "ID", textField: "MONITOR_TYPE_NAME", url: "MethodInfoEdit.aspx?type=getMonitorType"} }
                        ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#Seachdetail"),
                top: 90, width: 350, height: 200, title: "方法依据查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }
    }



    function search() {
        var SEA_CODE = encodeURI($("#SEA_CODE").val());
        var SEA_NAME = encodeURI($("#SEA_NAME").val());
        var SEA_MONITOR_ID = encodeURI($("#MONITOR_ID_OP").val());

        objMethodInfoGrid.set('url', "MethodInfo.aspx?type=getMethodInfo&srhCode=" + SEA_CODE + "&srhName= " + SEA_NAME + "&srhMonitorId=" + SEA_MONITOR_ID);


    }

    function clearSearchDialogValue() {
        $("#SEA_CODE").val("");
        $("#SEA_NAME").val("");
        $("#MONITOR_ID_NAME_BOX").ligerGetComboBoxManager().setValue("");
    }
});
//分析方法管理
$(document).ready(function () {
    var gridHeight = $(window).height() / 2;
    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: updateData, icon: 'modify' },

            { id: 'menudel', text: '删除', click: deleteData, icon: 'delete' }
            ]
    });
    objAnalysisInfoGrid = $("#AnalysisInfoGrid").ligerGrid({
        title: '分析方法',
        columns: [
                { display: '方法依据代码', name: 'APPARATUS_ID', align: 'left', width: 200, minWidth: 60, render: function (record) {
                    return getMonitorCode(record.METHOD_ID);
                }
                },
                { display: '方法依据名称', name: 'APPARATUS_NAME', minWidth: 250, render: function (record) {
                    return getMonitorName(record.METHOD_ID);
                }
                },
                { display: '分析方法名称', name: 'ANALYSIS_NAME', minWidth: 140 }
                ], width: '100%', pageSizeOptions: [5, 10, 15, 20], height: gridHeight, heightDiff: -10,
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' }
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
        if (strMethodId == "") {
            $.ligerDialog.warn('请先选择方法依据信息');
            return;
        }
        $.ligerDialog.open({ title: '分析方法增加', width: 350, height: 150, buttons:
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
            objAnalysisInfoGrid.loadData();
        }
        },
        { text:
        '关闭', onclick: function (item, dialog) {
            dialog.close();
        }
        }
        ], url: 'AnalysisInfoEdit.aspx?MethodId=' + strMethodId
        });
    }
    //修改数据
    function updateData() {
        if (objAnalysisInfoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        var strAnalysisId = objAnalysisInfoGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: '分析方法编辑', width: 350, height: 150, buttons:
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
            objAnalysisInfoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'AnalysisInfoEdit.aspx?id=' + strAnalysisId
        });
    }
    //删除数据
    function deleteData() {
        if (objAnalysisInfoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除分析方法 ' + objAnalysisInfoGrid.getSelectedRow().ANALYSIS_NAME + " 吗？", function (yes) {
            if (yes == true) {
                var strValue = objAnalysisInfoGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "MethodInfo.aspx/deleteAnalysisInfo",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objAnalysisInfoGrid.loadData();
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
    //获取方法依据代码
    function getMonitorCode(strMethodId) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "MethodInfo.aspx/getMonitorCode",
            data: "{'strValue':'" + strMethodId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
    //获取方法依据名称
    function getMonitorName(strMethodId) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "MethodInfo.aspx/getMonitorName",
            data: "{'strValue':'" + strMethodId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
});