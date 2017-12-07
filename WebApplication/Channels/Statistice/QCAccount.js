//@ Create By 潘德军 2013-4-22
//@ Company: Comleader(珠海高凌)
//@ 功能：质量控制结果统计表

$(document).ready(function () {
    var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
    var objgrid_QCAll, objGrid_QCOutEmpty;

    //创建表单结构 --点位基本信息
    $("#divQCSrh").ligerForm({
        inputWidth: 160, labelWidth: 80, space: 0, labelAlign: 'right',
        fields: [
                { display: "开始时间", name: "QC_BEGIN_DATE", newline: false, type: "date", format: "yyyy-MM-dd", showTime: "false", group: "查询", groupicon: groupicon },
                { display: "结束时间", name: "QC_END_DATE", newline: false, type: "date", format: "yyyy-MM-dd", showTime: "false" },
                { display: "监测类型", name: "MONITOR_ID", newline: false, type: "select", comboboxName: "MONITOR_ID1", options: { valueFieldID: "MONITOR_ID", url: "../BASE/MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} }
                ]
    });

    var btnQCSrh, btnQCExcel;
    btnQCSrh = $("#btnQCSrh").ligerButton(
    {
        text: "搜索",
        width: "40",
        click: function () {
            showQCAll();
        }
    }
    );
    btnQCExcel = $("#btnQCExcel").ligerButton(
    {
        text: "导出",
        width: "40",
        click: function () {
            QCExcel();
        }
    }
    );

    function showQCAll() {
        if (!ifHasDate()) {
            return;
        }

        objgrid_QCAll = null;
        objGrid_QCOutEmpty = null;

        var QC_BEGIN_DATE = encodeURI($("#QC_BEGIN_DATE").val());
        var QC_END_DATE = encodeURI($("#QC_END_DATE").val());
        var MONITOR_ID = $("#MONITOR_ID").val();

        var strJson = "";
        var strJsonAll, strJsonOutEmpty;
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: 'QCAccount.aspx?type=getQCAccount&QC_BEGIN_DATE= ' + QC_BEGIN_DATE + '&QC_END_DATE=' + QC_END_DATE + '&MONITOR_ID=' + MONITOR_ID,
            contentType: "application/json; charset=utf-8",
            dataType: "text",
            success: function (data, textStatus) {
                strJson = data;
            }
        });

        var strArr = strJson.split('|');

        showGridQCAll(strArr[0]);
        showGridQCOutEmpty(strArr[1]);
        showGridQCInEmpty(strArr[2]);
        showGridQCOutTwin(strArr[3]);
        showGridQCInTwin(strArr[4]);
        showGridQCPwdTwin(strArr[5]);
        showGridQCAdd(strArr[6]);
        showGridQCSt(strArr[7]);
    }

    function showGridQCAll(strJsonAll) {
        var jsonAll = eval("(" + strJsonAll + ")");
        objgrid_QCAll = $("#grid_QCAll").ligerGrid({
            title: '',
            dataAction: 'server',
            usePager: false,
            width: '100%',
            height: '100%',
            data: jsonAll,
            columns: [
                    { display: '序号', name: 'NUM', width: 40 },
                    { display: '分析项目', name: 'TEST_ITEM', width: 200 },
                    { display: '样品总数', name: 'ALL_COUNT', width: 60 },

                    { display: '现场空白个数', name: 'OUT_EMPTY_COUNT', width: 80 },
                    { display: '合格率%', name: 'OUT_EMPTY_OK_PER', width: 60 },

                    { display: '实验空白个数', name: 'IN_EMPTY_COUNT', width: 80 },
                    { display: '相对偏差范围%', name: 'IN_EMPTY_RANGE', width: 90 },
                    { display: '合格率%', name: 'IN_EMPTY_OK_PER', width: 60 },

                    { display: '平行样个数', name: 'TWIN_COUNT', width: 60 },
                    { display: '样品比例%', name: 'TWIN_PER', width: 60 },
                    { display: '相对偏差范围%', name: 'TWIN_RANGE', width: 90 },
                    { display: '合格数', name: 'TWIN_OK_COUNT', width: 60 },
                    { display: '合格率%', name: 'TWIN_OK_PER', width: 60 },

                    { display: '加标回收个数', name: 'ADD_COUNT', width: 80 },
                    { display: '样品比例%', name: 'ADD_PER', width: 60 },
                    { display: '回收率范围%', name: 'ADD_RANGE', width: 80 },
                    { display: '合格数', name: 'ADD_OK_COUNT', width: 60 },
                    { display: '合格率%', name: 'ADD_OK_PER', width: 60 },

                    { display: '自配标样个数', name: 'SELF_ST_COUNT', width: 80 },
                    { display: '样品比例%', name: 'SELF_ST_PER', width: 60 },
                    { display: '相对误差范围%', name: 'SELF_ST_RANGE', width: 90 },
                    { display: '合格数', name: 'SELF_ST_OK_COUNT', width: 60 },
                    { display: '合格率%', name: 'SELF_ST_OK_PER', width: 60 },

                    { display: '自配标样个数', name: 'ST_COUNT', width: 80 },
                    { display: '合格率%', name: 'ST_OK_PER', width: 60 }
                ]
        });
    }

    function showGridQCOutEmpty(strJsonOutEmpty) {
        var jsonOutEmpty = eval("(" + strJsonOutEmpty + ")");
        objGrid_QCOutEmpty = $("#grid_QCOutEmpty").ligerGrid({
            title: '',
            dataAction: 'server',
            usePager: false,
            width: '100%',
            height: '100%',
            data: jsonOutEmpty,
            columns: [
                    { display: '分析项目', name: 'TEST_ITEM', width: 200 },
                    { display: '分析批次', name: 'TEST_BATCH', width: 80 },
                    { display: '分析日期', name: 'TEST_DATE', width: 120 },

                    { display: '现场空白测定值1', name: 'RESULT1', width: 120 },
                    { display: '现场空白测定值2', name: 'RESULT2', width: 120 },
                    { display: '现场空白测定值范围', name: 'OFFSET', width: 120 },

                    { display: '方法检出限', name: 'CHECKOUT', width: 100 },

                    { display: '总空白样数', name: 'AlL_QC_COUNT', width: 100 },
                    { display: '合格空白样数', name: 'AlL_QC_OK_COUNT', width: 100 },
                    { display: '合格率%', name: 'AlL_QC_OK_PER', width: 80 }
                ]
        });
    }

    function showGridQCInEmpty(strJsonInEmpty) {
        var jsonInEmpty = eval("(" + strJsonInEmpty + ")");
        objGrid_QCInEmpty = $("#grid_QCInEmpty").ligerGrid({
            title: '',
            dataAction: 'server',
            usePager: false,
            width: '100%',
            height: '100%',
            data: jsonInEmpty,
            columns: [
                    { display: '分析项目', name: 'TEST_ITEM', width: 200 },
                    { display: '分析批次', name: 'TEST_BATCH', width: 80 },
                    { display: '分析日期', name: 'TEST_DATE', width: 120 },

                    { display: '空白测定值1(A)', name: 'RESULT1', width: 120 },
                    { display: '空白测定值2(A)', name: 'RESULT2', width: 120 },
                    { display: '相对偏差%', name: 'OFFSET', width: 120 },
                    { display: '相对偏差范围%', name: 'RANGE', width: 120 },

                    { display: '方法要求值', name: 'CHECKOUT', width: 100 },

                    { display: '总空白样数', name: 'AlL_QC_COUNT', width: 100 },
                    { display: '合格空白样数', name: 'AlL_QC_OK_COUNT', width: 100 },
                    { display: '合格率%', name: 'AlL_QC_OK_PER', width: 80 }
                ]
        });
    }

    function showGridQCOutTwin(strJsonOutTwin) {
        var jsonOutTwin = eval("(" + strJsonOutTwin + ")");
        objGrid_QCOutTwin = $("#grid_QCOutTwin").ligerGrid({
            title: '',
            dataAction: 'server',
            usePager: false,
            width: '100%',
            height: '100%',
            data: jsonOutTwin,
            columns: [
                    { display: '分析项目', name: 'TEST_ITEM', width: 200 },
                    { display: '分析批次', name: 'TEST_BATCH', width: 80 },
                    { display: '分析日期', name: 'TEST_DATE', width: 120 },

                    { display: '平行双样测定值1', name: 'RESULT1', width: 120 },
                    { display: '平行双样测定值2', name: 'RESULT2', width: 120 },
                    { display: '相对偏差%', name: 'OFFSET', width: 120 },
                    { display: '相对偏差范围%', name: 'RANGE', width: 120 },

                    { display: '总平行样数对', name: 'AlL_QC_COUNT', width: 100 },
                    { display: '合格平行样对', name: 'AlL_QC_OK_COUNT', width: 100 },
                    { display: '合格率%', name: 'AlL_QC_OK_PER', width: 80 }
                ]
        });
    }

    function showGridQCInTwin(strJsonInTwin) {
        var jsonInTwin = eval("(" + strJsonInTwin + ")");
        objGrid_QCInTwin = $("#grid_QCInTwin").ligerGrid({
            title: '',
            dataAction: 'server',
            usePager: false,
            width: '100%',
            height: '100%',
            data: jsonInTwin,
            columns: [
                    { display: '分析项目', name: 'TEST_ITEM', width: 200 },
                    { display: '分析批次', name: 'TEST_BATCH', width: 80 },
                    { display: '分析日期', name: 'TEST_DATE', width: 120 },

                    { display: '平行双样测定值1', name: 'RESULT1', width: 120 },
                    { display: '平行双样测定值2', name: 'RESULT2', width: 120 },
                    { display: '相对偏差%', name: 'OFFSET', width: 120 },
                    { display: '相对偏差范围%', name: 'RANGE', width: 120 },

                    { display: '总平行样数对', name: 'AlL_QC_COUNT', width: 100 },
                    { display: '合格平行样对', name: 'AlL_QC_OK_COUNT', width: 100 },
                    { display: '合格率%', name: 'AlL_QC_OK_PER', width: 80 }
                ]
        });
    }

    function showGridQCPwdTwin(strJsonPwdTwin) {
        var jsonPwdTwin = eval("(" + strJsonPwdTwin + ")");
        objGrid_QCPwdTwin = $("#grid_QCPwdTwin").ligerGrid({
            title: '',
            dataAction: 'server',
            usePager: false,
            width: '100%',
            height: '100%',
            data: jsonPwdTwin,
            columns: [
                    { display: '分析项目', name: 'TEST_ITEM', width: 200 },
                    { display: '分析批次', name: 'TEST_BATCH', width: 80 },
                    { display: '分析日期', name: 'TEST_DATE', width: 120 },

                    { display: '平行双样测定值1', name: 'RESULT1', width: 120 },
                    { display: '平行双样测定值2', name: 'RESULT2', width: 120 },
                    { display: '相对偏差%', name: 'OFFSET', width: 120 },
                    { display: '相对偏差范围%', name: 'RANGE', width: 120 },

                    { display: '总平行样数对', name: 'AlL_QC_COUNT', width: 100 },
                    { display: '合格平行样对', name: 'AlL_QC_OK_COUNT', width: 100 },
                    { display: '合格率%', name: 'AlL_QC_OK_PER', width: 80 }
                ]
        });
    }

    function showGridQCAdd(strJsonAdd) {
        var jsonAdd = eval("(" + strJsonAdd + ")");
        objGrid_QCAdd = $("#grid_QCAdd").ligerGrid({
            title: '',
            dataAction: 'server',
            usePager: false,
            width: '100%',
            height: '100%',
            data: jsonAdd,
            columns: [
                    { display: '分析项目', name: 'TEST_ITEM', width: 200 },
                    { display: '分析批次', name: 'TEST_BATCH', width: 80 },
                    { display: '分析日期', name: 'TEST_DATE', width: 120 },

                    { display: '室内加标回收率%', name: 'RESULT1', width: 120 },
                    { display: '现场加标回收率%', name: 'RESULT2', width: 120 },
                    { display: '回收率范围%', name: 'RANGE', width: 120 },

                    { display: '总加标样数', name: 'AlL_QC_COUNT', width: 100 },
                    { display: '合格加标样数', name: 'AlL_QC_OK_COUNT', width: 100 },
                    { display: '合格率%', name: 'AlL_QC_OK_PER', width: 80 }
                ]
        });
    }

    function showGridQCSt(strJsonSt) {
        var jsonSt = eval("(" + strJsonSt + ")");
        objGrid_QCSt = $("#grid_QCSt").ligerGrid({
            title: '',
            dataAction: 'server',
            usePager: false,
            width: '100%',
            height: '100%',
            data: jsonSt,
            columns: [
                    { display: '分析项目', name: 'TEST_ITEM', width: 200 },
                    { display: '分析批次', name: 'TEST_BATCH', width: 80 },
                    { display: '分析日期', name: 'TEST_DATE', width: 120 },

                    { display: '标样浓度', name: 'ND', width: 120 },
                    { display: '标样测定值1', name: 'RESULT1', width: 120 },
                    { display: '标样测定值2', name: 'RESULT2', width: 120 },

                    { display: '标样总数', name: 'AlL_QC_COUNT', width: 100 },
                    { display: '标样合格数', name: 'AlL_QC_OK_COUNT', width: 100 },
                    { display: '合格率%', name: 'AlL_QC_OK_PER', width: 80 }
                ]
        });
    }

    function QCExcel() {
        if (!ifHasDate()) {
            return;
        }

        $("#hdQC_BEGIN_DATE").val($("#QC_BEGIN_DATE").val());
        $("#hdQC_END_DATE").val($("#QC_END_DATE").val());
        $("#hdMONITOR_ID").val($("#MONITOR_ID").val());
        $("#btnImport").click();
    }

    function ifHasDate() {
        if ($("#QC_BEGIN_DATE").val() == "") {
            $.ligerDialog.warn('请选择开始时间！');
            return false;
        }
        if ($("#QC_END_DATE").val() == "") {
            $.ligerDialog.warn('请选择结束时间！');
            return false;
        }
        return true;
    }
});

