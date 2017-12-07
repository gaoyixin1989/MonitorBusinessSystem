// Create by 潘德军 2012.11.13  "点位信息管理的监测项目列表"功能

var strItemId = "";
var ItemGrid;

//点位信息管理的监测项目管理功能
$(document).ready(function () {
    var gridHeight = $(window).height() / 2;

    //分析方法grid
    ItemGrid = $("#ItemGrid").ligerGrid({
        columns: [
            { display: '监测点', name: 'Point_Name', width: 250, align: 'left', isSort: false, render: function (record) {
                return getPointName(record.POINT_ID);
            }
            },
            { display: '监测项目', name: 'ITEM_NAME', width: 200, align: 'left', isSort: false, render: function (record) {
                return getItemName(record.ITEM_ID);
            }
            }
        ], width: '100%', pageSizeOptions: [5, 8, 10], height: gridHeight, heightDiff: -10,
        url: 'PointList.aspx?Action=GetItems',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        title: '监测项目',
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
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

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
            url: "PointList.aspx/CopyPointItem",
            data: "{'strCopyID':'" + strCopyPointID + "','strPastID':'" + selectedPoint.ID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    ItemGrid.set('url', "PointList.aspx?type=GetItems&selPointID=" + selectedPoint.ID);
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
        var MonitorID = selectedPoint.MONITOR_ID;
        if (selectedPoint.REMARK1 != "") {
            MonitorID = selectedPoint.REMARK1;
        }
        $.ligerDialog.open({ title: '设置监测项目', top: 0, width: 480, height: 400, buttons:
        [{ text: '确定', onclick: f_SaveDateItem },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'PointItemEdit.aspx?PointID=' + selectedPoint.ID + '&MonitorType=' + MonitorID
        });
    }

    //save函数
    function f_SaveDateItem(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        $.ajax({
            cache: false,
            type: "POST",
            url: "PointList.aspx/SaveDataItem",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    var selectedPoint = objPointGrid.getSelectedRow();
                    $.ligerDialog.success('数据保存成功');
                    dialog.close();
                    ItemGrid.set('url', "PointList.aspx?type=GetItems&selPointID=" + selectedPoint.ID);
                }
                else {
                    $.ligerDialog.warn('数据保存失败');
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
        url: "PointList.aspx/getPointName",
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
        url: "PointList.aspx/getItemName",
        data: "{'strValue':'" + strItemID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}