var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var objGrid = null;
var vContractData = null, vMonitorType = null, vAreaItem = null, vContratTypeItem = null, vTypeItem = null, vTypeNameItem = null, vTypeArry = null;
var strEvnTypeId = "", strEvnTypeName = "", strEvnYear = "", strEnvMonth = "", strEvnPointId = "", 
strPointNames = "", strPlanId = ""; strPointItems = "", strPointItemsName = "";strProjectName = "",strQcType="", strPointQcSettingId = "";
var objGrid = null;
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
    strEvnTypeName = decodeURI($.getUrlVar('strEvnTypeName'));
    strEvnYear = $.getUrlVar('strEnvYear');
    strEvnPointId = $.getUrlVar('strEvnPointId');
    strPlanId = $.getUrlVar('strPlanId');
    strProjectName = $.getUrlVar('strProjectName');

    strEvnYear = new Date().getFullYear();
    strEnvMonth = new Date().getMonth() + 1;

    objGrid = $("#gridItems").ligerGrid({
        columns: [
                    { display: '垂线/监测点', name: 'POINT_NAME', align: 'left', width: 200 },
                    { display: '年度', name: 'YEAR', align: 'center', width: 80 },
                    { display: '月度', name: 'MONTH', align: 'center', width: 80 },
                    { display: '环境类别', name: 'EnvType', width: 160, minWidth: 100, render: function (items) {
                        return strEvnTypeName;
                    }
                    },
                { display: '现场空白项目', name: 'XC_ITEMS', align: 'left', width: 200, render: function (items) {
                    var strPxItems = getItemList(items.ID, '1');
                    if (strPxItems.length > 20) {
                        return "<a title='" + strPxItems + "'>" + strPxItems.substring(0, 20) + "...</a>"
                    }
                    return strPxItems;
                }
                },
                    { display: '现场平行项目', name: 'KB_ITEMS', align: 'left', width: 200, render: function (items) {
                        var strKbItems = getItemList(items.ID, '3');
                        if (strKbItems.length > 20) {
                            return "<a title='" + strKbItems + "'>" + strKbItems.substring(0, 20) + "...</a>"
                        }
                        return strKbItems;
                    }
                    }
                ],
        width: '100%',
        height: '99%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        toolbar: { items: [
                { id: 'save', text: '选择点位', click: f_selectList, icon: 'bluebook' },
                { line: true },
                { id: 'del', text: '删除点位', click: f_delete, icon: 'delete' },
                { line: true },
                { id: 'kb_item', text: '现场空白项目', click: f_kbTable, icon: 'bluebook' },
                { line: true },
                { id: 'px_item', text: '现场平行项目', click: f_pxTable, icon: 'bluebook' },
                { line: true }
                ]
        },
        checkbox: true,
        onDblClickRow: function (data, rowindex, rowobj) {
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }

    });
    $("#pageloading").hide();

    objGrid.set('url', 'MonitoringPlan.ashx?action=GetEnvQCSettingPointList&strMonitorId=' + strEvnTypeId + '&strYear=' + strEvnYear + '&strMonth=' + strEnvMonth);



    function getItemList(strQcSettingId, strQcTypeId) {
        var ItemName = "";
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=GetEnvPointItemsForQcSetting&strPointQcSetting_Id=" + strQcSettingId + "&strQcStatus=" + strQcTypeId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != "0") {
                    for (var i = 0; i < data.Rows.length; i++) {
                        ItemName += data.Rows[i].ITEM_NAME + ";";
                    }
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
        if (ItemName != "") {
            ItemName = ItemName.substring(0, ItemName.length - 1);
        }
        return ItemName;
    }



    function SaveQcSettingPoint(_strMonitorId, _strPointId, _strPointName, _strProjectName) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=InsertQcSettingPoint&strMonitorId=" + _strMonitorId + "&strPointId=" + _strPointId + "&strPoint_Name=" + encodeURI(_strPointName) + "&strProjectName=" + encodeURI(_strProjectName) + "&strYear=" + strEvnYear + "&strMonth=" + strEnvMonth,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data == "True") {
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetEnvQCSettingPointList&strMonitorId=' + strEvnTypeId + '&strYear=' + strEvnYear + '&strMonth=' + strEnvMonth);
                }
                else {
                    objGrid.set('url', 'MonitoringPlan.ashx?action=GetEnvQCSettingPointList&strMonitorId=' + strEvnTypeId + '&strYear=' + strEvnYear + '&strMonth=' + strEnvMonth);
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                return;
            }
        });
    }

    function f_delete() {
        var rowSelect = null, grid = null;
        rowSelect = objGrid.getSelectedRow()
        grid = objGrid;

        if (rowSelect != null) {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: "MonitoringPlan.ashx?action=DelPointQcSetting&strPointQcSetting_Id=" + rowSelect.ID,
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    if (data == "True") {
                        objGrid.loadData();
                        $.ligerDialog.success('数据操作成功！');
                    }
                    else {
                        $.ligerDialog.warn('数据操作失败！');
                    }
                },
                error: function (msg) {
                    $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                }
            });
        }
        else {
            $.ligerDialog.warn('请选择一行！');
            return;
        }
    }
    function f_selectList() {
        if (strEvnTypeId == "") {
            objGrid.loadData();
            $.ligerDialog.warn('请选择环境质量类别!'); return;
        } else {
            parent.$.ligerDialog.open({ title: '点位选择', name: 'winselector', width: 700, height: 500, top: 10, url: 'SelectQcSettingList.aspx?strEvnTypeId=' + strEvnTypeId + '&strEnvYear=' + strEvnYear + '&strEnvTypeName=' + strEvnTypeName, buttons: [
                { text: '确定', onclick: f_selectListOK },
                { text: '返回', onclick: f_selectListCancel }
            ]
            });
            return false;
        }
    }

    function f_kbTable() {
        var objRows = objGrid.getSelectedRow();
        if (objRows == null) {
            $.ligerDialog.warn('请先选择一行!');
            return;
        } else {
            strPointQcSettingId = objRows.ID;
            strQcType = '1';
            parent.$.ligerDialog.open({ title: '现场空白项目设置', name: 'winselector_XC', width: 480, height: 380, top: 20, url: 'SelectPointItems.aspx?strEvnTypeId=' + objRows.MONITOR_ID + '&strPointId=' + objRows.POINT_ID + '&strQCType=' + strQcType + '&strPointQcSetting_Id=' + objRows.ID, buttons: [
                { text: '确定', onclick: f_selectListSetting },
                { text: '返回', onclick: f_selectListSettingCancel }
            ]
            });
            return false;
        }
    }

    function f_pxTable() {
        var objRows = objGrid.getSelectedRow();
        if (objRows==null) {
            $.ligerDialog.warn('请先选择一行!');
            return;
        } else {
            strPointQcSettingId = objRows.ID;
            strQcType = '3';
            parent.$.ligerDialog.open({ title: '现场平行项目设置', name: 'winselector_PX', width: 480, height: 380, top: 20, url: 'SelectPointItems.aspx?strEvnTypeId=' + objRows.MONITOR_ID + '&strPointId=' + objRows.POINT_ID + '&strQCType=' + strQcType + '&strPointQcSetting_Id=' + objRows.ID, buttons: [
                { text: '确定', onclick: f_selectListSetting },
                { text: '返回', onclick: f_selectListSettingCancel }
            ]
            });
            return false;
        }
    }

    function f_selectListSetting(item, dialog) {
        var fn = dialog.frame.GetSelectItems || dialog.frame.window.GetSelectItems;
        var data = fn();
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=InsertQcSettingPointItems&strPointQcSetting_Id=" + strPointQcSettingId + "&strPoint_Name=" + strPointQcSettingId + "&strQcStatus=" + strQcType + data,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data == "True") {
                    dialog.close();
                    objGrid.loadData();
                    $.ligerDialog.success('数据保存成功！');
                    return;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                return;
            }
        });

    }

    function f_selectListSettingCancel(item, dialog) {
        dialog.close();
    }

    function f_selectListOK(item, dialog) {
        var fn = dialog.frame.f_select || dialog.frame.window.f_select;
        var data = fn();
        if (!data) {
            $.ligerDialog.warn('请选择行!');
            return;
        }
        strPointNames = "", strEvnPointId = "";
        for (var i = 0; i < data.length; i++) {
            GetEvnGridData(data[i].ID, data[i].ENVTYPE_NAME);
            //            strPointNames += data[i].POINT_NAME + ",";
            //            strEvnPointId += data[i].ID + ",";
            //        }
            if (strEvnPointId != "" && strPointNames != "") {
                strEvnPointId = strEvnPointId.substring(0, strEvnPointId.length - 1);
                strPointNames = strPointNames.substring(0, strPointNames.length - 1);
                SaveQcSettingPoint(strEvnTypeId, strEvnPointId, strPointNames, strProjectName);
                //LoadEvnGridData();
            }
        }
        dialog.close();
    }

    function f_selectListCancel(item, dialog) {
        dialog.close();
    }


    function GetEvnGridData(strMumiPointId, strMumiPointName) {
        var url = "";
        if (strEvnTypeId != "" && strMumiPointId) {
            switch (strEvnTypeId) {
                case "EnvRiver": //河流 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvnForCX&strFatherKeyColumn=SECTION_ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=VERTICAL_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_RIVER_V&strGridFatherTable=T_ENV_P_RIVER&strGridFatherKeyColumn=SECTION_NAME';
                    break;
                case "EnvReservoir": //湖库 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvnForCX&strFatherKeyColumn=SECTION_ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=VERTICAL_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_LAKE_V&strGridFatherTable=T_ENV_P_LAKE&strGridFatherKeyColumn=SECTION_NAME';
                    break;
                case "EnvStbc": //生态补偿
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_PAYFOR';
                    break;
                case "EnvDrinking": //地下饮用水 Modify By 胡方扬 2013-06-13 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_DRINK_UNDER';
                    break;
                case "EnvDrinkingSource": //饮用水源地（河流、湖库)   Modify By 胡方扬 2013-06-13 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvnForCX&strFatherKeyColumn=SECTION_ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=VERTICAL_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_DRINK_SRC_V&strGridFatherTable=T_ENV_P_DRINK_SRC&strGridFatherKeyColumn=SECTION_NAME';
                    break;
                case "EnvDWWater": //双三十水  Modify By 胡方扬 2013-06-13 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvnForCX&strFatherKeyColumn=SECTION_ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=VERTICAL_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_RIVER30_V&strGridFatherTable=T_ENV_P_RIVER30&strGridFatherKeyColumn=SECTION_NAME';
                    break;
                case "EnvMudRiver": //沉积物（河流） Modify By 胡方扬 2013-06-14 根据新数据结构修改  新
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvnForCX&strFatherKeyColumn=SECTION_ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=VERTICAL_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_MUD_RIVER_V&strGridFatherTable=T_ENV_P_MUD_RIVER&strGridFatherKeyColumn=SECTION_NAME';
                    break;
                case "EnvMudSea": //沉积物（海水） Modify By 胡方扬 2013-06-14 根据新数据结构修改 新
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvnForCX&strFatherKeyColumn=SECTION_ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=VERTICAL_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_MUD_SEA_V&strGridFatherTable=T_ENV_P_MUD_SEA&strGridFatherKeyColumn=SECTION_NAME';
                    break;
                case "EnvSoil": //土壤 Modify By 胡方扬 2013-06-14 根据新数据结构修改 新
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_SOIL';
                    break;
                case "EnvPSoild": //固废 Modify By 胡方扬 2013-06-14 根据新数据结构修改 新
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_SOLID';

                    break;
                case "EnvEstuaries": //入海河口 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvnForCX&strFatherKeyColumn=SECTION_ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=VERTICAL_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_ESTUARIES_V&strGridFatherTable=T_ENV_P_ESTUARIES&strGridFatherKeyColumn=SECTION_NAME';
                    break;
                case "EnvSear": //近岸海域 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_SEA';
                    break;
                case "EnvSource": //近岸直排 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_OFFSHORE';
                    break;
                case "EnvSeaBath": //海水浴场 Modify By 胡方扬 2013-06-14 根据新数据结构修改 新
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_SEABATH';
                    break;
                case "EnvDust": //降尘 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_DUST';
                    break;
                case "EnvRain": //降水 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_RAIN';
                    break;
                /*======================================噪声 Start========================================*/ 
                case "EnvRoadNoise": //道路交通噪声 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_NOISE_ROAD';
                    break;
                case "FunctionNoise": //功能区噪声 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_NOISE_FUNCTION';
                    break;
                case "AreaNoise": //区域环境噪声  Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_NOISE_AREA';

                    break;
                /*=======================================噪声 End=======================================*/ 
                /*======================================环境空气 Start========================================*/ 
                case "EnvAir": //环境空气 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_AIR';

                    break;
                case "EnvSpeed": //硫酸盐化速率 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_ALKALI';

                    break;
                case "EnvDWAir": //双三十废气 Modify By 胡方扬 2013-06-14 根据新数据结构修改
                    url = 'MonitoringPlan.ashx?action=GetPlanPointForEvn&strFatherKeyColumn=ID&strFatherKeyValue=' + strMumiPointId + '&SubKeyColumn=POINT_NAME&strEnvTypeId=' + strEvnTypeId + '&strEnvTypeName=' + encodeURI(strMumiPointName) + '&strTableName=T_ENV_P_AIR30';
                    break;
                /*======================================环境空气 End========================================*/ 
                default:
                    break;
            }

            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: url,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.Total != "0") {
                        for (var i = 0; i < data.Rows.length; i++) {
                            strPointNames += data.Rows[i].POINT_NAME + ",";
                            strEvnPointId += data.Rows[i].ID + ",";
                        }
                    }
                },
                error: function (msg) {
                    $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                    return;
                }
            });
        }
    }
});