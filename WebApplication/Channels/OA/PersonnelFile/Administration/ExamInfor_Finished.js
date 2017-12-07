var isAdd = true, isView = false, strView = "", strType = "", strWf = "", strWFfirst = "", strIsWfStart = "1";
var vExamInforList = null, vEmployeList = null, vEmployeLeaderList = null, strEmployeID = "", strEmployeName = "", strEmployePostID = "", strEmployePostName = "", strEmployeDeptName = "",
strExamStatus = "", strtaskID = "", strExamDate = "", strExamContent = "", strExamLevel = "", strExamLeaderAppID = "", strExamLeaderAppContent = "", strExamLeaderAppDate = "", strExamType = "1", strProjectName = "", strExamLevel = "";
var strUserID = "", strLeaderName = "", strExamDeptAppContent = "", strExamDeptAppDate = "", strExamDeptAppID="",strTypeValue = "", strTypeText = "";
//流程变量
var wf_inst_task_id = "", wf_inst_id = "", wf_id = "", vFlowInfo = null;
var strExamAuditAppContent = "", strExamCheckAppContent = "", strExamPersonAppContent = "", strSupperID = "";
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


    var strImgAdminInfor = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="员工基本信息"/><span>员工基本信息</span>';
    var strImgContent = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="本人年度工作总计"/><span>本人年度工作总计</span>';
    var strImgOption = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="所在基础单位审核意见"/><span>所在基础单位审核意见</span>';

    var strImgAdminOption = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="主管领导评语及等次意见"/><span>主管领导评语及等次意见</span>';
    var strImgDeptOption = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="考核单位审核意见"/><span>考核单位审核意见</span>';
    var strImgAduitOption = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="主管单位审核意见"/><span>主管单位审核意见</span>';
    var strImgPessonOption = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="个人意见"/><span>个人意见</span>';

    var strImgCheckOption = '<img src="../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif"  alt="复核或申述情况说明"/><span>复核或申述情况说明</span>';

    $(strImgContent).appendTo(divImgContent);
    $(strImgAdminInfor).appendTo(divImgAdminInfor);
    $(strImgAdminOption).appendTo(divImgAdminOption);

    $(strImgAduitOption).appendTo(divImgAduitOption);

    $(strImgPessonOption).appendTo(divImgPessonOption);
    $(strImgDeptOption).appendTo(divImgDeptOption);
    $(strImgCheckOption).appendTo(divImgCheckOption);
    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: 1800 });

    $("#PersonDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 });
    $("#LeaderDate").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 });
    $("#EmployBirthday").ligerDateEditor({ format: "yyyy-MM-dd", initValue: currentTime(), width: 200 });

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
                    //获取委托书信息
                    GetTaskInfo();
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


    function GetTaskInfo() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "../ExamHander.ashx?action=GetEmployeExamInfor&strtaskID=" + strtaskID + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vExamInforList = data.Rows;
                    //调用用户控件，为用户控件赋值
                    GetInivalue();
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
})

function GetInivalue() {
    GetUserEmployInfor();
    GetDeptFullName();
    strSupperID = vExamInforList[0].USERID;
    GetSubUserEmployInfor(vExamInforList[0].USERID);
    $("#lbDeptName").html(strEmployeDeptName);
    $("#EmployName").val(strEmployeName);
    $("#ExamPerson").val(strEmployeName);
    


    $("#ExamLeader").val(strLeaderName);

    GetDutyText("user_sex", vEmployeList[0].SEX);
    $("#EmploySex").val(strTypeText);
    $("#EmployBirthday").val(vEmployeList[0].BIRTHDAY);

    GetDutyText("nation", vEmployeList[0].NATION);
    $("#EmployNation").val(strTypeText);

    GetDutyText("PoliticalStatus", vEmployeList[0].POLITICALSTATUS);
    $("#EmployPlo").val(strTypeText);

    GetDutyText("DegreeStatus", vEmployeList[0].EDUCATIONLEVEL);
    $("#EmployEdu").val(strTypeText);

    GetDutyText("EmployeLeve", vEmployeList[0].POST_LEVEL);
    $("#ManageLevel").val(strTypeText);

    GetDutyText("SkillGrade", vEmployeList[0].TECHNOLOGY_LEVEL);
    $("#SkillLevel").val(strTypeText);

    GetDutyText("SkillGrade", vEmployeList[0].SKILL_LEVEL);
    $("#WorkSkillLevel").val(strTypeText);
    $("#ExamContent").val(vExamInforList[0].EXAMINE_CONTENT);


    $("#EmployName").ligerTextBox({ disabled: true });
    $("#EmploySex").ligerTextBox({ disabled: true });
    $("#EmployBirthday").ligerTextBox({ disabled: true });
    $("#EmployNation").ligerTextBox({ disabled: true });
    $("#EmployPlo").ligerTextBox({ disabled: true });
    $("#EmployEdu").ligerTextBox({ disabled: true });
    $("#ManageLevel").ligerTextBox({ disabled: true });
    $("#SkillLevel").ligerTextBox({ disabled: true });
    $("#WorkSkillLevel").ligerTextBox({ disabled: true });
    $("#ExamContent").ligerTextBox({ disabled: true });
    $("#divDeptOption").attr("style", "display:");

    $("#ExamDeptOption").val(vExamInforList[0].DEPT_APP);
    $("#ExamDeptOption").ligerTextBox({ disabled: true });

    $("#OptionAdminDiv").attr("style", "display:");
    $("#ExamOption").val(vExamInforList[0].LEADER_APP);
    $("#ExamOption").ligerTextBox({ disabled: true });

    $("#divAduitOption").attr("style", "display:");
    $("#divPersonOption").attr("style", "display:");
    $("#divCheckOption").attr("style", "display:");
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
                vEmployeLeaderList = data.Rows;
                strLeaderName = vEmployeLeaderList[0].EMPLOYE_NAME;
                strExamLeaderAppID = vEmployeLeaderList[0].ID;
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
                strEmployeID = vEmployeList[0].ID;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

}

function GetDutyText(Dtype,strValue) {
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
                        if (data[i].DICT_CODE == strValue) {
                            strTypeText = data[i].DICT_TEXT;
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
//                $.ligerDialog.success('数据保存成功！');
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
    strExamAuditAppContent = $("#ExamAduitOption").val();
    strExamPersonAppContent = $("#ExamPersonOption").val(); ;
    strExamCheckAppContent = $("#ExamCheckOption").val(); ;

    strDate += "&strtaskID=" + strtaskID;
    strDate += "&strExamStatus=" + strExamStatus;
    strDate += "&strExamType=" + strExamType;
    strDate += "&strEmployeID=" + strEmployeID;
    strDate += "&strEmployeName=" + encodeURI(strEmployeName);
    strDate += "&strSupperID=" + strSupperID;
//    $("#hidContent").val(strExamContent);
    $("#hidAduitOption").val(strExamAuditAppContent);
    $("#hidPersonOption").val(strExamPersonAppContent);
    $("#hidCheckOption").val(strExamCheckAppContent);
    return strDate;
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