
// Create By 魏林 2013-06-18 "河流数据填报"功能

var objGrid = null;
var gridJSON = null;
var url = "RiverFill.aspx";
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strYear = null, strMonth = null;

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
    //构建查询表单
    $("#searchForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "年度", name: "ddlYear", newline: true, type: "select", group: "查询信息", groupicon: groupicon, comboboxName: "ddlYearBox", options: { valueFieldID: "hidYear", valueField: "value", textField: "value", resize: false, url: url + "?type=GetYear"} },
                      { display: "月份", name: "ddlMonth", newline: false, type: "select", comboboxName: "ddlMonthBox", options: { valueFieldID: "hidMonth", valueField: "value", textField: "value", resize: false, url: url + "?type=GetMonth"} },
                      { display: "监测点", name: "ddlPoint", newline: true, type: "select", comboboxName: "ddlPointBox", options: { valueFieldID: "hidPoint", valueField: "ID", textField: "SECTION_NAME", resize: false} }
                 ]
    });

    //构建流程表单
    $("#flowForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "年度", name: "ddlFYear", newline: true, type: "select", group: "查询信息", groupicon: groupicon, comboboxName: "ddlFYearBox", options: { valueFieldID: "hidFYear", valueField: "value", textField: "value", resize: false, url: url + "?type=GetYear"} },
                      { display: "月份", name: "ddlFMonth", newline: false, type: "select", comboboxName: "ddlFMonthBox", options: { valueFieldID: "hidFMonth", valueField: "value", textField: "value", resize: false, url: url + "?type=GetMonth"} }
                 ]
    });

    //查询下拉框事件
    $.ligerui.get("ddlMonthBox").bind("selected", function () { });
    $.ligerui.get("ddlYearBox").bind("selected", function () { });
    $.ligerui.get("ddlPointBox").bind("selected", function () { });
    //查询下拉框默认值
    if (strYear == null)
        $.ligerui.get("ddlYearBox").selectValue(new Date().getFullYear());
    else
        $.ligerui.get("ddlYearBox").selectValue(strYear);
    if (strMonth == null)
        $.ligerui.get("ddlMonthBox").selectValue(new Date().getMonth() + 1);
    else
        $.ligerui.get("ddlMonthBox").selectValue(parseInt(strMonth));

    $.ligerui.get("ddlFYearBox").selectValue(new Date().getFullYear());
    $.ligerui.get("ddlFMonthBox").selectValue(new Date().getMonth() + 1);

    //构建采样日期表单
    $("#samplingDayForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "采样日期", name: "ddlDay", newline: true, type: "select", group: "设置采样日期", groupicon: groupicon, comboboxName: "ddlDayBox", options: { valueFieldID: "hidDay", valueField: "VALUE", textField: "DAY", resize: false} }
                    ]
    });

    //构建填报表格
    objGrid = $("#grid").ligerGrid({
        title: "河流数据填报",
        //dataAction: 'server',
        usePager: false,
        alternatingRow: true,
        checkbox: true,
        width: '100%',
        height: "99.5%",
        enabledEdit: true,
        toolbar: { items: [
                { text: '查询', click: showSearch, icon: 'search' },
                { text: '采样日期', click: showSamplingDay, icon: 'calendar' },
                { text: '计算评价值', click: modifyJUDGE, icon: 'calendar' },
                { text: '数据导入', click: ExcelImport, icon: 'database_wrench' },
                { text: '数据导出', click: ExcelExport, icon: 'excel' },
                { text: '审批签发', click: showFlow, icon: 'database' },
                { text: '补测数据', click: showBcData, icon: 'database' },
            //                { text: '排污河流导出', click: ExcelExportSewageRiver, icon: 'excel' },
            //                { text: '湖库水质导出', click: ExcelExportLakeRiver, icon: 'excel' },
            //                { text: '水质月报导出', click: ExcelExportWQRiver, icon: 'excel' },
            //                { text: '高桥导出', click: ExcelExportGQRiver, icon: 'excel' },
            //                { text: '界牌导出', click: ExcelExportJPRiver, icon: 'excel' },
            //                { text: '蓝藻水华导出', click: ExcelExportLZSHRiver, icon: 'excel' },
            //                { text: '重金属', click: ExcelExportMetalRiver, icon: 'excel' },
            //                { text: '饮用水源', click: ExcelExportWaterRiver, icon: 'excel' }
                {text: '导出模板汇总表', click: showSheetTypeDialog, icon: 'excel' }
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
                    var columnsArr = [
                    //                        { display: 'FillID', name: 'FillID', width: 60, minWidth: 60, align: "center" }
                    //                        { display: 'SECTION_ID', name: 'SECTION_ID', width: 60, minWidth: 60, align: "center" },
                    //                        { display: 'POINT_ID', name: 'POINT_ID', width: 60, minWidth: 60, align: "center" },
                    //                        { display: '年份', name: 'YEAR', width: 60, minWidth: 60, align: "center" },
                    //                        { display: '断面名称', name: 'SECTION_NAME', width: 120, minWidth: 60, align: "center" },
                    //                        { display: '垂线名称', name: 'VERTICAL_NAME', width: 60, minWidth: 60, align: "center" },
                    //                        { display: '月份', name: 'MONTH', minWidth: 100, width: 60, minWidth: 60, align: "center" },
                    //                        { display: '采样日期', name: 'SAMPLING_DAY', width: 60, minWidth: 60, align: "center", editor: { type: "text"} },
                    //                        { display: '水期', name: 'KPF', width: 60, minWidth: 60, align: "center", editor: { type: "select", valueField: "DICT_CODE", textField: "DICT_TEXT", data: KPFJson() },
                    //                            render: function (item) {
                    //                                return getDicName(item.KPF, 'KPF');
                    //                            }
                    //                        },
                    //                        { display: '超标污染物', name: 'OVERPROOF', width: 60, minWidth: 60, align: "center", editor: { type: "text"} }
                    ];

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
                    //objGrid.toggleCol("SECTION_ID");
                    //objGrid.toggleCol("POINT_ID");

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
        //data: "type=GetDict&dictType=administrative_area",
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

function f_onAfterEdit(e, r) {
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
                    //e.value = "1";
                    //getData();
                    //objGrid.updateCell(e.column.columnname, '<a style="color:red">1</a>', e.rowindex);
                } else {
                }
            }
        });
      
    }
}

