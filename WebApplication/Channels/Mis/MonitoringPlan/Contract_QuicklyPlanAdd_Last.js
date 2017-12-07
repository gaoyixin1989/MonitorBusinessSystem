
/// 日历控件--监测计划新增
/// 创建时间：2012-12-22
/// 创建人：胡方扬
var objGrid = null, vMonitorType = null, vPointItem = null, vMonth = null, vContractInfor = null, vDutyInitUser = null;
var task_id = "", struser = "", strmonitorID = "", monitorTypeName = "";
var tr = "", MonitorArr = [];
var saveflag = "";
var vDutyList = null;
var strPlanId = "", strIfPlan = "", strDate = "";
var strYear = "", strProjectName = "", strContratTypeId = "", strContactType = "";
var strCompanyId = "", strCompanyName = "", strIndustryId = "", strAreaId = "", strContactName = "", strTelPhone = "", strAddress = "";
var strCompanyIdFrim = "", strCompanyNameFrim = "", strIndustryIdFrim = "", strAreaIdFrim = "", strContactNameFrim = "", strTelPhoneFrim = "", strAddressFrim = "";

var strFreqId = "", strPointId = "", strInitValue = "", strMonitorItem_ID = "";
var isEdit = false;
var checkedCustomer = [], checkedMonitorArr = [], checkedPoint = [], moveCheckPoint = [], moveCheckCustomer = [];
var vContratTypeItem = null, vAutoYearItem = null, vMonitorType = null;
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
    $("#layout1").ligerLayout({ topHeight: 100, leftWidth: "70%", rightWidth: "29%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });
    strPlanId = $.getUrlVar('strPlanId');
    strIfPlan = $.getUrlVar('strIfPlan');
    strDate = $.getUrlVar('strDate');


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
        //                    { display: '监测频次', name: 'FREQ', width: 50 },
        //                    { display: '监测序号', name: 'NUM', width: 50, render: function (items) {
        //                        return '第' + items.NUM + '次';
        //                    }
        //                    }
                ],
        width: '100%',
        height: '96%',
        pageSizeOptions: [5, 10, 15, 20],
        pageSize: 10,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        toolbar: { items: [
                { id: 'add', text: '增加', click: CreateData, icon: 'add' },
                { line: true },
                { id: 'del', text: '删除', click: DeleteData, icon: 'delete' },
                                { line: true },
                { id: 'setting', text: '监测项目设置', click: SetItems, icon: 'database_wrench' }
                ]
        },
        checkbox: true
        //        onBeforeCheckRow: CkeckRowEvn,
        //        isChecked: f_isChecked, onCheckRow: f_onCheckRow

    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    function SetItems() {
        var row = objGrid.getSelectedRow();
        if (row == null) {
            $.ligerDialog.warn('请选择监测点位！');
            return;
        } else {
            strPointId = row.CONTRACT_POINT_ID;
            strMonitorItem_ID = row.MONITOR_ID;
            $.ligerDialog.open({ title: '监测项目设置', top: 10, width: 440, height: 380, buttons:
        [{ text: '确定', onclick: f_SaveDivItemData },
         { text: '返回', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../Contract/ProgramInforDetail/ContractPointItems.aspx?strid=' + strPointId + '&ContractId=' + task_id + '&MonitorTypeId=' + strMonitorItem_ID + '&CompanyID=' + strCompanyIdFrim + '&strProject=' + encodeURI(strProjectName) + '&ContractTypeId=' + strContratTypeId
            });
        }
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
    //增加数据
    function CreateData() {
        if (strCompanyIdFrim != "") {
            $.ligerDialog.open({ title: '点位信息增加', top: 0, width: 680, height: 500, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../../Base/Company/PointEdit.aspx?ContractID=' + strContratTypeId + '&CompanyID=' + strCompanyIdFrim
            });
        } else {
            $.ligerDialog.warn('请先选择企业信息，并生成委托书！');
        }
    }
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
            url: "../../Base/Company/PointList.aspx/SaveCompanyPointData",
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

    //生成委托书监测点位频次信息
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
                    LoadGridData();
                    //$.ligerDialog.success("数据保存成功！"); 
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
        CreateInputComboBox();
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
//动态创建项目负责人下拉选择框
function CreateInputComboBox() {
    var tab = $('#tabCombox');       //获取table的对象
    tab.find("tr").each(function () {
        $(this).remove();
    });
    if (MonitorArr.length > 0) {
        for (var i = 0; i < MonitorArr.length; i++) {
            GetDutyList(MonitorArr[i].ID);
            tr = "";
            tr += '<tr>';
            tr += '<th>' + MonitorArr[i].MONITOR_TYPE_NAME + '采样负责人:</th>';
            tr += '<td align="left"  class="l-table-edit-td" >  <select  id="txt' + MonitorArr[i].ID + '_Selected"  name="txt' + MonitorArr[i].ID + '_Selected" style="width:90px" >';
            tr += '<option value="" selected>=请选择=</option>';  //为Select插入一个Option(第一个位置) 
            if (vDutyList != null) {
                for (var n = 0; n < vDutyList.length; n++) {
                    tr += ' <option value=' + vDutyList[n].USERID + '>' + vDutyList[n].REAL_NAME + '</option>';
                }
                tr += '</select>';
                tr += '<input type="hidden"  id="' + MonitorArr[i].ID + 'HID"  name="' + MonitorArr[i].ID + 'HID"  value=' + MonitorArr[i].ID + ' />';
                tr += '</td>'
                $(tr).appendTo(tabCombox);
                var InitVar = SetRptboxInitValue(MonitorArr[i].ID);
                var selectOp = $('#txt' + MonitorArr[i].ID + '_Selected');
                if (InitVar != "") {
                    selectOp.children("option").each(function () {
                        if ($(this).val() == InitVar) {
                            $(this).attr("selected", true);
                        }
                    })
                }
            }
        }
    }



}

function GetDutyList(strMointorId) {
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
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}

function GetContractDutyUser(mointorid) {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MonitoringPlan.ashx?action=GetContractDutyUser&strMonitorId=" + mointorid + "&task_id=" + task_id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vDutyInitUser = data.Rows;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}

//设置ComboxBox默认值
function SetRptboxInitValue(mointorid) {
    if (!isEdit) {
        if (vDutyList.length > 0) {
            for (var i = 0; i < vDutyList.length; i++) {
                if (vDutyList[i].IF_DEFAULT == '0') {
                    strInitValue = vDutyList[i].USERID;
                }
            }
        }
    }
    else {

        GetContractDutyUser(mointorid);
        if (vDutyInitUser != null) {
            strInitValue = vDutyInitUser[0].SAMPLING_MANAGER_ID;
        }
    }

    return strInitValue;
}

function SaveDate() {
    var MonitorTypeArr = [];
    var flag = "";
    var hidContractId = task_id;
    strCompanyId =strCompanyIdFrim;
    if (task_id == "") {
        $.ligerDialog.warn('委托书尚未生成！'); return;
    }
    else {
        //        var rowdate = objGrid.getSelectedRows();
        //        var rowdate = objGrid.getCheckedRows();
//        var rowdate = checkedMonitorArr;
//        if (rowdate.length < 1) {
//            $.ligerDialog.warn('请选择排口！'); return;
//        }
        var rowdate = objGrid.data.Rows;
        for (var i = 0; i < rowdate.length; i++) {
            //循环遍历剔除选择的行的监测类型，如果不存在则直接加入数组中
            //var _exist = $.inArray(rowdate[i].MONITOR_ID, MonitorTypeArr);
            var _exist = $.inArray(rowdate[i].MONITOR_ID, MonitorTypeArr);
            if (_exist < 0) {
                //MonitorTypeArr.push(rowdate[i].MONITOR_ID);
                MonitorTypeArr.push(rowdate[i].MONITOR_ID);
            }
        }
        struser = "", strmonitorID = "", monitorTypeName = "";
        var tabi = $("#tabCombox");
        if (MonitorTypeArr.length > 0) {
            for (var i = 0; i < MonitorTypeArr.length; i++) {
                //查找指定监测类别默认负责人
                var findUserid = tabi.find("tr select[name='txt" + MonitorTypeArr[i] + "_Selected']").find('option:selected').val();
                if (findUserid != "") {
                    struser += findUserid + ";";
                    strmonitorID += MonitorTypeArr[i] + ";";
                }
                //如果指定监测类别采样负责人为空，则记录下该采样类别 方便进行提示用户选择
                else {
                    for (var n = 0; n < MonitorArr.length; n++) {
                        if (MonitorTypeArr[i] == MonitorArr[n].ID) {
                            monitorTypeName += MonitorArr[n].MONITOR_TYPE_NAME + ";";
                        }
                    }
                }
            }
        }
        //模糊查找指定控件的ID
        //遍历符合模糊查询条件的Select下拉框中选中的值

        //        tabi.find("tr select[name$='_Selected']").each(function () {
        //            if ($(this).find('option:selected').val()!= "") {
        //                struser += $(this).find('option:selected').val()+ ";";
        //            }
        //        });
        //        //监测类型值
        //        tabi.find("tr input[name$='HID']").each(function () {
        //            if ($(this).val() != "") {
        //                strmonitorID += $(this).val() + ";";
        //            }
        //        });
        struser = struser.substring(0, struser.length - 1);
        strmonitorID = strmonitorID.substring(0, strmonitorID.length - 1);
        monitorTypeName = monitorTypeName.substring(0, monitorTypeName.length - 1);
        //如果有需要选择采样负责人的 监测类别未选择负责人，则向父页面传值0，并提示。
        if (monitorTypeName != "") {
            saveflag = "0";
            $.ligerDialog.warn('请选择【<a style="color:Red; font-size:12px; font-weight:bold;">' + monitorTypeName + '</a>】类采样负责人');
        }
        //如果没有需要选择采样负责人的 监测类别未选择负责人进行保存
        else {
            var strUserArr = struser.split(';');
            var strMonitorIdArr = strmonitorID.split(';');
                SavePlan();
                return saveflag;

// else {
//                DelPlan();
//                return saveflag;
//            }
        }
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
            url: "MonitoringPlan.ashx?action=SavePlanPoint&strFreqId=" + strFreqId + "&strPointId=" + strPointId + "&strPlanId=" + strPlanId,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    SavePlanPeople();
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
}
function SavePlanPeople() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MonitoringPlan.ashx?action=SavePlanPeople&task_id=" + task_id + "&strMonitorId=" + strmonitorID + "&strPlanId=" + strPlanId + "&strUserId=" + struser,
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