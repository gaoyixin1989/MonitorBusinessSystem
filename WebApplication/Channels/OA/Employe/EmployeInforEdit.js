/// 站务管理--人员档案编辑
/// 创建时间：2013-01-07
/// 创建人：胡方扬

///-------------------------------------------------------------------------------------
///定义变量
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var NationItems = null, UserListItems = null;
var strEmployeID = "", strUserName = "", strUserID = "", strUserRealName = "";
///-------------------------------------------------------------------------------------

///-------------------------------------------------------------------------------------
///获取URL参数
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
///-------------------------------------------------------------------------------------

$(document).ready(function () {
    strEmployeID = $.getUrlVar('strEmployeID');

    if (!strEmployeID)
        strEmployeID = "";

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "EmployeHander.ashx?action=GetDict&type=nation",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                NationItems = data;
                //定义了sort的比较函数
                NationItems = NationItems.sort(function (a, b) {
                    return a["AUTO_LOAD"] - b["AUTO_LOAD"];
                });
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    //创建表单结构 --点位基本信息
    $("#divEdit").ligerForm({
        inputWidth: 410, labelWidth: 120, space: 40, labelAlign: 'right', modal: true,
        fields: [
        //                { display: "员工登录名", name: "LOGNAME", newline: true, type: "text",width:160, group: "基本信息", groupicon: groupicon },
        //                { display: "员工编号", name: "EMPLOYE_CODE", newline: false, width: 160, type: "text" },
        //                { display: "姓名", name: "EMPLOYE_NAME", newline: true, width: 160, type: "text" },
        //                { display: "身份证号", name: "ID_CARD", newline: false, width: 160, type: "text" },
        //                { display: "性别", name: "SEX", newline: true, width: 160, type: "select", comboboxName: "SEX_BOX", options: { valueFieldID: "SEX_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|user_sex"} },
        //                { display: "出生日期", name: "BIRTHDAY", newline: false, width: 160, type: "date" },
        //                { display: "民族", name: "NATION", newline: true, width: 160, type: "select", comboboxName: "NATION_BOX", options: { valueFieldID: "NATION_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: NationItems} },
        //                {display: "政治面貌", name: "POLITICALSTATUS", newline: false, width: 160, type: "select", comboboxName: "POLITICALSTATUS_BOX", options: { valueFieldID: "POLITICALSTATUS_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|PoliticalStatus"} },
        //                { display: "学历", name: "EDUCATIONLEVEL", newline: true, width: 160, type: "select", comboboxName: "EDUCATIONLEVEL_BOX", options: { valueFieldID: "EDUCATIONLEVEL_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|DegreeStatus"} },
        //                { display: "部门", name: "DEPART", newline: false, width: 160, type: "select", comboboxName: "DEPART_BOX", options: { valueFieldID: "DEPART_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|dept"} },
        //                { display: "职务", name: "POST", newline: true, width: 160, type: "select", comboboxName: "POST_BOX", options: { valueFieldID: "POST_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|zhiwu"} },
        //                { display: "岗位", name: "POSTION", newline: false, width: 160, type: "select", comboboxName: "POSTION_BOX", options: { valueFieldID: "POSTION_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|PostDuty"} },
        //                { display: "级别", name: "POST_LEVEL", newline: true, width: 160, type: "select", comboboxName: "POST_LEVEL_BOX", options: { valueFieldID: "POST_LEVEL_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|EmployeLeve"} },
        //                { display: "人员分类", name: "EMPLOYE_TYPE", newline: false, width: 160, type: "select", comboboxName: "EMPLOYE_TYPE_BOX", options: { valueFieldID: "EMPLOYE_TYPE_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|EmployeType"} },
        //                { display: "任现职时间", name: "POST_DATE", newline: true, width: 160, type: "date" },
        //                { display: "编制类型", name: "ORGANIZATION_TYPE", newline: false, width: 160, type: "select", comboboxName: "ORGANIZATION_TYPE_BOX", options: { valueFieldID: "ORGANIZATION_TYPE_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|Organization_Type"} },
        //                { display: "取得职称时间", name: "ORGANIZATION_DATE", newline: true, width: 160, type: "date" },
        //                { display: "参加工作时间", name: "ENTRYDATE", newline: false, width: 160, type: "date" },
        //                { display: "聘任专业技术职务", name: "TECHNOLOGY_POST", newline: true, width: 160, type: "select", comboboxName: "TECHNOLOGY_POST_BOX", options: { valueFieldID: "TECHNOLOGY_POST_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|ProfessionPost"} },
        //                { display: "入本单位时间", name: "APPLY_DATE", newline: false, width: 160, type: "date" },
        //                { display: "毕业院校", name: "GRADUATE", newline: true, width: 160, type: "text" },
        //                { display: "毕业时间", name: "GRADUATE_DATE", newline: false, width: 160, type: "date" },
        //                { display: "所学专业", name: "SPECIALITY", newline: true, width: 160, type: "text" },
        //                { display: "专业技术等级", name: "TECHNOLOGY_LEVEL", newline: false, width: 160, type: "select", comboboxName: "TECHNOLOGY_LEVEL_BOX", options: { valueFieldID: "TECHNOLOGY_LEVEL_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|TechGrade"} },
        //                { display: "工勤技能等级", name: "SKILL_LEVEL", newline: true, width: 160, type: "select", comboboxName: "SKILL_LEVEL_BOX", options: { valueFieldID: "SKILL_LEVEL_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|SkillGrade"} },
        //                { display: "工作状态", name: "POST_STATUS", newline: false, width: 160, type: "select", comboboxName: "POST_STATUS_BOX", options: { valueFieldID: "POST_STATUS_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|EmployeStatus"} },
        //                { display: "备注", name: "INFO", newline: true, type: "textarea" }
                {display: "姓名", name: "EMPLOYE_NAME", newline: true, width: 160, type: "text", group: "基本信息", groupicon: groupicon },
                { display: "部门", name: "DEPART", newline: false, width: 160, type: "select", comboboxName: "DEPART_BOX", options: { valueFieldID: "DEPART_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|dept"} },

                { display: "员工编号", name: "EMPLOYE_CODE", newline: true, width: 160, type: "text" },
                { display: "职务", name: "POST", newline: false, width: 160, type: "select", comboboxName: "POST_BOX", options: { valueFieldID: "POST_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|zhiwu"} },

                { display: "性别", name: "SEX", newline: true, width: 160, type: "select", comboboxName: "SEX_BOX", options: { valueFieldID: "SEX_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|user_sex"} },
                { display: "编制类型", name: "ORGANIZATION_TYPE", newline: false, width: 160, type: "select", comboboxName: "ORGANIZATION_TYPE_BOX", options: { valueFieldID: "ORGANIZATION_TYPE_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|Organization_Type"} },

                { display: "民族", name: "NATION", newline: true, width: 160, type: "select", comboboxName: "NATION_BOX", options: { valueFieldID: "NATION_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: NationItems} },
                { display: "专业技术职务", name: "POST_LEVEL", newline: false, width: 160, type: "select", comboboxName: "POST_LEVEL_BOX", options: { valueFieldID: "POST_LEVEL_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|EmployeLeve"} },

                { display: "出生日期", name: "BIRTHDAY", newline: true, width: 160, type: "date" },
                { display: "岗位", name: "POSTION", newline: false, width: 160, type: "select", comboboxName: "POSTION_BOX", options: { valueFieldID: "POSTION_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|PostDuty"} },

                { display: "身份证号", name: "ID_CARD", newline: true, width: 160, type: "text" },
                { display: "人员分类", name: "EMPLOYE_TYPE", newline: false, width: 160, type: "select", comboboxName: "EMPLOYE_TYPE_BOX", options: { valueFieldID: "EMPLOYE_TYPE_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|EmployeType"} },

                { display: "政治面貌", name: "POLITICALSTATUS", newline: true, width: 160, type: "select", comboboxName: "POLITICALSTATUS_BOX", options: { valueFieldID: "POLITICALSTATUS_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|PoliticalStatus"} },
                { display: "兼职情况", name: "TECHNOLOGY_POST", newline: false, width: 160, type: "select", comboboxName: "TECHNOLOGY_POST_BOX", options: { valueFieldID: "TECHNOLOGY_POST_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|ProfessionPost&Order=ORDER_ID"} },

                { display: "学历", name: "EDUCATIONLEVEL", newline: true, width: 160, type: "select", comboboxName: "EDUCATIONLEVEL_BOX", options: { valueFieldID: "EDUCATIONLEVEL_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|DegreeStatus"} },
                { display: "任现职时间", name: "POST_DATE", newline: false, width: 160, type: "date" },

                { display: "参加工作时间", name: "ENTRYDATE", newline: true, width: 160, type: "date" },
                { display: "取得职称时间", name: "ORGANIZATION_DATE", newline: false, width: 160, type: "date" },

                { display: "毕业院校", name: "GRADUATE", newline: true, width: 160, type: "text" },
                { display: "进入本单位时间", name: "APPLY_DATE", newline: false, width: 160, type: "date" },

                { display: "所学专业", name: "SPECIALITY", newline: true, width: 160, type: "text" },
                { display: "岗位等级", name: "SKILL_LEVEL", newline: false, width: 160, type: "select", comboboxName: "SKILL_LEVEL_BOX", options: { valueFieldID: "SKILL_LEVEL_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|TechGrade"} },

                { display: "毕业时间", name: "GRADUATE_DATE", newline: true, width: 160, type: "date" },
                { display: "工作状态", name: "POST_STATUS", newline: false, width: 160, type: "select", comboboxName: "POST_STATUS_BOX", options: { valueFieldID: "POST_STATUS_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|EmployeStatus"} },

        //{ display: "专业技术等级", name: "TECHNOLOGY_LEVEL", newline: false, width: 160, type: "select", comboboxName: "TECHNOLOGY_LEVEL_BOX", options: { valueFieldID: "TECHNOLOGY_LEVEL_OP", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|SkillGrade"} },
                {display: "备注", name: "INFO", newline: true, type: "textarea" }
                ]
    });

    $("#EMPLOYE_CODE").attr("validate", "[{required:true, msg:'请输入员工编号'},{minlength:2,maxlength:15,msg:'员工编号最小长度为2，最大长度为15'}]");
    $("#EMPLOYE_NAME").attr("validate", "[{required:true, msg:'请输入员工姓名'},{minlength:2,maxlength:15,msg:'员工姓名最小长度为2，最大长度为15'}]");

    //$("#ID_CARD").attr("validate", "[{iscard:true, msg:'请输入正确的身份证号!'}]");
    $("#INFO").attr("cols", "100").attr("rows", "4").attr("class", "l-textareaCl").attr("style", "width:300px");

    if (strEmployeID != "") {
        var InitDataList = [];
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "EmployeHander.ashx?action=GetEmployeInfor&strEmployeID=" + strEmployeID + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                InitDataList = data.Rows;
            }
        });
        SetInitValue();
    }

    //编辑模式下 初始化基本信息
    function SetInitValue() {
        //        if (InitDataList[0].USER_ID != "") {
        //            UserListItems = null;
        //            strUserName = "", strUserID = "";
        //            strUserID = InitDataList[0].USER_ID;
        //            GetIsExistLogName();
        //            if (UserListItems != null) {
        //                $("#LOGNAME").val(UserListItems[0].USER_NAME);
        //                $("#LOGNAME").ligerTextBox({ disabled: true });
        //            }
        //        }
        $("#EMPLOYE_CODE").val(InitDataList[0].EMPLOYE_CODE);
        $("#EMPLOYE_CODE").ligerTextBox({ disabled: true });

        $("#EMPLOYE_NAME").val(InitDataList[0].EMPLOYE_NAME);
        $("#ID_CARD").val(InitDataList[0].ID_CARD);

        $("#SEX_BOX").ligerGetComboBoxManager().setValue(InitDataList[0].SEX);
        $("#BIRTHDAY").ligerGetDateEditorManager().setValue(InitDate(InitDataList[0].BIRTHDAY));
        $("#NATION_BOX").ligerGetComboBoxManager().setValue(InitDataList[0].NATION);
        $("#POLITICALSTATUS_BOX").ligerGetComboBoxManager().setValue(InitDataList[0].POLITICALSTATUS);
        $("#EDUCATIONLEVEL_BOX").ligerGetDateEditorManager().setValue(InitDataList[0].EDUCATIONLEVEL);
        $("#DEPART_BOX").ligerGetComboBoxManager().setValue(InitDataList[0].DEPART);

        $("#POST_BOX").ligerGetComboBoxManager().setValue(InitDataList[0].POST);
        $("#POSTION_BOX").ligerGetDateEditorManager().setValue(InitDataList[0].POSITION);
        $("#POST_LEVEL_BOX").ligerGetComboBoxManager().setValue(InitDataList[0].POST_LEVEL);
        $("#EMPLOYE_TYPE_BOX").ligerGetComboBoxManager().setValue(InitDataList[0].EMPLOYE_TYPE);
        $("#POST_DATE").ligerGetDateEditorManager().setValue(InitDate(InitDataList[0].POST_DATE));

        $("#ORGANIZATION_TYPE_BOX").ligerGetComboBoxManager().setValue(InitDataList[0].ORGANIZATION_TYPE);
        $("#ORGANIZATION_DATE").ligerGetDateEditorManager().setValue(InitDate(InitDataList[0].ORGANIZATION_DATE));
        $("#ENTRYDATE").ligerGetDateEditorManager().setValue(InitDate(InitDataList[0].ENTRYDATE));
        $("#TECHNOLOGY_POST_BOX").ligerGetComboBoxManager().setValue(InitDataList[0].TECHNOLOGY_POST);

        $("#APPLY_DATE").ligerGetDateEditorManager().setValue(InitDate(InitDataList[0].APPLY_DATE));
        $("#GRADUATE").val(InitDataList[0].GRADUATE)
        $("#GRADUATE_DATE").ligerGetDateEditorManager().setValue(InitDate(InitDataList[0].GRADUATE_DATE));

        $("#SPECIALITY").val(InitDataList[0].SPECIALITY);
        //$("#TECHNOLOGY_LEVEL_BOX").ligerGetComboBoxManager().setValue(InitDataList[0].TECHNOLOGY_LEVEL);

        $("#SKILL_LEVEL_BOX").ligerGetComboBoxManager().setValue(InitDataList[0].SKILL_LEVEL);
        $("#POST_STATUS_BOX").ligerGetComboBoxManager().setValue(InitDataList[0].POST_STATUS);
        $("#INFO").val(InitDataList[0].INFO);
    }

    //    $("#LOGNAME").blur(function () {
    //        if ($(this).val() != "") {
    //            strUserName = "", strUserID = "";
    //            strUserName = $(this).val();
    //            GetIsExistLogName();
    //            if (UserListItems != null) {
    //                strUserID = UserListItems[0].ID;
    //                strUserRealName = UserListItems[0].REAL_NAME;

    //                $("#EMPLOYE_NAME").val(strUserRealName);
    //                $("#EMPLOYE_NAME").ligerTextBox({ disabled: true });
    //                $(this).val($(this).val().toLowerCase());
    //                $(this).ligerTextBox({ disabled: true });
    //            } else {
    //                $.ligerDialog.warn('用户不存在,请核实！'); return;
    //            }
    //        }
    //    })
})

