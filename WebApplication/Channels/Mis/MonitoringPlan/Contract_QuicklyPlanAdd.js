
/// 日历控件--监测计划新增
/// 创建时间：2012-12-22
/// 创建人：胡方扬
var objGrid = null, objItemGrid = null, vMonitorType = null, vPointItem = null, vMonth = null, vContractInfor = null, vDutyInitUser = null, GetMonitorItemData = null;
var task_id = "", struser = "", strmonitorID = "", monitorTypeName = "";
var tr = "", MonitorArr = [], Copy = [];
var saveflag = "";
var vDutyList = null;
var strPlanId = "", strIfPlan = "", strDate = "";
var strYear = "", strProjectName = "", strContratTypeId = "", strContactType = "";
var strCompanyId = "", strCompanyName = "", strIndustryId = "", strAreaId = "", strContactName = "", strTelPhone = "", strAddress = "";
var strCompanyIdFrim = "", strCompanyNameFrim = "", strIndustryIdFrim = "", strAreaIdFrim = "", strContactNameFrim = "", strTelPhoneFrim = "", strAddressFrim = "";

var strFreqId = "", strPointId = "", strCopyPointId = "", strInitValue = "", strMonitorItem_ID = "", strAskingDate = "";
var isEdit = false;
var checkedCustomer = [], checkedMonitorArr = [], checkedPoint = [], moveCheckPoint = [], moveCheckCustomer = [];
var vContratTypeItem = null, vAutoYearItem = null, vMonitorType = null;
var strMonitorListNew = "", strDutyUserListNew = "";
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
    $("#layout1").ligerLayout({ topHeight: 0, leftWidth: "60%", rightWidth: "40%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });
    //$("#layout1").ligerLayout({ topHeight: 100, leftWidth: "60%",rightWidth: "39%",allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });
    strPlanId = $.getUrlVar('strPlanId');
    strIfPlan = $.getUrlVar('strIfPlan');
    strDate = $.getUrlVar('strDate');

    strDate = TogetDate(new Date());
    strAskingDate = TogetDateForAfter7(new Date());
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetDict&type=Contract_Type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vContratTypeItem = data;
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
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetContratYearHistory",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vAutoYearItem = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $("#Contract_Type").ligerComboBox({ data: vContratTypeItem, width: 200, valueFieldID: 'Contract_Type_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false, onSelected: function (value, text) {
        strYear = $("#Contrat_Year").val();
        strContactType = text;
        strContratTypeId = value;
        if (strCompanyNameFrim != "" && strYear != "") {
            $("#txtProjectName").val(strCompanyNameFrim + strYear + "年度" + strContactType);
            strProjectName = $("#txtProjectName").val();
            $("#btnOk").attr("disabled", false);

            $("#btnOk").bind("click", function () {
                SaveData();
            })
        }

    }
    });
    $("#Contrat_Year").ligerComboBox({ data: vAutoYearItem, width: 200, valueFieldID: 'Contrat_Year_OP', valueField: 'ID', textField: 'YEAR', isMultiSelect: false, initValue: SetRYearboxInitValue(), onSelected: function (value, text) {
        strYear = text;
        if (strCompanyNameFrim != "" && strContactType != "") {
            $("#txtProjectName").val(strCompanyNameFrim + strYear + "年度" + strContactType);
            strProjectName = $("#txtProjectName").val();
            $("#btnOk").attr("disabled", false);

            $("#btnOk").bind("click", function () {
                SaveData();
            })
        }
    }
    });
    $("#txtDate").ligerDateEditor({ width: 200, initValue: strDate });
    //    $("#txtDate").ligerGetDateEditorManager().setDisabled();

    $("#txtAskingDate").ligerDateEditor({ width: 200, initValue: strAskingDate });
    //加载监测项目下拉列表信息信息
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetMonitorItems&strMonitorType=" + strMonitorItem_ID + "",
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


    function SetRYearboxInitValue() {
        var strValue = "";
        strValue = vAutoYearItem[0].ID;
        return strValue;
    }
    objGrid = $("#gridItems").ligerGrid({
        columns: [
                    { display: '点位', name: 'POINT_NAME', align: 'left', width: 200 },
                    { display: '监测类别', name: 'MONITOR_ID', width: 80, minWidth: 60, data: vMonitorType, render: function (items) {
                        for (var i = 0; i < vMonitorType.length; i++) {
                            if (vMonitorType[i].ID == items.MONITOR_ID) {
                                return vMonitorType[i].MONITOR_TYPE_NAME;
                            }
                        }
                        return items.MONITOR_ID;
                    }
                    }
                ],
        width: '100%',
        height: '96%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        onCheckRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            strPointId = rowindex.CONTRACT_POINT_ID;
            SelectPointItemList(strPointId);
        },
        toolbar: { items: [
                        { id: 'pointselect', text: '点位设置', click: f_PointSelected, icon: 'database' },
                { line: true },
                { id: 'add', text: '增加', click: CreateData, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: UpdateData, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: DeleteData, icon: 'delete' }
                ]
        },
        checkbox: true

    });
    $("#pageloading").hide();
    //    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    objItemGrid = $("#gridPointItems").ligerGrid({
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
        height: '96%',
        pageSizeOptions: [5, 10],
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        rownumbers: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                    { id: 'setting', text: '监测项目设置', click: itemclickOfToolbar, icon: 'database_wrench' },
                    { line: true },
                    { id: 'copy', text: '复制', click: itemclickOfToolbar, icon: 'page_copy' },
                    { line: true },
                    { id: 'paste', text: '粘贴', click: itemclickOfToolbar, icon: 'page_paste' }
                    ]
        }

    });
    $("#pageloading").hide();

    function SelectPointItemList(strContractPointId) {
        objItemGrid.set('url', '../Contract/MethodHander.ashx?action=GetContractPointItem&strContractPointId=' + strContractPointId);
    }
    function getPointItems(strID) {
        var objPointItems = [];
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../Contract/MethodHander.ashx?action=GetContractPointItem&strContractPointId=" + strID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    objPointItems = data.Rows;
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
        return objPointItems;
    }


    function itemclickOfToolbar(item) {
        switch (item.id) {

            case 'setting':
                var objrow = objGrid.getSelectedRows();
                if (objrow.length < 1) {
                    $.ligerDialog.warn('请选择监测点位！');
                    return;
                }
                if (objrow.length > 1) {
                    $.ligerDialog.warn('设置监测项目同时只能选择一个点位！');
                    return;
                }
                else {
                    var row = objrow[0];
                    strPointId = row.CONTRACT_POINT_ID;
                    strMonitorItem_ID = row.MONITOR_ID;
                    $.ligerDialog.open({ title: '监测项目设置', top: 10, width: 440, height: 380, buttons:
        [{ text: '确定', onclick: f_SaveDivItemData },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../Contract/ProgramInforDetail/ContractPointItems.aspx?strid=' + strPointId + '&ContractId=' + task_id + '&MonitorTypeId=' + strMonitorItem_ID + '&CompanyID=' + strCompanyIdFrim + '&strProject=' + encodeURI(strProjectName) + '&ContractTypeId=' + strContratTypeId
                    });
                }
                break;
            case 'copy':
                var objrow = objGrid.getSelectedRows();
                if (objrow.length < 1) {
                    $.ligerDialog.warn('请选择监测点位！');
                    return;
                }
                if (objrow.length > 1) {
                    $.ligerDialog.warn('复制监测项目同时只能选择一个点位！');
                    return;
                }
                else {
                    var rownew = objrow[0];
                    strCopyPointId = rownew.CONTRACT_POINT_ID;
                    var selected = objItemGrid.getCheckedRows();
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
            case 'paste':
                if (Copy.length <= 0) {
                    $.ligerDialog.warn('请选择要进行粘贴的监测项目！');
                    return;
                }
                var objrow = objGrid.getSelectedRows();
                if (objrow.length < 1) {
                    $.ligerDialog.warn('请选择要粘贴的目标监测点位！');
                    return;
                }
                if (objrow.length > 1) {
                    $.ligerDialog.warn('粘贴监测项目同时只能选择一个点位！');
                    return;
                }
                else {
                    var rowpaste = objrow[0];
                    strPointId = rowpaste.CONTRACT_POINT_ID;
                    strMonitorItem_ID = rowpaste.MONITOR_ID;
                    if (strPointId == strCopyPointId) {
                        $.ligerDialog.warn('当前监测点位已存在相同监测项目！');
                        return;
                    }
                    if (Copy.length > 0) {
                        SaveCopyItemData(Copy);
                    }
                    else {
                        $.ligerDialog.warn('没有要设置的监测项目！'); return;
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
            url: "../Contract/MethodHander.ashx?action=SaveDivItemData&strPointId=" + strPointId + "&strPointAddItemsId=" + strSelectItems + "&ContractId=" + task_id + "&MonitorTypeId=" + strMonitorItem_ID + "",
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
                    objItemGrid.loadData();
                    objGrid.loadData();
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
            dialog.close();
        }
    }


    //保存监测项目
    function SaveDivItemData(strSelectData, strMovedata) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../Contract/MethodHander.ashx?action=SaveDivItemData&strPointId=" + strPointId + "&strPointAddItemsId=" + strSelectData + "&strPointItemsMoveId=" + strMovedata + "&strContratId=" + task_id + "&strMonitroType=" + strMonitorItem_ID + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    objItemGrid.loadData();
                    objGrid.loadData();
                    $.ligerDialog.success('数据保存成功！');
                }
                else {
                    $.ligerDialog.warn('数据操作失败！');
                }
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载数据失败！');
            }
        });
    }

    function f_PointSelected() {

        if (task_id != "") {
            var strExistPoint = "";
            if (objGrid.recordNumber > 0) {
                var row = objGrid.data.Rows;
                if (row.length > 0) {
                    for (var i = 0; i < row.length; i++) {
                        strExistPoint += row[i].CONTRACT_POINT_ID + ";";
                    }
                    strExistPoint = strExistPoint.substring(0, strExistPoint.length - 1);
                }
            }
            $.ligerDialog.open({ title: '点位选择', top: 30, width: 600, height: 320, buttons:
        [{ text: '确定', onclick: f_SavePointData },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'PointSelect.aspx?strContractId=' + task_id + '&strPointID=' + strExistPoint
            });
        } else {
            $.ligerDialog.warn('请先选择企业信息，并生成委托书！');
        }

    }

    function f_SavePointData(item, dialog) {
        var fn = dialog.frame.GetSelectPoint || dialog.frame.window.GetSelectPoint;
        var strdata = fn();
        if (strdata != "") {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: "MonitoringPlan.ashx?action=InsertPointFreq&task_id=" + task_id + "&strPointId=" + strdata,
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    if (data == "True") {
                        LoadGridData();
                        dialog.close();
                        $.ligerDialog.success('数据操作成功！');
                    }
                    else {
                        $.ligerDialog.warn('数据操作是失败！');
                    }
                },
                error: function (msg) {
                    $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                }
            });
        }

    }
    //增加数据 加的基础数据
    function CreateData() {
        if (strCompanyIdFrim != "") {
            $.ligerDialog.open({ title: '点位信息增加', top: 0, width: 780, height: 470, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../../Base/Company/PointEdit.aspx?ContractID=' + strContratTypeId + '&CompanyID=' + strCompanyIdFrim
            });
        } else {
            $.ligerDialog.warn('请先选择企业信息，并生成委托书！');
        }
    }


    //修改数据
    function UpdateData() {
        var row = objGrid.getSelectedRow();

        if (row == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        else {
            strPointId = row.CONTRACT_POINT_ID;
            strMonitorItem_ID = row.MONITOR_ID;
            parent.$.ligerDialog.open({ title: '点位信息编辑', top: 0, width: 680, height: 500, buttons:
        [{ text: '确定', onclick: f_UpdateDate },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../Contract/ProgramInforDetail/ContractPointEdit.aspx?strid=' + strPointId + '&ContractId=' + task_id + '&MonitorTypeId=' + strMonitorItem_ID + '&CompanyID=' + strCompanyIdFrim + '&strProject=' + strProjectName + '&ContractTypeId=' + strContratTypeId
            });
        }
    }
    //删除数据
    function DeleteData() {
        var rowSelected = objGrid.getSelectedRow();
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行删除！');
            return;
        }
        else {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: "../Contract/MethodHander.ashx?action=DelContratPointForID&strContratId=" + task_id + "&strPointId=" + rowSelected.CONTRACT_POINT_ID,
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    if (data == "True") {
                        objGrid.loadData();
                        LoadGridData();
                        $.ligerDialog.success('数据操作成功！');
                    }
                    else {
                        $.ligerDialog.warn('获取数据失败！');
                    }
                },
                error: function (msg) {
                    $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                }
            });
        }
    }

    //save函数
    function f_SaveDate(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        $.ajax({
            cache: false,
            type: "POST",
            url: "../../Base/Company/PointList.aspx/SaveCompanyPointDataEx",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d != "") {
                    SaveNewPoint(data.d);
                    dialog.close();
                    //  $.ligerDialog.success('数据保存成功');
                }
                else {
                    $.ligerDialog.warn('数据保存失败');
                }
            }
        });
    }

    function SaveNewPoint(strNewPointId) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../Contract/MethodHander.ashx?action=InsertContratPointForID&strContratId=" + task_id + "&strPointId=" + strNewPointId,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data == "True") {
                    objGrid.loadData();
                    LoadGridData();

                    $.ligerDialog.success('数据保存成功！');
                    return;
                }
                else {
                    $.ligerDialog.warn('数据保存失败！');
                    return;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    //保存数据
    function f_UpdateDate(item, dialog) {
        var fnSave = dialog.frame.GetBaseInfoStr || dialog.frame.window.GetBaseInfoStr;
        var strReques = fnSave();
        SaveContractPoint(strReques);
        dialog.close();
    }

    //保存不存在的数据
    function SaveContractPoint(strReques) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../Contract/MethodHander.ashx?action=SaveContractPoint" + strReques + "",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    objGrid.loadData();
                    LoadGridData();
                    parent.$.ligerDialog.success('数据保存成功！');
                    return;
                }
                else {
                    parent.$.ligerDialog.warn('数据保存失败！');
                }
            },
            error: function () {
                parent.$.ligerDialog.warn('Ajax加载数据失败！');
            }
        });

    }

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetMonitorType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vMonitorType = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $("#Company_Name").unautocomplete();

    $("#Company_Name").autocomplete("../Contract/MethodHander.ashx?action=GetCompanyInfo",
    {
        max: 12,     // 列表里的条目数 
        minChars: 0,     // 自动完成激活之前填入的最小字符 
        matchContains: true,     // 包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示 
        autoFill: false,     // 自动填充 
        width: 250,
        max: 20,
        dataType: "json",
        scrollHeight: 150,
        parse: function (data) {

            return $.map(eval(data), function (row) {
                return {
                    data: row,

                    value: row.ID + " <" + row.COMPANY_NAME + ">",
                    result: row.COMPANY_NAME

                }
            });
        },
        formatItem: function (item) {
            //return "<font color=green>" + item.ID + "</font>&nbsp;(" + item.COMPANY_NAME + ")";
            return item.COMPANY_NAME;
        }
    }
    );
    $("#Company_Name").result(findValueCallback); //加这个主要是联动显示id

    function findValueCallback(event, data, formatted) {
        strCompanyId = data["ID"]; //获取选择的ID
        strCompanyName = data["COMPANY_NAME"];
        strAreaId = data["AREA"];
        strContactName = data["CONTACT_NAME"];
        strIndustryId = data["INDUSTRY"];
        strTelPhone = data["PHONE"];
        strAddress = data["CONTACT_ADDRESS"];

        $.ligerDialog.confirm("是否复制委托单位为受检单位\r\n?", function (result) {
            if (result == true) {
                $("#Company_NameFrim").val(strCompanyName);

                strCompanyIdFrim = strCompanyId;
                strCompanyNameFrim = strCompanyName;
                strAreaIdFrim = strAreaId;
                strContactNameFrim = strContactName;
                strIndustryIdFrim = strIndustryId;
                strTelPhoneFrim = strTelPhone;
                strAddressFrim = strAddress;
                strContactType = $("#Contract_Type").val();
                strYear = $("#Contrat_Year").val();
                if (strYear != "" && strContactType != "") {
                    $("#txtProjectName").val(strCompanyNameFrim + strYear + "年度" + strContactType);
                    strProjectName = $("#txtProjectName").val();
                    $("#btnOk").attr("disabled", false);

                    $("#btnOk").bind("click", function () {
                        SaveData();
                    })
                }
            }
        });

    }
    $("#Company_Name").bind("change", function () {
        if ($("#Company_Name").val() == "") {
            ClearValue();
        }
    })

    function ClearValue() {
        strCompanyId = "";
        strCompanyName = "";
        strContactName = "";
        strAreaId = "";
        strIndustryId = "";
        strTelPhone = "";
        strAddress = "";
    }
    $("#Company_NameFrim").unautocomplete();
    $("#Company_NameFrim").autocomplete("../Contract/MethodHander.ashx?action=GetCompanyInfo",
    {
        max: 12,     // 列表里的条目数 
        minChars: 0,     // 自动完成激活之前填入的最小字符 
        matchContains: true,     // 包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示 
        autoFill: false,     // 自动填充 
        width: 250,
        max: 20,
        dataType: "json",
        scrollHeight: 150,
        parse: function (data) {

            return $.map(eval(data), function (row) {
                return {
                    data: row,

                    value: row.ID + " <" + row.COMPANY_NAME + ">",
                    result: row.COMPANY_NAME

                }
            });
        },
        formatItem: function (item) {
            //return "<font color=green>" + item.ID + "</font>&nbsp;(" + item.COMPANY_NAME + ")";
            return item.COMPANY_NAME;
        }
    }
    );
    $("#Company_NameFrim").result(findValueCallbackFrim); //加这个主要是联动显示id

    function findValueCallbackFrim(event, data, formatted) {
        strCompanyIdFrim = data["ID"]; //获取选择的ID
        strCompanyNameFrim = data["COMPANY_NAME"];
        strAreaIdFrim = data["AREA"];
        strContactNameFrim = data["CONTACT_NAME"];
        strIndustryIdFrim = data["INDUSTRY"];
        strTelPhoneFrim = data["PHONE"];
        strAddressFrim = data["CONTACT_ADDRESS"];

        if (strYear != "" && strContactType != "") {
            $("#txtProjectName").val(strCompanyNameFrim + strYear + "年度" + strContactType);
            strProjectName = $("#txtProjectName").val();
            $("#btnOk").attr("disabled", false);

            $("#btnOk").bind("click", function () {
                SaveData();
            })
        }
    }

    $("#Company_NameFrim").bind("change", function () {
        if ($("#Company_NameFrim").val() == "") {
            ClearFrimValue();
        }
    })

    function ClearFrimValue() {
        strCompanyIdFrim = "";
        strCompanyNameFrim = "";
        strContactNameFrim = "";
        strAreaIdFrim = "";
        strIndustryIdFrim = "";
        strTelPhoneFrim = "";
        strAddressFrim = "";
    }



    function createQueryString() {
        strContratTypeId = $("#Contract_Type_OP").val();

        strReqContratId = "&strContratId=" + task_id + ""
        strParme = strReqContratId + "&strCompanyId=" + strCompanyId + "&strCompanyName=" + encodeURI(strCompanyName) + "&strIndustryId=" + strIndustryId + "&strAreaId=" + strAreaId + "&strContactName=" + encodeURI(strContactName) + "&strTelPhone=" + strTelPhone + "&strAddress=" + encodeURI(strAddress) + "";
        strParme += "&strCompanyIdFrim=" + strCompanyIdFrim + "&strCompanyNameFrim=" + encodeURI(strCompanyNameFrim) + "&strIndustryIdFrim=" + strIndustryIdFrim + "&strAreaIdFrim=" + strAreaIdFrim + "&strContactNameFrim=" + encodeURI(strContactNameFrim) + "&strTelPhoneFrim=" + strTelPhoneFrim + "&strAddressFrim=" + encodeURI(strAddressFrim) + "";
        strParme += "&strContratType=" + strContratTypeId + "&strContratYear=" + strYear + "&strBookType=0&strQuck=5&strProjectName=" + encodeURI(strProjectName);
        return strParme;
    }

    //生成委托书与委托企业信息
    function SaveData() {
        //创建参数字符串
        createQueryString();
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../Contract/MethodHander.ashx?action=InsertInfo" + strParme,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    task_id = data;
                    CreateQuckPointPlan();
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('AJAX数据请求失败！');
                return;
            }
        });
    }

    //生成委托书信息
    function CreateQuckPointPlan() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../Contract/MethodHander.ashx?action=CreateQuckPointPlan&strContratType=" + strContratTypeId + "&strContratYear=" + strYear + "&strCompanyIdFrim=" + strCompanyIdFrim + "&strContratId=" + task_id + "&strQuck=false",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    $("#btnOk").attr('disabled',true);
                    $.ligerDialog.success('数据操作成功,您可以添加点位了！');
                    return;
                } else {
                    $.ligerDialog.warn("数据保存失败！"); return;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('AJAX数据请求失败！');
                return;
            }
        });
    }
    if (!strPlanId) {
        strPlanId = "";
    }
    else {
        isEdit = true;
        //        EditDate();
    }
    if (!strIfPlan)
        strIfPlan = "";
    if (!strDate)
        strDate = "";
})

