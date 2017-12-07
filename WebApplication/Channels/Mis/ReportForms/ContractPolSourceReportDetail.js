//企业缴费信息明细记录
//创建人：胡方扬 
//创建时间:2013-02-01
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var maingrid = null, vMonthItem = null, vQuarterItem = null;
var strTaskId = "";
var SEA_YEAR = "", SEA_MONTH = "", SEA_QUARTER = "", SEA_COMPANYNAME = "";

///-------------------------------------------------------------------------------------
///获取URL参数
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
///-------------------------------------------------------------------------------------

$(document).ready(function () {
    strTaskId = $.getUrlVar('strTask_id');


    window['g1'] = maingrid = $("#maingrid").ligerGrid({
        columns: [
            { display: '污染物', name: 'ITEM_NAME', align: 'left', width: 160, minWidth: 60 },
            { display: '监测点位', name: 'PIONT_NAME', width: 100, minWidth: 60 },
            { display: '样品号', name: 'SAMPLE_CODE', width: 120, minWidth: 60 },
            { display: '标准值', name: 'QJVALUE', width: 100, minWidth: 60 },
            { display: '监测值', name: 'ITEM_RESULT', width: 100, minWidth: 60 }
            ],
        title: '污染源超标明细列表',
        width: '100%', height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        url: "ContractPolSourceReport.aspx?action=GetPolSourceDetail&strTask_id=" + strTaskId,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
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
