// Create by 魏林 2014.03.25  "噪声采样任务"功能

var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strUrl = "Sampling_Noise.aspx";
var WeatherValue = [];
var AttributeTypeList = [];
var strSubTaskID = "";
var strMonitorID = "";
var strLink = "";   //环节标志：Sample-采样环节 Check-现场结果复核环节 QcCheck-现场结果审核环节
var objPointGrid = null, objColumns = null, objToolbar = null, objSource = null;
var iEdit = true;

objSource = [{ text: '机械', id: '0' }, { text: '交通', id: '1' }, { text: '社会生活', id: '2' }, { text: '自然', id: '3' }, { text: '机械、交通', id: '4' }, { text: '机械、社会生活', id: '5' }, { text: '机械、自然', id: '6' }, { text: '交通、社会生活', id: '7' }, { text: '交通、自然', id: '8' }, { text: '社会生活、自然', id: '9'}];

$(document).ready(function () {
    $("#framecenter").ligerTab({ contextmenu: false, onBeforeSelectTabItem: function (tabid) { },
        //在点击选项卡之后触发   点击其他的选项卡后，刷新该选项卡，防止CSS样式被串
        onAfterSelectTabItem: function (tabid) { }
    });

    strSubTaskID = getQueryString("strSubtaskID");
    strMonitorID = getQueryString("strMonitor_ID");
    strLink = getQueryString("Link");

    //委托书信息
    $.post(strUrl + '?type=getContractInfo&strSubtaskID=' + strSubTaskID + '&strLink=' + strLink, function (data) {
        SetContractInfo(data);
    }, 'json');

    //获取任务对应的动态属性
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=getWeatherInfo&strSubtaskID=" + strSubTaskID + "&strMonitorID=" + strMonitorID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            AttributeTypeList = data;
        }
    });
    //获取任务对应的动态属性值
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?type=getWeatherValue&strSubtaskID=" + strSubTaskID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            WeatherValue = data;
        }
    });
    //动态属性信息
    initAttributeControl();
    initAttributeControlValue();

    //设置动态属性控件不可用
    SetAttributeControlDisabled();

    if (strLink == "Sample") {
        objToolbar = { items: [
                { text: '增加', click: showAdd, icon: 'add' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' },
                { line: true },
                { text: '点位图上传', click: upLoadFile, icon: 'fileup' },
                { line: true },
                { text: '点位图下载', click: downLoadFile, icon: 'filedown' },
                { line: true },
                { text: '监测方案下载', click: downLoadSolution, icon: 'filedown' },
                { line: true },
                { text: '特殊样说明', click: SpecialSampleRemark, icon: 'bluebook' },
                { line: true },
                { text: '监测项目设置', click: SetData_Item, icon: 'add' }
                ]
        };
        iEdit = true;
    }
    else {
        objToolbar = { items: [
                { text: '点位图下载', click: downLoadFile, icon: 'filedown' }
                ]
        };
        iEdit = false;
    }
    if (strMonitorID == "000000004") {
        objColumns = [
                 { display: '测点编号', name: 'SAMPLE_CODE', align: 'left', isSort: false, width: 120,
                     editor: {
                         type: 'text'
                     }
                 },
                 { display: '监测点名称', name: 'SAMPLE_NAME', align: 'left', isSort: false, width: 150,
                     editor: {
                         type: 'text'
                     }
                 },
                 { display: '特殊样说明', name: 'SPECIALREMARK', align: 'center', width: 100, render: function (items) {
                     if (items.SPECIALREMARK != "") {
                         return "<a href='javascript:showDetailRemarkSrh(\"" + items.ID + "\",\"" + items.SPECIALREMARK + "\",false)'>查看</a>";
                     }
                 }
                 },
                 { display: '昼间主要声源', width: 120, name: 'D_SOURCE', isSort: false, align: 'left',
                     editor: {
                         type: 'select', valueColumnName: 'text', displayColumnName: 'text',
                         data: objSource
                     }
                 },
                 { display: '夜间主要声源', name: 'N_SOURCE', align: 'left', isSort: false, width: 120,
                     editor: {
                         type: 'select', valueColumnName: 'text', displayColumnName: 'text',
                         data: objSource
                     }
                 }

                ];
    }
    else {
        objColumns = [
                 { display: '测点编号', name: 'SAMPLE_CODE', align: 'left', isSort: false, width: 100
                 },
                 { display: '监测点名称', name: 'SAMPLE_NAME', align: 'left', isSort: false, width: 150,
                     editor: {
                         type: 'text'
                     }
                 },
                 { display: '特殊样说明', name: 'SPECIALREMARK', align: 'center', width: 100, render: function (items) {
                     if (items.SPECIALREMARK != "") {
                         return "<a href='javascript:showDetailRemarkSrh(\"" + items.ID + "\",\"" + items.SPECIALREMARK + "\",false)'>查看</a>";
                     }
                 }
                 },
                 { display: '月', name: 'ENV_MONTH', align: 'left', isSort: false, width: 60,
                     editor: {
                         type: 'text'
                     }
                 },
                 { display: '日', name: 'ENV_DAY', align: 'left', isSort: false, width: 60,
                     editor: {
                         type: 'text'
                     }
                 },
                 { display: '时', name: 'ENV_HOUR', align: 'left', isSort: false, width: 60,
                     editor: {
                         type: 'text'
                     }
                 }
                ];
        if (strMonitorID != "FunctionNoise") {
            objColumns.push({ display: '分', name: 'ENV_MINUTE', align: 'left', isSort: false, width: 60,
                editor: {
                    type: 'text'
                }
            });
        }
    }
    AddColumns();

    objPointGrid = $("#divPoint").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 24,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [10, 20, 30],
        height: '95%',
        enabledEdit: iEdit,
        url: strUrl + '?type=getPoint&strSubtaskID=' + strSubTaskID + '&strMonitorID=' + strMonitorID,
        columns: objColumns,
        toolbar: objToolbar,
        onAfterEdit: SampleEdit,
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
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
    $(".l-layout-header-toggle").css("display", "none");

});
//添加动态列
function AddColumns() {
    var objUnSureColumns = [];
    $.each(objColumns, function (i, n) {
        objUnSureColumns.push(n);
    });
    $.ajax({
        url: strUrl + "?type=GetItemData&strSubtaskID=" + strSubTaskID + "&strMonitorID=" + strMonitorID,
        data: "",
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
                objUnSureColumns.push({ display: n.columnName, name: n.columnId, width: 100, minWidth: 60, align: "center", editor: { type: "text"} });
            });
            //objColumns.push(objUnSureColumns);
            objPointGrid.set("columns", objUnSureColumns);

            $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
        }
    });
}

