// Create by 魏林 2014.01.21  "分析结果录入\分析结果校核"功能
var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;

var strUrl = "AnalysisResult.aspx";
var strOneGridTitle = "样品号";
var ResultId = "";
//定义分析方法数据集对象
var ComboBoxValue = "";
var ComboBoxText = "";
var vSampleInfor = null, strSubTask_Id = "";
var objResultID = "";
var a = "", b = "";
//样品号

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
function Save() {
    return true;
}

$(document).ready(function () {
    objResultID = $("#cphData_RESULT_ID").val();

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
        url: strUrl + '?type=getOneGridInfo&strResultID=' + objResultID,
        columns: [
                 { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 180, minWidth: 60, frozen: true, render: function (record) {
                     return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
                 }
                 },
                 { display: '样品数', name: 'SAMPLE_COUNT', align: 'left', width: 100, minWidth: 60, frozen: true },
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
            //            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ID);
            //            objThreeGrid.set("data", emptyArray);

            //黄飞 20150721 获取监测项目ID
            a = rowdata.ITEM_ID;
            strSampleid = "";
            objTwoGrid.set('url', strUrl + "?type=getTwoGridInfo&oneGridId=" + rowdata.ITEM_ID + "&strResultID=" + objResultID);
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
        url: "../../../Mis/Monitor/sampling/QY/SamplePoint.aspx?type=getBaseItemInfor&strItemId=" + strItemID,
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
                { text: '前处理说明', click: DoSampleRemark, icon: 'bookadd' },
                //{ text: '获取仪器结果', click: GetLims, icon: 'bookadd' },
                //{ text: '查看仪器数据', click: ShowLims, icon: 'attibutes' },
                { text: '查看时限要求', click: ChkTimeMgm, icon: 'attibutes' },
                { text: '上传附件', click: Upload, icon: 'attibutes' },
                { text: '下载附件', click: DownLoad, icon: 'attibutes' }
                ]
        },
        columns: [
        //{ display: '样品条码', name: 'SAMPLE_BARCODE', align: 'left', width: 160, minWidth: 40, frozen: true },
                 {display: '样品编号', name: 'SAMPLE_CODE', align: 'left', width: 160, minWidth: 40, frozen: true },
        //                 { display: '样品记录单', name: 'DATAINFO', align: 'left', width: 100, minWidth: 40,
        //                     render: function (record, rowindex, value) {
        //                         return "<a href=\"javascript:EditOriginalInfo()\">编辑</a> ";
        //                     }
        //                 },
                {display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60,
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
                                        $.ligerDialog.open({ title: '选择分析方法', name: 'winselector', width: 700, height: 370, url: '../../../Mis/Monitor/Result/SelectItemAnalysis.aspx?ItemID=' + rowdata.ITEM_ID, buttons: [
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
                     }
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
                 }
                ],
        onContextmenu: function (parm, e) {

        },
        onDblClickRow: function (data, rowindex, rowobj) {

        },
        onCheckRow: function (checked, rowdata, rowindex) {
            if (checked) {
                //点击的时候加载内控信息
                b = rowdata.SAMPLE_ID;
                strSampleid = b + "," + strSampleid;
                objThreeGrid.set('url', strUrl + "?type=getThreeGridInfo&twoGridId=" + rowdata.ID);
                ResultId = rowdata.ID;


            }
        },
        onAfterEdit: AfterEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return true; }
    });
    //$(".l-grid-hd-cell-btn-checkbox").css("display", "none");


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

        //        if (selectedSample.REMARK_3 != "1") {
        //            $.ligerDialog.warn('请先获取仪器结果！');
        //            return;
        //        }

        $.ligerDialog.open({ title: '仪器数据查看', top: 0, width: 750, height: 450, buttons:
        [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); } }],
            url: 'AnalysisResult_Lims.aspx?ResultID=' + objTwoGrid.ID
        });
    }

    //by yinchengyi 2015-9-18 查看时限要求
    function ChkTimeMgm() {

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

        $.ligerDialog.open({ title: "时限管理", width: 800, height: 300, buttons:
                [
                { text:
                '关闭', onclick: function (item, dialog) { dialog.close(); }
                }], url: "../../ProcessMgm/ProcessMgm.aspx?strResultID=" + selectedSample.ID
        });
    }

    function Upload() {
        var selectedItemSample = objOneGrid.getSelectedRow();
        if (!selectedItemSample) {
            $.ligerDialog.warn('请先选择样品编号！');
            return;
        }
        var selectedItem = objTwoGrid.getSelectedRow();
        if (!selectedItem) {
            $.ligerDialog.warn('请先选择监测项目！');
            return;
        }

        var c = strSampleid.substring(0, strSampleid.length - 1);
        var filetype = c; //类型
        Doc_ID = a; //获取ID

        //alert(filetype + "|" + Doc_ID + "|" + b);
        $.ligerDialog.open({ title: '附件上传', width: 500, height: 250, isHidden: false,
            buttons: [
             { text: '上传', onclick: function (item, dialog) {
                 dialog.frame.upLoadFile();
                 alert("上传成功");
                 //GetData(filetype);
                 dialog.close();
             }
             },
                { text: '关闭',
                    onclick: function (item, dialog) { dialog.close(); }
                }], url: '../../../OA/ATT/AttFileUpload.aspx?id=' + Doc_ID + '&filetype=' + filetype
        });
    }
    function getTaskID(strSubtask) {
        alert(strSubtask);
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/getTaskID",
            data: "{'strSubtask':'" + strSubtask + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }

    function DownLoad() {
        var selectedItemSample = objOneGrid.getSelectedRow();
        if (!selectedItemSample) {
            $.ligerDialog.warn('请先选择监测样品！');
            return;
        }
        var selectedItem = objTwoGrid.getSelectedRow();
        if (!selectedItem) {
            $.ligerDialog.warn('请先选择样品！');
            return;
        }
        var c = strSampleid.substring(0, strSampleid.length - 1);
        var filetype = c; //类型
        Doc_ID = a; //获取ID
        //alert(filetype + "|" + Doc_ID);
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?id=' + Doc_ID + '&filetype=' + filetype
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
        var strFinishDate = e.record.FINISH_DATE;
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
        }], url: "../../../Mis/Monitor/Result/QcSetting_QY.aspx?ResultJson=" + ResultJson
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
        }], url: "../../../Mis/Monitor/Result/QY/QcEditing_QY.aspx?ID=" + objThreeGrid.getSelectedRow().ID + "&QC_TYPE=" + objThreeGrid.getSelectedRow().QC_TYPE
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
        }], url: "../../../Mis/Monitor/Result/DefaultUserEx.aspx?strResultId=" + strResultId
    });
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

