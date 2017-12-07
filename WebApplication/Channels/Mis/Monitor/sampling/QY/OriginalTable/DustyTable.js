var strSubTask_Id = "", strItem_Id = "", strIsView = "", strLinkCode = "", strPurPose = "", strSampleName= "";
var blEdit = false, strBaseInfor_Id = "", strAtt_Id = "", strCompany_Name = "", strAtt_Id = "", CellDdate = "", strSAMPLE_CODE;
var objGrid = null, vCompanyArr = null, vFireArr = null, vBaseInforArr = null;
var strUrl="DustyTable.aspx";
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
    strSubTask_Id = $.getUrlVar('strSubTask_Id');
    strItem_Id = $.getUrlVar('strItem_Id');
    strIsView = $.getUrlVar('strIsView');
    strEdit = $.getUrlVar('strEdit');
    strLinkCode = $.getUrlVar('strLinkCode');
    strPurPose = $.getUrlVar('strPurPose');
    strSampleName = $.getUrlVar('strSampleName');
    var height = 150;
    //如果是“监测分析”环节表头不用显示出来
    if (strLinkCode == "02") {
        height = 2;
    }
    $("#layout1").ligerLayout({ topHeight: height, leftWidth: "99%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });
    var objTooblar = null;
    var gridEdit = true;
    if (strIsView == "false") {
        objTooblar = { items: [
                { text: '增加一行', click: createData, icon: 'add' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' },
                { line: true },
                { text: '平均', click: avgData, icon: 'database_wrench' }
                 ]
        };
        gridEdit = true;
    }

    if (strLinkCode != "01" && strLinkCode != "05") {
        setInputDisabled();
    }

    if (strEdit) {
        if (strEdit == "true") {
            gridEdit = true;
        }
    }

    var objColumns = null;
    switch (strLinkCode) {
        case "01":    //采样
            objColumns = [
                         { display: '序号', name: 'ID', hide: true, width: 90, align: 'left' },
                        { display: '采样序号<a style="color:red;padding-left:5px;">*</a>', name: 'SAMPLE_CODE', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '样品编号', name: 'S_CODE', width: 140, align: 'left', render:
                            function (r, i) {
                                return strSAMPLE_CODE + "-" + (i + 1);
                            }
                        },
                        { display: '滤筒编号<a style="color:red;padding-left:5px;">*</a>', name: 'FITER_CODE', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '采样时间<a style="color:red;padding-left:5px;">*</a>', name: 'SAMPLE_DATE', width: 120, align: 'left', editor: { type: 'text'} },
                        { display: '烟气动压<br>(Pa)<a style="color:red;padding-left:5px;">*</a>', name: 'SMOKE_MOVE_PRESSURE', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '烟气静压<br>(Pa)<a style="color:red;padding-left:5px;">*</a>', name: 'SMOKE_STATIC_PRESSURE', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '烟气全压<br>(Pa)<a style="color:red;padding-left:5px;">*</a>', name: 'SMOKE_ALL_PRESSURE', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '烟气计压<br>(KPa)<a style="color:red;padding-left:5px;">*</a>', name: 'SMOKE_K_PRESSURE', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '烟气温度<br>(℃)<a style="color:red;padding-left:5px;">*</a>', name: 'SMOKE_TEMPERATURE', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '烟气含氧量<br>(%)<a style="color:red;padding-left:5px;">*</a>', name: 'SMOKE_OXYGEN', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '烟气流速<br>(m/s)<a style="color:red;padding-left:5px;">*</a>', name: 'SMOKE_SPEED', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '标态流量<br>(Nm³/h)<a style="color:red;padding-left:5px;">*</a>', name: 'NM_SPEED', width: 100, align: 'left', editor: { type: 'text'} },
                        { display: '标况体积<br>(L)<a style="color:red;padding-left:5px;">*</a>', name: 'L_STAND', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '滤筒初重<br>(g)', name: 'FITER_BEGIN_WEIGHT', width: 90, align: 'left' },
                        { display: '滤筒终重<br>(g)', name: 'FITER_AFTER_WEIGHT', width: 90, align: 'left' },
                        { display: '样品重量<br>(g)', name: 'SAMPLE_WEIGHT', width: 90, align: 'left' },
                        { display: '烟尘浓度<br>(mg/Nm³)', name: 'SMOKE_POTENCY', width: 90, align: 'left' },
                        { display: '烟尘折算浓度<br>(mg/Nm³)', name: 'SMOKE_POTENCY2', width: 90, align: 'left' },
                        { display: '烟尘排放量<br>(kg/h)', name: 'SMOKE_DISCHARGE', width: 90, align: 'left' },
                        { display: '备注', name: 'REMARK1', width: 90, align: 'left', editor: { type: 'text'} }
                        ];
            break;
        case "02":  //监测分析
            objColumns = [
                         { display: '序号', name: 'ID', hide: true, width: 90, align: 'left' },
                        { display: '采样序号', name: 'SAMPLE_CODE', width: 90, align: 'left' },
                        { display: '样品编号', name: 'S_CODE', width: 140, align: 'left', render: function (r, i) { return strSAMPLE_CODE + "-" + (i + 1); } },
                        { display: '滤筒编号', name: 'FITER_CODE', width: 90, align: 'left' },
                        { display: '采样时间', name: 'SAMPLE_DATE', width: 120, align: 'left' },
                        { display: '烟气动压<br>(Pa)', name: 'SMOKE_MOVE_PRESSURE', width: 90, align: 'left' },
                        { display: '烟气静压<br>(Pa)', name: 'SMOKE_STATIC_PRESSURE', width: 90, align: 'left' },
                        { display: '烟气全压<br>(Pa)', name: 'SMOKE_ALL_PRESSURE', width: 90, align: 'left' },
                        { display: '烟气计压<br>(KPa)', name: 'SMOKE_K_PRESSURE', width: 90, align: 'left' },
                        { display: '烟气温度<br>(℃)', name: 'SMOKE_TEMPERATURE', width: 90, align: 'left' },
                        { display: '烟气含氧量<br>(%)', name: 'SMOKE_OXYGEN', width: 90, align: 'left' },
                        { display: '烟气流速<br>(m/s)', name: 'SMOKE_SPEED', width: 90, align: 'left' },
                        { display: '标态流量<br>(Nm³/h)', name: 'NM_SPEED', width: 100, align: 'left' },
                        { display: '标况体积<br>(L)', name: 'L_STAND', width: 90, align: 'left' },
                        { display: '滤筒初重<br>(g)<a style="color:red;padding-left:5px;">*</a>', name: 'FITER_BEGIN_WEIGHT', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '滤筒终重<br>(g)<a style="color:red;padding-left:5px;">*</a>', name: 'FITER_AFTER_WEIGHT', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '样品重量<br>(g)<a style="color:red;padding-left:5px;">*</a>', name: 'SAMPLE_WEIGHT', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '烟尘浓度<br>(mg/Nm³)', name: 'SMOKE_POTENCY', width: 90, align: 'left' },
                        { display: '烟尘折算浓度<br>(mg/Nm³)', name: 'SMOKE_POTENCY2', width: 90, align: 'left' },
                        { display: '烟尘排放量<br>(kg/h)', name: 'SMOKE_DISCHARGE', width: 90, align: 'left' },
                        { display: '备注', name: 'REMARK1', width: 90, align: 'left' }
                        ];
            objTooblar = { items: [
                { text: '平均', click: avgData, icon: 'database_wrench' }
                 ]
            };
            break;
        case "05":  //核录
            objColumns = [
                         { display: '序号', name: 'ID', hide: true, width: 90, align: 'left' },
                        { display: '采样序号', name: 'SAMPLE_CODE', width: 90, align: 'left' },
                        { display: '样品编号', name: 'S_CODE', width: 140, align: 'left', render: function (r, i) { return strSAMPLE_CODE + "-" + (i + 1); } },
                        { display: '滤筒编号', name: 'FITER_CODE', width: 90, align: 'left' },
                        { display: '采样时间', name: 'SAMPLE_DATE', width: 120, align: 'left' },
                        { display: '烟气动压<br>(Pa)', name: 'SMOKE_MOVE_PRESSURE', width: 90, align: 'left' },
                        { display: '烟气静压<br>(Pa)', name: 'SMOKE_STATIC_PRESSURE', width: 90, align: 'left' },
                        { display: '烟气全压<br>(Pa)', name: 'SMOKE_ALL_PRESSURE', width: 90, align: 'left' },
                        { display: '烟气计压<br>(KPa)', name: 'SMOKE_K_PRESSURE', width: 90, align: 'left' },
                        { display: '烟气温度<br>(℃)', name: 'SMOKE_TEMPERATURE', width: 90, align: 'left' },
                        { display: '烟气含氧量<br>(%)', name: 'SMOKE_OXYGEN', width: 90, align: 'left' },
                        { display: '烟气流速<br>(m/s)', name: 'SMOKE_SPEED', width: 90, align: 'left' },
                        { display: '标态流量<br>(Nm³/h)', name: 'NM_SPEED', width: 100, align: 'left' },
                        { display: '标况体积<br>(L)', name: 'L_STAND', width: 90, align: 'left' },
                        { display: '滤筒初重<br>(g)', name: 'FITER_BEGIN_WEIGHT', width: 90, align: 'left' },
                        { display: '滤筒终重<br>(g)', name: 'FITER_AFTER_WEIGHT', width: 90, align: 'left' },
                        { display: '样品重量<br>(g)', name: 'SAMPLE_WEIGHT', width: 90, align: 'left' },
                        { display: '烟尘浓度<br>(mg/Nm³)<a style="color:red;padding-left:5px;">*</a>', name: 'SMOKE_POTENCY', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '烟尘折算浓度<br>(mg/Nm³)<a style="color:red;padding-left:5px;">*</a>', name: 'SMOKE_POTENCY2', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '烟尘排放量<br>(kg/h)<a style="color:red;padding-left:5px;">*</a>', name: 'SMOKE_DISCHARGE', width: 90, align: 'left', editor: { type: 'text'} },
                        { display: '备注', name: 'REMARK1', width: 90, align: 'left' }
                        ];
            objTooblar = { items: [
                { text: '平均', click: avgData, icon: 'database_wrench' }
                 ]
            };
            break;
        default:
            objColumns = [
                         { display: '序号', name: 'ID', hide: true, width: 90, align: 'left' },
                        { display: '采样序号', name: 'SAMPLE_CODE', width: 90, align: 'left' },
                        { display: '样品编号', name: 'S_CODE', width: 140, align: 'left', render: function (r, i) { return strSAMPLE_CODE + "-" + (i + 1); } },
                        { display: '滤筒编号', name: 'FITER_CODE', width: 90, align: 'left' },
                        { display: '采样时间', name: 'SAMPLE_DATE', width: 120, align: 'left' },
                        { display: '烟气动压<br>(Pa)', name: 'SMOKE_MOVE_PRESSURE', width: 90, align: 'left' },
                        { display: '烟气静压<br>(Pa)', name: 'SMOKE_STATIC_PRESSURE', width: 90, align: 'left' },
                        { display: '烟气全压<br>(Pa)', name: 'SMOKE_ALL_PRESSURE', width: 90, align: 'left' },
                        { display: '烟气计压<br>(KPa)', name: 'SMOKE_K_PRESSURE', width: 90, align: 'left' },
                        { display: '烟气温度<br>(℃)', name: 'SMOKE_TEMPERATURE', width: 90, align: 'left' },
                        { display: '烟气含氧量<br>(%)', name: 'SMOKE_OXYGEN', width: 90, align: 'left' },
                        { display: '烟气流速<br>(m/s)', name: 'SMOKE_SPEED', width: 90, align: 'left' },
                        { display: '标态流量<br>(Nm³/h)', name: 'NM_SPEED', width: 100, align: 'left' },
                        { display: '标况体积<br>(L)', name: 'L_STAND', width: 90, align: 'left' },
                        { display: '滤筒初重<br>(g)', name: 'FITER_BEGIN_WEIGHT', width: 90, align: 'left' },
                        { display: '滤筒终重<br>(g)', name: 'FITER_AFTER_WEIGHT', width: 90, align: 'left' },
                        { display: '样品重量<br>(g)', name: 'SAMPLE_WEIGHT', width: 90, align: 'left' },
                        { display: '烟尘浓度<br>(mg/Nm³)', name: 'SMOKE_POTENCY', width: 90, align: 'left' },
                        { display: '烟尘折算浓度<br>(mg/Nm³)', name: 'SMOKE_POTENCY2', width: 90, align: 'left' },
                        { display: '烟尘排放量<br>(kg/h)', name: 'SMOKE_DISCHARGE', width: 90, align: 'left' },
                        { display: '备注', name: 'REMARK1', width: 90, align: 'left' }
                        ];
            break;
    }

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../../../Contract/MethodHander.ashx?action=GetDict&type=Fire_Type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                vFireArr = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: strUrl + "?action=getCompanyInfor&strSubTask_Id=" + strSubTask_Id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.Total != "0") {
                vCompanyArr = data.Rows;
                $("#txtCompany").val(vCompanyArr[0].COMPANY_NAME);
                strSAMPLE_CODE = vCompanyArr[0].SAMPLE_CODE;
            }
        }
    });
    objGrid = $("#CreateDiv").ligerGrid({
        columns: objColumns,
        headerRowHeight: 42,
        width: '100%',
        height: 400,
        pageSizeOptions: [10, 15, 20, 50],
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        //        url: strUrl + '?action=getAttInfor&strBaseInfor_Id=' + strBaseInfor_Id,
        alternatingRow: false,
        toolbar: objTooblar,
        checkbox: true,
        enabledEdit: gridEdit,
        rownumbers: true,
        onAfterEdit: f_onAfterEdit,
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
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    function getHourAndMin() {
        var strValue = "";
        var nowDate = new Date();
        CellDdate = nowDate.getHours() + ":" + nowDate.getMinutes()
        strValue = CellDdate;
        return strValue;
    }

    function f_onAfterEdit(e) {
        //保存数据
        var data = ""
        var columnname = "";
        data = JSON.stringify(e.record);
        columnname = e.column.columnname;
        var fill_id = e.record["ID"];
        var value = encodeURIComponent(e.value);
        if (columnname == "SAMPLE_DATE") {
            if (value != "" && value != undefined) {
                //value = TogetDate(value);
            }
        }
        //if (e.record["__status"] != "nochanged") {
        if (data != "") {
            $.ajax({
                cache: false,
                async: false,
                url: strUrl + "?action=UpdateAttValue&strUpdateCell=" + columnname + "&strUpdateCellValue=" + value + "&strAttInfor_Id=" + fill_id,
                type: "post",
                contentType: "application/json; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    if (data == "True") {
                        //objGrid.loadData();
                        switch (columnname) {
                            case "FITER_BEGIN_WEIGHT":
                            case "FITER_AFTER_WEIGHT":
                                //计算样品重量：终重-初重
                                var dWEIGHT_BEGIN = e.record.FITER_BEGIN_WEIGHT;
                                var dWEIGHT_END = e.record.FITER_AFTER_WEIGHT;
                                var dWEIGHT = Math.round(Math.abs(dWEIGHT_END - dWEIGHT_BEGIN) * 10000) / 10000;
                                objGrid.updateCell('SAMPLE_WEIGHT', dWEIGHT, e.record);
                                //计算烟尘浓度：（样品重量/标况体积）*1000000
                                var dL_STAND = e.record.L_STAND;
                                var dPOTENCY = 0;
                                if (dL_STAND != "0") {
                                    dPOTENCY = Math.round((dWEIGHT / dL_STAND) * 1000000);
                                    objGrid.updateCell('SMOKE_POTENCY', dPOTENCY, e.record);
                                }
                                //计算烟尘折算浓度：（（21/（21-烟气含氧量））*烟尘浓度）/折算系数 
                                var dOXYGEN = e.record.SMOKE_OXYGEN;
                                var dMODUL_NUM = $("#txtMODUL_NUM").val();
                                var dPOTENCY2 = 0;
                                if (dMODUL_NUM != "0") {
                                    dPOTENCY2 = Math.round(((21 / (21 - dOXYGEN)) * dPOTENCY) / dMODUL_NUM);
                                    objGrid.updateCell('SMOKE_POTENCY2', dPOTENCY2, e.record);
                                }
                                //计算烟尘排放量：（烟尘浓度*标态流量）/1000000
                                var dSPEED = e.record.NM_SPEED;
                                objGrid.updateCell('SMOKE_DISCHARGE', Math.round((dPOTENCY * dSPEED / 1000000) * 1000) / 1000, e.record);
                                break;
                            case "SAMPLE_WEIGHT":
                                //计算烟尘浓度：（样品重量/标况体积）*1000000
                                var dWEIGHT = e.record.SAMPLE_WEIGHT
                                var dL_STAND = e.record.L_STAND;
                                var dPOTENCY = 0;
                                if (dL_STAND != "0") {
                                    dPOTENCY = Math.round((dWEIGHT / dL_STAND) * 1000000);
                                    objGrid.updateCell('SMOKE_POTENCY', dPOTENCY, e.record);
                                }
                                //计算烟尘折算浓度：（（21/（21-烟气含氧量））*烟尘浓度）/折算系数 
                                var dOXYGEN = e.record.SMOKE_OXYGEN;
                                var dMODUL_NUM = $("#txtMODUL_NUM").val();
                                var dPOTENCY2 = 0;
                                if (dMODUL_NUM != "0") {
                                    dPOTENCY2 = Math.round(((21 / (21 - dOXYGEN)) * dPOTENCY) / dMODUL_NUM);
                                    objGrid.updateCell('SMOKE_POTENCY2', dPOTENCY2, e.record);
                                }
                                //计算烟尘排放量：（烟尘浓度*标态流量）/1000000
                                var dSPEED = e.record.NM_SPEED;
                                objGrid.updateCell('SMOKE_DISCHARGE', Math.round((dPOTENCY * dSPEED / 1000000) * 1000) / 1000, e.record);
                                break;
                            case "SMOKE_POTENCY":
                                //计算烟尘折算浓度：（（21/（21-烟气含氧量））*烟尘浓度）/折算系数 
                                var dPOTENCY = e.record.SMOKE_POTENCY;
                                var dOXYGEN = e.record.SMOKE_OXYGEN;
                                var dMODUL_NUM = $("#txtMODUL_NUM").val();
                                var dPOTENCY2 = 0;
                                if (dMODUL_NUM != "0") {
                                    dPOTENCY2 = Math.round(((21 / (21 - dOXYGEN)) * dPOTENCY) / dMODUL_NUM);
                                    objGrid.updateCell('SMOKE_POTENCY2', dPOTENCY2, e.record);
                                }
                                //计算烟尘排放量：（烟尘浓度*标态流量）/1000000
                                var dSPEED = e.record.NM_SPEED;
                                objGrid.updateCell('SMOKE_DISCHARGE', Math.round((dPOTENCY * dSPEED / 1000000) * 1000) / 1000, e.record);
                                break;
                            default:

                        }
                    } else {
                    }
                }
            });
        }
        // }
    }

    //$("#txtFUEL_TYPE").ligerComboBox({ data: vFireArr, valueField: 'DICT_CODE', textField: 'DICT_TEXT' });

    $("#txtSampleDate").ligerDateEditor({ showTime: true, initValue: TogetDate(new Date()) + " " + new Date().getHours() + ":" + new Date().getMinutes() });
    //    $("#txtSampleDate").ligerDateEditor({width:200, initValue: new Date() });

    $("#txtMECHIE_MODEL").ligerComboBox({
        width: 200, onBeforeOpen: Apparatus_select, valueFieldID: 'hidMECHIE_MODEL'
    });

    //弹出仪器grid
    function Apparatus_select() {
        $.ligerDialog.open({ title: '选择分析仪器', name: 'winselectorYQ', top: 60, width: 700, height: 400, isHidden: false, url: '../../../../../Base/Item/SelectApparatus.aspx', buttons: [
                { text: '确定', onclick: Apparatus_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
        });
        return false;
    }

    //仪器弹出grid ok按钮
    function Apparatus_selectOK(item, dialog) {
        var fn = dialog.frame.f_select || dialog.frame.window.f_select;
        var data = fn();
        if (!data) {
            $.ligerDialog.warn('请选择分析仪器!');
            return;
        }
        $("#txtMECHIE_MODEL").val(data.NAME + (data.MODEL));
        $("#txtMECHIE_CODE").val(data.APPARATUS_CODE);
        $("#hidMECHIE_MODEL").val(data.ID);
        dialog.close();
    }

    $("#txtMethold").ligerComboBox({
        width: 200, onBeforeOpen: Method_select, valueFieldID: 'hidMethodID'
    });

    if (strSubTask_Id != "" && strItem_Id != "") {
        getDustyInfor(strSubTask_Id, strItem_Id);
    }

    //弹出分析方法grid
    function Method_select() {
        $.ligerDialog.open({ title: '选择方法依据', name: 'winselector', width: 700, height: 370, isHidden: false, url: '../../../../../Base/Item/SelectMethod.aspx?MethodSel_ItemID=' + strItem_Id + '&METHOD_ID=' + encodeURI($("#hidMethodID").val()) + '&METHOD_NAME=' + encodeURI($("#txtMethold").val()), buttons: [
                { text: '确定', onclick: Method_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
        });
        return false;
    }

    //分析方法弹出grid ok按钮
    function Method_selectOK(item, dialog) {
        var fn = dialog.frame.f_selects || dialog.frame.window.f_selects;
        var data = fn().split('|');
        if (!data) {
            $.ligerDialog.warn('请选择方法依据!');
            return;
        }
        var strMethod_Codes = "";
        var strMethod_IDs = "";
        strMethod_Codes = data[1];
        strMethod_IDs = data[0];
        /*
        for (var i = 0; i < data.length; i++) {
        if (strMethod_Codes == "")
        { strMethod_Codes = data[i].METHOD_CODE; }
        else
        { strMethod_Codes += ";" + data[i].METHOD_CODE; }
        if (strMethod_IDs == "")
        { strMethod_IDs = data[i].ID; }
        else
        { strMethod_IDs += ";" + data[i].ID; }
        }
        */

        $("#txtMethold").val(strMethod_Codes);
        $("#hidMethodID").val(strMethod_IDs);
        dialog.close();
    }

    //cancel按钮
    function selectCancel(item, dialog) {
        dialog.close();
    }


    $("#btnOk").bind("click", function () {
        SaveBaseInfor();
    })

    function SaveBaseInfor() {
        //getBaseInfor(strSubTask_Id, strItem_Id);
        var data = getBaseInfor();
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "?action=SaveBaseInfor" + data,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data, textStatus) {
                if (data != "") {
                    strBaseInfor_Id = data;
                    //                    $("#btnOk").attr("disabled", "disabled");
                    objGrid.set('url', strUrl + '?action=getAttInfor&strBaseInfor_Id=' + strBaseInfor_Id);
                    $.ligerDialog.success('数据保存成功！');
                }
            }
        });
    }

    function getDustyInfor(strId, itemId) {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "?action=getBaseInfor&strSubTask_Id=" + strId + "&strItem_Id=" + itemId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.Total != "0") {
                    vBaseInforArr = data.Rows;
                    strBaseInfor_Id = vBaseInforArr[0].ID;
                    setInputDate();
                }
                else {
                    vBaseInforArr = data.Rows;
                    setPreDate();
                    $("#txtPurPress").val(strPurPose);
                    $("#txtPOSITION").val(strSampleName);
                }
            }
        });
    }

    function getAttInfor(strId) {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: strUrl + "?action=getAttInfor&strBaseInfor_Id=" + strId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.Total != "0") {
                    strAtt_Id = data.Rows[0].ID;
                    objGrid.set('url', strUrl + '?action=getAttInfor&strBaseInfor_Id=' + strId);
                }
            }
        });
    }

    function setInputDate() {
        if (vBaseInforArr != null) {

            $("#txtMethold").val(vBaseInforArr[0].METHOLD_NAME);
            $("#hidMethodID").val(vBaseInforArr[0].METHOLD_ID);
            $("#txtPurPress").val(vBaseInforArr[0].PURPOSE);
            $("#txtSampleDate").val(vBaseInforArr[0].SAMPLE_DATE);
            $("#txtBOILER_NAME").val(vBaseInforArr[0].BOILER_NAME);
            $("#txtFUEL_TYPE").val(vBaseInforArr[0].FUEL_TYPE);
            $("#txtHEIGHT").val(vBaseInforArr[0].HEIGHT);
            $("#txtPOSITION").val(vBaseInforArr[0].POSITION);
            $("#txtSECTION_DIAMETER").val(vBaseInforArr[0].SECTION_DIAMETER);
            $("#txtSECTION_AREA").val(vBaseInforArr[0].SECTION_AREA);
            $("#txtGOVERM_METHOLD").val(vBaseInforArr[0].GOVERM_METHOLD);
            $("#txtMECHIE_WIND_MEASURE").val(vBaseInforArr[0].MECHIE_WIND_MEASURE);
            $("#txtHUMIDITY_MEASURE").val(vBaseInforArr[0].HUMIDITY_MEASURE);
            $("#txtMODUL_NUM").val(vBaseInforArr[0].MODUL_NUM);
            $("#txtMECHIE_MODEL").val(vBaseInforArr[0].MECHIE_MODEL);
            $("#txtMECHIE_CODE").val(vBaseInforArr[0].MECHIE_CODE);
            $("#txtSAMPLE_POSITION_DIAMETER").val(vBaseInforArr[0].SAMPLE_POSITION_DIAMETER);
            $("#txtENV_TEMPERATURE").val(vBaseInforArr[0].ENV_TEMPERATURE);
            $("#txtAIR_PRESSURE").val(vBaseInforArr[0].AIR_PRESSURE);

            $("#btnOk").val("修 改");

            getAttInfor(strBaseInfor_Id);
        }
    }
    //虚拟数据
    function setPreDate() {
        if (vBaseInforArr.length > 0) {
            //$("#txtMethold").val(vBaseInforArr[0].METHOLD_NAME);
            //$("#hidMethodID").val(vBaseInforArr[0].METHOLD_ID);
            $("#txtPurPress").val(vBaseInforArr[0].PURPOSE);
            //$("#txtSampleDate").val(vBaseInforArr[0].SAMPLE_DATE);
            $("#txtBOILER_NAME").val(vBaseInforArr[0].BOILER_NAME);
            $("#txtFUEL_TYPE").val(vBaseInforArr[0].FUEL_TYPE);
            $("#txtHEIGHT").val(vBaseInforArr[0].HEIGHT);
            $("#txtPOSITION").val(vBaseInforArr[0].POSITION);
            $("#txtSECTION_DIAMETER").val(vBaseInforArr[0].SECTION_DIAMETER);
            $("#txtSECTION_AREA").val(vBaseInforArr[0].SECTION_AREA);
            $("#txtGOVERM_METHOLD").val(vBaseInforArr[0].GOVERM_METHOLD);
            $("#txtMECHIE_WIND_MEASURE").val(vBaseInforArr[0].MECHIE_WIND_MEASURE);
            $("#txtHUMIDITY_MEASURE").val(vBaseInforArr[0].HUMIDITY_MEASURE);
            $("#txtMODUL_NUM").val(vBaseInforArr[0].MODUL_NUM);
            //$("#txtMECHIE_MODEL").val(vBaseInforArr[0].MECHIE_MODEL);
            //$("#txtMECHIE_CODE").val(vBaseInforArr[0].MECHIE_CODE);
            $("#txtSAMPLE_POSITION_DIAMETER").val(vBaseInforArr[0].SAMPLE_POSITION_DIAMETER);
            $("#txtENV_TEMPERATURE").val(vBaseInforArr[0].ENV_TEMPERATURE);
            $("#txtAIR_PRESSURE").val(vBaseInforArr[0].AIR_PRESSURE);
        }
    }

    function setInputDisabled() {
        $("#btnOk").attr("disabled", "disabled");

        $("#txtCompany").attr("disabled", "disabled");
        $("#txtMethold").attr("disabled", "disabled");
        $("#txtPurPress").attr("disabled", "disabled");
        $("#txtSampleDate").attr("disabled", "disabled");
        $("#txtBOILER_NAME").attr("disabled", "disabled");
        $("#txtFUEL_TYPE").attr("disabled", "disabled");
        $("#txtHEIGHT").attr("disabled", "disabled");
        $("#txtPOSITION").attr("disabled", "disabled");
        $("#txtSECTION_DIAMETER").attr("disabled", "disabled");
        $("#txtSECTION_AREA").attr("disabled", "disabled");
        $("#txtGOVERM_METHOLD").attr("disabled", "disabled");
        $("#txtMECHIE_WIND_MEASURE").attr("disabled", "disabled");
        $("#txtHUMIDITY_MEASURE").attr("disabled", "disabled");
        $("#txtMODUL_NUM").attr("disabled", "disabled");
        $("#txtMECHIE_MODEL").attr("disabled", "disabled");
        $("#txtMECHIE_CODE").attr("disabled", "disabled");
        $("#txtSAMPLE_POSITION_DIAMETER").attr("disabled", "disabled");
        $("#txtENV_TEMPERATURE").attr("disabled", "disabled");
        $("#txtAIR_PRESSURE").attr("disabled", "disabled");

    }


    function getBaseInfor() {
        var strValue = "";
        if (strBaseInfor_Id != "") {
            strValue += "&strBaseInfor_Id=" + strBaseInfor_Id;
        }
        strValue += "&strSubTask_Id=" + strSubTask_Id;
        strValue += "&strItem_Id=" + strItem_Id;
        strValue += "&strMethold=" + encodeURIComponent($("#txtMethold").val());
        strValue += "&strMetholdID=" + encodeURIComponent($("#hidMethodID").val());
        strValue += "&strPurPress=" + encodeURIComponent($("#txtPurPress").val());
        strValue += "&strSampleDate=" + $("#txtSampleDate").val();
        strValue += "&strBoiler_Name=" + encodeURIComponent($("#txtBOILER_NAME").val());
        strValue += "&strFuelType=" + encodeURIComponent($("#txtFUEL_TYPE").val());
        strValue += "&strHeight=" + encodeURIComponent($("#txtHEIGHT").val());
        strValue += "&strPosition=" + encodeURIComponent($("#txtPOSITION").val());
        strValue += "&strSectionDiameter=" + encodeURIComponent($("#txtSECTION_DIAMETER").val());
        strValue += "&strSectionArea=" + encodeURIComponent($("#txtSECTION_AREA").val());
        strValue += "&strGovMethold=" + encodeURIComponent($("#txtGOVERM_METHOLD").val());
        strValue += "&strMechieWindMea=" + encodeURIComponent($("#txtMECHIE_WIND_MEASURE").val());
        strValue += "&strHumidityMea=" + encodeURIComponent($("#txtHUMIDITY_MEASURE").val());
        strValue += "&strModelNum=" + encodeURIComponent($("#txtMODUL_NUM").val());
        strValue += "&strMechie_Mode=" + encodeURIComponent($("#txtMECHIE_MODEL").val());
        strValue += "&strMechie_Code=" + encodeURIComponent($("#txtMECHIE_CODE").val());
        strValue += "&strSample_Diameter=" + encodeURIComponent($("#txtSAMPLE_POSITION_DIAMETER").val());
        strValue += "&strEnv_Temperature=" + encodeURIComponent($("#txtENV_TEMPERATURE").val());
        strValue += "&strAir_Pressure=" + encodeURIComponent($("#txtAIR_PRESSURE").val());

        return strValue;
    }

    function createData() {
        if (strBaseInfor_Id != "") {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "?action=SaveAttInfor&strBaseInfor_Id=" + strBaseInfor_Id,
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data, textStatus) {
                    if (data != "") {
                        strAtt_Id = data;
                        objGrid.set('url', strUrl + '?action=getAttInfor&strBaseInfor_Id=' + strBaseInfor_Id);
                        //$.ligerDialog.success('数据保存成功！');
                    }
                }
            });
        } else {
            $.ligerDialog.warn('尚未生成原始记录基础数据！');
            return;
        }
    }

    function deleteData() {
        var rowselected = objGrid.getSelectedRow();
        if (rowselected == null) {
            $.ligerDialog.warn('请选选择一行进行操作！');
            return;
        } else {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "?action=delAttInfor&strAttInfor_Id=" + rowselected.ID,
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data, textStatus) {
                    if (data == "True") {
                        objGrid.set('url', strUrl + '?action=getAttInfor&strBaseInfor_Id=' + strBaseInfor_Id);
                        $.ligerDialog.success('数据操作成功！');
                        return
                    } else {
                        $.ligerDialog.warn('数据操作失败！');
                        return;
                    }
                }
            });
        }
    }


})
function TogetDate(date) {
    var strD = "";
    var thisYear = date.getYear();
    thisYear = (thisYear < 1900) ? (1900 + thisYear) : thisYear;
    var thisMonth = date.getMonth() + 1;
    //如果月份长度是一位则前面补0    
    if (thisMonth < 10) thisMonth = "0" + thisMonth;
    var thisDay = date.getDate();
    var thisTime=date.get
    //如果天的长度是一位则前面补0    
    if (thisDay < 10) thisDay = "0" + thisDay;
    {

        strD = thisYear + "-" + thisMonth + "-" + thisDay;
    }
    return strD;
}
//计算平均值
function avgData() {
    objGrid.endEdit();
    if (strBaseInfor_Id != "") {
        if (objGrid.rows.length > 0) {
            $.ajax({
                cache: false,
                async: false,
                type: "POST",
                url: strUrl + "?action=AvgAttInfor&strBaseInfor_Id=" + strBaseInfor_Id,
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data, textStatus) {
                    
                    if (data == "1") {
                        objGrid.set('url', strUrl + '?action=getAttInfor&strBaseInfor_Id=' + strBaseInfor_Id);
                    }
                }
            });
        }
        else {
            $.ligerDialog.warn('先增加原始记录数据！');
        }

    } else {
        $.ligerDialog.warn('尚未生成原始记录基础数据！');
        return;
    }
}