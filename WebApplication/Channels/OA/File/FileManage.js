var parent_menu; //根目录菜单
var son_menu; //子目录菜单
var treeManager; //树对象
var selectedNode; //选择节点
var selectedRootname; //选择的目录名称
var fileManager; //档案文件列表
var selectFile; //选择档案文件
var tab; //标签管理对象
var selecttabindex; //选择标签序号
var borrowManager; //借阅
var sendManager; //分发
var checkManager; //修订
var updateManager; //查新
var delManager; //废止
var destroyManager; //销毁
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(function () {
    //整体布局
    var topHeight = $(window).height() / 2;
    var gridHeight = $(window).height() / 2;
    $("#layout1").ligerLayout({ width: '100%', leftWidth: 210, height: '100%' });
    $("#layout2").ligerLayout({ width: '100%', height: '100%', topHeight: topHeight });

    /*============================档案目录管理 Start=========================================*/
    //根节点菜单
    parent_menu = $.ligerMenu({ top: 100, left: 100, width: 120, items:
            [
            { id: 'add', text: '新建文件夹', click: onclickMenu, icon: 'add' }
            ]
    });
    //子节点菜单
    son_menu = $.ligerMenu({ top: 100, left: 100, width: 120, items:
            [
            { id: 'add', text: '新建文件夹', click: onclickMenu, icon: 'add' },
            { line: true },
            { id: 'rename', text: '重命名', click: onclickMenu, icon: 'modify' },
            { line: true },
            { id: 'del', text: '删除', click: onclickMenu, icon: 'archives' }
            ]
    });
    //加载树控件
    treeManager =
    $("#tree").ligerTree({
        url: "FileManage.aspx?action=getFileInfo",
        treeLine: true,
        single: true,
        slide: true,
        nodeDraggable: true,
        parentIcon: "folder",
        childIcon: "folder",
        checkbox: false,
        btnClickToToggleOnly: true,
        nodeDraggable: false,
        nodeWidth: 500,
        onContextmenu: function (node, e) {
            selectedNode = node;
            if (node != null && (node.data.id == "01" || node.data.id == "02" || node.data.id == "03" || node.data.id == "04")) {
                son_menu.hide();
                parent_menu.show({ top: e.pageY, left: e.pageX });
                return false;
            }
            parent_menu.hide();
            son_menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        nodeDraggingRender: function (nodes, draggable, grid) {
            //初始目录文件不允许拖动
            if (nodes.length > 0) {
                if (nodes[0].id == "01" || nodes[0].id == "02" || nodes[0].id == "03" || nodes[0].id == "04")
                    $.ligerDialog.warn("根目录不可移动");
            }
        },
        onSelect: function (node) {
            selectedRootname = node.data.text;
            fileManager.set('url', "FileManage.aspx?type=getDocumentInfo&root_id=" + node.data.id);
        }
    });
    //菜单选择
    function onclickMenu(order) {
        switch (order.id) {
            case 'add':
                addRoot();
                break;
            case "rename":
                rename();
                break;
            case "del":
                deleteRoot();
                break;
            case "upload":
                Upload();
                break;
            case "download":
                Download();
                break;
            default:
                break;
        }
    }
    //新建文件夹
    function addRoot() {
        var node = selectedNode;
        if (node == null || node.data.id == "") {
            $.ligerDialog.warn("请单击选择操作的目录");
            return false;
        }
        //数据库添加
        var strReturnObj;
        $.ajax({
            type: "POST",
            url: "FileManage.aspx?type=addRoot&parent_id=" + node.data.id,
            async: false,
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                strReturnObj = data;
            }
        });
        if (strReturnObj) {
            var nodes = [];
            nodes.push({ text: strReturnObj.DIRECTORY_NAME, id: strReturnObj.ID });
            if (node)
                treeManager.append(node.target, nodes);
            else
                treeManager.append(null, nodes);
        }
        else
            $.ligerDialog.warn("新增失败，请重试或刷新页面！");
    }
    //重命名
    function rename() {
        var node = selectedNode;
        if (node == null || node.data.id == "") {
            $.ligerDialog.warn("请单击选择操作的目录");
            return false;
        }
        $("#edit").show();
        $("#txt_rename").attr("value", node.data.text);
    }
    //删除
    function deleteRoot() {
        var node = selectedNode;
        if (node == null || node.data.id == "") {
            $.ligerDialog.warn("请单击选择操作的目录");
            return false;
        }
        $.ligerDialog.confirm("将会删除所有子目录，确定吗？", function (yes) {
            if (yes == true) {
                //数据库删除
                var strReturn;
                $.ajax({
                    type: "POST",
                    url: "FileManage.aspx?type=delRoot&id=" + node.data.id,
                    async: false,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        strReturn = data;
                    }
                });
                if (strReturn)
                    treeManager.remove(node.target);
                else
                    $.ligerDialog.warn("删除失败，请重试或刷新页面！");
            }
        })
    }
    /*============================档案目录管理 End=========================================*/
    /*============================档案文件管理 Start=========================================*/
    //    档案文件菜单
    menuFile = $.ligerMenu({ top: 100, left: 100, width: 120, items:
            [
            { id: 'add', text: '新建', click: createFile, icon: 'add' },
            { line: true },
            { id: 'edit', text: '修改', click: updateFile, icon: 'modify' },
            { line: true },
            { id: 'delete', text: '废止', click: deleteFile, icon: 'delete' },
            { line: true },
            { id: 'destroy', text: '销毁', click: destroyFile, icon: 'destroy' }
            ]
    });
    //保存类型
    var arrSaveType;
    $.ajax({
        type: "POST",
        async: false,
        url: "FileManageEdit.aspx?type=getDictJson&dict_type=save_type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            arrSaveType = data;
        }
    });
    //保存类型（查询）
    var arrSaveTypeForSearch;
    $.ajax({
        type: "POST",
        async: false,
        url: "FileManageEdit.aspx?type=getDictJsonForSearch&dict_type=save_type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            arrSaveTypeForSearch = data;
        }
    });

    //档案文件列表
    fileManager =
    $("#file").ligerGrid({
        title: "档案文件",
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "DOCUMENT_NAME",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: gridHeight,
        url: 'FileManage.aspx?type=getDocumentInfo',
        columns: [
                { display: '档案编号', name: 'DOCUMENT_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '档案名称', name: 'DOCUMENT_NAME', align: 'left', width: 150, minWidth: 60 },
                { display: '版本号', name: 'VERSION', align: 'left', width: 50, minWidth: 50 },
                { display: '保存类型', name: 'SAVE_TYPE', align: 'left', width: 60, minWidth: 60, render: function (data) {
                    for (var i = 0; i < arrSaveType.length; i++) {
                        if (arrSaveType[i].DICT_CODE == data.SAVE_TYPE)
                            return arrSaveType[i].DICT_TEXT;
                    }
                }
                },
                { display: '保存年份', name: 'SAVE_YEAR', align: 'left', width: 60 },
                { display: '借阅状态', name: 'BORROW_STATUS', align: 'left', width: 60, render: function (data) {
                    var status = BorrowStatus(data.ID);
                    if (status == "1")
                        return "已借出";
                    else
                        return "未借出";
                }
                },
                { display: '使用状态', name: 'IS_DEL', align: 'left', width: 100,minWidth: 60, render: function (data) {
                    if (data.IS_DEL == "1")
                        return "销毁未审核";
                    else if (data.IS_DEL == "0")
                        return "正常";
                }
                }
                ],
        toolbar: { items: [
                        { text: '查询', click: showDetailSrh, icon: 'search' },
                        { line: true },
                        { text: '新建', click: createFile, icon: 'add' },
                        { line: true },
                        { text: '编辑', click: updateFile, icon: 'modify' },
                        { line: true },
                        { id: 'delete', text: '废止', click: deleteFile, icon: 'delete' },
                        { line: true },
                        { id: 'destroy', text: '销毁', click: destroyFile, icon: 'gridwarning' },
                        { line: true },
                        { id: 'upload', text: '上传', click: onclickMenu, icon: 'fileup' },
                        { line: true },
                        { id: 'download', text: '下载', click: onclickMenu, icon: 'filedown' }
                        ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            menuFile.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            updateFile();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            changeTab(selecttabindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    //借阅状态
    function BorrowStatus(document_id) {
        var BorrowStatus;
        $.ajax({
            type: "POST",
            async: false,
            url: "FileManage.aspx?type=getBorrowStatus&document_id=" + document_id,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                BorrowStatus = data;
            }
        });
        return BorrowStatus;
    }
    //档案文件新建
    function createFile() {
        if (!treeManager.getSelected()) {
            $.ligerDialog.warn("请选择目录！");
            return;
        }
        $.ligerDialog.open({ title: '新建文档', top: 0, width: 650, height: 450, buttons:
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
            fileManager.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'FileManageEdit.aspx?directory_id=' + treeManager.getSelected().data.id
        });
    }
    //档案文件修改
    function updateFile() {
        if (!fileManager.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: '文档编辑', top: 0, width: 650, height: 450, buttons:
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
            fileManager.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'FileManageEdit.aspx?id=' + fileManager.getSelectedRow().ID
        });
    }
    //档案文件废止
    function deleteFile() {
        if (!fileManager.getSelectedRow()) {
            $.ligerDialog.warn("请选择档案文件");
            return;
        }
        $.ligerDialog.confirm("确定废止档案'" + fileManager.getSelectedRow().DOCUMENT_NAME + "'吗？", function (yes) {
            if (yes == true) {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "FileManage.aspx?type=delDocumentInfo&document_id=" + fileManager.getSelectedRow().ID,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data == "1")
                            $.ligerDialog.warn("废止成功");
                        else
                            $.ligerDialog.warn("废止失败");
                    }
                });
                fileManager.loadData();
                //delManager.loadData();
            }
        });
    }
    //档案文件销毁
    function destroyFile() {
        if (!fileManager.getSelectedRow()) {
            $.ligerDialog.warn("请选择档案文件");
            return;
        }
        $.ligerDialog.confirm("确定销毁档案'" + fileManager.getSelectedRow().DOCUMENT_NAME + "'吗？", function (yes) {
            if (yes == true) {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "FileManage.aspx?type=destroyDocumentInfo&document_id=" + fileManager.getSelectedRow().ID,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data == "1")
                            $.ligerDialog.warn("销毁成功");
                        else
                            $.ligerDialog.warn("销毁失败");
                    }
                });
                fileManager.loadData();
                //destroyManager.loadData();
            }
        });
    }
    //档案文件查询
    var detailWinSrh = null;
    function showDetailSrh() {
        if (!treeManager.getSelected()) {
            $.ligerDialog.warn("请选择目录！");
            return;
        }
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构
            var divmainform = $("#searchDiv");
            divmainform.ligerForm({
                inputWidth: 150, labelWidth: 90, space: 20,
                fields: [
                        { display: "档案编号", name: "SEA_DOCUMENT_CODE", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                        { display: "保存类型", name: "SEA_SAVE_TYPE", labelWidth: 60, width: 100, newline: false, type: "select", comboboxName: "SAVE_TYPE_ID", options: { valueFieldID: "SEA_SAVE_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", data: arrSaveTypeForSearch} },
                        { display: "档案名称", name: "SEA_DOCUMENT_NAME", newline: true, type: "text", width: 330 },
                        { display: "主题词/关键字", name: "SEA_P_KEY", newline: true, type: "text", width: 330 },
                        { display: "存放位置", name: "SEA_DOCUMENT_LOCATION", newline: true, type: "text", width: 330 }
                    ]
            });
            detailWinSrh = $.ligerDialog.open({
                target: $("#searchDetail"),
                width: 500, height: 250, top: 90, title: "档案查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }
    }
    //查询表单清除
    function clearSearchDialogValue() {
        $("#SEA_DOCUMENT_CODE").val("");
        $("#SAVE_TYPE_ID").val(arrSaveTypeForSearch[0].DICT_TEXT);
        $("#SEA_SAVE_TYPE").val(arrSaveTypeForSearch[0].DICT_CODE);
        $("#SEA_DOCUMENT_NAME").val("");
        $("#SEA_P_KEY").val("");
        $("#SEA_DOCUMENT_LOCATION").val("");
    }
    //查询
    function search() {
        var SEA_DOCUMENT_CODE = encodeURI($("#SEA_DOCUMENT_CODE").val());
        var SEA_SAVE_TYPE = $("#SEA_SAVE_TYPE").val();
        var SEA_DOCUMENT_NAME = encodeURI($("#SEA_DOCUMENT_NAME").val());
        var SEA_P_KEY = encodeURI($("#SEA_P_KEY").val());
        var SEA_DOCUMENT_LOCATION = encodeURI($("#SEA_DOCUMENT_LOCATION").val());

        fileManager.set('url', "FileManage.aspx?type=getDocumentInfo&root_id=" + treeManager.getSelected().data.id + "&document_code=" + SEA_DOCUMENT_CODE + "&save_type=" + SEA_SAVE_TYPE + "&document_name=" + SEA_DOCUMENT_NAME + "&p_key=" + SEA_P_KEY + "&document_location=" + SEA_DOCUMENT_LOCATION);
    }
    /*=====================档案文件管理 End============================================================*/
    /*=====================档案操作历史管理 Start========================================================*/
    //档案管理Tab
    $("#navtab1").ligerTab({ onAfterSelectTabItem: function (tabid) {

        tab = $("#navtab1").ligerGetTabManager();
        selecttabindex = tab.getSelectedTabItemID();
        changeTab(selecttabindex);
    }
    });
    //Tab标签切换事件
    function changeTab(tabid) {
        switch (tabid) {
            // 借阅管理                                                                                                                                                                                                                                                                                                                                      
            case "tabitem1":
                if (fileIsSelected())
                    borrowManager.set('url', "FileManage.aspx?type=getBorrowDocument&document_id=" + fileManager.getSelectedRow().ID);
                break;
            // 分发管理                                                                                                                                                                                                                                                                                                                                      
            case "tabitem2":
                if (fileIsSelected())
                    sendManager.set('url', "FileManage.aspx?type=getSendDocument&document_id=" + fileManager.getSelectedRow().ID);
                break;
            // 修订管理                                                                                                                                                                                                                                                                                                                                      
            case "tabitem3":
                if (fileIsSelected())
                    checkManager.set('url', "FileManage.aspx?type=getCheckDocument&document_id=" + fileManager.getSelectedRow().ID);
                break;
            // 查新管理                                                                                                                                                                                                                                                                                                                                      
            case "tabitem4":
                if (fileIsSelected())
                    updateManager.set('url', "FileManage.aspx?type=getUpdateDocument&document_id=" + fileManager.getSelectedRow().ID);
                break;
            // 废止管理                                                                                                                                                                                                                                                                                                                                      
            case "tabitem5":
                if (rootIsSelected())
                    delManager.set('url', "FileManage.aspx?type=getDelDocument&root_id=" + treeManager.getSelected().data.id);
                break;
            // 销毁管理                                                                                                                                                                                                                                                                                                                                      
            case "tabitem6":
                if (rootIsSelected())
                    destroyManager.set('url', "FileManage.aspx?type=getDestroyDocument&root_id=" + treeManager.getSelected().data.id);
                break;
            default:
                borrowManager.set('url', "FileManage.aspx?type=getBorrowDocument&document_id=" + fileManager.getSelectedRow().ID);
                break;
        }
    }
    //是否选择档案文件
    function fileIsSelected() {
        if (!fileManager.getSelectedRow()) {
            $.ligerDialog.warn("请选择档案文件");
            return false;
        }
        else
            return true;
    }
    //是否选择档案目录
    function rootIsSelected() {
        if (!treeManager.getSelected()) {
            $.ligerDialog.warn("请选择档案目录");
            return false;
        }
        else
            return true;
    }
    /*=====================借阅管理 Start========================================================*/
    //借阅管理
    borrowManager =
    $("#maingrid1").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "",
        width: '100%',
        height: gridHeight,
        heightDiff: -40,
        pageSizeOptions: [5, 10, 15, 20],
        columns: [
                { display: '档案编号', name: 'DOCUMENT_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '借阅人/归还人', name: 'BORROWER_NAME', align: 'left', width: 100, minWidth: 60 },
                { display: '借出/归还时间', name: 'LOAN_TIME', align: 'left', width: 100, minWidth: 60 },
                { display: '借阅操作', name: 'LENT_OUT_STATE', align: 'left', width: 60, minWidth: 60, render: function (data) {
                    if (data.LENT_OUT_STATE == "0") {
                        return "归还";
                    }
                    else if (data.LENT_OUT_STATE == "1")
                        return "借出";
                }
                }
                ],
        toolbar: { items: [
                        { text: '借出', click: createBorrowData, icon: 'fileup' },
                        { line: true },
                        { text: '归还', click: createLendData, icon: 'filedown' },
                        { line: true },
                        { text: '删除', click: deleteBorrowData, icon: 'delete' },
                        { line: true }
                        ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    //新建借阅
    function createBorrowData() {
        if (!fileManager.getSelected()) {
            $.ligerDialog.warn("请选择档案文件！");
            return;
        }
        $.ligerDialog.open({ title: '添加借出记录', top: 0, width: 650, height: 360, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                //$.ligerDialog.warn('数据保存失败');
            }
            borrowManager.loadData();
            fileManager.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'FileBorrowEdit.aspx?document_id=' + fileManager.getSelectedRow().ID
        });
    }
    //新建归还
    function createLendData() {
        if (!fileManager.getSelected()) {
            $.ligerDialog.warn("请选择档案文件！");
            return;
        }
        $.ligerDialog.open({ title: '添加归还记录', top: 0, width: 650, height: 360, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                //$.ligerDialog.warn('数据保存失败');
            }
            borrowManager.loadData();
            fileManager.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'FileLendEdit.aspx?document_id=' + fileManager.getSelectedRow().ID
        });
    }
    //删除借阅记录
    function deleteBorrowData() {
        if (!borrowManager.getSelectedRow()) {
            $.ligerDialog.warn("请选择借阅记录！");
            return;
        }
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "FileManage.aspx?type=deleteFile&borrow_id=" + borrowManager.getSelectedRow().ID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                borrowManager.loadData();
            }
        });
    }
    /*=====================借阅管理 End========================================================*/
    /*=====================分发管理 Start========================================================*/
    //分发管理
    sendManager =
    $("#maingrid2").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "",
        width: '100%',
        height: gridHeight,
        heightDiff: -40,
        pageSizeOptions: [5, 10, 15, 20],
        columns: [
                { display: '档案编号', name: 'DOCUMENT_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '分发对象', name: 'BORROWER', align: 'left', width: 120, minWidth: 60 },
                { display: '份数', name: 'HOLD_TIME', align: 'left', width: 60, minWidth: 60 },
                { display: '分发时间', name: 'LOAN_TIME', align: 'left', width: 100, minWidth: 60 },
                { display: '操作类型', name: 'LENT_OUT_STATE', align: 'left', width: 60, minWidth: 60, render: function (data) {
                    if (data.LENT_OUT_STATE == "0") {
                        return "回收";
                    }
                    else if (data.LENT_OUT_STATE == "1")
                        return "分发";
                }
                }
                ],
        toolbar: { items: [
                        { text: '分发', click: createSendData, icon: 'fileup' },
                        { line: true },
                        { text: '删除', click: deleteSendData, icon: 'delete' },
                        { line: true }
                        ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //分发
    function createSendData() {
        if (!fileManager.getSelected()) {
            $.ligerDialog.warn("请选择档案文件！");
            return;
        }
        //文件功能过滤
        //        if (selectedRootname != "程序文件" && selectedRootname != "作业指导书") {
        //            $.ligerDialog.warn("该类文件不可分发！");
        //            return;
        //        }
        $.ligerDialog.open({ title: '添加分发记录', top: 0, width: 650, height: 400, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                //$.ligerDialog.warn('数据保存失败');
            }
            sendManager.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'FileSendEdit.aspx?document_id=' + fileManager.getSelectedRow().ID
        });
    }
    //删除
    function deleteSendData() {
        if (!sendManager.getSelectedRow()) {
            $.ligerDialog.warn("请选择分发记录！");
            return;
        }
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "FileManage.aspx?type=deleteSendFile&send_id=" + sendManager.getSelectedRow().ID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                sendManager.loadData();
            }
        });
    }
    /*=====================分发管理 End========================================================*/

    /*=====================修订管理 Start========================================================*/
    //修订管理

    //修订类别
    var update_type;
    $.ajax({
        type: "POST",
        async: false,
        url: "FileManageEdit.aspx?type=getDictJson&dict_type=update_type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            update_type = data;
        }
    });

    checkManager =
    $("#maingrid3").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "",
        width: '100%',
        height: gridHeight,
        heightDiff: -40,
        pageSizeOptions: [5, 10, 15, 20],
        columns: [
                { display: '档案编号', name: 'DOCUMENT_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '修订前名称', name: 'OLD_FILE_NAME', align: 'left', width: 150, minWidth: 60 },
                { display: '修订后名称', name: 'DOCUMENT_NAME', align: 'left', width: 150, minWidth: 60 },
                { display: '修订类别', name: 'UPDATE_TYPE', align: 'left', width: 60, minWidth: 60, render: function (data) {
                    for (var i = 0; i < update_type.length; i++) {
                        if (update_type[i].DICT_CODE == data.UPDATE_TYPE)
                            return update_type[i].DICT_TEXT;
                    }
                }
                },
                { display: '修订人', name: 'UPDATE_ID', align: 'left', width: 100, minWidth: 60 },
                { display: '修订时间', name: 'UPDATE_TIME', align: 'left', width: 100, minWidth: 60 }

                ],
        toolbar: { items: [
                        { text: '修订', click: createCheck, icon: 'add' },
                        { line: true },
                        { text: '查看', click: ViewCheck, icon: 'modify' },
                        { line: true },
                        { text: '删除', click: deleteCheck, icon: 'delete' }
                        ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            selectFile = rowdata;
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //修订新建
    function createCheck() {
        if (!fileManager.getSelected()) {
            $.ligerDialog.warn("请选择档案文件！");
            return;
        }
        //文件功能过滤
        //        if (selectedRootname != "程序文件" && selectedRootname != "作业指导书") {
        //            $.ligerDialog.warn("该类文件不可修订！");
        //            return;
        //        }
        $.ligerDialog.open({ title: '档案文件修订', top: 0, width: 650, height: 420, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                $.ligerDialog.warn('数据保存失败');
            }
            checkManager.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'FileCheckEdit.aspx?document_id=' + fileManager.getSelectedRow().ID
        });
    }
    //修订查看
    function ViewCheck() {
        if (!checkManager.getSelected() || !fileManager.getSelected()) {
            $.ligerDialog.warn("请选择修订记录文件！");
            return;
        }
        $.ligerDialog.open({ title: '档案修订记录', top: 0, width: 650, height: 420, buttons: [{ text: "关闭", onclick: function (item, dialog) {
            dialog.close();
        }
        }], url: 'FileCheckEdit.aspx?id=' + checkManager.getSelectedRow().ID + "&document_id=" + fileManager.getSelectedRow().ID
        });
    }
    //修订记录删除
    function deleteCheck() {
        if (!checkManager.getSelectedRow()) {
            $.ligerDialog.warn("请选择修订记录！");
            return;
        }
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "FileManage.aspx?type=deleteCheckFile&check_id=" + checkManager.getSelectedRow().ID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                checkManager.loadData();
            }
        });
    }
    /*=====================修订管理 End========================================================*/

    /*=====================查新管理 Start========================================================*/
    //查新管理
    var check_type;
    $.ajax({
        type: "POST",
        async: false,
        url: "FileManageEdit.aspx?type=getDictJson&dict_type=check_type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            check_type = data;
        }
    });

    updateManager =
    $("#maingrid4").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "",
        width: '100%',
        height: gridHeight,
        heightDiff: -40,
        pageSizeOptions: [5, 10, 15, 20],
        columns: [
                { display: '档案编号', name: 'DOCUMENT_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '查新前名称', name: 'DOCUMENT_NAME', align: 'left', width: 150, minWidth: 60 },
                { display: '查新后名称', name: 'AFTER_NAME', align: 'left', width: 150, minWidth: 60 },
                { display: '查新类别', name: 'UPDATE_WAY', align: 'left', width: 60, render: function (data) {
                    for (var i = 0; i < check_type.length; i++) {
                        if (check_type[i].DICT_CODE == data.UPDATE_WAY)
                            return check_type[i].DICT_TEXT;
                    }
                }
                },
                { display: '查新人', name: 'PERSON_ID', align: 'left', width: 100, minWidth: 60 },
                { display: '查新时间', name: 'UPDATE_TIME', align: 'left', width: 100, minWidth: 60 }
                ],
        toolbar: { items: [
                        { text: '查新', click: createUpdate, icon: 'add' },
                        { line: true },
                        { text: '查看', click: ViewUpdate, icon: 'modify' },
                        { line: true },
                        { text: '删除', click: deleteUpdate, icon: 'delete' }
                        ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            selectFile = rowdata;
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //查新新建
    function createUpdate() {
        if (!fileManager.getSelected()) {
            $.ligerDialog.warn("请选择档案文件！");
            return;
        }
        //文件功能过滤
        //        if (selectedRootname.indexOf("技术标准") <= 0) {
        //            $.ligerDialog.warn("该类文件不可查新！");
        //            return;
        //        }
        $.ligerDialog.open({ title: '档案文件查新', top: 0, width: 650, height: 420, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                $.ligerDialog.warn('数据保存失败');
            }
            updateManager.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'FileUpdateEdit.aspx?document_id=' + fileManager.getSelectedRow().ID
        });
    }
    //查新查看
    function ViewUpdate() {
        if (!updateManager.getSelected() || !fileManager.getSelected()) {
            $.ligerDialog.warn("请选择查新记录文件！");
            return;
        }
        $.ligerDialog.open({ title: '档案查新记录', top: 0, width: 650, height: 420, buttons: [{ text: "关闭", onclick: function (item, dialog) {
            dialog.close();
        }
        }], url: 'FileUpdateEdit.aspx?id=' + updateManager.getSelectedRow().ID + "&document_id=" + fileManager.getSelectedRow().ID
        });
    }
    //查新删除
    function deleteUpdate() {
        if (!updateManager.getSelectedRow()) {
            $.ligerDialog.warn("请选择查新记录！");
            return;
        }
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "FileManage.aspx?type=deleteUpdateFile&update_id=" + updateManager.getSelectedRow().ID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                updateManager.loadData();
            }
        });
    }
    /*=====================查新管理 End========================================================*/

    /*=====================废止管理 Start========================================================*/
    //废止历史
    delManager =
    $("#maingrid5").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "",
        width: '100%',
        height: gridHeight,
        heightDiff: -40,
        pageSizeOptions: [5, 10, 15, 20],
        url: 'FileManage.aspx?type=getDelDocument',
        columns: [
                { display: '档案编号', name: 'DOCUMENT_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '档案名称', name: 'DOCUMENT_NAME', align: 'left', width: 200, minWidth: 60 },
                { display: '版本号/修订数', name: 'VERSION', align: 'left', width: 100, minWidth: 60 },
                { display: '废止时间', name: 'OPERATE_TIME', align: 'left', width: 150 },
                { display: '废止人', name: 'OPERATOR_NAME', align: 'left', width: 60 }
                ],
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    /*=====================废止管理 End========================================================*/

    /*=====================销毁管理 Start========================================================*/
    //销毁记录
    destroyManager =
    $("#maingrid6").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "",
        width: '100%',
        height: gridHeight,
        heightDiff: -40,
        pageSizeOptions: [5, 10, 15, 20],
        url: 'FileManage.aspx?type=getDestroyDocument',
        columns: [
                { display: '档案编号', name: 'DOCUMENT_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '档案名称', name: 'DOCUMENT_NAME', align: 'left', width: 200, minWidth: 60 },
                { display: '版本号/修订数', name: 'VERSION', align: 'left', width: 100, minWidth: 60 },
                { display: '销毁时间', name: 'OPERATE_TIME', align: 'left', width: 150 },
                { display: '销毁人', name: 'OPERATOR_NAME', align: 'left', width: 60 }
                ],
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});
/*=====================销毁管理 End========================================================*/

//重命名确定
function btnRename_Click() {
    var node = selectedNode;
    $.ajax({
        type: "POST",
        url: "FileManage.aspx?type=editRoot&id=" + node.data.id + "&name=" + encodeURIComponent($("#txt_rename").val()),
        async: false,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data == true)
                treeManager.update(node.target, { text: $("#txt_rename").val(), id: node.data.id });
            else
                $.ligerDialog.warn("更新失败，请重试或刷新页面！");
        }
    });
    btnCancel_Click();
}
//重命名取消
function btnCancel_Click() {
    $("#edit").hide();
    $("#txt_rename").attr("value", "");
}
//上传
function Upload() {
    if (fileManager.getSelectedRow() == null) {
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
            }], url: '../ATT/AttFileUpload.aspx?filetype=DocumentFile&id=' + fileManager.getSelectedRow().ID
    });
}
//下载
function Download() {
    if (fileManager.getSelectedRow() == null) {
        $.ligerDialog.warn('上传/下载文件之前请先选择一条记录');
        return;
    }
    $.ligerDialog.open({ title: '文件上传/下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../ATT/AttFileDownLoad.aspx?filetype=DocumentFile&id=' + fileManager.getSelectedRow().ID
    });
}