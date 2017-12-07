var objGrid = null;
var objSummaryGrid = null;
var gridJSON = null;
var url = "RoadNoiseFill.aspx";
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
                      { display: "年度", name: "ddlYear", newline: true, type: "select", group: "查询信息", groupicon: groupicon, comboboxName: "ddlYearBox", options: { valueFieldID: "hidYear", valueField: "value", textField: "value", resize: false, url: url + "?type=GetYear"} }
                 ]
    });

    //构建导出月度表单
    $("#monthForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "月度", name: "ddlMonth", newline: true, type: "select", group: "导出月度", groupicon: groupicon, comboboxName: "ddlMonthBox", options: { valueFieldID: "hidMonth", valueField: "value", textField: "value", resize: false, url: url + "?type=GetMonth"} }
                 ]
    });
    //查询下拉框默认值
    $.ligerui.get("ddlYearBox").selectValue(new Date().getFullYear());
    $.ligerui.get("ddlMonthBox").selectValue(new Date().getMonth() + 1);
    //郑州采样任务办理
    if (getQueryString("strYear") != null && getQueryString("strMonth") != null) {
        $.ligerui.get("ddlYearBox").selectValue(getQueryString("strYear"));
        $.ligerui.get("ddlMonthBox").selectValue(parseInt(getQueryString("strMonth")));

        $("#ddlYearBox").ligerGetComboBoxManager().setDisabled();
    }

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
                { text: '计算汇总信息', click: operSummary, icon: 'calendar' },
                { text: '数据导入', click: ExcelImport, icon: 'database_wrench' },
                { text: '数据导出', click: ShowExport, icon: 'excel' }
                ]
        };
        bEdit = true;
    }

    //构建统计表格
    objSummaryGrid = $("#gridSummary").ligerGrid({
        title: '道路交通噪声数据统计',
        dataAction: 'server',
        usePager: false,
        alternatingRow: true,
        checkbox: true,
        enabledEdit: bEdit,
        width: '100%',
        height: '100%',
        columns: [
                { display: 'ID', name: 'ID', align: 'left', width: 200, minWidth: 100, align: "center" },
                { display: '年度', name: 'YEAR', align: 'left', width: 100, minWidth: 100, align: "center" },
                { display: '道路干线数目(条)', name: 'LINE_COUNT', width: 120, minWidth: 100, align: "center", editor: { type: "text"} },
                { display: '有效测点数(个)', name: 'VALID_COUNT', width: 100, minWidth: 100, align: "center", editor: { type: "text"} },
                { display: '起始月', name: 'BEGIN_MONTH', width: 100, minWidth: 100, align: "center", editor: { type: "text"} },
                { display: '起始日', name: 'BEGIN_DAY', width: 100, minWidth: 100, align: "center", editor: { type: "text"} },
                { display: '结束月', name: 'END_MONTH', width: 100, minWidth: 100, align: "center", editor: { type: "text"} },
                { display: '结束日', name: 'END_DAY', width: 100, minWidth: 100, align: "center", editor: { type: "text"} },
                { display: '城市机动车拥有量(万辆)', name: 'TRAFFIC_COUNT', width: 150, minWidth: 100, align: "center", editor: { type: "text"} }
                ],
        toolbar: objToolbar,
        onAfterEdit: f_onAfterEdit,
        onBeforeSubmitEdit: function (v) {
            if (v.value != '') {
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
        title: "道路交通噪声数据填报",
        dataAction: 'server',
        usePager: false,
        alternatingRow: true,
        checkbox: true,
        width: '100%',
        height: "99.5%",
        enabledEdit: bEdit,
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

    getFillData(); //一开始获取一次数据
});


//获取数据
function getFillData() {
    var year = $("#hidYear").val();
    if (year != "") {
        $.ajax({
            url: url,
            data: "type=GetData&year=" + year,
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
                    objSummaryGrid.set("url", url + "?type=GetSummary&year=" + year);

                    //构建表格列
                    //固定的列
                    var columnsArr = [];

                    //添加所有动态的列
                    $.each(json.UnSureColumns, function (i, n) {
                        //相应可以编辑的列做相应的处理
                        if (n.columnId.indexOf("_ITEM") != -1 || n.columnId.indexOf("DAY") != -1 || n.columnId.indexOf("MEASURE_TIME") != -1 || n.columnId.indexOf("HOUR") != -1 || n.columnId.indexOf("WEEK") != -1 || n.columnId.indexOf("MINUTE") != -1) {
                            columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth) - 30, minWidth: 60, align: "center", editor: { type: "text"} });
                        }
                        else {
                            if (n.columnId.indexOf("POINT_ID") != -1) {
                                columnsArr.push({ display: "道路名称", name: n.columnId, width: parseInt(n.columnWidth) - 30, minWidth: 60, align: "center",
                                    render: function (item, i, v) {
                                        return getRoadName(v);
                                    }
                                });

                                columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth), minWidth: 60, align: "center",
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
                    objSummaryGrid.set("data", json);
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

//计算汇总信息
function operSummary() {
    var year = $("#hidYear").val();
    $.ajax({
        url: url,
        data: "type=operSummary&year=" + year,
        type: "post",
        dataType: "json",
        async: true,
        cache: false,
        success: function (json) {
            if (json.result == "success") {
                //查询统计数据
                objSummaryGrid.set("url", url + "?type=GetSummary&year=" + year);
            } else {
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
            value = $("iframe")[0].contentWindow.Import("NoiseRoad");
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); getFillData(); }
        }], url: "../FillImport.aspx" 
    });
}
//导出Excel
var monthDialog = null;
function ShowExport() {
    if (monthDialog) {
        monthDialog.show();
    } else {

        monthDialog = $.ligerDialog.open({
            target: $("#ExportMonthDiv"),
            width: 400, height: 180, top: 90, title: "导出",
            buttons: [
                  { text: '确定', onclick: function () { ExcelExport(); monthDialog.hide(); } },
                  { text: '取消', onclick: function () { monthDialog.hide(); } }
                  ]
        });
    }
}
function ExcelExport() {
    var month = $("#hidMonth").val();
    var year = $("#hidYear").val();
    if (month != "" && year != "") {
        var vsURL = "../FilOutport.aspx?action=FillData&type=NoiseRoad&month=" + month + "&year=" + year;
        var iWidth = screen.availWidth - 400;
        var iHeight = screen.availHeight - 300;
        var vsStyle = "menubar= no, toolbar= no, scrollbars=no, status=yes,location=no,left = 0,top= 0, resizable= yes, titlebar= yes, width= 1, height= 1";
        window.open(vsURL, "newwindow", vsStyle);
    }
    else {
        $.ligerDialog.warn('查询条件的年度和月度不能为空');
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

//获取道路名称信息
function getRoadName(strV) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: url + "/getRoadName",
        data: "{'strV':'" + strV + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}