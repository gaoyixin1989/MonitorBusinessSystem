
/// 环境质量附件上传下载管理
/// 创建人：魏林
/// 创建时间：2014-08-04

var objGrid = null;
var strUrl = "EnvFillAttInfo.aspx";
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

var monthJSON = [
    { "VALUE": " ", "MONTH": "--" },
    { "VALUE": "1", "MONTH": "1" },
    { "VALUE": "2", "MONTH": "2" },
    { "VALUE": "3", "MONTH": "3" },
    { "VALUE": "4", "MONTH": "4" },
    { "VALUE": "5", "MONTH": "5" },
    { "VALUE": "6", "MONTH": "6" },
    { "VALUE": "7", "MONTH": "7" },
    { "VALUE": "8", "MONTH": "8" },
    { "VALUE": "9", "MONTH": "9" },
    { "VALUE": "10", "MONTH": "10" },
    { "VALUE": "11", "MONTH": "11" },
    { "VALUE": "12", "MONTH": "12" }
];

var SeasonJson = [{ "VALUE": " ", "Season": "--" }, { "VALUE": "1", "Season": "1" }, { "VALUE": "2", "Season": "2" }, { "VALUE": "3", "Season": "3" }, { "VALUE": "4", "Season": "4"}];

$(document).ready(function () {
    var objToolbar = null;
    if ($.getUrlVar('type') == 'view') {
        objToolbar = { items: [
                { id: 'srh', text: '查询', click: itemclickOfToolbar, icon: 'search' },
                { line: true },
                { id: 'all', text: '全部显示', click: itemclickOfToolbar, icon: 'search' }
                ]
        };
    }
    else {
        objToolbar = { items: [
                { id: 'add', text: '增加', click: itemclickOfToolbar, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: itemclickOfToolbar, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: itemclickOfToolbar, icon: 'delete' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclickOfToolbar, icon: 'search' },
                { line: true },
                { id: 'all', text: '全部显示', click: itemclickOfToolbar, icon: 'search' }
                ]
        };
    }

    objGrid = $("#grid").ligerGrid({
        columns: [
        { display: '类别', name: 'ENVTYPE', width: 150, align: 'left', render: function (record) {
            return getDictName(record.ENVTYPE, 'EnvTypes_ATT');
        }
        },
        { display: '年', name: 'YEAR', width: 100, align: 'left' },
        { display: '季度', name: 'SEASON', width: 100, align: 'left' },
        { display: '月', name: 'MONTH', width: 100, align: 'left' },
        { display: '日', name: 'DAY', width: 100, align: 'left' },
        { display: '说明', name: 'REMARK', width: 260, align: 'left' },
        { display: '附件上传', name: 'upLoad', width: 100, align: 'left', render: function (record) {
            return "<a href=\"javascript:upLoadFile('" + record.ID + "')\">附件上传</a> ";
        }
        },
        { display: '附件下载', name: 'REMARK1', width: 200, align: 'left', render: function (record) {
            return "<a href=\"javascript:downLoadFile('" + record.ID + "')\">" + record.REMARK1 + "</a> ";
        }
        }
        ],
        width: '100%',
        pageSizeOptions: [20, 30, 40, 50],
        height: '100%',
        url: strUrl + '?Action=GetList',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 30,
        title: '环境质量附件管理',
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: objToolbar,
        onContextmenu: function (parm, e) {

        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetail({
                ID: data.ID,
                ENVTYPE: data.ENVTYPE,
                YEAR: data.YEAR,
                SEASON: data.SEASON,
                MONTH: data.MONTH,
                DAY: data.DAY,
                REMARK: data.REMARK
            }, false);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );

    if ($.getUrlVar('type') == 'view') {
        objGrid.toggleCol("upLoad");
    }
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});

//grid 的toolbar click事件
function itemclickOfToolbar(item) {
    switch (item.id) {
        case 'add':
            showDetail(null, true);
            break;
        case 'modify':
            var data = objGrid.getSelectedRow();
            if (!data) { $.ligerDialog.warn('请先选择要编辑的记录！'); ; return }
            
            showDetail({
                ID: data.ID,
                ENVTYPE: data.ENVTYPE,
                YEAR: data.YEAR,
                SEASON: data.SEASON,
                MONTH: data.MONTH,
                DAY: data.DAY,
                REMARK: data.REMARK
            }, false);

            break;
        case 'del':
            var rows = objGrid.getCheckedRows();
            var strDelID = "";
            $(rows).each(function () {
                strDelID += (strDelID.length > 0 ? "," : "") + this.ID;
            });

            if (strDelID.length == 0) {
                $.ligerDialog.warn('请先选择要删除的信息！');
            }
            else {
                jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                    if (confirm)
                        delDate(strDelID);
                });
            }

            break;
        case 'srh':
            showDetailSrh();
            break;
        case 'all':
            objGrid.set('url', strUrl + "?Action=GetList");
            break;
        default:
            break;
    }
}

