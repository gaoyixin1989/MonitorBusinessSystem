var objGrid = null;
var gridJSON = null;
var url = "DrinkUnder.aspx";
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
    //构建查询表单
    $("#searchForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "年度", name: "ddlYear", newline: true, type: "select", group: "查询信息", groupicon: groupicon, comboboxName: "ddlYearBox", options: { valueFieldID: "hidYear", valueField: "value", textField: "value", resize: false, url: url + "?type=GetYear"} },
                      { display: "月份", name: "ddlMonth", newline: false, type: "select", comboboxName: "ddlMonthBox", options: { valueFieldID: "hidMonth", valueField: "value", textField: "value", resize: false, url: url + "?type=GetMonth"} },
                      { display: "监测点", name: "ddlPoint", newline: true, type: "select", comboboxName: "ddlPointBox", options: { valueFieldID: "hidPoint", valueField: "ID", textField: "POINT_NAME", resize: false} }
                 ]
    });
    //查询下拉框事件
    $.ligerui.get("ddlMonthBox").bind("selected", function () { });
    $.ligerui.get("ddlYearBox").bind("selected", function () { });
    $.ligerui.get("ddlPointBox").bind("selected", function () { });
    //查询下拉框默认值
    $.ligerui.get("ddlYearBox").selectValue(new Date().getFullYear());
    $.ligerui.get("ddlMonthBox").selectValue(new Date().getMonth() + 1);

    //构建采样日期表单
    $("#samplingDayForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "采样日期", name: "ddlDay", newline: true, type: "select", group: "设置采样日期", groupicon: groupicon, comboboxName: "ddlDayBox", options: { valueFieldID: "hidDay", valueField: "VALUE", textField: "DAY", resize: false} }
                    ]
    });

    var objToolbar = null;
    var strEdit = "";
    var bEdit = null;
    strEdit = $.getUrlVar("edit");
    if (strEdit == "false") {
        objToolbar = { items: [
                { text: '查询', click: showSearch, icon: 'search' }
                ]
        };
        bEdit = false;
    }
    else {
        objToolbar = { items: [
                { text: '查询', click: showSearch, icon: 'search' },
                { text: '采样日期', click: showSamplingDay, icon: 'calendar' },
                { text: '数据导入', click: ExcelImport, icon: 'database_wrench' },
                { text: '数据导出', click: ExcelExport, icon: 'excel' }
                ]
        };
        bEdit = true;
    }

    //构建填报表格
    objGrid = $("#grid").ligerGrid({
        title: "地下水数据填报",
        dataAction: 'server',
        usePager: false,
        alternatingRow: true,
        checkbox: true,
        width: '100%',
        height: "99.5%",
        enabledEdit: bEdit,
        toolbar: objToolbar,
        onAfterEdit: f_onAfterEdit,
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
    getPoint(); //一开始获取一次监测点
    getData(); //一开始获取一次数据
});
//获取监测点
function getPoint() {
    var year = $("#hidYear").val();
    var month = $("#hidMonth").val();

    $.ligerui.get("ddlPointBox").clearContent();
    $("#ddlPointBox").val("");
    if (year != "") {
        $.ajax({
            url: url,
            data: "type=GetPoint&year=" + year + "&month=" + month,
            type: "post",
            dataType: "json",
            async: false,
            cache: false,
            success: function (json) {
                if (json.length > 0) {
                    $.ligerui.get("ddlPointBox").setData(eval(json));
                    $.ligerui.get("ddlPointBox").selectValue(json[0].ID)
                }
            }
        });
    }
}
//获取数据
function getData() {
    var year = $("#hidYear").val();
    var month = $("#hidMonth").val();
    var pointId = $("#hidPoint").val();
    if (year != "" && month != "") {
        $.ajax({
            url: url,
            data: "type=GetData&year=" + year + "&month=" + month + "&pointId=" + pointId,
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
                            columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth), minWidth: 60, align: "center", editor: { type: "text"} });
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
                    });
                    objGrid.set("columns", columnsArr);
                    objGrid.set("data", json);
                    //隐藏不需要显示的列
                    objGrid.toggleCol("ID");
                    objGrid.toggleCol("T_ENV_FILL_DRINK_UNDER@OVERPROOF@超标污染物");         
                    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
                }
                else {
                    objGrid.set("data", json);
                }
            }
        });
    }
}
function f_onAfterEdit(e) {
    //保存数据
    var ID = ""              //填报表ID
    var updateName = "";     //列名的信息
    var value = "";          //更新的值
    ID = e.record["ID"];
    updateName = e.column.columnname;
    value = e.value;
    if (e.record["__status"] != "nochanged") {

        $.ajax({
            url: url,
            data: "type=SaveData&id=" + ID + "&updateName=" + updateName + "&value=" + value,
            type: "post",
            dataType: "json",
            async: true,
            cache: false,
            success: function (json) {
                if (json.result == "success") {
                    //getData();
                    //objGrid.updateCell("FillID", json.fillid, e.rowindex);
                } else {
                }
            }
        });

    }
}
//弹出查询窗口
var searchDialog = null;
function showSearch() {
    if (searchDialog) {
        searchDialog.show();
    } else {
        //监测点下拉框联动
        $.ligerui.get("ddlYearBox").bind("selected", function () {
            getPoint();
        });
        $.ligerui.get("ddlMonthBox").bind("selected", function () {
            getPoint();
        });

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

//导入Excel
function ExcelImport() {
    $.ligerDialog.open({ title: "Excel导入界面", width: 500, height: 200, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            value = $("iframe")[0].contentWindow.Import("DrinkUnder");
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); getData(); }
        }], url: "../FillImport.aspx" 
    });
}
//导出Excel
function ExcelExport() {
    var month = $("#hidMonth").val();
    var year = $("#hidYear").val();
    if (month != "" && year != "") {
        var vsURL = "../FilOutport.aspx?action=FillData&type=DrinkUnder&month=" + month + "&year=" + year;
        var iWidth = screen.availWidth - 400;
        var iHeight = screen.availHeight - 300;
        var vsStyle = "menubar= no, toolbar= no, scrollbars=no, status=yes,location=no,left = 0,top= 0, resizable= yes, titlebar= yes, width= 1, height= 1";
        window.open(vsURL, "newwindow", vsStyle);
    }
    else {
        $.ligerDialog.warn('查询条件的年度和月度不能为空');
    }
}

