// Create by 潘德军 2012.11.08  "点位信息管理"功能

var objPointGrid = null;
var strPointId = "";
var strPointType = "";
var strCompanyID = "";

//点位信息管理功能
$(document).ready(function () {
    var topHeight = $(window).height() / 2;
    var gridHeight = $(window).height() / 2;

    $("#layout1").ligerLayout({ width: '98%', height: '100%', topHeight: topHeight });

    strCompanyID = request('CompanyID');
    strCompanyName = getCompanyNameEx(strCompanyID);

    //监测点位grid的菜单
    var objPointmenu = $.ligerMenu({ width: 120, items:
            [
            { text: '修改', click: updateData, icon: 'modify' },
            { line: true },
            { text: '删除', click: deleteData, icon: 'delete' }
            ]
    });

    objPointGrid = $("#PointGrid").ligerGrid({
        title: '点位信息: （' + strCompanyName + '）',
        dataAction: 'server',
        usePager: true,
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 8, 10],
        height: gridHeight,
        url: 'PointList.aspx?type=getPoint&CompanyID=' + strCompanyID,
        columns: [
                 { display: '监测点位', name: 'POINT_NAME', align: 'left', isSort: false, width: 200 },
                 { display: '监测类别', name: 'MONITOR_ID', align: 'left', width: 150, isSort: false, render: function (record) {
                     return getMonitorName(record.MONITOR_ID);
                 }
                 },
                 { display: '委托类型', name: 'POINT_TYPE', align: 'left', width: 150, isSort: false, render: function (record) {
                     return getDictNameEx(record.POINT_TYPE, 'Contract_Type');
                 }
                 },
        //                { display: '监测频次', name: 'FREQ', align: 'left', isSort: false, width: 100 ,render: function (record) {
        //                    return getDictNameEx(record.FREQ, 'Freq');
        //                 }
        //            },
                {display: '采样频次', name: 'SAMPLE_FREQ', align: 'left', isSort: false, width: 100, render: function (record) {
                    return getDictNameEx(record.SAMPLE_FREQ, 'POINT_SAMPLEFREQ');
                }
            },
                { display: '序号', name: 'NUM', align: 'left', isSort: false, width: 100 }
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

            strPointId = parm.data.ID;
            strPointType = parm.data.POINT_TYPE;
            objPointmenu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            strPointId = data.ID;
            strPointType = data.POINT_TYPE
            updateData();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            strPointId = rowdata.ID;
            strPointType = rowdata.POINT_TYPE;
            ItemGrid.set('url', "PointList.aspx?type=GetItems&selPointID=" + strPointId);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //增加数据
    function createData() {
        $.ligerDialog.open({ title: '点位信息增加', top: 0, width: 800, height: 550, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'PointEdit.aspx?CompanyID=' + strCompanyID + '&ContractID='
        });
    }
    //修改数据
    function updateData() {
        if (strPointId == "") {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: '点位信息编辑', top: 0, width: 800, height: 550, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'PointEdit.aspx?strid=' + strPointId + '&CompanyID=' + strCompanyID + '&ContractID='+strPointType
        });
    }

    //save函数
    function f_SaveDate(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        $.ajax({
            cache: false,
            type: "POST",
            url: "PointList.aspx/SaveData",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    $.ligerDialog.success('数据保存成功');
                    dialog.close();
                    objPointGrid.loadData();
                }
                else {
                    $.ligerDialog.warn('数据保存失败');
                }
            }
        });
    }

    //删除数据
    function deleteData() {
        if (objPointGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除点位信息 ' + objPointGrid.getSelectedRow().POINT_NAME + " 吗？", function (yes) {
            if (yes == true) {
                var strValue = objPointGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "PointList.aspx/deletePoint",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objPointGrid.loadData();
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
    //获取字典项信息
    function getDictNameEx(strDictCode, strDictType) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "PointList.aspx/getDictName",
            data: "{'strDictCode':'" + strDictCode + "','strDictType':'" + strDictType + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
    //获取监测类别信息
    function getMonitorName(strMonitorID) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "PointList.aspx/getMonitorName",
            data: "{'strValue':'" + strMonitorID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
});

//获取企业信息
function getCompanyNameEx(strMonitorID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "PointList.aspx/getCompanyName",
        data: "{'strValue':'" + strMonitorID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}