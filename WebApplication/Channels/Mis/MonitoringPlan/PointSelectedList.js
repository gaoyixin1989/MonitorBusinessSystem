var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var objGrid = null;
var vContractData = null, vMonitorType = null, vAreaItem = null, vContratTypeItem = null, vTypeItem = null, vTypeNameItem = null, vTypeArry = null;
var strEvnTypeId = "", strEvnTypeName = "", strEvnYear = "", strEnvMonth = "", strEvnPointId = "", strPointNames = "", strPlanId = ""; strPointItems = "", strPointItemsName = ""; strProjectName = "";
var objGrid = null;
var strKeyColumns = "", strTableName = "";
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
    //alert(strEvnTypeId + "123456");
    strEvnTypeName = decodeURI($.getUrlVar('strEvnTypeName'));
    strEvnYear = $.getUrlVar('strEvnYear');
    strEnvMonth = $.getUrlVar('strEnvMonth');
    strEvnPointId = $.getUrlVar('strEvnPointId');
    strPlanId = $.getUrlVar('strPlanId');
    // add huangjinjun 20150108 污染源常规
    if (strEvnTypeId == 'EnvPollute' || strEvnTypeId == 'SewagePlant' || strEvnTypeId == 'StateControlledWastewater' || strEvnTypeId == 'StateControlledGas' || strEvnTypeId == 'HeavyMetal' || strEvnTypeId == 'Control' || strEvnTypeId == 'SensitiveGroundwater' || strEvnTypeId == 'SewageCharges') {
        objGrid = $("#gridItems").ligerGrid({
            columns: [
                    { display: '垂线/监测点', name: 'POINT_NAME', align: 'left', width: 200 },
                    { display: '环境类别', name: 'EnvType', width: 160, minWidth: 100, render: function (items) {
                        return strEvnTypeName;
                    }
                    },
                    {
                        display: '企业名称', name: 'ENTER_NAME', align: 'left', width: 200
                    }
                ],
            width: '100%',
            height: '98%',
            pageSizeOptions: [5, 10, 15, 20],
            pageSize: 10,
            dataAction: 'server', //服务器排序
            usePager: false,       //服务器分页
            rownumbers: true,
            toolbar: { items: [
                { id: 'save', text: '选择点位', click: f_selectList, icon: 'bluebook' }
                ]
            }
        });
    } else {
        objGrid = $("#gridItems").ligerGrid({
            columns: [
                    { display: '垂线/监测点', name: 'POINT_NAME', align: 'left', width: 200 },
                    { display: '环境类别', name: 'EnvType', width: 160, minWidth: 100, render: function (items) {
                        return strEvnTypeName;
                    }
                    }
                ],
            width: '100%',
            height: '98%',
            pageSizeOptions: [5, 10, 15, 20],
            pageSize: 10,
            dataAction: 'server', //服务器排序
            usePager: false,       //服务器分页
            rownumbers: true,
            toolbar: { items: [
                { id: 'save', text: '选择点位', click: f_selectList, icon: 'bluebook' }
                ]
            }
        });
    }

    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    LoadEvnGridData();

    //    function f_SaveData() {
    //        var rowSelected = objGrid.getSelectedRows();
    //        if (rowSelected.length < 1) {
    //            $.ligerDialog.warn("请选择行！");
    //            return;
    //        } else {
    //            strPointItems = "", strPointItemsName = "";
    //            for (var i = 0; i < rowSelected.length; i++) {
    //                strPointItems += rowSelected[i].ID + ";";
    //                strPointItemsName += rowSelected[i].POINT_NAME + ";";
    //            }

    //            strPointItems = strPointItems.substring(0, strPointItems.length - 1);
    //            strPointItemsName = strPointItemsName.substring(0, strPointItemsName.length - 1);
    //            SavePointItems();
    //        }
    //    }
    //    function SavePointItems() {
    //        $.ajax({
    //            cache: false,
    //            async: false, //设置是否为异步加载,此处必须
    //            type: "POST",
    //            url: "MonitoringPlan.ashx?action=InsertEnvContractPoint&strPlanId=" + strPlanId + "&strEvnTypeId=" + strEvnTypeId + "&strPointItem=" + strPointItems + "&strPointItemName=" + encodeURI(strPointItemsName) + "&strProjectName=" + encodeURI(strProjectName) + "&strTableName=" + strTableName + "&strKeyColumns=" + strKeyColumns,
    //            contentType: "application/text; charset=utf-8",
    //            dataType: "text",
    //            success: function (data) {
    //                if (data == "True") {
    //                    objGrid.loadData();
    //                    $.ligerDialog.success('数据保存成功！');
    //                    return;
    //                }
    //            },
    //            error: function (msg) {
    //                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
    //                return;
    //            }
    //        });
    //    }
    function f_selectList() {

        if (strEvnTypeId == "") {
            objGrid.loadData();
            $.ligerDialog.warn('请选择环境质量类别!'); return;
        } else {
            parent.$.ligerDialog.open({ title: '点位选择', name: 'winselector', width: 700, height: 500, top: 10, url: '../../Mis/MonitoringPlan/SelectList.aspx?strEvnTypeId=' + strEvnTypeId + '&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strEnvTypeName=' + strEvnTypeName, buttons: [
                { text: '确定', onclick: f_selectListOK },
                { text: '返回', onclick: f_selectListCancel }
            ]
            });
            return false;
        }
    }

    function f_selectListOK(item, dialog) {
        var fn = dialog.frame.f_select || dialog.frame.window.f_select;
        var data = fn();
        if (!data) {
            $.ligerDialog.warn('请选择行!');
            return;
        }
        var strPointNames = "";
        strEvnPointId = "";
        for (var i = 0; i < data.length; i++) {
            strPointNames += data[i].POINT_NAME + ",";
            strEvnPointId += data[i].ID + ",";
        }
        if (strEvnPointId != "" && strPointNames != "") {
            LoadEvnGridData();
        }
        dialog.close();
    }

    function f_selectListCancel(item, dialog) {
        dialog.close();
    }

    function LoadEvnGridData() {

        if (strEvnTypeId != "") {
            //alert(strEvnTypeId + "999asdfg");
            //alert(strEvnPointId);
            switch (strEvnTypeId) {
                case "SewageCharges": //污染源常规（排污收费企业） add 黄进军 20151217
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_POLLUTE&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_POLLUTE_ITEM";
                    break;
                case "SewagePlant": //污染源常规（污水厂企业） add 黄进军 20150525 
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_POLLUTE&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_POLLUTE_ITEM";
                    break;
                case "StateControlledWastewater": //重点企业（废水） add 黄进军 20150525 
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_POLLUTE&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_POLLUTE_ITEM";
                    break;
                case "StateControlledGas": //重点企业（废气） add 黄进军 20150525 
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_POLLUTE&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_POLLUTE_ITEM";
                    break;
                case "HeavyMetal": //重金属企业 add 黄进军 20150525 
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_POLLUTE&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_POLLUTE_ITEM";
                    break;
//                case "EnvRiver": //地表水 Modify By 黄进军 2015-09-17 根据新数据结构修改
//                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_SEDIMENT&strGridItemTable=T_ENV_P_SEDIMENT_ITEM&strGridItemKeyColumn=POINT_ID&strGridFatherKeyColumn=SECTION_NAME&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
//                    //点位关键字段  
//                    strKeyColumns = "POINT_ID";
//                    //获取点位表   
//                    strTableName = "T_ENV_P_SEDIMENT"; //huangjinjun update 20150917
//                    break;
                case "EnvRiver": //地表水 add By 黄进军 2015-09-17 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_SEDIMENT&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_SEDIMENT_ITEM";
                    break;
                case "EnvReservoir": //湖库 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvnForCX&strFatherKeyColumn=SECTION_ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=VERTICAL_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_LAKE_V&strGridFatherTable=T_ENV_P_LAKE&strGridItemTable=T_ENV_P_LAKE_V_ITEM&strGridItemKeyColumn=POINT_ID&strGridFatherKeyColumn=SECTION_NAME&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_LAKE_V_ITEM";
                    break;
                case "EnvStbc": //生态补偿
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_PAYFOR&strGridItemTable=T_ENV_P_PAYFOR_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_PAYFOR_ITEM";
                    break;
                case "EnvDrinking": //地下饮用水 Modify By 胡方扬 2013-06-13 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_DRINK_UNDER&strGridItemTable=T_ENV_P_DRINK_UNDER_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_DRINK_UNDER_ITEM";
                    break;
                case "EnvDrinkingSource": //饮用水源地（河流、湖库)   Modify By 胡方扬 2013-06-13 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvnForCX&strFatherKeyColumn=SECTION_ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=VERTICAL_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_DRINK_SRC_V&strGridFatherTable=T_ENV_P_DRINK_SRC&strGridItemTable=T_ENV_P_DRINK_SRC_V_ITEM&strGridItemKeyColumn=POINT_ID&strGridFatherKeyColumn=SECTION_NAME&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    //点位关键字段   
                    strKeyColumns = "POINT_ID";
                    //获取点位表   
                    strTableName = "T_ENV_P_DRINK_SRC_V_ITEM";
                    break;
                case "EnvDWWater": //双三十水  Modify By 胡方扬 2013-06-13 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvnForCX&strFatherKeyColumn=SECTION_ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=VERTICAL_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_RIVER30_V&strGridFatherTable=T_ENV_P_RIVER30&strGridItemTable=T_ENV_P_RIVER30_V_ITEM&strGridItemKeyColumn=POINT_ID&strGridFatherKeyColumn=SECTION_NAME&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    //点位关键字段   
                    strKeyColumns = "POINT_ID";
                    //获取点位表   
                    strTableName = "T_ENV_P_RIVER30_V_ITEM";
                    break;
                case "EnvMudRiver": //沉积物（河流） Modify By 胡方扬 2013-06-14 根据新数据结构修改  新
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvnForCX&strFatherKeyColumn=SECTION_ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=VERTICAL_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_MUD_RIVER_V&strGridFatherTable=T_ENV_P_MUD_RIVER&strGridItemTable=T_ENV_P_MUD_RIVER_V_ITEM&strGridItemKeyColumn=POINT_ID&strGridFatherKeyColumn=SECTION_NAME&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    //点位关键字段   
                    strKeyColumns = "POINT_ID";
                    //获取点位表   
                    strTableName = "T_ENV_P_MUD_RIVER_V_ITEM";
                    break;
                case "EnvMudSea": //沉积物（海水） Modify By 胡方扬 2013-06-14 根据新数据结构修改 新
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvnForCX&strFatherKeyColumn=SECTION_ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=VERTICAL_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_MUD_SEA_V&strGridFatherTable=T_ENV_P_MUD_SEA&strGridItemTable=T_ENV_P_MUD_SEA_V_ITEM&strGridItemKeyColumn=POINT_ID&strGridFatherKeyColumn=SECTION_NAME&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    //点位关键字段   
                    strKeyColumns = "POINT_ID";
                    //获取点位表   
                    strTableName = "T_ENV_P_MUD_SEA_V_ITEM";
                    break;
                case "EnvSoil": //土壤 Modify By 胡方扬 2013-06-14 根据新数据结构修改 新
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_SOIL&strGridItemTable=T_ENV_P_SOIL_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_SOIL_ITEM";
                    break;
                case "EnvPSoild": //固废 Modify By 胡方扬 2013-06-14 根据新数据结构修改 新
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_SOLID&strGridItemTable=T_ENV_P_SOLID_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_SOLID_ITEM";
                    break;
                case "EnvEstuaries": //入海河口 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvnForCX&strFatherKeyColumn=SECTION_ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=VERTICAL_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_ESTUARIES_V&strGridFatherTable=T_ENV_P_ESTUARIES&strGridItemTable=T_ENV_P_ESTUARIES_V_ITEM&strGridItemKeyColumn=POINT_ID&strGridFatherKeyColumn=SECTION_NAME&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    //点位关键字段   
                    strKeyColumns = "POINT_ID";
                    //获取点位表   
                    strTableName = "T_ENV_P_ESTUARIES_V_ITEM";
                    break;
                case "EnvSear": //近岸海域 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_SEA&strGridItemTable=T_ENV_P_SEA_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_SEA_ITEM";
                    break;
                case "EnvSource": //近岸直排 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_OFFSHORE&strGridItemTable=T_ENV_P_OFFSHORE_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_OFFSHORE_ITEM";
                    break;
                case "EnvSeaBath": //海水浴场 Modify By 胡方扬 2013-06-14 根据新数据结构修改 新
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_SEABATH&strGridItemTable=T_ENV_P_SEABATH_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_SEABATH_ITEM";
                    break;
                case "EnvDust": //地下水 Modify By 黄进军 2015-09-17 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_DUST&strGridItemTable=T_ENV_P_DUST_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_DUST_ITEM";
                    break;
                case "EnvRain": //降水 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_RAIN&strGridItemTable=T_ENV_P_RAIN_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_RAIN_ITEM";
                    break;
                /*======================================噪声 Start========================================*/ 
                case "EnvRoadNoise": //道路交通噪声 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_NOISE_ROAD&strGridItemTable=T_ENV_P_NOISE_ROAD_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_NOISE_ROAD_ITEM";
                    break;
                case "FunctionNoise": //功能区噪声 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_NOISE_FUNCTION&strGridItemTable=T_ENV_P_NOISE_FUNCTION_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_NOISE_FUNCTION_ITEM";
                    break;
                case "AreaNoise": //区域环境噪声  Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_NOISE_AREA&strGridItemTable=T_ENV_P_NOISE_AREA_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_NOISE_AREA_ITEM";
                    break;
                /*=======================================噪声 End=======================================*/ 
                /*======================================环境空气 Start========================================*/ 
                case "EnvAir": //环境空气 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_AIR&strGridItemTable=T_ENV_P_AIR_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_AIR_ITEM";
                    break;
                case "EnvSpeed": //硫酸盐化速率 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_ALKALI&strGridItemTable=T_ENV_P_ALKALI_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_ALKALI_ITEM";
                    break;
                case "EnvDWAir": //双三十废气 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strEvnPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strEvnTypeName) + '&strTableName=T_ENV_P_AIR30&strGridItemTable=T_ENV_P_AIR30_ITEM&strGridItemKeyColumn=POINT_ID&strEnvYear=' + strEvnYear + '&strEnvMonth=' + strEnvMonth + '&strPlanId=' + strPlanId);
                    strKeyColumns = "POINT_ID";
                    strTableName = "T_ENV_P_AIR30_ITEM";
                    break;
                /*======================================环境空气 End========================================*/ 
                default:
                    break;
            }
        }
    }
});