//删除函数
function delDate(id) {
    $.ajax({
        cache: false,
        type: "POST",
        url: strUrl+"/deleteData",
        data: "{'strDelID':'" + id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                objGrid.loadData();
            }
            else {
                $.ligerDialog.warn('删除失败！');
            }
        }
    });
}

var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        $("#searchForm").ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "类别", name: "ddlSrhType", newline: true, type: "select", group: "查询条件", groupicon: groupicon, comboboxName: "ddlSrhTypeBox", options: { valueFieldID: "hidSrhType", valueField: "DICT_CODE", textField: "DICT_TEXT", resize: false, url: strUrl + "?action=GetDict&dictType=EnvTypes_ATT"} },
                      { display: "年", name: "ddlSrhYear", newline: false, type: "select", comboboxName: "ddlSrhYearBox", options: { valueFieldID: "hidSrhYear", valueField: "value", textField: "value", resize: false, url: strUrl + "?action=GetYear"} },
                      { display: "季度", name: "ddlSrhSeason", newline: true, type: "select", comboboxName: "ddlSrhSeasonBox", options: { valueFieldID: "hidSrhSeason", valueField: "VALUE", textField: "Season", resize: false, data: SeasonJson} },
                      { display: "月", name: "ddlSrhMonth", newline: false, type: "select", comboboxName: "ddlSrhMonthBox", options: { valueFieldID: "hidSrhMonth", valueField: "VALUE", textField: "MONTH", resize: false, data: monthJSON} },
                      { display: "日", name: "SrhDay", newline: true, type: "text" }
                    ]
        });

        //下拉框默认值 
        $.ligerui.get("ddlSrhTypeBox").selectValue('EnvRiver');
        $.ligerui.get("ddlSrhYearBox").selectValue(new Date().getFullYear());
        $.ligerui.get("ddlSrhSeasonBox").selectValue(" "); //季度默认值
        $.ligerui.get("ddlSrhMonthBox").selectValue(" "); 

        detailWinSrh = $.ligerDialog.open({
            target: $("#searchDiv"),
            width: 650, height: 300, top: 90, title: "查询物料",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhType = $("#hidSrhType").val();
        var SrhYear = $("#hidSrhYear").val();
        var SrhSeason = $("#hidSrhSeason").val();
        var SrhMonth = $("#hidSrhMonth").val();
        var SrhDay = $("#SrhDay").val();
        //alert(SrhType + '|' + SrhYear + '|' + SrhSeason + '|' + SrhMonth + '|' + SrhDay);
        objGrid.set('url', strUrl + "?Action=GetList&SrhType=" + SrhType + "&SrhYear=" + SrhYear + "&SrhSeason=" + SrhSeason + "&SrhMonth=" + SrhMonth + "&SrhDay=" + SrhDay);
    }
}

//maingrid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $.ligerui.get("ddlSrhSeasonBox").selectValue(" ");
    $.ligerui.get("ddlSrhMonthBox").selectValue(" ");
    $("#SrhDay").val("");
}

