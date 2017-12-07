var wf_inst_task_id = "", wf_inst_id = "", wf_id = "", vFlowInfo = null;
var isAdd = true, isView = false, strView = "", strType = "", strWf = "", strWFfirst = "", strIsWfStart = "1";
var vExamInforList = null, vEmployeList = null, strEmployeID = "", strEmployeName = "", strEmployePostID = "", strEmployePostName = "", strEmployeDeptName = "",
strExamStatus = "", strtaskID = "", strExamDate = "", strExamContent = "", strExamLeaderAppID = "", strExamLeaderAppContent = "", strExamLeaderAppDate = "", strExamType = "2", strProjectName = "", strExamLevel = "";
var strUserID = "",strLeaderName="";
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

    var strImgContent = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="本年度岗位任务及完成情况"/><span>本年度岗位任务及完成情况</span>';
    var strImgOption = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="所在基础单位审核意见"/><span>所在基础单位审核意见</span>';
    var strImgSpUInfor = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="员工基本信息"/><span>员工基本信息</span>';

    $(strImgContent).appendTo(divImgContent);
    $(strImgSpUInfor).appendTo(divImgSpUInfor);
    $(strImgOption).appendTo(divImgOption);
    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: 1000 });

    $("#ExamDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 });
    $("#LeaderDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 });
    strType = $.getUrlVar('type');
    strWf = $.getUrlVar('WF_ID');
    strEmployeID = $.getUrlVar('strEmployeID');
    if (!strType) {
        //获取流程业务参数ID，得到业务ID 和流程ID
        wf_inst_task_id = $.getUrlVar("WF_INST_TASK_ID");
        wf_inst_id = $.getUrlVar("WF_INST_ID");
        wf_id = $.getUrlVar("WF_ID");


        if (wf_inst_task_id != "" & wf_inst_id != "") {

            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: "../../../Mis/Contract/ProgrammingHandler.ashx?action=GetFlowTaskInfo&wf_inst_task_id=" + wf_inst_task_id + "&wf_inst_id=" + wf_inst_id + "&service_key_name=task_id",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.Total != 0) {
                        vFlowInfo = data.Rows;
                        strtaskID = vFlowInfo[0].SERVICE_KEY_VALUE;
                        $("#hidTaskId").val(strtaskID);
                        GetEmployeExamInfor();
                    }
                },
                error: function (msg) {
                    $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                }
            });
        }
        else {
            $.ligerDialog.warn('流程业务参数错误！'); return;
        }
        if (vExamInforList != null) {
            GetSubUserEmployInfor(strUserID);
        }
        SetVauleModify();
    }
    else {
        if (strType == "true") {
            isAdd = true;
            GetUserEmployInfor();
            GetDeptFullName();
            $("#EmployName").val(strEmployeName);
            $("#PostName").val(strEmployePostName);
            $("#DeptName").val(strEmployeDeptName);
            $("#divContratSubmit").attr("style", "");
        }
        else {
            isAdd = false;
            strtaskID = $.getUrlVar('strtaskID');
            strView = $.getUrlVar('view');
            strUserID = $.getUrlVar('strUserID');
            GetEmployeExamInfor();
            if (vExamInforList != null) {
                GetSubUserEmployInfor(strUserID);
            }
            if (strView == "true") {
                isView = true;
            }
            SetVauleModify();
        }
    }
    function SetVauleModify() {
        GetDeptFullName();
        $("#ExamContent").val(vExamInforList[0].EXAMINE_CONTENT);
        $("#EmployName").val(strEmployeName);
        $("#PostName").val(strEmployePostName);
        $("#DeptName").val(strEmployeDeptName);

        $("#ExamDate").ligerGetDateEditorManager().setValue(SetDate(vExamInforList[0].EXAMINE_DATE));
        if (isView == false) {
            $("#divContratSubmit").attr("style", "");
        }

        if (isView == true) {
            $("#ExamLevel").val(vExamInforList[0].EXAM_LEVEL);
            $("#ExamOption").val(vExamInforList[0].LEADER_APP);

            GetSubUserEmployInfor(vExamInforList[0].LEADER_APP_ID);
            $("#ExamLeader").val(strEmployeName);
            $("#LeaderDate").ligerGetDateEditorManager().setValue(SetDate(vExamInforList[0].LEADER_APP_DATE));

            $("#EmployName").ligerTextBox({ disabled: true });
            $("#PostName").ligerTextBox({ disabled: true });
            $("#DeptName").ligerTextBox({ disabled: true });
            $("#ExamContent").ligerTextBox({ disabled: true });
            $("#ExamLevel").ligerTextBox({ disabled: true });
            $("#ExamLeader").ligerTextBox({ disabled: true });
            $("#LeaderDate").ligerGetDateEditorManager().setDisabled();
            $("#ExamDate").ligerGetDateEditorManager().setDisabled();
            $("#ExamOption").ligerTextBox({ disabled: true });
            $("#OptionDiv").attr("style", "");
            $("#divContratSubmit").remove();
        }
    }

    //JS 获取当前时间
    function currentTime() {

        var d = new Date(), str = '';
        if (isAdd == true) {
            str += d.getFullYear() + '-';
            str += d.getMonth() + 1 + '-';
            str += d.getDate();
        }
        else {
            if (vExamInforList != null) {
                str = vExamInforList[0].EXAMINE_DATE;
            }
        }
        return str;
    }

    // 获取当前使用流程的第一个环节
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../../Mis/Contract/MethodHander.ashx?action=GetWFfirstNode&strWF_ID=" + strWf,
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                strWFfirst = data;
            }
            else {
                $.ligerDialog.warn('流程ID设置错误，不存在该流程或未设置流程环节！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
})

