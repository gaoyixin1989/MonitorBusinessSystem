
//@ Create By Castle(胡方扬) 2012-12-01
//@ Company: Comleader(珠海高凌)
//@ 功能：委托书监测点位设置
//@ *修改人（时间）:
//@ *修改原因：
var mdivsamp = null, sampdiv = null;
var Itemgrid = null;
var gridPointSelectId = "";
var strContractPointId="";
var GetFRQData = null, GetMonitorItemData = null;
var boolresult = false;
var Copy = [], strCopyContractPointId = "",vSumList=null;
var objJsonTool1 = null, objJsonTool2 = null;
var strFreqTask = "", strFreqMonitor = "";
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
    $("#layout1").ligerLayout({ topHeight: 0, leftWidth: "50%", rightWidth: "50%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });

    //获取URL 参数
    var strContratId = $.getUrlVar('strContratId'); //$.query.get('standartId');
    var strurlMonitorTypeId = $.getUrlVar('strMonitorType');
    var strCompanyIdFrim = $.getUrlVar('strCompanyIdFrim');
    var strProject = $.getUrlVar('strProject');
    var strContractTypeId = $.getUrlVar('strContractTypeId');
    var strIsView = $.trim($.getUrlVar('isView'));
    var strContractCode = $.getUrlVar('strContractCode');
    if (strIsView == "true") {
        objJsonTool1 = null;
        objJsonTool2 = null;
    }

    //清远启用是否使用 频次生成任务
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MethodHander.ashx?action=GetDict&type=FreqTask",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                if (data.length > 0) {
                    strFreqTask = data[0].DICT_CODE;
                }
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    //可生成频次任务的类别
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MethodHander.ashx?action=GetDict&type=FreqMonitor",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                if (data.length > 0) {
                    strFreqMonitor = data[0].DICT_CODE;
                }
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    //加载监测频次下拉列表信息信息
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "MethodHander.ashx?action=GetDict&type=Freq",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                GetFRQData = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax请求数据失败！');
        }
    });

    //加载监测项目下拉列表信息信息
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "MethodHander.ashx?action=GetMonitorItems&strMonitorType=" + strurlMonitorTypeId + "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                GetMonitorItemData = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax请求数据失败！');
        }
    });

    //如果为非查看状态，则添加操作按钮
    if (strIsView == "false") {
        objJsonTool1 = { items: [
                { id: 'add', text: '增加', click: createData, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: f_deleteData, icon: 'delete' }
                ]
        };

        objJsonTool2 = { items: [
                { id: 'setting', text: '监测项目设置', click: itemclickOfToolbar, icon: 'database_wrench' },
                { line: true },
                { id: 'copy', text: '复制', click: itemclickOfToolbar, icon: 'page_copy' },
                { line: true },
                { id: 'paste', text: '粘贴', click: itemclickOfToolbar, icon: 'page_paste' }
                ]
        };
    }
    //初始化加载监测点位数据列表
    LoadContractPoint();
    //初始化加载监测类别列表
    SelectPointItemList();
    //填充平行布局
    gridDraggable(g1, g2);


    function LoadContractPoint() {

        window['g1'] = mdivsamp = $("#divsamp").ligerGrid({
            columns: [
                { display: '监测点位名称', name: 'POINT_NAME', align: 'left', width: 160, minWidth: 60, editor: { type: 'text'} },
                { display: '监测频次', name: 'FREQ', align: 'left', width: 100, minWidth: 60, editor: { type: 'select', data: GetFRQData, valueColumnName: 'DICT_CODE', displayColumnName: 'DICT_TEXT' }, render: function (item) {
                    for (var i = 0; i < GetFRQData.length; i++) {
                        if (GetFRQData[i]['DICT_CODE'] == item.FREQ)
                            return '每年' + GetFRQData[i]['DICT_TEXT'] + '次';
                    }
                    return item.FREQ;
                }
                }
                ],
            //            title: '监测点位列表',
            width: '100%', height: '90%',
            pageSizeOptions: [5, 10],
            url: "MethodHander.ashx?action=GetContractPoint&strContratId=" + strContratId + "&strMonitroType=" + strurlMonitorTypeId + "",
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            toolbar: objJsonTool1,
            onAfterEdit: f_onAfterEdit,
            whenRClickToSelect: true,
            onCheckRow: function (data, rowindex, rowobj) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
                gridPointSelectId = rowindex.ID;
                SelectPointItemList();

            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }

        });

        function f_onAfterEdit(e) {
            mdivsamp.updateCell('POINT_NAME', e.record.POINT_NAME, e.record);
            mdivsamp.updateCell('FREQ', e.record.FREQ, e.record);

        }
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    }


    //增加数据
    function createData() {
        strContractPointId = "";
        parent.$.ligerDialog.open({ title: '点位信息增加', top: 360, width: 700, height: 460, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'ContractPointEdit.aspx?ContractId=' + strContratId + '&MonitorTypeId=' + strurlMonitorTypeId + '&CompanyID=' + strCompanyIdFrim + '&strProject=' + strProject + '&ContractTypeId=' + strContractTypeId
        });
    }
    //修改数据
    function updateData() {
        var row = mdivsamp.getSelectedRow();

        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        else {
            strContractPointId = row.ID;
            parent.$.ligerDialog.open({ title: '点位信息编辑', top: 360, width: 700, height: 460, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'ContractPointEdit.aspx?strid=' + strContractPointId + '&ContractId=' + strContratId + '&MonitorTypeId=' + strurlMonitorTypeId + '&CompanyID=' + strCompanyIdFrim + '&strProject=' + strProject + '&ContractTypeId=' + strContractTypeId
            });
        }
    }

    function f_deleteData() {
        var row = mdivsamp.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        else {
            strContractPointId = row.ID
            parent.$.ligerDialog.confirm('您确定要删除该委托书的点位信息吗？', function (yes) {
                if (yes) {
                    deletePointData();
                }
                else
                { return; }
            })
        }
    }

    //保存数据
    function f_SaveDate(item, dialog) {
        var fn = dialog.frame.GetIsExiststr || dialog.frame.window.GetIsExiststr;
        var strRequestdata = fn();
        if (strRequestdata != "") {
            f_booExistDate(strRequestdata);
        }
        if (boolresult) {
            var fnSave = dialog.frame.GetBaseInfoStr || dialog.frame.window.GetBaseInfoStr;
            var strReques = fnSave();
            SaveContractPoint(strReques);
            dialog.close();
        }
    }


    //判断是否存在该点位信息
    function f_booExistDate(strRequestParme) {
        boolresult = false;
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MethodHander.ashx?action=IsExistContractPoint" + strRequestParme,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    parent.$.ligerDialog.warn('已存在符合当前条件的项目！');
                    boolresult = false;
                }
                else {
                    boolresult = true;
                }
            },
            error: function () {
                parent.$.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }

    //保存不存在的数据

    function SaveContractPoint(strReques) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MethodHander.ashx?action=SaveContractPoint" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    parent.$.ligerDialog.success('数据保存成功！');
                    mdivsamp.loadData();
                    return;
                }
                else {
                    parent.$.ligerDialog.success('数据保存失败！');
                }
            },
            error: function () {
                parent.$.ligerDialog.warn('Ajax加载数据失败！');
            }
        });

    }


    function deletePointData() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MethodHander.ashx?action=DelContractPoint&strPointId=" + strContractPointId + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $.ligerDialog.success('数据删除成功！');
                    mdivsamp.loadData();
                    return;
                }
                else {
                    $.ligerDialog.success('数据删除失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }

    function SetUserList(item) {
        if (gridPointSelectId == "") {
            $.ligerDialog.warn('请选择监测点位！');
            return;
        }
        else {
            setUserItem();
        }

    }

    function clearDialogValue() {
        $("#SEA_CONTRACTPOINT_ID").val("");
        $("#SEA_POINT_NAME").val("");
        $("#SEA_FREQ_Box").ligerGetComboBoxManager().setValue("");
    }

    function SelectPointItemList() {


        window['g2'] = Itemgrid = $("#divuserlist").ligerGrid({
            columns: [
                { display: '监测项目名称', name: 'ITEM_ID', align: 'left', width: 160, minWidth: 60, data: GetMonitorItemData, render: function (item) {
                    for (var i = 0; i < GetMonitorItemData.length; i++) {
                        if (GetMonitorItemData[i]['ID'] == item.ITEM_ID)
                            return GetMonitorItemData[i]['ITEM_NAME']
                    }
                    return item.ITEM_ID;
                }
                }
                ],
            //            title: '点位监测项目列表',
            width: '100%',
            height: '90%',
            pageSizeOptions: [5, 10],
            url: 'MethodHander.ashx?action=GetContractPointItem&strContractPointId=' + gridPointSelectId,
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            whenRClickToSelect: true,
            toolbar: objJsonTool2,

            onBeforeCheckAllRow: function (checked, grid, element) { return false; }

        });
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    }


    function itemclickOfToolbar(item) {
        switch (item.id) {

            case 'setting':
                var row = mdivsamp.getSelectedRow();
                if (row == null) {
                    $.ligerDialog.warn('请选择监测点位！');
                    return;
                } else {
                    strContractPointId = row.ID;
                    parent.$.ligerDialog.open({ title: '监测项目设置', top: 360, width: 440, height: 380, buttons:
        [{ text: '确定', onclick: f_SaveDivItemData },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'ContractPointItems.aspx?strid=' + strContractPointId + '&ContractId=' + strContratId + '&MonitorTypeId=' + strurlMonitorTypeId + '&CompanyID=' + strCompanyIdFrim + '&strProject=' + strProject + '&ContractTypeId=' + strContractTypeId
                    });
                }
                break;
            case 'paste':
                if (Copy.length <= 0) {
                    $.ligerDialog.warn('请选择要进行粘贴的监测项目！'); return;
                }
                var rowpaste = mdivsamp.getSelectedRow();
                if (rowpaste == null) {
                    $.ligerDialog.warn('请选择要粘贴的目标监测点位！'); return;
                }
                else {
                    strContractPointId = rowpaste.ID;
                    if (strContractPointId == strCopyContractPointId) {
                        $.ligerDialog.warn('当前监测点位已存在相同监测项目！'); return;
                    }
                    if (Copy.length > 0) {
                        SaveCopyItemData(Copy);
                    }
                    else {
                        $.ligerDialog.warn('没有要设置的监测项目！'); return;
                    }
                }
                break;
            case 'copy':
                var rownew = mdivsamp.getSelectedRow();
                if (rownew == null) {
                    $.ligerDialog.warn('请选择监测点位！'); return;
                }
                else {
                    strCopyContractPointId = rownew.ID;
                    var selected = null;
                    Copy = [];
                    var checked = Itemgrid.getCheckedRows();
                    if (checked.length > 0) {
                        selected = checked;
                    }
                    else {
                        selected = Itemgrid.data.Rows;
                    }
                    if (selected.length <= 0) { $.ligerDialog.warn('请先选择要复制的记录！'); return; }
                    else {
                        var rownu = selected.length;
                        if (rownu != null)
                            var succnu = 0;
                        for (var i = 0; i < rownu; i++) {
                            Copy.push(selected[i]);
                            succnu++;
                        }
                        if (succnu == rownu) {
                            $.ligerDialog.success('所选数据复制成功！');
                        }
                    }
                }
                break;
            default:
                break;

        }
    }

    function SaveCopyItemData(strCopy) {
        var strSelectItems = "";
        for (var i = 0; i < strCopy.length; i++) {
            strSelectItems += strCopy[i].ITEM_ID + ";";
        }
        strSelectItems = strSelectItems.substring(0, strSelectItems.length - 1);
        $.ajax({
            cache: false,
            type: "POST",
            //            url: "EvaluationTapSetting.aspx/InsertCopyData",
            url: "MethodHander.ashx?action=SaveDivItemData&strPointId=" + strContractPointId + "&strPointAddItemsId=" + strSelectItems + "&ContractId=" + strContratId + "&MonitorTypeId=" + strurlMonitorTypeId + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                $('body').append("<div class='jloading'>正在保存数据中...</div>");
                $.ligerui.win.mask();
            },
            complete: function () {
                $('body > div.jloading').remove();
                $.ligerui.win.unmask({ id: new Date().getTime() });
            },
            success: function (data) {
                if (data != "") {
                    Itemgrid.loadData();
                    $.ligerDialog.success('粘贴数据操作成功！');
                }
                else {
                    $.ligerDialog.warn('粘贴数据操作失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax请求数据失败！');
            }
        });
    }
    //通过获取弹出窗口页面中 获取监测项目右侧ListBox集合以及移除的监测项目
    function f_SaveDivItemData(item, dialog) {
        var fn = dialog.frame.GetMoveItems || dialog.frame.window.GetMoveItems;
        var strMovedata = fn();

        var fn1 = dialog.frame.GetSelectItems || dialog.frame.window.GetSelectItems;
        var strSelectData = fn1();

        if (strSelectData == "" & strMovedata == "") {
            return;
        }
        else {
            SaveDivItemData(strSelectData, strMovedata);
            GetConstractFeeCount();
            dialog.close();
        }
    }

    function GetConstractFeeCount() {
        // 获取监测费用总计
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MethodHander.ashx?action=GetConstractFeeCount&strContratId=" + strContratId + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vSumList = data.Rows;
                    parent.$("#constDetail").html(vSumList[0].BUDGET);
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    //保存监测项目
    function SaveDivItemData(strSelectData, strMovedata) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MethodHander.ashx?action=SaveDivItemData&strPointId=" + strContractPointId + "&strPointAddItemsId=" + strSelectData + "&strPointItemsMoveId=" + strMovedata + "&strContratId=" + strContratId + "&strMonitroType=" + strurlMonitorTypeId + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    Itemgrid.loadData();
                    parent.$.ligerDialog.success('数据保存成功！');
                }
                else {
                    parent.$.ligerDialog.warn('数据操作失败！');
                }
            },
            error: function () {
                parent.$.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }

});
