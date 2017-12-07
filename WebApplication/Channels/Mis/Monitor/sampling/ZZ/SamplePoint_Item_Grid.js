// Create by 苏成斌 2012.12.14  "采样-点位信息管理的监测项目列表"功能

var strItemId = "";
var ItemGrid;

//点位信息管理的监测项目管理功能
$(document).ready(function () {
    //分析方法grid
    ItemGrid = $("#ItemGrid").ligerGrid({
        columns: [
        //            { display: '监测点', name: 'Point_Name', width: 180, align: 'left', isSort: false, render: function (record) {
        //                return getPointName(record.TASK_POINT_ID);
        //            }
        //            },
            {display: '监测项目', name: 'ITEM_NAME', width: 140, align: 'left', isSort: false, render: function (record) {
                return getItemName(record.ITEM_ID);
            }
        }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: '96%',
        url: 'SamplePoint.aspx?Action=GetItems',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'set', text: '设置监测项目', click: SetData_Item, icon: 'add' }
//                { line: true },
//                { id: 'copy', text: '复制监测项目', click: copyData_Item, icon: 'page_copy' },
//                { id: 'past', text: '粘贴监测项目', click: pastData_Item, icon: 'page_paste' }
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