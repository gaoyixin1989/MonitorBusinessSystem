// Modify by 刘静楠 2013.06.24  "硫酸盐化速率数据填报"功能
var objGrid = null;
var url = "Alkali.aspx";
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

    //创建查询表单结构
    $("#searchForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "年度", name: "ddlYear", newline: true, type: "select", group: "查询信息", groupicon: groupicon, comboboxName: "ddlYearBox", options: { valueFieldID: "hidYear", valueField: "value", textField: "value", resize: false, url: url + "?action=GetYear"} },
                      { display: "月份", name: "ddlMonth", newline: false, type: "select", comboboxName: "ddlMonthBox", options: { valueFieldID: "hidMonth", valueField: "VALUE", textField: "MONTH", resize: false, data: monthJSON} },
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
    $.ligerui.get("ddlMonthBox").bind("selected", function () { $("#hidMonthToExport").val($("#hidMonth").val()); });
    $.ligerui.get("ddlYearBox").bind("selected", function () { $("#hidYearToExport").val($("#hidYear").val()); });
    //下拉框默认值
    $.ligerui.get("ddlYearBox").selectValue(new Date().getFullYear());
    $.ligerui.get("ddlMonthBox").selectValue(new Date().getMonth() + 1);

    $.ligerui.get("ddlFYearBox").selectValue(new Date().getFullYear());
    $.ligerui.get("ddlFMonthBox").selectValue(new Date().getMonth() + 1);

    //构建数据导入表单
    $("#ddlImportConfig").ligerComboBox({
        resize: false,
        textField: "CONFIG_NAME",
        valueField: "ID",
        valueFieldID: "hidImportConfig",
        width: 300
    });

    //构建数据导出表单
    $("#ddlExportConfig").ligerComboBox({
        resize: false,
        textField: "CONFIG_NAME",
        valueField: "ID",
        valueFieldID: "hidExportConfig",
        width: 300
    });

    //构建统计表格
    objGrid = $("#grid").ligerGrid({
        title: '硫酸盐化速率数据填报',
        dataAction: 'server',
        usePager: true,
        pageSize: 31,
        pageSizeOptions: [11, 21, 31, 41],
        alternatingRow: true,
        checkbox: true,
        enabledEdit: true,
        width: '100%',
        height: '100%', 
        toolbar: { items: [
                { text: '查询', click: showSearch, icon: 'search' },
              //  { text: '保存', click: saveData, icon: 'save' },
//                { text: '新增行', click: addRow, icon: 'add' },
//                { text: '删除行', click: deleteRow, icon: 'delete' },
                { text: '数据导入', click: showDataImport, icon: 'database_wrench' },
                { text: '数据导出', click: showDataExport, icon: 'excel' },
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

    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
    getPoint($("#ddlYear").val()); //一开始获取一次监测点
    getData(); //一开始获取一次填报数据
});
//获取监测点
function getPoint() {
    var year = $("#hidYear").val(); //获取查询框年的值
    var month = $("#hidMonth").val();
    $.ligerui.get("ddlPointBox").clearContent();
    $("#ddlPointBox").val("");
    if (year != "") {
        $.ajax({
            url: url,
            data: "action=GetPoint&year=" + year + "&month=" + month, //发送给对象服务器，以对象或URL的形式
            type: "post", //请求方式
            dataType: "json", //希望服务器以“json”的形式返回值
            async: false, //true:异步请求；fasle:同步请求
            cache: false, //不需要缓存
            success: function (json) {//如果请求成功执行该函数
                if (json.length > 0) {
                    $.ligerui.get("ddlPointBox").setData(eval(json));
                    $.ligerui.get("ddlPointBox").selectValue(json[0].ID)
                }
            }
        });
    }
}
//查询统计数据
function getData(isAddRow) {
    var year = $("#hidYear").val();
    var month = $("#hidMonth").val();
    var pointID = $("#hidPoint").val(); //点位的ID
    //objGrid.set("url", url + "?action=GetData&year=" + year + "&month=" + month);
    if (year != "" && month != "") {
        $.ajax({
            url: url,
            data: "action=GetData&year=" + year + "&month=" + month + "&pointId=" + pointID,
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
                    var columnsArr = [];
                    //动态的列
                    $.each(json.UnSureColumns, function (i, n) {
                        //相应可以编辑的列做相应的处理 
                        if (n.columnId.indexOf("T_ENV_FILL_ALKALI_ITEM") != -1 || n.columnId.indexOf("BEGIN_DAY") != -1 || n.columnId.indexOf("BEGIN_HOUR") != -1 || n.columnId.indexOf("BEGIN_MINUTE") != -1|| n.columnId.indexOf("END_DAY") != -1 || n.columnId.indexOf("END_HOUR") != -1 || n.columnId.indexOf("END_MINUTE") != -1) {
                            columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth), minWidth: 100, align: "center", editor: { type: "text"} }); 
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
    //构建填报数据字符串
    var gridJSON = objGrid.getData();
    var data = JSON.stringify(gridJSON);
}

//新增行
function addRow() {
    saveTemp();

    objGrid.set("url", url + "?action=AddRow");
}

//删除行
function deleteRow() {
    var allData = objGrid.getData();
    var selectedData = objGrid.getSelectedRows();

    if (allData.length > 1 && selectedData.length > 0 && (allData.length - selectedData.length) > 0) {
        $.ligerDialog.confirm('确定删除？', function (result) {
            if (result) {
                saveTemp();

                var gridJSON = selectedData;
                var rowNum = "";
                $.each(gridJSON, function (i, n) {
                    rowNum += n.row_no + ",";
                });
                rowNum = rowNum.substring(0, rowNum.length - 1);

                objGrid.set("url", url + "?action=DeleteRow&rowNum=" + rowNum);
            }
        });
    }
}

//临时保存
function saveTemp() {
    var gridJSON = objGrid.getData();
    var data = JSON.stringify(gridJSON);

    $.ajax({
        url: url,
        data: "action=SaveTemp&data=" + data,
        type: "post",
        dataType: "json",
        async: false,
        cache: false,
        error: function () { $.ligerDialog.error("临时保存出错"); },
        success: function () { }
    });
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
        //监测点下拉框联动
        $.ligerui.get("ddlMonthBox").bind("selected", function () {
            getPoint();
        });
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
                            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true|pf_id=AL^' + year + '^' + month + '|&WF_ID=PF';
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

//数据导入窗口
function showDataImport() {
    $.ligerDialog.open({ title: "Excel导入界面", width: 500, height: 200, isHidden: false, buttons: 
        [
        { text:
        '确定', onclick: function (item, dialog) {
            value = $("iframe")[0].contentWindow.Import("Alkali");
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

//Excel导入方法
function showDataExport() {
    if (objGrid.rows.length > 0) {
        var month = $("#hidMonth").val();
        var year = $("#hidYear").val();
        if (month != "" && year != "") {
            var vsURL = "../FillExportDemo.aspx?action=FillData&type=Alkali&month=" + month + "&year=" + year;
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



//获取环境质量类别
function getEnvConfig(objName) {
    $.ajax({
        url: url,
        data: "action=GetEnvConfig",
        type: "post",
        dataType: "json",
        async: true,
        cache: false,
      //  error: function () { $.ligerDialog.error("获取环境质量类别错误"); },
        success: function (json) {
            $.ligerui.get(objName).setData(json);
        }
    });
}