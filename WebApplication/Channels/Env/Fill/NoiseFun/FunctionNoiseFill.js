
// Create By 魏林 2013-06-26 "功能区噪声数据填报"功能
var quarter = null;
var objGrid = null;
var objSummaryGrid = null;
var gridJSON = null;
var url = "FunctionNoiseFill.aspx";
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {

    //构建查询表单
    $("#searchForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "年度", name: "ddlYear", newline: true, type: "select", group: "查询信息", groupicon: groupicon, comboboxName: "ddlYearBox", options: { valueFieldID: "hidYear", valueField: "value", textField: "value", resize: false, url: url + "?type=GetYear"} },
                      { display: "季度", name: "ddlSeason", newline: false, type: "select", comboboxName: "ddlSeasonBox", options: { valueFieldID: "hidSeason", valueField: "value", textField: "text", resize: false, url: url + "?type=GetSeason"} },
                      { display: "功能区", name: "ddlFunctionArea", newline: true, type: "select", comboboxName: "ddlFunctionAreaBox", options: { valueFieldID: "hidFunctionArea", valueField: "DICT_CODE", textField: "DICT_TEXT", resize: false, url: url + "?type=GetFunctionArea"} },
                      { display: "监测点", name: "ddlPoint", newline: false, type: "select", comboboxName: "ddlPointBox", options: { valueFieldID: "hidPoint", valueField: "ID", textField: "POINT_NAME", resize: false} }
                 ]
    });
    //构建流程表单
    $("#flowForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "年度", name: "ddlFYear", newline: true, type: "select", group: "查询信息", groupicon: groupicon, comboboxName: "ddlFYearBox", options: { valueFieldID: "hidFYear", valueField: "value", textField: "value", resize: false, url: url + "?type=GetYear"} },
                      { display: "季度", name: "ddlFSeason", newline: false, type: "select", comboboxName: "ddlFSeasonBox", options: { valueFieldID: "hidFSeason", valueField: "value", textField: "text", resize: false, url: url + "?type=GetSeason"} }
                 ]
    });

    //查询下拉框事件
    //$.ligerui.get("ddlMonthBox").bind("selected", function () { });
    //$.ligerui.get("ddlYearBox").bind("selected", function () { });
    //$.ligerui.get("ddlPointBox").bind("selected", function () { });
    //查询下拉框默认值
    $.ligerui.get("ddlYearBox").selectValue(new Date().getFullYear());
    quarter = parseInt((new Date().getMonth() + 1) / 3) + ((new Date().getMonth() + 1) % 3 > 0 ? 1 : 0);
    $.ligerui.get("ddlSeasonBox").selectValue(quarter);
    $.ligerui.get("ddlFunctionAreaBox").selectValue("1");

    $.ligerui.get("ddlFYearBox").selectValue(new Date().getFullYear());
    $.ligerui.get("ddlFSeasonBox").selectValue(quarter);

    //郑州采样任务办理
    if (getQueryString("strYear") != null && getQueryString("strMonth") != null) {
        $.ligerui.get("ddlYearBox").selectValue(getQueryString("strYear"));
        quarter = parseInt((getQueryString("strMonth")) / 3) + ((getQueryString("strMonth")) % 3 > 0 ? 1 : 0);
        $.ligerui.get("ddlSeasonBox").selectValue(quarter);

        $("#ddlYearBox").ligerGetComboBoxManager().setDisabled();
        $("#ddlSeasonBox").ligerGetComboBoxManager().setDisabled();
    }

    //创建填报时用于设置日期的表单
    $("#fillSetDateForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "日期", name: "ddlDay", newline: true, type: "select", group: "设置日期", groupicon: groupicon, comboboxName: "ddlDayBox", options: { valueFieldID: "hidDay", valueField: "VALUE", textField: "DAY", resize: false} }
                    ]
    });

    //构建统计表格
    objSummaryGrid = $("#gridSummary").ligerGrid({
        title: '功能区噪声数据统计',
        dataAction: 'server',
        usePager: false,
        alternatingRow: true,
        checkbox: true,
        enabledEdit: true,
        width: '100%',
        height: '100%',
        columns: [
                { display: 'ID', name: 'ID', align: 'left', width: 200, minWidth: 100, align: "center" },
                { display: '监测日期(月)', name: 'MONTH', align: 'left', width: 100, minWidth: 100, align: "center" },
                { display: '气像条件', name: 'WEATHER', align: 'left', width: 200, minWidth: 100, align: "center", editor: { type: "text"} },
                { display: '白昼均值Ld', name: 'LD', align: 'left', width: 100, minWidth: 100, align: "center", editor: { type: "text"} },
                { display: '夜间均值Ln', name: 'LN', align: 'left', width: 100, minWidth: 100, align: "center", editor: { type: "text"} },
                { display: '日均值Ldn', name: 'LDN', width: 100, minWidth: 100, align: "center", editor: { type: "text"} }
                ],
        toolbar: { items: [
                { text: '查询', click: showSearch, icon: 'search' },
                { text: '数据导入', click: ExcelImport, icon: 'database_wrench' },
                { text: '数据导出', click: ExcelExport, icon: 'excel' },
                { text: '审批签发', click: showFlow, icon: 'database' }
                ]
        },
        onAfterEdit: f_onAfterEdit,
        onBeforeSubmitEdit: function (v) {
            if (v.value != '' && v.column.columnname.indexOf("L") != -1) {
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

            //getFillData();
        }
    });
    //隐藏不需要显示的列
    objSummaryGrid.toggleCol("ID");

    //构建填报表格
    objGrid = $("#gridFill").ligerGrid({
        title: "功能区噪声数据填报",
        dataAction: 'server',
        usePager: false,
        alternatingRow: true,
        checkbox: true,
        width: '100%',
        height: "99.5%",
        enabledEdit: true,
        toolbar: { items: [
                { text: '监测日期', click: showSetDate, icon: 'calendar' }
                ]
        },
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

    getFillData(); //一开始获取一次数据
});

//获取监测点
function getPoint() {
    var year = $("#hidYear").val();
    var month = getMonthBySeason(parseInt($("#hidSeason").val()));
    var functionAreaCode = $("#hidFunctionArea").val();

    $.ligerui.get("ddlPointBox").clearContent();
    $("#ddlPointBox").val("");
    $("#hidPoint").val("");
    if (year != "") {
        $.ajax({
            url: url,
            data: "type=GetPoint&year=" + year + "&month=" + month+"&functionAreaCode="+functionAreaCode,
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

//根据季度返回月份
function getMonthBySeason(Season) {
    var months = null;
    months = Season * 3;
    months = (months - 2) + "," + (months - 1) + "," + months;

    return months;
}

//获取数据
function getFillData() {
    var pointId = $("#hidPoint").val();
    if (pointId != "") {
        $.ajax({
            url: url,
            data: "type=GetData&pointId=" + pointId,
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

                    //查询统计数据
                    objSummaryGrid.set("url", url + "?type=GetSummary&pointId=" + pointId);

                    //构建表格列
                    //固定的列
                    var columnsArr = [];

                    //添加所有动态的列
                    $.each(json.UnSureColumns, function (i, n) {
                        //相应可以编辑的列做相应的处理
                        if (n.columnId.indexOf("_ITEM") != -1 || n.columnId.indexOf("DAY") != -1 || n.columnId.indexOf("MEASURE_TIME") != -1) {
                            columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth) - 30, minWidth: 60, align: "center", editor: { type: "text"} });
                        }
                        else {
                            if (n.columnId.indexOf("POINT_ID") != -1) {
                                columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth) - 30, minWidth: 60, align: "center",
                                    render: function (item, i, v) {
                                        return getPointName(v);
                                    }
                                });
                            }
                            else {
                                columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth) - 30, minWidth: 60, align: "center" });
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
    else {
        objSummaryGrid.set("data", []);
        objGrid.set("data", []);
    }
}

function f_onAfterEdit(e) {
    var pointId = $("#hidPoint").val();
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
            data: "type=SaveData&id=" + ID + "&updateName=" + updateName + "&value=" + value + "&pointId=" + pointId,
            type: "post",
            dataType: "json",
            async: true,
            cache: false,
            success: function (json) {
                if (json.result == "success") {
                    if (updateName.toUpperCase().indexOf("LEQ") != -1) {
                        //查询统计数据
                        objSummaryGrid.set("url", url + "?type=GetSummary&pointId=" + pointId);
                    }
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
        $.ligerui.get("ddlSeasonBox").bind("selected", function () {
            getPoint();
        });
        $.ligerui.get("ddlFunctionAreaBox").bind("selected", function () {
            getPoint();
        });

        searchDialog = $.ligerDialog.open({
            target: $("#searchDiv"),
            width: 650, height: 200, top: 90, title: "查询",
            buttons: [
                  { text: '确定', onclick: function () { getFillData(); searchDialog.hide(); } },
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
    var quter = $("#hidFSeason").val();

    //根据年、月获取填报唯一值（格式：类型^年份^月份）
    $.ajax({
        url: url,
        data: "type=GetFillID&year=" + year + "&quter=" + quter,
        type: "post",
        dataType: "json",
        async: false,
        cache: false,
        success: function (obj) {
            if (obj.ID.length > 0) {
                if (obj.STATUS == "" || obj.STATUS == "0") {
                    $.ligerDialog.confirm('确定填报数据已经完整？', function (yes) {
                        if (yes) {
                            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true|pf_id=NF^' + year + '^' + quter + '|&WF_ID=PF';
                            top.f_overTab('填报提交', surl);
                        }
                    });
                }
                else {
                    $.ligerDialog.warn('【' + year + '】年【' + quter + '】季的填报数据已经提交审核，不能再提交!');
                }
            }
            else {
                $.ligerDialog.warn('找不到【' + year + '】年【' + quter + '】季的填报数据');
            }
        }
    });
}

//导入Excel
function ExcelImport() {
    $.ligerDialog.open({ title: "Excel导入界面", width: 500, height: 200, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            value = $("iframe")[0].contentWindow.Import("NoiseFun");
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); getFillData(); }
        }], url: "../FillImportDemo.aspx"
    });
}
//导出Excel
function ExcelExport() {
    if (objSummaryGrid.rows.length > 0) {
        var month = objSummaryGrid.rows[0]["MONTH"];
        var year = $("#hidYear").val();
        if (month != "" && year != "") {
            var vsURL = "../FillExportDemo.aspx?action=FillData&type=NoiseFun&month=" + month + "&year=" + year;
            var iWidth = screen.availWidth - 400;
            var iHeight = screen.availHeight - 300;
            var vsStyle = "menubar= no, toolbar= no, scrollbars=no, status=yes,location=no,left = 0,top= 0, resizable= yes, titlebar= yes, width= 1, height= 1";
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

//弹出设置日期窗口
var setDateDialog = null;
function showSetDate() {
    getDay();

    if (setDateDialog) {
        setDateDialog.show();
    } else {
        setDateDialog = $.ligerDialog.open({
            target: $("#fillSetDateDiv"),
            width: 450, height: 200, top: 90, title: "设置监测日期",
            buttons: [
                  { text: '确定', onclick: function () { setDate(); setDateDialog.hide(); } },
                  { text: '取消', onclick: function () { setDateDialog.hide(); } }
                  ]
        });
    }
}

//获取日期
function getDay() {
    var month = objSummaryGrid.rows[0]["MONTH"];
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

//设置日期
function setDate() {
    var day = $("#hidDay").val();
    var obj = null;
    for (var i = 0; i < gridJSON.Rows.length; i++) {
        var rowDom = objGrid.getRowObj(i);
        objGrid.getRow(i)["__status"] = "updated";
        //更新数据
        obj = [{ column: { columnname: gridJSON.UnSureColumns[6].columnId }, record: objGrid.getRow(i), rowindex: i, value: day}][0];
        f_onAfterEdit(obj);

        objGrid.updateCell(gridJSON.UnSureColumns[6].columnId, day, rowDom);
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