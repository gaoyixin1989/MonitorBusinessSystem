
// Create By 魏林 2014-11-19 "河流数据填报补测"功能

var objGrid = null;
var gridJSON = null;
var strYear = "", strMonth = "", strPointID = "";
var url = "RiverFill.aspx";
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

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
    strYear = $.getUrlVar('strYear');
    strMonth = $.getUrlVar('strMonth');
    strPointID = $.getUrlVar('strPointID');

    //构建填报表格
    objGrid = $("#grid").ligerGrid({
        title: "河流数据填报补测数据",
        //dataAction: 'server',
        usePager: false,
        alternatingRow: true,
        checkbox: false,
        width: '100%',
        height: "99.5%",
        enabledEdit: false,
        toolbar: { items: [
                ]
        },
        onBeforeSubmitEdit: function (v) {
            if (v.value != '' && v.column.columnname.indexOf("ITEM") != -1) {
                if (isDouble(v.value))
                    return true;
                else
                    return false;
            }
            else {
                return true;
            }
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        }
    });
    
    getData(); //一开始获取一次数据
});

//获取数据
function getData() {

    var year = strYear;
    var month = strMonth;
    var pointId = strPointID;

    if (year != "" && month != "") {
        $.ajax({
            url: url,
            data: "type=GetBcData&year=" + year + "&month=" + month + "&pointId=" + pointId,
            type: "post",
            dataType: "json",
            async: true,
            cache: false,
            beforeSend: function () {
                $.ligerDialog.waitting('数据加载中,请稍候...');
            },
            complete: function () {
                $.ligerDialog.closeWaitting();
            },
            success: function (json) {
                if (parseInt(json.Total) > 0) {
                    gridJSON = json;

                    //构建表格列
                    //固定的列
                    var columnsArr = [];

                    //添加所有动态的列
                    $.each(json.UnSureColumns, function (i, n) {
                        //相应可以编辑的列做相应的处理
                        if (n.columnId.indexOf("_ITEM") != -1 || n.columnId.indexOf("DAY") != -1 || n.columnId.indexOf("OVERPROOF") != -1) {
                            columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth) + 20, minWidth: 60, align: "center", editor: { type: "text"} });
                        }
                        else {
                            if (n.columnId.indexOf("KPF") != -1) {
                                columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth), minWidth: 60, align: "center", editor: { type: "select", valueField: "DICT_CODE", textField: "DICT_TEXT", data: KPFJson() },
                                    render: function (item, i, v) {
                                        return getDicName(v, 'KPF');
                                    }
                                });
                            }
                            else {
                                if (n.columnId.indexOf("SECTION_ID") != -1) {
                                    columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth), minWidth: 60, align: "center",
                                        render: function (item, i, v) {
                                            return getSectionName(v);
                                        }
                                    });
                                }
                                else {
                                    if (n.columnId.indexOf("POINT_ID") != -1) {
                                        columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth), minWidth: 60, align: "center",
                                            render: function (item, i, v) {
                                                return getPointName(v);
                                            }
                                        });
                                    }
                                    else {
                                        columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth), minWidth: 60, align: "center" });
                                    }
                                }
                            }
                        }
                    });

                    objGrid.set("columns", columnsArr);
                    objGrid.set("data", json);

                    //隐藏不需要显示的列
                    objGrid.toggleCol("ID");

                    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
                }
                else {
                    objGrid.set("data", json);
                }
            }
        });
    }
}

function KPFJson() {
    var objReturnValue = null;
    $.ajax({
        url: url + "?type=GetDict&dictType=KPF",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: false,
        success: function (data) {
            objReturnValue = data;
        }
    });
    return objReturnValue;
}

//获取字典项信息
function getDicName(strDictCode, strDictType) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: url + "/getDictName",
        data: "{'strDictCode':'" + strDictCode + "','strDictType':'" + strDictType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取断面名称信息
function getSectionName(strV) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: url + "/getSectionName",
        data: "{'strV':'" + strV + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取测点名称信息
function getPointName(strV) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: url + "/getPointName",
        data: "{'strV':'" + strV + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}