function LoadGridData() {
    if (task_id != "") {
        objGrid.set('url', 'MonitoringPlan.ashx?action=GetPointInfors&task_id=' + task_id + '&strIfPlan=' + strIfPlan);
        GetContractMonitorType();
        //CreateInputComboBox();
    }
}


function GetContractMonitorType() {
        strIfPlan = 0;
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: 'MonitoringPlan.ashx?action=GetPointMonitorInfor&task_id=' + task_id + '&strIfPlan=' + strIfPlan,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    MonitorArr = data.Rows;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    function SaveDate() {
        var MonitorTypeArr = [];
        var flag = "";
        var hidContractId = task_id;
        strCompanyId = strCompanyIdFrim;
        if (task_id == "") {
            $.ligerDialog.warn('委托书尚未生成！'); return;
        }
        else {
                SavePlan();
                return saveflag;
            }
        //委托书信息传输到后台处理复制监测任务表中
    }
function SavePlan() {
    var rowdate = objGrid.data.Rows;
    strDate = $("#txtDate").val();
    if (rowdate.length > 0) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=SavePlan&task_id=" + task_id + "&strCompanyId=" + strCompanyIdFrim + "&strDate=" + strDate,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    strPlanId = data;
                    SavePlanPoint();
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    else {
        return;
    }
}

function getPlanId() {
    return strPlanId;
}

/**
*功能描述:返回父页面请求的URL参数
*/

function getReturnPageValue() {
    var strRequest = "";
    strProjectName = $("#txtProjectName").val();
    strYear = $("#Contrat_Year").val();
    strDate = $("#txtDate").val();
    strAskingDate = $("#txtAskingDate").val();
    strRequest += "&strPlanId=" + strPlanId;
    strRequest += "&strCompanyId=" + strCompanyId;
    strRequest += "&strCompanyFrimId=" + strCompanyIdFrim;
    strRequest += "&strProjectName=" + encodeURI(strProjectName);
    strRequest += "&strContractTypeId=" + strContratTypeId;
    strRequest += "&strContractYear=" + strYear;
    strRequest += "&strTest_Type=" + getSelectedMonitor();
    strRequest += "&strDate=" + strDate;
    strRequest += "&strAskingDate=" + strAskingDate;
    //strRequest += "&strQC_Status=1";
    return strRequest;
}

function getSelectedMonitor() {
    var strMonitorList = "", strDutyUserList = "";
    var MonitorTypeArr = [];
    var rowdate = objGrid.getSelectedRows();
    for (var i = 0; i < rowdate.length; i++) {
        //循环遍历剔除选择的行的监测类型，如果不存在则直接加入数组中
        var _exist = $.inArray(rowdate[i].MONITOR_ID, MonitorTypeArr);
        if (_exist < 0) {
            MonitorTypeArr.push(rowdate[i].MONITOR_ID);
            strMonitorList += rowdate[i].MONITOR_ID + ";"
        }
    }

    return strMonitorList.substring(0, strMonitorList.length - 1);
}
//获取默认负责人信息

function SetSelectedMonitorAndDutyUser() {
    strMonitorListNew = "", strDutyUserListNew = "";
    var MonitorTypeArrNew = [];
    var rowdate = objGrid.getSelectedRows();
    for (var i = 0; i < rowdate.length; i++) {
        //循环遍历剔除选择的行的监测类型，如果不存在则直接加入数组中
        var _exist = $.inArray(rowdate[i].MONITOR_ID, MonitorTypeArrNew);
        if (_exist < 0) {
            MonitorTypeArrNew.push(rowdate[i].MONITOR_ID);
            strMonitorListNew += rowdate[i].MONITOR_ID + ";"
            strDutyUserListNew += GetDutyList(rowdate[i].MONITOR_ID) + ";"
        }
    }
    strMonitorListNew = strMonitorListNew.substring(0, strMonitorListNew.length - 1);
    strDutyUserListNew = strDutyUserListNew.substring(0, strDutyUserListNew.length - 1);
}


function GetDutyList(strMointorId) {
    var strSampleManagerId="";
    vDutyList = null;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MonitoringPlan.ashx?action=GetMonitorDutyInfor&strMonitorId=" + strMointorId + "&strType=duty_sampling",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vDutyList = data.Rows;
                if (vDutyList.length > 0) {
                    for (var i = 0; i < vDutyList.length; i++) {
                        if (vDutyList[i].IF_DEFAULT == '0') {
                            strSampleManagerId = vDutyList[i].USERID;
                        }
                    }
                }
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    return strSampleManagerId;
}

function SavePlanPeople() {
    SetSelectedMonitorAndDutyUser();
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MonitoringPlan.ashx?action=SavePlanPeople&task_id=" + task_id + "&strMonitorId=" + strMonitorListNew + "&strPlanId=" + strPlanId + "&strUserId=" + strDutyUserListNew,
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                saveflag = "1";
            }
            else {
                saveflag = "";
            }
        },
        error: function (msg) {
            saveflag = "";
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}

