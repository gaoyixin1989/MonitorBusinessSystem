//created by 刘静楠 填报查询统计界面

var objGrid = null;
var gridJSON = null;
var url = "FillQry.aspx";
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
var SeasonJson = [{ "VALUE": " ", "Season": "--全部--" }, { "VALUE": "1", "Season": "一季度" }, { "VALUE": "2", "Season": "二季度" }, { "VALUE": "3", "Season": "三季度" }, { "VALUE": "4", "Season": "四季度"}];
var HalfYearJson = [{ "VALUE": " ", "HalfYear": "--全部--" }, { "VALUE": "1", "HalfYear": "上半年" }, { "VALUE": "2", "HalfYear": "下半年"}];
var ContionJson = [{ "VALUE": "1", "TimeType": "月度查询" }, { "VALUE": "2", "TimeType": "季度查询" }, { "VALUE": "3", "TimeType": "半年查询"}];
$(document).ready(function () {
    //创建查询表单结构
    var aa = $("#searchForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "时间类别", name: "ddlTimeType", newline: false, type: "select", group: "必选", groupicon: groupicon, comboboxName: "ddlTimeTypeBox", width: 450, options: { valueFieldID: "hidTimeType", valueField: "VALUE", textField: "TimeType", resize: false, data: ContionJson} },
                      { display: "类别", name: "ddlType", newline: true, type: "select", comboboxName: "ddlTypeBox", options: { valueFieldID: "hidType", valueField: "DICT_CODE", textField: "DICT_TEXT", resize: false} },
                      { display: "年度", name: "ddlYear", newline: false, type: "select", comboboxName: "ddlYearBox", options: { valueFieldID: "hidYear", valueField: "value", textField: "value", resize: false, url: url + "?action=GetYear"} },
                      { display: "监测点", name: "ddlPoint", newline: true, type: "select", group: "可选", groupicon: groupicon, comboboxName: "ddlPointBox", options: { valueFieldID: "hidPoint", valueField: "CODE", textField: "NAME", resize: false} },
                      { display: "月份", name: "ddlMonth", newline: false, type: "select", comboboxName: "ddlMonthBox", options: { valueFieldID: "hidMonth", valueField: "VALUE", textField: "MONTH", resize: false, data: monthJSON, isShowCheckBox: true, isMultiSelect: true, selectBoxHeight: 265} },
                      { display: "季度", name: "ddlSeason", newline: true, type: "select", comboboxName: "ddlSeasonBox", options: { valueFieldID: "hidSeason", valueField: "VALUE", textField: "Season", resize: false, data: SeasonJson} },
                      { display: "半年", name: "ddlHalfYear", newline: false, type: "select", comboboxName: "ddlHalfYearBox", options: { valueFieldID: "hidHalfYear", valueField: "VALUE", textField: "HalfYear", resize: false, data: HalfYearJson} }
                    ]
    });

    //下拉框默认值 
    $.ligerui.get("ddlYearBox").selectValue(new Date().getFullYear());
    $.ligerui.get("ddlTimeTypeBox").selectValue("1"); //时间类别默认值
    $.ligerui.get("ddlSeasonBox").selectValue(" "); //季度默认值
    $.ligerui.get("ddlHalfYearBox").selectValue(" "); //半年默认值
    var timetype = $("#hidTimeType").val();
    if (timetype == "1") {//月度：半年和季度不能选
        $("#ddlMonthBox").ligerGetComboBoxManager().setEnabled();
        $("#ddlSeasonBox").ligerGetComboBoxManager().setDisabled();
        $("#ddlHalfYearBox").ligerGetComboBoxManager().setDisabled();
    }

    //构建统计表格
    objGrid = $("#grid").ligerGrid({
        title: '填报查询统计',
        dataAction: 'server',
        usePager: true,
        pageSize: 31,
        pageSizeOptions: [11, 21, 31, 41],
        alternatingRow: true,
        checkbox: true,
        enabledEdit: true,
        width: '100%',
        height: '100%',
        columns: [
                { display: '年度', name: 'YEAR', align: 'left', width: 100, minWidth: 60 },
                { display: '月度', name: 'TEMP', align: 'left', width: 100, minWidth: 60 }, //根据查询条的中的半年、季度和月度的值，显示其字段名称
                {display: '监测点名称', name: 'POINT_NAME', align: 'left', width: 100, minWidth: 60 },
                { display: '监测项目名称', name: 'ITEM_NAME', minWidth: 100 },
                { display: '监测项目平均值', name: 'AVG_VALUE', minWidth: 100 },
                { display: '监测项目最大值', name: 'MAX_VALUE', minWidth: 100 },
                { display: '监测项目最小值', name: 'MIN_VALUE', minWidth: 100 }
        ],
        toolbar: { items: [
                { text: '查询', click: showSearch, icon: 'search' },
                { text: '数据导出', click: ExcelOut, icon: 'excel' }
                ]
        }
    });
    objGrid.toggleCol("TEMP"); //隐藏列
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
});