function GetDataRow() {
    var strParme = "";

    var rowdata = objGrid.data.Rows;
    if (rowdata && rowdata.length > 0) {
        strParme = "1";
    }
    return strParme;
}


function GetTableName() {
    return strTableName;
}

function GetKeyName() {
    return strKeyColumns;
}

function GetDataRowPoint() {
    var vPointArr = [];
    var rowSelected = objGrid.data.Rows;
    if (rowSelected && rowSelected.length > 0) {
        var tempArr = new Array();

        tempArr.length = rowSelected.length;
        for (var i = 0; i < tempArr.length; i++) {
            var arr = new Array(); //声明数组，用来存储信息
            arr.ID = rowSelected[i].ID;
            arr.POINT_NAME = rowSelected[i].POINT_NAME;
            arr.PLAN_ID = strPlanId;
            arr.MONITOR_ID = strEvnTypeId;
            arr.MONITOR_NAME = strEvnTypeName;
            arr.PROJECT_NAME = strProjectName;
            arr.TABLE_NAME = strTableName;
            arr.KEY_COLUMN = strKeyColumns;

            //何海亮添加 update huangjinjun 20150108
            if (strEvnTypeId == 'EnvPollute' || strEvnTypeId == 'SewagePlant' || strEvnTypeId == 'StateControlledWastewater' || strEvnTypeId == 'StateControlledGas' || strEvnTypeId == 'HeavyMetal' || strEvnTypeId == 'Control' || strEvnTypeId == 'SensitiveGroundwater' || strEvnTypeId == 'SewageCharges') {
                arr.ENTER_NAME = rowSelected[i].ENTER_NAME;
                arr.PID = rowSelected[i].PID;
            }

            tempArr[i] = arr;
        }
        vPointArr = [];
        vPointArr = tempArr;
    }

    return vPointArr;
}