function SavePlanPoint() {
    var ObjData = objGrid.data.Rows;
    if (ObjData != null) {
        strFreqId = "", strPointId = "";
        for (var i = 0; i < ObjData.length; i++) {

            strFreqId += ObjData[i].ID + ';';
            strPointId += ObjData[i].CONTRACT_POINT_ID + ';';
        }
        strFreqId = strFreqId.substring(0, strFreqId.length - 1);
        strPointId = strPointId.substring(0, strPointId.length - 1);
    }

    if (strFreqId != "" && strPointId != "") {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=SavePlanPoint&task_id=" + task_id + "&strFreqId=" + strFreqId + "&strPointId=" + strPointId + "&strPlanId=" + strPlanId,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                //saveflag = "1";
                 SavePlanPeople();
                }else{
                //saveflag = "";
                }
            },
            error: function (msg) {
            //saveflag = "";
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
}

function TogetDate(date) {
    var strD = "";
    var thisYear = date.getYear();
    thisYear = (thisYear < 1900) ? (1900 + thisYear) : thisYear;
    var thisMonth = date.getMonth() + 1;
    //如果月份长度是一位则前面补0    
    if (thisMonth < 10) thisMonth = "0" + thisMonth;
    var thisDay = date.getDate();
    //如果天的长度是一位则前面补0    
    if (thisDay < 10) thisDay = "0" + thisDay;
    {

        strD = thisYear + "-" + thisMonth + "-" + thisDay;
    }
    return strD;
}

