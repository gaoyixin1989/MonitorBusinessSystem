var objGrid = null;
var gridJSON = null;
var url = "RainFill.aspx";
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var monthJSON = [
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
                      { display: "年度", name: "ddlYear", newline: true, type: "select", group: "查询信息", groupicon: groupicon, comboboxName: "ddlYearBox", options: { valueFieldID: "hidYear", valueField: "value", textField: "value", resize: false, url: url + "?action=GetYear"} },
                      { display: "月份", name: "ddlMonth", newline: false, type: "select", comboboxName: "ddlMonthBox", options: { valueFieldID: "hidMonth", valueField: "VALUE", textField: "MONTH", resize: false, data: monthJSON} },
                      {display: "监测点", name: "ddlPoint", newline: true, type: "select", comboboxName: "ddlPointBox", options: { valueFieldID: "hidPoint", valueField: "ID", textField: "POINT_NAME", resize: false} }
                    ]
    });
    //查询下拉框事件
    $.ligerui.get("ddlPointBox").bind("selected", function (value, text) { $("#hidPointIdToSave").val(value); });
    //查询下拉框默认值
    $.ligerui.get("ddlYearBox").selectValue(new Date().getFullYear());
    $.ligerui.get("ddlMonthBox").selectValue(new Date().getMonth() + 1);

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
                { text: '数据导入', click: function () { showDataImport() }, icon: 'database_wrench' },
                { text: '数据导出', click: function () { showDataExport() }, icon: 'excel' }
                ]
        };
        bEdit = true;
    }

    //构建填报表格
    objGrid = $("#grid").ligerGrid({
        title: "降水数据填报",
        dataAction: 'server',
        usePager: true,
        pageSize: 31,
        pageSizeOptions: [11, 21, 31, 41],
        alternatingRow: true,
        checkbox: true,
        width: '100%',
        height: "99.5%",
        enabledEdit: bEdit,
        toolbar: objToolbar,
        onAfterEdit: f_onAfterEdit,
        onBeforeSubmitEdit: function (v) {//校验值为数字
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
    getPoint($("#ddlYear").val()); //一开始获取一次监测点
  //  getData(); //一开始获取一次数据 
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
            data: "action=GetPoint&year=" + year + "&month=" + month,
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
            data: "action=GetData&year=" + year + "&month=" + month + "&pointId=" + pointId,
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
                    //动态的列 
                    $.each(json.UnSureColumns, function (i, n) {
                        if (n.columnId.indexOf("T_ENV_FILL_RAIN_ITEM") != -1 || n.columnId.indexOf("BEGIN_DAY") != -1 || n.columnId.indexOf("BEGIN_HOUR") != -1 || n.columnId.indexOf("BEGIN_MINUTE") != -1 || n.columnId.indexOf("END_DAY") != -1 || n.columnId.indexOf("END_HOUR") != -1 || n.columnId.indexOf("END_MINUTE") != -1 || n.columnId.indexOf("RAIN_TYPE") != -1 || n.columnId.indexOf("PRECIPITATION") != -1 || n.columnId.indexOf("JUDGE") != -1 || n.columnId.indexOf("OVERPROOF") != -1) {
                            columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth), minWidth: 60, align: "center", editor: { type: "text"} });
                        }
                        else {
                            if (n.columnId.indexOf("POINT_ID") != -1) {
                                columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth), minWidth: 60, align: "center",
                                    render: function (item, i, v) {
                                        return getPointName(v);
                                    }
                                });
                            } else {
                                columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth), minWidth: 60, align: "center" });
                            }
                        }
                    });
                    objGrid.set("columns", columnsArr);
                    objGrid.set("data", json);
                    //隐藏不需要显示的列
                    objGrid.toggleCol("ID");
                    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
                }
            }
        });
    }
}
function f_onAfterEdit(e) {
    //保存数据
    var data = ""
    var columnname = "";
    data = JSON.stringify(e.record);
    var value = e.value;
    var fill_id = e.record["ID"];
    columnname = e.column.columnname;
    if (e.record["__status"] != "nochanged") {
        if (data != "") {
            $.ajax({
                url: url,
                data: "action=SaveData&data=" + data + "&itemname=" + columnname + "&value=" + value + "&fill_id=" + fill_id,
                type: "post",
                dataType: "json",
                async: true,
                cache: false,
                success: function (json) {
                    if (json.result == "success") {
                        getData();
                    } else {
                    }
                }
            });
        }
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
//保存数据
function saveData() {
    var data = "";
    var pointId = $("#hidPointIdToSave").val();

    //构建填报数据字符串
    data = JSON.stringify(objGrid.getData());
}
//弹出查询窗口
var searchDialog = null;
function showSearch() {
    if (searchDialog) {
        searchDialog.show();
    } else {
        //年份下拉框与监测点下拉框联动
        $.ligerui.get("ddlYearBox").bind("selected", function () {
            getPoint();

        });
        //        月份下拉框与日期下拉框联动
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
//数据导入窗口
function showDataImport() {
    $.ligerDialog.open({ title: "Excel导入界面", width: 500, height: 200, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            value = $("iframe")[0].contentWindow.Import("Rain");
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); getData(); }
        }], url: "../FillImport.aspx"  
    });

}
//Excel导出方法
function showDataExport() {
    if (objGrid.rows.length > 0) {
        var month = $("#hidMonth").val();
        var year = $("#hidYear").val();
        if (month != "" && year != "") {
            var vsURL = "../FilOutport.aspx?action=FillData&type=Rain&month=" + month + "&year=" + year;
            var iWidth = screen.availWidth - 400;
            var iHeight = screen.availHeight - 300;
            var vsStyle = "menubar= no, toolbar= no, scrollbars=no, status=yes,location=no,left = 0,top= 0, resizable= yes, titlebar= yes, width= 10, height= 12";
            window.open(vsURL, "newwindow", vsStyle);
        }
        else {
            $.ligerDialog.warn('查询条件的年度和月度不能为空');
        }
    }
    else {
        $.ligerDialog.warn('查询的季度没数据');
    }
}