function SampleEdit(e) {
    var id = e.record.ID;
    var strCellName = e.column.columnname;
    var strCellValue = e.value;

    if (strCellName.indexOf("车流量") != -1) {
        if (strCellValue % 3 != 0) {
            $.ligerDialog.warn('车流量必须是【<a style="color:Red;font-weight:bolder">3</a>】倍数!');
            objPointGrid.updateCell(e.column.columnname, "", e.record);
            return;
        } 
    }
    if (e.record["__status"] != "nochanged") {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "/updateSample",
            data: "{'id':'" + id + "','strCellName':'" + strCellName + "','strCellValue':'" + strCellValue + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == 1) {
                    objPointGrid.cancelEdit(e.rowindex);
                }
            }
        });
    }
}

//初始委托书信息模块
function SetContractInfo(data) {
    $("#divContract").ligerForm({
        inputWidth: 160, labelWidth: 100, space: 30, labelAlign: 'right',
        fields: [
                { display: "合同编号", name: "CONTRACT_CODE", newline: true, type: "text", width: 180, group: "委托信息", groupicon: groupicon },
                { display: "监测类型", name: "MONITOR_NAME", newline: false, width: 150, type: "text" },
                { display: "受检单位", name: "TESTED_COMPANY_ID", newline: true, width: 180, type: "text" },
                { display: "委托类型", name: "CONTRACT_TYPE", newline: false, width: 150, type: "text" },
                { display: "监测目的", name: "TEST_PURPOSE", newline: true, width: 460, type: "text" },
                { display: "联系人", name: "CONTACT_NAME", newline: true, width: 180, type: "text" },
                { display: "联系电话", name: "LINK_PHONE", newline: false, width: 150, type: "text" },
                { display: "采样日期", name: "SAMPLE_ASK_DATE", space: 30, newline: true, width: 180, type: "date" },
                { display: "要求完成日期", name: "SAMPLE_FINISH_DATE", newline: false, width: 150, type: "date" },
                { display: "采样负责人", name: "SAMPLING_MANAGER_ID", newline: true, width: 180, type: "text" },
                { display: "采样人员", name: "SAMPLING_MAN", newline: false, width: 150, type: "text"}]
    });


    //赋值
    if (data) {
        $("#CONTRACT_CODE").val(data.REMARK1);
        $("#MONITOR_NAME").val(data.MONITOR_ID);
        $("#CONTRACT_TYPE").val(data.REMARK2);
        $("#TESTED_COMPANY_ID").val(data.REMARK3);
        $("#CONTACT_NAME").val(data.REMARK4);
        $("#LINK_PHONE").val(data.REMARK5);
        if (strLink == "Sample") {
            $("#SAMPLE_ASK_DATE").val(null);
        }
        else {
            $("#SAMPLE_ASK_DATE").val(data.SAMPLE_ASK_DATE);
        }
        $("#SAMPLE_FINISH_DATE").val(data.SAMPLE_FINISH_DATE);
        $("#SAMPLING_MANAGER_ID").val(data.SAMPLING_MANAGER_ID);
        $("#SAMPLING_MAN").val(data.SAMPLING_MAN);
        $("#TEST_PURPOSE").val(data.SAMPLE_APPROVE_INFO);

        $("#lbSuggestion")[0].innerText = data.SAMPLING_METHOD;
    }

    $("#SAMPLE_ASK_DATE").ligerGetDateEditorManager().bind('changedate', function (v) {
        updateDate(v, '');
    });
    $("#SAMPLE_FINISH_DATE").ligerGetDateEditorManager().bind('changedate', function (v) {
        updateDate('', v);
    });

    $("#CONTRACT_CODE").ligerGetComboBoxManager().setDisabled();
    $("#MONITOR_NAME").ligerGetComboBoxManager().setDisabled();
    $("#CONTRACT_TYPE").ligerGetComboBoxManager().setDisabled();
    $("#TESTED_COMPANY_ID").ligerGetComboBoxManager().setDisabled();
    $("#CONTACT_NAME").ligerGetComboBoxManager().setDisabled();
    $("#LINK_PHONE").ligerGetComboBoxManager().setDisabled();
    $("#SAMPLING_MANAGER_ID").ligerGetComboBoxManager().setDisabled();
    $("#SAMPLING_MAN").ligerGetComboBoxManager().setDisabled();
    $("#TEST_PURPOSE").ligerGetComboBoxManager().setDisabled();

    if (strLink != "Sample") {
        $("#SAMPLE_ASK_DATE").ligerGetComboBoxManager().setDisabled();
        $("#SAMPLE_FINISH_DATE").ligerGetComboBoxManager().setDisabled();
    }

}

