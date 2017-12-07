// Create by 熊卫华 2012.11.20  "近岸海域监测点管理"功能 Modify by 刘静楠 

var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;
var strOneGridId = "";
var strTwoGridId = "";
var strThreeGridId = "";

var strTableName = "T_ENV_P_SEA_ITEM";
var strColumnName = "ITEM_ID";
var strWhereColumnName = "POINT_ID";

var strOneGridTitle = "近岸海域监测点信息";

var strOneGridAddName = "近岸海域监测点信息增加";
var strOneGridUpdateName = "近岸海域监测点信息编辑";

var strUrl = "SeaList.aspx";
var strOneGridUpdateUrl = "SeaEdit.aspx";

var intOneGridWidth = 700;
var intOneGridHeight = 400;

var strTwoGridTitle = "监测项目信息";
var strTwoGridAddName = "设置监测项目";

var intTwoGridWidth = 460;
var intTwoGridHeight = 370;

var gridHeight = $(window).height() / 2;

var menu;

//监测点管理
$(document).ready(function () {
    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: updateData, icon: 'modify' },

            { id: 'menudel', text: '删除', click: deleteData, icon: 'delete' }
            ]
    });

    objOneGrid = $("#oneGrid").ligerGrid({
        title: strOneGridTitle,
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: gridHeight,
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '年度', name: 'YEAR', align: 'left', width: 100, minWidth: 60 },
                { display: '月度', name: 'MONTH', width: 100, minWidth: 60 },
                { display: '点位编码', name: 'POINT_CODE', width: 120 }, 
                { display: '点位名称', name: 'POINT_NAME', minWidth: 120 },
                { display: '点位类别', name: 'POINT_TYPE', render: function (record) {
                    return getDictName(record.POINT_TYPE, 'SeaPoint_Type');
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

            strOneGridId = rowdata.ID;
            //点击的时候加载垂线数据
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //增加数据
    function createData() {
        $.ligerDialog.open({ title: strOneGridAddName, width: intOneGridWidth, height: intOneGridHeight, isHidden: false, buttons: 
        [
        { text:
        '确定', onclick: function (item, dialog) {
            var obj = $("iframe")[0].contentWindow.DataSaveN();
            if (obj != null) {
                if (obj.result == "success") {
                    dialog.close();
                    $.ligerDialog.success(obj.msg, '数据保存成功');
                }
                else {
                    // dialog.close();
                    $.ligerDialog.warn(obj.msg, '数据保存失败');
                }
                objOneGrid.loadData();
            }
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: strOneGridUpdateUrl
        });
    }
    //修改数据
    function updateData() {
        if (!objOneGrid.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: strOneGridUpdateName, width: intOneGridWidth, height: intOneGridHeight, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            var obj = $("iframe")[0].contentWindow.DataSaveN();
            if (obj != null) {
                if (obj.result == "success") {
                    dialog.close();
                    $.ligerDialog.success(obj.msg, '数据更新成功');
                }
                else {
                    //dialog.close();
                    $.ligerDialog.warn(obj.msg, '数据更新失败');
                }
                objOneGrid.loadData();
            }
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: strOneGridUpdateUrl + '?id=' + objOneGrid.getSelectedRow().ID
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
                    data: "{'strTable':'T_ENV_P_SEA','strYear':'" + Year_To + "', 'strMonth':'" + Month_To + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            $.ligerDialog.confirm("已经存在" + Year_To + "年" + Month_To + "月的点位信息，是否覆盖？<br><a style='color:Red'>注意：覆盖之后之前的点位信息将被删除无法恢复</a>",
                            function (yes) {
                                if (yes == true) {
                                    var rText = $("iframe")[0].contentWindow.DataCopy("type=copySealList&Year_From=" + Year_From + "&Month_From=" + Month_From + "&Year_To=" + Year_To + "&Month_To=" + Month_To);
                                    var d = eval(rText);
                                    if (d.result) {
                                        dialog.close();
                                        $.ligerDialog.success('复制成功')
                                    }
                                    else {
                                        dialog.close();
                                        $.ligerDialog.warn('复制失败：' + d.msg)
                                    }
                                    objOneGrid.loadData();
                                }
                            });
                        }
                        else {
                            var rText = $("iframe")[0].contentWindow.DataCopy("type=copySealList&Year_From=" + Year_From + "&Month_From=" + Month_From + "&Year_To=" + Year_To + "&Month_To=" + Month_To);
                            var d = eval(rText);
                            if (d.result) {
                                dialog.close();
                                $.ligerDialog.success('复制成功')
                            }
                            else {
                                dialog.close();
                                $.ligerDialog.warn('复制失败：' + d.msg)
                            }
                            objOneGrid.loadData();
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
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm("确认删除监测点信息吗？", function (yes) {
            if (yes == true) {
                var strValue = objOneGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: strUrl + "/deleteOneGridInfo",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objOneGrid.loadData();
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



    //设置grid 的弹出查询对话框
    var detailWinSrh = null;
    function showDetailSrh() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构
            var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
            var divmainform = $("#Seachdiv");

            divmainform.ligerForm({
                inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
                fields: [
                        { display: "年度", name: "SEA_YEAR", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                        { display: "测点名称", name: "SEA_NAME", newline: false, type: "text" },
                        { display: "功能区代码", name: "SEA_FUNCTION_CODE", newline: true, type: "text" },
                        { display: "点位类别", name: "SEA_POINT_TYPE_NAME", newline: false, type: "select", comboboxName: "POINT_TYPE_NAME_BOX", options: { valueFieldID: "POINT_TYPE_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "" + strOneGridUpdateUrl + "?type=getDict&dictType=SeaPoint_Type"} }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#Seachdetail"),
                width: 600, height: 200, top: 90, title: strOneGridTitle + "查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }
    }

    function search() {
        var SEA_YEAR = encodeURI($("#SEA_YEAR").val());
        var SEA_NAME = encodeURI($("#SEA_NAME").val());
        var SEA_FUNCTION_CODE = encodeURI($("#SEA_FUNCTION_CODE").val());
        var SEA_POINT_TYPE_OP = encodeURI($("#POINT_TYPE_OP").val());

        objOneGrid.set('url', "SeaList.aspx?type=getOneGridInfo&srhYear= " + SEA_YEAR + "&srhName=" + SEA_NAME + "&srhFunctionCode=" + SEA_FUNCTION_CODE + "&srhPointType=" + SEA_POINT_TYPE_OP);


    }

    function clearSearchDialogValue() {
        $("#SEA_YEAR").val("");
        $("#SEA_NAME").val("");
        $("#SEA_FUNCTION_CODE").val("");
        $("#POINT_TYPE_NAME_BOX").ligerGetComboBoxManager().setValue("");
    }

});
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
        height: gridHeight, heightDiff: -10,
        columns: [
                    { display: '监测项目', name: 'VERTICAL_NAME', align: 'left', width: 100, minWidth: 60, frozen: true, render: function (record) {
                        return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                    }
                    }
                ],
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
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请先选择监测点');
            return;
        }
        strItemListTitleName = encodeURIComponent(objOneGrid.getSelectedRow().POINT_NAME);
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
            url: strUrl + "/saveItemData",
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
            $.ligerDialog.warn('请先选择环境监测点信息');
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
            url: "../PointInfoCopy.aspx?type=copySeaEditer&fromID=" + $("#VerticalID").val() + "&toID=" + strOneGridId,
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
        $.ligerDialog.confirm("确认删除监测项目信息吗？", function (yes) {
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
        url: strUrl + "/getDictName",
        data: "{'strDictCode':'" + strDictCode + "','strDictType':'" + strDictType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取监测项目资料信息
function getItemInfoName(strItemCode, strItemName) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getItemInfoName",
        data: "{'strItemCode':'" + strItemCode + "','strItemName':'" + strItemName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}