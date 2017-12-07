// Create by 苏成斌 2012.12.14  "采样-点位信息管理的监测项目列表"功能

var strItemId = "";
var ItemGrid;
//INSTRUMENT_NAME

//点位信息管理的监测项目管理功能
$(document).ready(function () {
    //分析方法grid
    ItemGrid = $("#ItemGrid").ligerGrid({
        columns: [
            { display: '监测项目', name: 'ITEM_NAME', width: 140, align: 'left', isSort: false, render: function (record) {
                return getItemName(record.ITEM_ID);
            }
            },
            { display: '采样容器', name: 'SAMPLING_INSTRUMENT', width: 200, align: 'left', isSort: false,
                editor: {
                    type: 'select', valueColumnName: 'ID', displayColumnName: 'INSTRUMENT_NAME',
                    ext:
                            function (rowdata) {
                                return {
                                    selectBoxWidth: 200,
                                    url: "SamplePoint.aspx?type=getSamplingInstrument&strItemId=" + rowdata.ITEM_ID,
                                    onSelected: function (value, text) {
                                        rowdata.SAMPLING_INSTRUMENT = value;
                                        rowdata.REMARK_1 = text;
                                    }
                                };
                            }
                },
                render: function (record) {
                    return record.REMARK_1;
                }
            },
            { display: '现场项目', name: 'IS_SAMPLEDEPT', width: 100, align: 'left', isSort: false, render: function (record) {
                return getItemDept(record.ITEM_ID);
            }
            }
        ],
        width: '100%',
        pageSizeOptions: [5, 8, 10],
        height: '96%',
        url: 'SamplePoint.aspx?Action=GetItems',
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        enabledEdit: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'set', text: '设置监测项目', click: SetData_Item, icon: 'add' },
                { line: true },
                { id: 'copy', text: '复制监测项目', click: copyData_Item, icon: 'page_copy' },
                { id: 'past', text: '粘贴监测项目', click: pastData_Item, icon: 'page_paste' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; },
        onAfterEdit: AfterEdit
    }
    );
    //$(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    if (strMonitorID == "000000002" || strMonitorID.indexOf("000000004") != -1) {
        ItemGrid.changeHeaderText("SAMPLING_INSTRUMENT", "采样设备")
    } 

    var strCopyPointID = "";
    //复制监测项目
    function copyData_Item() {
        var selectedPoint = objPointGrid.getSelectedRow();
        if (!selectedPoint) {
            $.ligerDialog.warn('请先选择要复制的监测点位！');
            return;
        }

        strCopyPointID = selectedPoint.ID;
    }

    //粘贴监测项目
    function pastData_Item() {
        var selectedPoint = objPointGrid.getSelectedRow();
        if (!selectedPoint) {
            $.ligerDialog.warn('请先选择要粘帖的监测点位！');
            return;
        }
        if (selectedPoint.ID == strCopyPointID) {
            return;
        }

        $.ajax({
            cache: false,
            type: "POST",
            url: "SamplePoint.aspx/CopyPointItem",
            data: "{'strCopyID':'" + strCopyPointID + "','strPastID':'" + selectedPoint.ID + "','strSubtaskID':'" + strSubtaskID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    ItemGrid.set('url', "SamplePoint.aspx?type=GetItems&selPointID=" + selectedPoint.ID);
                    $.ligerDialog.success('复制监测项目成功！')
                }
                else {
                    $.ligerDialog.warn('复制监测项目失败！');
                }
            }
        });
    }

    //设置监测项目
    function SetData_Item() {
        var selectedPoint = objPointGrid.getSelectedRow();
        if (!selectedPoint) {
            $.ligerDialog.warn('请先选择监测点位！');
            return;
        }

        parent.$.ligerDialog.open({ title: '设置监测项目', top: 0, width: 500, height: 380, buttons:
        [{ text: '确定', onclick: f_SaveDateItem },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'SamplePointItemEdit.aspx?PointID=' + selectedPoint.ID + '&SubtaskID=' + selectedPoint.SUBTASK_ID
        });
    }

    //save函数
    function f_SaveDateItem(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        strdata = "{'strSubtaskID':'" + strSubtaskID + "'," + strdata + "}";
        $.ajax({
            cache: false,
            type: "POST",
            url: "SamplePoint.aspx/SaveDataItem",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    var selectedPoint = objPointGrid.getSelectedRow();
                    parent.$.ligerDialog.success('数据保存成功');
                    dialog.close();
                    ItemGrid.set('url', "SamplePoint.aspx?type=GetItems&selPointID=" + selectedPoint.POINT_ID + "&strSampleID=" + selectedPoint.ID);
                }
                else {
                    parent.$.ligerDialog.warn('数据保存失败');
                }
            }
        });
    }
    function AfterEdit(e) {
        var id = e.record.ID;
        var strSamplingInstrument = e.record.SAMPLING_INSTRUMENT;
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "SamplePoint.aspx/saveSamplingInstrument",
            data: "{'id':'" + id + "','strSamplingInstrumentId':'" + strSamplingInstrument + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == 1) {
                    ItemGrid.cancelEdit(e.rowindex);
                }
            }
        });
    }
});
//获取监测点位信息
function getPointName(strPointID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SamplePoint.aspx/getPointName",
        data: "{'strValue':'" + strPointID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取监测项目信息
function getItemName(strItemID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SamplePoint.aspx/getItemName",
        data: "{'strValue':'" + strItemID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取监测项目是否是现场项目
function getItemDept(strItemID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SamplePoint.aspx/getItemDept",
        data: "{'strValue':'" + strItemID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}