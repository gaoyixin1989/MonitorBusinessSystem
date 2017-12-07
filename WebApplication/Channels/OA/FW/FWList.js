//Create By 苏成斌 发文列表
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var strUrl = "FWList.aspx";
var strWf = "";
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

$(document).ready(function () {
    $("#topmenu").ligerMenuBar({ items: [
                { id: 'add', text: '增加', click: AddData, icon: 'add' },
                { id: 'modify', text: '修改', click: ModifData, icon: 'modify' },
                { id: 'del', text: '删除', click: DeleteData, icon: 'delete' }
            ]
    });

    strWf = $.getUrlVar('WF_ID');

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
                { display: '发文编号', name: 'FWNO', width: 120, minWidth: 60 },
                { display: '发文标题', name: 'FW_TITLE', width: 360, minWidth: 60 },
                { display: '主办单位', name: 'ZB_DEPT', width: 160, minWidth: 60 },
                { display: '密级', name: 'MJ', width: 100, minWidth: 60 },
                { display: '发文日期', name: 'FW_DATE', width: 120, minWidth: 60 },
                { display: '状态', name: 'FW_STATUS', width: 80, minWidth: 60, render: function (item) {
                    if (item.FW_STATUS == '0') {
                        return "<a style='color:Red'>未提交</a>";
                    }
                    return item.FW_STATUS;
                }
                }
                ],
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        url: strUrl + '?type=GetFWViewList&strFWStatus=0',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: true,
        whenRClickToSelect: true,
        onDblClickRow: function (data, rowindex, rowobj) {
            ModifData();
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
                { display: '发文编号', name: 'FWNO', width: 120, minWidth: 60 },
                { display: '发文标题', name: 'FW_TITLE', width: 360, minWidth: 60 },
                { display: '主办单位', name: 'ZB_DEPT', width: 160, minWidth: 60 },
                { display: '密级', name: 'MJ', width: 100, minWidth: 60 },
                { display: '发文日期', name: 'FW_DATE', width: 120, minWidth: 60 },
                { display: '状态', name: 'FW_STATUS', width: 80, minWidth: 60, render: function (item) {
                    return "<a style='color:Red'>流转中</a>";
                }
                }
                ],
            width: '100%',
            height: '100%',
            pageSizeOptions: [5, 10, 15, 20],
            pageSize: 10,
            url: strUrl + '?type=GetFWViewList&strFWStatus=99',
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
        maingrid2= $("#maingrid2").ligerGrid({
            columns: [
                { display: '发文编号', name: 'FWNO', width: 120, minWidth: 60 },
                { display: '发文标题', name: 'FW_TITLE', width: 360, minWidth: 60 },
                { display: '主办单位', name: 'ZB_DEPT', width: 160, minWidth: 60 },
                { display: '密级', name: 'MJ', width: 100, minWidth: 60 },
                { display: '发文日期', name: 'FW_DATE', width: 120, minWidth: 60 },
                { display: '状态', name: 'FW_STATUS', width: 80, minWidth: 60, render: function (item) {
                    if (item.FW_STATUS == '9') {
                        return "<a style='color:Red'>已办结(归档)</a>";
                    }
                    return item.FW_STATUS;
                }
                }
                ],
            width: '100%',
            height: '100%',
            pageSizeOptions: [5, 10, 15, 20],
            pageSize: 10,
            url: strUrl + '?type=GetFWViewList&strFWStatus=9',
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

    function AddData(item) {
        var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true|&WF_ID=' + strWf;
        top.f_overTab('发文新增', surl);
    }

    function ModifData() {

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
            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|fw_id=' + rowSelected.ID + '|&WF_ID=' + strWf;
            top.f_overTab('发文修改', surl);
        }
    };

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
            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true|fw_id=' + rowSelected.ID + '|&WF_ID=' + strWf;
            top.f_overTab('发文查看', surl);
        }

    };
    function DeleteData() {
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
        $.ligerDialog.confirm('确定要删除该条记录？\r\n', function (result) {
            if (result == true) {
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "FWList.aspx/deleteFWInfo",
                    data: "{'strValue':'" + rowSelected.ID + "'}",
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
        })
    };
})