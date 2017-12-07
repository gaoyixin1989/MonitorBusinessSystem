// Create by 苏成斌 2012.12.14  "采样-点位信息管理的监测项目列表"功能

var strItemId = "";
var ItemGrid;
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

//点位信息管理的监测项目管理功能
$(document).ready(function () {
    //分析方法grid
    ItemGrid = $("#ItemGrid").ligerGrid({
        columns: [
            { display: '监测点', name: 'Point_Name', width: 140, align: 'left', isSort: false, render: function (record) {
                return getPointName(record.TASK_POINT_ID);
            }
        },
//            { display: '样品记录单', name: 'DATAINFO', align: 'left', width: 100, minWidth: 40,
//                render: function (record, rowindex, value) {
//                    return "<a href=\"javascript:EditOriginalInfo()\">编辑</a> ";
//                }
//            },
            { display: '监测项目', name: 'ITEM_NAME', width: 100, align: 'left', isSort: false, render: function (record) {
                return getItemName(record.ITEM_ID);
            }
            },
            
       { display: '分析现场项目', name: 'IS_ANYSCENE_ITEM', width: 100, align: 'center', isSort: false, render: function (record) {
           var strItem = getItemInfor(record.ITEM_ID);
           if (strItem != null) {
               if (strItem[0].IS_ANYSCENE_ITEM == "1") {

                   return "<a style='color:Red'>是</a>";
               } else {
                   return "否";
               }
           }
           return "";
       }
       }
        ], width: '100%', pageSizeOptions: [5, 8, 10], height: '96%',
        url: 'SamplePoint.aspx?Action=GetItems',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        //checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'set', text: '监测项目设置', click: SetData_Item, icon: 'add' },
                { line: true },
                { id: 'copy', text: '复制', click: copyData_Item, icon: 'page_copy' },
                { line: true },
                { id: 'past', text: '粘贴', click: pastData_Item, icon: 'page_paste' }
                //{ line: true },
                //{ id: 'setPoll', text: '污染源原始记录信息', click: SetTable, icon: 'attibutes' },
                //{ line: true },
                //{ id: 'setAir', text: '大气原始记录信息', click: SetTable, icon: 'attibutes' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        } //,
        //        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
       $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    var strCopyPointID = "";
    //复制监测项目
    function copyData_Item() {
        var selectedPoint = objPointGrid.getSelectedRow();
        if (!selectedPoint) {
            $.ligerDialog.warn('请先选择要复制的监测点位！');
            return;
        }

        strCopyPointID = selectedPoint.ID;
    }

    //粘贴监测项目
    function pastData_Item() {
        var selectedPoint = objPointGrid.getSelectedRow();
        if (!selectedPoint) {
            $.ligerDialog.warn('请先选择要粘帖的监测点位！');
            return;
        }

        if (strCopyPointID == "") {
            $.ligerDialog.warn('请先点击复制按钮复制监测点位！');
            return;
        }

        if (selectedPoint.ID == strCopyPointID) {
            return;
        }

        $.ajax({
            cache: false,
            type: "POST",
            url: "SamplePoint.aspx/CopyPointItem",
            data: "{'strCopyID':'" + strCopyPointID + "','strPastID':'" + selectedPoint.ID + "','strSubtaskID':'" + strSubtaskID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    ItemGrid.set('url', "SamplePoint.aspx?type=GetItems&selPointID=" + selectedPoint.ID);
                    $.ligerDialog.success('复制监测项目成功！')
                }
                else {
                    $.ligerDialog.warn('复制监测项目失败！');
                }
            }
        });
    }

    //设置监测项目
    function SetData_Item() {
        var selectedPoint = objPointGrid.getSelectedRow();
        if (!selectedPoint) {
            $.ligerDialog.warn('请先选择监测点位！');
            return;
        }

        parent.$.ligerDialog.open({ title: '设置监测项目', top: 0, width: 500, height: 380, buttons:
        [{ text: '确定', onclick: f_SaveDateItem },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../../Mis/Monitor/sampling/SamplePointItemEdit.aspx?PointID=' + selectedPoint.ID + '&SubtaskID=' + selectedPoint.SUBTASK_ID
        });
    }

    //生成原始记录表
    function SetTable(type) {

        var selectedPoint = objPointGrid.getSelectedRow();
        if (!selectedPoint) {
            $.ligerDialog.warn('请先选择监测点位！');
            return;
        } else {
            var selectedItem = ItemGrid.getSelectedRow();
            if (!selectedItem) {
                $.ligerDialog.warn('请先选择监测项目！');
                return;
            }

            //结果类型，Poll：污染源 Air：大气
            var strPA = selectedItem.REMARK_5;
            //获取监测项目信息
            var ItemInfor = getItemInfor(selectedItem.ITEM_ID);
            //获取要使用的原始记录表名称
            var strCataLogName = ItemInfor[0].ORI_CATALOG_TABLEID;
            //获取监测项目的监测类型 废气：000000002
            var strMONITORID = ItemInfor[0].MONITOR_ID;
            var strTitle = "原始记录表", strPageUrl = "", strKeyTableName = "", strBaseTableName = "";
            if (strCataLogName != "" || strMONITORID == "000000002") {
                if (ItemInfor[0].ITEM_NAME != "烟气黑度") {
                    if (type.id == "setPoll") {
                        //固定污染源原始记录表
                        if (strPA != "" && strPA != "Poll") {
                            $.ligerDialog.confirm('该项目已经生成大气的原始记录，是否删除吗？', function (yes) {
                                if (yes == true) {
                                    $.ajax({
                                        cache: false,
                                        type: "POST",
                                        url: "SamplePoint.aspx/DeleteDustInfor",
                                        data: "{'strResultID':'" + selectedItem.ID + "','strItemID':'" + selectedItem.ITEM_ID + "'}",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (data, textStatus) {
                                            switch (strCataLogName) {
                                                //烟尘类的 使用该表作为原始记录主表                                                             
                                                case "T_MIS_MONITOR_DUSTATTRIBUTE":
                                                    if (ItemInfor[0].ITEM_NAME.indexOf("油烟") != -1) {
                                                        strTitle = "饮食业油烟分析原始记录表";
                                                        strPageUrl = "OriginalTable/DustyTable_YY.aspx";
                                                    }
                                                    else {
                                                        strTitle = "固定污染源排气中颗粒物采样分析原始记录表";
                                                        strPageUrl = "OriginalTable/DustyTable.aspx";
                                                    }
                                                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE";
                                                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                                                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID, selectedItem.REMARK_1, selectedPoint.SAMPLE_NAME);
                                                    break;
                                                //除了标明原始记录表名的监测项目外其他废气的监测项目也使用该表作为原始记录主表                                                  
                                                case "":
                                                    strTitle = "污染源采样原始记录表";
                                                    strPageUrl = "OriginalTable/DustyTable_PM.aspx";
                                                    strKeyTableName = "";
                                                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                                                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID, selectedItem.REMARK_1, selectedPoint.SAMPLE_NAME);
                                                    break;
                                                //PM的和总悬浮物项目类的 使用该表作为原始记录主表                                                              
                                                case "T_MIS_MONITOR_DUSTATTRIBUTE_PM":
                                                    strTitle = "污染源采样原始记录表";
                                                    strPageUrl = "OriginalTable/DustyTable_PM.aspx";
                                                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE_PM";
                                                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                                                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID, selectedItem.REMARK_1, selectedPoint.SAMPLE_NAME);
                                                    break;
                                                //SO2和NOX类的 使用该表作为原始记录主表                                                              
                                                case "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX":
                                                    var strID, strItemID;
                                                    var strSO2 = "99999", strNOX = "99999";
                                                    strID = selectedItem.ID;
                                                    strItemID = selectedItem.ITEM_ID;

                                                    for (var i = 0; i < ItemGrid.rows.length; i++) {
                                                        if (getItemName(ItemGrid.rows[i].ITEM_ID) == "二氧化硫")
                                                            strSO2 = ItemGrid.rows[i].ID;
                                                        if (getItemName(ItemGrid.rows[i].ITEM_ID) == "氮氧化物")
                                                            strNOX = ItemGrid.rows[i].ID;
                                                    }

                                                    if (getItemName(selectedItem.ITEM_ID) == '二氧化硫' || getItemName(selectedItem.ITEM_ID) == '氮氧化物') {
                                                        var obj = getDustInfor(strSO2, strNOX);
                                                        if (obj != null && obj.length > 0) {
                                                            strID = obj[0].SUBTASK_ID;
                                                            strItemID = obj[0].ITEM_ID;
                                                        }
                                                    }

                                                    strTitle = "固定污染源排气中气态污染物采样分析原始记录表";
                                                    strPageUrl = "OriginalTable/DustyTable_So2OrNox.aspx";
                                                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX";
                                                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                                                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, strItemID, strID, selectedItem.REMARK_1, selectedPoint.SAMPLE_NAME);
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    });
                                }
                                else {
                                    return;
                                }
                            });
                        }
                        else {
                            switch (strCataLogName) {
                                //烟尘类的 使用该表作为原始记录主表                                                            
                                case "T_MIS_MONITOR_DUSTATTRIBUTE":
                                    if (ItemInfor[0].ITEM_NAME.indexOf("油烟") != -1) {
                                        strTitle = "饮食业油烟分析原始记录表";
                                        strPageUrl = "OriginalTable/DustyTable_YY.aspx";
                                    }
                                    else {
                                        strTitle = "固定污染源排气中颗粒物采样分析原始记录表";
                                        strPageUrl = "OriginalTable/DustyTable.aspx";
                                    }
                                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE";
                                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID, selectedItem.REMARK_1, selectedPoint.SAMPLE_NAME);
                                    break;
                                //除了标明原始记录表名的监测项目外其他废气的监测项目也使用该表作为原始记录主表                                                 
                                case "":
                                    strTitle = "污染源采样原始记录表";
                                    strPageUrl = "OriginalTable/DustyTable_PM.aspx";
                                    strKeyTableName = "";
                                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID, selectedItem.REMARK_1, selectedPoint.SAMPLE_NAME);
                                    break;
                                //PM的和总悬浮物项目类的 使用该表作为原始记录主表                                                             
                                case "T_MIS_MONITOR_DUSTATTRIBUTE_PM":
                                    strTitle = "污染源采样原始记录表";
                                    strPageUrl = "OriginalTable/DustyTable_PM.aspx";
                                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE_PM";
                                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID, selectedItem.REMARK_1, selectedPoint.SAMPLE_NAME);
                                    break;
                                //SO2和NOX类的 使用该表作为原始记录主表                                                             
                                case "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX":
                                    var strID, strItemID;
                                    var strSO2 = "99999", strNOX = "99999";
                                    strID = selectedItem.ID;
                                    strItemID = selectedItem.ITEM_ID;

                                    for (var i = 0; i < ItemGrid.rows.length; i++) {
                                        if (getItemName(ItemGrid.rows[i].ITEM_ID) == "二氧化硫")
                                            strSO2 = ItemGrid.rows[i].ID;
                                        if (getItemName(ItemGrid.rows[i].ITEM_ID) == "氮氧化物")
                                            strNOX = ItemGrid.rows[i].ID;
                                    }

                                    if (getItemName(selectedItem.ITEM_ID) == '二氧化硫' || getItemName(selectedItem.ITEM_ID) == '氮氧化物') {
                                        var obj = getDustInfor(strSO2, strNOX);
                                        if (obj != null && obj.length > 0) {
                                            strID = obj[0].SUBTASK_ID;
                                            strItemID = obj[0].ITEM_ID;
                                        }
                                    }

                                    strTitle = "固定污染源排气中气态污染物采样分析原始记录表";
                                    strPageUrl = "OriginalTable/DustyTable_So2OrNox.aspx";
                                    strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX";
                                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, strItemID, strID, selectedItem.REMARK_1, selectedPoint.SAMPLE_NAME);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else {
                        //大气采样原始记录表
                        //alert('大气采样原始记录表' + strPA);
                        if (strPA != "" && strPA != "Air") {
                            $.ligerDialog.confirm('该项目已经生成大气的原始记录，是否删除吗？', function (yes) {
                                if (yes == true) {
                                    $.ajax({
                                        cache: false,
                                        type: "POST",
                                        url: "SamplePoint.aspx/DeleteDustInfor",
                                        data: "{'strResultID':'" + selectedItem.ID + "','strItemID':'" + selectedItem.ITEM_ID + "'}",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (data, textStatus) {
                                            strTitle = "大气采样原始记录表";
                                            strPageUrl = "OriginalTable/DustyTable_Air.aspx";
                                            strKeyTableName = strCataLogName;
                                            strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                                            OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID, selectedItem.REMARK_1, selectedPoint.SAMPLE_NAME);
                                        }
                                    });
                                }
                                else {
                                    return;
                                }
                            });
                        }
                        else {
                            strTitle = "大气采样原始记录表";
                            strPageUrl = "OriginalTable/DustyTable_Air.aspx";
                            strKeyTableName = strCataLogName;
                            strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                            OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, selectedItem.ID, selectedItem.REMARK_1, selectedPoint.SAMPLE_NAME);
                        }
                    }
                }
                else {
                    parent.$.ligerDialog.open({ Title: "烟气黑度类", top: 0, width: 800, height: 275, buttons:
                         [{ text: '确定', onclick: f_SaveDateAttr }, { text: '取消', onclick: function (item, dialog) { dialog.close(); }
                         }], url: 'OriginalTable/DustyTable_YH.aspx?strID=' + selectedPoint.POINT_ID
                    });
                }
            } else {
                return;
            }
        }
    }
    //strLinkCode环节编号，01：采样环节；02：监测分析环节；03：分析结果复核环节；04：质控审核环节；05：现场项目结果核录环节；06：分析主任审核环节；07：现场结果复核环节；08：现场室主任审核环节
    function OpenDialog(Title, PageUrl, KeyTable, BaseTable, ItemID, SubTaskID, PurPose, SampleName) {
        parent.$.ligerDialog.open({ Title: Title, top: 0, width: 1100, height: 680, buttons:
         [{ text: '关闭', onclick: function (item, dialog) { ItemGrid.loadData(); dialog.close(); }
         }], url: PageUrl + '?strSubTask_Id=' + SubTaskID + '&strIsView=false&strEdit=true&strItem_Id=' + ItemID + '&strKeyTableName=' + KeyTable + '&strBaseTableName=' + BaseTable + '&strLinkCode=01' + '&strPurPose=' + PurPose + '&strSampleName=' + SampleName
        });
    }
    //save函数
    function f_SaveDateItem(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        strdata = "{'strSubtaskID':'" + strSubtaskID + "'," + strdata + "}";
        $.ajax({
            cache: false,
            type: "POST",
            url: "SamplePoint.aspx/SaveDataItem",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    var selectedPoint = objPointGrid.getSelectedRow();
                    parent.$.ligerDialog.success('数据保存成功');
                    dialog.close();
                    ItemGrid.set('url', "SamplePoint.aspx?type=GetItems&selPointID=" + selectedPoint.POINT_ID + "&strSampleID=" + selectedPoint.ID);
                }
                else {
                    parent.$.ligerDialog.warn('数据保存失败');
                }
            }
        });
    }
});

//获取监测点位信息
function getPointName(strPointID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SamplePoint.aspx/getPointName",
        data: "{'strValue':'" + strPointID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取监测项目信息
function getItemName(strItemID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SamplePoint.aspx/getItemName",
        data: "{'strValue':'" + strItemID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取监测项目信息
function getItemInfor(strItemID) {
    var strValue = null;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SamplePoint.aspx?type=getBaseItemInfor&strItemId=" + strItemID,
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
        url: "SamplePoint.aspx?type=getDustInfor&strSO2=" + strSO2 + "&strNOX=" + strNOX,
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

//save函数
function f_SaveDateAttr(item, dialog) {
    var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
    var strdata = fn();

    $.ajax({
        cache: false,
        type: "POST",
        url: "SamplePoint.aspx/SaveDataAttr",
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                dialog.close();
            }
            else {
                parent.$.ligerDialog.warn('数据保存失败');
            }
        }
    });
}

function EditOriginalInfo() {
    
    window.open("../Monitor/Result/AnalysisOriginalInfo.aspx?ccflowWorkId=" + $.getUrlVar('ccflowWorkId') + "&ccflowFid=" + $.getUrlVar('ccflowFid'));
}