var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var g = null;
var vContractData = null, vMonitorType = null, vAreaItem = null, vContratTypeItem = null;
var strEvnTypeId = "", strEvnTypeName = "", strEvnYear = "", strEnvMonth = "";
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
$(function () {
    strEvnTypeId = $.getUrlVar('strEvnTypeId');
    strEvnTypeName = decodeURI($.getUrlVar('strEnvTypeName'));
    strEvnYear = $.getUrlVar('strEnvYear');
    strEnvMonth = $.getUrlVar('strEnvMonth');
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetDict&type=administrative_area",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vAreaItem = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    function GetEnvTypeList() {
        //alert(strEvnTypeId+"987");
        if (strEvnTypeId) {
            switch (strEvnTypeId) {
                case "SewageCharges": //污染源常规（排污收费企业） add 黄进军 20151217
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strPoint_Area=POINT_CODE&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strTableName=T_ENV_P_POLLUTE&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    break;
                case "SewagePlant": //污染源常规（污水厂企业） add 黄进军 20150525
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strPoint_Area=POINT_CODE&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strTableName=T_ENV_P_POLLUTE&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    break;
                case "StateControlledWastewater": //污染源常规（企业（废水）） add 黄进军 20150525 
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strPoint_Area=POINT_CODE&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strTableName=T_ENV_P_POLLUTE&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    break;
                case "StateControlledGas": //污染源常规（企业（废气）） add 黄进军 20150525 
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strPoint_Area=POINT_CODE&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strTableName=T_ENV_P_POLLUTE&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    break;
                case "HeavyMetal": //污染源常规（重金属企业） add 黄进军 20150525 
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strPoint_Area=POINT_CODE&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strTableName=T_ENV_P_POLLUTE&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    break;
                case "EnvRiver": //地表水 Modify By 黄进军 2015-09-17 根据新数据结构修改
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strPoint_Area=AREA_ID&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strRiverArea=RIVER_ID&strTableName=T_ENV_P_SEDIMENT&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    SetColumns();
                    break;
                case "EnvReservoir": //湖库 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=SECTION_CODE&strPoint_Name=SECTION_NAME&strPoint_Area=AREA_ID&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strRiverArea=RIVER_ID&strValleyArea=VALLEY_ID&strTableName=T_ENV_P_LAKE&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    SetColumns();
                    break;
                case "EnvStbc": //生态补偿 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strPoint_Area=AREA_ID&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strRiverArea=RIVER_ID&strValleyArea=VALLEY_ID&strTableName=T_ENV_P_PAYFOR&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    SetColumns();
                    break;
                case "EnvDrinking": //(湖库)地下饮用水 Modify By 胡方扬 2013-06-13 根据新数据结构修改
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strPoint_Area=AREA_ID&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strRiverArea=RIVER_ID&strTableName=T_ENV_P_DRINK_UNDER&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    SetColumns();
                    break;
                case "EnvDrinkingSource": //饮用水源地（河流、湖库）Modify By 胡方扬 2013-06-13 根据新数据结构修改
                    //g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=SECTION_CODE&strPoint_Name=SECTION_NAME&strPoint_Area=AREA_ID&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strRiverArea=RIVER_ID&strTableName=T_ENV_P_DRINK_SRC|T_ENV_P_DRINK_SRC_V&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    //常规任务下达还是有几个类型点出不来（BUG） huangfei 20151214  by  strTableName  这个参数传值问题
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=SECTION_CODE&strPoint_Name=SECTION_NAME&strPoint_Area=AREA_ID&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strRiverArea=RIVER_ID&strTableName=T_ENV_P_DRINK_SRC&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    SetColumns();
                    break;

                case "EnvSoil": //土壤 Modify By 胡方扬 2013-06-14 根据新数据结构修改 新
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strPoint_Area=AREA_ID&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strTableName=T_ENV_P_SOIL&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    break;
                case "EnvDust": //降尘 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strPoint_Area=AREA_ID&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strTableName=T_ENV_P_DUST&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    break;
                case "EnvRain": //降水 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strPoint_Area=AREA_ID&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strTableName=T_ENV_P_RAIN&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    break;
                case "EnvAir": //环境空气 add By 黄进军 2015-09-17 根据新数据结构修改
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strPoint_Area=AREA_ID&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strTableName=T_ENV_P_AIR&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    break;
                /*======================================噪声 Start========================================*/ 
                case "EnvRoadNoise": //道路交通噪声 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    SetColumns();
                    g.set('url', "MonitoringPlan.ashx?action=GetPointForSome&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strTableName=T_ENV_P_NOISE_ROAD&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    break;
                case "FunctionNoise": //功能区噪声 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    SetColumns();
                    g.set('url', "MonitoringPlan.ashx?action=GetPointForSome&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strTableName=T_ENV_P_NOISE_FUNCTION&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    break;
                case "AreaNoise": //区域环境噪声 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    g.set('url', "MonitoringPlan.ashx?action=GetPoint&strYear=YEAR&strPoint_Code=POINT_CODE&strPoint_Name=POINT_NAME&strPoint_Area=AREA_ID&strEnvTypeId=" + strEvnTypeId + "&strEnvTypeName=" + encodeURI(strEvnTypeName) + "&strTableName=T_ENV_P_NOISE_AREA&strConditionAndValue=YEAR='" + strEvnYear + "'|MONTH='" + strEnvMonth + "'");
                    break;
                /*=======================================噪声 End=======================================*/ 

                default:
                    break;
            }
        }
    }

    function SetColumns() {
        var objColumn = null
        if (strEvnTypeId) {
            switch (strEvnTypeId) {
                case "EnvRoadNoise":
                    objColumn = [
                { display: '年度', name: 'YEAR', align: 'left', width: 80 },
                { display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },
                { display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },
                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' }
                ],
                    g.set("columns", objColumn);
                    break;
                case "FunctionNoise":
                    objColumn = [
                { display: '年度', name: 'YEAR', align: 'left', width: 80 },
                { display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },
                { display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },
                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' }
                ],
                    g.set("columns", objColumn);
                    break;
                //                case "EnvEstuaries":   
                //                    objColumn = [   
                //                { display: '年度', name: 'YEAR', align: 'left', width: 80 },   
                //                { display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },   
                //                { display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },   
                //                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' }   
                //                ],   
                //                    g.set("columns", objColumn);   
                //                    break;   
                case "EnvSear":
                    objColumn = [
                { display: '年度', name: 'YEAR', align: 'left', width: 80 },
                { display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },
                { display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },
                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' }
                ],
                    g.set("columns", objColumn);
                    break;
                case "EnvSeaBath":
                    objColumn = [
                { display: '年度', name: 'YEAR', align: 'left', width: 80 },
                { display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },
                { display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },
                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' }
                ],
                    g.set("columns", objColumn);
                    break;
                case "EnvRiver":
                    objColumn = [
                { display: '年度', name: 'YEAR', align: 'left', width: 80 },
                    //{ display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },
                {display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },
                { display: '垂线位置', name: 'VERTICAL_NAME', width: 100, align: 'left' },
                { display: '所属流域', name: 'VALLEY_ID', width: 120, align: 'left', render: function (items) {
                    if (items.VALLEY_ID != "") {
                        return getDictName(items.VALLEY_ID);
                    }
                    return items.VALLEY_ID;
                }
                },
                { display: '所属河流', name: 'RIVER_ID', width: 120, align: 'left', render: function (items) {
                    if (items.RIVER_ID != "") {
                        return getDictName(items.RIVER_ID);
                    }
                    return items.RIVER_ID;
                }
                },
                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' },
                { display: '所在区域', name: 'POINT_AREA', width: 80, minWidth: 60, data: vAreaItem, render: function (items) {
                    for (var i = 0; i < vAreaItem.length; i++) {
                        if (vAreaItem[i].DICT_CODE == items.POINT_AREA) {
                            return vAreaItem[i].DICT_TEXT;
                        }
                    }
                    return items.POINT_AREA;
                }
                }
                ],
                    g.set("columns", objColumn);
                    break;
                case "EnvReservoir":
                    objColumn = [
                { display: '年度', name: 'YEAR', align: 'left', width: 80 },
                    //{ display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },
                {display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },
                { display: '垂线位置', name: 'VERTICAL_NAME', width: 100, align: 'left' },
                { display: '所属流域', name: 'VALLEY_ID', width: 120, align: 'left', render: function (items) {
                    if (items.VALLEY_ID != "") {
                        return getDictName(items.VALLEY_ID);
                    }
                    return items.VALLEY_ID;
                }
                },
                { display: '所属河流', name: 'RIVER_ID', width: 120, align: 'left', render: function (items) {
                    if (items.RIVER_ID != "") {
                        return getDictName(items.RIVER_ID);
                    }
                    return items.RIVER_ID;
                }
                },
                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' },
                { display: '所在区域', name: 'POINT_AREA', width: 80, minWidth: 60, data: vAreaItem, render: function (items) {
                    for (var i = 0; i < vAreaItem.length; i++) {
                        if (vAreaItem[i].DICT_CODE == items.POINT_AREA) {
                            return vAreaItem[i].DICT_TEXT;
                        }
                    }
                    return items.POINT_AREA;
                }
                }
                ],
                    g.set("columns", objColumn);
                    break;
                case "EnvStbc":
                    objColumn = [
                { display: '年度', name: 'YEAR', align: 'left', width: 80 },
                { display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },
                { display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },
                { display: '所属流域', name: 'VALLEY_ID', width: 120, align: 'left', render: function (items) {
                    if (items.VALLEY_ID != "") {
                        return getDictName(items.VALLEY_ID);
                    }
                    return items.VALLEY_ID;
                }
                },
                { display: '所属河流', name: 'RIVER_ID', width: 120, align: 'left', render: function (items) {
                    if (items.RIVER_ID != "") {
                        return getDictName(items.RIVER_ID);
                    }
                    return items.RIVER_ID;
                }
                },
                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' },
                { display: '所在区域', name: 'POINT_AREA', width: 80, minWidth: 60, data: vAreaItem, render: function (items) {
                    for (var i = 0; i < vAreaItem.length; i++) {
                        if (vAreaItem[i].DICT_CODE == items.POINT_AREA) {
                            return vAreaItem[i].DICT_TEXT;
                        }
                    }
                    return items.POINT_AREA;
                }
                }
                ],
                    g.set("columns", objColumn);
                    break;
                case "EnvDrinking":
                    objColumn = [
                { display: '年度', name: 'YEAR', align: 'left', width: 80 },
                    //{ display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },
                {display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },
                { display: '所属河流', name: 'RIVER_ID', width: 120, align: 'left', render: function (items) {
                    if (items.RIVER_ID != "") {
                        return getDictName(items.RIVER_ID);
                    }
                    return items.RIVER_ID;
                }
                },
                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' },
                { display: '所在区域', name: 'POINT_AREA', width: 80, minWidth: 60, data: vAreaItem, render: function (items) {
                    for (var i = 0; i < vAreaItem.length; i++) {
                        if (vAreaItem[i].DICT_CODE == items.POINT_AREA) {
                            return vAreaItem[i].DICT_TEXT;
                        }
                    }
                    return items.POINT_AREA;
                }
                }
                ],
                    g.set("columns", objColumn);
                    break;
                case "EnvDrinkingSource":
                    objColumn = [
                { display: '年度', name: 'YEAR', align: 'left', width: 80 },
                    //{ display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },
                {display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },
                { display: '垂线位置', name: 'VERTICAL_NAME', width: 100, align: 'left' },
                { display: '所属河流', name: 'RIVER_ID', width: 120, align: 'left', render: function (items) {
                    if (items.RIVER_ID != "") {
                        return getDictName(items.RIVER_ID);
                    }
                    return items.RIVER_ID;
                }
                },
                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' },
                { display: '所在区域', name: 'POINT_AREA', width: 80, minWidth: 60, data: vAreaItem, render: function (items) {
                    for (var i = 0; i < vAreaItem.length; i++) {
                        if (vAreaItem[i].DICT_CODE == items.POINT_AREA) {
                            return vAreaItem[i].DICT_TEXT;
                        }
                    }
                    return items.POINT_AREA;
                }
                }
                ],
                    g.set("columns", objColumn);
                    break;
                case "EnvDWWater":
                    objColumn = [
                { display: '年度', name: 'YEAR', align: 'left', width: 80 },
                { display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },
                { display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },
                { display: '所属流域', name: 'VALLEY_ID', width: 120, align: 'left', render: function (items) {
                    if (items.VALLEY_ID != "") {
                        return getDictName(items.VALLEY_ID);
                    }
                    return items.VALLEY_ID;
                }
                },
                { display: '所属河流', name: 'RIVER_ID', width: 120, align: 'left', render: function (items) {
                    if (items.RIVER_ID != "") {
                        return getDictName(items.RIVER_ID);
                    }
                    return items.RIVER_ID;
                }
                },
                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' },
                { display: '所在区域', name: 'POINT_AREA', width: 80, minWidth: 60, data: vAreaItem, render: function (items) {
                    for (var i = 0; i < vAreaItem.length; i++) {
                        if (vAreaItem[i].DICT_CODE == items.POINT_AREA) {
                            return vAreaItem[i].DICT_TEXT;
                        }
                    }
                    return items.POINT_AREA;
                }
                }
                ],
                    g.set("columns", objColumn);
                    break;
                case "EnvMudRiver":
                    objColumn = [
                { display: '年度', name: 'YEAR', align: 'left', width: 80 },
                { display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },
                { display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },
                { display: '所属流域', name: 'VALLEY_ID', width: 120, align: 'left', render: function (items) {
                    if (items.VALLEY_ID != "") {
                        return getDictName(items.VALLEY_ID);
                    }
                    return items.VALLEY_ID;
                }
                },
                { display: '所属河流', name: 'RIVER_ID', width: 120, align: 'left', render: function (items) {
                    if (items.RIVER_ID != "") {
                        return getDictName(items.RIVER_ID);
                    }
                    return items.RIVER_ID;
                }
                },
                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' },
                { display: '所在区域', name: 'POINT_AREA', width: 80, minWidth: 60, data: vAreaItem, render: function (items) {
                    for (var i = 0; i < vAreaItem.length; i++) {
                        if (vAreaItem[i].DICT_CODE == items.POINT_AREA) {
                            return vAreaItem[i].DICT_TEXT;
                        }
                    }
                    return items.POINT_AREA;
                }
                }
                ],
                    g.set("columns", objColumn);
                    break;
                case "EnvMudSea":
                    objColumn = [
                { display: '年度', name: 'YEAR', align: 'left', width: 80 },
                { display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },
                { display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },
                { display: '所属流域', name: 'VALLEY_ID', width: 120, align: 'left', render: function (items) {
                    if (items.VALLEY_ID != "") {
                        return getDictName(items.VALLEY_ID);
                    }
                    return items.VALLEY_ID;
                }
                },
                { display: '所属河流', name: 'RIVER_ID', width: 120, align: 'left', render: function (items) {
                    if (items.RIVER_ID != "") {
                        return getDictName(items.RIVER_ID);
                    }
                    return items.RIVER_ID;
                }
                },
                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' },
                { display: '所在区域', name: 'POINT_AREA', width: 80, minWidth: 60, data: vAreaItem, render: function (items) {
                    for (var i = 0; i < vAreaItem.length; i++) {
                        if (vAreaItem[i].DICT_CODE == items.POINT_AREA) {
                            return vAreaItem[i].DICT_TEXT;
                        }
                    }
                    return items.POINT_AREA;
                }
                }
                ],
                    g.set("columns", objColumn);
                    break;
                case "EnvEstuaries":
                    objColumn = [
                { display: '年度', name: 'YEAR', align: 'left', width: 80 },
                { display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },
                { display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },
                { display: '所属流域', name: 'VALLEY_ID', width: 120, align: 'left', render: function (items) {
                    if (items.VALLEY_ID != "") {
                        return getDictName(items.VALLEY_ID);
                    }
                    return items.VALLEY_ID;
                }
                },
                { display: '所属河流', name: 'RIVER_ID', width: 120, align: 'left', render: function (items) {
                    if (items.RIVER_ID != "") {
                        return getDictName(items.RIVER_ID);
                    }
                    return items.RIVER_ID;
                }
                },
                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' },
                { display: '所在区域', name: 'POINT_AREA', width: 80, minWidth: 60, data: vAreaItem, render: function (items) {
                    for (var i = 0; i < vAreaItem.length; i++) {
                        if (vAreaItem[i].DICT_CODE == items.POINT_AREA) {
                            return vAreaItem[i].DICT_TEXT;
                        }
                    }
                    return items.POINT_AREA;
                }
                }
                ],
                    g.set("columns", objColumn);
                    break;
                default:
                    break;
            }
        }

    }

    g = $("#maingrid4").ligerGrid({
        columns: [
                { display: '年度', name: 'YEAR', align: 'left', width: 80 },
                { display: '代码', name: 'POINT_CODE', width: 120, align: 'left' },
                { display: '名称', name: 'POINT_NAME', width: 120, align: 'left' },
                { display: '监测类别', name: 'ENVTYPE_NAME', width: 120, align: 'left' },
                { display: '所在区域', name: 'POINT_AREA', width: 80, minWidth: 60, data: vAreaItem, render: function (items) {
                    for (var i = 0; i < vAreaItem.length; i++) {
                        if (vAreaItem[i].DICT_CODE == items.POINT_AREA) {
                            return vAreaItem[i].DICT_TEXT;
                        }
                    }
                    return items.POINT_AREA;
                }
                },
                {
                    display: '企业名称', name: 'ENTER_NAME', align: 'left', width: 200//黄进军 add 20150525
                }
                ],
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        dataAction: 'server', //服务器排序
        usePager: false,       //服务器分页
        checkbox: true,
        rownumbers: true
    });
    $("#pageloading").hide();

    GetEnvTypeList();

    //设置grid 的弹出查询对话框
    var detailWinSrh = null;
    function showDetailSrh() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构

            var mainform = $("#SrhForm");
            mainform.ligerForm({
                inputWidth: 170, labelWidth: 90, space: 40,
                fields: [
                     { display: "受检企业", name: "SEA_COMPANYNAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "所在区域", name: "SEA_AREA", newline: false, type: "select", comboboxName: "SEA_AREA_BOX", options: { valueFieldID: "SEA_AREA_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vAreaItem, render: function (item) {
                         for (var i = 0; i < vAreaItem.length; i++) {
                             if (vAreaItem[i]['DICT_CODE'] == item.AREA)
                                 return vAreaItem[i]['DICT_TEXT'];
                         }
                         return item.AREA;
                     }
                     }
                     },
                     { display: "合同号", name: "SEA_CONTRACT_CODE", newline: true, type: "text" },
                    { display: "监测类型", name: "SEA_MONITOR_TYPE", newline: false, type: "select", comboboxName: "SEA_MONITOR_TYPE_BOX", options: { valueFieldID: "SEA_MONITOR_TYPE_OP", valueField: "ID", textField: "MONITOR_TYPE_NAME", data: vMonitorType }, render: function (item) {
                        for (var i = 0; i < vMonitorType.length; i++) {
                            if (vMonitorType[i]['ID'] == item.MONITOR_ID)
                                return vMonitorType[i]['MONITOR_TYPE_NAME'];
                        }
                        return item.MONITOR_ID;
                    }
                    }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 660, height: 170, top: 90, title: "点位查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SEA_COMPANYNAME = encodeURI($("#SEA_COMPANYNAME").val());
            var SEA_AREA_OP = encodeURI($("#SEA_AREA_OP").val());
            var SEA_CONTRACT_CODE = $("#SEA_CONTRACT_CODE").val();
            var SEA_MONITOR_TYPE_OP = $("#SEA_MONITOR_TYPE_OP").val();

            g.set('url', "MonitoringPlan.ashx?action=GetContractPlanPointInfor&strIfPlan=0&strCompanyNameFrim=" + SEA_COMPANYNAME + "&strAreaIdFrim=" + SEA_AREA_OP + "&strMonitorId=" + SEA_MONITOR_TYPE_OP + "&strContractCode=" + SEA_CONTRACT_CODE);
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_COMPANYNAME").val("");
        $("#SEA_CONTRACT_CODE").val("");
        $("#SEA_AREA_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_MONITOR_TYPE_BOX").ligerGetComboBoxManager().setValue("");
    }

});
function f_select() {
    return g.getSelectedRows();
}

function getDictName(strDictID) {
    var Vitem = null;
    var strVal = "";
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MonitoringPlan.ashx?action=GetDictNameForID&strDictID=" + strDictID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != '0') {
                Vitem = data.Rows;
                strVal = Vitem[0].DICT_TEXT;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    return strVal;
}