function getNewAskDate() {
    var strDate = "";
    strDate = $("#SAMPLE_ASK_DATE").val();
    return strDate;
}

//修改任务日期信息
function updateDate(strAskDate, strFinishDate) {

    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "/updateDate",
        data: "{'strSubTaskID':'" + strSubTaskID + "','strAskDate':'" + strAskDate + "','strFinishDate':'" + strFinishDate + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            //strValue = data.d;
        }
    });
}

//根据指定的动态属性，初始化动态属性控件
function initAttributeControl() {
    var dataJson = GetAttributeControlJson(AttributeTypeList);

    $("#divLocale").html("");

    var obj1 = $("#divLocale").ligerForm(dataJson);
    //obj1._render();
}
//根据指定的动态属性集合，生成json，以动态生成控件
function GetAttributeControlJson(data) {
    var strdata = { inputWidth: 160, labelWidth: 100, space: 90, labelAlign: 'right', fields: [] };

    if (data.length > 0) {
        for (i = 0; i < data.length; i++) {
            var strNewLine = false;
            var strdata1 = "";
            if (i % 2 == 0) {
                strNewLine = true;
            }
            else {
                strNewLine = false;
            }
            if (data[i].DICT_TEXT == "有雨" || data[i].DICT_TEXT == "天气" || data[i].DICT_TEXT == "采样方式" || data[i].DICT_TEXT == "采样方法依据" || data[i].DICT_TEXT == "声级计型号") {
                strdata1 += "{ display: '" + data[i].DICT_TEXT + "',name: '" + data[i].DICT_CODE + "-1', newline:" + strNewLine + ", width: 160, type: 'select',";
                strdata1 += "comboboxName: '" + data[i].DICT_CODE + "',options:";

                if (data[i].DICT_TEXT == "有雨") {
                    strdata1 += "{ data: [{ text: '是', id: '0' }, { text: '否', id: '1'}]}";
                }
                if (data[i].DICT_TEXT == "天气") {
                    strdata1 += "{ data: [{ text: '晴', id: '0' }, { text: '阴', id: '1'}, { text: '多云', id: '2'}, { text: '雨', id: '3'}]}";
                }
                if (data[i].DICT_TEXT == "采样方式") {
                    strdata1 += "{ data: [{ text: '手工瞬时', id: '0' }, { text: '动压平衡法', id: '1'}]}";
                }
                if (data[i].DICT_TEXT == "采样方法依据" && data[i].DICT_TYPE == "noise_weather") {
                    strdata1 += '{ valueFieldID: "WAY_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: strUrl + "?type=GetDict&dictType=noise_way", isShowCheckBox: true, isMultiSelect: true }';
                }
                if (data[i].DICT_TEXT == "声级计型号" && data[i].DICT_TYPE == "noise_weather") {
                    strdata1 += "{ data: [{ text: '噪声统计分析仪（AWA6218B）', id: '0' }, { text: '多功能声级计(AWA6228)', id: '1'}]}";
                }
                if (data[i].DICT_TEXT == "采样方法依据" && data[i].DICT_TYPE == "gerenal_weather") {
                    if (strMonitorID == "000000001") {
                        strdata1 += '{ valueFieldID: "WAY_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: strUrl + "?type=GetDict&dictType=water_way", isShowCheckBox: true, isMultiSelect: true }';
                    }
                    else if (strMonitorID == "000000002") {
                        strdata1 += '{ valueFieldID: "WAY_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: strUrl + "?type=GetDict&dictType=gas_way", isShowCheckBox: true, isMultiSelect: true }';
                    }
                    else {
                        strdata1 += "{ valueFieldID: 'WAY_ID', data: [{ text: 'HJ 494-2009', id: '0' }, { text: 'GB/T 16844-1996', id: 'GB/T 16844-1996'}]}";
                    }
                }
                if (i == 0)
                    strdata1 += ", group: '现状信息', groupicon: groupicon";
                strdata1 += "}";
            }
            else {
                strdata1 += "{ display: '" + data[i].DICT_TEXT + "', name: '" + data[i].DICT_CODE + "', newline: " + strNewLine + ", width: 160, type: 'text'";
                if (i == 0)
                    strdata1 += ", group: '现状信息', groupicon: groupicon";
                strdata1 += "}";
            }
            var strJsonData = eval('(' + strdata1 + ')');
            strdata.fields.push(strJsonData);
        }
    }
    return strdata;
}
//给动态属性控件赋值
function initAttributeControlValue() {
    for (i = 0; i < AttributeTypeList.length; i++) {
        for (j = 0; j < WeatherValue.length; j++) {
            if (AttributeTypeList[i].DICT_CODE == WeatherValue[j].WEATHER_ITEM) {
                if (AttributeTypeList[i].DICT_CODE == "noise_way" || AttributeTypeList[i].DICT_CODE == "gerenal_way") {
                    $("#WAY_ID").val(WeatherValue[j].WEATHER_INFO);
                }
                else {
                    $("#" + AttributeTypeList[i].DICT_CODE).val(WeatherValue[j].WEATHER_INFO);
                }
            }
        }
    }
}
//设置动态属性控件不可用
function SetAttributeControlDisabled() {
    if (strLink != "Sample") {
        
        for (i = 0; i < AttributeTypeList.length; i++) {
            $("#" + AttributeTypeList[i].DICT_CODE).ligerGetComboBoxManager().setDisabled();
        }
    }
}

