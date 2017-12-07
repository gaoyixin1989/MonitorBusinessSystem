/// 站务管理--物料管理
/// 创建时间：2013-01-31
/// 创建人：胡方扬
/// 修改人：魏林 2013-09-12
var maingrid = null, strPartName = "", strPartId = "";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

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
    maingrid = $("#maingrid").ligerGrid({
        columns: [
            { display: '物料编码', name: 'PART_CODE', align: 'left', width: 160, minWidth: 60 },
            { display: '物料名称', name: 'PART_NAME', width: 100, minWidth: 60 },
            { display: '单位', name: 'UNIT', width: 100, minWidth: 60 },
            { display: '规格型号', name: 'MODELS', width: 100, minWidth: 60 },
            { display: '库存量', name: 'INVENTORY', width: 100, minWidth: 60, render: function (item) {
                if (parseInt(item.INVENTORY) <= parseInt(item.ALARM)) {
                    return "<a title='库存不足' style='color:Red'>" + item.INVENTORY + "</a>"
                }
                return item.INVENTORY
            }
            },
            { display: '数量', name: 'Qty', width: 100, minWidth: 60 },
            { display: '报警值', name: 'ALARM', width: 100, minWidth: 60 },
            { display: '用途', name: 'USEING', width: 100, minWidth: 60 }
            ],
        title: '物料列表',
        width: '100%', height: '100%',
        pageSizeOptions: [25, 30, 35],
        url: "PartHandler.ashx?action=GetPartList&type=" + $.getUrlVar('type'),
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 25,
        toolbar: { items: [
                 { id: 'subin', text: '入库', click: InStoreData, icon: 'page_go' },
                 { line: true },
                 { id: 'subout', text: '领用', click: AcceptData, icon: 'TRUE' },
                 { line: true },
                 { id: 'subset', text: '报警阀值', click: SetWarnData, icon: 'database_wrench' },
                 { line: true },
                 { id: 'viewsubin', text: '入库明细', click: ViewInStoreData, icon: 'archives' },
                 { line: true },
                 { id: 'view', text: '领用明细', click: ViewData, icon: 'archives' },
                 { line: true },
                 { id: 'search', text: '检索', click: showDetailSrh, icon: 'search'}//,
            //{ line: true },
            //{ id: 'search', text: '查询', click: showSearch, icon: 'search' }
                ]
        },
        alternatingRow: false,
        checkbox: true,
        rownumbers: true,
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            AcceptData();
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }

    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
    if ($.getUrlVar('type')) {
        maingrid.toggleCol("PART_CODE");
    }

    function ViewData() {
        var rowSelected = null;
        rowSelected = maingrid.getSelectedRow();
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行进行操作！');
        } else {
            var tabid = "tabidDetial"
            var surl = '../Channels/OA/Part/PartCollarList.aspx?strPartId=' + rowSelected.ID;
            top.f_addTab(tabid + rowSelected.ID, '物料领用明细查看', surl);
        }
    }

    function ViewInStoreData() {
        var rowSelected = null;
        rowSelected = maingrid.getSelectedRow();
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行进行操作！');
        } else {
            var tabid = "tabidSubinDetial"
            var surl = '../Channels/OA/Part/PartOutList.aspx?strPartId=' + rowSelected.ID;
            top.f_addTab(tabid + rowSelected.ID, '物料入库明细查看', surl);
        }
    }

    function AcceptData() {
        var rowSelected = null;
        rowSelected = maingrid.getSelectedRow();
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行进行操作！');
        } else {
            if (parseInt(rowSelected.INVENTORY == "" ? "0" : rowSelected.INVENTORY) <= 0) {
                $.ligerDialog.warn('库存不足,无法执行领用操作！'); return;
            } else {
                $.ligerDialog.open({ title: '物料领用操作', top: 40, width: 700, height: 420, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'PartOutEdit.aspx?strPartId=' + encodeURI(rowSelected.ID) + '&strNeedQuanity=' + rowSelected.INVENTORY
                });
            }
        }

    };

    function InStoreData() {
        var rowSelected = null;
        rowSelected = maingrid.getSelectedRow();
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行物料进行入库！');
        } else {
            $.ligerDialog.open({ title: '采购物料入库', top: 40, width: 700, height: 420, isHidden: false, buttons:
            [{ text: '确定', onclick: f_InStoreData },
             { text: '取消', onclick: function (item, dialog) { dialog.close(); }
             }], url: 'PartAcceptEdit.aspx?strPartId=' + encodeURI(rowSelected.ID) + '&strPartCode=' + encodeURI(rowSelected.PART_CODE) + '&strPartName=' + encodeURI(rowSelected.PART_NAME) + '&strNeedQuanity=' + encodeURI(rowSelected.INVENTORY)
            });
        }

    }

    function SetWarnData() {
        var rowSelected = null;
        rowSelected = maingrid.getSelectedRow();
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行进行操作！');
        } else {
            $.ligerDialog.open({ title: '物料库存报警阀值设置', top: 40, width: 400, height: 200, buttons:
        [{ text: '确定', onclick: f_SaveWarnDate },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'PartWarnEdit.aspx?strPartId=' + encodeURI(rowSelected.ID) + '&strWarnValue=' + rowSelected.ALARM
            });
        }
    };

    function f_SaveDate(item, dialog) {
        var fnSave = dialog.frame.GetBaseInfoStr || dialog.frame.window.GetBaseInfoStr;
        var strReques = fnSave();
        if (strReques != "") {
            SaveDataGrid(strReques);
            dialog.close();
        }

    }

    function f_InStoreData(item, dialog) {
        var fnSave = dialog.frame.GetInStoreInfo || dialog.frame.window.GetInStoreInfo;
        var strReques = fnSave();
        if (strReques != "") {
            SavePartAccepted(strReques);
            dialog.close();
        }
    }
    //保存物料入库数据
    function SavePartAccepted(strReques) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "PartHandler.ashx?action=SavePartAccepted" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    maingrid.set('url', 'PartHandler.ashx?action=GetPartList&type='+$.getUrlVar('type'))
                    $.ligerDialog.success('物料入库成功！');
                }
                else {
                    $.ligerDialog.warn('物料入库失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }

    function f_SaveWarnDate(item, dialog) {
        var fnSave = dialog.frame.GetWarnInfoStr || dialog.frame.window.GetWarnInfoStr;
        var strReques = fnSave();
        if (strReques != "") {
            SaveWarnDataGrid(strReques);
            dialog.close();
        }

    }

    function SaveDataGrid(strReques) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "PartHandler.ashx?action=SavePartCollarDate" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    maingrid.set('url', 'PartHandler.ashx?action=GetPartList&type=' + $.getUrlVar('type'))
                    $.ligerDialog.success('数据保存成功！');
                    return;
                }
                else {
                    $.ligerDialog.warn('数据保存失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！'); return
            }
        });
    }

    function SaveWarnDataGrid(strReques) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "PartHandler.ashx?action=UpdatePartInfor" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    maingrid.set('url', 'PartHandler.ashx?action=GetPartList&type='+$.getUrlVar('type'))
                    $.ligerDialog.success('数据保存成功！');
                    return;
                }
                else {
                    $.ligerDialog.warn('数据保存失败！'); return;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！'); return
            }
        });
    }
    var Searches = null;
    //物料查询
    function showSearch() {
        if (Searches) {
            Searches.show();
        }
        else {
            //创建表单结构
            var searchform = $("#SearchForm");
            searchform.ligerForm({
                inputWidth: 160, labelWidth: 90, space: 40,
                fields: [
                         { display: "物料名称", name: "SEA_NAME", newline: false, type: "text", group: "基本信息", groupicon: groupicon },
                         { display: "开始时间", name: "StartTime", newline: true, type: "date", initValue: "2014-03-18" },
                         { display: "结束时间", name: "EndTime", newline: false, type: "date" }
                    ]
            });
            $("#StartTime").val(currentTime()); //赋默认值
            $("#EndTime").val(currentTime()); //赋默认值
            Searches = $.ligerDialog.open({
                target: $("#Search"),
                width: 600, height: 200, top: 90, title: "物料查询明细",
                buttons: [
                  { text: '确定', onclick: function () { SearchInfo(); ClearInfo(); Searches.hide(); } },
                  { text: '返回', onclick: function () { ClearInfo(); Searches.hide(); } }
                  ]
            });
        }
    }
    function SearchInfo() {
        var SEA_NAME = encodeURI($("#SEA_NAME").val()); //名称
        var StartTime = $("#StartTime").val(); //开始时间
        var EndTime = $("#EndTime").val(); //结束时间
        maingrid.set('url', "PartHandler.ashx?action=GetPartIOList&SEA_NAME=" + SEA_NAME + "&StartTime=" + StartTime + "&EndTime=" + EndTime); //获取出入库明细
    }
    //JS 获取当前时间
    function currentTime() {
        var d = new Date(), str = '';
        str += d.getFullYear() + '-';
        str += d.getMonth() + 1 + '-';
        str += d.getDate();
        return str;
    }
    function ClearInfo() {
        $("#SEA_NAME").val("");
        $("#StartTime").val(currentTime);
        $("#EndTime").val(currentTime); //结束时间 
    }
    //设置grid 的弹出查询对话框
    var detailWinSrh = null;
    function showDetailSrh() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构

            var mainform = $("#SrhForm");
            mainform.ligerForm({
                inputWidth: 160, labelWidth: 90, space: 40,
                fields: [
                     { display: "物料编码", name: "SEA_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "物料名称", name: "SEA_NAME", newline: false, type: "text" }
                    ]
            });
            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 600, height: 200, top: 90, title: "待验收物料查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SEA_CODE = encodeURI($("#SEA_CODE").val());
            var SEA_NAME = encodeURI($("#SEA_NAME").val());
            maingrid.set('url', "PartHandler.ashx?action=GetPartList&strPartName=" + SEA_NAME + "&strPartCode=" + SEA_CODE + "&type=" + $.getUrlVar('type'));
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_CODE").val("");
        $("#SEA_NAME").val("");
    }
})
