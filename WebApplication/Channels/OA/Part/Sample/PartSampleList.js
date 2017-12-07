///标准样品管理
///创建人：魏林 2013-09-13

var gridName = null;
var isFisrt = null;
var objOneGrid = null;
var objTwoGrid = null;
var strUrl = "PartSampleList.aspx";
$(document).ready(function () {

    $("#navtab1").ligerTab({ contextmenu: false, onBeforeSelectTabItem: function (tabid) {
    },
        //在点击选项卡之后触发   点击其他的选项卡后，刷新该选项卡，防止CSS样式被串
        onAfterSelectTabItem: function (tabid) {
            var navtab = $("#navtab1").ligerGetTabManager();
            if (tabid == "home") {
                gridName = "0";
            }
            if (tabid == "tabitem1") {
                gridName = "1";
            }
            navtab.reload(navtab.getSelectedTabItemID());
            if (tabid != 'home') {
                isFisrt = false;
                objTwoGrid.set("url", strUrl + '?type=getTwoGridInfo');
            }
            else {
                isFisrt = true;
            }
        }
    });

    objOneGrid = $("#oneGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 30,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        sortorder: "asc",
        width: '100%',
        pageSizeOptions: [30, 50, 60],
        height: '100%',
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '编号', name: 'SAMPLE_CODE', align: 'left', width: 120, minWidth: 60 },
                { display: '项目名称', name: 'SAMPLE_NAME', align: 'left', width: 200, minWidth: 60 },
                { display: '类别', name: 'SAMPLE_TYPE', align: 'left', width: 120, minWidth: 60,
                    render: function (record) {
                        return getDictName(record.SAMPLE_TYPE, 'sample_type');
                    }
                },
                { display: '分类', name: 'CLASS_TYPE', align: 'left', width: 120, minWidth: 60,
                    render: function (record) {
                        return getDictName(record.CLASS_TYPE, 'sample_class');
                    }
                },
                { display: '浓度', name: 'POTENCY', align: 'left', width: 160, minWidth: 60 },
                { display: '总数量', name: 'TOTAL_INVENTORY', align: 'left', width: 120, minWidth: 60 },
                { display: '现有数量', name: 'INVENTORY', align: 'left', width: 120, minWidth: 60 },
                { display: '单位', name: 'UNIT', align: 'left', width: 100, minWidth: 60 },
                { display: '购置日期', name: 'BUY_DATE', align: 'left', width: 140, minWidth: 60 },
                { display: '有效日期', name: 'EFF_DATE', align: 'left', width: 140, minWidth: 60,
                    render: function (record) {
                        if (new Date(currentTime()) > new Date(record.EFF_DATE))
                            return "<div style='color:Red'>" + record.EFF_DATE + "</div>";
                        else
                            return record.EFF_DATE;
                    }
                },
                { display: '样品来源', name: 'SAMPLE_SOURCE', align: 'left', width: 120, minWidth: 60,
                    render: function (record) {
                        return getDictName(record.SAMPLE_SOURCE, 'sample_sources');
                    }
                },
                { display: '质量等级', name: 'LEVEL', align: 'left', width: 120, minWidth: 60,
                    render: function (record) {
                        return getDictName(record.LEVEL, 'sample_level');
                    }
                },
                { display: '保管人', name: 'CARER', align: 'left', width: 100, minWidth: 60 }
                ],
        toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '领用', click: StockOutData, icon: 'TRUE' },
                { line: true },
                { text: '领用明细', click: ViewData, icon: 'archives' },
                { line: true },
                { text: '检索', click: showDetailSrh, icon: 'search' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
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
        }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");


    objTwoGrid = $("#twoGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 30,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [30, 50, 60],
        height: '100%',
        url: strUrl + '?type=getTwoGridInfo',
        columns: [
                { display: '编号', name: 'SAMPLE_CODE', align: 'left', width: 120, minWidth: 60 },
                { display: '项目名称', name: 'SAMPLE_NAME', align: 'left', width: 200, minWidth: 60 },
                { display: '类别', name: 'SAMPLE_TYPE', align: 'left', width: 120, minWidth: 60,
                    render: function (record) {
                        return getDictName(record.SAMPLE_TYPE, 'sample_type');
                    }
                },
                { display: '分类', name: 'CLASS_TYPE', align: 'left', width: 120, minWidth: 60,
                    render: function (record) {
                        return getDictName(record.CLASS_TYPE, 'sample_class');
                    }
                },
                { display: '浓度', name: 'POTENCY', align: 'left', width: 160, minWidth: 60 },
                { display: '总数量', name: 'TOTAL_INVENTORY', align: 'left', width: 120, minWidth: 60 },
                { display: '现有数量', name: 'INVENTORY', align: 'left', width: 120, minWidth: 60 },
                { display: '单位', name: 'UNIT', align: 'left', width: 100, minWidth: 60 },
                { display: '购置日期', name: 'BUY_DATE', align: 'left', width: 140, minWidth: 60 },
                { display: '有效日期', name: 'EFF_DATE', align: 'left', width: 140, minWidth: 60 },
                { display: '样品来源', name: 'SAMPLE_SOURCE', align: 'left', width: 120, minWidth: 60,
                    render: function (record) {
                        return getDictName(record.SAMPLE_SOURCE, 'sample_sources');
                    }
                },
                { display: '质量等级', name: 'LEVEL', align: 'left', width: 120, minWidth: 60,
                    render: function (record) {
                        return getDictName(record.LEVEL, 'sample_level');
                    }
                },
                { display: '保管人', name: 'CARER', align: 'left', width: 100, minWidth: 60 }
                ],
        toolbar: null,
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
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
        }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");


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