//弹出新增窗口
var addDialog = null;
function showAdd() {
    if (addDialog) {
        addDialog.show();
    } else {
        //构建查询表单
        $("#addForm").ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "监测点", name: "POINT_NAME", newline: true, type: "text", group: "新增信息", groupicon: groupicon },
                      { display: "监测类型", name: "MONITOR_TYPE", newline: true, type: "text" }
                 ]
        });
        $("#MONITOR_TYPE").val("噪声");
        $("#MONITOR_TYPE").ligerGetTextBoxManager().setDisabled();

        addDialog = $.ligerDialog.open({
            target: $("#addDiv"),
            width: 350, height: 200, top: 90, title: "新增监测点",
            buttons: [
                  { text: '确定', onclick: f_SaveDatePoint },
                  { text: '取消', onclick: function () { addDialog.hide(); } }
                  ]
        });
    }
}

//save函数
function f_SaveDatePoint(item, dialog) {
    $.ajax({
        cache: false,
        type: "POST",
        url: strUrl + "/SaveDataPoint",
        data: "{'strSubTaskID':'" + strSubTaskID + "','strPointName':'" + $("#POINT_NAME").val() + "','strMonitorID':'" + strMonitorID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                $("#POINT_NAME").val('');
                parent.$.ligerDialog.success('数据保存成功');
                dialog.hide();
                objPointGrid.loadData();
            }
            else {
                parent.$.ligerDialog.warn('数据保存失败');
            }
        }
    });
}
//设置监测项目
function SetData_Item() {
    if (objPointGrid.rows.length == 0) {
        return;
    }
    
    $.ligerDialog.open({ title: '设置监测项目', top: 0, width: 500, height: 380, buttons:
        [{ text: '确定', onclick: f_SaveDateItem },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'SamplePointItemEdit.aspx?PointID=' + objPointGrid.rows[0].ID + '&SubtaskID=' + strSubTaskID
    });
}
//save函数
function f_SaveDateItem(item, dialog) {
    var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
    var strdata = fn();

    var strPointIDs = '';
    for (var i = 0; i < objPointGrid.rows.length; i++) {
        if (i == objPointGrid.rows.length - 1) {
            strPointIDs += objPointGrid.rows[i].ID;
        }
        else {
            strPointIDs += objPointGrid.rows[i].ID + ",";
        }
    }

    strdata = "{'strSubTaskID':'" + strSubTaskID + "','strPointIDs':'" + strPointIDs + "'," + strdata + "}";
    $.ajax({
        cache: false,
        type: "POST",
        url: strUrl + "/SaveDataItem",
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                AddColumns();
                objPointGrid.loadData();
                parent.$.ligerDialog.success('数据保存成功');
                dialog.close();
            }
            else {
                parent.$.ligerDialog.warn('数据保存失败');
            }
        }
    });
}

