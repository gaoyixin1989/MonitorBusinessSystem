﻿// Create by 黄进军 2014.07.04  "秦皇岛收文阅办"功能

var objOneGrid = null;
var strUrl = "NewSWRead.aspx";

//未提交列表
$(document).ready(function () {
    objOneGrid = $("#oneGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [10, 15, 20],
        height: '100%',
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                { display: '收文编号', name: 'SW_CODE', align: 'left', width: 250, minWidth: 60 },
                { display: '来文单位', name: 'SW_FROM', align: 'left', width: 250, minWidth: 60 },
                { display: '收文标题', name: 'SW_TITLE', align: 'left', width: 350, minWidth: 60 },
                { display: '状态', name: 'IS_OK', width: 80, minWidth: 60, render: function (item) {
                    if (item.IS_OK == '0') {
                        return "<a style='color:Red'>未阅</a>";
                    } else {
                        return "<a style='color:Red'>已阅</a>";
                    }
                }
                }
                ],
        toolbar: { items: [
                { text: '查看', click: ReadData, icon: 'add' }
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
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    function ReadData() {

        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一行进行查看！');
        } else {
            $.ajax({
                cache: false,
                type: "POST",
                url: strUrl + "/updateStatus",
                data: "{'strID':'" + objOneGrid.getSelectedRow().ID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                    }
                    else {
                        $.ligerDialog.warn('阅办失败');
                    }
                }
            });
            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|fwread=1|SwID=' + objOneGrid.getSelectedRow().ID + '|&WF_ID=sw_001';
            top.f_overTab('收文查看', surl);
            
        }
    }
});