//获取当前日期的后7天的日期
function TogetDateForAfter7(date) {
    /*var strD = "";
    var thisYear = date.getYear();
    thisYear = (thisYear < 1900) ? (1900 + thisYear) : thisYear;
    var thisMonth = date.getMonth() + 1;
    //如果月份长度是一位则前面补0    
    if (thisMonth < 10) thisMonth = "0" + thisMonth;
    var thisDay = date.getDate() + 7;
    //如果天的长度是一位则前面补0    
    if (thisDay < 10) thisDay = "0" + thisDay;
    {

        strD = thisYear + "-" + thisMonth + "-" + thisDay;
    }
    return strD;
    */
    var newDate = new Date(date);
    newDate = newDate.valueOf();
    newDate = newDate + 7 * 24 * 60 * 60 * 1000;
    newDate = new Date(newDate);

    var y = newDate.getFullYear();
    var m = newDate.getMonth() + 1;
    var d = newDate.getDate();
    if (m <= 9) m = "0" + m;
    if (d <= 9) d = "0" + d;
    var strD = y + "-" + m + "-" + d;
    return strD;
}

function DelPlan() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MonitoringPlan.ashx?action=DelContractPlan&strPlanId=" + strPlanId,
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                SavePlan();
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}
//数组删除重复值
function delArr(Dataarr) {
    var a = {}, c = [], l = Dataarr.length;
    for (var i = 0; i < l; i++) {
        var b = Dataarr[i];
        var d = (typeof b) + b;
        if (a[d] === undefined) {
            c.push(b);
            a[d] = 1;
        }
    }
    return c;
}
