// Create by 潘德军 2012.11.21  "行业信息管理的监测项目列表"功能

var strItemId = "";

//行业信息管理的监测项目管理功能
$(document).ready(function () {
    var gridHeight = $(window).height() / 2;

    objSubGrid = $("#subgrid").ligerGrid({
        columns: [
            { display: '行业名称', name: 'INDUSTRY_NAME', width: 250, align: 'left', isSort: false, render: function (record) {
                return getIndustryName(record.INDUSTRY_ID);
            }
            },
            { display: '监测项目', name: 'ITEM_NAME', width: 200, align: 'left', isSort: false, render: function (record) {
                return getItemName(record.ITEM_ID);
            }
            }
        ], width: '100%', pageSizeOptions: [5, 8, 10], height: gridHeight, heightDiff: -10,
        url: 'IndustryList.aspx?Action=GetSubData',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        title:'默认监测项目',
        toolbar: { items: [
                { id: 'set', text: '设置监测项目', click: SetData_Item, icon: 'add' },
                { line: true },
                { id: 'copy', text: '复制监测项目', click: copyData_Item, icon: 'page_copy' },
                { line: true },
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

    var strCopyIndustryID = "";
    //复制监测项目
    function copyData_Item() {
        var selectedIndustry = objMainGrid.getSelectedRow();
        if (!selectedIndustry) {
            $.ligerDialog.warn('请先选择要复制的行业！');
            return;
        }

        strCopyIndustryID = selectedIndustry.ID;
    }

    //粘贴监测项目
    function pastData_Item() {
        var selectedIndustry = objMainGrid.getSelectedRow();
        if (!selectedIndustry) {
            $.ligerDialog.warn('请先选择要粘帖的行业！');
            return;
        }
        if (selectedIndustry.ID == strCopyIndustryID) {
            return;
        }

        $.ajax({
            cache: false,
            type: "POST",
            url: "IndustryList.aspx/CopyItem",
            data: "{'strCopyID':'" + strCopyIndustryID + "','strPastID':'" + selectedIndustry.ID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    objSubGrid.set('url', "IndustryList.aspx?type=GetSubData&selIndustryID=" + selectedIndustry.ID);
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
        var selectedIndustry = objMainGrid.getSelectedRow();
        if (!selectedIndustry) {
            $.ligerDialog.warn('请先选择行业！');
            return;
        }

        $.ligerDialog.open({ title: '设置监测项目', top: 0, width: 460, height: 370, buttons:
        [{ text: '确定', onclick: f_SaveDateItem },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'IndustryList_ItemEdit.aspx?IndustryID=' + selectedIndustry.ID 
        });
    }

    //save函数
    function f_SaveDateItem(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        $.ajax({
            cache: false,
            type: "POST",
            url: "IndustryList.aspx/SaveDataItem",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    var selectedIndustry = objMainGrid.getSelectedRow();
                    $.ligerDialog.success('数据保存成功');
                    dialog.close();
                    objSubGrid.set('url', "IndustryList.aspx?type=GetSubData&selIndustryID=" + selectedIndustry.ID);
                }
                else {
                    $.ligerDialog.warn('数据保存失败');
                }
            }
        });
    }
});

//获取行业信息
function getIndustryName(strIndustryID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "IndustryList.aspx/getIndustryName",
        data: "{'strValue':'" + strIndustryID + "'}",
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
        url: "IndustryList.aspx/getItemName",
        data: "{'strValue':'" + strItemID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}