
/// 日历控件--监测计划新增
/// 创建时间：2012-12-22
/// 创建人：胡方扬
var objGrid = null, vMonitorType = null, vPointItem = null, vMonth = null, vContractInfor = null,vDutyInitUser=null;
var task_id = "", struser = "", strmonitorID = "", monitorTypeName = "";
var tr = "", MonitorArr = [];
var saveflag = "";
var vDutyList = null;
var strPlanId = "", strIfPlan = "", strDate = "", strCompanyId = "", strAskingDate = "";
var strFreqId = "", strPointId = "", strInitValue = "", FreqSetting = "", strContractTypeId = "";
var isEdit = false, GetSampleFreqItems = null;
var strFreqTask = "", strFreqMonitor = "", vFreqContractType = null, vContratTypeItem = null;
var checkedCustomer = [], checkedMonitorArr = [], checkedPoint = [], moveCheckPoint = [], moveCheckCustomer = [], objColumns = null;
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
    $("#layout1").ligerLayout({ topHeight: 30, leftWidth: "70%", rightWidth: "29%", allowLeftCollapse: false, allowRightCollapse: false, height: "95%" });
    strIfPlan = 0;
    strDate = TogetDate(new Date());
    strAskingDate = TogetDateForAfter7(new Date());
    //清远启用是否使用 频次生成任务
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetDict&type=FreqTask",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                if (data.length > 0) {
                    strFreqTask = data[0].DICT_CODE;
                }
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
        url: "../Contract/MethodHander.ashx?action=GetDict&type=FreqMonitor",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                if (data.length > 0) {
                    strFreqMonitor = data[0].DICT_CODE;
                    vFreqContractType = strFreqMonitor.split(';');
                }
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
        url: "../Contract/MethodHander.ashx?action=GetDict&type=Contract_Type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                var tempArr = data;
                var TempArrNew = [];
                if (vFreqContractType != null) {
                    if (vFreqContractType.length > 0) {
                        for (var i = 0; i < vFreqContractType.length; i++) {
                            for (var n = 0; n < tempArr.length; n++) {
                                if (tempArr[n].DICT_CODE == vFreqContractType[i]) {//郑州 去掉验收类监测类别  秦皇岛 出去环评类监测类别
                                    TempArrNew.push(tempArr[n]);
                                }
                            }
                        }
                    }
                }
                vContratTypeItem = TempArrNew;
            }
            else {
                return;
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
        url: "../Contract/MethodHander.ashx?action=GetDict&type=POINT_SAMPLEFREQ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                GetSampleFreqItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax请求数据失败！');
        }
    });

    objColumns = [
                    { display: '点位', name: 'POINT_NAME', align: 'left', width: 240, render: function (items) {
                        if (items.NUM != "") {
                            return items.POINT_NAME + " (第" + "<a style='color:Red;font-weight:Bold'>" + items.NUM + "<a>" + "次)";
                        }
                        return items.POINT_NAME;
                    }
                    },
                    { display: '监测类别', name: 'MONITOR_ID', align: 'left', width: 120, minWidth: 80, data: vMonitorType, render: function (items) {
                        for (var i = 0; i < vMonitorType.length; i++) {
                            if (vMonitorType[i].ID == items.MONITOR_ID) {
                                return vMonitorType[i].MONITOR_TYPE_NAME;
                            }
                        }
                        return items.MONITOR_ID;
                    }
                    },
                   { display: '监测频次(每年/次)', name: 'FREQ', align: 'center', width: 120 }
                ];

    objGrid = $("#gridItems").ligerGrid({
        columns: objColumns,
        width: '100%',
        height: '95%',
        pageSizeOptions: [10, 15, 20, 25, 30],
        pageSize: 20,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: true,
        onBeforeCheckAllRow: function (checked, grid, element) { return false; },
        onBeforeCheckRow: CkeckRowEvn,
        isChecked: f_isChecked, onCheckRow: f_onCheckRow

    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll


    /*
    该例子实现 表单分页多选
    即利用onCheckRow将选中的行记忆下来，并利用isChecked将记忆下来的行初始化选中
    */

    function findCheckedCustomer(CustomerID, PointID, MonitorID) {
        for (var i = 0; i < checkedCustomer.length; i++) {
            if (checkedCustomer[i] == CustomerID) return i;
        }
        return -1;
    }
    function addCheckedCustomer(CustomerID, PointID, MonitorID) {
        if (findCheckedCustomer(CustomerID) == -1)
            checkedCustomer.push(CustomerID);
        checkedPoint.push(PointID);
        checkedMonitorArr.push(MonitorID);

    }
    function removeCheckedCustomer(CustomerID, PointID, MonitorID) {
        var i = findCheckedCustomer(CustomerID, PointID, MonitorID);
        if (i == -1) return;
        checkedCustomer.splice(i, 1);
        checkedPoint.splice(i, 1);
        checkedMonitorArr.splice(i, 1);
    }
    function f_isChecked(rowdata) {
        if (isEdit) {
            if (rowdata.IF_PLAN == '1') {
                checkedCustomer.push(rowdata.ID);
                checkedPoint.push(rowdata.CONTRACT_POINT_ID);
                checkedMonitorArr.push(rowdata.MONITOR_ID);
                return true;
            }
            else {
                if (findCheckedCustomer(rowdata.ID) == -1)
                    return false;
                return true;
            }
        }
        else {
            if (findCheckedCustomer(rowdata.ID) == -1)
                return false;
            return true;
        }
    }
    function f_onCheckRow(checked, data) {
        if (checked) addCheckedCustomer(data.ID, data.CONTRACT_POINT_ID, data.MONITOR_ID);
        else removeCheckedCustomer(data.ID, data.CONTRACT_POINT_ID, data.MONITOR_ID);
    }

    function CkeckRowEvn(checked, data, rowindex, rowobj) {

        if (checkedPoint.length > 0) {
            for (var i = 0; i < checkedPoint.length; i++) {
                if (checkedPoint[i] == data.CONTRACT_POINT_ID && checkedCustomer[i] != data.ID) {
                    $.ligerDialog.warn('同一时间内相同排口只能预约一个！');
                    objGrid.loadData();
                    return false;
                }
                if (checkedCustomer[i] == data.ID) {
                    checkedMonitorArr.splice(i, 1);
                    checkedPoint.splice(i, 1);
                    checkedCustomer.splice(i, 1);
                }
            }
            return true;
        }
    }

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MonitoringPlan.ashx?action=GetConfigSetting&strConfigKey=FreqSetting",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                FreqSetting = data;
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

    $("#txtContractType").ligerComboBox({ data: vContratTypeItem, width: 200, valueFieldID: 'Contract_Type_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', isMultiSelect: false, onSelected: function (value, text) {
        strContractTypeId = value;
        objGrid.set("data", []);
        $("#tabCombox").html("");
        if (value = "11") {
            SetColumn();
            objGrid.set("columns", objColumns);
        }
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    }
    });
    $("#txtProjectName").ligerComboBox({ onBeforeOpen: f_selectContact, valueFieldID: 'hidContractID', width: 310 });
    $("#txtDate").ligerDateEditor({ initValue: strDate, width: 200, onChangeDate: function (value) {
        strDate = value;
    }
    });
    $("#txtDate").ligerDateEditor({ width: 200, initValue: strDate });

    $("#txtAskingDate").ligerDateEditor({ initValue: strAskingDate, width: 200, onChangeDate: function (value) {
        strAskingDate = value;
    }
    });
    $("#txtAskingDate").ligerDateEditor({ width: 200, initValue: strAskingDate });

    $("#btnSave").bind("click", function () {
        SaveDate();
    })
})



