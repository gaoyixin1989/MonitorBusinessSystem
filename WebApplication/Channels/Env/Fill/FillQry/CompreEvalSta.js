
/// 综合评价统计报表
/// 创建人：魏林
/// 创建时间：2013-08-28

var objGrid = null;
var gridJSON = null;
var url = "CompreEvalSta.aspx";
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
    $("#searchForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "时间类别", name: "ddlTimeType", newline: false, type: "select", group: "必选", groupicon: groupicon, comboboxName: "ddlTimeTypeBox", width: 450, options: { valueFieldID: "hidTimeType", valueField: "VALUE", textField: "TimeType", resize: false, data: ContionJson} },
                      { display: "类别", name: "ddlType", newline: true, type: "select", comboboxName: "ddlTypeBox", options: { valueFieldID: "hidType", valueField: "DICT_CODE", textField: "DICT_TEXT", resize: false} },
                      { display: "年度", name: "ddlYear", newline: false, type: "select", comboboxName: "ddlYearBox", options: { valueFieldID: "hidYear", valueField: "value", textField: "value", resize: false, url: url + "?action=GetYear"} },
                      { display: "监测点", name: "ddlPoint", newline: true, type: "select", group: "可选", groupicon: groupicon, comboboxName: "ddlPointBox", options: { valueFieldID: "hidPoint", valueField: "CODE", textField: "NAME", resize: false} },
                      { display: "月份", name: "ddlMonth", newline: false, type: "select", comboboxName: "ddlMonthBox", options: { valueFieldID: "hidMonth", valueField: "VALUE", textField: "MONTH", resize: false, data: monthJSON, isShowCheckBox: true, isMultiSelect: true, selectBoxHeight: 265} },
                      { display: "季度", name: "ddlSeason", newline: true, type: "select", comboboxName: "ddlSeasonBox", options: { valueFieldID: "hidSeason", valueField: "VALUE", textField: "Season", resize: false, data: SeasonJson} },
                      { display: "半年", name: "ddlHalfYear", newline: false, type: "select", comboboxName: "ddlHalfYearBox", options: { valueFieldID: "hidHalfYear", valueField: "VALUE", textField: "HalfYear", resize: false, data: HalfYearJson} },
                      { display: "河流", name: "ddlRiver", newline: true, type: "select", comboboxName: "ddlRiverBox", options: { valueFieldID: "hidRiver", valueField: "ID", textField: "DICT_TEXT", resize: false} }
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
        title: '综合评价统计',
        dataAction: 'server',
        usePager: true,
        pageSize: 31,
        pageSizeOptions: [11, 21, 31, 41],
        alternatingRow: true,
        checkbox: false,
        enabledEdit: false,
        width: '100%',
        height: '100%',
        toolbar: { items: [
                { text: '查询', click: showSearch, icon: 'search' }
                ]
        }
    });

    GetEvalDict();  //获取类别  
    getPoint();     //获取监测点
    GetDict();  //获取河流

    getData();
});

//弹出查询窗口
var searchDialog = null;
function showSearch() {
    if (searchDialog) {
        searchDialog.show();
    } else {
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
            $("#ddlRiverBox").ligerGetComboBoxManager().setValue('');
            if ($("#hidType").val() == "EnvRiver")
                $("#ddlRiverBox").ligerGetComboBoxManager().setEnabled();
            else
                $("#ddlRiverBox").ligerGetComboBoxManager().setDisabled();
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
                $("#ddlMonthBox").ligerGetComboBoxManager().setData(monthJSON); //重新设置月份的值
            }
            if (timetype == "2") {//季度：月度和半年不能选
                $("#ddlMonthBox").ligerGetComboBoxManager().setDisabled();
                $("#ddlHalfYearBox").ligerGetComboBoxManager().setValue(" ");
                $("#ddlSeasonBox").ligerGetComboBoxManager().setEnabled();
                $("#ddlMonthBox").ligerGetComboBoxManager().setDisabled();
                $("#ddlMonthBox").ligerGetComboBoxManager().clearContent(); //清空月份的值
                $("#ddlMonthBox").ligerGetComboBoxManager().setValue(" "); //默认值为空 

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
            width: 650, height: 280, top: 90, title: "查询",
            buttons: [
                  { text: '确定', onclick: function () { getData(); searchDialog.hide(); } },
                  { text: '取消', onclick: function () { searchDialog.hide(); } }
                  ]
        });
    }
}

//获取类别
function GetEvalDict() {
    $.ajax({
        url: url,
        data: "action=GetEvalDict&dictType=EnvTypes", //发送给对象服务器，以对象或URL的形式
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

//获取类别
function GetDict() {
    $.ajax({
        url: url,
        data: "action=GetDict&dictType=LuanHe_Watershed", //发送给对象服务器，以对象或URL的形式
        type: "post", //请求方式
        dataType: "json", //希望服务器以“json”的形式返回值
        async: false, //true:异步请求；fasle:同步请求
        cache: false, //不需要缓存
        success: function (json) {//如果请求成功执行该函数
            if (json.length > 0) {
                $.ligerui.get("ddlRiverBox").setData(eval(json));
                $.ligerui.get("ddlRiverBox").selectValue(json[0].DICT_CODE)
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
    var timetype = $("#hidTimeType").val(); //时间类别
    var year = $("#hidYear").val(); //年度
    var month = $("#hidMonth").val(); //月度
    var type = $("#hidType").val(); //类别
    var point_id = $("#hidPoint").val(); //监测点
    var season = $("#hidSeason").val(); //季度
    var halfyear = $("#hidHalfYear").val(); //半年
    var river_id = $("#hidRiver").val(); //河流ID

    $.ajax({
        url: url,
        data: "action=GetData&year=" + year + "&month=" + month + "&type=" + type + "&point=" + point_id + "&searchType=" + timetype + "&quarter=" + season + "&half=" + halfyear+ "&river_id="+river_id,
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
                    columnsArr.push({ display: n.columnName, name: n.columnId, width: parseInt(n.columnWidth)+40, minWidth: 60, align: "center" });
                });

                objGrid.set("columns", columnsArr);
                objGrid.set("data", json);

                //隐藏不需要显示的列
                //objGrid.toggleCol("ID");
                //objGrid.toggleCol("SECTION_ID");
                //objGrid.toggleCol("POINT_ID");

                $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
            }
            else {
                objGrid.set("data", json);
            }
        }
    });
//    objGrid.set("url", url + "?action=GetData&year=" + year + "&month=" + month + "&type=" + type + "&point=" + point_id + "&searchType=" + timetype + "&quarter=" + season + "&half=" + halfyear);
}