//删除数据
function deleteData() {
    if (objPointGrid.getSelectedRow() == null) {
        parent.$.ligerDialog.warn('请选择一条记录进行删除');
        return;
    }
    parent.$.ligerDialog.confirm("确认删除点位信息吗？", function (yes) {
        if (yes == true) {
            var strValue = objPointGrid.getSelectedRow().ID;
            $.ajax({
                cache: false,
                type: "POST",
                url: strUrl+"/deletePoint",
                data: "{'strValue':'" + strValue + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                        objPointGrid.loadData();
                        parent.$.ligerDialog.success('删除数据成功')
                    }
                    else {
                        parent.$.ligerDialog.warn('删除数据失败');
                    }
                }
            });
        }
    });
}
//特殊样品说明
function SpecialSampleRemark() {
    if (objPointGrid.getSelectedRow() == null) {
        parent.$.ligerDialog.warn('请选择一条记录进行操作!');
        return;
    }
    else {
        var strValue = objPointGrid.getSelectedRow().ID;
        var oldRemark = objPointGrid.getSelectedRow().SPECIALREMARK;
        showDetailRemarkSrh(strValue, oldRemark, true);
    }
}
//设置grid 的弹出特殊样说明录入对话框
var detailRemarkWinSrh = null;
function showDetailRemarkSrh(strSampleID, oldRemark, isAdd) {
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
                  { text: '确定', onclick: function () { SaveRemark(strSampleID); } },
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
function SaveRemark(strSampleID) {
    var strRemark = $("#SEA_REMARK").val();
    $.ajax({
        cache: false,
        type: "POST",
        url: strUrl + "/SaveRemark",
        data: "{'strValue':'" + strSampleID + "','strRemark':'" + strRemark + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "true") {
                objPointGrid.loadData();
                clearRemarkDialogValue();
                detailRemarkWinSrh.hide();
                parent.$.ligerDialog.success('数据操作成功')
            }
            else {
                parent.$.ligerDialog.warn('数据操作失败');
            }
        }
    });
}
///附件上传
function upLoadFile() {
    $.ligerDialog.open({ title: '点位图上传', width: 500, height: 270, top: 50, isHidden: false,
        buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        $("iframe")[0].contentWindow.upLoadFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../../OA/ATT/AttFileUpload.aspx?filetype=PointMap&id=' + strSubTaskID
    });
}
///附件下载
function downLoadFile() {
    $.ligerDialog.open({ title: '点位图下载', width: 500, height: 270, top: 50, isHidden: false,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../../OA/ATT/AttFileDownLoad.aspx?filetype=PointMap&id=' + strSubTaskID
    });
}
function getCompanyID(strID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl+"?type=getCompanyID&strSubTaskId=" + strID,
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data, textStatus) {
            if (data != "") {
                strValue = data;
            }
        }
    });
    return strValue;
}
///监测方案下载
function downLoadSolution() {
    $.ligerDialog.open({ title: '监测方案下载', width: 500, height: 270, top: 50,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../../OA/ATT/AttFileDownLoad.aspx?filetype=Contract&id=' + strSubTaskID
    });
}

