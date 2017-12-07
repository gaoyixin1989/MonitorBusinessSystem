//创建日期：2013-6-26
//创建人  ：李焕明

var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var strUrl = "FWList.aspx";
var strWf = "";
var isFisrt = "", gridName = "1";
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
//var StatusJson = [{ "value": "0", "Status": "未提交" }, { "value": "1", "Status": "办公室审核" }, { "value": "2", "Status": "分管领导审核" }, { "value": "3", "Status": "书记审核" }, { "value": "4", "Status": "归档发布"}];

var StatusJson = [{ 'value': '0,1,2,3,4,5,6,7,8,9', 'Status': '--全部--' }, { 'value': '0', 'Status': '未提交' }, { 'value': '1,2,3', 'Status': '审核中' }, { 'value': '4', 'Status': '已审核'}]; 

$(document).ready(function () {

    strWf = $.getUrlVar('WF_ID');

    //创建查询表单结构
    $("#searchForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
            { display: "状态", name: "ddlStatus", newline: true, type: "select", group: "查询信息", groupicon: groupicon, comboboxName: "ddlStatusBox", options: { valueFieldID: "hidStatus", valueField: "value", textField: "Status", initValue: '0,1,2,3,4,5,6,7,8,9', resize: false, data: StatusJson} },
            { display: "发文日期", name: "ddlDate", newline: false, type: "date"},
            { display: "发文编号", name: "ddlNumber", newline: true, type: "text" }
                    ]
    });

    $("#layout").ligerLayout({ height: "100%" });

    GetIngDate();

    function GetIngDate() {
        maingrid1 = $("#maingrid1").ligerGrid({
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
                   else if (item.FW_STATUS == '1') {
                        return "<a style='color:Red'>办公室审核</a>";
                    }
                    else if (item.FW_STATUS == '2') {
                        return "<a style='color:Red'>分管领导审核</a>";
                    }
                    else if (item.FW_STATUS == '3') {
                        return "<a style='color:Red'>书记审核</a>";
                    }
                    else if (item.FW_STATUS >= '4') {
                        return "<a style='color:Red'>发布、归档</a>";
                    }
//                    else {
//                        return "<a style='color:Red'>流转中</a>";
//                    }
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
                { id: 'view', text: '查询', click: SelectData, icon: 'search' },
                { id: 'view', text: '查看', click: ViewData, icon: 'archives' },
                { id: 'export', text: '打印', click: ExportData, icon: 'add' }
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
                { id: 'view', text: '查看', click: ViewData, icon: 'archives' },
                { id: 'export', text: '打印', click: ExportData, icon: 'add' }
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
            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=true2|fw_id=' + rowSelected.ID + '|&WF_ID=' + strWf;
            top.f_overTab('发文查看', surl);
        }

    };
    function ExportData() {
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
            $.ligerDialog.warn('打印之前请先选择发文');
            return;
        }
        $("#hidFwId").val(rowSelected.ID);
        $("#btnExport").click();
    }
})
//查询
var searchDialog = null;
function SelectData() {
    if (searchDialog) {
        searchDialog.show();
    } else {
        //弹出窗口
        searchDialog = $.ligerDialog.open({
            target: $("#searchDiv"),
            width: 650, height: 200, top: 90, title: "查询",
            buttons: [
                  { text: '确定', onclick: function () { getData(); searchDialog.hide(); } },
                  { text: '取消', onclick: function () { searchDialog.hide(); } }
                  ]
        });
    }

}

var url = "FWAllList.aspx";
//获取数据
function getData() {
    var Date = $("#ddlDate").val();
    var Status = $("#hidStatus").val();
    var Number = $("#ddlNumber").val();
    maingrid1.set("url", url + "?type=GetData&Status=" + Status + "&Date=" + Date + "&Number=" + Number);
}