function GetContractInfor() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MonitoringPlan.ashx?action=GetContractInfor&strPlanId=" + strPlanId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vContractInfor = data.Rows;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}

function GetContractMonitorType() {
    if (task_id != "") {

        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            //url: 'MonitoringPlan.ashx?action=GetPointMonitorInfor&task_id=' + task_id + '&strPlanId=' + strPlanId + '&strIfPlan=' + strIfPlan,
            url: 'MonitoringPlan.ashx?action=GetPointMonitorInfor&task_id=' + task_id + '&strIfPlan=' + strIfPlan,
//            url: "../Contract/MethodHander.ashx?action=GetContractMonitorType&strContratId=" + task_id,
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
}

function f_selectContact() {
    if (strContractTypeId != "") {
        $.ligerDialog.open({ title: '选择项目', name: 'winselectorProject', top: 60, width: 800, height: 600, url: 'SelectContact.aspx?strContractTypeId=' + strContractTypeId + '', buttons: [
                { text: '确定', onclick: f_selectContactOK },
                { text: '返回', onclick: f_selectContactCancel }
            ]
        });
    }
    else {
        $.ligerDialog.warn('请先选择委托类别！');
    }
    return false;
}

function f_selectContactOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        $.ligerDialog.warn('请选择行!');
        return;
    }

    $("#txtProjectName").val(data.PROJECT_NAME );
    $("#hidContractID").val(data.ID);
    $("#hidCompanyId").val(data.TESTED_COMPANY_ID);
    task_id = data.ID;
    if (task_id != "") {
        if (FreqSetting != "1") {
            objGrid.set('url', 'MonitoringPlan.ashx?action=GetPointInfors&task_id=' + task_id + '&strIfPlan=' + strIfPlan);
        } else {
//            $(".l-grid-hd-cell-btn-checkbox").css("display", ""); //隱藏checkAll
            objGrid.set('url', 'MonitoringPlan.ashx?action=GetPointInforsForFreq&task_id=' + task_id);
        }
        GetContractMonitorType();
//        var tab = $('#tabCombox');       //获取table的对象
//        tab.find("tr").each(function () {
//            $(this).remove();
        //        });
        SetColumn();
        objGrid.set('columns', objColumns);
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
        CreateInputComboBox();
    }
    dialog.close();
}