var detailWin = null, curentData = null, currentIsAddNew;
function showDetail(data, isAddNew) {
    curentData = data;
    currentIsAddNew = isAddNew;
    if (detailWin) {
        detailWin.show();
    }
    else {
        //创建表单结构
        $("#editForm").ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { name: "ID", type: "hidden" },
                      { display: "类别", name: "ddlType", newline: true, type: "select", group: "基本信息", groupicon: groupicon, comboboxName: "ddlTypeBox", options: { valueFieldID: "hidType", valueField: "DICT_CODE", textField: "DICT_TEXT", resize: false, url: strUrl + "?action=GetDict&dictType=EnvTypes_ATT"} },
                      { display: "年", name: "ddlYear", newline: false, type: "select", comboboxName: "ddlYearBox", options: { valueFieldID: "hidYear", valueField: "value", textField: "value", resize: false, url: strUrl + "?action=GetYear"} },
                      { display: "季度", name: "ddlSeason", newline: true, type: "select", comboboxName: "ddlSeasonBox", options: { valueFieldID: "hidSeason", valueField: "VALUE", textField: "Season", resize: false, data: SeasonJson} },
                      { display: "月", name: "ddlMonth", newline: false, type: "select", comboboxName: "ddlMonthBox", options: { valueFieldID: "hidMonth", valueField: "VALUE", textField: "MONTH", resize: false, data: monthJSON} },
                      { display: "日", name: "Day", newline: true, type: "text" },
                      { display: "说明", name: "Remark", width: 470, newline: true, type: "text" }
                    ]
        });
        //下拉框默认值 
        $.ligerui.get("ddlTypeBox").selectValue('EnvRiver');
        $.ligerui.get("ddlYearBox").selectValue(new Date().getFullYear());
        $.ligerui.get("ddlSeasonBox").selectValue(" "); //季度默认值
        $.ligerui.get("ddlMonthBox").selectValue(" "); 

        detailWin = $.ligerDialog.open({
            target: $("#editDiv"),
            width: 650, height: 300, top: 90, title: "附件信息",
            buttons: [
                  { text: '确定', onclick: function () { save(); } },
                  { text: '取消', onclick: function () { clearDialogValue(); detailWin.hide(); } }
                  ]
        });
    }

    if (curentData) {
        $("#ID").val(curentData.ID);
        $.ligerui.get("ddlTypeBox").selectValue(curentData.ENVTYPE);
        $.ligerui.get("ddlYearBox").selectValue(curentData.YEAR);
        $.ligerui.get("ddlSeasonBox").selectValue(curentData.SEASON);
        $.ligerui.get("ddlMonthBox").selectValue(curentData.MONTH);
        $("#Day").val(curentData.DAY);
        $("#Remark").val(curentData.REMARK);
    }

    function save() {
        var strData = "{";
        strData += "'strID':'" + $("#ID").val() + "',";
        strData += "'strEnvType':'" + $("#hidType").val() + "',";
        strData += "'strYear':'" + $("#hidYear").val() + "',";
        strData += "'strSeason':'" + $("#hidSeason").val() + "',";
        strData += "'strMonth':'" + $("#hidMonth").val() + "',";
        strData += "'strDay':'" + $("#Day").val() + "',";
        strData += "'strRemark':'" + $("#Remark").val() + "'";
        strData += "}";

        $.ajax({
            cache: false,
            type: "POST",
            url: strUrl+"/EditData",
            data: strData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    detailWin.hidden();
                    objGrid.loadData();
                    clearDialogValue();
                }
                else if (data.d == "0") {
                    $.ligerDialog.warn('保存失败！');
                }
                else {
                    $.ligerDialog.warn(data.d);
                }
            }
        });
    }
}

//监测类别grid 弹出编辑框 清空
function clearDialogValue() {
    $("#ID").val("");
    $.ligerui.get("ddlSeasonBox").selectValue(" ");
    $.ligerui.get("ddlMonthBox").selectValue(" ");
    $("#Day").val("");
    $("#Remark").val("");
}
///附件上传
function upLoadFile(id) {
    $.ligerDialog.open({ title: '附件上传', width: 500, height: 270,
        buttons: [
                {
                    text:
                    '上传', onclick: function (item, dialog) {
                        $("iframe")[0].contentWindow.upLoadFile();
                        objGrid.loadData();
                    }
                },
                {
                    text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
                }], url: '../../OA/ATT/AttFileUpload.aspx?filetype=EnvData&id=' + id
    });
}
///附件下载
function downLoadFile(id) {
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=EnvData&id=' + id
    });
}