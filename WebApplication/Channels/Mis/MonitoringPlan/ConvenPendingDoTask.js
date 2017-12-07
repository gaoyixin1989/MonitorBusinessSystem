
/// 日历控件--监测计划办理
/// 创建时间：2013-4-1
/// 创建人：胡方扬
var objGrid = null, vMonitorType = null, vPointItem = null, vMonth = null, vContractInfor = null, vDutyInitUser = null;
var task_id = "", struser = "", strmonitorID = "", monitorTypeName = "";
var tr = "", MonitorArr = [];
var saveflag = "";
var vDutyList = null;
var strPlanId = "", strIfPlan = "", strDate = "", strCompanyId = "";
var strFreqId = "", strPointId = "", strInitValue = "", strWorkTask_id = "",strAskFinishDate="";
var isEdit = false, GetSampleFreqItems = null;
var checkedCustomer = [], checkedMonitorArr = [], checkedPoint = [], moveCheckPoint = [], moveCheckCustomer = [];
var YesNoItems = null;
var strQcSetting = "", strQcStatus = "1", strAllQCStatus = "0"; //设置默认质控设置为已经设置完成
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
    $("#layout1").ligerLayout({ topHeight: 80, leftWidth: "100%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });
    strPlanId = $.getUrlVar('strPlanId');
    strIfPlan = $.getUrlVar('strIfPlan');
    strDate = $.getUrlVar('strDate');
    strProjectNmae = decodeURI($.getUrlVar('strProjectNmae'));
    task_id = $.getUrlVar('strTaskId');
    strWorkTask_id = $.getUrlVar('strWorkTask_id');
    strAskFinishDate = $.getUrlVar('strAskFinishDate');

    //获取是否启用预约质控设置环节
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MonitoringPlan.ashx?action=GetConfigSetting&strConfigKey=QCsetting",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                strQcSetting = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    //获取是否字典项
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetDict&type=company_yesno",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                YesNoItems = data;
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
    if (!strDate) {
        strDate = TogetDate(new Date());
    }
    if (!strAskFinishDate) {
        strAskFinishDate = TogetDate(new Date());
    }
    else {
        strAskFinishDate = new Date(Date.parse(strAskFinishDate.replace(/-/g, '/')));
        strAskFinishDate = TogetDate(strAskFinishDate);
    }
    $("#txtProjectName").val(strProjectNmae);
    $("#txtProjectName").ligerTextBox({ disabled: true });
    $("#txtDate").ligerDateEditor({ initValue: strDate });
    $("#txtFinishDate").ligerDateEditor({ initValue: strAskFinishDate });

    if (strQcSetting == "1") {
        $("#txtQcSet").ligerComboBox({ data: YesNoItems, width: 140, valueFieldID: 'txtQcSet_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', initValue: SetInitValue(), onSelected: function (value, text) {
            if (value == "1") {
                strQcStatus = "1";
            } else {
                strQcStatus = "3";
            }
        }
    });
    $("#txtAllQcSet").ligerComboBox({ data: YesNoItems, width: 140, valueFieldID: 'txtAllQcSet_OP', valueField: 'DICT_CODE', textField: 'DICT_TEXT', initValue: SetAllInitValue(), onSelected: function (value, text) {
        if (value == "1") {
            strAllQCStatus = "1";
        } else {
            strAllQCStatus = "0";
        }
    }
    });
    }
    else {
        $("#tdQcLab").attr("style", "padding:3px;display:none");
        $("#txtQcSet").attr("style", "padding:3px;display:none");
        $("#tdAllQcLab").attr("style", "padding:3px;display:none");
        $("#txtAllQcSet").attr("style", "padding:3px;display:none");
        $("#trQcRow").attr("style", "display:none");
        strAllQCStatus = "0";
        strQcStatus = "3";
    }
    //if (task_id != "") {
        GetContractMonitorType();
        CreateInputComboBox();
    //}
})

function SetInitValue() {
    var strVal = "";
    if (strQcSetting != "") {
        if (strQcSetting = "1") {
            strVal = "1";
            strQcStatus = "1";
        }
        else {
            strVal = "0";
            strQcStatus = "3";
        }
    }
    return strVal;
}