//增加数据
function createData() {
    $.ligerDialog.open({ title: "新增标准样品", width: 730, height: 390, isHidden: false, buttons:
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
                    //dialog.close();
                    $.ligerDialog.warn(obj.msg, '保存失败');
                }
                objOneGrid.set("url", strUrl + '?type=getOneGridInfo');
            }
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "PartSampleEdit.aspx?type=add"
    });
}

//JS 获取当前时间
function currentTime() {
    var d = new Date(), str = '';
    str += d.getFullYear() + '/';
    str += d.getMonth() + 1 + '/';
    str += d.getDate();
    return str;
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
            inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                        { display: "编号", name: "SAMPLE_CODE", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                        { display: "名称", name: "SAMPLE_NAME", newline: true, type: "text" },
                        { display: "类别", name: "SAMPLE_TYPE_NAME", newline: true, type: "select", comboboxName: "SAMPLE_TYPE_BOX", options: { valueFieldID: "SAMPLE_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "PartSampleEdit.aspx?type=getDict&dictType=sample_type"} },
                        { display: "分类", name: "CLASS_TYPE_NAME", newline: true, type: "select", comboboxName: "CLASS_TYPE_BOX", options: { valueFieldID: "CLASS_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "PartSampleEdit.aspx?type=getDict&dictType=sample_class" }
                        }
                        ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#Seachdetail"),
            top: 90, width: 350, height: 220, title: "查询",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }
}



function search() {
    var SAMPLE_CODE = encodeURI($("#SAMPLE_CODE").val());
    var SAMPLE_NAME = encodeURI($("#SAMPLE_NAME").val());
    var SAMPLE_TYPE = encodeURI($("#SAMPLE_TYPE").val());
    var CLASS_TYPE = encodeURI($("#CLASS_TYPE").val());

    objOneGrid.set('url', strUrl + "?type=getOneGridInfo&SAMPLE_CODE=" + SAMPLE_CODE + "&SAMPLE_NAME=" + SAMPLE_NAME + "&SAMPLE_TYPE=" + SAMPLE_TYPE + "&CLASS_TYPE=" + CLASS_TYPE);
}

function clearSearchDialogValue() {
    $("#SAMPLE_CODE").val("");
    $("#SAMPLE_NAME").val("");
    $("#SAMPLE_TYPE_BOX").ligerGetComboBoxManager().setValue("");
    $("#CLASS_TYPE_BOX").ligerGetComboBoxManager().setValue("");
}

function StockOutData() {
    var rowSelected = null;
    rowSelected = objOneGrid.getSelectedRow();
    if (rowSelected == null) {
        $.ligerDialog.warn('请选择一行进行操作！');
    } else {
        if (parseInt(rowSelected.INVENTORY == "" ? "0" : rowSelected.INVENTORY) <= 0) {
            $.ligerDialog.warn('库存不足,无法执行领用操作！'); return;
        } else {
            $.ligerDialog.open({ title: '样品领用操作', top: 40, width: 700, height: 420, buttons:
                [{ text: '确定', onclick: f_SaveDate },
                 { text: '返回', onclick: function (item, dialog) { dialog.close(); }
                 }], url: '../PartOutEdit.aspx?strPartId=' + encodeURI(rowSelected.ID) + '&strNeedQuanity=' + rowSelected.INVENTORY
            });
        }
    }

};

function f_SaveDate(item, dialog) {
    
    var fnSave = dialog.frame.GetBaseInfoStr || dialog.frame.window.GetBaseInfoStr;
    var strReques = fnSave();
    if (strReques != "") {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: strUrl + "?type=SaveCollarDate" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    objOneGrid.set('url', strUrl + "?type=getOneGridInfo");
                    $.ligerDialog.success('领用成功！');
                    return;
                }
                else {
                    $.ligerDialog.warn('领用失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！'); return
            }
        });

        dialog.close();
    }

}

function ViewData() {
    var rowSelected = null;
    rowSelected = objOneGrid.getSelectedRow();
    if (rowSelected == null) {
        $.ligerDialog.warn('请选择一行进行操作！');
    } else {
        var tabid = "tabidDetial"
        var surl = '../Channels/OA/Part/Sample/SampleCollarList.aspx?strPartId=' + rowSelected.ID;
        top.f_addTab(rowSelected.ID, '样品领用明细查看', surl);
    }
}