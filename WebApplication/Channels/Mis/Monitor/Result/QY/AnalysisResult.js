// Create by 熊卫华 2012.12.05  "分析结果录入"功能
var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;

var strUrl = "AnalysisResult.aspx";
var strOneGridTitle = "样品号";
var ResultId = "";
//定义分析方法数据集对象
var ComboBoxValue = "";
var ComboBoxText = "";
var vSampleInfor = null,strSubTask_Id = "";
//样品号
$(document).ready(function () {
    objOneGrid = $("#oneGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [10, 15, 20],
        height: '100%',
        url: strUrl + '?type=getOneGridInfo',
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 180, minWidth: 60, frozen: true, render: function (record) {
                     return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
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
                ],
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            //点击的时候加载样品信息
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ITEM_ID);
            objThreeGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});

//获取监测项目信息
function getItemInfor(strItemID) {
    var strValue = null;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../../sampling/QY/SamplePoint.aspx?type=getBaseItemInfor&strItemId=" + strItemID,
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
//样品信息
$(document).ready(function () {
    twoHeight = 3 * $(window).height() / 5;

    objTwoGrid = $("#twoGrid").ligerGrid({
        dataAction: 'server',
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        height: twoHeight,
        usePager: false,
        enabledEdit: true,
        toolbar: { items: [
                { text: '发送', click: SendToNext, icon: 'add' },
                { text: '退回', click: GoToBack, icon: 'add' },
                { text: '前处理说明', click: DoSampleRemark, icon: 'bookadd' },
                { text: '样品原始记录', click: SetTable, icon: 'attibutes' },
                { text: '获取仪器结果', click: GetLims, icon: 'bookadd' },
                { text: '查看仪器数据', click: ShowLims, icon: 'attibutes' }
                ]
        },
        columns: [
                 { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 200, minWidth: 40, frozen: true },
                { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60,
                    editor: {
                        type: 'text'
                    }
                },
                { display: '单位', name: 'UNIT', align: 'left', width: 100, minWidth: 60 },
                 { display: '方法依据', name: 'METHOD_CODE', align: 'left', width: 200, minWidth: 60 },
                 { display: '分析方法', name: 'ANALYSIS_METHOD_ID', align: 'left', width: 200, minWidth: 60,
                     editor: {
                         type: 'select', valueColumnName: 'ID', displayColumnName: 'ANALYSIS_NAME',
                         ext:
                            function (rowdata) {
                                return {
                                    onBeforeOpen: function () {
                                        $.ligerDialog.open({ title: '选择分析方法', name: 'winselector', width: 700, height: 370, url: '../SelectItemAnalysis.aspx?ItemID=' + rowdata.ITEM_ID, buttons: [
                                                  { text: '确定', onclick: function (item, dialog) {
                                                      var fn = dialog.frame.f_select || dialog.frame.window.f_select;
                                                      var data = fn();
                                                      if (!data) {
                                                          $.ligerDialog.warn('请选择分析方法!');
                                                          return;
                                                      }
                                                      rowdata.ANALYSIS_METHOD_ID = data.ID;
                                                      rowdata.ANALYSIS_NAME = data.ANALYSIS_METHOD;

                                                      //objTwoGrid.updateCell("ANALYSIS_NAME", data.ANALYSIS_METHOD, rowdata);
                                                      objTwoGrid.updateCell("RESULT_CHECKOUT", data.LOWER_CHECKOUT, rowdata);
                                                      objTwoGrid.updateCell("APPARATUS_CODE", data.APPARATUS_CODE, rowdata);
                                                      objTwoGrid.updateCell("APPARATUS_NAME", data.INSTRUMENT, rowdata);
                                                      objTwoGrid.updateCell("METHOD_CODE", data.METHOD, rowdata);
                                                      objTwoGrid.updateCell("UNIT", data.UNIT, rowdata);
                                                      objTwoGrid.endEdit();
                                                      dialog.close();
                                                  }
                                                  },
                                                  { text: '取消', onclick: function (item, dialog) {
                                                      dialog.close();
                                                  }
                                                  }
                                             ]
                                        });
                                        return false;
                                    },
                                    render: function () {
                                        return rowdata.ANALYSIS_NAME;
                                    }
                                    /*
                                    selectBoxWidth: 200,
                                    url: strUrl + "?type=getAnalysisByItemId&strItemId=" + rowdata.ITEM_ID,
                                    onSelected: function (value, text) {
                                    rowdata.ANALYSIS_METHOD_ID = value;
                                    rowdata.ANALYSIS_NAME = text;
                                    //查询最低检出线和仪器
                                    if (this.data != null) {
                                    for (var i = 0; i < this.data.length; i++) {
                                    if (value == this.data[i]['ID']) {
                                    rowdata.LOWER_CHECKOUT = this.data[i]['LOWER_CHECKOUT'];
                                    rowdata.APPARATUS_CODE = this.data[i]['APPARATUS_CODE'];
                                    rowdata.APPARATUS_NAME = this.data[i]['INSTRUMENT_NAME'];
                                    rowdata.METHOD_CODE = this.data[i]['METHOD_CODE'];
                                    }
                                    }
                                    }
                                    }*/
                                };
                            }
                     },
                     render: function (item) {
                         return item.ANALYSIS_NAME;
                     }
                 },
                 { display: '检出限', name: 'RESULT_CHECKOUT', align: 'left', width: 100, minWidth: 60,
                     editor: {
                         type: 'text'
                         //                         ext:
                         //                            function (rowdata) {
                         //                                return {
                         //                                    //selectBoxWidth: 200,
                         //                                    url: strUrl + "?type=getAnalysisByItemId&strItemId=" + 12222
                         //                                };
                         //                            }
                         //                     },
                         //                        render: function (item) {
                         //                         return item.LOWER_CHECKOUT;
                     }
                     //                     editor: {
                     //                         type: 'text',
                     //                         ext:
                     //                         function (rowdata) {
                     //                             return {
                     //                                 url: strUrl + "?type=getAnalysisByItemId&strItemId=" + rowdata.ITEM_ID                                
                     //                                 cache: false,
                     //                                 async: false,
                     //                                 type: "POST",
                     //                                 url: strUrl + "/saveCheckInfo",
                     //                                 data: "{'strItemID':'" + rowdata.ITEM_ID + "','strcheckID':'" + rowdata.LOWER_CHECKOUT_ID + "','strLowResult':'" + rowdata.LOWER_CHECKOUT + "','strAnalysisMethod':'" + rowdata.ANALYSIS_METHOD_ID + "'}",
                     //                                 contentType: "application/json; charset=utf-8",
                     //                                 dataType: "json",
                     //                                 success: function (data, textStatus) {
                     //                                    if (data.d == 1) {
                     //                                        objTwoGrid.cancelEdit(e.rowindex);
                     //                                    }
                     //                                }
                     //                             };
                     //                         }
                     //                     },
                     //                     render: function (item) {
                     //                         return item.LOWER_CHECKOUT;
                     //                     }
                 },
                 { display: '仪器编号', name: 'APPARATUS_CODE', align: 'left', width: 100, minWidth: 60 },
                 { display: '仪器', name: 'APPARATUS_NAME', align: 'left', width: 200, minWidth: 60 },
                 { display: '要求完成时间', name: 'ASKING_DATE', align: 'left', width: 100, minWidth: 60 },
                 { display: '实际完成时间', name: 'FINISH_DATE', align: 'left', width: 100, minWidth: 60,
                     render: function (record, rowindex, value) {
                         var strFinishDate = value;
                         var strFinishDateTemp = "";
                         if (strFinishDate != "")
                             strFinishDateTemp = strFinishDate;
                         else
                             strFinishDateTemp = "请选择";
                         return "<a href=\"javascript:getFinishDate('FINISH_DATE'," + rowindex + ")\">" + strFinishDateTemp + "</a> ";
                     }
                 },
                 { display: '前处理说明', name: 'REMARK_2', align: 'center', width: 100, minWidth: 60, render: function (items) {
                     if (items.REMARK_2 != "") {
                         return "<a href='javascript:showSampleRemarkSrh(\"" + items.ID + "\",\"" + items.REMARK_2 + "\",false)'>查看</a>";
                     }
                     return items.REMARK_2;
                 }
                 },
                 { display: '特殊样说明', name: 'RemarkView', align: 'center', width: 100, render: function (items) {
                     if (items.SPECIALREMARK != "") {
                         return "<a href='javascript:showDetailRemarkSrh(\"" + items.SAMPLE_ID + "\",\"" + items.SPECIALREMARK + "\",false)'>查看</a>";
                     }
                 }
                 },
                { display: '子样', name: 'SUBSAMPLE', align: 'center', width: 100, render: function (items) {
                    if (getSubSample(items.SAMPLE_ID) != null) {
                        return "<a href='javascript:ShowSubSample(\"" + items.SAMPLE_ID + "\")'>明细</a>";
                    }
                }
                },
                 { display: '分析负责人', name: 'HEAD_USERID', align: 'left', width: 100, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUserName(record.HEAD_USERID);
                 }
                 },
                 { display: '分析协同人', name: 'ASSISTANT_USERID', align: 'left', width: 150, minWidth: 60,
                     render: function (record, rowindex, value) {
                         var objUserArray = getAjaxUseExNameEdit(record.ID);
                         var strUserName = "";
                         if (objUserArray.length > 0)
                             strUserName = objUserArray[0]["UserName"];
                         else
                             strUserName = "请选择";
                         return "<a href=\"javascript:getDefaultUserExName('" + record.ID + "')\">" + strUserName + "</a> ";
                     }
                 },
                 { display: '退回意见', name: 'REMARK_1', align: 'left', width: 180, minWidth: 60,
                     render: function (record, rowindex, value) {
                         return "<a href=\"javascript:showSuggestion('" + value + "')\">" + (value.length > 10 ? value.substring(0, 10) + "......" : value) + "</a> ";
                     }
                 }
                ],
        onContextmenu: function (parm, e) {

        },
        onDblClickRow: function (data, rowindex, rowobj) {

        },
        onCheckRow: function (checked, rowdata, rowindex) {
            if (checked) {
                //点击的时候加载内控信息
                objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID);
                ResultId = rowdata.ID;
            }
        },
        onAfterEdit: AfterEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return true; }
    });
    //$(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    function SetTable() {
        var selectedItem = objOneGrid.getSelectedRow();
        if (!selectedItem) {
            $.ligerDialog.warn('请先选择监测项目！');
            return;
        }
        var strItem = getItemInfor(selectedItem.ITEM_ID);
        //        if (strItem[0].IS_ANYSCENE_ITEM != "1") {
        //            $.ligerDialog.warn('非分析类现场监测项目，请重新选择！');
        //            return;
        //        }

        var selectedSample = objTwoGrid.getSelectedRow();
        if (!selectedSample) {
            $.ligerDialog.warn('请先选择样品！');
            return;
        }
        else {
            getSampleInfor(selectedSample.SAMPLE_ID);
            if (vSampleInfor != null) {
                strSubTask_Id = vSampleInfor[0].SUBTASK_ID;
            }
            strSubTask_Id = selectedSample.ID;
            //结果类型，Poll：污染源 Air：大气
            var strPA = selectedSample.REMARK_5;
            var ItemInfor = getItemInfor(selectedItem.ITEM_ID);
            var strCataLogName = ItemInfor[0].ORI_CATALOG_TABLEID;
            //获取监测项目的监测类型 废气：000000002
            var strMONITORID = ItemInfor[0].MONITOR_ID;
            var strTitle = "原始记录表", strPageUrl = "", strKeyTableName = "", strBaseTableName = "";
            if (strCataLogName != "" || strMONITORID == "000000002") {

                if (strPA != "Air") {
                    //固定污染源原始记录表
                    switch (strCataLogName) {
                        //烟尘类的 使用该表作为原始记录主表                                                          
                        case "T_MIS_MONITOR_DUSTATTRIBUTE":
                            if (ItemInfor[0].ITEM_NAME.indexOf("油烟") != -1) {
                                strTitle = "饮食业油烟分析原始记录表";
                                strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_YY.aspx";
                            }
                            else {
                                strTitle = "固定污染源排气中颗粒物采样分析原始记录表";
                                strPageUrl = "../../sampling/QY/OriginalTable/DustyTable.aspx";
                            }
                            strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE";
                            strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                            OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, strSubTask_Id);
                            break;
                        //除了标明原始记录表名的监测项目外其他废气的监测项目也使用该表作为原始记录主表                                        
                        case "":
                            strTitle = "污染源采样原始记录表";
                            strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_PM.aspx";
                            strKeyTableName = "";
                            strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                            OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, strSubTask_Id);
                            break;
                        //PM的和总悬浮物项目类的 使用该表作为原始记录主表                                                          
                        case "T_MIS_MONITOR_DUSTATTRIBUTE_PM":
                            strTitle = "污染源采样原始记录表";
                            strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_PM.aspx";
                            strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE_PM";
                            strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                            OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, strSubTask_Id);
                            break;
                        //SO2和NOX类的 使用该表作为原始记录主表                                                          
                        case "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX":
                            strTitle = "固定污染源排气中气态污染物采样分析原始记录表";
                            strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_So2OrNox.aspx";
                            strKeyTableName = "T_MIS_MONITOR_DUSTATTRIBUTE_SO2ORNOX";
                            strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                            OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, strSubTask_Id);
                            break;
                        default:
                            break;
                    }
                }
                else {
                    strTitle = "大气采样原始记录表";
                    strPageUrl = "../../sampling/QY/OriginalTable/DustyTable_Air.aspx";
                    strKeyTableName = strCataLogName;
                    strBaseTableName = "T_MIS_MONITOR_DUSTINFOR";
                    OpenDialog(strTitle, strPageUrl, strKeyTableName, strBaseTableName, selectedItem.ITEM_ID, strSubTask_Id);
                }
            } else {
                return;
            }
            //            $.ligerDialog.open({ title: strItem[0].ITEM_NAME + '样品原始记录表', top: 0, width: 1100, height: 680, buttons:
            //         [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); }
            //         }], url: '../../sampling/QY/OriginalTable/DustyTable.aspx?strSubTask_Id=' + strSubTask_Id + '&strIsView=true&strEdit=true&strItem_Id=' + selectedItem.ITEM_ID
            //            });
        }
    }
    //strLinkCode环节编号，01：采样环节；02：监测分析环节；03：分析结果复核环节；04：质控审核环节；05：现场项目结果核录环节；06：分析主任审核环节；07：现场结果复核环节；08：现场室主任审核环节
    function OpenDialog(Title, PageUrl, KeyTable, BaseTable, ItemID, SubTaskID) {
        $.ligerDialog.open({ Title: Title, top: 0, width: 1100, height: 550, buttons:
         [{ text: '关闭', onclick: function (item, dialog) { objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + ItemID); dialog.close(); }
         }], url: PageUrl + '?strSubTask_Id=' + SubTaskID + '&strIsView=true&strEdit=false&strItem_Id=' + ItemID + '&strKeyTableName=' + KeyTable + '&strBaseTableName=' + BaseTable + '&strLinkCode=02'
        });
    }
    function getSampleInfor(strSampleId) {

        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "?type=getSampleInfor&strSampleId=" + strSampleId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.Total != "0") {
                    vSampleInfor = data.Rows;
                }
            }
        });
    }

    function GetLims() {
        var selectedItem = objOneGrid.getSelectedRow();
        if (!selectedItem) {
            $.ligerDialog.warn('请先选择监测项目！');
            return;
        }
        var strValue = selectedItem.ITEM_ID;
        $.ajax({
            cache: false,
            type: "POST",
            url: strUrl + "/GetLims",
            data: "{'strValue':'" + strValue + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + strValue);
                    //$.ligerDialog.success('获取仪器结果成功')
                }
                else {
                    //$.ligerDialog.warn('获取仪器结果失败');
                }
            }
        });
    }

    function ShowLims() {
        var selectedItem = objOneGrid.getSelectedRow();
        if (!selectedItem) {
            $.ligerDialog.warn('请先选择监测项目！');
            return;
        }
        var selectedSample = objTwoGrid.getSelectedRow();
        if (!selectedSample) {
            $.ligerDialog.warn('请先选择样品！');
            return;
        }

        if (selectedSample.REMARK_3 != "1") {
            $.ligerDialog.warn('请先获取仪器结果！');
            return;
        }

        $.ligerDialog.open({ title: '仪器数据查看', top: 0, width: 750, height: 450, buttons:
        [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); } }],
            url: 'AnalysisResult_Lims.aspx?ResultID=' + selectedSample.ID
        });
    }

    //发送到下一环节
    function SendToNext() {
        if (objTwoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('发送之前请先选择监测项目信息');
            return;
        }
        var spit = '';
        var strSum = "";
        for (var i = 0; i < objTwoGrid.getCheckedRows().length; i++) {
            if (objTwoGrid.getCheckedRows()[i].ITEM_RESULT == "" && objTwoGrid.getCheckedRows()[i].MONITOR_ID != "000000002") {
                $.ligerDialog.warn('分析结果没有填写完整');
                return;
            }
            strSum = strSum + spit + objTwoGrid.getCheckedRows()[i].ID;
            spit = ",";
        }
        if (!CheckDustyTable(strSum)) {
            $.ligerDialog.warn('原始记录信息没有填写完整，请检查！');
            return;
        }
        
        $.ligerDialog.open({ title: "选择分析结果复核人", width: 400, height: 250, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            //var strUserId = $("iframe")[0].contentWindow.getUserId();
            var fn = dialog.frame.getUserId || dialog.frame.window.getUserId;
            var strUserId = fn();
            if (strUserId != "") {
                if (SendResultToNextFlow(strSum, strUserId) == "1") {
                    objThreeGrid.set("data", emptyArray);
                    objOneGrid.loadData();
                    objTwoGrid.loadData();
                    objThreeGrid.loadData();
                    dialog.close();
                    $.ligerDialog.success('任务发送成功，发送至【<a style="color:Red;font-weight:bold">分析结果复核</a>】环节');
                    return;
                }
                else {
                    $.ligerDialog.warn('任务发送失败');
                    return;
                }
                objTwoGrid.loadData();
            }
            else {
                $.ligerDialog.warn('请先选择分析结果复核人');
                return;
            }
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "ResultUserSetting.aspx?strItemId=" + objTwoGrid.getSelectedRow().ITEM_ID + "&strSampleId=" + objTwoGrid.getSelectedRow().SAMPLE_ID
        });
    }
    //检查废气监测项目的原始记录信息结果值有没有填写
    function CheckDustyTable(strSumResultId) {
        var isSuccess = false;
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "?type=CheckDustyTable&strSumResultId=" + strSumResultId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data == "1")
                    isSuccess = true;
            }
        });
        return isSuccess;
    }
    //将结果发送至下一个环节
    function SendResultToNextFlow(strSumResultId, strNextFlowUserId) {
        var isSuccess = false;
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "?type=SendResultToNext&strSumResultId=" + strSumResultId + "&strNextFlowUserId=" + strNextFlowUserId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                isSuccess = data;
            }
        });
        return isSuccess;
    }
    function GoToBack() {
        if (objTwoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('退回之前请先选择样品');
            return;
        }
        var spit = '';
        var strSum = "";
        for (var i = 0; i < objTwoGrid.getCheckedRows().length; i++) {
            strSum = strSum + spit + objTwoGrid.getCheckedRows()[i].ID;
            spit = ",";
        }
        $.ligerDialog.prompt('退回意见', '', true, function (yes, value) {

            if (yes) {
                //退回该项目
                $.ajax({
                    cache: false,
                    async: false,
                    type: "POST",
                    url: strUrl + "?type=GoToBack&strResultId=" + strSum + "&strSuggestion=" + encodeURI(value),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data == "1") {
                            objTwoGrid.loadData();
                            objThreeGrid.set("data", emptyArray);
                            $.ligerDialog.success('样品退回成功');
                            return;
                        }
                        else {
                            $.ligerDialog.warn('样品退回失败');
                            return;
                        }
                    }
                });
            }
        });

    }

    function DoSampleRemark() {
        if (objTwoGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录！');
            return;
        }
        else {
            var rowselected = objTwoGrid.getSelectedRow();
            showSampleRemarkSrh(rowselected.ID, "", true);
        }
    }
    function AfterEdit(e) {
        var id = e.record.ID;
        var strItemResult = e.record.ITEM_RESULT;
        var strAnalysisMethod = e.record.ANALYSIS_METHOD_ID;
        //var strFinishDate = e.record.FINISH_DATE;
        var strLowResult = e.record.RESULT_CHECKOUT;

        if (strLowResult != "") {
            if (parseFloat(strItemResult) < parseFloat(strLowResult)) {
                $.ligerDialog.warn('结果值低于最低检出限【<a style="color:Red;font-weight:bolder">' + strLowResult + '</a>】!');
                objTwoGrid.updateCell("ITEM_RESULT", "", e.record);
                return;
            }
        }
        var columnname = "", value = "";
        columnname = e.column.columnname;
        value = e.value;
        if (e.record["__status"] != "nochanged") {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/saveItemInfo",
                data: "{'id':'" + id + "','strColumnName':'" + columnname + "','strValue':'" + value + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == 1) {
                        objTwoGrid.cancelEdit(e.rowindex);
                    }
                }
            });
        }

        //        $.ajax({
        //            cache: false,
        //            async: false,
        //            type: "POST",
        //            url: strUrl + "/saveCheckInfo",
        //            data: "{'strItemID':'" + e.record.ITEM_ID + "','strcheckID':'" + e.record.LOWER_CHECKOUT_ID + "','strLowResult':'" + e.record.LOWER_CHECKOUT + "','strAnalysisMethod':'" + e.record.ANALYSIS_METHOD_ID + "'}",
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",
        //            success: function (data, textStatus) {
        //                if (data.d == 1) {
        //                    objTwoGrid.cancelEdit(e.rowindex);
        //                }
        //            }
        //        });
    }
});
//实验室质控信息
$(document).ready(function () {
    threeHeight = 2 * $(window).height() / 5 - 35;

    objThreeGrid = $("#threeGrid").ligerGrid({
        dataAction: 'server',
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        height: threeHeight,
        usePager: false,
        toolbar: { items: [
                { text: '实验室质控设置', click: QcSetting, icon: 'add' },
                { text: '修改', click: QcEditing, icon: 'modify' },
                { text: '删除', click: QcDelete, icon: 'delete' }
                ]
        },
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 100, minWidth: 60, frozen: true, render: function (record) {
                     return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                 }
                 },
                 { display: '质控手段', name: 'QC_TYPE', align: 'left', width: 100, minWidth: 60, render: function (record) {
                     return getQcName(record.QC_TYPE);
                 }
                 },
                  { display: '结果值', name: 'ITEM_RESULT', align: 'left', width: 50, minWidth: 60 },
                  { display: '质控结果', name: 'REMARK', align: 'left', width: 300, minWidth: 60 },
                  { display: '是否合格', name: 'IS_OK', align: 'left', width: 50, minWidth: 60, render: function (record) {
                      if (record.IS_OK == "0")
                          return "<span style='color:red'>否</span>";
                      else
                          return "是";
                  }
                  }
                ],
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onAfterEdit: AfterEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    objThreeGrid.toggleCol("IS_OK");
    //$(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //实验室质控设置
    function QcSetting() {
        if (!objTwoGrid.getSelectedRow()) {
            $.ligerDialog.warn('请先选择需要设置的一个样品');
            return;
        }
        /*
        var ResultJson = "[";
        for (var i = 0; i < objTwoGrid.getSelectedRows().length; i++) {
            ResultJson += "{";
            ResultJson += "ResultID:\"" + objTwoGrid.getSelectedRows()[i].ID + "\",SampleCode:\"" + objTwoGrid.getSelectedRows()[i].SAMPLE_CODE + "\",ItemResult:\"" + objTwoGrid.getSelectedRows()[i].ITEM_RESULT + "\""
            if (i == objTwoGrid.getSelectedRows().length - 1)
                ResultJson += "}";
            else
                ResultJson += "},";
        }
        ResultJson += "]";
        */
        var ResultJson = "";
        for (var i = 0; i < objTwoGrid.getSelectedRows().length; i++) {
            if (i == objTwoGrid.getSelectedRows().length - 1)
                ResultJson += objTwoGrid.getSelectedRows()[i].ID;
            else
                ResultJson += objTwoGrid.getSelectedRows()[i].ID + ",";
        }
        $.ligerDialog.open({ title: "实验室质控设置", width: 500, height: 500, isHidden: false, buttons:
        [
        { text:
        '计算', onclick: function (item, dialog) {
            $("iframe")[0].contentWindow.calculate()
        }
        },
        { text:
        '保存', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.saveQcValue()) {
                objTwoGrid.loadData();
                objThreeGrid.loadData();
                dialog.close();
                $.ligerDialog.success('数据保存成功');
                return;
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据保存失败');
                return;
            }
            objThreeGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../QcSetting_QY.aspx?ResultJson=" + ResultJson
        });
    }
    //实验室质控修改
    function QcEditing() {
        if (!objThreeGrid.getSelectedRow()) {
            $.ligerDialog.warn('请先选择需要修改的质控样');
            return;
        }
        
        $.ligerDialog.open({ title: "实验室质控修改", width: 500, height: 200, isHidden: false, buttons:
        [
        { text:
        '计算', onclick: function (item, dialog) {
            $("iframe")[0].contentWindow.calculate()
        }
        },
        { text:
        '保存', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.saveQcValue()) {
                objThreeGrid.loadData();
                dialog.close();
                $.ligerDialog.success('数据修改成功');
                return;
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据修改失败');
                return;
            }
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "QcEditing_QY.aspx?ID=" + objThreeGrid.getSelectedRow().ID + "&QC_TYPE=" + objThreeGrid.getSelectedRow().QC_TYPE
        });
    }
    //实验室质控样的删除
    function QcDelete() {
        if (ResultId == "") {
            $.ligerDialog.warn('请先选择一个样品进行操作');
            return;
        }
        var objQcSelect = objThreeGrid.getSelectedRow();
        if (!objQcSelect) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }

        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/deleteQcAnalysis",
            data: "{'id':'" + objQcSelect.ID + "','qc_type':'" + objQcSelect.QC_TYPE + "','result_id':'" + ResultId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == 1) {
                    objThreeGrid.loadData();
                    $.ligerDialog.success('数据删除成功');
                }
                else {
                    $.ligerDialog.warn('数据删除失败');
                }
            }
        });
    }

    function AfterEdit(e) {

    }
});
//获取质控手段名称
function getQcName(strQcId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getQcName",
        data: "{'strQcId':'" + strQcId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取默认负责人用户名称
function getAjaxUserName(strUserId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDefaultUserName",
        data: "{'strUserId':'" + strUserId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取默认协同人名称
function getAjaxUseExName(strUserId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDefaultUserExName",
        data: "{'strUserId':'" + strUserId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
//获取默认协同人名称
function getAjaxUseExNameEdit(strResultId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getDefaultUserExNameEdit",
        data: "{'strResultId':'" + strResultId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return jQuery.parseJSON(strValue);
}
//弹出选择默认协同人
function getDefaultUserExName(strResultId) {
    $.ligerDialog.open({ title: "选择分析协同人", width: 400, height: 250, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.UserSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('数据保存失败');
            }
            objTwoGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../DefaultUserEx.aspx?strResultId=" + strResultId
    });
}


//设置grid 的弹出特殊样说明录入对话框
var detailRemarkWinSrh = null;
function showDetailRemarkSrh(strSubTaskId, oldRemark, isAdd) {
    //创建表单结构

    var mainRemarkform = $("#RemarkForm");
    mainRemarkform.ligerForm({
        inputWidth: 430, labelWidth: 90, space: 40,
        fields: [
                     { display: "特殊样说明", name: "SEA_REMARK", newline: true, type: "textarea" }
                    ]
    });
    $("#SEA_REMARK").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:400px");

    $("#SEA_REMARK").val(oldRemark);
    var ObjButton = [];
    if (!isAdd) {
        $("#SEA_REMARK").attr("disabled", true);
        ObjButton = [
                  { text: '返回', onclick: function () { clearRemarkDialogValue(); detailRemarkWinSrh.hide(); } }
                  ];
    } else {
        $("#SEA_REMARK").attr("disabled", false);
        ObjButton = [
                  { text: '确定', onclick: function () { SaveRemark(strSubTaskId); } },
                  { text: '返回', onclick: function () { clearRemarkDialogValue(); detailRemarkWinSrh.hide(); } }
                  ];
    }
    detailRemarkWinSrh = $.ligerDialog.open({
        target: $("#detailRemark"),
        width: 660, height: 170, top: 90, title: isAdd ? "特殊样说明录入" : "特殊样说明查看",
        buttons: ObjButton
    });
}
function clearRemarkDialogValue() {
    $("#SEA_REMAKR").val("");
}

//设置grid 的弹出特殊样说明录入对话框
var SampleRemarkWinSrh = null;
function showSampleRemarkSrh(strSubTaskId, oldRemark, isAdd) {
    //创建表单结构

    var sampleRemarkform = $("#SampleRemarkForm");
    sampleRemarkform.ligerForm({
        inputWidth: 430, labelWidth: 90, space: 40,
        fields: [
                     { display: "前处理说明", name: "SEA_SAMPLEREMARK", newline: true, type: "textarea" }
                    ]
    });
    $("#SEA_SAMPLEREMARK").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:400px");

    $("#SEA_SAMPLEREMARK").val(oldRemark);
    var ObjButton = [];
    if (!isAdd) {
        $("#SEA_SAMPLEREMARK").attr("disabled", true);
        ObjButton = [
                  { text: '返回', onclick: function () { clearSampleRemarkDialogValue(); SampleRemarkWinSrh.hide(); } }
                  ];
    } else {
        $("#SEA_SAMPLEREMARK").attr("disabled", false);
        ObjButton = [
                  { text: '确定', onclick: function () { SaveSampleRemark(strSubTaskId); } },
                  { text: '返回', onclick: function () { clearSampleRemarkDialogValue(); SampleRemarkWinSrh.hide(); } }
                  ];
    }
    SampleRemarkWinSrh = $.ligerDialog.open({
        target: $("#detailSampleRemark"),
        width: 660, height: 170, top: 90, title: isAdd ? "前处理说明录入" : "前处理说明查看",
        buttons: ObjButton
    });
}
function clearSampleRemarkDialogValue() {
    $("#SEA_SAMPLEREMARK").val("");
}

function SaveSampleRemark(strSubTaskId) {
    var strRemark = $("#SEA_SAMPLEREMARK").val();
    $.ajax({
        cache: false,
        type: "POST",
        url: "AnalysisResult.aspx/SaveRemark",
        data: "{'strValue':'" + strSubTaskId + "','strSampleRemark':'" + strRemark + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "true") {
                objTwoGrid.loadData();
                clearSampleRemarkDialogValue();
                SampleRemarkWinSrh.hide();
                $.ligerDialog.success('数据操作成功')
            }
            else {
                $.ligerDialog.warn('数据操作失败');
            }
        }
    });
}