//弹出选择分析完成时间
function getFinishDate(strColumnName, iRow) {
    $.ligerDialog.open({ title: "录入分析完成时间", width: 400, height: 300, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            objTwoGrid.updateCell(strColumnName, $("iframe")[0].contentWindow.GetDate(), iRow);

            var id = objTwoGrid.rows[iRow].ID;
            //var strItemResult = objTwoGrid.rows[iRow].ITEM_RESULT;
            //var strAnalysisMethod = objTwoGrid.rows[iRow].ANALYSIS_METHOD_ID;
            var strFinishDate = objTwoGrid.rows[iRow].FINISH_DATE;
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/saveItemFinishDate",
                //data: "{'id':'" + id + "','strItemResult':'" + strItemResult + "','strAnalysisMethod':'" + strAnalysisMethod + "','strFinishDate':'" + strFinishDate + "'}",
                data: "{'id':'" + id + "','strFinishDate':'" + strFinishDate + "'}",
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
        }], url: "../../../Mis/Monitor/Result/FinishDateSelected.aspx"
    });
}
function EditOriginalInfo() {

    window.open("AnalysisOriginalInfo2.aspx?ccflowWorkId=" + $.getUrlVar('WorkID') + "&ccflowFid=" + $.getUrlVar('FID'));
}


function getTaskID(strSubtask) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getTaskID",
        data: "{'strSubtask':'" + strSubtask + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

function getItemId(strSubtask) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/getItemId",
        data: "{'strSubtask':'" + strSubtask + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}