//处理 日期函数
function InitDate(vDate) {
    if (vDate == "") return;
    var createDate = new Date(Date.parse(vDate.replace(/-/g, '/')))
    var strData = createDate.getFullYear() + "-";
    strData += (createDate.getMonth() + 1) + "-";
    strData += createDate.getDate();
    return strData;
}



function GetIsExistLogName() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "EmployeHander.ashx?action=GetIsExistLogName&strUserName=" + strUserName + "&strUserID=" + strUserID,
        contentType: "application/josn; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                UserListItems = data.Rows;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}

//验证用户编号是否存在
function GetIsExiststr() {
    var strData = "";
    var isTrue = $("#form1").validate();
    if (isTrue) {
        strData += "&strEmployeCode=" + $("#EMPLOYE_CODE").val();
    }
    return strData;

}



//得到基本信息保存参数
function GetBaseInfoStr() {
    //if (strUserID != "") {
    var strData = "";
    //strData += "&strUserID=" + strUserID;
    strData += "&strEmployeID=" + strEmployeID;

    strData += "&strEmployeCode=" + encodeURI($("#EMPLOYE_CODE").val());
    strData += "&strEmployeName=" + encodeURI($("#EMPLOYE_NAME").val());
    strData += "&strIdCard=" + encodeURI($("#ID_CARD").val());
    strData += "&strSex=" + encodeURI($("#SEX_OP").val());
    strData += "&strBirthday=" + $("#BIRTHDAY").val();
    strData += "&strNation=" + $("#NATION_OP").val()
    strData += "&strPoliticalStatus=" + $("#POLITICALSTATUS_OP").val();
    strData += "&strEduLevel=" + $("#EDUCATIONLEVEL_OP").val();
    strData += "&strDepart=" + encodeURI($("#DEPART_OP").val());
    strData += "&strPost=" + encodeURI($("#POST_OP").val());
    strData += "&strPostion=" + encodeURI($("#POSTION_OP").val());
    strData += "&strPostLevel=" + $("#POST_STATUS_OP").val();

    strData += "&strEmployeType=" + $("#EMPLOYE_TYPE_OP").val();
    strData += "&strPostDate=" + $("#POST_DATE").val();
    strData += "&strOrgType=" + $("#ORGANIZATION_TYPE_OP").val();
    strData += "&strOrgDate=" + $("#ORGANIZATION_DATE").val();
    strData += "&strEnterDate=" + $("#ENTRYDATE").val();
    strData += "&strTechPost=" + $("#TECHNOLOGY_POST_OP").val();
    strData += "&strApplyDate=" + $("#APPLY_DATE").val();
    strData += "&strGraduate=" + encodeURI($("#GRADUATE").val());


    strData += "&strGraduteDate=" + $("#GRADUATE_DATE").val();
    strData += "&strSpec=" + encodeURI($("#SPECIALITY").val());
    //strData += "&strTechLevel=" + $("#TECHNOLOGY_LEVEL_OP").val();
    strData += "&strSkillLevel=" + $("#SKILL_LEVEL_OP").val();
    strData += "&strPostStatus=" + $("#POST_STATUS_OP").val();
    strData += "&strInfor=" + encodeURI($("#INFO").val());
    //}
    return strData;
}