function SetAllInitValue() {
    var strVal = "";
    if (strQcSetting != "") {
        if (strQcSetting = "1") {
            strVal = "1";
            strAllQCStatus = "1";
        }
        else {
            strVal = "0";
            strAllQCStatus = "0";
        }
    }
    return strVal;
}
    function GetContractMonitorType() {
        //if (task_id != "") {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: 'MonitoringPlan.ashx?action=GetPointMonitorInfor&task_id=' + task_id + '&strPlanId='+strPlanId+'&strIfPlan=' + strIfPlan,
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
       // }
    }

    //动态创建项目负责人下拉选择框
    function CreateInputComboBox() {
        var tab = $('#tabCombox');       //获取table的对象
        tab.find("tr").each(function () {
            $(this).remove();
        });
        var MarrLength = MonitorArr.length;
        var Scount = 0;
        if (MonitorArr.length > 0) {
            for (var i = 0; i < MonitorArr.length; i++) {
                tr = "";
                Scount++;
                if (Scount % 3 == 0) {
                    tr += '<tr>';
                }
                GetDutyList(MonitorArr[i].ID);
                tr += '<th>' + MonitorArr[i].MONITOR_TYPE_NAME + '采样负责人:</th>';
                tr += '<td align="left" style="padding:3px;"  class="l-table-edit-td" >  <select  id="txt' + MonitorArr[i].ID + '_Selected"  name="txt' + MonitorArr[i].ID + '_Selected" style="width:90px" >';
                tr += '<option value="" selected>=请选择=</option>';  //为Select插入一个Option(第一个位置) 
                if (vDutyList != null) {
                    for (var n = 0; n < vDutyList.length; n++) {
                        tr += ' <option value=' + vDutyList[n].USERID + '>' + vDutyList[n].REAL_NAME + '</option>';
                    }
                    tr += '</select>';
                    tr += '</td>'
                }
                if (Scount % 3 == 0) {
                    tr += '</tr>';
                }
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

                if (vDutyList.length > 0) {
                    for (var i = 0; i < vDutyList.length; i++) {
                        if (vDutyList[i].IF_DEFAULT == '0') {
                            strInitValue = vDutyList[i].USERID;
                        }
                    }
                }
                else 
            {
                GetContractDutyUser(mointorid);
                if (vDutyInitUser != null) {
                    strInitValue = vDutyInitUser[0].SAMPLING_MANAGER_ID;
                }
            }
            return strInitValue;
    }

    function SaveData() {
        struser = "", strmonitorID = "", monitorTypeName = "";
        var tabi = $("#tabCombox");
        if (MonitorArr.length > 0) {
            for (var i = 0; i < MonitorArr.length; i++) {
                //查找指定监测类别默认负责人
                // var findUserid = tabi.find("tr select[name='txt" + MonitorArr[i].ID + "_Selected']").find('option:selected').val();
                var findUserid = $("#txt" + MonitorArr[i].ID + "_Selected").find("option:selected").val(); 
                if (findUserid != ""||findUserid) {
                    struser += findUserid + ";";
                    strmonitorID += MonitorArr[i].ID + ";";
                }
            }
        }
        //模糊查找指定控件的ID
        //遍历符合模糊查询条件的Select下拉框中选中的值
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
            strDate = $("#txtDate").val();
           UpdatePlan();
            return saveflag;
        }
    }

    function UpdatePlan() {
        strDate = $("#txtDate").val();
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=UpdatePlan&strPlanId=" + strPlanId + "&strDate=" + strDate,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data == "true") {
                    //SavePlanPeople();
                    SaveFinishData();
                }
                else {
                    return
                }
            },
            error: function (msg) {
                saveflag = "";
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
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


    function SaveFinishData() {
        strAskFinishDate=$("#txtFinishDate").val();
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=SaveFinishData&strWorkTask_Id=" + strWorkTask_id+"&strAskFinishDate="+strAskFinishDate+"&strQcStatus="+strQcStatus+"&strAllQcStatus="+strAllQCStatus,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data == "True") {
                    SavePlanPeople();
                }
                else {
                    return;
                }
            },
            error: function (msg) {
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