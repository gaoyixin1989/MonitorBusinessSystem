// Create by 邵世卓 2013.03.07  "现场监测结果复核"功能

var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;
var objFourGrid = null;

var strUrl = "SampleAnalysisResultCheck.aspx";
var strOneGridTitle = "";
var strTwoGridTitle = "监测项目信息";
var strThreeGridTitle = "监测项目信息";
var vSampleInfor = null, strSubTask_Id = "";
var intSuccessNum = 0;
var objResultID = "";

function Save() { 

}

//监测任务管理
$(document).ready(function () {
    objResultID = $("#cphData_RESULT_ID").val();

    topHeight = 2 * $(window).height() / 5 - 60;
    objOneGrid = $("#oneGrid").ligerGrid({
        title: strOneGridTitle,
        dataAction: 'server',
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: topHeight,
        url: strUrl + '?type=getOneGridInfo&strResultID=' + objResultID,
        columns: [
                {
                    display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 150, minWidth: 60
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
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID + "&strResultID=" + objResultID);
            objThreeGrid.set("data", emptyArray);
            objFourGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");


    function setResultStatus(strInforId) {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "?type=setResultStatus&strInfor_Id=" + strInforId,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data == "True") {
                    intSuccessNum++;
                }
            }
        });
    }

});
//监测类别管理
$(document).ready(function () {
    bottomHeight = 3 * $(window).height() / 5 - 35;
    objTwoGrid = $("#twoGrid").ligerGrid({
        dataAction: 'server',
        usePager: false,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        height: bottomHeight,
        columns: [
                    { display: '监测类别', name: 'MONITOR_ID', align: 'left', width: 150, minWidth: 60, render: function (record) {
                        return getMonitorTypeName(record.MONITOR_ID);
                    }
                    }
                ],
        toolbar: { items: [
                { text: '点位图下载', click: downLoadFile, icon: 'filedown'}]
        },
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

            //点击的时候加载监测项目信息
            objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID + "&strResultID=" + objResultID);
            objFourGrid.set("data", emptyArray);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
///附件下载
function downLoadFile() {
    if (objTwoGrid.getSelectedRow() == null) {
        $.ligerDialog.warn('请先选择子任务类别信息');
        return;
    }
    $.ligerDialog.open({ title: '点位图下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../../OA/ATT/AttFileDownLoad.aspx?filetype=SubTask&id=' + objTwoGrid.getSelectedRow().ID
    });
}
//样品信息
$(document).ready(function () {
    twoHeight = 3 * $(window).height() / 5;
    objThreeGrid = $("#threeGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: true,
        pageSize: 100,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: twoHeight,
        columns: [
                { display: '样品号', name: 'SAMPLE_CODE', align: 'left', width: 150, minWidth: 60 },
                      { display: '前处理说明', name: 'SAMPLE_REMARK', align: 'center', width: 80, render: function (items) {
                          if (items.SAMPLE_REMARK != "") {
                              return "<a href='javascript:showSampleRemarkSrh(\"" + items.ID + "\",\"" + items.SAMPLE_REMARK + "\",false)'>查看</a>";
                          }
                          return items.SAMPLE_REMARK;
                      }
                      },
                 { display: '特殊样说明', name: 'RemarkView', align: 'center', width: 80, render: function (items) {
                     if (items.SPECIALREMARK != "") {
                         return "<a href='javascript:showDetailRemarkSrh(\"" + items.ID + "\",\"" + items.SPECIALREMARK + "\",false)'>查看</a>";
                     }
                 }
                 },
                { display: '子样', name: 'SUBSAMPLE', align: 'center', width: 80, render: function (items) {
                    if (getSubSample(items.ID) != null) {
                        return "<a href='javascript:ShowSubSample(\"" + items.ID + "\")'>明细</a>";
                    }
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

            //点击的时候加载监测项目信息
            objFourGrid.set('url', strUrl + "?type=getFourGridInfo&threeGridId=" + rowdata.ID + "&strResultID=" + objResultID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
//监测项目信息
$(document).ready(function () {
    threeHeight = 6 * $(window).height() / 10;
    objFourGrid = $("#fourGrid").ligerGrid({
        title: "",
        dataAction: 'server',
        usePager: false,
        pageSize: 100,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: threeHeight,
        enabledEdit: true,
        rownumbers: true,
        onAfterEdit: f_onAfterEdit,
        toolbar: { items: [
                { id: 'setting', text: '原始记录信息', click: SetTable, icon: 'attibutes' }
                ]
        },
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
                     return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                 }
                 },
                { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60, editor: { type: 'text'} },
                { display: '单位', name: 'ITEM_ID', align: 'left', width: 60, minWidth: 60, render: function (record) {
                    return getItemUnit(record.ITEM_ID);
                }
                },
                { display: '采样完成日期', name: 'SAMPLE_FINISH_DATE', align: 'left', width: 100, minWidth: 60
                },
                 { display: '采样负责人', name: 'SAMPLING_MANAGE', align: 'left', width: 100, minWidth: 60, render: function (record, rowindex, value) {
                     return getAjaxUserName(record.SAMPLING_MANAGER_ID);
                 }
                 },
                 { display: '采样协同人', name: 'SAMPLING_MAN', align: 'left', width: 200, minWidth: 60
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
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    function SetTable() {

        var selectedSample = objThreeGrid.getSelectedRow();
        if (!selectedSample) {
            $.ligerDialog.warn('请先选择样品！');
            return;
        }
        var selectedItem = objFourGrid.getSelectedRow();
        if (!selectedItem) {
            $.ligerDialog.warn('请先选择监测项目！');
            return;
        }
        //        var strItem = getItemInfor(selectedItem.ITEM_ID);
        //        if (strItem[0].IS_ANYSCENE_ITEM != "1") {
        //            $.ligerDialog.warn('非分析类现场监测项目，请重新选择！');
        //            return;
        //        } 
        else {
            //结果类型，Poll：污染源 Air：大气
            var strPA = selectedItem.REMARK_5;
            //strSubTask_Id = selectedSample.SUBTASK_ID;
            strSubTask_Id = selectedItem.ID;
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
            //            $.ligerDialog.open({ title: getItemInfoName(selectedItem.ITEM_ID, "ITEM_NAME") + '样品原始记录表', top: 0, width: 1100, height: 680, buttons:
            //         [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); }
            //         }], url: '../../sampling/QY/OriginalTable/DustyTable.aspx?strSubTask_Id=' + strSubTask_Id + '&strIsView=true&strItem_Id=' + selectedItem.ITEM_ID
            //            });
        }
    }
    //strLinkCode环节编号，01：采样环节；02：监测分析环节；03：分析结果复核环节；04：质控审核环节；05：现场项目结果核录环节；06：分析主任审核环节；07：现场结果复核环节；08：现场室主任审核环节
    function OpenDialog(Title, PageUrl, KeyTable, BaseTable, ItemID, SubTaskID) {
        $.ligerDialog.open({ Title: Title, top: 0, width: 1100, height: 680, buttons:
         [{ text: '关闭', onclick: function (item, dialog) { objFourGrid.set('url', strUrl + "?type=getFourGridInfo&threeGridId=" + objThreeGrid.getSelectedRow().ID); dialog.close(); }
         }], url: PageUrl + '?strSubTask_Id=' + SubTaskID + '&strIsView=true&strItem_Id=' + ItemID + '&strKeyTableName=' + KeyTable + '&strBaseTableName=' + BaseTable + '&strLinkCode=05'
        });
    }
    function f_onAfterEdit(e) {
        //保存数据
        var data = ""
        var columnname = "";
        data = JSON.stringify(e.record);
        columnname = e.column.columnname;
        var fill_id = e.record["ID"];
        var value = e.value;
        if (e.record["__status"] != "nochanged") {
            if (data != "") {
                $.ajax({
                    cache: false,
                    async: false,
                    url: strUrl + "?type=UpdateCellValue&data=" + data + "&strUpdateCell=" + columnname + "&strUpdateCellValue=" + value + "&strInfor_Id=" + fill_id,
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data == "True") {
                            objOneGrid.loadData();
                            objTwoGrid.loadData();
                            objThreeGrid.loadData();
                            objFourGrid.loadData();
                        } else {
                        }
                    }
                });
            }
        }
    }

    //获取监测项目信息
    function getItemInfor(strItemID) {
        var strValue = null;
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../../sampling/SamplePoint.aspx?type=getBaseItemInfor&strItemId=" + strItemID,
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

    function getSampleInfor(strSampleId) {

        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "AnalysisResult.aspx?type=getSampleInfor&strSampleId=" + strSampleId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.Total != "0") {
                    vSampleInfor = data.Rows;
                }
            }
        });
    }
    
});

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

//获取字典项信息
function getItemUnit(strItemID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getItemUnit",
        data: "{'strItemID':'" + strItemID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
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
                $.ligerDialog.success('数据操作成功');
                return;
            }
            else {
                $.ligerDialog.warn('数据操作失败');
                return;
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
        url: "../../../Mis/Monitor/sampling/QY/SubSample.aspx?action=GetSubSampleList&strSampleId=" + SampleId,
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
    $.ligerDialog.open({ title: '子样明细', name: 'winaddtor', width: 700, height: 400, top: 90, url: '../../../Mis/Monitor/sampling/QY/SubSample.aspx?strView=true&strSampleId=' + strId, buttons: [
                { text: '返回', onclick: function (item, dialog) { dialog.close() } }
            ]
    });
}

