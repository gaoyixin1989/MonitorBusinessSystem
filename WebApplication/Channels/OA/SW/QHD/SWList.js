// Create by 苏成斌 2013.03.15  "秦皇岛收文管理功能"功能

var strUrl = "SWList.aspx";
var isFisrt = "", gridName = "0";
var maingrid = null, maingrid1 = null, maingrid2 = null;

$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});

//未提交列表
$(document).ready(function () {
    $("#topmenu").ligerMenuBar({ items: [
                { id: 'add', text: '增加', click: createData, icon: 'add' },
                { id: 'modify', text: '修改', click: updateData, icon: 'modify' },
                { id: 'del', text: '删除', click: deleteData, icon: 'delete' }
            ]
    });

    $("#navtab1").ligerTab({ contextmenu: false, onBeforeSelectTabItem: function (tabid) {
    },
        //在点击选项卡之后触发   点击其他的选项卡后，刷新该选项卡，防止CSS样式被串
        onAfterSelectTabItem: function (tabid) {
            navtab = $("#navtab1").ligerGetTabManager();
            if (tabid == "home") {
                gridName = "0";
            }
            if (tabid == "tabitem1") {
                gridName = "1";
            }
            if (tabid == "tabitem2") {
                gridName = "2";
            }
            navtab.reload(navtab.getSelectedTabItemID());
            if (tabid != 'home') {
                isFisrt = false;
                GetIngDate();
                GetFinishedDate();
            }
            else {
                isFisrt = true;
            }
        }
    });

    maingrid = $("#maingrid").ligerGrid({
        columns: [
                { display: '收文编号', name: 'SW_CODE', align: 'left', width: 250, minWidth: 60 },
                { display: '来文单位', name: 'SW_FROM', align: 'left', width: 250, minWidth: 60 },
                { display: '收文标题', name: 'SW_TITLE', align: 'left', width: 350, minWidth: 60 },
                { display: '状态', name: 'SW_STATUS', width: 80, minWidth: 60, render: function (item) {
                    if (item.SW_STATUS == '0') {
                        return "<a style='color:Red'>未提交</a>";
                    }
                    return item.SW_STATUS;
                }
                }
                ],
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        url: strUrl + '?type=GetSWViewList&strSWStatus=0',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: true,
        whenRClickToSelect: true,
        onDblClickRow: function (data, rowindex, rowobj) {
            updateData();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    function GetIngDate() {
        maingrid1 = $("#maingrid1").ligerGrid({
            columns: [
                { display: '收文编号', name: 'SW_CODE', align: 'left', width: 250, minWidth: 60 },
                { display: '来文单位', name: 'SW_FROM', align: 'left', width: 250, minWidth: 60 },
                { display: '收文标题', name: 'SW_TITLE', align: 'left', width: 350, minWidth: 60 },
                { display: '状态', name: 'SW_STATUS', width: 80, minWidth: 60, render: function (item) {
                    return "<a style='color:Red'>流转中</a>";
                }
                }
                ],
            width: '100%',
            height: '100%',
            pageSizeOptions: [5, 10, 15, 20],
            pageSize: 10,
            url: strUrl + '?type=GetSWViewList&strSWStatus=99',
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            toolbar: { items: [
                { id: 'view', text: '查看', click: ViewData, icon: 'archives' }
                ]
            },
            rownumbers: true,
            checkbox: true,
            whenRClickToSelect: true,
            onDblClickRow: function (data, rowindex, rowobj) {
                ViewData();
            },
            onCheckRow: function (checked, rowdata, rowindex) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }
        });
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    }

    function GetFinishedDate() {
        maingrid2 = $("#maingrid2").ligerGrid({
            columns: [
                { display: '收文编号', name: 'SW_CODE', align: 'left', width: 250, minWidth: 60 },
                { display: '来文单位', name: 'SW_FROM', align: 'left', width: 250, minWidth: 60 },
                { display: '收文标题', name: 'SW_TITLE', align: 'left', width: 350, minWidth: 60 },
                { display: '状态', name: 'SW_STATUS', width: 80, minWidth: 60, render: function (item) {
                    if (item.SW_STATUS == '9') {
                        return "<a style='color:Red'>已办结(归档)</a>";
                    }
                    return item.SW_STATUS;
                }
                }
                ],
            width: '100%',
            height: '100%',
            pageSizeOptions: [5, 10, 15, 20],
            pageSize: 10,
            url: strUrl + '?type=GetSWViewList&strSWStatus=9',
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            toolbar: { items: [
                { id: 'view', text: '查看', click: ViewData, icon: 'archives' }
                ]
            },
            rownumbers: true,
            checkbox: true,
            whenRClickToSelect: true,
            onDblClickRow: function (data, rowindex, rowobj) {
                ViewData();
            },
            onCheckRow: function (checked, rowdata, rowindex) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }
        });
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    }

    function createData() {
        var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true&WF_ID=sw_001';
        top.f_overTab('新增收文登记单', surl);
    }
    function updateData() {
        var rowSelected = null, grid = null;
        if (gridName == "0") {
            rowSelected = maingrid.getSelectedRow()
            grid = maingrid;
        }
        if (gridName == "1") {
            rowSelected = maingrid1.getSelectedRow()
            grid = maingrid1;
        }
        if (gridName == "2") {
            rowSelected = maingrid2.getSelectedRow()
            grid = maingrid2;
        }
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行进行修改！');
        } else {
            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|SwID=' + rowSelected.ID + '|&WF_ID=sw_001';
            top.f_overTab('收文登记单修改', surl);
        }
    }
    function ViewData() {
        var rowSelected = null, grid = null;
        if (gridName == "0") {
            rowSelected = maingrid.getSelectedRow()
            grid = maingrid;
        }
        if (gridName == "1") {
            rowSelected = maingrid1.getSelectedRow()
            grid = maingrid1;
        }
        if (gridName == "2") {
            rowSelected = maingrid2.getSelectedRow()
            grid = maingrid2;
        }
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行进行查看！');
        } else {
            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|fwread=0|SwID=' + rowSelected.ID + '|&WF_ID=sw_001';
            top.f_overTab('收文查看', surl);
        }

    };
    function deleteData() {
        var rowSelected = null, grid = null;
        if (gridName == "0") {
            rowSelected = maingrid.getSelectedRow()
            grid = maingrid;
        }
        if (gridName == "1") {
            rowSelected = maingrid1.getSelectedRow()
            grid = maingrid1;
        }
        if (gridName == "2") {
            rowSelected = maingrid2.getSelectedRow()
            grid = maingrid2;
        }
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！'); return;
        }
        $.ligerDialog.confirm("确认删除该收文信息吗？", function (yes) {
            if (yes == true) {
                var strValue = rowSelected.ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: strUrl + "/deleteOneGridInfo",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            grid.loadData();
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