function modifyJUDGE() {
    var ids = "";
    if (objGrid.rows.length > 0) {
        for (var i = 0; i < objGrid.rows.length; i++) {
            ids += objGrid.getRow(i)["ID"] + ',';
        }
        $.ajax({
            url: url,
            data: "type=ModifyJUDGE&ids=" + ids,
            type: "post",
            dataType: "json",
            async: true,
            cache: false,
            success: function (json) {
                if (json.result == "success") {
                    $.ligerDialog.success("计算成功");
                } else {
                    $.ligerDialog.warn("计算失败");
                }
            }
        });
    }
}

//保存数据
function saveData() {
    var data = "";

    //构建填报数据字符串
    data = JSON.stringify(objGrid.getData());

//    //开始保存数据
//    if (data != "") {
//        $.ajax({
//            url: url,
//            data: "type=SaveData&data=" + data,
//            type: "post",
//            dataType: "json",
//            async: true,
//            cache: false,
//            beforeSend: function () {
//                $.ligerDialog.waitting('数据保存中,请稍候...');
//            },
//            complete: function () {
//                $.ligerDialog.closeWaitting();
//            },
//            error: function () { $.ligerDialog.error("保存数据出错"); },
//            success: function (json) {
//                if (json.result == "success") {
//                    $.ligerDialog.success('保存成功');
//                    getData();
//                } else {
//                    $.ligerDialog.warn('保存失败');
//                }
//            }
//        });
//    }
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
        data: "type=GetFillID&year=" + year + "&month=" + month,
        type: "post",
        dataType: "json",
        async: false,
        cache: false,
        success: function (obj) {
            if (obj.ID.length > 0) {
                if (obj.STATUS == "" || obj.STATUS == "0") {
                    $.ligerDialog.confirm('确定填报数据已经完整？', function (yes) {
                        if (yes) {
                            var surl = '../Sys/WF/WFStartPage.aspx?action=|type=true|pf_id=HL^' + year + '^' + month + '|&WF_ID=PF';
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

//导入Excel
function ExcelImport() {
    $.ligerDialog.open({ title: "Excel导入界面", width: 500, height: 200, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            value = $("iframe")[0].contentWindow.Import("River");
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); getData(); }
        }], url: "../FillImportDemo.aspx"
    });
}
//导出Excel
function ExcelExport() {
    var month = $("#hidMonth").val();
    var year = $("#hidYear").val();
    if (month != "" && year != "") {
        var vsURL = "../FillExportDemo.aspx?action=FillData&type=River&month=" + month + "&year=" + year;
        var iWidth = screen.availWidth - 400;
        var iHeight = screen.availHeight - 300;
        var vsStyle = "menubar= no, toolbar= no, scrollbars=no, status=yes,location=no,left = 0,top= 0, resizable= yes, titlebar= yes, width= 1, height= 1";
        window.open(vsURL, "newwindow", vsStyle);
    }
    else {
        $.ligerDialog.warn('查询条件的年度和月度不能为空');
    }
} 

//导出Excel 排污河流数据 黄进军添加20141114
function ExcelExportSewageRiver() {
    var month = $("#hidMonth").val();
    var year = $("#hidYear").val();
    if (month != "" && year != "") {
        var vsURL = "../FillExportDemo.aspx?action=FillData&type=SewageRiver&month=" + month + "&year=" + year;
        var iWidth = screen.availWidth - 400;
        var iHeight = screen.availHeight - 300;
        var vsStyle = "menubar= no, toolbar= no, scrollbars=no, status=yes,location=no,left = 0,top= 0, resizable= yes, titlebar= yes, width= 1, height= 1";
        window.open(vsURL, "newwindow", vsStyle);
    }
    else {
        $.ligerDialog.warn('查询条件的年度和月度不能为空');
    }
}

//导出Excel 湖库水质 河流数据 黄进军添加20141114
function ExcelExportLakeRiver() {
    var month = $("#hidMonth").val();
    var year = $("#hidYear").val();
    if (month != "" && year != "") {
        var vsURL = "../FillExportDemo.aspx?action=FillData&type=LakeRiver&month=" + month + "&year=" + year;
        var iWidth = screen.availWidth - 400;
        var iHeight = screen.availHeight - 300;
        var vsStyle = "menubar= no, toolbar= no, scrollbars=no, status=yes,location=no,left = 0,top= 0, resizable= yes, titlebar= yes, width= 1, height= 1";
        window.open(vsURL, "newwindow", vsStyle);
    }
    else {
        $.ligerDialog.warn('查询条件的年度和月度不能为空');
    }
}

//导出Excel 水质月报 河流数据 黄进军添加20141114
function ExcelExportWQRiver() {
    var month = $("#hidMonth").val();
    var year = $("#hidYear").val();
    if (month != "" && year != "") {
        var vsURL = "../FillExportDemo.aspx?action=FillData&type=WQRiver&month=" + month + "&year=" + year;
        var iWidth = screen.availWidth - 400;
        var iHeight = screen.availHeight - 300;
        var vsStyle = "menubar= no, toolbar= no, scrollbars=no, status=yes,location=no,left = 0,top= 0, resizable= yes, titlebar= yes, width= 1, height= 1";
        window.open(vsURL, "newwindow", vsStyle);
    }
    else {
        $.ligerDialog.warn('查询条件的年度和月度不能为空');
    }
}

//导出Excel 高桥断面 河流数据 黄进军添加20141114
function ExcelExportGQRiver() {
    var month = $("#hidMonth").val();
    var year = $("#hidYear").val();
    if (month != "" && year != "") {
        var vsURL = "../FillExportDemo.aspx?action=FillData&type=GQRiver&month=" + month + "&year=" + year;
        var iWidth = screen.availWidth - 400;
        var iHeight = screen.availHeight - 300;
        var vsStyle = "menubar= no, toolbar= no, scrollbars=no, status=yes,location=no,left = 0,top= 0, resizable= yes, titlebar= yes, width= 1, height= 1";
        window.open(vsURL, "newwindow", vsStyle);
    }
    else {
        $.ligerDialog.warn('查询条件的年度和月度不能为空');
    }
}

//导出Excel 界牌 河流数据 黄进军添加20141114
function ExcelExportJPRiver() {
    var month = $("#hidMonth").val();
    var year = $("#hidYear").val();
    if (month != "" && year != "") {
        var vsURL = "../FillExportDemo.aspx?action=FillData&type=JPRiver&month=" + month + "&year=" + year;
        var iWidth = screen.availWidth - 400;
        var iHeight = screen.availHeight - 300;
        var vsStyle = "menubar= no, toolbar= no, scrollbars=no, status=yes,location=no,left = 0,top= 0, resizable= yes, titlebar= yes, width= 1, height= 1";
        window.open(vsURL, "newwindow", vsStyle);
    }
    else {
        $.ligerDialog.warn('查询条件的年度和月度不能为空');
    }
}

//导出Excel 蓝藻水华 河流数据 黄进军添加20141114
function ExcelExportLZSHRiver() {
    var month = $("#hidMonth").val();
    var year = $("#hidYear").val();
    if (month != "" && year != "") {
        var vsURL = "../FillExportDemo.aspx?action=FillData&type=LZSHRiver&month=" + month + "&year=" + year;
        var iWidth = screen.availWidth - 400;
        var iHeight = screen.availHeight - 300;
        var vsStyle = "menubar= no, toolbar= no, scrollbars=no, status=yes,location=no,left = 0,top= 0, resizable= yes, titlebar= yes, width= 1, height= 1";
        window.open(vsURL, "newwindow", vsStyle);
    }
    else {
        $.ligerDialog.warn('查询条件的年度和月度不能为空');
    }
}

//导出Excel 重金属 河流数据 黄进军添加20141114
function ExcelExportMetalRiver() {
    var month = $("#hidMonth").val();
    var year = $("#hidYear").val();
    if (month != "" && year != "") {
        var vsURL = "../FillExportDemo.aspx?action=FillData&type=MetalRiver&month=" + month + "&year=" + year;
        var iWidth = screen.availWidth - 400;
        var iHeight = screen.availHeight - 300;
        var vsStyle = "menubar= no, toolbar= no, scrollbars=no, status=yes,location=no,left = 0,top= 0, resizable= yes, titlebar= yes, width= 1, height= 1";
        window.open(vsURL, "newwindow", vsStyle);
    }
    else {
        $.ligerDialog.warn('查询条件的年度和月度不能为空');
    }
}

//导出Excel 饮用水源 河流数据 黄进军添加20141114
function ExcelExportWaterRiver() {
    var month = $("#hidMonth").val();
    var year = $("#hidYear").val();
    if (month != "" && year != "") {
        var vsURL = "../FillExportDemo.aspx?action=FillData&type=WaterRiver&month=" + month + "&year=" + year;
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
        obj = [{ column: { columnname: gridJSON.UnSureColumns[5].columnId }, record: objGrid.getRow(i), rowindex: i, value: day}][0];
        f_onAfterEdit(obj);

        objGrid.updateCell(gridJSON.UnSureColumns[5].columnId, day, rowDom);
    }
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

function showBcData() {
    var year = $("#hidYear").val();
    var month = $("#hidMonth").val();
    var pointId = $("#hidPoint").val();

    $.ligerDialog.open({ title: "补测数据", width: 900, height: 550, isHidden: false, buttons:
        [
        { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "RiverFillBc.aspx?strYear=" + year + "&strMonth=" + month + "&strPointID=" + pointId
    });
}



//何海亮修改
function showSheetTypeDialog() {
    var vSheetType = null

    vSheetType = [
    { "VALUE": "ExcelExportSewageRiver", "NAME": "排污河流导出" },
    { "VALUE": "ExcelExportLakeRiver", "NAME": "湖库水质导出" },
    { "VALUE": "ExcelExportWQRiver", "NAME": "水质月报导出" },
    { "VALUE": "ExcelExportGQRiver", "NAME": "高桥导出" },
    { "VALUE": "ExcelExportWaterRiver", "NAME": "饮用水源" },
    { "VALUE": "ExcelExportJPRiver", "NAME": "界牌导出" },
    { "VALUE": "ExcelExportLZSHRiver", "NAME": "蓝藻水华导出" },
    { "VALUE": "ExcelExportMetalRiver", "NAME": "重金属" }
    ];

    //弹出窗口，供客户选择汇总表
    var sheetTypeForm = $("#sheetTypeForm");
    sheetTypeForm.ligerForm({
        inputWidth: 320, labelWidth: 90, space: 0, labelAlign: 'right',
        fields: [
                      { display: "模板", name: "ddlSheetType", comboboxName: "ddlSheetType_OP", newline: false, type: "select", group: "查询信息", groupicon: groupicon, options: { valueFieldID: "ddlSheetType", valueField: "VALUE", textField: "NAME", resize: false, data: vSheetType} }
                    ]
    });

    //$("#ddlSheetType_OP").ligerGetComboBoxManager().setData(vSheetType);

    var showSheetTypeWin = null;
    showSheetTypeWin = $.ligerDialog.open({
        target: $("#sheetType"),
        width: 450, height: 150, top: 90, title: "请选择导出模板",
        buttons: [
                  { text: '确定', onclick: function () { excelExportFor_SubTask_Summary(); showSheetTypeWin.hide(); } },
                  { text: '取消', onclick: function () { showSheetTypeWin.hide(); } }
                  ]
    });
    //根据客户选择的汇总表导出Excel
    function excelExportFor_SubTask_Summary() {
        var functionName = $("#ddlSheetType").val();
        switch (functionName) {
            case functionName = "ExcelExportSewageRiver":
                ExcelExportSewageRiver();
                break;
            case functionName = "ExcelExportLakeRiver":
                ExcelExportLakeRiver()
                break;
            case functionName = "ExcelExportWQRiver":
                ExcelExportWQRiver()
                break;
            case functionName = "ExcelExportGQRiver":
                ExcelExportGQRiver()
                break;
            case functionName = "ExcelExportJPRiver":
                ExcelExportJPRiver()
                break;
            case functionName = "ExcelExportLZSHRiver":
                ExcelExportLZSHRiver()
                break;
            case functionName = "ExcelExportMetalRiver":
                ExcelExportMetalRiver()
                break;
            case functionName = "ExcelExportWaterRiver":
                ExcelExportWaterRiver()
                break;
        }
    }
}

