// Create by 苏成斌 2012.12.14  "监测任务点位信息管理"功能

var objPointGrid = null;
var strPointId = "";
var strSubtaskID = "", strMonitor_ID = "";
var strUrl = "SamplePoint.aspx"
var objSubItems = null, objToolbar = null, objColumns = null;
var strRowdataID = "";
var strCCflowWorkId = '';
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


//点位信息管理功能
$(document).ready(function () {
    $("#layout1").ligerLayout({ leftWidth: 670, height: '100%', topHeight: 200 });

    strSubtaskID = $.getUrlVar('strSubtaskID');
    strMonitor_ID = $.getUrlVar('strMonitor_ID');
    strCCflowWorkId = $.getUrlVar('ccflowWorkId');
    //构建查询表单
    $("#dateForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "", name: "DATE_CODE", newline: true, type: "text" }
                 ]
    });

    if (strMonitor_ID == "000000004") {//如果是000000004则为噪声
        objToolbar = { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' },
                { line: true },
                { text: '停产', click: discontinued, icon: 'gridwarning' },
                { line: true },
                { text: '点位图上传', click: upLoadFile, icon: 'fileup' },
                { line: true },
                { text: '点位图下载', click: downLoadFile, icon: 'filedown' },
                { line: true },
                //{ text: '监测方案下载', click: downLoadSolution, icon: 'filedown' },
                { line: true },
                { text: '特殊样说明', click: SpecialSampleRemark, icon: 'bluebook'}//,
            //{ line: true },
            //{ text: '添加子样', click: AddSubSample, icon: 'database_wrench' }
                ]
        }

        objColumns = [
                 { display: '监测点位', name: 'POINT_ID', align: 'left', isSort: false, width: 120, render: function (record) {
                     return getPointName(record.POINT_ID);
                 }
                 },
                 { display: '样品名称', name: 'SAMPLE_NAME', align: 'left', isSort: false, width: 150,
                     editor: {
                         type: 'text'
                     }
                 },
        //{ display: '样品编号', name: 'SAMPLE_CODE', align: 'left', isSort: false, width: 150 },
                 {display: '原样编号', name: 'QC_SOURCE_CODE', align: 'left', isSort: false, width: 140, render: function (record) {
                     return getQcSourceCode(record.QC_SOURCE_ID);
                 }
             },
                 { display: '质控类型', name: 'QC_TYPE', align: 'left', isSort: false, width: 100, render: function (record) {
                     return GetQcType(record.QC_TYPE);
                 }
                 },
                 { display: '特殊样说明', name: 'RemarkView', align: 'center', width: 100, render: function (items) {
                     if (items.SPECIALREMARK != "") {
                         return "<a href='javascript:showDetailRemarkSrh(\"" + items.ID + "\",\"" + items.SPECIALREMARK + "\",false)'>查看</a>";
                     }
                 }
                 }//,
        //{ display: '子样', name: 'SUBSAMPLE', align: 'center', width: 100, render: function (items) {
        //    if (getSubSample(items.ID) != null) {
        //        return "<a href='javascript:ShowSubSample(\"" + items.ID + "\")'>明细</a>";
        //}
        //}
        //}
                ]
    } else {
        objToolbar = { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' },
                { line: true },
                { text: '停产', click: discontinued, icon: 'gridwarning' },
                //{ line: true },
                //{ text: '监测方案下载', click: downLoadSolution, icon: 'filedown' },
                { line: true },
                { text: '特殊样说明', click: SpecialSampleRemark, icon: 'bluebook' },
                // line: true },
                //{ text: '添加子样', click: AddSubSample, icon: 'database_wrench' },
                //{ line: true },
                //{ text: '修改质控标准', click: editQcInfo, icon: 'database_wrench' },
                //{ line: true },
                //{ text: '修改编号', click: showDate, icon: 'database_wrench' },
                { line: true },
                { text: '拆分项目', click: showItemSplit, icon: 'database_wrench' },
                { line: true },
                { text: '现场平行', click: addQcTwinInfo, icon: 'add' },
                { line: true },
                { text: '现场空白', click: addQcEmptyInfo, icon: 'add' },
                { line: true },
                { text: '现场加标', click: addQcAddInfo, icon: 'add' },
                { line: true },
                { text: '现场密码', click: addQcBlindInfo, icon: 'add' },
                { line: true },
                { text: '上传原始记录表', click: Upload, icon: 'add' },
                { line: true },
                { text: '下载原始记录表', click: DownLoad, icon: 'add' }

                ]
        }

        objColumns = [
                 { display: '监测点位', name: 'POINT_ID', align: 'left', isSort: false, width: 120, frozen: true, render: function (record) {
                     return getPointName(record.POINT_ID);
                 }
                 },
                 { display: '样品名称', name: 'SAMPLE_NAME', align: 'left', isSort: false, frozen: true, width: 150,
                     editor: {
                         type: 'text'
                     }
                 },
                 { display: '样品编号', name: 'SAMPLE_CODE', align: 'left', isSort: false, frozen: true, width: 150,
                     editor: {
                         type: 'text'
                     }
                 },
                 { display: '原样编号', name: 'QC_SOURCE_CODE', align: 'left', isSort: false, width: 140, render: function (record) {
                     return getQcSourceCode(record.QC_SOURCE_ID);
                 }
                 },
                 { display: '质控类型', name: 'QC_TYPE', align: 'left', isSort: false, width: 100, render: function (record) {
                     return GetQcType(record.QC_TYPE);
                 }
                 },
                 { display: '特殊样说明', name: 'RemarkView', align: 'center', width: 100, render: function (items) {
                     if (items.SPECIALREMARK != "") {
                         return "<a href='javascript:showDetailRemarkSrh(\"" + items.ID + "\",\"" + items.SPECIALREMARK + "\",false)'>查看</a>";
                     }
                 }
                 },
                { display: '子样', name: 'SUBSAMPLE', align: 'center', width: 100, render: function (items) {
                    if (getSubSample(items.ID) != null) {
                        return "<a href='javascript:ShowSubSample(\"" + items.ID + "\")'>明细</a>";
                    }
                }
                },
                 { display: '采样时间<a style="color:red;padding-left:5px;">*</a>', name: 'SAMPLE_ACCEPT_DATEORACC', align: 'left', isSort: false, width: 80,
                     editor: {
                         type: 'text'
                     }
                 },
                 { display: '样品份数', name: 'SAMPLE_COUNT', align: 'left', isSort: false, width: 80,
                     editor: {
                         type: 'text'
                     }
                 },
                 { display: '废气排放量', name: 'REMARK5', hide: true, align: 'left', isSort: false, width: 80,
                     editor: {
                         type: 'text'
                     }
                 }
                ];

    }

    //获取动态属性数据动态生成列
    if (strMonitor_ID == "000000001" || strMonitor_ID == "EnvDrinkingSource") {
        var objUnSureColumns = [];
        $.ajax({
            url: "SamplePoint.aspx",
            data: "type=GetAttData&Type_ID=000000017",   //感官描述属性：000000017  流量测定情况：000000210
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
                        objUnSureColumns.push({ display: n.columnName + '<a style="color:red;padding-left:5px;">*</a>', name: n.columnId, width: 80, minWidth: 60, align: "center", editor: { type: "select", valueField: "DICT_CODE", textField: "DICT_TEXT", data: DictJson(objStr[4].toString()) },
                            render: function (item, i, v) {
                                return getDictName(v, objStr[4].toString());
                            }
                        });
                });
                //objColumns.push({ display: '流量测定情况、感官描述', columns: objUnSureColumns });
                objColumns.push({ display: '感官描述', columns: objUnSureColumns });
                objPointGrid.set("columns", objColumns);
                objPointGrid.toggleCol("POINT_ID");
                objPointGrid.toggleCol("QC_TYPE");
                objPointGrid.toggleCol("REMARK5");
            }
        });
    }
    //获取动态属性数据动态生成列
    if (strMonitor_ID == "EnvRiver") {
        var objUnSureColumns = [];
        $.ajax({
            url: "SamplePoint.aspx",
            data: "type=GetAttData&Type_ID=000000211",   //感官描述属性：000000211 
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
                        objUnSureColumns.push({ display: n.columnName, name: n.columnId, width: 100, minWidth: 60, align: "center", editor: { type: "text"} });
                    else
                        objUnSureColumns.push({ display: n.columnName + '<a style="color:red;padding-left:5px;">*</a>', name: n.columnId, width: 80, minWidth: 60, align: "center", editor: { type: "select", valueField: "DICT_CODE", textField: "DICT_TEXT", data: DictJson(objStr[4].toString()) },
                            render: function (item, i, v) {
                                return getDictName(v, objStr[4].toString());
                            }
                        });
                });
                objColumns.push({ display: '感官描述', columns: objUnSureColumns });
                objPointGrid.set("columns", objColumns);
                objPointGrid.toggleCol("POINT_ID");
                objPointGrid.toggleCol("QC_TYPE");
                objPointGrid.toggleCol("REMARK5");
            }
        });
    }

    //by yinchengyi 2015-9-16 承德感官描述
    //获取动态属性数据动态生成列--污水处理厂
    var GetAttDataType = "";
    if (strMonitor_ID == "SewagePlant") {
        GetAttDataType = "000000212";
    }
    else if (strMonitor_ID == "EnvSurWater") {
        GetAttDataType = "000000213";
    }
    else if (strMonitor_ID == "SensitiveGroundwater") {
        GetAttDataType = "000000214";
    }
    if (GetAttDataType != "") {
        var objUnSureColumns = [];
        $.ajax({
            url: "SamplePoint.aspx",
            data: "type=GetAttData&Type_ID=000000212",   //感官描述属性：
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
                        objUnSureColumns.push({ display: n.columnName, name: n.columnId, width: 100, minWidth: 60, align: "center", editor: { type: "text"} });
                    else
                        objUnSureColumns.push({ display: n.columnName + '<a style="color:red;padding-left:5px;">*</a>', name: n.columnId, width: 80, minWidth: 60, align: "center", editor: { type: "select", valueField: "DICT_CODE", textField: "DICT_TEXT", data: DictJson(objStr[4].toString()) },
                            render: function (item, i, v) {
                                return getDictName(v, objStr[4].toString());
                            }
                        });
                });
                objColumns.push({ display: '感官描述', columns: objUnSureColumns });
                objPointGrid.set("columns", objColumns);
                objPointGrid.toggleCol("POINT_ID");
                objPointGrid.toggleCol("QC_TYPE");
                objPointGrid.toggleCol("REMARK5");
            }
        });
    }


    //监测点位grid的菜单
    var objPointmenu = $.ligerMenu({ width: 120, items:
            [
            { text: '修改', click: updateData, icon: 'modify' },
            { line: true },
            { text: '删除', click: deleteData, icon: 'delete' }
            ]
    });


    objPointGrid = $("#PointGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        enabledEdit: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [10, 15, 20],
        height: '96%',
        enabledEdit: true,
        url: 'SamplePoint.aspx?type=getPoint&strSubtaskID=' + strSubtaskID + '&strCCflowWorkId=' + strCCflowWorkId,
        columns: objColumns,
        toolbar: objToolbar,
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            strPointId = parm.data.POINT_ID;
            objPointmenu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            strPointId = data.POINT_ID;
            updateData();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            strPointId = rowdata.POINT_ID;
            var strSampleID = rowdata.ID;
            strRowdataID = rowdata.ID;
            ItemGrid.set('url', "SamplePoint.aspx?type=GetItems&selPointID=" + strPointId + "&strSubtaskID=" + strSubtaskID + "&strSampleID=" + strSampleID);
        },
        onAfterEdit: SampleEdit,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    $(".l-layout-header-toggle").css("display", "none");

    objPointGrid.toggleCol("POINT_ID");
    objPointGrid.toggleCol("QC_TYPE");
    if (strMonitor_ID == "000000002") {
        objPointGrid.changeHeaderText("SAMPLE_CODE", "编号");
    }

    function SampleEdit(e) {
        var id = e.record.ID;
        var PointID = e.record.POINT_ID;
        var strCellName = e.column.columnname;
        var strCellValue = e.value;
        if (e.record["__status"] != "nochanged") {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "/saveSample",
                data: "{'id':'" + id + "','strPointID':'" + PointID + "','strCellName':'" + strCellName + "','strCellValue':'" + strCellValue + "'}",
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

    //增加数据
    function createData() {
        parent.$.ligerDialog.open({ title: '点位信息增加', top: 0, width: 780, height: 520, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../../Mis/Monitor/sampling/QY/SamplePointEdit.aspx?strSubtaskID=' + strSubtaskID + '&strMonitorID=' + strMonitor_ID
        });
    }
    //修改数据
    function updateData() {
        if (strPointId == "") {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        parent.$.ligerDialog.open({ title: '点位信息编辑', top: 0, width: 780, height: 520, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../../Mis/Monitor/sampling/QY/SamplePointEdit.aspx?strid=' + strPointId + '&strSubtaskID=' + strSubtaskID
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

                        //$("iframe")[0].contentWindow.upLoadFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { GetFileSataus(item, dialog); dialog.close(); }
            }], url: '../../../OA/ATT/AttFileUpload.aspx?filetype=PointMap&id=' + getCompanyID(strSubtaskID)
        });
    }
    ///附件下载
    function downLoadFile() {
        $.ligerDialog.open({ title: '点位图下载', width: 500, height: 270, top: 50, isHidden: false,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?filetype=SubTask&id=' + strSubtaskID
        });
    }

    function GetFileSataus(item, dialog) {
        var fn = dialog.frame.getUpLoadStatus();
        if (fn == "1") {
            DelFile();
        }
        if (fn == "0" || fn == "2") {
            DelFile();
        }
        if (fn == "3" || fn == "") {
            return;
        }
    }
    function DelFile() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "SamplePoint.aspx?type=DelFile&strSubtaskID=" + strSubtaskID + "&strFileType=SubTask",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data == "True") {
                    objPointGrid.loadData();
                }
                else {
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }


    ///监测方案下载
    function downLoadSolution() {
        $.ligerDialog.open({ title: '监测方案下载', width: 500, height: 270, top: 50,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?filetype=Contract&id=' + strSubtaskID
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

    function getSubSample(SampleId) {
        var objItems = null;
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "../../Mis/Monitor/sampling/QY/SubSample.aspx?action=GetSubSampleList&strSampleId=" + SampleId,
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
    //save函数
    function f_SaveDate(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        $.ajax({
            cache: false,
            type: "POST",
            url: "SamplePoint.aspx/SaveData",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    parent.$.ligerDialog.success('数据保存成功');
                    dialog.close();
                    objPointGrid.loadData();
                }
                else {
                    parent.$.ligerDialog.warn('数据保存失败');
                }
            }
        });
    }


    //现场平行质控添加
    function addQcTwinInfo() {
        if (objPointGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择设置质控的样品');
            return;
        }
        if (objPointGrid.getSelectedRow().QC_TYPE != "0") {
            $.ligerDialog.warn('只有原始样可以设置质控');
            return;
        }
        var strSampleID = objPointGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: "现场平行设置", width: 450, height: 400, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.QcSave() == "1") {
                dialog.close();
                $.ligerDialog.success('质控保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('质控保存失败');
            }
            objPointGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../Mis/Monitor/sampling/QY/QcTwinSetting.aspx?strSampleId=" + strSampleID + "&strQcType=3"
        });
    }

    //现场空白质控添加
    function addQcEmptyInfo() {
        if (objPointGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择设置质控的样品');
            return;
        }
        if (objPointGrid.getSelectedRow().QC_TYPE != "0") {
            $.ligerDialog.warn('只有原始样可以设置质控');
            return;
        }
        var strSampleID = objPointGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: "现场空白设置", width: 450, height: 400, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.QcSave() == "1") {
                dialog.close();
                $.ligerDialog.success('质控保存成功');
            }
            else {
                dialog.close();
                $.ligerDialog.warn('质控保存失败');
            }
            objPointGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../Mis/Monitor/sampling/QY/QcEmptySetting.aspx?strSampleId=" + strSampleID + "&strQcType=1"
        });
    }

    //现场加标信息添加
    function addQcAddInfo() {
        if (objPointGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择设置质控的样品');
            return;
        }
        if (objPointGrid.getSelectedRow().QC_TYPE != "0") {
            $.ligerDialog.warn('只有原始样可以设置质控');
            return;
        }
        var strSampleID = objPointGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: "现场加标设置", width: 450, height: 500, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.QcSave() == "1") {
                dialog.close();
                $.ligerDialog.success('质控保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('质控保存失败');
            }
            objPointGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../Mis/Monitor/sampling/QY/QcAddSetting.aspx?strSampleId=" + strSampleID + "&strQcType=2"
        });
    }

    //标准盲样添加
    function addQcBlindInfo() {
        if (objPointGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择监测类别信息');
            return;
        }
        var strSubTaskId = objPointGrid.getSelectedRow().ID;
        $.ligerDialog.open({ title: "现场密码设置", width: 450, height: 500, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.QcSave() == "1") {
                dialog.close();
                $.ligerDialog.success('质控保存成功')
            }
            else {
                dialog.close();
                $.ligerDialog.warn('质控保存失败');
            }
            objPointGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../Mis/Monitor/sampling/QY/QcBlindSetting.aspx?strSubTaskId=" + strSubTaskId + "&strQcType=11"
        });
    }


    function Upload() {
        var selectedItemSample = objPointGrid.getSelectedRow();
        if (!selectedItemSample) {
            $.ligerDialog.warn('请先选择样品！');
            return;
        }


        var filetype = strRowdataID; //样品ID
        var Doc_ID = strSubtaskID; //任务ID

        //alert(filetype + "|" + Doc_ID);
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
                }], url: '../../OA/ATT/AttFileUpload.aspx?id=' + Doc_ID + '&filetype=' + filetype
        });

    }


    function DownLoad() {
        var selectedItemSample = objPointGrid.getSelectedRow();
        if (!selectedItemSample) {
            $.ligerDialog.warn('请先选择样品！');
            return;
        }
        var filetype = strRowdataID; //样品ID
        var Doc_ID = strSubtaskID; //任务ID
        //      alert(filetype + "|" + Doc_ID);
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?id=' + Doc_ID + '&filetype=' + filetype
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
                    url: "SamplePoint.aspx/deletePoint",
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


    function discontinued() {
        if (objPointGrid.getSelectedRow() == null) {
            parent.$.ligerDialog.warn('请选择一条记录');
            return;
        }
        parent.$.ligerDialog.confirm("确定所选点位停产吗？", function (yes) {
            if (yes == true) {
                var strValue = objPointGrid.getSelectedRow().ID;
                showDetailSrh(strValue);
            }
        });
    }

    function DelPlanStopPoint(strSubTaskId) {

        $.ajax({
            cache: false,
            type: "POST",
            url: "SamplePoint.aspx/discontinued",
            data: "{'strValue':'" + strSubTaskId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    objPointGrid.loadData();
                    parent.$.ligerDialog.success('停产成功')
                }
                else if (data.d == "2") {
                    var tabid = "tabidSamplingListNew"
                    var surl = "../SamplingList.aspx";
                    top.f_addTab(tabid, '采样任务列表', surl);
                }
                else {
                    parent.$.ligerDialog.warn('停产失败');
                }
            }
        });
    }

    //设置grid 的弹出停产原因对话框
    var detailWinSrh = null;
    function showDetailSrh(strSubTaskId) {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构

            var mainform = $("#InputForm");
            mainform.ligerForm({
                inputWidth: 430, labelWidth: 90, space: 40,
                fields: [
                     { display: "停产原因", name: "SEA_REASON", newline: true, type: "textarea" }
                    ]
            });
            $("#SEA_REASON").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:400px");
            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 660, height: 170, top: 90, title: "停产原因录入",
                buttons: [
                  { text: '确定', onclick: function () { SaveStopReason(strSubTaskId); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }
    }
    function SaveStopReason(strSubTaskId) {
        var strReason = $("#SEA_REASON").val();
        $.ajax({
            cache: false,
            type: "POST",
            url: "SamplePoint.aspx/SaveStopReason",
            data: "{'strValue':'" + strSubTaskId + "','strReason':'" + strReason + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "true") {
                    clearSearchDialogValue();
                    detailWinSrh.hide();
                    DelPlanStopPoint(strSubTaskId);
                }
                else {
                    return;
                }
            }
        });
    }
    function clearSearchDialogValue() {
        $("#SEA_REASON").val("");
    }



    //获取字典项信息
    function getDictName(strDictCode, strDictType) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "SamplePoint.aspx/getDictName",
            data: "{'strDictCode':'" + strDictCode + "','strDictType':'" + strDictType + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }
    //获取监测类别信息
    function getMonitorName(strMonitorID) {
        var strValue = "";
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "SamplePoint.aspx/getMonitorName",
            data: "{'strValue':'" + strMonitorID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                strValue = data.d;
            }
        });
        return strValue;
    }

});