function f_selectContactCancel(item, dialog) {
    dialog.close();
}

function SetColumn() {
    if (strContractTypeId == "11") {
        objColumns = [
                    { display: '点位', name: 'POINT_NAME', align: 'left', width: 240, render: function (items) {
                        if (items.NUM != "") {
                            return items.POINT_NAME + " (第" + "<a style='color:Red;font-weight:Bold'>" + items.NUM + "<a>" + "次)";
                        }
                        return items.POINT_NAME;
                    }
                    },
                    { display: '监测类别', name: 'MONITOR_ID', width: 120, minWidth: 80, data: vMonitorType, render: function (items) {
                        for (var i = 0; i < vMonitorType.length; i++) {
                            if (vMonitorType[i].ID == items.MONITOR_ID) {
                                return vMonitorType[i].MONITOR_TYPE_NAME;
                            }
                        }
                        return items.MONITOR_ID;
                    }
                    },
                   { display: '监测频次(每年/次)', name: 'FREQ', align: 'center', width: 120 },
                   { display: '样品数目', name: 'SAMPLENUM', align: 'center', width: 120, render: function (items) {
                       if (items.SAMPLENUM != "") {
                           return items.SAMPLENUM + " 个/每次";
                       }
                       return items.SAMPLENUM;
                   }
                   }
                ];
    }
           else {
               objColumns = [
                    { display: '点位', name: 'POINT_NAME', align: 'left', width: 240, render: function (items) {
                        if (items.NUM != "") {
                            return items.POINT_NAME + " (第" + "<a style='color:Red;font-weight:Bold'>" + items.NUM + "<a>" + "次)";
                        }
                        return items.POINT_NAME;
                    }
                    },
                    { display: '监测类别', name: 'MONITOR_ID', width: 120, minWidth: 80, data: vMonitorType, render: function (items) {
                        for (var i = 0; i < vMonitorType.length; i++) {
                            if (vMonitorType[i].ID == items.MONITOR_ID) {
                                return vMonitorType[i].MONITOR_TYPE_NAME;
                            }
                        }
                        return items.MONITOR_ID;
                    }
                    },
                { display: '监测频次(每年/次)', name: 'FREQ', align: 'center', width: 120 }
                ];
    }
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
            tr+='<option value="" selected>=请选择=</option>';  //为Select插入一个Option(第一个位置) 
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
    vDutyList=null;
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
        url: "MonitoringPlan.ashx?action=GetContractDutyUser&strMonitorId="+mointorid+"&task_id="+task_id,
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
if(!isEdit){
  if (vDutyList.length > 0) {
        for (var i = 0; i < vDutyList.length; i++) {
            if (vDutyList[i].IF_DEFAULT == '0') {
                strInitValue = vDutyList[i].USERID;
            }
        }
    }
}
else{

    GetContractDutyUser(mointorid);
        if(vDutyInitUser!=null){
          strInitValue= vDutyInitUser[0].SAMPLING_MANAGER_ID;
        }
  }

  return strInitValue;
}

