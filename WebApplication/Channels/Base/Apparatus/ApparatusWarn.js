// Create by 潘德军 2013.7.15  "仪器检定报警"功能
//Modify By 魏林 2013.7.24 添加“仪器报废报警”功能

var objApparatusWarnGrid = null;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var Title = null;
var Column = null;
var Action = null;
//仪器信息管理
$(document).ready(function () {
    Action = getQueryString("action");
    if (Action == "ident") {
        Title = "仪器检定报警";
        Column = [
                { display: '仪器编号', name: 'APPARATUS_CODE', align: 'left', width: 60 },
                { display: '仪器名称', name: 'NAME', width: 120 },
                { display: '规格型号', name: 'MODEL', width: 120 },

                { display: '检定方式', name: 'TEST_MODE', width: 60 },
                { display: '最近检定/校准时间', name: 'BEGIN_TIME', width: 120, render: function (items) {
                    return items.BEGIN_TIME.replace("0:00:00", "").replace("00:00:00", "");
                }
                },
                { display: '到期检定/校准时间', name: 'END_TIME', width: 120, render: function (items) {
                    if (items.is_END_TIME == '1') {
                        return "<a style='color:Red'>" + items.END_TIME.replace("0:00:00", "").replace("00:00:00", "") + "</a>";
                    } else {
                        return items.END_TIME.replace("0:00:00", "").replace("00:00:00", "");
                    }
                }
                },
                { display: '核查方式', name: 'VERIFICATION_WAY', width: 60 },
                { display: '最近核查时间', name: 'VERIFICATION_BEGIN_TIME', width: 120, render: function (items) {
                    return items.VERIFICATION_BEGIN_TIME.replace("0:00:00", "").replace("00:00:00", "");
                }
                },
                { display: '到期核查时间', name: 'VERIFICATION_END_TIME', width: 120, render: function (items) {
                    if (items.is_VERIFICATION_END_TIME == '1') {
                        return "<a style='color:Red'>" + items.VERIFICATION_END_TIME.replace("0:00:00", "").replace("00:00:00", "") + "</a>";
                    } else {
                        return items.VERIFICATION_END_TIME.replace("0:00:00", "").replace("00:00:00", "");
                    }
                }
                },
                { display: '保管人', name: 'KEEPER', width: 80 },
                { display: '放置地点', name: 'POSITION', width: 120 }
                ];
    }
    else {
        Title = "仪器报废报警";
        Column = [
                { display: '仪器编号', name: 'APPARATUS_CODE', align: 'left', width: 60 },
                { display: '仪器名称', name: 'NAME', width: 120 },
                { display: '规格型号', name: 'MODEL', width: 120 },

                { display: '检定方式', name: 'TEST_MODE', width: 60 },
                { display: '购买时间', name: 'BUY_TIME', width: 120, render: function (items) {
                    return items.BUY_TIME.replace("0:00:00", "").replace("00:00:00", "");
                }
                },
                { display: '报废时间', name: 'SCRAP_TIME', width: 120, render: function (items) {
                    if (items.is_SCRAP_TIME == '1') {
                        return "<a style='color:Red'>" + items.SCRAP_TIME.replace("0:00:00", "").replace("00:00:00", "") + "</a>";
                    } else {
                        return items.SCRAP_TIME.replace("0:00:00", "").replace("00:00:00", "");
                    }
                }
                },
                { display: '保管人', name: 'KEEPER', width: 80 },
                { display: '放置地点', name: 'POSITION', width: 120 }
                ];
    }
    objApparatusWarnGrid = $("#apparatusWarnGrid").ligerGrid({
        title: Title,
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "APPARATUS_CODE",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '99%',
        url: 'ApparatusWarn.aspx?type=getApparatusWarn&action=' + Action,
        columns: Column,
        toolbar: { items: [
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll


    //设置grid 的弹出查询对话框
    var detailWinSrh = null;
    function showDetailSrh() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构
            var divmainform = $("#Seachdiv");
            divmainform.ligerForm({
                inputWidth: 170, labelWidth: 90, space: 40,
                fields: [
                        { display: "仪器编号", name: "SEA_APPARATUS_CODE", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                        { display: "仪器名称", name: "SEA_NAME", newline: false, type: "text" },
                        { display: "仪器型号", name: "SEA_MODEL", newline: true, type: "text" },
                        { display: "供应商名称", name: "SEA_FITTINGS_PROVIDER", newline: false, type: "text" }
                    ]
            });
            detailWinSrh = $.ligerDialog.open({
                target: $("#Seachdetail"),
                width: 600, height: 200, top: 90, title: "仪器设备查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }
    }
    function search() {
        var SEA_APPARATUS_CODE = encodeURI($("#SEA_APPARATUS_CODE").val());
        var SEA_NAME = encodeURI($("#SEA_NAME").val());
        var SEA_MODEL = encodeURI($("#SEA_MODEL").val());
        var SEA_FITTINGS_PROVIDER = encodeURI($("#SEA_FITTINGS_PROVIDER").val());

        objApparatusInfoGrid.set('url', "ApparatusInfo.aspx?type=getApparatusInfo&srhApparatus_Code=" + SEA_APPARATUS_CODE + "&srh_Name=" + SEA_NAME + "&srh_Model=" + SEA_MODEL + "&srhProvider=" + SEA_FITTINGS_PROVIDER);
    }
    function clearSearchDialogValue() {
        $("#SEA_APPARATUS_CODE").val("");
        $("#SEA_NAME").val("");
        $("#SEA_MODEL").val("");
        $("#SEA_FITTINGS_PROVIDER").val("");
    }
});

//获取Url参数 Create By 魏林
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}