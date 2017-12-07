//企业缴费信息明细记录
//创建人：胡方扬 
//创建时间:2013-02-01
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var maingrid = null, vMonthItem = null, vQuarterItem = null;
var strCompanyId = "";
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
    strCompanyId = $.getUrlVar('strCompanyId');
    SEA_YEAR = $.getUrlVar('strYear');
    SEA_MONTH = $.getUrlVar('strMonth');
    SEA_QUARTER = $.getUrlVar('strQuarter');

    window['g1'] = maingrid = $("#maingrid").ligerGrid({
        columns: [
            { display: '合同号', name: 'CONTRACT_CODE', align: 'left', width: 160, minWidth: 60 },
            { display: '项目名称', name: 'PROJECT_NAME', width: 100, minWidth: 60 },
            { display: '企业名称', name: 'COMPANY_NAME', width: 120, minWidth: 60 },
            { display: '委托类型', name: 'CONTRACT_TYPE', width: 100, minWidth: 60 },
            { display: '费用', name: 'INCOME', width: 100, minWidth: 60, render: function (item) {
                return "￥" + item.INCOME;
            }
        },
            { display: '是否缴费', name: 'IF_PAY', width: 100, minWidth: 60 },
            { display: '签订日期', name: 'ASKING_DATE', width: 100, minWidth: 60 }
            ],
        title: '企业收费明细列表',
        width: '100%', height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        url: "CompanyFreeDetail.aspx?action=GetContractFreeDetialList&strCompanyId="+strCompanyId+"&strYear="+SEA_YEAR+"&strMonth="+SEA_MONTH+"&strQuarter="+SEA_QUARTER,
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