//弹出发送人窗口
var sendhDialog = null;
function showSend() {
    if (strLink == "Sample") {
        var strdate = getNewAskDate(); //郭海华修改提醒功能
        if (!strdate && strMonitorID == "000000004") {
            $.ligerDialog.warn('采样日期不能为空！');
            return;
        }
        if (sendhDialog) {
            sendhDialog.show();
        } else {

            //构建发送人表单
            $("#sendForm").ligerForm({
                inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
                fields: [
                      { display: "现场复核人", name: "ddlUser", newline: true, type: "select", comboboxName: "ddlUserBox", options: { valueFieldID: "hidUser", valueField: "ID", textField: "REAL_NAME", resize: false, url: strUrl + "?type=GetCheckUser&MonitorID=" + strMonitorID + "&Link=" + strLink} }
                 ]
            });

            sendhDialog = $.ligerDialog.open({
                target: $("#sendDiv"),
                width: 300, height: 160, top: 90, title: "选择现场复核人",
                buttons: [
                        { text: '确定', onclick: function (item, dialog) {
                            if ($("#hidUser").val() == "") {
                                $.ligerDialog.warn('请选择现场复核人');
                                return;
                            }
                            dialog.hide();
                            SendClick();
                        }
                        },
                        { text: '取消', onclick: function (item, dialog) { dialog.hide(); } }
                    ]
            });
        }
    }
    else if (strLink == "Check") {
        if (sendhDialog) {
            sendhDialog.show();
        } else {

            //构建发送人表单
            $("#sendForm").ligerForm({
                inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
                fields: [
                      { display: "审核人", name: "ddlUser", newline: true, type: "select", comboboxName: "ddlUserBox", options: { valueFieldID: "hidUser", valueField: "ID", textField: "REAL_NAME", resize: false, url: strUrl + "?type=GetCheckUser&MonitorID=" + strMonitorID + "&Link=" + strLink} }
                 ]
            });

            sendhDialog = $.ligerDialog.open({
                target: $("#sendDiv"),
                width: 300, height: 160, top: 90, title: "选择审核人",
                buttons: [
                        { text: '确定', onclick: function (item, dialog) {
                            if ($("#hidUser").val() == "") {
                                $.ligerDialog.warn('请选择审核人');
                                return;
                            }
                            dialog.hide();
                            SendClick();
                        }
                        },
                        { text: '取消', onclick: function (item, dialog) { dialog.hide(); } }
                    ]
            });
        }
    }
    else  {
        SendClick();
    }
}