//弹出采样日期窗口
var samplingDayDialog = null;
function showSamplingDay() {
    getDay();

    if (samplingDayDialog) {
        samplingDayDialog.show();
    } else {
        samplingDayDialog = $.ligerDialog.open({
            target: $("#samplingDayDiv"),
            width: 650, height: 200, top: 90, title: "查询",
            buttons: [
                  { text: '确定', onclick: function () { setSamplingDay(); samplingDayDialog.hide(); } },
                  { text: '取消', onclick: function () { samplingDayDialog.hide(); } }
                  ]
        });
    }
}

//获取日期
function getDay() {
    var month = $("#hidMonth").val();
    var year = $("#hidYear").val();

    $.ligerui.get("ddlDayBox").clearContent();
    $("#ddlDayBox").val("");
    if (month != "") {
        $.ajax({
            url: url,
            data: "type=GetDay&month=" + month + "&year=" + year,
            type: "post",
            dataType: "json",
            async: false,
            cache: false,
            success: function (json) {
                if (json.length > 0) {
                    $.ligerui.get("ddlDayBox").setData(eval(json));
                    $.ligerui.get("ddlDayBox").selectValue(json[0].VALUE)
                }
            }
        });
    }
}

//设置采样日期
function setSamplingDay() {
    var day = $("#hidDay").val();
    var obj = null;
    for (var i = 0; i < gridJSON.Rows.length; i++) {
        var rowDom = objGrid.getRowObj(i);
        objGrid.getRow(i)["__status"] = "updated";
        //更新数据
        obj = [{ column: { columnname: gridJSON.UnSureColumns[4].columnId }, record: objGrid.getRow(i), rowindex: i, value: day}][0];
        f_onAfterEdit(obj);

        objGrid.updateCell(gridJSON.UnSureColumns[4].columnId, day, rowDom);
    }
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
