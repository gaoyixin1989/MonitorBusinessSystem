// Create by 熊卫华 2012.10.31  "仪器管理"功能

var objApparatusInfoGrid = null;
var objApparatusDocumentGrid = null;
var objApparatusCertificGrid = null;
var strApparatusId = "";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

//仪器信息管理
$(document).ready(function () {
    var gridHeight = $(window).height() / 2;

    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: updateData, icon: 'modify' },
            { id: 'menudel', text: '删除', click: deleteData, icon: 'delete' }
            ]
    });
    objApparatusInfoGrid = $("#apparatusInfoGrid").ligerGrid({
        title: '仪器设备',
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "APPARATUS_CODE",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: gridHeight,
        url: 'ApparatusInfo.aspx?type=getApparatusInfo',
        columns: [
                { display: '仪器编号', name: 'APPARATUS_CODE', align: 'left', width: 100, minWidth: 60 },
                { display: '仪器名称', name: 'NAME', minWidth: 120 },
                { display: '规格型号', name: 'MODEL', minWidth: 140 },
                { display: '仪器供应商', name: 'APPARATUS_PROVIDER' }
                ],
        toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' },
                { line: true },
                { text: '检定报警', click: showWarn, icon: 'settings' },
                { line: true },
                { text: '报废报警', click: showScrapWarn, icon: 'settings' },
                { line: true },
                { text: '查询', click: showDetailSrh, icon: 'search' },
                { line: true },
                { text: '批量导入', click: showExcelData, icon: 'excel' }
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

            strApparatusId = rowdata.ID;
            //点击的时候加载仪器资料数据
            objApparatusDocumentGrid.set('url', "ApparatusInfo.aspx?type=getApparatusDocumentInfo&appId=" + rowdata.ID);
            //点击的时候加载仪器检定证书数据
            objApparatusCertificGrid.set('url', "ApparatusInfo.aspx?type=getApparatusCertificInfo&appId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    function showWarn() {
        var surl = '../Channels/Base/Apparatus/ApparatusWarn.aspx?MenuNo=toApparatusWarn&action=ident';
        top.f_addTab('toApparatusWarn', '检定报警', surl);
    }
    function showScrapWarn() {
        var surl = '../Channels/Base/Apparatus/ApparatusWarn.aspx?MenuNo=toApparatusScrapWarn&action=scrap';
        top.f_addTab('toApparatusScrapWarn', '报废报警', surl);
    }

    //数据批量导入功能 黄进军 add 2015-03-27
    function showExcelData() {
        $.ligerDialog.open({ title: "Excel导入界面", width: 500, height: 200, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            var value = $("iframe")[0].contentWindow.Import();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "ImportDemo.aspx"
        });
    }
    
    //增加数据
    function createData() {
        $.ligerDialog.open({ title: '仪器设备增加', top: 0, width: 700, height: 500, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                //dialog.close();
                $.ligerDialog.warn('数据保存失败,请检查是否已存在该编号');
            }
            objApparatusInfoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'ApparatusInfoEdit.aspx'
        });
    }
    //修改数据
    function updateData() {
        if (!objApparatusInfoGrid.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: '仪器设备编辑', top: 0, width: 700, height: 500, buttons:
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
            objApparatusInfoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'ApparatusInfoEdit.aspx?id=' + objApparatusInfoGrid.getSelectedRow().ID
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
                inputWidth: 170, labelWidth: 90, space: 40,
                fields: [
                        { display: "仪器编号", name: "SEA_APPARATUS_CODE", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                        { display: "仪器名称", name: "SEA_NAME", newline: false, type: "text" },
                        { display: "仪器型号", name: "SEA_MODEL", newline: true, type: "text" },
                        { display: "供应商名称", name: "SEA_FITTINGS_PROVIDER", newline: false, type: "text" }
                    ]
            });
            detailWinSrh = $.ligerDialog.open({
                target: $("#Seachdetail"),
                width: 600, height: 200, top: 90, title: "仪器设备查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }
    }
    function search() {
        var SEA_APPARATUS_CODE = encodeURI($("#SEA_APPARATUS_CODE").val());
        var SEA_NAME = encodeURI($("#SEA_NAME").val());
        var SEA_MODEL = encodeURI($("#SEA_MODEL").val());
        var SEA_FITTINGS_PROVIDER = encodeURI($("#SEA_FITTINGS_PROVIDER").val());

        objApparatusInfoGrid.set('url', "ApparatusInfo.aspx?type=getApparatusInfo&srhApparatus_Code=" + SEA_APPARATUS_CODE + "&srh_Name=" + SEA_NAME + "&srh_Model=" + SEA_MODEL + "&srhProvider=" + SEA_FITTINGS_PROVIDER);
    }
    function clearSearchDialogValue() {
        $("#SEA_APPARATUS_CODE").val("");
        $("#SEA_NAME").val("");
        $("#SEA_MODEL").val("");
        $("#SEA_FITTINGS_PROVIDER").val("");
    }
    //删除数据
    function deleteData() {
        if (objApparatusInfoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除仪器信息 ' + objApparatusInfoGrid.getSelectedRow().NAME + " 吗？", function (yes) {
            if (yes == true) {
                var strValue = objApparatusInfoGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "ApparatusInfo.aspx/deleteApparatusInfo",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objApparatusInfoGrid.loadData();
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
});
//仪器资料管理
$(document).ready(function () {
    var gridHeight = $(window).height() / 2;

    //监测类别grid的菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: updateData, icon: 'modify' },

            { id: 'menudel', text: '删除', click: deleteData, icon: 'delete' }
            ]
    });
    objApparatusDocumentGrid = $("#apparatusDocumentGrid").ligerGrid({
        columns: [
               { display: '仪器编号', name: 'APPARATUS_ID', align: 'left', width: 100, minWidth: 60 },
                { display: '仪器名称', name: 'APPARATUS_NAME', minWidth: 120 },
                { display: '资料名称', name: 'APPARATUS_ATT_NAME', minWidth: 140 }
                ], width: '100%', pageSizeOptions: [5, 10, 15, 20], height: gridHeight, heightDiff: -30,
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true, onRClickToSelect: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' },
                { line: true },
                { text: '上传', click: upLoadFile, icon: 'add' },
                { line: true },
                { text: '下载', click: downLoadFile, icon: 'add' }
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

    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    //增加数据
    function createData() {
        if (strApparatusId == "") {
            $.ligerDialog.warn('请先选择仪器设备信息');
            return;
        }
        $.ligerDialog.open({ title: '仪器资料增加', width: 450, height: 150, buttons:
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
            objApparatusDocumentGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'ApparatusInfoFileBakEdit.aspx?ApparatusId=' + strApparatusId
        });
    }
    //修改数据
    function updateData() {
        if (objApparatusDocumentGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        var strApparatusDocumentId = objApparatusDocumentGrid.getSelectedRow().ID;

        $.ligerDialog.open({ title: '仪器设资料备编辑', width: 450, height: 150, buttons:
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
            objApparatusDocumentGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'ApparatusInfoFileBakEdit.aspx?id=' + strApparatusDocumentId
        });
    }
    //删除数据
    function deleteData() {
        if (objApparatusDocumentGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除仪器资料 ' + objApparatusDocumentGrid.getSelectedRow().APPARATUS_ATT_NAME + " 吗？", function (yes) {
            if (yes == true) {
                var strValue = objApparatusDocumentGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "ApparatusInfo.aspx/deleteApparatusDocumentInfo",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objApparatusDocumentGrid.loadData();
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
        if (objApparatusDocumentGrid.getSelectedRow() == null) {
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
            }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=ApparatusDocument&id=' + objApparatusDocumentGrid.getSelectedRow().ID
        });
    }
    ///附件下载
    function downLoadFile() {
        if (objApparatusDocumentGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('下载附件之前请先选择一条记录');
            return;
        }
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=ApparatusDocument&id=' + objApparatusDocumentGrid.getSelectedRow().ID
        });
    }
});
//检定证书管理
$(document).ready(function () {
    var gridHeight = $(window).height() / 2;

    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: updateData, icon: 'modify' },

            { id: 'menudel', text: '删除', click: deleteData, icon: 'delete' }
            ]
    });
    objApparatusCertificGrid = $("#apparatusCertificGrid").ligerGrid({
        columns: [
                { display: '仪器编号', name: 'APPARATUS_CODE', align: 'left', width: 100, minWidth: 60 },
                { display: '仪器名称', name: 'APPARATUS_NAME', minWidth: 120 },
                { display: '检定证书名称', name: 'APPRAISAL_NAME', minWidth: 140 },
                { display: '检定时间', name: 'APPRAISAL_DATE' }
                ], width: '100%', pageSizeOptions: [5, 10, 15, 20], height: gridHeight, heightDiff: -30,
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true, onRClickToSelect: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' },
                { line: true },
                { text: '上传', click: upLoadFile, icon: 'add' },
                { line: true },
                { text: '下载', click: downLoadFile, icon: 'add' }
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
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    //增加数据
    function createData() {
        if (strApparatusId == "") {
            $.ligerDialog.warn('请先选择仪器检定证书信息');
            return;
        }
        $.ligerDialog.open({ title: '检定证书增加', width: 500, height: 400, buttons:
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
            objApparatusCertificGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'ApparatusInfoCertificEdit.aspx?ApparatusId=' + strApparatusId
        });
    }
    //修改数据
    function updateData() {
        if (objApparatusCertificGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        var strobjApparatusCertificId = objApparatusCertificGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: '检定证书编辑', width: 500, height: 400, buttons:
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
            objApparatusCertificGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'ApparatusInfoCertificEdit.aspx?id=' + strobjApparatusCertificId
        });
    }
    //删除数据
    function deleteData() {
        if (objApparatusCertificGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除仪器检定资料信息 ' + objApparatusCertificGrid.getSelectedRow().APPRAISAL_NAME + " 吗？", function (yes) {
            if (yes == true) {
                var strValue = objApparatusCertificGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "ApparatusInfo.aspx/deleteApparatusCertificInfo",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objApparatusCertificGrid.loadData();
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
        if (objApparatusCertificGrid.getSelectedRow() == null) {
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
            }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=ApparatusCertific&id=' + objApparatusCertificGrid.getSelectedRow().ID
        });
    }
    ///附件下载
    function downLoadFile() {
        if (objApparatusCertificGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('下载附件之前请先选择一条记录');
            return;
        }
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=ApparatusCertific&id=' + objApparatusCertificGrid.getSelectedRow().ID
        });
    }
});