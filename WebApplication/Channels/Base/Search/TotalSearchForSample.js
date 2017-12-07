// Create by 邵世卓 2012.11.28  "项目查询"功能
var firstManager;
var secondManager;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var objColumns = null;

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
    var topHeight = $(window).height() / 2;
    var gridHeight = $(window).height() / 2;

    $("#layout1").ligerLayout({ height: '100%', topHeight: topHeight });

    //创建表单结构
    var strContractType = $.getUrlVar('contract_type'); //委托类型
    var strItem_type = $.getUrlVar('item_type'); //委托类型
    if (strItem_type == undefined) {
        strItem_type = '';
    }
    var strTaskID = $.getUrlVar('task_id'); //任务ID
    //委托书grid
    //自送样
    if (strContractType == "04") {
        firstManager = $("#firstgrid").ligerGrid({
            columns: [
         { display: '样品号', name: 'SAMPLE_CODE', width: 120, align: 'left', isSort: false },
         { display: '样品名称', name: 'POINT_NAME', width: 80, align: 'left', isSort: false },
         //{ display: '样品类型', name: 'SAMPLE_TYPE', width: 60, align: 'left', isSort: false },
         { display: '质控类型', name: 'QC_TYPE', width: 60, align: 'left', isSort: false, render: function (data) {
             return getQcType(data.QC_TYPE);
         }
         }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeight,
            title: "样品信息",
            url: '../../Mis/Report/ReportSchedule.aspx?type=getSampleInfo&task_id=' + strTaskID + '&strContractType=' + strContractType + '&item_type=' + strItem_type + "&QC=true",
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            whenRClickToSelect: true,
            onCheckRow: function (checked, rowdata, rowindex) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
                //点击的时候加载点位数据
                secondManager.set('url', "../../Mis/Report/ReportSchedule.aspx?type=getItemInfo&sample_id=" + rowdata.ID + "&qc_type=" + rowdata.QC_TYPE);
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }
        });
    }
    //    //验收类
    //    else if (strContractType == "05") {
    //        $.ligerDialog.warn('验收委托不存在样品信息！');
    //    }
    //常规类
    else {
        objColumns = [
                    { display: '点位', name: 'SAMPLE_NAME', width: 80, align: 'left', isSort: false }, //黄进军修改
                    {display: '样品号', name: 'SAMPLE_CODE', width: 120, align: 'left', isSort: false },
                    //{ display: '样品类型', name: 'SAMPLE_TYPE', width: 60, align: 'left', isSort: false },
                    { display: '质控类型', name: 'QC_TYPE', width: 60, align: 'left', isSort: false, render: function (data) {
                        return getQcType(data.QC_TYPE);
                    }
                    }
        ];

        if (strItem_type == "000000001") {
            var objUnSureColumns = [];
            $.ajax({
                url: "../../Mis/Monitor/sampling/QY/SamplePoint.aspx",
                data: "type=GetAttData&Type_ID=000000017,000000210",   //感官描述属性：000000017  流量测定情况：000000210
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
                    //添加所有动态的列
                    $.each(json.UnSureColumns, function (i, n) {
                        var objStr = n.columnId.split('@');
                        if (objStr[4].toString() == "")
                            objUnSureColumns.push({ display: n.columnName, name: n.columnId, width: 80, minWidth: 60, align: "center", editor: { type: "text"} });
                        else
                            objUnSureColumns.push({ display: n.columnName, name: n.columnId, width: 80, minWidth: 60, align: "center", editor: { type: "select", valueField: "DICT_CODE", textField: "DICT_TEXT", data: DictJson(objStr[4].toString()) },
                                render: function (item, i, v) {
                                    return getDictNames(v, objStr[4].toString());
                                }
                            });
                    });
                    objColumns.push({ display: '采样时间', name: 'SAMPLE_ACCEPT_DATEORACC', width: 60, align: 'left', isSort: false });
                    objColumns.push({ display: '流量测定情况、感官描述', columns: objUnSureColumns });
                    firstManager.set("columns", objColumns);
                }
            });
        }

        firstManager = $("#firstgrid").ligerGrid({
            columns: objColumns,
            width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeight,
            title: "样品信息",
            url: '../../Mis/Report/ReportSchedule.aspx?type=getSampleInfo&task_id=' + strTaskID + '&strContractType=' + strContractType + '&item_type=' + strItem_type + "&QC=true",
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            whenRClickToSelect: true,
            onCheckRow: function (checked, rowdata, rowindex) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);

                //点击的时候加载项目数据
                secondManager.set('url', "../../Mis/Report/ReportSchedule.aspx?type=getItemInfo&sample_id=" + rowdata.ID + "&qc_type=" + rowdata.QC_TYPE);
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }
        });


    }
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //项目信息
    secondManager = $("#secondgrid").ligerGrid({
        columns: [
        { display: '监测项目', name: 'ITEM_NAME', width: 150, align: 'left', isSort: false },
//        { display: '监测结果', name: 'ITEM_RESULT', id: 'ITEM_RESULT', width: 60, align: 'left', isSort: false, render: function (record) {
//            if (record.ITEM_RESULT.indexOf("red") >= 0)
//                return "<span style='color:red'>" + record.ITEM_RESULT.replace("red", "") + "</span>";
//            else
//                return record.ITEM_RESULT;
//        }
//        },
//        { display: '评价标准', name: 'STANDARD_VALUE', width: 60, align: 'left', isSort: false },
        {display: '质控类型', name: 'QC', width: 80, align: 'left', isSort: false, render: function (record) {
            return getQcType(record.QC);
        }
        },
        { display: '分析负责人', name: 'HEAD_USER', width: 80, align: 'left', isSort: false },
        { display: '分析方法', name: 'METHOD_NAME', width: 150, align: 'left', isSort: false },
        { display: '仪器', name: 'APPARATUS_NAME', width: 150, align: 'left', isSort: false },
        { display: '检出限', name: 'RESULT_CHECKOUT', width: 60, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: '100%',
        title: "项目信息",
        toolbar: { items: [
                { text: '原始记录信息', click: SetTable, icon: 'attibutes'}]
        },
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").hide(); //隱藏checkAll
    if (request("step_type") == "QHD") {
        secondManager.toggleCol('ITEM_RESULT', false);
    }
    if (strItem_type == "000000004") {
        firstManager.toggleCol('QC_TYPE', false);
        secondManager.toggleCol('QC', false);
        secondManager.toggleCol('HEAD_USER', false);
    }
});

//获取质控手段
function getQcType(value) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../Mis/Report/ReportSchedule.aspx/getQcType",
        data: "{'strValue':'" + value + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
        }
    });
    return strReturn;
}
//获取质控类型
function getQcType(type) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "TotalSearchForSample.aspx/getQcType",
        data: "{'strValue':'" + type + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
        }
    });
    return strReturn;
}

