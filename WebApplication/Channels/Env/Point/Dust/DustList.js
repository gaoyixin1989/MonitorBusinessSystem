// Create by 熊卫华 2012.11.12  "降尘监测点"功能, Modify by 刘静楠 2012/6/18

var objGrid = null;//objOneGrid
var objTwoGrid = null;
var strOneGridId = "";
var strTwoGridId = "";
var strId = "";

var strTableName = "T_ENV_P_DUST_ITEM"; //表名:降尘监测项目表
var strColumnName = "ITEM_ID"; //监测项目ID
var strWhereColumnName = "POINT_ID"; //点位监测ID

var url = "DustList.aspx";
var updateUrl = "DustEdit.aspx";

var strAddInfo = "降尘监测点信息增加";
var strUpdateInfo = "降尘监测点信息编辑";

var gridWidth = 700;
var gridHeight = 350;

var strTwoGridTitle = "降尘监测点信息项目";
var strTwoGridAddName = "设置监测项目";

var intTwoGridWidth = 460;
var intTwoGridHeight = 370;

var gridAllHeight = $(window).height() /2; //grid高度
var menu;

$(document).ready(function () {
    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: updateData, icon: 'modify' },

            { id: 'menudel', text: '删除', click: deleteData, icon: 'delete' }
            ]
    });

    objGrid = $("#grid").ligerGrid({
        title: '降尘监测点信息',
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: gridAllHeight,
        url: url + '?type=getDataInfo',
        columns: [
                { display: '年度', name: 'YEAR', align: 'left', width: 100, minWidth: 60 },
                { display: '月度', name: 'MONTH', width: 100, minWidth: 60 },
                { display: '测点编码', name: 'POINT_CODE', minWidth: 100 },
                { display: '测点名称', name: 'POINT_NAME', minWidth: 100 },
                { display: '行政区', name: 'AREA_ID', minWidth: 140, render: function (record) {
                    return getDictName(record.AREA_ID, 'administrative_area');
                }
                },
                { display: '控制级别', name: 'CONTRAL_LEVEL_ID', minWidth: 100, render: function (record) {
                    return getDictName(record.CONTRAL_LEVEL_ID, 'control_level');
                }
                }
                ],
        toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '复制', click: copyData, icon: 'page_copy' },
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
            strOneGridId = rowdata.ID;
            //strId = rowdata.ID;
            //点击的时候加载监测项目数据
            objTwoGrid.set('url', url + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //增加数据
    function createData() {
        $.ligerDialog.open({ title: strAddInfo, width: gridWidth, height: gridHeight, isHidden: false, buttons: 
        [
        { text:
        '确定', onclick: function (item, dialog) {
            var obj = $("iframe")[0].contentWindow.DataSaveN();
            //$("iframe")[0].contentWindow.DataSave()
            if (obj != null) {
                if (obj.result == "success") {
                    dialog.close();
                    $.ligerDialog.success(obj.msg, '数据保存成功');
                }
                else {
                    //dialog.close(); 
                    $.ligerDialog.warn(obj.msg, '数据保存失败');
                }
                objGrid.loadData();
            }
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: updateUrl
        });
    }
    //修改数据
    function updateData() {
        if (!objGrid.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: strUpdateInfo, width: gridWidth, height: gridHeight, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            var obj = $("iframe")[0].contentWindow.DataSaveN();
            if (obj != null) {
                if (obj.result == "success") {
                    dialog.close();
                    $.ligerDialog.success(obj.msg, '数据更新成功')
                }
                else {
                    //dialog.close();
                    $.ligerDialog.warn(obj.msg, '数据更新失败');
                }
                objGrid.loadData();
            }
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: updateUrl + '?id=' + objGrid.getSelectedRow().ID
        });
    }
    //复制数据
    function copyData() {
        $.ligerDialog.open({ title: "复制信息", width: 720, height: 250, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {

            var v = $("iframe")[0].contentWindow.formValidate();
            if (v) {
                var Year_From = dialog.frame.$("#StartYear").val();
                var Month_From = dialog.frame.$("#StartMonth").val();
                var Year_To = dialog.frame.$("#EndYear").val();
                var Month_To = dialog.frame.$("#EndMonth").val();
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: "../PointInfoCopy.aspx/isExistData",
                    data: "{'strTable':'T_ENV_P_DUST','strYear':'" + Year_To + "', 'strMonth':'" + Month_To + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            $.ligerDialog.confirm("已经存在" + Year_To + "年" + Month_To + "月的点位信息，是否覆盖？<br><a style='color:Red'>注意：覆盖之后之前的点位信息将被删除无法恢复</a>",
                            function (yes) {
                                if (yes == true) {
                                    var rText = $("iframe")[0].contentWindow.DataCopy("type=copyDust&Year_From=" + Year_From + "&Month_From=" + Month_From + "&Year_To=" + Year_To + "&Month_To=" + Month_To);
                                    var d = eval(rText);
                                    if (d.result) {
                                        dialog.close();
                                        $.ligerDialog.success('复制成功')
                                    }
                                    else {
                                        dialog.close();
                                        $.ligerDialog.warn('复制失败：' + d.msg)
                                    }
                                    objGrid.loadData();
                                }
                            });
                        }
                        else {
                            var rText = $("iframe")[0].contentWindow.DataCopy("type=copyDust&Year_From=" + Year_From + "&Month_From=" + Month_From + "&Year_To=" + Year_To + "&Month_To=" + Month_To);
                            var d = eval(rText);
                            if (d.result) {
                                dialog.close();
                                $.ligerDialog.success('复制成功')
                            }
                            else {
                                dialog.close();
                                $.ligerDialog.warn('复制失败：' + d.msg)
                            }
                            objGrid.loadData();
                        }
                    }
                });
            }
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../PointInfoCopy.aspx"
        });
    }
    //删除数据
    function deleteData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除已经选择的信息吗？', function (yes) {
            if (yes == true) {
                var strValue = objGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: url + "/deleteDataInfo",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objGrid.loadData();
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

    //监测项目信息
    $(document).ready(function () {
        objTwoGrid = $("#twoGrid").ligerGrid({
            title: strTwoGridTitle,
            dataAction: 'server',
            usePager: true,
            pageSize: 10,
            alternatingRow: false,
            checkbox: true,
            onRClickToSelect: true,
            width: '100%',
            pageSizeOptions: [5, 10, 15, 20],
            height: gridAllHeight,
            heightDiff: -10,
            columns: [
                    { display: '监测项目', name: 'VERTICAL_NAME', align: 'left', width: 100, minWidth: 60, frozen: true, render: function (record) {
                        return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                    }
                    }
                ],
            //功能按钮
            toolbar: { items: [
                { text: '设置监测项目', click: ItemSetting, icon: 'add' },
                { line: true },
                { text: '复制', click: CopyV, icon: 'page_copy' },
                { line: true },
                { text: '粘贴', click: PasteItem, icon: 'page_copy' }
                ]
            },
            onCheckRow: function (checked, rowdata, rowindex) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);

                strTwoGridId = rowdata.ID;
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }
        });
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
        //监测项目设置
        function ItemSetting() {
            if (objGrid.getSelectedRow() == null) {
                $.ligerDialog.warn('请先选择断面');
                return;
            }
            strItemListTitleName = encodeURIComponent(objGrid.getSelectedRow().POINT_NAME);
            $.ligerDialog.open({ title: strTwoGridAddName, width: intTwoGridWidth, height: intTwoGridHeight, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            var strItemValue = $("iframe")[0].contentWindow.getSelectedData();
            SaveItemData(strItemValue);
            dialog.close();
            objTwoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../ItemInfoSetting.aspx?TableName=" + strTableName + "&strItemListTitleName=" + strItemListTitleName + "&strWhereColumnName=" + strWhereColumnName + "&ColumnName=" + strColumnName + "&PointCode=" + strOneGridId + "&MonitorType=" + GlobalMonitorType
            });
        }
        function SaveItemData(strValue) {
            $.ajax({
                cache: false,
                type: "POST",
                url: url + "/saveItemData",
                data: "{'strPointId':'" + strOneGridId + "','strValue':'" + strValue + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                        objTwoGrid.loadData();
                        $.ligerDialog.success('保存监测项目成功')
                    }
                    else {
                        $.ligerDialog.warn('保存监测项目失败');
                    }
                }
            });
        }
        //把需要复制的环境空气ID保存起来
        function CopyV() {
            if (strOneGridId == "") {
                $.ligerDialog.warn('请先选择降尘监测点信息');
                return;
            }
            $("#VerticalID")[0].value = strOneGridId;
        }
        //粘贴垂线的监测项目
        function PasteItem() {
            if ($("#VerticalID").val() == "") {
                $.ligerDialog.warn('请先复制垂线');
                return;
            }
            $.ajax({
                cache: false,
                async: true,
                type: "POST",
                url: "../PointInfoCopy.aspx?type=CopyDust_Item&fromID=" + $("#VerticalID").val() + "&toID=" + strOneGridId,
                contentType: "application/json; charset=utf-8",
                dataType: "text",
                success: function (data, textStatus) {
                    var d = eval(data);
                    if (d.result) {
                        $.ligerDialog.success('粘贴成功')
                    }
                    else {
                        $.ligerDialog.warn('粘贴失败：' + d.msg)
                    }
                    objTwoGrid.loadData();
                },
                error: function (msg) {
                }
            });
        }
        //删除数据
        function deleteData() {
            if (objTwoGrid.getSelectedRow() == null) {
                $.ligerDialog.warn('请选择一条记录进行删除');
                return;
            }
            $.ligerDialog.confirm("确认删除垂线监测项目信息吗？", function (yes) {
                if (yes == true) {
                    var strValue = objTwoGrid.getSelectedRow().ID;
                    $.ajax({
                        cache: false,
                        type: "POST",
                        url: strUrl + "/deleteTwoGridInfo",
                        data: "{'strValue':'" + strValue + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data, textStatus) {
                            if (data.d == "1") {
                                objTwoGrid.loadData();
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
    //获取监测项目资料信息
    function getItemInfoName(strItemCode, strItemName) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: url + "/getItemInfoName",
            data: "{'strItemCode':'" + strItemCode + "','strItemName':'" + strItemName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
    function refreshGrid(type) {
        if (type == "objOneGrid") {

        }
        if (type == "objTwoGrid") {

        }
        if (type == "objThreeGrid") {

        }
    }
    //获取字典项信息
    function getDictName(strDictCode, strDictType) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: url + "/getDictName",
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