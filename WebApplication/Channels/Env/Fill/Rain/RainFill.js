// Create by 钟杰华 2013.02.21  "降水数据填报"功能
 //Modify by 刘静楠 2013.06.24 
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

$(document).ready(function () {

    //构建查询表单
    $("#searchForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "年度", name: "ddlYear", newline: true, type: "select", group: "查询信息", groupicon: groupicon, comboboxName: "ddlYearBox", options: { valueFieldID: "hidYear", valueField: "value", textField: "value", resize: false, url: url + "?action=GetYear"} },
                      { display: "月份", name: "ddlMonth", newline: false, type: "select", comboboxName: "ddlMonthBox", options: { valueFieldID: "hidMonth", valueField: "VALUE", textField: "MONTH", resize: false, data: monthJSON} },
                     // { display: "日期", name: "ddlDay", newline: true, type: "select", comboboxName: "ddlDayBox", options: { valueFieldID: "hidDay", valueField: "VALUE", textField: "DAY", resize: false} },
                      { display: "监测点", name: "ddlPoint", newline: true, type: "select", comboboxName: "ddlPointBox", options: { valueFieldID: "hidPoint", valueField: "ID", textField: "POINT_NAME", resize: false} }
                    ]
    });
    //构建流程表单
    $("#flowForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "年度", name: "ddlFYear", newline: true, type: "select", group: "查询信息", groupicon: groupicon, comboboxName: "ddlFYearBox", options: { valueFieldID: "hidFYear", valueField: "value", textField: "value", resize: false, url: url + "?action=GetYear"} },
                      { display: "月份", name: "ddlFMonth", newline: false, type: "select", comboboxName: "ddlFMonthBox", options: { valueFieldID: "hidFMonth", valueField: "VALUE", textField: "MONTH", resize: false, data: monthJSON} }
                 ]
    });

    //查询下拉框事件
    $.ligerui.get("ddlPointBox").bind("selected", function (value, text) { $("#hidPointIdToSave").val(value); });
    //查询下拉框默认值
    $.ligerui.get("ddlYearBox").selectValue(new Date().getFullYear());
    $.ligerui.get("ddlMonthBox").selectValue(new Date().getMonth() + 1);

    $.ligerui.get("ddlFYearBox").selectValue(new Date().getFullYear());
    $.ligerui.get("ddlFMonthBox").selectValue(new Date().getMonth() + 1);

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
        enabledEdit: true,
        toolbar: { items: [
                { text: '查询', click: showSearch, icon: 'search' },
//                { text: '添加', click: addRow, icon: 'add' },
//                { text: '删除', click: deleteRow, icon: 'delete' }, 
               // { text: '保存', click: saveData, icon: 'save' },
                { text: '数据导入', click: function () { showDataImport() }, icon: 'database_wrench' },
                { text: '数据导出', click: function () { showDataExport() }, icon: 'excel' },
                { text: '审批签发', click: showFlow, icon: 'database' }
                ]
        },
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

    //getData(); //一开始获取一次数据 
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
    //var day = $("#hidDay").val();
     
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

//弹出流程窗口
var flowDialog = null;
function showFlow() {
    if (flowDialog) {
        flowDialog.show();
    } else {
        flowDialog = $.ligerDialog.open({
            target: $("#flowDiv"),
            width: 650, height: 200, top: 90, title: "发送流程",
            buttons: [
                  { text: '确定', onclick: function () { flowData(); flowDialog.hide(); } },
                  { text: '取消', onclick: function () { flowDialog.hide(); } }
                  ]
        });
    }
}

function flowData() {
    var year = $("#hidFYear").val();
    var month = $("#hidFMonth").val();

    //根据年、月获取填报唯一值（格式：类型^年份^月份）
    $.ajax({
        url: url,
        data: "action=GetFillID&year=" + year + "&month=" + month,
        type: "post",
        dataType: "json",
        async: false,
        cache: false,
        success: function (obj) {
            if (obj.ID.length > 0) {
                if (obj.STATUS == "" || obj.STATUS == "0") {
                    $.ligerDialog.confirm('确定填报数据已经完整？', function (yes) {
                        if (yes) {
                            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true|pf_id=RA^' + year + '^' + month + '|&WF_ID=PF';
                            top.f_overTab('填报提交', surl);
                        }
                    });
                }
                else {
                    $.ligerDialog.warn('【' + year + '】年【' + month + '】月的填报数据已经提交审核，不能再提交!');
                }
            }
            else {
                $.ligerDialog.warn('找不到【' + year + '】年【' + month + '】月的填报数据');
            }
        }
    });
}

//删除行
function deleteRow() {
    var selectRow = objGrid.getSelectedRow();

    if ($(selectRow).size() == 0) {
        $.ligerDialog.warn('请选择需要删除的数据行', '系统提示');
        return;
    }

    $.ligerDialog.confirm('确定删除？', '系统提示', function (result) {
        if (result)
            objGrid.remove(selectRow);
    });
}

//添加行
function addRow() {
    objGrid.addRow();
    var firstRowJSON = gridJSON.Rows;
    var newRowObj = objGrid.getRowObj(firstRowJSON.length - 1);

    objGrid.updateCell("point_id", firstRowJSON[0].point_id, newRowObj);
    objGrid.updateCell("fill_id", firstRowJSON[0].fill_id, newRowObj);
    objGrid.updateCell("year", firstRowJSON[0].year, newRowObj);
    objGrid.updateCell("point_name", firstRowJSON[0].point_name, newRowObj);
    objGrid.updateCell("begin_month", "", newRowObj);
    objGrid.updateCell("begin_day", "", newRowObj);
    objGrid.updateCell("begin_hour", "", newRowObj);
    objGrid.updateCell("begin_minute", "", newRowObj);
    objGrid.updateCell("end_month", "", newRowObj);
    objGrid.updateCell("end_day", "", newRowObj);
    objGrid.updateCell("end_hour", "", newRowObj);
    objGrid.updateCell("end_minute", "", newRowObj);
    objGrid.updateCell("rain_type", "", newRowObj);
    objGrid.updateCell("precipitation", "", newRowObj);

    $.each(gridJSON.UnSureColumns, function (i, n) {
        if (n.columnName.indexOf("_id_") > -1) {
            objGrid.updateCell(n.columnName, eval("firstRowJSON[0]." + n.columnName), newRowObj);
        } else {
            objGrid.updateCell(n.columnName, "", newRowObj);
        }
    });
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
        }], url: "../FillImportDemo.aspx"
    });

}
////数据导出窗口
//function showDataExport() {
//    $("#cphData_btnExcelOut").click();
//}


//Excel导出方法
function showDataExport() {
    if (objGrid.rows.length > 0) {
        var month = $("#hidMonth").val();
        var year = $("#hidYear").val();
        if (month != "" && year != "") {
            var vsURL = "../FillExportDemo.aspx?action=FillData&type=Rain&month=" + month + "&year=" + year; 
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

//获取日期
//function getDay() {
//    var month = $("#hidMonth").val();
//    var year = $("#hidYear").val();

//    $.ligerui.get("ddlDayBox").clearContent();
//    $("#ddlDayBox").val("");
//    if (month != "") {
//        $.ajax({
//            url: url,
//            data: "action=GetDay&month=" + month + "&year=" + year,
//            type: "post",
//            dataType: "json",
//            async: false,
//            cache: false,
//            success: function (json) {
//                if (json.length > 0) {
//                    $.ligerui.get("ddlDayBox").setData(eval(json));
//                    $.ligerui.get("ddlDayBox").selectValue(json[0].VALUE)
//                }
//            }
//        });
//    }
//}