//获取字典项信息
function getDictNames(strDictCode, strDictType) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../../Mis/Monitor/sampling/QY/SamplePoint.aspx/getDictName",
        data: "{'strDictCode':'" + strDictCode + "','strDictType':'" + strDictType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

function DictJson(DictType) {
    var objReturnValue = null;
    $.ajax({
        url: "../../Mis/Monitor/sampling/QY/SamplePoint.aspx?type=GetDict&dictType=" + DictType,
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

function SetTable() {
    var selectedSample = null;
    var selectedItem = null;
    selectedSample = firstManager.getSelectedRow();
    selectedItem = secondManager.getSelectedRow();

    if (!selectedItem) {
        $.ligerDialog.warn('请先选择监测项目！');
        return;
    }
    
    //结果类型，Poll：污染源 Air：大气
    var strPA = selectedItem.REMARK_5;
    var ItemInfor = getItemInfor(selectedItem.ITEM_ID);
    var strCataLogName = ItemInfor[0].ORI_CATALOG_TABLEID;
    //获取监测项目的监测类型 废气：000000002
    var strMONITORID = ItemInfor[0].MONITOR_ID;
    var strTitle = "原始记录表", strPageUrl = "", strKeyTableName = "", strBaseTableName = "";

    if (strCataLogName != "" || strMONITORID == "000000002") {
        if (ItemInfor[0].ITEM_NAME != "烟气黑度") {
            if (strPA != "Air") {
                //固定污染源原始记录表
                switch (strCataLogName) {
                    //烟尘类的 使用该表作为原始记录主表               
                    case "T_MIS_MONITOR_DUSTATTRIBUTE":
                        if (ItemInfor[0].ITEM_NAME.indexOf("油烟") != -1) {
                            strTitle = "饮食业油烟分析原始记录表";
                            strPageUrl = "../../../Mis/Monitor/sampling/QY/OriginalTable/DustyTable_YY.aspx";
                        }
                        else {
                            strTitle = "固定污染源排气中颗粒物采样分析原始记录表";
                            strPageUrl = "../../../Mis/Monitor/sampling/QY/OriginalTable/DustyTable.aspx";
                        }
                        strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE";
                        strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                        OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                        break;
                    //除了标明原始记录表名的监测项目外其他废气的监测项目也使用该表作为原始记录主表    
                    case "":
                        strTitle = "污染源采样原始记录表";
                        strPageUrl = "../../../Mis/Monitor/sampling/QY/OriginalTable/DustyTable_PM.aspx";
                        strKeyTableName = "";
                        strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                        OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                        break;
                    //PM的和总悬浮物项目类的 使用该表作为原始记录主表               
                    case "T_MIS_MONITOR_DUSTATTRIBUTE_PM":
                        strTitle = "污染源采样原始记录表";
                        strPageUrl = "../../../Mis/Monitor/sampling/QY/OriginalTable/DustyTable_PM.aspx";
                        strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE_PM";
                        strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                        OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
                        break;
                    //SO2和NOX类的 使用该表作为原始记录主表               
                    case "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX":
                        var strID, strItemID;
                        var strSO2 = "99999", strNOX = "99999";
                        strID = selectedItem.ID;
                        strItemID = selectedItem.ITEM_ID;

                        for (var i = 0; i < secondManager.rows.length; i++) {
                            if (secondManager.rows[i].ITEM_NAME == "二氧化硫")
                                strSO2 = secondManager.rows[i].ID;
                            if (secondManager.rows[i].ITEM_NAME == "氮氧化物")
                                strNOX = secondManager.rows[i].ID;
                        }

                        if (selectedItem.ITEM_NAME == '二氧化硫' || selectedItem.ITEM_NAME == '氮氧化物') {
                            var obj = getDustInfor(strSO2, strNOX);
                            if (obj != null && obj.length > 0) {
                                strID = obj[0].SUBTASK_ID;
                                strItemID = obj[0].ITEM_ID;
                            }
                        }

                        strTitle = "固定污染源排气中气态污染物采样分析原始记录表";
                        strPageUrl = "../../../Mis/Monitor/sampling/QY/OriginalTable/DustyTable_So2OrNox.aspx";
                        strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX";
                        strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                        OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, strItemID, strID);
                        break;
                    default:
                        break;
                }
            }
            else {

                strTitle = "大气采样原始记录表";
                strPageUrl = "../../../Mis/Monitor/sampling/QY/OriginalTable/DustyTable_Air.aspx";
                strKeyTableName = strCataLogName;
                strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID);
            }
        }
        else {
            parent.$.ligerDialog.open({ Title: "烟气黑度类", top: 100, width: 800, height: 275, buttons:
                         [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); }
                         }], url: '../../../Mis/Monitor/sampling/QY/OriginalTable/DustyTable_YH.aspx?strID=' + selectedSample.POINT_ID
            });
        }
    } else {
    
        return;
    }

}
function OpenDialog(Title, PageUrl, KeyTable, BaseTable, ItemID, SubTaskID) {
    
    parent.$.ligerDialog.open({ Title: Title, top: 0, width: 1100, height: 680, buttons:
         [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: PageUrl + '?strSubTask_Id=' + SubTaskID + '&strIsView=true&strItem_Id=' + ItemID + '&strKeyTableName=' + KeyTable + '&strBaseTableName=' + BaseTable 
    });
}

//获取监测项目信息
function getItemInfor(strItemID) {
    var strValue = null;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../../Mis/Monitor/sampling/QY/SamplePoint.aspx?type=getBaseItemInfor&strItemId=" + strItemID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.Total != '0') {
                strValue = data.Rows;
            }
        }
    });
    return strValue;
}

function getDustInfor(strSO2, strNOX) {
    var strValue = null;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../../Mis/Monitor/sampling/QY/SamplePoint.aspx?type=getDustInfor&strSO2=" + strSO2 + "&strNOX=" + strNOX,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.Total != '0') {
                strValue = data.Rows;
            }
        }
    });
    return strValue;
}