//弹出查询窗口
var searchDialog = null;
function showSearch() {
    if (searchDialog) {
        searchDialog.show();
    } else {
        GetDict();  //获取类别  
        getPoint();     //获取监测点
        //年下拉框联动
        $.ligerui.get("ddlYearBox").bind("selected", function () {
            getPoint();
        });
        //月下拉框联动
        $.ligerui.get("ddlMonthBox").bind("selected", function () {
            getPoint();
        });
        //类别下拉框联动
        $.ligerui.get("ddlTypeBox").bind("selected", function () {
            getPoint();
        });
        //季度下拉框联动
        $.ligerui.get("ddlSeasonBox").bind("selected", function () {
            getPoint();
        });
        //半年下拉框联动
        $.ligerui.get("ddlHalfYearBox").bind("selected", function () {
            getPoint();
        });
        //类别下拉框联动
        $.ligerui.get("ddlTimeTypeBox").bind("selected", function () {
            var timetype = $("#hidTimeType").val();
            if (timetype == "1") {//月度：半年和季度不能选
                $("#ddlMonthBox").ligerGetComboBoxManager().setEnabled();
                $("#ddlSeasonBox").ligerGetComboBoxManager().setDisabled();
                $("#ddlHalfYearBox").ligerGetComboBoxManager().setDisabled();
                $("#ddlHalfYearBox").ligerGetComboBoxManager().setValue(" ");
                $("#ddlSeasonBox").ligerGetComboBoxManager().setValue(" ");
                $("#ddlMonthBox").ligerGetComboBoxManager().setData(monthJSON);//重新设置月份的值
            }
            if (timetype == "2") {//季度：月度和半年不能选
                $("#ddlMonthBox").ligerGetComboBoxManager().setDisabled();
                $("#ddlHalfYearBox").ligerGetComboBoxManager().setValue(" ");
                $("#ddlSeasonBox").ligerGetComboBoxManager().setEnabled();
                $("#ddlMonthBox").ligerGetComboBoxManager().setDisabled();
                $("#ddlMonthBox").ligerGetComboBoxManager().clearContent();//清空月份的值
                $("#ddlMonthBox").ligerGetComboBoxManager().setValue(" ");//默认值为空 
               
            }
            if (timetype == "3") {//半年：月度和季度不能选
                $("#ddlMonthBox").ligerGetComboBoxManager().setDisabled();
                $("#ddlSeasonBox").ligerGetComboBoxManager().setValue(" ");
                $("#ddlSeasonBox").ligerGetComboBoxManager().setDisabled();
                $("#ddlHalfYearBox").ligerGetComboBoxManager().setEnabled();
                $("#ddlMonthBox").ligerGetComboBoxManager().clearContent(); //清空月份的值
                $("#ddlMonthBox").ligerGetComboBoxManager().setValue(" "); //默认值为空 
            }
        });
        //弹出窗口
        searchDialog = $.ligerDialog.open({
            target: $("#searchDiv"),
            width: 650, height: 250, top: 90, title: "查询",
            buttons: [
                  { text: '确定', onclick: function () { getData();searchDialog.hide(); } },
                  { text: '取消', onclick: function () { searchDialog.hide(); } } 
                  ]
        });
    }
}