function getCompanyID(strID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SamplePoint.aspx?type=getCompanyID&strSubTaskId=" + strID,
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

//获取原样样品编号
function getQcSourceCode(sample_id) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SamplePoint.aspx/getQcSourceCode",
        data: "{'strValue':'" + sample_id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取企业信息
function getCompanyName(strCompanyID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SamplePoint.aspx/getCompanyName",
        data: "{'strValue':'" + strCompanyID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取排口信息
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
//获取质控类型
function GetQcType(qc_type) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "SamplePoint.aspx/GetQcType",
        data: "{'strValue':'" + qc_type + "'}",
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

function SaveRemark(strSubTaskId) {
    var strRemark = $("#SEA_REMARK").val();
    $.ajax({
        cache: false,
        type: "POST",
        url: "SamplePoint.aspx/SaveRemark",
        data: "{'strValue':'" + strSubTaskId + "','strRemark':'" + strRemark + "'}",
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

function AddSubSample() {
    if (objPointGrid.getSelectedRow() == null) {
        parent.$.ligerDialog.warn('请选择一条记录进行操作!');
        return;
    }

    else {
        var SelectedRow = objPointGrid.getSelectedRow();
        $.ligerDialog.open({ title: '新增子样', name: 'winaddtor', width: 700, height: 400, top: 0, url: '../../Mis/Monitor/sampling/QY/SubSample.aspx?strSampleId=' + SelectedRow.ID + '&strSampleCode=' + SelectedRow.SAMPLE_CODE, buttons: [
                { text: '关闭', onclick: function (item, dialog) { objPointGrid.loadData(); dialog.close() } }
            ]
        });
    }
}


function ShowSubSample(strId) {
    $.ligerDialog.open({ title: '子样明细', name: 'winaddtor', width: 700, height: 400, top: 0, url: '../../Mis/Monitor/sampling/QY/SubSample.aspx?strView=true&strSampleId=' + strId, buttons: [
                { text: '返回', onclick: function (item, dialog) { objPointGrid.loadData(); dialog.close() } }
            ]
    });
}

//修改现场加标和密码盲样
function editQcInfo() {
    if (objPointGrid.getSelectedRow() == null) {
        parent.$.ligerDialog.warn('请选择一条记录进行操作!');
        return;
    }
    var SelectedRow = objPointGrid.getSelectedRow();
    var strSampleID = SelectedRow.ID;
    var strQcType = SelectedRow.QC_TYPE;
    var EditUrl = "";
    var EditTitle = "";
    if (strQcType == "2") {
        EditUrl = "QcEditAddSetting.aspx";
        EditTitle = "现场加标设置";
    }
    else if (strQcType == "11") {
        EditUrl = "QcEditBlindSetting.aspx";
        EditTitle = "现场密码设置";
    }
    else {
        parent.$.ligerDialog.warn('只能对现场加标和现场密码进行设置!');
        return;
    }
    $.ligerDialog.open({ title: EditTitle, width: 450, height: 450, isHidden: false, buttons:
        [
        { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: EditUrl + "?strSampleId=" + strSampleID + "&strQcType=" + strQcType
    });
}

//弹出修改样品编号日期部分
var dateDialog = null;
function showDate() {
    var strSampleId = "";
    var strDateCode = "";
    var SelectedRow = objPointGrid.getSelectedRow();
    if (SelectedRow == null) {
        parent.$.ligerDialog.warn('请选择一条记录进行操作!');
        return;
    }
    else {
        strSampleId = SelectedRow.ID;
        if (SelectedRow.SAMPLE_CODE.split('-').length == 3) {
            $("#DATE_CODE").val(SelectedRow.SAMPLE_CODE.split('-')[1].substr(2, 6));
        }
    }
    if (dateDialog) {
        dateDialog.show();
    } else {

        $.ligerDialog.open({
            target: $("#dateDiv"),
            width: 320, height: 120, top: 90, title: "修改样品编号的日期部分",
            buttons: [
                  { text: '全部', onclick: function (item, dialog) {
                      strDateCode = $("#DATE_CODE").val();
                      UpdateSampleCode(strSubtaskID, strSampleId, strDateCode, true);
                      dialog.hide();
                  }
                  },
                  { text: '单个', onclick: function (item, dialog) {
                      strDateCode = $("#DATE_CODE").val();
                      UpdateSampleCode(strSubtaskID, strSampleId, strDateCode, false);
                      dialog.hide();
                  }
                  },
                  { text: '取消', onclick: function (item, dialog) { dialog.hide(); } }
                  ]
        });
    }
}

function UpdateSampleCode(strSubTaskId, strSampleId, strDateCode, isAll) {

    $.ajax({
        cache: false,
        type: "POST",
        url: "SamplePoint.aspx/UpdateSampleCode",
        data: "{'strSubTaskId':'" + strSubTaskId + "','strSampleId':'" + strSampleId + "','strDateCode':'" + strDateCode + "','isAll':'" + isAll + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "true") {
                objPointGrid.loadData();
            }
            else {
                parent.$.ligerDialog.warn('更新失败');
            }
        }
    });
}

function DictJson(DictType) {
    var objReturnValue = null;
    $.ajax({
        url: strUrl + "?type=GetDict&dictType=" + DictType,
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

//拆分样品项目
function showItemSplit() {
    if (objPointGrid.getSelectedRow() == null) {
        parent.$.ligerDialog.warn('请选择一条记录进行操作!');
        return;
    }
    var SelectedRow = objPointGrid.getSelectedRow();
    var strSampleID = SelectedRow.ID;
    var strSampleCode = SelectedRow.SAMPLE_CODE;
    var strSampleName = SelectedRow.SAMPLE_NAME;
    var strQcType = SelectedRow.QC_TYPE;

    if (strQcType != "0") {
        parent.$.ligerDialog.warn('只能对原始样进行项目拆分!');
        return;
    }

    parent.$.ligerDialog.open({ title: '样品项目拆分', top: 0, width: 630, height: 520, isHidden: false, buttons:
        [{ text: '确定', onclick: f_SaveItemSplit },
         { text: '取消', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../../Mis/Monitor/sampling/QY/PointItemSplit.aspx?SampleID=' + strSampleID + '&SampleCode=' + strSampleCode + '&SampleName=' + strSampleName + '&MonitorID=' + strMonitor_ID + '&SubTaskID=' + strSubtaskID
    });
}

function f_SaveItemSplit(item, dialog) {
    var fn = dialog.frame.SaveItemSplit || dialog.frame.window.SaveItemSplit;
    var strdata = fn();

    if (strdata == "true") {
        objPointGrid.loadData();
        ItemGrid.loadData();
        dialog.close();
    }
}
//检查废水的采样时间和样品状态
function CheckPointData() {
    var iTure = true;
    if (strMonitor_ID == "000000001") {
        for (var i = 0; i < objPointGrid.rows.length; i++) {
            if (objPointGrid.rows[i]['QC_TYPE'] == "0") {
                if (objPointGrid.rows[0]['000000151@txtYanse@颜色@textbox@'] == "" || objPointGrid.rows[0]['000000152@txtQiwei@气味@dropdownlist@qiwei'] == "" || objPointGrid.rows[0]['000000153@txtFuyou@浮油@dropdownlist@fuyou'] == "" || objPointGrid.rows[0]['SAMPLE_ACCEPT_DATEORACC'] == "") {
                    return false;
                }
            }
        }
    }
    return true;
}