function SaveDate() {
    var MonitorTypeArr = [];
    var flag = "";
    var hidContractId = $("#hidContractID").val();
    strCompanyId = $("#hidCompanyId").val();
    if (hidContractId == "") {
        $.ligerDialog.warn('请选择委托项目！'); return;
    }
    else {
        //        var rowdate = objGrid.getSelectedRows();
        var rowdate =[];
        if (FreqSetting != "1") {
            rowdate = checkedMonitorArr;
        }
        else {
           var  rowdateGrid = objGrid.getCheckedRows();
           for (var i = 0; i < rowdateGrid.length; i++) {
               checkedCustomer.push(rowdateGrid[i].ID);
               checkedPoint.push(rowdateGrid[i].CONTRACT_POINT_ID);
               checkedMonitorArr.push(rowdateGrid[i].MONITOR_ID);
               rowdate = checkedMonitorArr;
            }
        }

        if (rowdate.length < 1) {
            $.ligerDialog.warn('请选择点位！');return;
        }

        for (var i = 0; i < rowdate.length; i++) {
        //循环遍历剔除选择的行的监测类型，如果不存在则直接加入数组中
            //var _exist = $.inArray(rowdate[i].MONITOR_ID, MonitorTypeArr);
            var _exist = $.inArray(rowdate[i], MonitorTypeArr);
            if (_exist < 0) {
                //MonitorTypeArr.push(rowdate[i].MONITOR_ID);
                MonitorTypeArr.push(rowdate[i]);
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
                            monitorTypeName += MonitorArr[n].MONITOR_TYPE_NAME+";";
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
            //return saveflag;

        }
     }
    //委托书信息传输到后台处理复制监测任务表中
 }

 function DoPlan() {
     var result = "";
     $.ajax({
         cache: false,
         async: false, //设置是否为异步加载,此处必须
         type: "POST",
         url: "../MonitoringPlan/PlanAdd_QY.aspx?type=doPlan&strPlanId=" + strPlanId + "&strTest_Type=" + strmonitorID + "&strAskingDate=" + strAskingDate,
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
             if (data == "1") {
                 objGrid.loadData();
                 $.ligerDialog.success('任务已发送至【<a style="color:Red;font-weight:Bold">预约办理</a>】环节！');
                 return;
                 // result = "1";
             }
             else {
                 //result = "";
                 $.ligerDialog.warn('预约计划任务生成失败！' + msg);
                 return;
             }
         },
         error: function (msg) {
             $.ligerDialog.warn('Ajax加载数据失败！' + msg);
         }
     });
     return result;
 }
 function SavePlan() {
     var rowdate = objGrid.getSelectedRows();


 if (rowdate.length > 0) {
     $.ajax({
         cache: false,
         async: false, //设置是否为异步加载,此处必须
         type: "POST",
         url: "MonitoringPlan.ashx?action=SavePlan&task_id=" + task_id + "&strCompanyId=" + strCompanyId + "&strDate=" + strDate,
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

 function SavePlanPoint() {
     if (checkedCustomer.length < 1) {
         $.ligerDialog('请选择要进行预约的数据！'); return;
     }
     strFreqId = "", strPointId = "";
     for (var i = 0; i < checkedCustomer.length; i++) {

         strFreqId += checkedCustomer[i] + ';';
         strPointId += checkedPoint[i] + ';';
     }
     strFreqId = strFreqId.substring(0, strFreqId.length - 1);
     strPointId = strPointId.substring(0, strPointId.length - 1);

     if (strFreqId != "" && strPointId != "") {
         $.ajax({
             cache: false,
             async: false, //设置是否为异步加载,此处必须
             type: "POST",
             url: "MonitoringPlan.ashx?action=SavePlanPoint&task_id="+task_id+"&strFreqId=" + strFreqId + "&strPointId=" + strPointId + "&strPlanId=" + strPlanId,
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
            if (data == "true") {
                // saveflag = "1";
                DoPlan();
            }
            else {
                $.ligerDialog.warn('预约计划生成失败！' + msg);
                //saveflag = "";
            }
        },
        error: function (msg) {
           // saveflag = "";
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


//获取当前日期的后7天的日期
function TogetDateForAfter7(date) {
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