//获取类别
function GetDict() {
    $.ajax({
        url: url,
        data: "action=GetDict&dictType=EnvTypes", //发送给对象服务器，以对象或URL的形式
        type: "post", //请求方式
        dataType: "json", //希望服务器以“json”的形式返回值
        async: false, //true:异步请求；fasle:同步请求
        cache: false, //不需要缓存
        success: function (json) {//如果请求成功执行该函数
            if (json.length > 0) {
                $.ligerui.get("ddlTypeBox").setData(eval(json));
                $.ligerui.get("ddlTypeBox").selectValue(json[0].DICT_CODE)
            }
        }
    });
}

//获取监测点
function getPoint() {
    var year = $("#hidYear").val(); //获取查询框年的值
    var month = $("#hidMonth").val();
    var type = $("#hidType").val();
    var season = $("#hidSeason").val(); //季度
    var halfyear = $("#hidHalfYear").val(); //半年
    var timetype = $("#hidTimeType").val(); //时间类别
    $.ligerui.get("ddlPointBox").clearContent();
    if (year != "") {
        $.ajax({
            url: url,
            data: "action=GetPoint&year=" + year + "&month=" + month + "&type=" + type + "&searchType=" + timetype + "&quarter=" + season + "&half=" + halfyear, //发送给对象服务器，以对象或URL的形式
            type: "post", //请求方式
            dataType: "json", //希望服务器以“json”的形式返回值
            async: false, //true:异步请求；fasle:同步请求
            cache: false, //不需要缓存
            success: function (json) {//如果请求成功执行该函数
                if (json.length > 0) {
                    $.ligerui.get("ddlPointBox").setData(eval(json));
                    $.ligerui.get("ddlPointBox").selectValue(json[0].CODE)
                }
            }
        });
    }
}

//获取数据
function getData() {
    var timetype = $("#hidTimeType").val();//时间类别
    var year = $("#hidYear").val();//年度
    var month = $("#hidMonth").val();//月度
    var type = $("#hidType").val();//类别
    var point_id = $("#hidPoint").val(); //监测点
    var season = $("#hidSeason").val(); //季度
    var halfyear= $("#hidHalfYear").val(); //半年
    if (timetype == "1") {//月度
        if (month != "") {
            objGrid.toggleCol("TEMP","true");
            objGrid.changeHeaderText("TEMP", "月份"); //动态显示列
        }
        else
            objGrid.toggleCol("TEMP");
    }
    if (timetype == "2") {//季度：
        if (season != " ") {
            objGrid.toggleCol("TEMP", "true");
            objGrid.changeHeaderText("TEMP", "季度"); //动态显示列
        }
        else
            objGrid.toggleCol("TEMP");
    }
    if (timetype == "3") {//半年
        if (halfyear != " ") {
            objGrid.toggleCol("TEMP", "true");
            objGrid.changeHeaderText("TEMP", "半年"); //动态显示列
        }
        else
            objGrid.toggleCol("TEMP");
    }
    objGrid.set("url", url + "?action=GetData&year=" + year + "&month=" + month + "&type=" + type + "&point=" + point_id + "&searchType=" + timetype + "&quarter=" + season + "&half=" + halfyear);
}

//Excel导出
function ExcelOut() {
    var month = $("#hidMonth").val();//月份
    var year = $("#hidYear").val();//年份
    var type =  $("#hidType").val();//类别
    var point_id = $("#hidPoint").val();//监测点
    var timetype = $("#hidTimeType").val(); //时间类别
    var season = $("#hidPoint").val(); //季度
    var halfyear = $("#hidHalfYear").val(); //半年
    if (year != "" && type != "" && timetype != "") {
        var vsURL = "../FillExportDemo.aspx?action=FillQry&type=" + type + "&month=" + month + "&year=" + year + "&point_id=" + point_id + "&searchType=" + timetype + "&quarter=" + season + "&half=" + halfyear;
        var iWidth = screen.availWidth - 400;
        var iHeight = screen.availHeight - 300;
        var vsStyle = "menubar= no, toolbar= no, scrollbars=no, status=yes,location=no,left = 0,top= 0, resizable= yes, titlebar= yes, width= 10, height= 12";
        window.open(vsURL, "newwindow", vsStyle);
    }
}

