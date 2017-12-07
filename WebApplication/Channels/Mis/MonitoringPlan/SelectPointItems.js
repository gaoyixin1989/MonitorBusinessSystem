
//@ Create By Castle(胡方扬) 2012-12-01
//@ Company: Comleader(珠海高凌)
//@ 功能：委托书监测点位监测项目设置
//@ *修改人（时间）:
//@ *修改原因：
var mdivsamp = null, sampdiv = null;
var Itemgrid = null;
var gridPointSelectId = "";
var strContractPointId = "";
var GetFRQData = null, GetMonitorItemData = null, GetIndustryItemData = null, GetSelectedComboxData = null;
var boolresult = false;
var moveid = "", strKeyColumns = "", strTableName = "", strFatherTableName = "", strFatherKeyColumn = "",  strEnvTypeId = "", strPointId = "", strPointQcSetting_Id = "", strQcStatus = "";
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
    //获取URL 参数
    strEnvTypeId = $.getUrlVar('strEvnTypeId'); //$.query.get('standartId');
    strPointId = $.getUrlVar('strPointId');
    strPointQcSetting_Id = $.getUrlVar('strPointQcSetting_Id');
    strQcStatus = $.getUrlVar('strQCType');

    if (strEnvTypeId && strEnvTypeId != "") {
        LoadEvnGridData();
    }
    function ListBoxClear() {
        strListBox = $("#listRight option");
        strListBox.each(function () {
            this.selected = true;
        })
        vSelect = $("#listRight option:selected");

        if (vSelect.length > 0) {
            vSelect.clone().appendTo("#listLeft");
            vSelect.remove();
        }
    }
    GetMonitorSubItems();
    getItems();

    function LoadEvnGridData() {
        if (strEnvTypeId != "" && strPointId) {
            switch (strEnvTypeId) {
                case "EnvRiver": //河流 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    strFatherKeyColumn = "ID";
                    strFatherTableName = "T_ENV_P_RIVER_V";
                    //点位关键字段   
                    strKeyColumns = "POINT_ID";
                    //获取点位表   
                    strTableName = "T_ENV_P_RIVER_V_ITEM";
                    break;
                case "EnvReservoir": //湖库 Modify By 胡方扬 2013-06-14 根据新数据结构修改

                    strFatherKeyColumn = "ID";
                    strFatherTableName = "T_ENV_P_LAKE_V";

                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_LAKE_V_ITEM";
                    break;
                case "EnvStbc": //生态补偿

                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_PAYFOR_ITEM";
                    break;
                case "EnvDrinking": //地下饮用水 Modify By 胡方扬 2013-06-13 根据新数据结构修改
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_DRINK_UNDER_ITEM";
                    break;
                case "EnvDrinkingSource": //饮用水源地（河流、湖库)   Modify By 胡方扬 2013-06-13 根据新数据结构修改

                    strFatherKeyColumn = "ID";
                    strFatherTableName = "T_ENV_P_DRINK_SRC_V";
                    //点位关键字段   
                    strKeyColumns = "POINT_ID";
                    //获取点位表   
                    strTableName = "T_ENV_P_DRINK_SRC_V_ITEM";
                    break;
                case "EnvDWWater": //双三十水  Modify By 胡方扬 2013-06-13 根据新数据结构修改

                    strFatherKeyColumn = "ID";
                    strFatherTableName = "T_ENV_P_RIVER30_V";
                    //点位关键字段   
                    strKeyColumns = "POINT_ID";
                    //获取点位表   
                    strTableName = "T_ENV_P_RIVER30_V_ITEM";
                    break;
                case "EnvMudRiver": //沉积物（河流） Modify By 胡方扬 2013-06-14 根据新数据结构修改  新

                    strFatherKeyColumn = "ID";
                    strFatherTableName = "T_ENV_P_MUD_RIVER_V";
                    //点位关键字段   
                    strKeyColumns = "POINT_ID";
                    //获取点位表   
                    strTableName = "T_ENV_P_MUD_RIVER_V_ITEM";
                    break;
                case "EnvMudSea": //沉积物（海水） Modify By 胡方扬 2013-06-14 根据新数据结构修改 新

                    strFatherKeyColumn = "ID";
                    strFatherTableName = "T_ENV_P_MUD_SEA_V";
                    //点位关键字段   
                    strKeyColumns = "POINT_ID";
                    //获取点位表   
                    strTableName = "T_ENV_P_MUD_SEA_V_ITEM";
                    break;
                case "EnvSoil": //土壤 Modify By 胡方扬 2013-06-14 根据新数据结构修改 新

                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_SOIL_ITEM";
                    break;
                case "EnvPSoild": //固废 Modify By 胡方扬 2013-06-14 根据新数据结构修改 新

                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_SOILD_ITEM";
                    break;
                case "EnvEstuaries": //入海河口 Modify By 胡方扬 2013-06-14 根据新数据结构修改

                    strFatherKeyColumn = "ID";
                    strFatherTableName = "T_ENV_P_ESTUARIES_V";
                    //点位关键字段   
                    strKeyColumns = "POINT_ID";
                    //获取点位表   
                    strTableName = "T_ENV_P_ESTUARIES_V_ITEM";
                    break;
                case "EnvSear": //近岸海域 Modify By 胡方扬 2013-06-14 根据新数据结构修改

                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_SEA_ITEM";
                    break;
                case "EnvSource": //近岸直排 Modify By 胡方扬 2013-06-14 根据新数据结构修改

                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_OFFSHORE_ITEM";
                    break;
                case "EnvSeaBath": //海水浴场 Modify By 胡方扬 2013-06-14 根据新数据结构修改 新

                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_SEABATH_ITEM";
                    break;
                case "EnvDust": //降尘 Modify By 胡方扬 2013-06-14 根据新数据结构修改

                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_DUST_ITEM";
                    break;
                case "EnvRain": //降水 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_RAIN_ITEM";
                    break;
                /*======================================噪声 Start========================================*/ 
                case "EnvRoadNoise": //道路交通噪声 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_NOISE_ROAD_ITEM";
                    break;
                case "FunctionNoise": //功能区噪声 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_NOISE_FUNCTION_ITEM";
                    break;
                case "AreaNoise": //区域环境噪声  Modify By 胡方扬 2013-06-14 根据新数据结构修改

                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_NOISE_AREA_ITEM";
                    break;
                /*=======================================噪声 End=======================================*/ 
                /*======================================环境空气 Start========================================*/ 
                case "EnvAir": //环境空气 Modify By 胡方扬 2013-06-14 根据新数据结构修改

                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_AIR_ITEM";
                    break;
                case "EnvSpeed": //硫酸盐化速率 Modify By 胡方扬 2013-06-14 根据新数据结构修改

                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_ALKALI_ITEM";
                    break;
                case "EnvDWAir": //双三十废气 Modify By 胡方扬 2013-06-14 根据新数据结构修改

                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_AIR30_ITEM";
                    break;
                /*======================================环境空气 End========================================*/ 
                default:
                    break;
            }
        }
    }
    //DIV 设置条件项目监测项目部分

    var vSelectedData = null, vItemData = null, vHaveItemData = null;

    //初始化左侧ListBox
    function GetMonitorSubItems() {
        vItemData = null;
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=GetEnvPointItems&strPointQcSetting_Id=" + strPointQcSetting_Id + "&strQcStatus="+strQcStatus+"&strPointId=" + strPointId + "&strTableName=" + strTableName + "&strKeyColumns=" + strKeyColumns+"&strFatherKeyColumn="+strFatherKeyColumn+"&strFatherTableName="+strFatherTableName,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    vItemData = data.Rows;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });

        //bind data
        var vlist = "";
        //遍历json数据,获取监测项目列表
        jQuery.each(vItemData, function (i, n) {
            vlist += "<option value=" + vItemData[i].ID + ">" + vItemData[i].ITEM_NAME + "</option>";
        });
        //绑定数据到listLeft
        $("#listLeft").append(vlist);

        $("#pageloading").hide();
    }


    //right move
    $("#btnRight").click(function () {
        moveright();
    });
    //double click to move left
    $("#listLeft").dblclick(function () {
        //克隆数据添加到listRight中
        moveright();
    });

    //left move 
    $("#btnLeft").click(function () {
        moveleft();
    });

    //double click to move right
    $("#listRight").dblclick(function () {
        moveleft();
    });

    function moveright() {
        //数据option选中的数据集合赋值给变量vSelect
        var isExist = false;
        var vSelect = $("#listLeft option:selected");
        //克隆数据添加到listRight中
        if ($("#listRight option").length > 0) {
            $("#listRight option").each(function () {
                if ($(this).val() == $("#listLeft option:selected").val()) {
                    $.ligerDialog.warn('所选数据已存在！');
                    return isExist = false;
                }
                else {
                    isExist = true;
                }
            });
        }
        else {
            isExist = true;
        }
        if (isExist) {
            vSelect.clone().appendTo("#listRight");
            vSelect.remove();
        }
    }
    function moveleft() {
        moveid = "";
        var vSelect = $("#listRight option:selected");
        if (vSelect.length > 0) {
            for (var i = 0; i < vSelect.length; i++) {
                moveid += vSelect[i].value + ";";
            }
        }
        vSelect.clone().appendTo("#listLeft");
        vSelect.remove();
    }
    function getItems() {
        vSelectedData = null;
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=GetEnvPointItemsForQcSetting&strQcStatus=" + strQcStatus + "&strPointQcSetting_Id=" + strPointQcSetting_Id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    vSelectedData = data.Rows;
                }
                else {
                    $.ligerDialog.warn('获取数据失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });

        var vItemlist = "";
        jQuery.each(vSelectedData, function (i, n) {
            vItemlist += "<option value=" + vSelectedData[i].ID + ">" + vSelectedData[i].ITEM_NAME + "</option>";
        });
        if (vItemlist.length > 0) {
            $("#listRight").append(vItemlist);
        }
    }
    $("#pageloading").hide();
});


function GetMoveItems() {
    var strData = "";
    moveid = moveid.substring(0, moveid.length - 1);
    strData = moveid;

    return strData;
}

function GetSelectItems() {
    var strData = "";
    if ($("#listRight option").length > 0) {
        var strevmonitorItems = "", strpointitemName = "";
        $("#listRight option").each(function () {
            strevmonitorItems += $(this).val() + ";";
            strpointitemName += $(this).text() +";"
        });
        strevmonitorItems = strevmonitorItems.substring(0, strevmonitorItems.length - 1);
        strpointitemName = strpointitemName.substring(0, strpointitemName.length - 1);
        strData = "&strPointItem=" + strevmonitorItems + "&strPointItemName=" +encodeURI(strpointitemName);
    }

    return strData;
}

function txtSeachOption() {
    //移除上次查询的底色
    $('#listLeft option').css({ 'background-color': '' })
    //获取所有包含查询内容的文本ListBox,并遍历
    $('#listLeft option:contains("' + $('#txtSeach').val() + '")').each(function () {
        $(this).css({ 'background-color': '#6FC8F5' });
    });
}