function GetUserEmployInfor() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../ExamHander.ashx?action=GetUserEmployInfor",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vEmployeList = data.Rows;
                strEmployeName = vEmployeList[0].EMPLOYE_NAME;
                GetDutyText("PostDuty");
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

}

function GetSubUserEmployInfor(struID) {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../ExamHander.ashx?action=GetSubUserEmployInfor&strUserID=" + struID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vEmployeList = data.Rows;
                strEmployeName = vEmployeList[0].EMPLOYE_NAME;
                 GetDutyText("PostDuty");
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

}

function GetDutyText(Dtype) {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../../Mis/Contract/MethodHander.ashx?action=GetDict&type=" + Dtype,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    if (vEmployeList != null) {
                        if (data[i].DICT_CODE == vEmployeList[0].POST) {
                            strEmployePostName = data[i].DICT_TEXT;
                        }
                    }
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
}
    function GetDeptFullName() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../ExamHander.ashx?action=GetDeptFullName",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    strEmployeDeptName = data;
                }
                else {
                    $.ligerDialog.warn('数据加载错误！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    function GetEmployeExamInfor() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../ExamHander.ashx?action=GetEmployeExamInfor&strtaskID=" + strtaskID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vExamInforList = data.Rows;
                }
                else {
                    $.ligerDialog.warn('数据加载错误！');
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }


    function SaveExamInfor() {
        GetExamInputtInfor();
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../ExamHander.ashx?action=SaveExamInfor" + GetExamInputtInfor(),
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data != "") {
                    var DataArr = data.split('|');
                    strtaskID = DataArr[0];
                    strProjectName = DataArr[1];
                    SetHidInputValue();
                }
                else {
                    $.ligerDialog.warn('数据保存失败！');
                    return;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('AJAX数据请求失败！');
                return;
            }
        });
    }

    function GetExamInputtInfor() {
        var strDate = "";
        strExamStatus = "0";
        strExamContent = $("#ExamContent").val();
        strExamDate = $("#ExamDate").val();

    
        strDate += "&strExamStatus=" + strExamStatus;
        strDate += "&strExamType=" + strExamType;
        strDate += "&strEmployeID=" + strEmployeID;
        strDate += "&strEmployeName=" +encodeURI( strEmployeName);
//        strDate += "&strExamContent=" +strExamContent;
        strDate += "&strExamDate=" + strExamDate;
        $("#hidContent").val(strExamContent);
         return strDate;
    }
    function SetVauleModify() {
    }


    function SetHidInputValue() {
        $("#hidTaskId").val(strtaskID);
        $("#hidTaskProjectName").val(strProjectName);
        var strContent = strExamContent.length;

        $("#hidLength").val(strContent);
    }

    function SetDate(strDateValue) {
        var v = strDateValue;
        if (v == "") return;
        var createDate = new Date(Date.parse(v.replace(/-/g, '/')))
        var strData = createDate.getFullYear() + "-";
        strData += (createDate.getMonth() + 1) + "-";
        strData += createDate.getDate();
        return strData;
    }




    function SendSave() {
        SaveExamInfor();
        //防止多次点击按钮产生多个任务
        $("#divContratSubmit")[0].style.display = "none";
    }

    function Save() {
            SaveExamInfor();
    }