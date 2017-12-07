//污染源常规 , 刘静楠
var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;
var objFourGrid = null;

var strOneGridId = "";
var strTwoGridId = "";
var strThreeGridId = "";
var strFourGridId = "";

var strTableName = "T_ENV_P_POLLUTE_ITEM";
var strColumnName = "ITEM_ID";
var strWhereColumnName = "POINT_ID";

var strUrl = "EnterpriseInfo.aspx";

var strOneGridUpdateUrl = "EnterpriseEdit.aspx";
var strOneGridTitle = "企业信息";
var strOneGridAddName = "企业信息增加";
var strOneGridUpdateName = "企业信息编辑";
var intOneGridWidth = 700;
var intOneGridHeight = 450;

var strTwoGridUpdateUrl = "PolluteType.aspx";
var strTwoGridTitle = "类别管理";
var strTwoGridAddName = "类别信息增加";
var strTwoGridUpdateName = "类别信息编辑";
var intTwoGridWidth = 500;
var intTwoGridHeight = 200;

var strThreeGridUpdateUrl = "PolluteEidt.aspx";
var strThreeGridTitle = "监测点信息";
var strThreeGridAddName = "监测点信息增加";
var strThreeGridUpdateName = "监测点信息编辑";
var intThreeGridWidth = 800;
var intThreeGridHeight = 450;

var strFourGridTitle = "监测项目信息";
var strFourGridAddName="监测项目信息增加";
var strFourGridUpdateName="监测项目信息编辑";
var intFourGridWidth = 464;
var intFourGridHeight = 370;

