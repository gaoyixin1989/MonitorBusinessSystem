//标准样品领用记录
//创建人：魏林
//创建时间:2013-09-17
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var maingrid = null, vUserList = "", strPartId = "";

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

strPartId = $.getUrlVar('strPartId');
$(document).ready(function () {
    function getUserName(strUserID) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../PartHandler.ashx?action=GetUserInfor",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.Total != '0') {
                    strValue = data.Rows[0].REAL_NAME;
                }
            }
        });
        return strValue;
    }

    window['g1'] = maingrid = $("#divPartInfo").ligerGrid({
        columns: [
            { display: '编号', name: 'SAMPLE_CODE', width: 100, minWidth: 60 },
            { display: '样品名称', name: 'SAMPLE_NAME', width: 100, minWidth: 60 },
            { display: '领用数量', name: 'USED_QUANTITY', width: 100, minWidth: 60 },
            { display: '单位', name: 'UNIT', width: 80, minWidth: 60 },
            { display: '领用时间', name: 'LASTIN_DATE', width: 100, minWidth: 60 },
            { display: '领用人', name: 'REAL_NAME', width: 100, minWidth: 60 },
            { display: '领用原因', name: 'REASON', width: 200, minWidth: 60, render: function (item) {
                if (item.REASON.length >= 20) {
                    return "<a title=" + item.REASON + ">" + item.REASON.substring(0, 20) + "</a>";
                }
                return item.REASON;
            }
            }
            ],
        title: '样品领用历史明细',
        width: '100%', height: '99%',
        pageSizeOptions: [5, 10, 20],
        url: "SampleCollarList.aspx?type=GetSampleCollarInfor&strPartId=" + strPartId,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        rownumbers: false,
        toolbar: { items: [
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }//,
                //{ line: true },
                //{ id: 'excel', text: '导出', click: exportExcel, icon: 'excel' }
                ]
        },
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }

    });
    
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll


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
                     { display: "编号", name: "SEA_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "样品名称", name: "SEA_NAME", newline: false, type: "text" },
                    { display: "领用开始日期", name: "SEA_BEGINDATE", newline: true, type: "date" },
                    { display: "领用截止日期", name: "SEA_ENDDATE", newline: false, type: "date" },
                    { display: "领用人", name: "SEA_USERNAME", newline: true, type: "text" }
                    ]
            });
            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 660, height: 200, top: 90, title: "样品领用查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SEA_CODE = encodeURI($("#SEA_CODE").val());
            var SEA_NAME = encodeURI($("#SEA_NAME").val());
            var SEA_ENDDATE = encodeURI($("#SEA_ENDDATE").val());
            var SEA_BEGINDATE = encodeURI($("#SEA_BEGINDATE").val());
            var SEA_USERNAME = encodeURI($("#SEA_USERNAME").val());
            if (SEA_BEGINDATE != "" && SEA_ENDDATE == "") {
                $.ligerDialog.warn('请选择截止日期！'); return;
            }
            if (SEA_ENDDATE != "" && SEA_BEGINDATE == "") {
                $.ligerDialog.warn('请选择开始日期！'); return;
            }
            maingrid.set('url', "SampleCollarList.aspx?type=GetSampleCollarInfor&strPartCode=" + SEA_CODE + "&strPartName=" + SEA_NAME + "&strBeginDate=" + SEA_BEGINDATE + "&strEndDate=" + SEA_ENDDATE + "&strReal_Name=" + SEA_USERNAME);
            clearSearchDialogValue();
            detailWinSrh.hide();
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_CODE").val("");
        $("#SEA_NAME").val("");
        $("#SEA_ENDDATE").val("");
        $("#SEA_BEGINDATE").val("");
        $("#SEA_USERNAME").val("");
    }

    function exportExcel() {
        $.ligerDialog.confirm('您确定要导出出库记录明细表吗？', function (yes) {
            if (yes == true) {
                var strPartCollarId = "";
                var spit = "";
                var rowSelected = maingrid.getSelectedRows();
                if (rowSelected.length > 1) {
                    for (var i = 0; i < rowSelected.length; i++) {
                        strPartCollarId += spit + rowSelected[i].ID;
                        spit = ",";
                    }
                    $("#hidExportData").val(strPartCollarId);
                    $("#btnExport").click();
                } else {//如果没有选择则导出全部的
                    $("#btnExport").click();
                }
                //maingrid.loadData();
            }
        });
    }
})
