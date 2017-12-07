/// 环境质量监测计划审批流程
/// 创建时间：2015-03-24
/// 创建人：魏林

var CCFLOW_WORKID = "";
var strPlanYear = "", strPlanMonth = "", strEnvTypeCode = "", strEnvTypeName = "";
var vEnvTypeCode = null, vEnvTypeName = null;

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
    $("#layout1").ligerLayout({ topHeight: 50, leftWidth: "99%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });

    CCFLOW_WORKID = $.getUrlVar('WorkID');

    $("#createDiv").html("<a style='color:Red;font-weight:bold'>加载数据出错，请重新登录系统</a>");

    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../../Mis/Contract/MethodHander.ashx?action=GetEnvPlanInfo&strCCFLOW_WORKID=" + CCFLOW_WORKID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null && data.Total != "0") {
                strPlanYear = data.Rows[0].PLAN_YEAR;
                strPlanMonth = data.Rows[0].PLAN_MONTH;
                strEnvTypeCode = data.Rows[0].PLAN_TYPE;
                strEnvTypeName = data.Rows[0].REAMRK1;

                vEnvTypeCode = strEnvTypeCode.split(";")
                vEnvTypeName = strEnvTypeName.split(";")

                CreatTable();
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax请求数据失败！');
        }
    });

})

function CreatTable() {
    $("#createDiv").html("");

    if (vEnvTypeCode.length > 0) {
        //根据选择的监测类别 动态生成监测类别Tab页
        var newDiv = '<div id="navtab1" position="center"  style = " width: 100%;height:100%; overflow:hidden; border:1px solid #A3C0E8; ">';
        for (var i = 0; i < vEnvTypeCode.length; i++) {

            if (i == 0) {
                newDiv += '<div id="div' + vEnvTypeCode[i] + '" title="' + vEnvTypeName[i] + '" tabid="home" lselected="true" style="height:680px" >';
            }
            else {
                newDiv += '<div id="div' + vEnvTypeCode[i] + '" title="' + vEnvTypeName[i] + '" style="height:680px" >';
            }

            var strUrl = "";
            switch (vEnvTypeCode[i]) {
                case "EnvRiver":
                    strUrl = "../../Env/Fill/River/RiverFill.aspx?strYear=" + strPlanYear + "&strMonth=" + strPlanMonth;
                    break;
                case "EnvDrinking":
                    strUrl = "../../Env/Fill/DrinkUnder/DrinkUnderFill.aspx?strYear=" + strPlanYear + "&strMonth=" + strPlanMonth;
                    break;
                case "EnvDrinkingSource":
                    strUrl = "../../Env/Fill/DrinkSource/DrinkSourceFill.aspx?strYear=" + strPlanYear + "&strMonth=" + strPlanMonth;
                    break;
                default:

            }

            newDiv += '<iframe frameborder="0" name="showmessage' + vEnvTypeCode[i] + '" src= "' + strUrl + '"></iframe>';
            newDiv += '</div>';
        }
        newDiv += '</div>';
        $(newDiv).appendTo(createDiv);
        $("#navtab1").ligerTab();


        $("#navtab1").ligerTab({
            //在点击选项卡之前触发
            onBeforeSelectTabItem: function (tabid) {
            },
            //在点击选项卡之后触发   点击其他的选项卡后，刷新该选项卡，防止CSS样式被串
            onAfterSelectTabItem: function (tabid) {
                navtab = $("#navtab1").ligerGetTabManager();
                navtab.reload(navtab.getSelectedTabItemID());
                if (tabid == "home") {
                    gridName = "0";
                }
                if (tabid == "tabitem1") {
                    gridName = "1";
                }
                // navtab.reload(tabid);
            }
        });

    }

}

//CCFLOW工作流发送前事件
function Save() {
    return 1;
}