var gridHeight = $(window).height() / 2;
var menu, subMenu;
//企业信息
$(document).ready(function () {
    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', icon: 'modify' },

            { id: 'menudel', text: '删除', icon: 'delete' }
            ]
    });
    objOneGrid = $("#oneGrid").ligerGrid({
        title: strOneGridTitle,
        dataAction: 'server',
        usePager: true,
        rownumbers: true,
        pageSize: 100,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [100, 200, 300],
        height: "320",
        url: strUrl + '?type=getOneGridInfo',
        columns: [
         { display: '企业代码', name: 'ENTER_CODE', align: 'left', width: 100, minWidth: 60 },
         { display: '企业名称', name: 'ENTER_NAME', align: 'left', width: 150, minWidth: 60 },
         { display: '所属省份', name: 'PROVINCE_ID', align: 'left', width: 100, minWidth: 60, render: function (record) {
             return getDictName(record.PROVINCE_ID, 'province');
         } 
         },
         { display: '所在地区', name: 'AREA_ID', align: 'left', width: 100, minWidth: 60, render: function (record) {
             return getDictName(record.AREA_ID, 'administrative_area');
         } 
         },
//         { display: '行业名称', name: 'LEVEL1', align: 'left', width: 140, minWidth: 60, render: function (record) {
//             return getDictName(record.LEVEL1, 'LEVEL1');
//         }  
//         },
//         { display: '二级行业名称', name: 'LEVEL2', align: 'left', width: 140, minWidth: 60, render: function (record) {
//             return getDictNames(record.LEVEL2);
//         } 
//         },
//         { display: '三级行业名称', name: 'LEVEL3', align: 'left', width: 140, minWidth: 60, render: function (record) {
//             return getDictNames(record.LEVEL3);
//         }
//         },
         { display: '企业类型', name: 'REMARK1', align: 'left', width: 150, minWidth: 60, render: function (record) {
             return getDictName(record.REMARK1, 'EnterpriseType');
         } 
         }
        ],
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
            strOneGridId = rowdata.ID;
            strTwoGridId = "";
           strThreeGridId = "";
           strFourGridId = "";
            //点击的时候加载类别数据
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID);
            //点击的时候加载监测点数据
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=undifine");
            //点击的时候加载监测点数据
            objFourGrid.set('url', strUrl + "?type=getFourGridInfo&threeGridId=undifine");
        }, onBeforeCheckAllRow: function (checked, grid, element) { return false; }
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
                    $.ligerDialog.success(obj.msg, '保存成功');
                }
                else {
                    $.ligerDialog.warn(obj.msg, '保存失败');
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
                    $.ligerDialog.success(obj.msg, '更新成功');
                }
                else {
                    $.ligerDialog.warn(obj.msg, '更新失败');
                }
                objOneGrid.loadData();
            }
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: strOneGridUpdateUrl + '?id=' + objOneGrid.getSelectedRow().ID
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
});
//类别管理
 $(document).ready(function () {
     //菜单
     subMenu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', icon: 'modify' },

            { id: 'menudel', text: '删除',  icon: 'delete' }
            ]
     });
     objTwoGrid = $("#twoGrid").ligerGrid({
       title: strTwoGridTitle,
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: "320",
                columns: [
                    { display: '类别', name: 'TYPE_NAME', align: 'left', width: 200, minWidth: 60  }
                ],
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
         },
         onCheckRow: function (checked, rowdata, rowindex) {
             for (var rowid in this.records)
                 this.unselect(rowid);
             this.select(rowindex);
             strThreeGridId = "";
             strFourGridId = "";
             strTwoGridId = rowdata.ID;
             //点击的时候加载垂线数据
             objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID);
             //点击的时候加载监测点数据
             objFourGrid.set('url', strUrl + "?type=getFourGridInfo&threeGridId=undifine");
         }, onBeforeCheckAllRow: function (checked, grid, element) { return false; }
     });
     $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
     //增加数据
     function createData() {
         if (objOneGrid.getSelectedRow() == null) {
             $.ligerDialog.warn('请先选择企业信息');
             return;
         }
         $.ligerDialog.open({ title: strTwoGridAddName, width: intTwoGridWidth, height: intTwoGridHeight, isHidden: false, buttons:
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
            objTwoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: strTwoGridUpdateUrl+ "?oneGridId=" + strOneGridId
         });
     }
     //修改数据
     function updateData() {
         if (!objTwoGrid.getSelectedRow()) {
             $.ligerDialog.warn('请选择一条记录进行编辑');
             return;
         }
         $.ligerDialog.open({ title: strTwoGridUpdateName, width: intTwoGridWidth, height: intTwoGridHeight, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据更新成功')
            }
            else {
                $.ligerDialog.warn('数据更新失败');
            }
            objTwoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: strTwoGridUpdateUrl + '?id=' + objTwoGrid.getSelectedRow().ID
         });
     }
     //删除数据
     function deleteData() {
         if (objTwoGrid.getSelectedRow() == null) {
             $.ligerDialog.warn('请选择一条记录进行删除');
             return;
         }
         $.ligerDialog.confirm("确认删除监测点信息吗？", function (yes) {
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

 //监测点管理
 $(document).ready(function () {
     //菜单
     subMenu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', icon: 'modify' },

            { id: 'menudel', text: '删除', icon: 'delete' }
            ]
     });
     objThreeGrid = $("#threeGrid").ligerGrid({
         title: strThreeGridTitle,
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
         columns: [
                    { display: '年', name: 'YEAR', align: 'left', width: 100, minWidth: 60 },
                    { display: '月', name: 'MONTH', align: 'left', width: 100, minWidth: 60 },
                     { display: '监测点代码', name: 'POINT_CODE', align: 'left', width: 100, minWidth: 60 },
                     { display: '监测点名称', name: 'POINT_NAME', align: 'left', width: 100, minWidth: 60 }
                ],
         toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' },
                { line: true },
                { text: '复制', click: copyData, icon: 'page_copy' }
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
         },
         onCheckRow: function (checked, rowdata, rowindex) {
             for (var rowid in this.records)
                 this.unselect(rowid);
             this.select(rowindex);
             strFourGridId = "";
             strThreeGridId = rowdata.ID;
             //点击的时候加载监测点数据
             objFourGrid.set('url', strUrl + "?type=getFourGridInfo&threeGridId=" + rowdata.ID);
         }, onBeforeCheckAllRow: function (checked, grid, element) { return false; }
     });
     $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
     //增加数据
     function createData() {
         if (objTwoGrid.getSelectedRow() == null) {
             $.ligerDialog.warn('请先选择类别');
             return;
         }
         $.ligerDialog.open({ title: strThreeGridAddName, width: intThreeGridWidth, height: intThreeGridHeight, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            var obj = $("iframe")[0].contentWindow.DataSaveN();
            //  if ($("iframe")[0].contentWindow.DataSave())
            if (obj != null) {
                if (obj.result == "success") {
                    dialog.close();
                    $.ligerDialog.success(obj.msg, '数据保存成功')
                }
                else {
                    $.ligerDialog.warn(obj.msg, '数据保存失败');
                }
                objThreeGrid.loadData();
            }
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: strThreeGridUpdateUrl + "?twoGridId=" + strTwoGridId
         });
     }
     //修改数据
     function updateData() {
         if (!objThreeGrid.getSelectedRow()) {
             $.ligerDialog.warn('请选择一条记录进行编辑');
             return;
         }
         $.ligerDialog.open({ title: strThreeGridUpdateName, width: intThreeGridWidth, height: intThreeGridHeight, isHidden: false, buttons:
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
                    $.ligerDialog.warn(obj.msg, '数据更新失败');
                }
                objThreeGrid.loadData();
            }
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: strThreeGridUpdateUrl + '?id=' + objThreeGrid.getSelectedRow().ID
         });
     }
     //删除数据
     function deleteData() {
         if (objThreeGrid.getSelectedRow() == null) {
             $.ligerDialog.warn('请选择一条记录进行删除');
             return;
         }
         $.ligerDialog.confirm("确认删除监测点信息吗？", function (yes) {
             if (yes == true) {
                 var strValue = objThreeGrid.getSelectedRow().ID;
                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: strUrl + "/deleteThreeGridInfo",
                     data: "{'strValue':'" + strValue + "'}",
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (data, textStatus) {
                         if (data.d == "1") {
                             objThreeGrid.loadData();
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
     //复制数据
     function copyData() {
         $.ligerDialog.open({ title: "复制信息", width: 720, height: 250, isHidden: false, buttons: [{
             text:
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
                    data: "{'strTable':'T_ENV_P_POLLUTE','strYear':'" + Year_To + "', 'strMonth':'" + Month_To + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            $.ligerDialog.confirm("已经存在" + Year_To + "年" + Month_To + "月的点位信息，是否覆盖？<br><a style='color:Red'>注意：覆盖之后之前的点位信息将被删除无法恢复</a>",
                            function (yes) {
                                if (yes == true) {
                                    var rText = $("iframe")[0].contentWindow.DataCopy("type=copyPolluteItem&Year_From=" + Year_From + "&Month_From=" + Month_From + "&Year_To=" + Year_To + "&Month_To=" + Month_To);
                                    var d = eval(rText);
                                    if (d.result) {
                                        dialog.close();
                                        $.ligerDialog.success('复制成功')
                                    }
                                    else {
                                        dialog.close();
                                        $.ligerDialog.warn('复制失败：' + d.msg)
                                    }
                                    objThreeGrid.loadData();
                                }
                            });
                        }
                        else {
                            var rText = $("iframe")[0].contentWindow.DataCopy("type=copyPolluteItem&Year_From=" + Year_From + "&Month_From=" + Month_From + "&Year_To=" + Year_To + "&Month_To=" + Month_To);
                            var d = eval(rText);
                            if (d.result) {
                                dialog.close();
                                $.ligerDialog.success('复制成功')
                            }
                            else {
                                dialog.close();
                                $.ligerDialog.warn('复制失败：' + d.msg)
                            }
                            objThreeGrid.loadData(); 
                        }
                    }
                });
            }
        }
         }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
         }
         ], url: "../PointInfoCopy.aspx"
         });
     }
 });
 //监测项目管理
 $(document).ready(function () {
     //菜单
     subMenu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', icon: 'modify' },

            { id: 'menudel', text: '删除', icon: 'delete' }
            ]
     });
     objFourGrid = $("#FourGrid").ligerGrid({
         title: strFourGridTitle,
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
         columns: [
                    { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
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
         },
         onCheckRow: function (checked, rowdata, rowindex) {
             for (var rowid in this.records)
                 this.unselect(rowid);
             this.select(rowindex);
         }, onBeforeCheckAllRow: function (checked, grid, element) { return false; }
     });
     $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
     //监测项目设置
     function ItemSetting() {
         if (objOneGrid.getSelectedRow() == null) {
             $.ligerDialog.warn('请先选择企业信息');
             return;
         }
         if (objTwoGrid.getSelectedRow() == null) {
             $.ligerDialog.warn('请先选择类别');
             return;
         }
         if (objThreeGrid.getSelectedRow() == null) {
             $.ligerDialog.warn('请先选择监测点');
             return;
         }
         strItemListTitleName = encodeURIComponent(objThreeGrid.getSelectedRow().POINT_NAME);
         $.ligerDialog.open({ title: strFourGridAddName, width: intFourGridWidth, height: intFourGridHeight, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            var strItemValue = $("iframe")[0].contentWindow.getSelectedData();
            SaveItemData(strItemValue);
            dialog.close();
            objFourGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../ItemInfoSetting.aspx?TableName=" + strTableName + "&strItemListTitleName=" + strItemListTitleName + "&strWhereColumnName=" + strWhereColumnName + "&ColumnName=" + strColumnName + "&PointCode=" + strThreeGridId + "&MonitorType=" + GlobalMonitorType
         });
     }
     function SaveItemData(strValue) {
         $.ajax({
             cache: false,
             type: "POST",
             url: strUrl + "/saveItemData",
             data: "{'strVerticalCode':'" + strThreeGridId + "','strValue':'" + strValue + "'}",
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (data, textStatus) {
                 if (data.d == "1") {
                     objThreeGrid.loadData();
                     $.ligerDialog.success('保存监测项目成功')
                 }
                 else {
                     $.ligerDialog.warn('保存监测项目失败');
                 }
             }
         });
     }
     //把需要复制的垂线ID保存起来
     function CopyV() {
         if (strThreeGridId == "") {
             $.ligerDialog.warn('请先选择监测点');
             return;
         }
         $("#VerticalID")[0].value = strThreeGridId;
     }
     //粘贴垂线的监测项目
     function PasteItem() {
         if ($("#VerticalID").val() == "") {
             $.ligerDialog.warn('请先复制监测点');
             return;
         }
         if (strThreeGridId == "") {
             $.ligerDialog.warn('请先选择监测点');
             return;
         }
         $.ajax({
             cache: false,
             async: true,
             type: "POST",
             url: "../PointInfoCopy.aspx?type=copyPolluteItemEditer&fromID=" + $("#VerticalID").val() + "&toID=" + strThreeGridId,
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
                 objFourGrid.loadData();
             },
             error: function (msg) {
             }
         });
     }
 });
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
//获取字典项信息
function getDictNames(strDictCode, strDictType) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDictNames",
        data: "{'strDictCode':'" + strDictCode + "'}",
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