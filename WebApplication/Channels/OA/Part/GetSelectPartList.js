//获取选择物料列表
//创建人：胡方扬
//创建时间：2013-02-05
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
    window['g1'] = maingrid = $("#divPartInfo").ligerGrid({
        columns: [
            { display: '物料编码', name: 'PART_CODE', align: 'left', width: 160, minWidth: 60 },
            { display: '物料名称', name: 'PART_NAME', width: 100, minWidth: 60 },
            { display: '单位', name: 'UNIT', width: 100, minWidth: 60 },
            { display: '规格型号', name: 'MODELS', width: 100, minWidth: 60},
            { display: '用途', name: 'USEING', width: 100, minWidth: 60}
            ],
        title: '物料列表',
        width: '100%', height: '100%',
        pageSizeOptions: [5, 10, 20],
        url: "PartHandler.ashx?action=GetPartList&keshi="+$.getUrlVar('keshi'),
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        toolbar: { items: [
                 { id: 'search', text: '检索', click: showDetailSrh, icon: 'search' }
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
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }

    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
})
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
                     { display: "物料编码", name: "SEA_PART_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "物料名称", name: "SEA_PART_NAME", newline: true, type: "text" }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 300, height: 170, top: 10, title: "物料信息查询",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SEA_PART_CODE = encodeURI($("#SEA_PART_CODE").val());
        var SEA_PART_NAME = encodeURI($("#SEA_PART_NAME").val());

        maingrid.set('url', "PartHandler.ashx?action=GetPartList&strPartCode=" + SEA_PART_CODE + "&strPartName=" + SEA_PART_NAME);
    }

    function clearSearchDialogValue() {
        $("#SEA_PART_CODE").val("");
        $("#SEA_PART_NAME").val("");
    }
}
function f_select() {
    return maingrid.getSelectedRows();
}