function SendClick() {
    $.ligerDialog.confirm('您确定要将该任务发送至下一环节吗？', function (yes) {
        if (yes == true) {
            var btnClick = "";
            if ($("#IS_BACK").val() == "1")
                btnClick = "btnBackSendClick";
            else
                btnClick = "btnSendClick";

            var fn = GetAttrStr(); 
            
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/" + btnClick,
                data: "{'strLink':'" + strLink + "','strSubtaskID':'" + strSubTaskID + "','strUserID':'" + $("#hidUser").val() + "'" + fn + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    var strValue = data.d;
                    var objValue = eval(strValue)[0];
                    if (objValue.result == "1") {
                        $.ligerDialog.confirmWF('发送成功,发送至【<a style="color:Red;font-weight:bold">' + objValue.msg + '</a>】环节', '提示', function (result) { if (result) { top.f_removeSelectedTabs(); } })
                        return;
                    }
                    else {
                        $.ligerDialog.warn(objValue.msg, '发送失败');
                        return;
                    }
                }
            });
        }
    });
}

//得到动态属性保存参数
function GetAttrStr() {
    var strData = "";
    var newData = new Array();
    for (i = 0; i < AttributeTypeList.length; i++) {
        newData.push(AttributeTypeList[i]);
    }

    if (newData.length > 0) {
        for (i = 0; i < newData.length; i++) {
            var strdata1 = "";
            if (newData[i].DICT_CODE == "noise_way" || newData[i].DICT_CODE == "gerenal_way") {
                strdata1 += newData[i].DICT_CODE + "|" + $("#WAY_ID").val();
            }
            else {
                strdata1 += newData[i].DICT_CODE + "|" + $("#" + newData[i].DICT_CODE).val();
            }
            strData += (strData.length > 0 ? "-" : "") + strdata1;
        }
    }
    if (strData.length > 0) {
        strData = ",'strAttribute':'" + strData + "'";
    }
    else
        strData = "'strAttribute':''";

    return strData;
}

function BackClick() {
    
    $.ligerDialog.prompt('退回意见', '', true, function (yes, value) {
        if (yes) {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/btnBackClick",
                data: "{'strLink':'" + strLink + "','strSubtaskID':'" + strSubTaskID + "','strSuggestion':'" + value + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    var strValue = data.d;
                    var objValue = eval(strValue)[0];
                    if (objValue.result == "1") {
                        $.ligerDialog.confirmWF('退回成功,退回至【<a style="color:Red;font-weight:bold">' + objValue.msg + '</a>】环节', '提示', function (result) { if (result) { top.f_removeSelectedTabs(); } })
                        return;
                    }
                    else {
                        $.ligerDialog.warn('退回失败');
                        return;
                    }
                }
            });
        }
    });
}