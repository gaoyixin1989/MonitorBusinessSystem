//物料入库记录
//创建人：潘德军
//创建时间:2013-02-01

var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
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
            url: "PartHandler.ashx?action=GetUserInfor",
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
            { display: '物料名称', name: 'PART_NAME', width: 100, minWidth: 60 },
            { display: '物料编码', name: 'PART_CODE', width: 100, minWidth: 60 },
            { display: '数量', name: 'NEED_QUANTITY', width: 100, minWidth: 60 },
            { display: '规格型号', name: 'MODELS', width: 100, minWidth: 60 }, //黄进军添加20141028
            { display: '单位', name: 'UNIT', width: 80, minWidth: 60 },
            { display: '单价', name: 'PRICE', width: 80, minWidth: 60 },
            { display: '金额', name: 'AMOUNT', width: 80, minWidth: 60 },
            { display: '收货日期', name: 'RECIVEPART_DATE', width: 100, minWidth: 60 },
            { display: '供应商', name: 'ENTERPRISE_NAME', width: 100, minWidth: 60 },
            { display: '验收人', name: 'REAL_NAME', width: 100, minWidth: 60 }
            ],
        title: '物料入库历史明细',
        width: '100%', height: '100%',
        pageSizeOptions: [5, 10, 20],
        url: "PartHandler.ashx?action=GetInStoreData&strPartId=" + strPartId,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        rownumbers: true,
        toolbar: { items: [
            { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' },
            { line: true },
                {id: 'menumodify', text: '修改', click: updateData, icon: 'modify'}//,
            //  { line: true },
            //    { id: 'excel', text: '导出', click: exportExcel, icon: 'excel' }
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
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll

    var detailWinSrh = null;
    //物料入库时间段查询
    function showDetailSrh() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构
            var searchform = $("#SrhForm");
            searchform.ligerForm({
                inputWidth: 160, labelWidth: 90, space: 40,
                fields: [
                         { display: "开始时间", name: "StartTime", newline: true, type: "date" },
                         { display: "结束时间", name: "EndTime", newline: false, type: "date" },
                         { display: "入库人", name: "SEA_USERNAME", newline: true, type: "text" }
                    ]
            });
            Searches = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 600, height: 200, top: 90, title: "物料入库时间段查询",
                buttons: [
                  { text: '确定', onclick: function () { SearchInfo(); ClearInfo(); Searches.hide(); } },
                  { text: '返回', onclick: function () { ClearInfo(); Searches.hide(); } }
                  ]
            });
        }
    }
    function SearchInfo() {
        var StartTime = encodeURI($("#StartTime").val()); //开始时间
        var EndTime = encodeURI($("#EndTime").val()); //结束时间
        var SEA_USERNAME = encodeURI($("#SEA_USERNAME").val()); //入库人
        maingrid.set('url', "PartHandler.ashx?action=GetPartInputTimeList&strReal_Name="+SEA_USERNAME+"&strPartId=" + strPartId + "&StartTime=" + StartTime + "&EndTime=" + EndTime); //获取入库时间段明细
    }

    function ClearInfo() {
        $("#StartTime").val(""); //开始时间
        $("#EndTime").val(""); //结束时间 
        $("#SEA_USERNAME").val(""); //入库人
    }

    function updateData() {
        var rowSelected = null;

        rowSelected = maingrid.getSelectedRow();

        var strPartCode = "";
        var strPartName = "";

        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "PartOutList.aspx?type=loadData_PART_CODE&strPartId=" + rowSelected.PART_ID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strPartCode = data;
            }
        });

        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "PartOutList.aspx?type=loadData_PART_NAME&strPartId=" + rowSelected.PART_ID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strPartName = data;
            }
        });

        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行进行操作！');
        } else {
            $.ligerDialog.open({ title: '物料入库明细修改', top: 40, width: 700, height: 420,
                buttons: [{ text: '确定', onclick: f_SaveUpdateData }, { text: '返回', onclick: function (item, dialog) { dialog.close(); } }],
                url: 'PartAcceptEdit.aspx?strId=' + encodeURI(rowSelected.ID) + '&strPartId=' + encodeURI(rowSelected.PART_ID) + '&strPartCode=' + encodeURI(strPartCode) + '&strPartName=' + encodeURI(strPartName) + '&strNeedQuanity=1'
            });
        }
    }


    function f_SaveUpdateData(item, dialog) {
        var fnSave = dialog.frame.GetInStoreInfoEx || dialog.frame.window.GetInStoreInfoEx;
        var strReques = fnSave();
        if (strReques != "") {
            SaveUpdateData(strReques);
            dialog.close();
        }
    }

    function SaveUpdateData(strReques) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "PartHandler.ashx?action=SavePartAccepted" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    maingrid.set('url', 'PartHandler.ashx?action=GetInStoreData&strPartId=' + strPartId)
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
});