function getSubSample(SampleId) {
    var objItems = null;
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../../sampling/QY/SubSample.aspx?action=GetSubSampleList&strSampleId=" + SampleId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != "0") {
                objItems = data.Rows;
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax请求数据失败！');
        }
    });
    return objItems;
}

function ShowSubSample(strId) {
    $.ligerDialog.open({ title: '子样明细', name: 'winaddtor', width: 700, height: 400, top: 90, url: '../../sampling/QY/SubSample.aspx?strView=true&strSampleId=' + strId, buttons: [
                { text: '返回', onclick: function (item, dialog) { dialog.close() } }
            ]
    });
}

//弹出退回意见框
var SuggestionDialog = null;
function showSuggestion(value) {
    //创建表单结构
    $("#SuggForm").ligerForm({
        inputWidth: 430, labelWidth: 90, space: 40,
        fields: [
                     { display: "退回意见", name: "Suggestion", newline: true, type: "textarea" }
                    ]
    });
    $("#Suggestion").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:320px");

    $("#Suggestion").val(value);
    var ObjButton = [];

    $("#Suggestion").attr("disabled", true);
    ObjButton = [
                  { text: '返回', onclick: function () { SuggestionDialog.hide(); } }
                  ];
    SuggestionDialog = $.ligerDialog.open({
        target: $("#divSugg"),
        width: 560, height: 170, top: 90, title: "退回意见查看",
        buttons: ObjButton
    });
}
//弹出选择分析完成时间
function getFinishDate(strColumnName, iRow) {
    $.ligerDialog.open({ title: "录入分析完成时间", width: 400, height: 300, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            objTwoGrid.updateCell(strColumnName, $("iframe")[0].contentWindow.GetDate(), iRow);
            
            var id = objTwoGrid.rows[iRow].ID;
            var strItemResult = objTwoGrid.rows[iRow].ITEM_RESULT;
            var strAnalysisMethod = objTwoGrid.rows[iRow].ANALYSIS_METHOD_ID;
            var strFinishDate = objTwoGrid.rows[iRow].FINISH_DATE;
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/saveItemInfo",
                data: "{'id':'" + id + "','strItemResult':'" + strItemResult + "','strAnalysisMethod':'" + strAnalysisMethod + "','strFinishDate':'" + strFinishDate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == 1) {
                        //objTwoGrid.cancelEdit(e.rowindex);
                    }
                }
            });
            dialog.close();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../FinishDateSelected.aspx"
    });
}